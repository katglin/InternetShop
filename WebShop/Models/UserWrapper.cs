using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class UserWrapper
    {
        public ApplicationUser appUser { get; set; }
        public string CurrentRole { get; set; }
    }
}