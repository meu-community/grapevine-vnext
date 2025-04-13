namespace MEU.GV4.METClassic.Models;

public class Boon
{
    public string? Type { get; set; }
    public string? Partner { get; set; }
    public bool Owed { get; set; }
    public DateTimeOffset? CreateDate { get; set; }
}
