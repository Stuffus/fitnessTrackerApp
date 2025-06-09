using fitnessTrackerApp.Model;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Text.RegularExpressions;


namespace fitnessTrackerApp.Utilities
{
    public static partial class DatabaseHelper
    {
        private const string DbFilePath = "Data/fitness.db";
        private const string ConnectionString = $"Data Source={DbFilePath}";

        public static void InitializeDatabase()
        {
            var folder = Path.GetDirectoryName(DbFilePath);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (!File.Exists(DbFilePath))
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                        CREATE TABLE Users (
                            user_id INTEGER PRIMARY KEY AUTOINCREMENT,
                            login TEXT NOT NULL UNIQUE,
                            password TEXT NOT NULL,
                            email TEXT NOT NULL UNIQUE,
                            user_weight REAL NULL
                        );

                        CREATE TABLE Workouts (
                            workout_id INTEGER PRIMARY KEY AUTOINCREMENT, 
                            user_id INTEGER NOT NULL,
                            workout_name TEXT,
                            workout_date TEXT,              
                            FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE
                        );

                        CREATE TABLE Exercises (
                            exercise_id INTEGER PRIMARY KEY AUTOINCREMENT,
                            workout_id INTEGER NOT NULL,
                            set_number INTEGER NOT NULL, 
                            exercise_name TEXT,
                            exercise_description TEXT NULL,
                            rep_count INTEGER,
                            rep_weight REAL,
                            FOREIGN KEY (workout_id) REFERENCES Workouts(workout_id) ON DELETE CASCADE
                        );

                    ";

                command.ExecuteNonQuery();
            }
        }

        public static void PopulateDbIfEmpty()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var checkCommand = connection.CreateCommand();
            checkCommand.CommandText = "SELECT COUNT(*) FROM Users";
            var count = Convert.ToInt32(checkCommand.ExecuteScalar());

            if (count == 0)
            {
                var populateCommand = connection.CreateCommand();
                populateCommand.CommandText =
                    @"
                        INSERT INTO Users(email, login, password, user_weight) VALUES
                        ('yurrski@gmail.com', 'stwz', '1234', 75.0),
                        ('aboba@gmail.com', 'aboba', '1234', 80.0);

                        INSERT INTO Workouts (user_id, workout_name, workout_date) VALUES
                        (1, 'Chest Day', '01/03/2025 9:00 am'),
                        (2, 'Back Day', '02/06/2025 6:30 pm');

                        INSERT INTO Exercises (workout_id, set_number, exercise_name, exercise_description, rep_count, rep_weight) VALUES
                        (1, 1, 'Bench Press', 'Flat bench press with barbell', 10, 60.0),
                        (1, 2, 'Bench Press', 'Flat bench press with barbell', 8, 65.0),
                        (1, 1, 'Pull-ups', NULL , 8, 0),
                        (2, 1, 'Deadlift', 'Barbell deadlift', 6, 100.0),
                        (1, 2, 'Lunges', 'Walking lunges with dumbbells', 12, 20.0);
                    ";
                populateCommand.ExecuteNonQuery();
            }
        }

        public static bool ValidateUser(string username, string password)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT COUNT(1)
                    FROM Users
                    WHERE login = $username AND password = $password;
                ";

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    return false;
                }

                command.Parameters.AddWithValue("$username", username);
                command.Parameters.AddWithValue("$password", password);

                try
                {
                    var result = command.ExecuteScalar();
                    var count = Convert.ToInt32(result);
                    if (count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public static bool RegisterUser(string username, string password, string email)
        {
            // validate basic null/empty
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
                return false;

            // username must be at least 4 characters
            if (username.Length < 4 || password.Length < 4) 
                return false;

            // basic email format check using regex
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailPattern))
                return false;

            // check if user already exists
            if (ValidateUser(username, password))
                return false;

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                   INSERT INTO Users (login, password, email, user_weight)
                   VALUES ($username, $password, $email, NULL);
               ";

                command.Parameters.AddWithValue("$username", username);
                command.Parameters.AddWithValue("$password", password);
                command.Parameters.AddWithValue("$email", email);

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static int GetUserID(string username)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                        SELECT user_id 
                        FROM Users
                        WHERE login = $username
                    ";

                if (string.IsNullOrEmpty(username))
                {
                    return 0;
                }
                command.Parameters.AddWithValue("$username", username);
                var result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        public static int GetWorkoutID(string workoutName)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                        SELECT workout_id 
                        FROM Workouts
                        WHERE workout_name = $workoutName
                    ";

                if (string.IsNullOrEmpty(workoutName))
                {
                    return 0;
                }
                command.Parameters.AddWithValue("$workoutName", workoutName);
                var result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        public static bool CreateWorkout(int userID, string workoutName) 
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var workoutCommand = connection.CreateCommand();

            workoutCommand.CommandText = 
                @"
                    INSERT INTO Workouts (user_id, workout_name, workout_date)
                    VALUES($userID, $workoutName, $dateTime)    
                ";
            workoutCommand.Parameters.AddWithValue("$userID", userID);
            workoutCommand.Parameters.AddWithValue("$workoutName", workoutName);

            string date = DateTime.Now.ToString("g") ;
            workoutCommand.Parameters.AddWithValue("$dateTime", date);

            try
            {
                workoutCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool CreateExercise(int workoutID, int setNumber, string exerciseName, string? exerciseDescription, int repCount, double repWeight)
        {
            // input validation
            if (setNumber <= 0)
                return false;

            if (string.IsNullOrWhiteSpace(exerciseName))
                return false;

            if (repCount <= 0)
                return false;

            if (repWeight < 0)
                return false;

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var exerciseCommand = connection.CreateCommand();
            exerciseCommand.CommandText =
                @"
                    INSERT INTO Exercises (workout_id, set_number, exercise_name, exercise_description, rep_count, rep_weight)
                    VALUES ($workoutID, $setNumber, $exerciseName, $exerciseDescription, $repCount, $repWeight)
                ";

            exerciseCommand.Parameters.AddWithValue("$workoutID", workoutID);
            exerciseCommand.Parameters.AddWithValue("$setNumber", setNumber);
            exerciseCommand.Parameters.AddWithValue("$exerciseName", exerciseName);

            if (string.IsNullOrWhiteSpace(exerciseDescription))
            {
                exerciseCommand.Parameters.AddWithValue("$exerciseDescription", DBNull.Value);
            }
            else
            {
                exerciseCommand.Parameters.AddWithValue("$exerciseDescription", exerciseDescription);
            }

            exerciseCommand.Parameters.AddWithValue("$repCount", repCount);
            exerciseCommand.Parameters.AddWithValue("$repWeight", repWeight);

            try
            {
                exerciseCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static bool SetUserWeight(int userID, double userWeight)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var weightCommand = connection.CreateCommand();
            weightCommand.CommandText =
                @"
                    UPDATE Users
                    SET user_weight = $userWeight
                    where user_id = $userID
                ";
            weightCommand.Parameters.AddWithValue("$userWeight", userWeight);
            weightCommand.Parameters.AddWithValue("$userID", userID);

            try
            {
                weightCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<Workout> GetWorkoutsWithExercises(int userID)
        {
            var workoutsDict = new Dictionary<int, Workout>();

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText =
                @"
            SELECT 
                w.workout_id,          
                w.workout_name,        
                w.workout_date,        
                e.set_number,          
                e.exercise_name,       
                e.exercise_description,
                e.rep_count,          
                e.rep_weight           
            FROM Workouts w
            LEFT JOIN Exercises e ON w.workout_id = e.workout_id
            WHERE w.user_id = $userID
            ORDER BY w.workout_id, e.set_number;
        ";
            cmd.Parameters.AddWithValue("$userID", userID);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int workoutID = reader.GetInt32(0);
                string workoutName = reader.IsDBNull(1) ? $"Workout {workoutID}" : reader.GetString(1);
                DateTime workoutDate = reader.GetDateTime(2);

                if (!workoutsDict.TryGetValue(workoutID, out var workout))
                {
                    workout = new Workout(workoutID, workoutName, workoutDate);
                    workoutsDict[workoutID] = workout;
                }

                // exercise_name is not null
                if (!reader.IsDBNull(4))
                {
                    var exercise = new Exercise(
                        workoutID,
                        reader.GetInt32(3),                     
                        reader.GetString(4),                    
                        reader.IsDBNull(5) ? "" : reader.GetString(5), 
                        reader.GetInt32(6),                      
                        reader.GetDouble(7),                     
                        workoutDate                              
                    );

                    workout.Exercises.Add(exercise);
                }
            }

            return workoutsDict.Values.ToList();
        }



    }
}

