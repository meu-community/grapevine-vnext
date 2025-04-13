using MEU.GV4.Base.Models;

namespace MEU.GV4.METClassic.Models;
public class Rote
{
    public string? Name { get; set; }
    public int? Level { get; set; }
    public string? Description { get; set; }
    public string? Grades { get; set; }
    public TraitList Spheres { get; set; } = [];
    public DateTimeOffset? CreateDate { get; set; }
    public DateTimeOffset? ModifyDate { get; set; }
}
