using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class StaticModel : ViewModelBase<int>
    {
        public Int64 Static_id { get; set; }

        public string About_shop { get; set; }

        public string Sales { get; set; }

        public string Delivery { get; set; }
    }
}