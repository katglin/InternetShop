using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class CommentModel
    {
        public Int64 Comment_id { get; set; }

        public string User_name { get; set; }

        public Int64 Artikul { get; set; }

        public string Comment_text { get; set; }

        public DateTime Comment_date { get; set; }
    }
}
