using System;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using Xamarin.Forms;

namespace StaySeoul_SS5.Models
{
    public class Cart
    {
        public long cartID { get; set; }
        public string serviceName { get;set; }
        public long UID => long.Parse(Application.Current.Properties["UID"].ToString());
        public Guid guid { get; set; }
        public long addOnID { get; set; }
        public long serviceID { get; set; }
        public decimal payableAmount { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string iconName { get; set; }
        public int noOfPPl { get; set; }
        
        public string couponCode { get; set; }
        public string addOnNotes { get; set; }
        public string payable => "$"+payableAmount.ToString("N2");
        public string dateDetail => "From: " + fromDate.ToShortDateString() + " To: " + toDate.ToShortDateString();
        public string pplNo => "Number of people: " + noOfPPl.ToString();
        public decimal totalPayable { get; set; }
    }
}
