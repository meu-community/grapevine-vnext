namespace MEU.GV4.METClassic.Models;
public class SubAction
{
    public string? Name { get; set; }
    public int? Level { get; set; }
    public int? Unused { get; set; }
    public int? Total { get; set; }
    public int? Growth { get; set; }
    public string? Act { get; set; }
    public string? Results { get; set; }
    public List<Link> LinkList { get; set; } = [];
}
