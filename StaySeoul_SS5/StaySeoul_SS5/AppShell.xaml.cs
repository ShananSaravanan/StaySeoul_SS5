
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace StaySeoul_SS5
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ServicesPage), typeof(ServicesPage));
        }

    }
}
