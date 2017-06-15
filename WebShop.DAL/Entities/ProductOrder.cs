using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebShop.DAL
{
    public class ProductOrder
    {
        [Key]
        public Int64 Pr_or_id { get; set; }

        public Int64 Order_id { get; set; }

        public Int64 Artikul { get; set; }

        public Int64 Amount { get; set; }

        public float Price { get; set; }
    }
}
