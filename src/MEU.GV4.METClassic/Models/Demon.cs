using MEU.GV4.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEU.GV4.METClassic.Models;

public class Demon : METCharacter
{
    public string? House { get; set; }
    public string? Faction { get; set; }
    public int? Torment { get; set; }
    public int? Faith { get; set; }
    public int? TempFaith { get; set; }
    public int? Conscience { get; set; }
    public int? TempConscience { get; set; }
    public int? Conviction { get; set; }
    public int? TempConviction { get; set; }
    public int? Courage { get; set; }
    public int? TempCourage { get; set; }
    public TraitList Lores { get; set; } = [];
    public TraitList Visage { get; set; } = [];
}
