using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebShop.DAL
{
    public class Comment
    {
        [Key]
        public Int64 Comment_id { get; set; }

        public string User_id { get; set; }

        public Int64 Artikul { get; set; }

        public string Comment_text { get; set; } 

        public DateTime Comment_date { get; set; }
    }
}
