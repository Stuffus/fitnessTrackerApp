//initializing an Exercise class that will store values from the db
namespace fitnessTrackerApp.Model
{
    public class Exercise
    {
        public int SetNumber { get; set; }
        public string ExerciseName { get; set; }
        public string ExerciseDescription { get; set; }
        public int RepCount { get; set; }
        public double RepWeight { get; set; }
        public int WorkoutID { get; set; }
        public DateTime WorkoutDate { get; set; }


        public Exercise() { }

        public Exercise(int workoutID, int setNumber, string exerciseName, string exerciseDescription,
            int repCount, double repWeight, DateTime workoutDate)
        {
            WorkoutID = workoutID;
            SetNumber = setNumber;
            ExerciseName = exerciseName;
            ExerciseDescription = exerciseDescription;
            RepCount = repCount;
            RepWeight = repWeight;
            WorkoutDate = workoutDate;
        }
    }
}

