using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.IRepositorys;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Repositorys
{
    public class FollowerRepository : IFollowerRepository
    {
        private readonly string _connectionString;

        public FollowerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        public void FollowUser(int userId, int followsId)
        {
            string checkQuery = "SELECT COUNT(*) FROM followers WHERE user_id_follower = @userId AND user_id_followed = @followsId";
            string insertQuery = "INSERT INTO followers (user_id_follower, user_id_followed) VALUES (@userId, @followsId)";

            using (var dbConn = new MySqlConnection(_connectionString))
            {
                dbConn.Open();

                using (var checkCmd = new MySqlCommand(checkQuery, dbConn))
                {
                    checkCmd.Parameters.AddWithValue("@userId", userId);
                    checkCmd.Parameters.AddWithValue("@followsId", followsId);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        throw new InvalidOperationException($"User {userId} is already following user {followsId}.");
                    }
                }

                using (var insertCmd = new MySqlCommand(insertQuery, dbConn))
                {
                    insertCmd.Parameters.AddWithValue("@userId", userId);
                    insertCmd.Parameters.AddWithValue("@followsId", followsId);
                    insertCmd.ExecuteNonQuery();
                }
            }
        }


        public void UnfollowUser(int userId, int followsId)
        {
            string query = "DELETE FROM followers WHERE user_id_follower = @userId AND user_id_followed = @followsId";

            using (var dbConn = new MySqlConnection(_connectionString))
            {
                dbConn.Open();
                using (var cmd = new MySqlCommand(query, dbConn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@followsId", followsId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<FollowerDTO> GetFollowers(int userId)
        {
            string query = @"SELECT f.user_id_follower, f.user_id_followed, u.username 
                             FROM followers f
                             JOIN users u ON f.user_id_followed = u.id
                             WHERE f.user_id_follower = @userId";

            var followers = new List<FollowerDTO>();

            using (var dbConn = new MySqlConnection(_connectionString))
            {
                dbConn.Open();
                using (var cmd = new MySqlCommand(query, dbConn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            followers.Add(new FollowerDTO
                            {
                                UserId = Convert.ToInt32(reader["user_id_follower"]),
                                FollowsId = Convert.ToInt32(reader["user_id_followed"]),
                                Username = reader["username"].ToString()
                            });
                        }
                    }
                }
            }

            return followers;
        }

        public List<int> GetFollowedUserIds(int userId)
        {
            string query = "SELECT user_id_followed FROM followers WHERE user_id_follower = @userId";
            var followedUserIds = new List<int>();

            using (var dbConn = new MySqlConnection(_connectionString))
            {
                dbConn.Open();
                using (var cmd = new MySqlCommand(query, dbConn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            followedUserIds.Add(Convert.ToInt32(reader["user_id_followed"]));
                        }
                    }
                }
            }

            return followedUserIds;
        }

        public List<UserDTO> GetAllUsers()
        {
            string query = "SELECT id, username FROM users";

            var users = new List<UserDTO>();

            using (var dbConn = new MySqlConnection(_connectionString))
            {
                dbConn.Open();
                using (var cmd = new MySqlCommand(query, dbConn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new UserDTO
                            {
                                UserId = Convert.ToInt32(reader["id"]),
                                Username = reader["username"].ToString()
                            });
                        }
                    }
                }
            }

            return users;
        }

        public bool IsFollowing(int userId, int followsId)
        {
            string query = "SELECT COUNT(*) FROM followers WHERE user_id_follower = @userId AND user_id_followed = @followsId";

            using (var dbConn = new MySqlConnection(_connectionString))
            {
                dbConn.Open();
                using (var cmd = new MySqlCommand(query, dbConn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@followsId", followsId);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

    }
}
