using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace PreEntrega_ADO.NET
{
    public class ProductHandler : DbHandler
    {
        public void Add(Product producto)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryAdd = @"INSERT INTO [SistemaGestion].[dbo].[Producto] (Descripciones, Costo, PrecioVenta, Stock, IdUsuario)
                                            VALUES (@Descripciones, @Costo, @PrecioVenta, @Stock, @IdUsuario);";

                    SqlParameter descripcionesParameter = new SqlParameter("Descripciones", SqlDbType.VarChar) { Value = producto.Description };
                    SqlParameter costoParameter = new SqlParameter("Costo", SqlDbType.Money) { Value = producto.PurchasePrice };
                    SqlParameter precioVentaParameter = new SqlParameter("PrecioVenta", SqlDbType.Money) { Value = producto.SalePrice };
                    SqlParameter stockParameter = new SqlParameter("Stock", SqlDbType.Int) { Value = producto.Stock };
                    SqlParameter idUsuarioParameter = new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = producto.UserId };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryAdd, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(descripcionesParameter);
                        sqlCommand.Parameters.Add(costoParameter);
                        sqlCommand.Parameters.Add(precioVentaParameter);
                        sqlCommand.Parameters.Add(stockParameter);
                        sqlCommand.Parameters.Add(idUsuarioParameter);
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

        public List<Product> GetProduct()
        {
            List<Product> getResult = new List<Product>();
            try
            {
                string queryGet = "SELECT * FROM [SistemaGestion].[dbo].[Producto];";

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

        public void Update(Product producto)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryUpdate = @"UPDATE [SistemaGestion].[dbo].[Producto] 
                                        SET Descripciones = @descripciones,
                                            Costo = @costo, 
                                            PrecioVenta = @precioVenta, 
                                            Stock = @stock, 
                                            IdUsuario = @idUsuario
                                        WHERE Id =  @id;";

                    SqlParameter descripcionesParameter = new SqlParameter("descripciones", SqlDbType.VarChar) { Value = producto.Description };
                    SqlParameter costoParameter = new SqlParameter("costo", SqlDbType.Money) { Value = producto.PurchasePrice };
                    SqlParameter precioVentaParameter = new SqlParameter("precioVenta", SqlDbType.Money) { Value = producto.SalePrice };
                    SqlParameter stockParameter = new SqlParameter("stock", SqlDbType.Int) { Value = producto.Stock };
                    SqlParameter idUsuarioParameter = new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = producto.UserId };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(descripcionesParameter);
                        sqlCommand.Parameters.Add(costoParameter);
                        sqlCommand.Parameters.Add(precioVentaParameter);
                        sqlCommand.Parameters.Add(stockParameter);
                        sqlCommand.Parameters.Add(idUsuarioParameter);
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

        public void Delete(Product producto)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryDelete = "DELETE FROM [SistemaGestion].[dbo].[Producto] WHERE Id = @id";

                    SqlParameter sqlParameter = new SqlParameter("id", SqlDbType.BigInt);
                    sqlParameter.Value = producto.Id;

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(sqlParameter);
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
    }
}



