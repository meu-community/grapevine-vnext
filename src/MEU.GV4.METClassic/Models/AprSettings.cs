using MEU.GV4.Base.Models;

namespace MEU.GV4.METClassic.Models;
/// <summary>
/// Settings related to Actions, Plots, and Rumors
/// </summary>
public class AprSettings
{
    public int? PersonalActions { get; set; }
    public bool AddCommon { get; set; }
    public bool CarryUnused { get; set; }
    public bool PublicRumors { get; set; }
    public bool PersonalRumors { get; set; }
    public bool RaceRumors { get; set; }
    public bool GroupRumors { get; set; }
    public bool SubGroupRumors { get; set; }
    public bool InfluenceRumors { get; set; }
    public bool PreviousRumors { get; set; }
    public bool CopyPrevious { get; set; }
    public TraitList Actions { get; set; } = [];
    public TraitList Backgrounds { get; set; } = [];
}
