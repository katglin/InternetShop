using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebShop.DAL
{
    public class VideoCard
    {
        [Key]
        public Int64 VideoCard_id { get; set; }

        public string Producer { get; set; } 

        public int Memory { get; set; }
    }
}
