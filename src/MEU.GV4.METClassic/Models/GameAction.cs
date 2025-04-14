
namespace MEU.GV4.METClassic.Models;
public class GameAction
{
    public DateOnly? GameDate { get; set; }
    public string? Character { get; set; }
    public bool Done { get; set; }
    public List<SubAction> SubActions { get; set; } = [];
    public DateTimeOffset? CreateDate { get; set; }
    public DateTimeOffset? ModifyDate { get; set; }
}
