using Newtonsoft.Json;
using StaySeoul_SS5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StaySeoul_SS5.Services
{
    public class ApiService
    {
        HttpClient client = new HttpClient();
        public int cartCount { get;set; }
        private string ngrokUrl = "https://bce3-2001-d08-102b-4cfa-e800-61c9-122c-9769.ngrok-free.app/";
        public async Task<LoginUser> LoginService(LoginUser loginUser)
        {
            string Url = ngrokUrl + "Api/Login";
            Uri uri = new Uri(string.Format(Url, string.Empty));
            string json = JsonConvert.SerializeObject(loginUser);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Console.WriteLine(data);
                LoginUser loggedinUser = JsonConvert.DeserializeObject<LoginUser>(data);
                if (string.IsNullOrEmpty(data))
                {
                    return null;
                }
                else
                {
                    Application.Current.Properties["FullName"] = loggedinUser.FullName;
                    Application.Current.Properties["UID"] = loggedinUser.ID;
                    Application.Current.Properties["FamilyCount"] = loggedinUser.FamilyCount;
                    Console.WriteLine("data" + loggedinUser.FullName);
                    return loggedinUser;
                }
            }
            return null;

        }
        public async Task<List<ServiceTypes>> GetServiceTypesService()
        {
            string Url = ngrokUrl + "Api/GetServiceTypes";
            Uri uri = new Uri(string.Format(Url, string.Empty));
            string json = "";
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            if(response.IsSuccessStatusCode) { 
            string data = await response.Content.ReadAsStringAsync();
            List<ServiceTypes> sList = JsonConvert.DeserializeObject<List<ServiceTypes>>(data);
            Console.WriteLine(data);
                if (string.IsNullOrEmpty(data))
                {
                    return null;
                }
                else
                {
                    return sList;
                }
            }
            return null;
        }
        public async Task<List<Models.Services>> GetServices(Models.Services service)
        {
            string Url = ngrokUrl + "Api/GetService";
            Uri uri = new Uri(string.Format(Url, string.Empty));
            string json = JsonConvert.SerializeObject(service);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Models.Services> serviceList = JsonConvert.DeserializeObject<List<Models.Services>>(data);
                Console.WriteLine("The data" + data);
                if (string.IsNullOrEmpty(data))
                {
                    return null;
                }
                else
                {
                    return serviceList;
                }
            }
            return null;
        }

       public async Task<int> GetSpotsServices(ServiceSpot s)
        {
            string Url = ngrokUrl + "Api/GetSpotInfo";
            Uri uri = new Uri(string.Format(Url, string.Empty));
            string json = JsonConvert.SerializeObject(s);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
               
                if (string.IsNullOrEmpty(data) == false)
                {
                    int conData = int.Parse(data);
                    Console.Write("Spots" + data);
                    return conData;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }
       public async Task<bool> AddToCartService(Cart c)
        {
            string Url = ngrokUrl + "Api/StoreToCart";
            Uri uri = new Uri(string.Format(Url, string.Empty));
            string json = JsonConvert.SerializeObject(c);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response" + data);
                if (string.IsNullOrEmpty(data) == false)
                {
                    cartCount++;
                    return true;
                }
                Application.Current.Properties["CartCount"] = cartCount;
            }
            return false;
        }
       public async Task<List<Cart>> GetCartService(Cart c)
        {
            string Url = ngrokUrl + "Api/GetCartService";
            Uri uri = new Uri(string.Format(Url, string.Empty));
            string json = JsonConvert.SerializeObject(c);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                if(string.IsNullOrEmpty(data) == false)
                {
                    Console.WriteLine("cart" + data);
                    List<Cart> cart = JsonConvert.DeserializeObject<List<Cart>>(data);
                    return cart;
                }
                return null;
            }
            return null;
        }
       
        public async Task<bool> RemoveService(Cart c)
        {
            string Url = ngrokUrl + "Api/RemoveFromCart";
            Uri uri = new Uri(string.Format(Url, string.Empty));
            string json = JsonConvert.SerializeObject(c);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                if(string.IsNullOrEmpty(data) == false)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public async Task<ServiceDiscount> GetDiscountService(ServiceDiscount s)
        {
            string Url = ngrokUrl + "Api/Discount";
            Uri uri = new Uri(string.Format(Url, string.Empty));
            string json = JsonConvert.SerializeObject(s);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                if(string.IsNullOrEmpty(data) == false)
                {
                    ServiceDiscount serviceDiscount = JsonConvert.DeserializeObject<ServiceDiscount>(data); 
                    return serviceDiscount;
                }
                return null;
            }
            return null;
        }
        public async Task<bool> CheckoutService(List<Cart> cart)
        {
            string Url = ngrokUrl + "Api/Checkout";
            Uri uri = new Uri(string.Format(Url, string.Empty));
            string json = JsonConvert.SerializeObject(cart);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                if(string.IsNullOrEmpty(data) == false)
                {
                    Console.WriteLine("after checkout" + data);
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
