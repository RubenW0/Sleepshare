using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.IRepositorys;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Repositorys
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        public UserDTO GetUser(string username, string password)
        {
            string query = @"SELECT id, username, password
                             FROM users
                             WHERE username = @username AND password = @password";

            using (var dbConn = new MySqlConnection(_connectionString))
            {
                try
                {
                    dbConn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, dbConn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);  

                        using (MySqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.Read())
                            {
                                return new UserDTO
                                {
                                    Id = Convert.ToInt32(dataReader["id"]),
                                    Username = dataReader["username"].ToString(),
                                    Password = dataReader["password"].ToString()  
                                };
                            }
                            return null; 
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while retrieving the user.", ex);
                }
                finally
                {
                    dbConn.Close();
                }
            }
        }

        public bool AddUser(string username, string password)
        {
            string query = @"INSERT INTO users (username, password) VALUES (@username, @password)";

            using (var dbConn = new MySqlConnection(_connectionString))
            {
                try
                {
                    dbConn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, dbConn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password); 

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while adding the user.", ex);
                }
                finally
                {
                    dbConn.Close();
                }
            }
        }

        public bool UserExists(string username)
        {
            string query = @"SELECT COUNT(*) FROM users WHERE username = @username";

            using (var dbConn = new MySqlConnection(_connectionString))
            {
                try
                {
                    dbConn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, dbConn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0; 
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while checking if the user exists.", ex);
                }
                finally
                {
                    dbConn.Close();
                }
            }
        }


    }
}
