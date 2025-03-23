using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEU.GV4.Data.Models.METClassic
{
    public class Demon : METCharacter
    {
        public string? House { get; set; }
        public string? Faction { get; set; }
        public int Torment { get; set; }
        public int Faith { get; set; }
        public int FaithTemp { get; set; }
        public int Conscience { get; set; }
        public int ConscienceTemp { get; set; }
        public int Conviction { get; set; }
        public int ConvictionTemp { get; set; }
        public int Courage { get; set; }
        public int CourageTemp { get; set; }
        public TraitList Lore { get; set; }
        public TraitList Visage { get; set; }
    }
}
