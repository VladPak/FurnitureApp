using FurnitureApp.Models;
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
    public partial class ContactPage : ContentPage
    {
        public ContactPage()
        {
            InitializeComponent();
        }

        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void BtnSendMessage_Clicked(object sender, EventArgs e)
        {
            var comp = new Complaint();
            comp.FullName = EntFullName.Text;
            comp.Email = EntEmail.Text;
            comp.PhoneNumber = EntPhoneNumber.Text;
            comp.Title = EntTitle.Text;
            comp.Description = EntDescription.Text;
            var response = await ApiService.RegisterComplaint(comp);

            if (response)
            {
                await DisplayAlert("", "Your complaint has been registered", "OK");
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("", "The complaint has not been registered", "OK");

            }
        }
    }
}