using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEU.GV4.Data.Models.METClassic
{
    public class Werewolf : METCharacter
    {
        public string? Tribe { get; set; }
        public string? Breed { get; set; }
        public string? Auspice { get; set; }
        public string? Rank { get; set; }
        public string? Pack { get; set; }
        public string? Totem { get; set; }
        public string? Camp { get; set; }
        public string? Position { get; set; }
        public int Notoriety { get; set; }
        public int Rage { get; set; }
        public int RageTemp { get; set; }
        public int Gnosis { get; set; }
        public int GnosisTemp { get; set; }
        public int Honor { get; set; }
        public int HonorTemp { get; set; }
        public int Glory { get; set; }
        public int GloryTemp { get; set; }
        public int Wisdom { get; set; }
        public int WisdomTemp { get; set; }
        public TraitList Features { get; set; } = [];
        public TraitList Gifts { get; set; } = [];
        public TraitList Rites { get; set; } = [];
        public TraitList HonorList { get; set; } = [];
        public TraitList GloryList { get; set; } = [];
        public TraitList WisdomList { get; set; } = [];
    }
}
