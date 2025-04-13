using MEU.GV4.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEU.GV4.METClassic.Models;

public class Various : METCharacter
{
    public string? Class { get; set; }
    public string? Subclass { get; set; }
    public string? Affinity { get; set; }
    public string? Plane { get; set; }
    public string? Brood { get; set; }
    public string? Other { get; set; }
    public TraitList Tempers { get; set; } = [];
    public TraitList Powers { get; set; } = [];
}
