using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class PrCategoryModel : ViewModelBase<int>
    {
        public Int64 Category_id { get; set; }

        public string Category { get; set; }
        //public virtual ICollection<Product> Products { get; set; }
        //public PrCategoryModel()
        //{
        //    Products = new List<Product>();
        //}
    }
}
