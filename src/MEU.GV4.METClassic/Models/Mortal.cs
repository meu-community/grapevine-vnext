using MEU.GV4.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEU.GV4.METClassic.Models;

public class Mortal : METCharacter
{
    public string? Motivation { get; set; }
    public string? Association { get; set; }
    public string? Regnant { get; set; }
    public int? Blood { get; set; }
    public int? TempBlood { get; set; }
    public int? Humanity { get; set; }
    public int? TempHumanity { get; set; }
    public int? Conscience { get; set; }
    public int? TempConscience { get; set; }
    public int? SelfControl { get; set; }
    public int? TempSelfControl { get; set; }
    public int? Courage { get; set; }
    public int? TempCourage { get; set; }
    public int? TrueFaith { get; set; }
    public int? TempTrueFaith { get; set; }
    public TraitList HumanityList { get; set; } = [];
    public TraitList NuminaList { get; set; } = [];
}
