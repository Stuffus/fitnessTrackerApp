//initializing variables that will be used in the viewmodels
namespace fitnessTrackerApp.Model
{
    public class PageModel
    {
        public int userID { get; set; }

        public string currentPage { get; set; }
        public bool isLoggedIn { get; set; }
        public bool showNotification { get; set; }


        public string login { get; set; }
        public string password { get; set; }
        public string email { get; set; }
     
        public int workoutCount { get; set; }
        public int workoutID { get; set; }
        public string workoutName { get; set; }
        public double userWeight { get; set; }

        public Exercise CurrentExercise { get; set; } = new Exercise();
        public List<Exercise> Exercises { get; set; } = new();
    }
}
