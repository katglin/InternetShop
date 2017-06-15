using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebShop.DAL
{
    public class ProductCategory
    {
        [Key]
        public Int64 Pr_cat_id { get; set; }

        public Int64 Artikul { get; set; }

        public Int64 Category_id { get; set; }
    }
}
