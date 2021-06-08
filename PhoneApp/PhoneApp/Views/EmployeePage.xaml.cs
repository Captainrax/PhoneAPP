using Newtonsoft.Json;
using PhoneApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneApp.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class EmployeePage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadEmployee(value);
            }
        }
        public EmployeePage()
        {
            InitializeComponent();

            Console.WriteLine(App.Database.ToString());
            // Set the BindingContext of the page to a new Employee.
            BindingContext = new Employee();
        }

        private static readonly Uri _uri = new Uri("https://10.0.2.2:4043/api/employees/");
        async void LoadEmployee(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                try
                {
                    var response = await client.GetAsync(_uri.ToString() + id);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Item = JsonConvert.DeserializeObject<Employee>(content);
                        BindingContext = Item;
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
            catch (Exception)
            {
                Console.WriteLine("Failed to load employee.");
            }
        }
        async void OnSaveButtonClicked(object senderr, EventArgs e)
        {
            var employee = (Employee)BindingContext;
            employee.Date = DateTime.UtcNow;
            if (!string.IsNullOrWhiteSpace(employee.Name))
            {

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                try
                {
                    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(_uri.ToString() + employee.ID, httpContent);

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
        async void OnDeleteButtonClicked(object senderr, EventArgs e)
        {
            var employee = (Employee)BindingContext;

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new HttpClient(clientHandler);

            try
            {
                var response = await client.DeleteAsync(_uri.ToString() + employee.ID);

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

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}