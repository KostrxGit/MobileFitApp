using fitApp;
using fitApp.Data;
using fitApp.Models;

namespace fitApp
{
    public partial class MainPage : ContentPage
    {
        private DatabaseService _databaseService;
       
        public MainPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();

            var activities = _databaseService.GetActivities();
            activitiesListView.ItemsSource = activities;
            LoadActivities();

            MessagingCenter.Subscribe<ActivitiesForm>(this, "RefreshActivities", (sender) =>
            {
                LoadActivities();
            });
        }

        public void LoadActivities()
        {
            var activities = _databaseService.GetActivities();
            activitiesListView.ItemsSource = activities;
        }

        public void OnDeleteClicked(object sender, EventArgs e)
        {
            var activityId = (int)(sender as Button).CommandParameter;
            _databaseService.DeleteActivityById(activityId);

            LoadActivities();
        }


        public void OpenPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ActivitiesForm());
        }

        
    }

}
