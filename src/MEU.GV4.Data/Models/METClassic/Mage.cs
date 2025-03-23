using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEU.GV4.Data.Models.METClassic
{
    public class Mage : METCharacter
    {
        public string? Essence { get; set; }
        public string? Tradition { get; set; }
        public string? Faction { get; set; }
        public string? Cabal { get; set; }
        public string? Rank { get; set; }
        public int Arete { get; set; }
        public int AreteTemp { get; set; }
        public int Quintessence { get; set; }
        public int QuintessenceTemp { get; set; }
        public int Paradox { get; set; }
        public int ParadoxTemp { get; set; }
        public TraitList Resonance { get; set; }
        public TraitList Reputation { get; set; }
        public TraitList Spheres { get; set; }
        public string? Foci { get; set; }
        public TraitList Rotes { get; set; }
    }
}
