using FurnitureApp.Models;
using FurnitureApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FurnitureApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public ObservableCollection<ShoppingCartItem> ShooppinngCartCollection;
        public CartPage()
        {
            InitializeComponent();
            ShooppinngCartCollection = new ObservableCollection<ShoppingCartItem>();
            GetShoppingItems();
            GetTotalPrice();
        }

        private async void GetTotalPrice()
        {
           var totalPrice = await ApiService.GetCartSubTotal(Preferences.Get("userId", 0));
           LblTotalPrice.Text = totalPrice.SubTotal.ToString();
        }

        private async void GetShoppingItems()
        {
            var shoppingCartItems = await ApiService.GetShoppingCartItems(Preferences.Get("userId", 0));
            foreach (var shoppingCart in shoppingCartItems)
            {
                ShooppinngCartCollection.Add(shoppingCart);
            }
            LvShoppingCart.ItemsSource = ShooppinngCartCollection;
        }

        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void TapClearCart_Tapped(object sender, EventArgs e)
        {
            var response = await ApiService.ClearShoppingCart(Preferences.Get("userId", 0));
            if (response)
            {
                await DisplayAlert("", "Your cart has been cleared", "Alright");
                LvShoppingCart.ItemsSource = null;
                LblTotalPrice.Text = "0";
            }
            else
            {
                await DisplayAlert("", "Smth went wrong", "Cancel");
            }
        }

        private void BtnProceed_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PlaceOrderPage(Convert.ToDouble(LblTotalPrice.Text)));
        }
    }
}