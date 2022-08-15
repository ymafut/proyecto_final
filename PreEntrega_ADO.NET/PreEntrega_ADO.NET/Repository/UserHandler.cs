using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreEntrega_ADO.NET
{
    public class UserHandler : DbHandler
    {
        public void Add(User usuario)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryAdd = @"INSERT INTO [SistemaGestion].[dbo].[Usuario] (Nombre, Apellido, NombreUsuario, Contraseña, Mail) 
                                    VALUES (@Nombre, @Apellido, @NombreUsuario, @Contraseña, @Mail);";

                    SqlParameter nombreParameter = new SqlParameter("Nombre", SqlDbType.VarChar) { Value = usuario.Name };
                    SqlParameter apellidoParameter = new SqlParameter("Apellido", SqlDbType.VarChar) { Value = usuario.LastName };
                    SqlParameter nombreUsuarioParameter = new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usuario.UserName };
                    SqlParameter contraseñaParameter = new SqlParameter("Contraseña", SqlDbType.VarChar) { Value = usuario.Password };
                    SqlParameter mailParameter = new SqlParameter("Mail", SqlDbType.VarChar) { Value = usuario.Email };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryAdd, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(nombreParameter);
                        sqlCommand.Parameters.Add(apellidoParameter);
                        sqlCommand.Parameters.Add(nombreUsuarioParameter);
                        sqlCommand.Parameters.Add(contraseñaParameter);
                        sqlCommand.Parameters.Add(mailParameter);
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

        public List<User> GetUser()
        {
            List<User> getresult = new List<User>();
            try
            {
                string queryGet = "SELECT * FROM [SistemaGestion].[dbo].[Usuario];";

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
                                    User user = new User();

                                    user.Id = Convert.ToInt64(dataReader["Id"]);
                                    user.Name = dataReader["Nombre"].ToString();
                                    user.LastName = dataReader["Apellido"].ToString();
                                    user.UserName = dataReader["NombreUsuario"].ToString();
                                    user.Password = dataReader["Contraseña"].ToString();
                                    user.Email = dataReader["Mail"].ToString();

                                    getresult.Add(user);
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
            return getresult;
        }

        public void Udpate(User usuario)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryUpdate = @"UPDATE [SistemaGestion].[dbo].[Usuario] 
                    SET Nombre = @Nombre, Apellido = @Apellido, NombreUsuario = @NombreUsuario, Contraseña = @Contraseña, Mail = @Mail
                    WHERE Id = @Id;";

                    SqlParameter idParameter = new SqlParameter("Id", SqlDbType.BigInt) { Value = usuario.Id };
                    SqlParameter nombreParameter = new SqlParameter("Nombre", SqlDbType.VarChar) { Value = usuario.Name };
                    SqlParameter apellidoParameter = new SqlParameter("Apellido", SqlDbType.VarChar) { Value = usuario.LastName };
                    SqlParameter nombreUsuarioParameter = new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usuario.UserName };
                    SqlParameter contraseñaParameter = new SqlParameter("Contraseña", SqlDbType.VarChar) { Value = usuario.Password };
                    SqlParameter mailParameter = new SqlParameter("Mail", SqlDbType.VarChar) { Value = usuario.Email };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(idParameter);
                        sqlCommand.Parameters.Add(nombreParameter);
                        sqlCommand.Parameters.Add(apellidoParameter);
                        sqlCommand.Parameters.Add(nombreUsuarioParameter);
                        sqlCommand.Parameters.Add(contraseñaParameter);
                        sqlCommand.Parameters.Add(mailParameter);
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

        public void Delete(User usuario)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryDelete = @"DELETE FROM [SistemaGestion].[dbo].[Usuario] 
                                            WHERE Id = @Id;";

                    SqlParameter idParameter = new SqlParameter("Id", SqlDbType.BigInt) { Value = usuario.Id };

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

        public bool Login(string userName, string password)
        {
            bool login = false;
            try
            {

                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryLogin = @"SELECT NombreUsuario, Contraseña FROM [SistemaGestion].[dbo].[Usuario]
                                        WHERE NombreUsuario = @userName AND Contraseña = @password;";

                    SqlParameter userNameParameter = new SqlParameter("userName", SqlDbType.VarChar) { Value = userName };
                    SqlParameter passwordParameter = new SqlParameter("password", SqlDbType.VarChar) { Value = password };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryLogin, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(userNameParameter);
                        sqlCommand.Parameters.Add(passwordParameter);

                        SqlDataReader dataReader = sqlCommand.ExecuteReader();

                        if (dataReader.HasRows)
                        {
                            login = true;
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return login;
        }
    }
}
 