using fitApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitApp.Data
{
    public class DatabaseService
    {
        private SQLiteConnection _database;
        private static string _databasePath;

        // Konstruktor klasy DatabaseService
        public DatabaseService()
        {
            // Ścieżka do pliku bazy danych
            _databasePath = Path.Combine(FileSystem.AppDataDirectory, "mydatabase.db");

            // Inicjalizacja połączenia z bazą danych
            _database = new SQLiteConnection(_databasePath);

            // Automatyczne utworzenie tabel, jeśli nie istnieją
            _database.CreateTable<Activites>();

            
        }

        public int SaveActivity(Activites activity)
        {
            return _database.Insert(activity);
        }

        public int DeleteActivityById(int activityId)
        {
            // Zakładamy, że masz metodę w bazie, która umożliwia wyszukanie obiektu na podstawie ID
            var activity = _database.Table<Activites>().FirstOrDefault(a => a.Id == activityId);

            if (activity != null)
            {
                return _database.Delete(activity);
            }

            return 0; // Jeśli nie znaleziono aktywności, nie wykonano żadnej operacji
        }

        public int GetTotalKilometers()
        {
            // Pobierz wszystkie aktywności
            var activities = GetActivities();

            // Zsumuj kilometry
            return activities.Sum(activity => activity.Kilometers);
        }

        public int GetAvgKilometers() 
        {
            var activities = GetActivities();

            return (int)activities.Average(activity => activity.Kilometers);
        }

        public int GetTotalCalories()
        {
            var activities = GetActivities();
            return activities.Sum(activity => activity.Calories);
        }

        public int GetAvgCalories()
        {
            var activities = GetActivities();

            return (int)activities.Average(activity => activity.Calories);
        }

        public List<Activites> GetActivities()
        {
            return _database.Table<Activites>().ToList();
        }


    }
}
