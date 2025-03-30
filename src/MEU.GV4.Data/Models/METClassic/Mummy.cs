using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEU.GV4.Data.Models.METClassic
{
    public class Mummy : METCharacter
    {
        public string? Amenti { get; set; }
        public int? Sekhem { get; set; }
        public int? TempSekhem { get; set; }
        public int? Balance { get; set; }
        public int? TempBalance { get; set; }
        public int? Memory { get; set; }
        public int? TempMemory { get; set; }
        public int? Integrity { get; set; }
        public int? TempIntegrity { get; set; }
        public int? Joy { get; set; }
        public int? TempJoy { get; set; }
        public int? Ba { get; set; }
        public int? TempBa { get; set; }
        public int? Ka { get; set; }
        public int? TempKa { get; set; }
        public TraitList Hekau { get; set; } = [];
        public TraitList Spells { get; set; } = [];
        public TraitList Rituals { get; set; } = [];
        public string? Inheritance { get; set; }
    }
}
