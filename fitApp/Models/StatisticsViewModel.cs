using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using fitApp.Data;

namespace fitApp.ViewModels
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        private int _totalKilometers;
        private int _avgKilometers;
        private int _totalCalories;
        private int _avgCalories;

        public event PropertyChangedEventHandler PropertyChanged;

        public int TotalKilometers
        {
            get => _totalKilometers;
            set
            {
                _totalKilometers = value;
                OnPropertyChanged(); // Powiadamia widok o zmianie
            }
        }

        public int AvgKilometers 
        {
            get => (int)_avgKilometers;
            set
            {
                _avgKilometers = value;
                OnPropertyChanged();
            }
        }

        public int TotalCalories
        {
            get => _totalCalories;
            set { _totalCalories = value; OnPropertyChanged(); }
        }

        public int AvgCalories 
        {
            get => _avgCalories;
            set
            {
                _avgCalories = value;
                OnPropertyChanged();
            }
        }

        private DatabaseService _databaseService;

        // Konstruktor
        public StatisticsViewModel()
        {
            _databaseService = new DatabaseService();

            // Pobierz sumę kilometrów i kalorii
            LoadStatistics();
        }

        public void LoadStatistics()
        {
            TotalKilometers = _databaseService.GetTotalKilometers();
            AvgKilometers = _databaseService.GetAvgKilometers();
            TotalCalories = _databaseService.GetTotalCalories();
            AvgCalories = _databaseService.GetAvgCalories();
            // Możesz dodać podobną metodę dla kalorii, jeśli taka istnieje
        }

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
