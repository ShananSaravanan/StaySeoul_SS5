using StaySeoul_SS5.Models;
using StaySeoul_SS5.Services;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StaySeoul_SS5
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseServicePage : ContentPage
    {
        public static string date;
        ApiService apiService = new ApiService();
        public static string chosenServiceType;
        public string iconName;
        public long serviceID;
        public static int maxPPl;
        public static int bookingCap;
        public static double amount;
        decimal total;
        DateTime selectedDate;
        List<Models.Services> displayList = new List<Models.Services>();
        List<string> madedates = new List<string>();
        List<string> separators = new List<string>();
        List<string> blackDates = new List<string>();
        public ChooseServicePage(ServiceTypes cService)
        {
            InitializeComponent();
            this.BindingContext = this;
            loadData(cService);
        }
        public async void loadData(ServiceTypes chosen)
        {
            displayList.Clear();
            chosenServiceType = chosen.serviceType;
            Title = "Seoul Stay - " + chosenServiceType;
            descitpionLbl.Text = chosen.description;
            iconName = chosen.iconName;
            Models.Services s = new Models.Services();
            s.ServiceTypeID = chosen.serviceID;
            var retrieved = await apiService.GetServices(s);
            displayList = retrieved;
            foreach(var d in displayList)
            {
                d.iconName = chosen.iconName;
            }
            services.ItemsSource = displayList.ToList();
            
        }
        public void loadSelectionData(long ID)
        {
            var selected = displayList.Where(x => x.ID == ID).FirstOrDefault();
            serviceDescLbl.Text = "Description of " + selected.Name + selected.durationDetail;
            sDesc.Text = selected.Description;
            Console.WriteLine(selected.Description);
            orderGrid.IsVisible = true;
            
        }
        public void resetLbl()
        {
            payableLbl.Text = "";
            spotLbl.Text = "";
            bLbl.Text = "";
            addNotes.Text = "";
            datepicker.Text = "";
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            
            long ID = long.Parse(((Grid)sender).ClassId.ToString());
            serviceID = ID;
            loadSelectionData(ID);
            resetLbl();

        }

        public void displayCalendar()
        {
            firstFrame.IsVisible = false;
            serviceDescLbl.IsVisible = false;
            orderGrid.IsVisible = false;
            descitpionLbl.IsVisible = false;
            serviceDescLbl.IsVisible = false;
            sDesc.IsVisible = false;
            calendar.IsVisible = true;
        }
        public void undisplayCalendar()
        {
            firstFrame.IsVisible = true;
            serviceDescLbl.IsVisible = true;
            orderGrid.IsVisible = true;
            descitpionLbl.IsVisible = true;
            serviceDescLbl.IsVisible = true;
            sDesc.IsVisible = true;
            calendar.IsVisible = false;
        }
        public void makeDate()
        {
            try
            {
                List<int> indexes = new List<int>();
                int minDateIndex = 0;
                int maxDateIndex = 0;
                for (int i = 0; i < separators.Count; i++)
                {
                    if (separators[i].Contains("-"))
                    {
                        indexes.Add(i);
                    }
                }
                foreach (var index in indexes)
                {
                    minDateIndex = index;
                    maxDateIndex = index + 1;
                    int dayCount = 0;
                    int initialDate = int.Parse(madedates[minDateIndex].ToString());
                    int maxDate = int.Parse(madedates[maxDateIndex].ToString());
                    for (int i = initialDate; i <= maxDate; i++)
                    {
                        int nextDate = initialDate + dayCount;
                        madedates.Add(nextDate.ToString());
                        dayCount++;
                    }
                    madedates = madedates.Distinct().ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("message" + e.Message);
            }
            for (int i = 1; i <= 31; i++)
            {
                if (madedates.Contains(i.ToString()))
                {

                }
                else
                {
                    blackDates.Add(i.ToString());
                }
            }
        }
        public void retrieveDate(string availDates)
        {
            madedates.Clear();
            if (availDates != "*")
            {
                try
                {
                    for (int i = 0; i < availDates.Count(); i++)
                    {

                        if (i == 0 || i == availDates.Count() - 1)
                        {


                            if (i == 0)
                            {
                                bool next = char.IsDigit(availDates[i + 1]);
                                if (next == true)
                                {
                                    madedates.Add(availDates[i].ToString() + availDates[i + 1].ToString());
                                }
                                else
                                {
                                    madedates.Add(availDates[i].ToString());
                                }
                            }
                            else
                            {
                                bool front = char.IsDigit(availDates[i - 1]);
                                if (front == true)
                                {
                                    madedates.Add(availDates[i - 1].ToString() + availDates[i].ToString());
                                }
                                else
                                {
                                    madedates.Add(availDates[i].ToString());
                                }
                            }
                        }
                        else
                        {
                            bool front = char.IsDigit(availDates[i - 1]);
                            bool next = char.IsDigit(availDates[i + 1]);
                            if (front == false && next == false)
                            {
                                madedates.Add(availDates[i].ToString());
                            }
                            if (front == false && next == true)
                            {
                                madedates.Add(availDates[i].ToString() + availDates[i + 1].ToString());
                            }

                        }
                    }
                    for (int i = 0; i < availDates.Count(); i++)
                    {
                        if (char.IsDigit(availDates[i]) == false)
                        {
                            separators.Add(availDates[i].ToString());
                            Console.WriteLine(availDates[i]);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("here" + e.Message);
                }




                madedates = madedates.Distinct().ToList();
                makeDate();
            }

        }
        private async void dateClicked(object sender, EventArgs e)
        {
            displayCalendar();
            var selected = displayList.Where(x => x.ID == serviceID).FirstOrDefault();
            if (selected.DayOfMonth.Trim().Count() >0 )
            {
                string dates = selected.DayOfMonth.ToString();
                retrieveDate(dates);
                
            }
            if (selected.DayOfWeek.Trim().Count() > 0)
            {
                string dates = selected.DayOfWeek.ToString();
                retrieveDate(dates);

            }
        }

        private void datepicker2_OnMonthCellLoaded(object sender, MonthCellLoadedEventArgs e)
        {

            List<DateTime> blackoutDates = new List<DateTime>();
            if (blackDates.Contains(e.Date.Day.ToString()))
            {
                datepicker2.BlackoutDates = new List<DateTime> { e.Date };
            }
        }
        public void setUnbuyable()
        {
            pplEntry.IsEnabled = false;
            btn.IsEnabled = false;
            addNotes.IsEnabled = false;
        }
        public async void findRemainingSpotsAsync(DateTime sd)
        {
            ServiceSpot sSpot = new ServiceSpot();
            sSpot.date = sd.Date;
            sSpot.serviceID = serviceID;
            var spots = await apiService.GetSpotsServices(sSpot);
            var selected = displayList.Where(x=> x.ID == sSpot.serviceID).FirstOrDefault();
            amount = (double)selected.Price;
            selected.remainingSpot = spots;
            bookingCap = (int)selected.BookingCap;
            spotLbl.Text = "Remaining " + spots + " spots";
            maxPPl = spots;
            if (spots == 0)
            {
                setUnbuyable();
            }
            string familycount = Application.Current.Properties["FamilyCount"].ToString();
            pplEntry.Text = familycount.ToString();

        }
        private void datepicker2_SelectionChanged(object sender, Syncfusion.SfCalendar.XForms.SelectionChangedEventArgs e)
        {
            undisplayCalendar();
            datepicker.Text = datepicker2.SelectedDate.Value.ToShortDateString();
            selectedDate = datepicker2.SelectedDate.Value.Date;
            findRemainingSpotsAsync(selectedDate);
            
        }


        private void pplEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (string.IsNullOrEmpty(pplEntry.Text) == false)
            {
                int spotValue = int.Parse(pplEntry.Text);
                if (spotValue > maxPPl)
                {
                    
                    pplEntry.Text = maxPPl.ToString();

                }
                else
                {
                    decimal divide = Decimal.Divide(spotValue, bookingCap);
                    int noOfBookings = 0;
                    if (divide % 1 != 0)
                    {
                        noOfBookings = (int)divide + 1;
                    }
                    else
                    {
                        noOfBookings = (int)divide;
                    }
                    bLbl.Text = "In " + noOfBookings + " bookings";
                    total = Decimal.Multiply(noOfBookings,(decimal)amount);
                    payableLbl.Text = "Amount payable: " + "$" + total.ToString("N2");
                }
            }
            else
            {
                bLbl.Text = "";
            }
        }

        private async void btn_Clicked(object sender, EventArgs e)
        {
            var orderDetails = displayList.Where(x => x.ID == serviceID).FirstOrDefault();
            long duration = orderDetails.Duration;
            int spotValue = int.Parse(pplEntry.Text);
            Cart c = new Cart();
            c.serviceID = serviceID;
            c.fromDate = selectedDate.Date;
            c.toDate = selectedDate.Date.AddDays(duration);
            c.iconName = orderDetails.iconName;
            c.payableAmount = total;
            c.noOfPPl = spotValue;
            c.serviceName = orderDetails.Name;
            c.addOnNotes = addNotes.Text.ToString();
            var sending = await apiService.AddToCartService(c);
            if(sending == true)
            {
                await DisplayAlert("Success", "Added to Cart!", "OK");
                await choosePage.Navigation.PopToRootAsync();
            }
            else
            {
                await DisplayAlert("Failed", "Not Added to Cart!", "OK");
            }
            
        }
    }
}