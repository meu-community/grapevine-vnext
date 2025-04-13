using MEU.GV4.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEU.GV4.METClassic.Models;

public class Wraith : METCharacter
{
    public string? Ethnos { get; set; }
    public string? Guild { get; set; }
    public string? Faction { get; set; }
    public string? Legion { get; set; }
    public string? Rank { get; set; }
    public int? Pathos { get; set; }
    public int? TempPathos { get; set; }
    public int? Corpus { get; set; }
    public int? TempCorpus { get; set; }
    public TraitList Arcanoi { get; set; } = [];
    public string? Passions { get; set; }
    public string? Fetters { get; set; }
    public string? Life { get; set; }
    public string? Death { get; set; }
    public string? Haunt { get; set; }
    public string? Regret { get; set; }
    public string? ShadowArchetype { get; set; }
    public string? ShadowPlayer { get; set; }
    public int? Angst { get; set; }
    public int? TempAngst { get; set; }
    public string? DarkPassions { get; set; }
    public TraitList ThornList { get; set; } = [];
    public TraitList WraithStatus { get; set; } = [];
}
