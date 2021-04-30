using FurnitureApp.Models;
using FurnitureApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FurnitureApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetailPage : ContentPage
    {
        public ObservableCollection<OrderDetail> OrderDetailsCollection;
        public OrderDetailPage(int orderId, double orderTotal)
        {
            InitializeComponent();
            LblTotalPrice.Text = orderTotal + " $"; 
            OrderDetailsCollection = new ObservableCollection<OrderDetail>();
            GetOrderDetails(orderId);
        }

        private async void GetOrderDetails(int orderId)
        {
            var orderDetails = await ApiService.GetOrderDetails(orderId);
            foreach (var orderDetail in orderDetails)
            {
                OrderDetailsCollection.Add(orderDetail);
            }
            LvOrderDetail.ItemsSource = OrderDetailsCollection;
        }

        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}