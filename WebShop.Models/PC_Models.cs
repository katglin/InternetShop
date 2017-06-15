using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class PC_Models
    {
    }

    public class HddModel
    {
        public Int64 HDD_id { get; set; }
        public int HDD { get; set; }

    }

    public class VideoCardModel
    {
        public Int64 Video_id { get; set; }
        public string VideoProducer { get; set; }
      //  public int VideoMemory { get; set; }
    }

    public class MemoryModel
    {
        public Int64 Memory_id { get; set; }
        public int Memory { get; set; }
    }

    public class ProcModel
    {
        public Int64 Proc_id { get; set; }
        public string ProcessorProducer { get; set; }
  //      public double ProcessorFreq { get; set; }
    }
}
