using PhoneApp.Views;
using Xamarin.Forms;

namespace PhoneApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(EmployeeEntryPage), typeof(EmployeeEntryPage));
            Routing.RegisterRoute(nameof(EmployeePage), typeof(EmployeePage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }
    }
}