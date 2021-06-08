using PhoneApp.Data;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneApp
{
    public partial class App : Application
    {
        static EmployeeDatabase database;

        // Create the database connection as a singleton.
        public static EmployeeDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new EmployeeDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Employees.db3"));
                }
                return database;
            }
        }

        public static string FolderPath { get; private set; }
        public App()
        {
            InitializeComponent();

            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
