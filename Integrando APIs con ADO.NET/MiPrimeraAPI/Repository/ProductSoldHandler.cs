using MiPrimeraAPI.Controllers.DTOs;
using MiPrimeraAPI.Model;
using System.Data;
using System.Data.SqlClient;

namespace MiPrimeraAPI.Repository
{
    public static class ProductSoldHandler
    {
        public const string ConnectionString = "Server=DESKTOP-5T99G1G;DataBase=SistemaGestion;Trusted_Connection=True";

        public static string Add(PostProductSold productSold)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryAdd = @"INSERT INTO [SistemaGestion].[dbo].[ProductoVendido] (Stock, IdProducto, IdVenta)
                                        VALUES (@stock, @productId, @sellId);";

                    SqlParameter stockParameter = new SqlParameter("stock", SqlDbType.Int) { Value = productSold.Stock };
                    SqlParameter productIdParameter = new SqlParameter("productId", SqlDbType.BigInt) { Value = productSold.ProductId };
                    SqlParameter sellIdParameter = new SqlParameter("sellId", SqlDbType.BigInt) { Value = productSold.SellId };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryAdd, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(stockParameter);
                        sqlCommand.Parameters.Add(productIdParameter);
                        sqlCommand.Parameters.Add(sellIdParameter);

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

        public static List<ProductSold> Get()
        {
            List<ProductSold> getResult = new List<ProductSold>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryGet = "SELECT * FROM [SistemaGestion].[dbo].[ProductoVendido];";

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryGet, sqlConnection))
                    {
                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    ProductSold productSold = new ProductSold();

                                    productSold.Id = Convert.ToInt64(dataReader["Id"]);
                                    productSold.Stock = Convert.ToInt32(dataReader["Stock"]);
                                    productSold.ProductId = Convert.ToInt64(dataReader["IdProducto"]);
                                    productSold.SellId = Convert.ToInt64(dataReader["IdVenta"]);

                                    getResult.Add(productSold);
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

        public static string Update(ProductSold productSold)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryUpdate = @"UPDATE [SistemaGestion].[dbo].[ProductoVendido]
                    SET Stock = @stock, IdProducto = @productId, IdVenta = @sellId
                    WHERE Id = @id;";

                    SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = productSold.Id };
                    SqlParameter stockParameter = new SqlParameter("stock", SqlDbType.Int) { Value = productSold.Stock };
                    SqlParameter idProductoParameter = new SqlParameter("productId", SqlDbType.BigInt) { Value = productSold.ProductId };
                    SqlParameter idVentaParameter = new SqlParameter("sellId", SqlDbType.BigInt) { Value = productSold.SellId };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(idParameter);
                        sqlCommand.Parameters.Add(stockParameter);
                        sqlCommand.Parameters.Add(idProductoParameter);
                        sqlCommand.Parameters.Add(idVentaParameter);

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

        public static string Delete(int id)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryDelete = @"DELETE FROM [SistemaGestion].[dbo].[ProductoVendido]
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
            return "0 rows affected.";
        }
    }
}
