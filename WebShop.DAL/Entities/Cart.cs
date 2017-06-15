using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebShop.DAL
{
    public class Cart
    {
        [Key]
        public Int64 Cart_id { get; set; }
        public string User_id { get; set; }
        public string IP { get; set; }
        public Int64 Artikul { get; set; }
        public int Number { get; set; }
        public DateTime Order_date { get; set; }
        public Int64 State { get; set; }
    }
}
