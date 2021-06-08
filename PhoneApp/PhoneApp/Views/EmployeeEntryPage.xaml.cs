using Newtonsoft.Json;
using PhoneApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneApp.Views
{
    //[QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class EmployeeEntryPage : ContentPage
    {
        //public string ItemId
        //{
        //    set
        //    {
        //        LoadEmployee(value);
        //    }
        //}

        public EmployeeEntryPage()
        {
            InitializeComponent();

            // Set the BindingContext of the page to a new Employee.
            BindingContext = new Employee();
        }

        //async void LoadEmployee(string itemId)
        //{
        //    try
        //    {
        //        int id = Convert.ToInt32(itemId);
        //        // Retrieve the employee and set it as the BindingContext of the page.
        //        Employee employee = await App.Database.GetEmployeeAsync(id);
        //        BindingContext = employee;
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("Failed to load employee.");
        //    }
        //}

        async void OnSaveButtonClicked(object senderr, EventArgs e)
        {
            var employee = (Employee)BindingContext;
            employee.Date = DateTime.UtcNow;
            if (!string.IsNullOrWhiteSpace(employee.Name))
            {
                Uri uri = new Uri("https://10.0.2.2:4043/api/employees/");

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                try
                {
                    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(uri,httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                    }
                    else
                    {
                        await DisplayAlert("response is fucked", response.ReasonPhrase, "fuck");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Super fucked", ex.Message, "kill me");
                }
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var employee = (Employee)BindingContext;
            //await App.Database.DeleteEmployeeAsync(employee);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}