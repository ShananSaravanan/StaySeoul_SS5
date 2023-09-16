using StaySeoul_SS5.Models;
using StaySeoul_SS5.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StaySeoul_SS5
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        ApiService apiService = new ApiService();
        private async void Button_Clicked(object sender, EventArgs e)
        {
            LoginUser user = new LoginUser();
            user.Username = uname.Text;
            user.Password = pwd.Text;
            var checkUser = await apiService.LoginService(user);
            if (checkUser == null)
            {
                errorLbl.IsVisible = true;
            }
            else
            {
                errorLbl.IsVisible = false;
                Application.Current.MainPage = new NavigationPage(new TabbedPage1());
            }
        }
    }
}