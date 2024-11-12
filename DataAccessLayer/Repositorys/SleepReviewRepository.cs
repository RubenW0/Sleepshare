﻿using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.IRepositorys;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Repositorys
{


    public class SleepReviewRepository : ISleepReviewRepository
    {
        private readonly string _connectionString;

        // Constructor met Dependency Injection van IConfiguration
        public SleepReviewRepository(IConfiguration configuration)
        {
            // Haal de verbindingstring uit de configuratie
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }


        public List<SleepReviewDTO> GetSleepReviews()
        {
            string query = @"SELECT sr.id, u.username, sr.sleep_rating, sr.description, sr.sleep_goal,
                             sr.sleep_duration, sr.start_time, sr.end_time, sr.date
                      FROM sleep_reviews sr
                      JOIN users u ON sr.user_id = u.id";
            List<SleepReviewDTO> sleepReviews = new List<SleepReviewDTO>();
            using (var dbConn = new MySqlConnection(_connectionString))
            {
                try
                {

                    dbConn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, dbConn))
                    {
                        using (MySqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                var review = new SleepReviewDTO
                                {
                                    Id = Convert.ToInt32(dataReader["id"]),
                                    Reviewer = dataReader["username"].ToString(),
                                    SleepRating = Convert.ToInt32(dataReader["sleep_rating"]),
                                    Description = dataReader["description"].ToString(),
                                    SleepGoal = Convert.ToInt32(dataReader["sleep_goal"]),
                                    SleepDuration = Convert.ToInt32(dataReader["sleep_duration"]),
                                    StartTime = Convert.ToDateTime(dataReader["start_time"]),
                                    EndTime = Convert.ToDateTime(dataReader["end_time"]),
                                    Date = Convert.ToDateTime(dataReader["date"])
                                };
                                sleepReviews.Add(review);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while retrieving sleep reviews.", ex);
                }
                finally
                {
                    dbConn.Close();
                }
            }

            return sleepReviews;
        }

        public bool UpdateSleepReview(SleepReviewDTO review)
        {
            string query = @"UPDATE sleep_reviews
                             SET sleep_rating = @sleep_rating,
                                 description = @description,
                                 sleep_goal = @sleep_goal,
                                 sleep_duration = @sleep_duration,
                                 start_time = @start_time,
                                 end_time = @end_time,
                                 date = @date
                             WHERE id = @id";

            using (var dbConn = new MySqlConnection(_connectionString))
                try
                {

                        dbConn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, dbConn))
                        {
                        cmd.Parameters.AddWithValue("@id", review.Id);
                        cmd.Parameters.AddWithValue("@sleep_rating", review.SleepRating);
                        cmd.Parameters.AddWithValue("@description", review.Description);
                        cmd.Parameters.AddWithValue("@sleep_goal", review.SleepGoal);
                        cmd.Parameters.AddWithValue("@sleep_duration", review.SleepDuration);
                        cmd.Parameters.AddWithValue("@start_time", review.StartTime);
                        cmd.Parameters.AddWithValue("@end_time", review.EndTime);
                        cmd.Parameters.AddWithValue("@date", review.Date);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the sleep review.", ex);
            }
            finally
            {
                dbConn.Close();
            }
        }
    }
}