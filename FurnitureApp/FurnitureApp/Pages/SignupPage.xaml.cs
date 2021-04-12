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
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        private async void TapSignup_Tapped(object sender, EventArgs e)
        {
            var response = await ApiService.RegisterUser(EntUsername.Text, EntEmail.Text, EntPassword.Text);
            if (response)
            {
                await DisplayAlert("", "Your account created", "Allright");
                await Navigation.PushModalAsync(new LoginPage());
            }
            else
            {
                await DisplayAlert("Error", "Smth wrong", "Cancel");
            }
        }

        private async void Span_Signup_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());

        }
    }
}