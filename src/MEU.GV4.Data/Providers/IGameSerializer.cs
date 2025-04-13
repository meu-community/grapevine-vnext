using MEU.GV4.Data.Models;

namespace MEU.GV4.Data.Providers;
public interface IGameSerializer
{
    Task SerializeAsync(Stream output, Game game);
    Task<Game?> DeserializeAsync(Stream input);
}
