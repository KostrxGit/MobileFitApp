using fitApp;
using fitApp.Data;
using fitApp.Models;
using fitApp.ViewModels;

namespace fitApp
{
    public partial class MainPage : ContentPage
    {
        private DatabaseService _databaseService;
        private StatisticsViewModel _statisticsViewModel;

        public MainPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            _statisticsViewModel = new StatisticsViewModel();
            BindingContext = _statisticsViewModel;

            var activities = _databaseService.GetActivities();
            activitiesListView.ItemsSource = activities;
            LoadActivities();

            MessagingCenter.Subscribe<ActivitiesForm>(this, "RefreshAll", (sender) =>
            {
                LoadActivities();
                _statisticsViewModel.LoadStatistics();
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
            Navigation.PushAsync(new ActivitiesForm(_statisticsViewModel));
            
        }

        
    }

}
