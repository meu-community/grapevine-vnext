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
        public int? Hun { get; set; }
        public int? TempHun { get; set; }
        public int? Po { get; set; }
        public int? TempPo { get; set; }
        public int? YinChi { get; set; }
        public int? TempYinChi { get; set; }
        public int? YangChi { get; set; }
        public int? TempYangChi { get; set; }
        public int? DemonChi { get; set; }
        public int? TempDemonChi { get; set; }
        public int? DharmaTraits { get; set; }
        public int? TempDharmaTraits { get; set; }
        public TraitList KuejinStatus { get; set; } = [];
        public TraitList Guanxi { get; set; } = [];
        public TraitList Disciplines { get; set; } = [];
        public TraitList Rites { get; set; } = [];

    }
}
