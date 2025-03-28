using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEU.GV4.Data.Models.METClassic
{
    public class Hunter : METCharacter
    {
        public string? Creed { get; set; }
        public string? Camp { get; set; }
        public string? Handle { get; set; }
        public int Conviction { get; set; }
        public int ConvictionTemp { get; set; }
        public int Mercy { get; set; }
        public int MercyTemp { get; set; }
        public int Vision { get; set; }
        public int VisionTemp { get; set; }
        public int Zeal { get; set; }
        public TraitList Edges { get; set; } = [];
    }
}
