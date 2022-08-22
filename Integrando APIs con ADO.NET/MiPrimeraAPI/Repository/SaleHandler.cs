using MiPrimeraAPI.Controllers.DTOs;
using MiPrimeraAPI.Model;
using System.Data;
using System.Data.SqlClient;

namespace MiPrimeraAPI.Repository
{
    public class SaleHandler
    {
        public const string ConnectionString = "Server=DESKTOP-5T99G1G;DataBase=SistemaGestion;Trusted_Connection=True";

        public static string Add(PostSale sale)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryAdd = @"INSERT INTO [SistemaGestion].[dbo].[Venta] (Comentarios)
                                        VALUES (@comments);";

                    SqlParameter commentsParameter = new SqlParameter("comments", SqlDbType.VarChar) { Value = sale.Comments };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryAdd, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(commentsParameter);   

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

        public static List<Sale> Get()
        {
            List<Sale> getResult = new List<Sale>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryGet = "SELECT * FROM [SistemaGestion].[dbo].[Venta];";

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryGet, sqlConnection))
                    {
                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    Sale sale = new Sale();

                                    sale.Id = Convert.ToInt64(dataReader["Id"]);
                                    sale.Comments = dataReader["Comentarios"].ToString();

                                    getResult.Add(sale);
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

        public static string Update(Sale sale)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryUpdate = @"UPDATE [SistemaGestion].[dbo].[Venta] 
                                        SET Comentarios = @comments
                                        WHERE Id = @id;";

                    SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = sale.Id };
                    SqlParameter commentsParameter = new SqlParameter("comments", SqlDbType.VarChar) { Value = sale.Comments };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(idParameter);
                        sqlCommand.Parameters.Add(commentsParameter);

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

        public static string Delete(int id)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryDelete = @"DELETE FROM [SistemaGestion].[dbo].[Venta]
                                            WHERE Id = @id;";

                    SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = id };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
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
