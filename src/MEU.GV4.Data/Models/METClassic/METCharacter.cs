using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEU.GV4.Data.Models;

namespace MEU.GV4.Data.Models.METClassic
{
    public abstract class METCharacter : Character
    {
        public string? Nature { get; set; }
        public string? Demeanor { get; set; }
        public IEnumerable<Trait> PhysicalTraits { get; set; }
        public IEnumerable<Trait> SocialTraits { get; set; }
        public IEnumerable<Trait> MentalTraits { get; set; }
        public IEnumerable<Trait> NegativePhysicalTraits { get; set; }
        public IEnumerable<Trait> NegativeSocialTraits { get; set; }
        public IEnumerable<Trait> NegativeMentalTraits { get; set; }
        public int PhysicalMax { get; set; }
        public int SocialMax { get; set; }
        public int MentalMax { get; set; }

        public IEnumerable<Trait> Abilities { get; set; }
        public IEnumerable<Trait> Influences { get; set; }
        public IEnumerable<Trait> Backgrounds { get; set; }
        public IEnumerable<Trait> Merits { get; set; }
        public IEnumerable<Trait> Flaws { get; set; }

    }
}
