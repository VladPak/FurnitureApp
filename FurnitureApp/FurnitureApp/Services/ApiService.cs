using FurnitureApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Net.Http.Headers;

namespace FurnitureApp.Services
{
    public static class ApiService
    {
        public static async Task<bool> RegisterUser(string name, string email, string password)
        {
            var register = new Register()
            {
                Name = name,
                Email = email,
                Password = password
            };

            var httpClient = new HttpClient();
            var  json = JsonConvert.SerializeObject(register);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/accounts/register", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<bool> Login(string email, string password)
        {
            var login = new Login() 
            { 
                Email = email,
                Password = password
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/accounts/login", content);
            if (!response.IsSuccessStatusCode) return false; 
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Token>(jsonResult);
                Preferences.Set("userToken", result.AccessToken);
                Preferences.Set("userId", result.UserId);
                Preferences.Set("userName", result.UserName);
                return true;
        }

        public static async Task<List<Category>> GetCategories()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("userToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/categories");

            return JsonConvert.DeserializeObject<List<Category>>(response);
        }

        public static async Task<Product> GetProductById(int productId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("userToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/products/" + productId);

            return JsonConvert.DeserializeObject<Product>(response);
        }

        public static async Task<List<ProductByCategory>> GetProductByCategory(int categoryId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("userToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/products/productsbycategory/" + categoryId);

            return JsonConvert.DeserializeObject<List<ProductByCategory>>(response);
        }

        
        public static async Task<List<TrendingProduct>> GetTrendingProducts()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("userToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/products/trendingproducts");

            return JsonConvert.DeserializeObject<List<TrendingProduct>>(response);
        }

        public static async Task<bool> AddItemsInCart(AddToCart addtoCart)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(addtoCart);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("userToken", string.Empty));

            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/ShoppingCartItems", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<CartSubTotal> GetCartSubTotal(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("userToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/ShoppingCartItems/SubTotal/" + userId);

            return JsonConvert.DeserializeObject<CartSubTotal>(response);
        }

        public static async Task<List<ShoppingCartItem>> GetShoppingCartItems(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("userToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/ShoppingCartItems/" + userId);

            return JsonConvert.DeserializeObject<List<ShoppingCartItem>>(response);
        }

        public static async Task<TotalCartItem> GetTotalCartItems(int cartId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("userToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/ShoppingCartItems/TotalItems/" + cartId);

            return JsonConvert.DeserializeObject<TotalCartItem>(response);
        }

        public static async Task<bool> ClearShoppingCart(int cartId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("userToken", string.Empty));
            var response = await httpClient.DeleteAsync(AppSettings.ApiUrl + "api/ShoppingCartItems/" + cartId);

            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public static async Task<OrderResponse> PlaceOrder(Order order)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("userToken", string.Empty));

            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/Orders", content);
            var jsonResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OrderResponse>(jsonResult);
        }

        public static async Task<List<OrderByUser>> GetOrdersByUser(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("userToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Orders/OrdersByUser/" + userId);

            return JsonConvert.DeserializeObject<List<OrderByUser>>(response);
        }

        
        public static async Task<List<OrderDetail>> GetOrderDetails(int orderId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("userToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Orders/OrderDetails/" + orderId);

            return JsonConvert.DeserializeObject<List<OrderDetail>>(response);
        }

        public static async Task<bool> RegisterComplaint(Complaint complaint)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(complaint);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("userToken", string.Empty));

            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/Complaints", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        


    }
}
