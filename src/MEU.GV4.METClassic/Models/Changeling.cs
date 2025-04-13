using MEU.GV4.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEU.GV4.METClassic.Models;

public class Changeling :  METCharacter
{
    public string? SeelieLegacy { get; set; }
    public string? UnseelieLegacy { get; set; }
    public string? Kith { get; set; }
    public string? Seeming { get; set; }
    public string? Court { get; set; }
    public string? House { get; set; }
    public string? Threshold { get; set; }
    public int? Glamour { get; set; }
    public int? TempGlamour { get; set; }
    public int? Banality { get; set; }
    public int? TempBanality { get; set; }
    public TraitList Arts { get; set; } = [];
    public TraitList Realms { get; set; } = [];
    public TraitList ChangelingStatus { get; set; } = [];
    public string? Oaths { get; set; }
}
