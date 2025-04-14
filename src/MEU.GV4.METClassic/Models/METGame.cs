using MEU.GV4.Base.Models;

namespace MEU.GV4.METClassic.Models;

public class METGame : Game
{
    public List<Item> Items { get; set; } = [];
    public List<Rote> Rotes { get; set; } = [];
    public AprSettings AprSettings { get; set; } = new AprSettings();
    public List<GameAction> Actions { get; set; } = [];
}
