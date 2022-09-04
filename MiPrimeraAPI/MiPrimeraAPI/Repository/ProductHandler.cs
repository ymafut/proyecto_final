using MiPrimeraAPI.Controllers.DTOs;
using MiPrimeraAPI.Model;
using System.Data;
using System.Data.SqlClient;

namespace MiPrimeraAPI.Repository
{
    public static class ProductHandler
    {
        public const string ConnectionString = "Server=DESKTOP-5T99G1G;DataBase=SistemaGestion;Trusted_Connection=True";

        //CREATE
        public static string Add(PostProduct product)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryAdd = @"INSERT INTO [SistemaGestion].[dbo].[Producto] (Descripciones, Costo, PrecioVenta, Stock, IdUsuario)
                                            VALUES (@descriptions, @purchasePrice, @salePrice, @stock, @userId);";

                    SqlParameter descriptionsParameter = new SqlParameter("descriptions", SqlDbType.VarChar) { Value = product.Description };
                    SqlParameter purchasePriceParameter = new SqlParameter("purchasePrice", SqlDbType.Money) { Value = product.PurchasePrice };
                    SqlParameter salePriceParameter = new SqlParameter("salePrice", SqlDbType.Money) { Value = product.SalePrice };
                    SqlParameter stockParameter = new SqlParameter("stock", SqlDbType.Int) { Value = product.Stock };
                    SqlParameter userIdParameter = new SqlParameter("userId", SqlDbType.BigInt) { Value = product.UserId };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryAdd, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(descriptionsParameter);
                        sqlCommand.Parameters.Add(purchasePriceParameter);
                        sqlCommand.Parameters.Add(salePriceParameter);
                        sqlCommand.Parameters.Add(stockParameter);
                        sqlCommand.Parameters.Add(userIdParameter);

                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                        if(rowsAffected > 0)
                        {
                            return $"{rowsAffected} rows affected.";
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "0 rows affected.";
        }

        //READ
        public static List<Product> Get()
        {
            List<Product> getResult = new List<Product>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryGet = "SELECT * FROM [SistemaGestion].[dbo].[Producto];";

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryGet, sqlConnection))
                    {
                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    Product product = new Product();

                                    product.Id = Convert.ToInt64(dataReader["Id"]);
                                    product.Description = dataReader["Descripciones"].ToString();
                                    product.PurchasePrice = Convert.ToDecimal(dataReader["Costo"]);
                                    product.SalePrice = Convert.ToDecimal(dataReader["PrecioVenta"]);
                                    product.Stock = Convert.ToInt32(dataReader["Stock"]);
                                    product.UserId = Convert.ToInt64(dataReader["IdUsuario"]);

                                    getResult.Add(product);
                                }
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return getResult;
        }

        //UPDATE
        public static string Update(int id, PostProduct product)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryUpdate = @"UPDATE [SistemaGestion].[dbo].[Producto]
                    SET Descripciones = @descriptions, Costo = @purchasePrice, PrecioVenta = @salePrice, Stock = @stock, IdUsuario = @userId
                    WHERE Id = @id;";

                    SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = id };
                    SqlParameter descriptionsParameter = new SqlParameter("descriptions", SqlDbType.VarChar) { Value = product.Description };
                    SqlParameter purchasePriceParameter = new SqlParameter("purchasePrice", SqlDbType.Money) { Value = product.PurchasePrice };
                    SqlParameter salePriceParameter = new SqlParameter("salePrice", SqlDbType.Money) { Value = product.SalePrice };
                    SqlParameter stockParameter = new SqlParameter("stock", SqlDbType.Int) { Value = product.Stock };
                    SqlParameter userIdParameter = new SqlParameter("userId", SqlDbType.BigInt) { Value = product.UserId };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(idParameter);
                        sqlCommand.Parameters.Add(descriptionsParameter);
                        sqlCommand.Parameters.Add(purchasePriceParameter);
                        sqlCommand.Parameters.Add(salePriceParameter);
                        sqlCommand.Parameters.Add(stockParameter);
                        sqlCommand.Parameters.Add(userIdParameter);

                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return $"{rowsAffected} rows affected.";
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "0 rows affected.";
        }

        //DELETE
        public static string Delete(int id)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryDelete = @"DELETE FROM [SistemaGestion].[dbo].[Producto]
                                            WHERE Id = @id;";

                    SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = id };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(idParameter);

                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                        if(rowsAffected > 0)
                        {
                            return $"{rowsAffected} rows affected.";
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "0 rows affected";
        }

        //DELETE (via update) [No va porque la BDD no admite "nulls"]
        public static string UpdateToDelete(int id)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryUpdate = @"UPDATE [SistemaGestion].[dbo].[Producto] 
                    SET Descripciones = null, Costo = 0, PrecioVenta = 0, Stock = 0, IdUsuario = 0
                    WHERE Id = @id;";

                    SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = id };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(idParameter);

                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return $"{rowsAffected} rows affected.";
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "0 rows affected.";
        }

    }
}
