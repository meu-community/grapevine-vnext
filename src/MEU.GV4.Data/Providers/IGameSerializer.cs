using MEU.GV4.Data.Models;

namespace MEU.GV4.Data.Providers;
public interface IGameSerializer
{
    string? Serialize(Game game);
    Game? Deserialize(string rawData);
}
