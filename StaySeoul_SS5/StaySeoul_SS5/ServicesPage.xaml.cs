using StaySeoul_SS5.Models;
using StaySeoul_SS5.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StaySeoul_SS5
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServicesPage : ContentPage
    {
        ApiService apiService = new ApiService();
        List<ServiceTypes> typeList = new List<ServiceTypes>();
        public ServicesPage()
        {
            InitializeComponent();
            loadData();
            this.BindingContext = this;
        }
        public async void loadData()
        {
            string greetings = "Welcome " + Application.Current.Properties["FullName"];
            greetingLbl.Text = greetings;
            var serviceTypeList = await apiService.GetServiceTypesService();
            typeList = serviceTypeList;
            displayList.ItemsSource = typeList;

        }


        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            string servicetypeName = ((Grid)(sender)).ClassId.ToString();
            ServiceTypes chosen = typeList.Where(x => x.serviceType == servicetypeName).FirstOrDefault();
            await Services.Navigation.PushAsync(new ChooseServicePage(chosen));
        }
    }
}