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

        //ValidCREATE
        public static bool FindUserByUserName(string userName)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryGet = @"SELECT NombreUsuario FROM [SistemaGestion].[dbo].[Usuario]
                                        WHERE NombreUsuario = @userName;";

                    SqlParameter userNameParameter = new SqlParameter("userName", SqlDbType.VarChar) { Value = userName };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryGet, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(userNameParameter);

                        using (SqlDataReader rowsAffected = sqlCommand.ExecuteReader())
                        {
                            if (rowsAffected.HasRows)
                            {
                                return true;
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
            return false;
        }

        //READ
        public static User Get(string userName)
        {
            User userRequested = new User();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryGet = @"SELECT * FROM [SistemaGestion].[dbo].[Usuario]
                                        WHERE NombreUsuario = @userName;";

                    SqlParameter userNameParameter = new SqlParameter("userName", SqlDbType.VarChar) { Value = userName };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryGet, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(userNameParameter);

                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    userRequested.Id = Convert.ToInt64(dataReader["Id"]);
                                    userRequested.Name = dataReader["Nombre"].ToString();
                                    userRequested.LastName = dataReader["Apellido"].ToString();
                                    userRequested.UserName = dataReader["NombreUsuario"].ToString();
                                    userRequested.Password = dataReader["Contraseña"].ToString();
                                    userRequested.Email = dataReader["Mail"].ToString();
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
            return userRequested;
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

        //DELETE (via update) [No va porque la BDD no admite "nulls"]
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

        //LOGIN
        public static string Login(PostLogin user)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryLogin = @"SELECT NombreUsuario, Contraseña FROM [SistemaGestion].[dbo].[Usuario]
                                        WHERE NombreUsuario = @userName AND Contraseña = @password;";

                    SqlParameter userNameParameter = new SqlParameter("userName", SqlDbType.VarChar) { Value = user.UserName };
                    SqlParameter passwordParameter = new SqlParameter("password", SqlDbType.VarChar) { Value = user.Password };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryLogin, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(userNameParameter);
                        sqlCommand.Parameters.Add(passwordParameter);

                        using (SqlDataReader loginSuccesful = sqlCommand.ExecuteReader())
                        {
                            if (loginSuccesful.HasRows)
                            {
                                return $"Bienvenido {user.UserName}.";
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
            return "Nombre de Usuario y Contraseña incorrectos.";
        }
    }
}
