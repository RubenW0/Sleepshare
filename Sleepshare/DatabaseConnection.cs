using MySql.Data.MySqlClient;
using Sleepshare.Models;
using System;
using System.Collections.Generic;

public class DatabaseConnection
{
    private MySqlConnection connection;
    private string server = "127.0.0.1";
    private string database = "sleepshare";
    private string uid = "root";
    private string password = "";

    public DatabaseConnection()
    {
        string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
        connection = new MySqlConnection(connectionString);
    }

    public bool OpenConnection()
    {
        try
        {
            connection.Open();
            return true;
        }
        catch (MySqlException ex)
        {
            // Handle the exception (log it, rethrow it, etc.)
            return false;
        }
    }

    public bool CloseConnection()
    {
        try
        {
            connection.Close();
            return true;
        }
        catch (MySqlException ex)
        {
            // Handle the exception
            return false;
        }
    }

    public List<SleepReview> GetSleepReviews()
    {
        string query = @"
        SELECT sr.id, u.username, sr.sleep_rating, sr.description, sr.sleep_goal, 
               sr.sleep_duration, sr.start_time, sr.end_time, sr.date 
        FROM sleep_reviews sr
        JOIN users u ON sr.user_id = u.id";

        List<SleepReview> sleepReviews = new List<SleepReview>();

        try
        {
            using (MySqlConnection conn = new MySqlConnection($"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};"))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            SleepReview review = new SleepReview
                            {
                                Reviewer = dataReader["username"].ToString(),  // Vervang user_id door username
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
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving sleep reviews.", ex);
        }

        return sleepReviews;
    }

    public bool UpdateSleepReview(int id, int sleepRating, string description, int sleepGoal, int sleepDuration, DateTime startTime, DateTime endTime, DateTime date)
    {
        string query = @"
    UPDATE sleep_reviews
    SET sleep_rating = @sleep_rating,
        description = @description,
        sleep_goal = @sleep_goal,
        sleep_duration = @sleep_duration,
        start_time = @start_time,
        end_time = @end_time,
        date = @date
    WHERE id = @id";

        using (MySqlConnection conn = new MySqlConnection($"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};"))
        {
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@sleep_rating", sleepRating);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@sleep_goal", sleepGoal);
                cmd.Parameters.AddWithValue("@sleep_duration", sleepDuration);
                cmd.Parameters.AddWithValue("@start_time", startTime);
                cmd.Parameters.AddWithValue("@end_time", endTime);
                cmd.Parameters.AddWithValue("@date", date);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0; // Return true als er rijen zijn bijgewerkt
            }
        }
    }


}
