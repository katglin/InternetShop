using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class ProductPageWithComments
    {
        public ProductPageModel product { get; set; }

        public List <CommentModel> comments { get; set; }

 		public PageInfo PageInfo { get; set; }

		public IEnumerable<ProductInfoModel> similarProducts { get; set; }

        public Queue<ProductInfoModel> recentProducts { get; set; }
    }
}
