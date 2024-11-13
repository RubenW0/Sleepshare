//using Microsoft.Extensions.Configuration;
//using MySql.Data.MySqlClient;
//using System;
//using System.IO;


//namespace DataAccessLayer
//{
//    public class DatabaseConnection
//    {
//        private readonly MySqlConnection connection;

//        public DatabaseConnection(IConfiguration configuration)
//        {
//            // Retrieve connection string from appsettings.json
//            string connectionString = configuration.GetConnectionString("MySqlConnection");
//            connection = new MySqlConnection(connectionString);
//        }

//        public MySqlConnection GetConnection()
//        {
//            return connection;
//        }


//        public bool OpenConnection()
//        {
//            try
//            {
//                if (connection.State == System.Data.ConnectionState.Closed)
//                {
//                    connection.Open();
//                }
//                return true;
//            }
//            catch (MySqlException ex)
//            {
//                // Handle exception
//                return false;
//            }
//        }

//        public bool CloseConnection()
//        {
//            try
//            {
//                if (connection.State == System.Data.ConnectionState.Open)
//                {
//                    connection.Close();
//                }
//                return true;
//            }
//            catch (MySqlException ex)
//            {
//                // Handle exception
//                return false;
//            }
//        }
//    }
//}
