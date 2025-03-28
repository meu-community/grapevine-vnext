using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEU.GV4.Data.Models.METClassic
{
    public class Mortal : METCharacter
    {
        public string? Motivation { get; set; }
        public string? Association { get; set; }
        public string? Regnant { get; set; }
        public int Blood { get; set; }
        public int BloodTemp { get; set; }
        public int Humanity { get; set; }
        public int HumanityTemp { get; set; }
        public int Conscience { get; set; }
        public int ConscienceTemp { get; set; }
        public int SelfControl { get; set; }
        public int SelfControlTemp { get; set; }
        public int Courage { get; set; }
        public int CourageTemp { get; set; }
        public int TrueFaith { get; set; }
        public int TrueFaithTemp { get; set; }
        public TraitList HumanityList { get; set; } = [];
        public TraitList NuminaList { get; set; } = [];
    }
}
