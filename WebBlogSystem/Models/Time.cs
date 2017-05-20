using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBlogSystem.Models
{
    public class Time
    {
        public string getCurrentDateTimeString()
        {
            
            string  now= DateTime.Now.ToString();
            return now;
        }
        public DateTime getCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}