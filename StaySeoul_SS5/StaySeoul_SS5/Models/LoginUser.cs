using System;
using System.Collections.Generic;
using System.Text;

namespace StaySeoul_SS5.Models
{
    public class LoginUser
    {
        public long ID { get; set; }
        public string Username { get; set;}
        public string Password { get; set;}
        public string FullName { get;set;}
        public int FamilyCount { get;set;}
    }
}
