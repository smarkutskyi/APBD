using System.Data;
using Cwiczenie9.Models.DTOs;
using Microsoft.Data.SqlClient;
using PrzykladowyKolokwium1.Exeption;

namespace Cwiczenie9.Services;

public class WarehouseService : IWarehouseService
{
    private readonly string _connectionString;
    public WarehouseService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Cwiczenie9") ?? string.Empty;
    }

    public async Task<int> DodajemyProduktDoMagazynuAsync(WarehouseDTO warehouse)
    {
        
        

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var transaction = connection.BeginTransaction();
        await using var cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.Transaction = transaction;

        try
        {
            

            var cmdText1 = @"SELECT * FROM Warehouse WHERE IdWarehouse = @Id";
            
            cmd.CommandText = cmdText1;
            cmd.Parameters.AddWithValue("@Id", warehouse.IdWarehouse);
            var czyIstniejeMagazyn = await cmd.ExecuteScalarAsync();
            if (czyIstniejeMagazyn == null)
            {
                throw new NotFoundException("Magazyn nie istnieje");
            }
            
            cmd.Parameters.Clear();

            var cmdText2 = @"SELECT * FROM Product WHERE IdProduct = @Id";

            cmd.CommandText = cmdText1;
            cmd.Parameters.AddWithValue("@Id", warehouse.IdProduct);
            var czyIstniejeProdukt = await cmd.ExecuteScalarAsync();
            if (czyIstniejeProdukt == null)
            {
                throw new NotFoundException("Produkt nie istnieje");
            }
            
            cmd.Parameters.Clear();
            
            var cmdText3 = @"SELECT TOP 1 o.IdOrder 
                FROM [Order] o
                LEFT JOIN Product_Warehouse pw ON o.IdOrder = pw.IdOrder
                WHERE o.IdProduct = @Id AND o.CreatedAt < @CreatedAt AND o.Amount = @Amount 
                  AND pw.IdProductWarehouse IS NULL ";
            
            cmd.CommandText = cmdText3;
            cmd.Parameters.AddWithValue("@Id", warehouse.IdProduct);
            cmd.Parameters.AddWithValue("@CreatedAt", warehouse.CreatedAt);
            cmd.Parameters.AddWithValue("@Amount", warehouse.Amount);

            var idZamuwienia = await cmd.ExecuteScalarAsync();

            if (idZamuwienia == null)
            {
                throw new NotFoundException("Nie znaleziono pasującego zamówienia");

            }
            
            var cmdText4 = @"UPDATE Order SET FulfilledAt = GETDATE() WHERE IdOrder = @IdZamuwienia";
            
            cmd.CommandText = cmdText4;
            cmd.Parameters.AddWithValue("@IdZamuwienia", idZamuwienia);

            
            try
            {
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                
                throw new ConflictException("Przeszkoda w zmianie daty (może wyzwalać)");
            }
            
            cmd.Parameters.Clear();

            var cmdText5 = @"SELECT Price FROM Product WHERE IdProduct = @IdProduct";
            cmd.CommandText = cmdText5;
            cmd.Parameters.AddWithValue("@IdProduct", idZamuwienia);
            var cenaProduktu = (decimal) await cmd.ExecuteScalarAsync();
            
            cmd.Parameters.Clear();
            var cmdText6 = @"INSERT INTO Product_Warehouse(IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt)
                output inserted.IdProductWarehouse
                VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, GETDATE())";
            cmd.CommandText = cmdText6;
            cmd.Parameters.AddWithValue("@IdWarehouse", warehouse.IdWarehouse);
            cmd.Parameters.AddWithValue("@IdProduct", warehouse.IdProduct);
            cmd.Parameters.AddWithValue("@IdOrder", idZamuwienia);
            cmd.Parameters.AddWithValue("@Amount", warehouse.Amount);
            
            var cenaKoncowa = cenaProduktu * warehouse.Amount;
            cmd.Parameters.AddWithValue("@Price", cenaKoncowa);

            var idProduktuWMagazynie = 0;
            try
            {
                idProduktuWMagazynie = (int) await cmd.ExecuteScalarAsync();
            }
            catch (Exception e)
            {
                
                throw new ConflictException("Przeszkoda w dodawaniu produktu do magazynu");
            }


            transaction.Commit();
            
            return idProduktuWMagazynie;
            
            
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw;
        }


    }

    public async Task<int> dodajemyProduktDoMagazynuProceduraAsync(WarehouseDTO warehouse)
    {
        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand("AddProductToWarehouse", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@IdProduct", warehouse.IdProduct);
        command.Parameters.AddWithValue("@IdWarehouse", warehouse.IdWarehouse);
        command.Parameters.AddWithValue("@Amount", warehouse.Amount);
        command.Parameters.AddWithValue("@CreatedAt", warehouse.CreatedAt);

        await connection.OpenAsync();

        try
        {
            var result = await command.ExecuteScalarAsync();
            
            if (result == null)
                throw new Exception("Procedura nic nie zwróciła");
            
            return (int) result;
        }
        catch (SqlException ex)
        {
            
            if (ex.Message.Contains("Invalid parameter: Provided IdProduct does not exist"))
            {
                
                throw new NotFoundException("Produkt nie istnieje w bazie danych");
                
            }
            
            if (ex.Message.Contains("Invalid parameter: There is no order to fullfill"))
            {
                
                throw new NotFoundException("Nie znaleziono pasującego zamówienia");
            }
            
            if (ex.Message.Contains("Invalid parameter: Provided IdWarehouse does not exist"))
            {
                
                throw new NotFoundException("Magazyn nie istnieje");
            }

            
            throw new ConflictException("Przeszkoda w dodawaniu produktu do magazynu: " + ex.Message);
        }
        catch (Exception ex)
        {
            
            throw new Exception("Wystąpił błąd podczas dodawania produktu do magazynu: " + ex.Message);
        }
    
    }
}