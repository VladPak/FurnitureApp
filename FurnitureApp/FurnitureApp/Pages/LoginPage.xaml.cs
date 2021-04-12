using FurnitureApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FurnitureApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void TapBack_Tapped(Object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void TapLogin_Tapped(object sender, EventArgs e)
        {
            var response = await ApiService.Login(EntEmail.Text, EntPassword.Text);
            if (response)
            {
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                await DisplayAlert("", "Oops smth went wrong!", "Cancel");
            }
        }
    }
}