using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.IRepositorys;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Repositorys
{
    public class SleepReviewRepository : ISleepReviewRepository
    {
        private readonly string _connectionString;

        public SleepReviewRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        //public SleepReviewRepository(AppConfiguration appConfig)
        //{
        //    _connectionString = appConfig.GetConnectionString();
        //}


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
                                    Date = Convert.ToDateTime(dataReader["date"]).Date
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

        public List<SleepReviewDTO> GetSleepReviewsByUserId(int userId)
        {
            string query = @"SELECT sr.id, u.username, sr.sleep_rating, sr.description, sr.sleep_goal,
                             sr.sleep_duration, sr.start_time, sr.end_time, sr.date
                      FROM sleep_reviews sr
                      JOIN users u ON sr.user_id = u.id
                      WHERE sr.user_id = @user_id";

            List<SleepReviewDTO> sleepReviews = new List<SleepReviewDTO>();

            using (var dbConn = new MySqlConnection(_connectionString))
            {
                try
                {
                    dbConn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, dbConn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", userId);

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
                                    Date = Convert.ToDateTime(dataReader["date"]).Date
                                };
                                sleepReviews.Add(review);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred while retrieving sleep reviews for user ID {userId}.", ex);
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

        public bool AddSleepReview(SleepReviewDTO review)
        {
            string query = @"INSERT INTO sleep_reviews (user_id, sleep_rating, description, sleep_goal,
                                                sleep_duration, start_time, end_time, date)
                     VALUES (@user_id, @sleep_rating, @description, @sleep_goal, 
                             @sleep_duration, @start_time, @end_time, @date)";

            using (var dbConn = new MySqlConnection(_connectionString))
            {
                try
                {
                    dbConn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, dbConn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", review.UserId);
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
                    throw new Exception("An error occurred while adding the sleep review.", ex);
                }
                finally
                {
                    dbConn.Close();
                }
            }
        }

        public bool DeleteSleepReview(int reviewId)
        {
            string query = @"DELETE FROM sleep_reviews WHERE id = @id";

            using (var dbConn = new MySqlConnection(_connectionString))
            {
                try
                {
                    dbConn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, dbConn))
                    {
                        cmd.Parameters.AddWithValue("@id", reviewId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while deleting the sleep review.", ex);
                }
                finally
                {
                    dbConn.Close();
                }
            }
        }

        public List<SleepReviewDTO> GetSleepReviewsByUserIds(List<int> userIds)
        {
            if (userIds == null || userIds.Count == 0)
                return new List<SleepReviewDTO>();

            string query = @"SELECT sr.id, u.username, sr.sleep_rating, sr.description, sr.sleep_goal,
                             sr.sleep_duration, sr.start_time, sr.end_time, sr.date, sr.user_id
                      FROM sleep_reviews sr
                      JOIN users u ON sr.user_id = u.id
                      WHERE sr.user_id IN (" + string.Join(",", userIds.Select(id => $"@user_id{id}")) + ")";

            List<SleepReviewDTO> sleepReviews = new List<SleepReviewDTO>();
            using (var dbConn = new MySqlConnection(_connectionString))
            {
                try
                {
                    dbConn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, dbConn))
                    {
                        foreach (var id in userIds)
                        {
                            cmd.Parameters.AddWithValue($"@user_id{id}", id);
                        }

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
                                    Date = Convert.ToDateTime(dataReader["date"]).Date,
                                    UserId = Convert.ToInt32(dataReader["user_id"])
                                };
                                sleepReviews.Add(review);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while retrieving sleep reviews by user IDs.", ex);
                }
                finally
                {
                    dbConn.Close();
                }
            }

            return sleepReviews;
        }


    }
}