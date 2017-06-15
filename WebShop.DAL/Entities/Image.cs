using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebShop.DAL
{
    public class Image
    {
        [Key]
        public Int64 Image_id { get; set; }

        public string Image_path { get; set; }

        public Int64 Artikul { get; set; }
    }
}
