using Android.OS;
using PhoneApp.Data;
using PhoneApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhoneApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }
        protected override async void OnAppearing()
        {
            LS_Database database = await LS_Database.Instance;
            // get
            Counter Count = await database.GetItemAsync(1);
            if(Count.Count > 0)
            {
                CounterButton.Text = $"You clicked {Count.Count} times.";
            }


            base.OnAppearing();
        }

        async void Button_Clicked(object sender, System.EventArgs e)
        {
            LS_Database database = await LS_Database.Instance;

            if (database.GetItemAsync(1).Result == null)
            {
                Counter Counttmp = new Counter
                {
                    Count = 0
                };
                await database.SaveItemAsync(Counttmp);
            }
            // get
            Counter Count = await database.GetItemAsync(1);

            Count.Count++;

            ((Button)sender).Text = $"You clicked {Count.Count} times.";

            // save
            await database.SaveItemAsync(Count);

            // Navigate backwards
            //await Navigation.PopAsync();
        }
    }
}
