using System;
using System.Collections.Generic;
using System.Text;

namespace StaySeoul_SS5.Models
{
    public class Services
    {
        public long ID { get; set; }
        public long ServiceTypeID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public long Duration { get; set; }
        public string Description { get; set; }
        public string DayOfWeek { get; set; }
        public string DayOfMonth { get; set; }
        public long DailyCap { get; set; }
        public long BookingCap { get; set; }
        public string iconName { get;set; }
        public int remainingSpot { get;set; }
        public string durationDetail => "(" + Duration + " day)";
    }
}
