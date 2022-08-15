using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreEntrega_ADO.NET
{
    public class ProductSoldHandler : DbHandler
    {
        public void Add(ProductSold productSold)
        {
            try
            {
                using(SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryAdd = @"INSERT INTO [SistemaGestion].[dbo].[ProductoVendido] (Stock, IdProducto, IdVenta)
                                            VALUES (@stock, @idProducto, @idVenta);";

                    SqlParameter stockParameter = new SqlParameter("stock", SqlDbType.Int) { Value = productSold.Stock};
                    SqlParameter idProductoParameter = new SqlParameter("idProducto", SqlDbType.BigInt) { Value = productSold.ProductId};
                    SqlParameter idVentaParameter = new SqlParameter("idVenta", SqlDbType.BigInt) { Value = productSold.SellId };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryAdd, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(stockParameter);
                        sqlCommand.Parameters.Add(idProductoParameter);
                        sqlCommand.Parameters.Add(idVentaParameter);
                        sqlCommand.ExecuteScalar();
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<ProductSold> GetProductSold()
        {
            List<ProductSold> getResult = new List<ProductSold>();
            try
            {
                string queryGet = "SELECT * FROM [SistemaGestion].[dbo].[ProductoVendido];";

                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
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

        public void Udpate(ProductSold productSold)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryUpdate = @"UPDATE [SistemaGestion].[dbo].[ProductoVendido]
                                        SET Stock = @stock, IdProducto = @idProducto, IdVenta = @idVenta
                                        WHERE Id = @id;";

                    SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = productSold.Id };
                    SqlParameter stockParameter = new SqlParameter("stock", SqlDbType.Int) { Value = productSold.Stock };
                    SqlParameter idProductoParameter = new SqlParameter("idProducto", SqlDbType.BigInt) { Value = productSold.ProductId };
                    SqlParameter idVentaParameter = new SqlParameter("idVenta", SqlDbType.BigInt) { Value = productSold.SellId };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(idParameter);
                        sqlCommand.Parameters.Add(stockParameter);
                        sqlCommand.Parameters.Add(idProductoParameter);
                        sqlCommand.Parameters.Add(idVentaParameter);
                        sqlCommand.ExecuteNonQuery();
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }   
        }

        public void Delete(ProductSold productSold)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryDelete = @"DELETE FROM [SistemaGestion].[dbo].[ProductoVendido]
                                            WHERE Id = @id;";

                    SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = productSold.Id };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(idParameter);
                        sqlCommand.ExecuteNonQuery();
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
