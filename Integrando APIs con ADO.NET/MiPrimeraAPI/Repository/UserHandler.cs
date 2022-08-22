using MiPrimeraAPI.Controllers.DTOs;
using MiPrimeraAPI.Model;
using System.Data;
using System.Data.SqlClient;

namespace MiPrimeraAPI.Repository
{
    public static class UserHandler
    {
        public const string ConnectionString = "Server=DESKTOP-5T99G1G;DataBase=SistemaGestion;Trusted_Connection=True";

        //CREATE
        public static string Add(PostUser user)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryAdd = @"INSERT INTO [SistemaGestion].[dbo].[Usuario] (Nombre, Apellido, NombreUsuario, Contraseña, Mail) 
                                        VALUES (@name, @lastName, @userName, @password, @email);";

                    SqlParameter nameParameter = new SqlParameter("name", SqlDbType.VarChar) { Value = user.Name };
                    SqlParameter lastNameParameter = new SqlParameter("lastName", SqlDbType.VarChar) { Value = user.LastName };
                    SqlParameter userNameParameter = new SqlParameter("userName", SqlDbType.VarChar) { Value = user.UserName };
                    SqlParameter passwordParameter = new SqlParameter("password", SqlDbType.VarChar) { Value = user.Password };
                    SqlParameter emailParameter = new SqlParameter("email", SqlDbType.VarChar) { Value = user.Email };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryAdd, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(nameParameter);
                        sqlCommand.Parameters.Add(lastNameParameter);
                        sqlCommand.Parameters.Add(userNameParameter);
                        sqlCommand.Parameters.Add(passwordParameter);
                        sqlCommand.Parameters.Add(emailParameter);

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
            return $"0 rows affected.";
        }

        //READ
        public static List<User> Get()
        {
            List<User> getResult = new List<User>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryGet = "SELECT * FROM [SistemaGestion].[dbo].[Usuario];";

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryGet, sqlConnection))
                    {
                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    User user = new User();

                                    user.Id = Convert.ToInt64(dataReader["Id"]);
                                    user.Name = dataReader["Nombre"].ToString();
                                    user.LastName = dataReader["Apellido"].ToString();
                                    user.UserName = dataReader["NombreUsuario"].ToString();
                                    user.Password = dataReader["Contraseña"].ToString();
                                    user.Email = dataReader["Mail"].ToString();

                                    getResult.Add(user);
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
        public static string Update(User user)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryUpdate = @"UPDATE [SistemaGestion].[dbo].[Usuario] 
                    SET Nombre = @name, Apellido = @lastName, NombreUsuario = @userName, Contraseña = @password, Mail = @email
                    WHERE Id = @id;";

                    SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = user.Id };
                    SqlParameter nameParameter = new SqlParameter("name", SqlDbType.VarChar) { Value = user.Name };
                    SqlParameter lastNameParameter = new SqlParameter("lastName", SqlDbType.VarChar) { Value = user.LastName };
                    SqlParameter userNameParameter = new SqlParameter("userName", SqlDbType.VarChar) { Value = user.UserName };
                    SqlParameter passwordParameter = new SqlParameter("password", SqlDbType.VarChar) { Value = user.Password };
                    SqlParameter emailParameter = new SqlParameter("email", SqlDbType.VarChar) { Value = user.Email };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(idParameter);
                        sqlCommand.Parameters.Add(nameParameter);
                        sqlCommand.Parameters.Add(lastNameParameter);
                        sqlCommand.Parameters.Add(userNameParameter);
                        sqlCommand.Parameters.Add(passwordParameter);
                        sqlCommand.Parameters.Add(emailParameter);

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

        //DELETE
        public static string Delete(int id)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryDelete = @"DELETE FROM [SistemaGestion].[dbo].[Usuario] 
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

        //DELETE (via update)
        public static string UpdateToDelete(int id)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryUpdate = @"UPDATE [SistemaGestion].[dbo].[Usuario] 
                    SET Nombre = null, Apellido = null, NombreUsuario = null, Contraseña = null, Mail = null
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
