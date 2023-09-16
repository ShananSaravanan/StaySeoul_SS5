using StaySeoul_SS5.Models;
using StaySeoul_SS5.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StaySeoul_SS5
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        List<Cart> personalCartList = new List<Cart>();
        ApiService apiService = new ApiService();
        decimal cartTotal = 0;
        decimal discountAmount = 0;
        string couponCode;
        int count;
        public CartPage()
        {
            InitializeComponent();
        }
        public void getCartTotal()
        {
            count = 0;
            cartTotal = 0;
            foreach (var item in personalCartList)
            {
                count++;
                cartTotal += item.payableAmount;

            }
            total.Text = "Total amount payable (" + count + " items): " + cartTotal.ToString("N2");
            if (count == 0)
            {
                checkoutBtn.IsEnabled = false;
            }
            else
            {
                checkoutBtn.IsEnabled = true;
            }
        }
        public async void loadCartData()
        {
            Cart cart = new Cart();
            long userID = long.Parse(Application.Current.Properties["UID"].ToString());
            var result = await apiService.GetCartService(cart);
            personalCartList = result.ToList();
            displayList.ItemsSource = personalCartList;
            getCartTotal();
            couponEntry.Text = "";
            successLbl.Text = "";
        }


        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            loadCartData();
            
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            long ID = long.Parse(((Image)(sender)).ClassId.ToString());
            Cart c = new Cart();
            c.cartID = ID;
            var removing = await apiService.RemoveService(c);
            if(removing == true)
            {
                await DisplayAlert("Sucess", "Item removed from cart", "OK");
            }
            else
            {
                await DisplayAlert("Fail", "Item not removed from cart", "OK");
            }
            
            loadCartData();
        }

        private async void btn_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(couponEntry.Text.ToString()) == false)
                {
                    string couponC = couponEntry.Text.ToString();
                    if (string.IsNullOrEmpty(couponC) == false)
                    {
                        ServiceDiscount s = new ServiceDiscount();
                        s.total = cartTotal;
                        s.couponCode = couponC;
                        
                        var sendDiscount = await apiService.GetDiscountService(s);
                        if (sendDiscount != null)
                        {
                            couponCode = s.couponCode;
                            decimal afterdiscount = sendDiscount.total;
                            discountAmount = cartTotal - afterdiscount;
                            total.Text = "Total amount payable (" + count + " items): " + afterdiscount.ToString("N2");
                            successLbl.Text = "Voucher successfully applied!";
                        }
                        else
                        {
                            successLbl.Text = "No Voucher found";
                        }
                    }
                }
                else
                {
                    successLbl.Text = " No coupon applied";
                }
            }
            catch (Exception ex)
            {
                successLbl.Text = " No coupon applied";
            }
        }
        private async void checkoutBtn_Clicked(object sender, EventArgs e)
        {
            foreach(var l in personalCartList)
            {
                l.couponCode = couponCode;
                if (discountAmount != 0)
                {
                    decimal toMinus = Decimal.Divide(discountAmount, count);
                    l.payableAmount = l.payableAmount - toMinus;
                }
                
            }
            var result = await apiService.CheckoutService(personalCartList);
            if (result == true)
            {
                foreach (var cart in personalCartList) {
                    Cart c = new Cart();
                    c.cartID =  cart.cartID;
                    var removing = await apiService.RemoveService(c);
                }
                await DisplayAlert("Success", "Checked Out", "OK");
            }
            loadCartData();
        }
    }
}