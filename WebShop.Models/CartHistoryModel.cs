using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class CartHistoryModel
    {
        public string User_id { get; set; }
        public Int64 Artikul { get; set; }

        public string Model { get; set; }

        public int Number { get; set; }
        public DateTime Order_date { get; set; }
        public Int64 State { get; set; }
    }
}
