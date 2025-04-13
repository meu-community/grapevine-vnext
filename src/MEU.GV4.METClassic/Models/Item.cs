using MEU.GV4.Data.Models;

namespace MEU.GV4.METClassic.Models;
public class Item
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? SubType { get; set; }
    public int? Level { get; set; }
    public int? Bonus { get; set; }
    public string? Damage { get; set; }
    public int? Amount { get; set; }
    public string? Conceal { get; set; }
    public string? Powers { get; set; }
    public string? Appearance { get; set; }
    public string? Notes { get; set; }
    public TraitList Tempers { get; set; } = [];
    public TraitList Abilities { get; set; } = [];
    public TraitList Negatives { get; set; } = [];
    public TraitList Availability { get; set; } = [];
    public DateTimeOffset? CreateDate { get; set; }
    public DateTimeOffset? ModifyDate { get; set; }
}
