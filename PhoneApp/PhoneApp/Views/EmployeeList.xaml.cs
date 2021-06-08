using Newtonsoft.Json;
using PhoneApp.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneApp.Views
{
    public partial class EmployeeList : ContentPage
    {
        public EmployeeList()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            //collectionView.ItemsSource = await App.Database.GetEmployeesAsync();

            Uri uri = new Uri("https://10.0.2.2:4043/api/employees");

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new HttpClient(clientHandler);

            try
            {
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Items = JsonConvert.DeserializeObject<List<Employee>>(content);
                    collectionView.ItemsSource = Items;
                } else
                {
                    await DisplayAlert("response is fucked", response.ReasonPhrase, "fuck");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Super fucked",ex.Message, "kill me");
            }

        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the NoteEntryPage, passing the ID as a query parameter.
                Employee employee = (Employee)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(EmployeePage)}?{nameof(EmployeePage.ItemId)}={employee.ID.ToString()}");
            }
        }

        //async void OnSearchClicked(object sender, EventArgs e)
        //{
        //    await Shell.Current.GoToAsync(nameof(MainPage));

        //}

        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the EmployeeEntryPage, without passing any data.
            await Shell.Current.GoToAsync(nameof(EmployeeEntryPage));
        }



    }
}