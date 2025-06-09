// initializing a Class Workout that will store the workout data from the db
// as well as a list of exercises for that workout
using fitnessTrackerApp.Model;

public class Workout
{
    public int WorkoutID { get; set; }
    public string WorkoutName { get; set; }
    public DateTime WorkoutDate { get; set; }
    public List<Exercise> Exercises { get; set; } = new();

    public Workout(int workoutId, string name, DateTime date)
    {
        WorkoutID = workoutId;
        WorkoutName = name;
        WorkoutDate = date;
        Exercises = new List<Exercise>();
    }

}
