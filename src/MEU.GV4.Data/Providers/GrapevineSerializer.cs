using System.Text.Json;
using MEU.GV4.Data.Models;

namespace MEU.GV4.Data.Providers;
public class GrapevineSerializer : IGameSerializer
{
    private JsonSerializerOptions options = new JsonSerializerOptions
    {
        WriteIndented = true,
        TypeInfoResolver = new METTypeResolver()
    };


    public Game? Deserialize(string rawData)
    {
        if (string.IsNullOrWhiteSpace(rawData))
        {
            throw new GrapevineProviderException(ErrorConstants.FILE_CONTENTS_EMPTY);
        }
        return JsonSerializer.Deserialize<Game>(rawData, options);
    }

    public string? Serialize(Game game)
    {
        return JsonSerializer.Serialize(game, options);
    }
}
