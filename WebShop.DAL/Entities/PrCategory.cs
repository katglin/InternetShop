using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebShop.DAL
{
    public class PrCategory
    {
        [Key]
        public Int64 Category_id { get; set; }

        public string Category { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public PrCategory()
        {
            Products = new List<Product>();
        }
    }
}
