using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEU.GV4.Data.Models.METClassic
{
    public class Vampire : METCharacter
    {
        public string? Clan { get; set; }
        public string? Sect { get; set; }
        public int Generation { get; set; }
        public string? Title { get; set; }
        public string? Coterie { get; set; }
        public string? Path { get; set; }
        public string? Sire { get; set; }
        public string? Aura { get; set; }
        public string? AuraBonus { get; set; }
        public int Blood { get; set; }
        public int Willpower { get; set; }
        public int Conscience { get; set; }
        public int SelfControl { get; set; }
        public int Courage { get; set; }
        public int PathTraits { get; set; }

        // TODO: Add remaining Vampire-specific traits...
    }
}
