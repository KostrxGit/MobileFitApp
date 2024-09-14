using fitApp.Data;
using fitApp.Models;
using fitApp.ViewModels;
using System;

namespace fitApp;

public partial class ActivitiesForm : ContentPage
{
    private readonly DatabaseService _databaseService;  // Definiujemy pole klasy
    private readonly StatisticsViewModel _statisticsViewModel;
    public ActivitiesForm(StatisticsViewModel statisticsViewModel)
	{
		InitializeComponent();
        _databaseService = new DatabaseService();  // Inicjalizujemy pole klasy
        _statisticsViewModel = new StatisticsViewModel();
    }


    public async void OnSaveClicked(object sender, EventArgs e)
    {
        int calories = 0;
        int kilometers;
        int minutes;

        // Bezpieczna konwersja tekstu na liczbê
        bool kmParseSuccess = int.TryParse(kmEntry.Text, out kilometers);
        bool timeParseSuccess = int.TryParse(timeEntry.Text, out minutes);

        if (!kmParseSuccess || !timeParseSuccess)
        {
            await DisplayAlert("Error", "Please enter valid numbers for kilometers and minutes.", "OK");
            return;
        }

        var pickActivity = pickerActivity.SelectedItem;
        if (pickActivity == null)
        {
            await DisplayAlert("Error", "Please select an activity.", "OK");
            return;
        }

        switch ((string)pickActivity)
        {
            case "Bieganie":
                calories = 10 * minutes;
                break;
            case "Jazda na rowerze":
                calories = 15 * minutes;
                break;
            case "P³ywanie":
                calories = 25 * minutes;
                break;
            default:
                await DisplayAlert("Error", "Invalid activity selected.", "OK");
                return;
        }

        // Save the activity
        Activites activity = new()
        {
            Name = (string)pickActivity,
            Calories = calories,
            Kilometers = kilometers,
            Minutes = minutes
        };

        // Save the activity
        try
        {
            _databaseService.SaveActivity(activity);

            _statisticsViewModel.LoadStatistics();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to save activity: {ex.Message}", "OK");
            return;
        }

        // Go back to the main page
        await Navigation.PopAsync();
        MessagingCenter.Send(this, "RefreshAll");
        
    }
}