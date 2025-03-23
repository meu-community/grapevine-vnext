using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEU.GV4.Data.Models.METClassic
{
    public class KueiJin : METCharacter
    {
        public string? Dharma { get; set; }
        public string? Direction { get; set; }
        public string? Balance { get; set; }
        public string? Station { get; set; }
        public string? PoArchetype { get; set; }
        public int Hun { get; set; }
        public int HunTemp { get; set; }
        public int Po { get; set; }
        public int PoTemp { get; set; }
        public int YinChi { get; set; }
        public int YinChiTemp { get; set; }
        public int YangChi { get; set; }
        public int YangChiTemp { get; set; }
        public int DemonChi { get; set; }
        public int DemonChiTemp { get; set; }
        public int DharmaTraits { get; set; }
        public int DharmaTraitsTemp { get; set; }
        public TraitList Guanxi { get; set; }
        public TraitList Disciplines { get; set; }
        public TraitList Rites { get; set; }

    }
}
