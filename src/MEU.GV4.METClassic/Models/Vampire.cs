using MEU.GV4.Data.Models;

namespace MEU.GV4.METClassic.Models;

public class Vampire : METCharacter
{
    public string? Clan { get; set; }
    public string? Sect { get; set; }
    public int? Generation { get; set; }
    public string? Coterie { get; set; }
    public string? Path { get; set; }
    public string? Sire { get; set; }
    public string? Aura { get; set; }
    public string? AuraBonus { get; set; }
    public int? Blood { get; set; }
    public int? TempBlood { get; set; }
    public int? Conscience { get; set; }
    public int? TempConscience { get; set; }
    public int? SelfControl { get; set; }
    public int? TempSelfControl { get; set; }
    public int? Courage { get; set; }
    public int? TempCourage { get; set; }
    public int? PathTraits { get; set; }
    public int? TempPathTraits { get; set; }
    public TraitList KindredStatus { get; set; } = [];
    public TraitList Bonds { get; set; } = [];
    public TraitList Disciplines { get; set; } = [];
    public TraitList Rituals { get; set; } = [];
    public List<Boon> Boons { get; set; } = [];
}
