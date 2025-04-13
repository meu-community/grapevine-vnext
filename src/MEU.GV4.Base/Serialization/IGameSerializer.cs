using MEU.GV4.Base.Models;

namespace MEU.GV4.Base.Serialization;
public interface IGameSerializer
{
    Task SerializeAsync(Stream output, Game game);
    Task<Game?> DeserializeAsync(Stream input);
}
