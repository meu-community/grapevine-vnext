using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using MEU.GV4.Data.Models;

namespace MEU.GV4.Data.Providers;
public class GrapevineSerializer : IGameSerializer
{
    private JsonSerializerOptions options = new JsonSerializerOptions
    {
        WriteIndented = true
    };

    public GrapevineSerializer(IJsonTypeInfoResolver typeInfoResolver)
    {
        options.TypeInfoResolver = typeInfoResolver;
    }


    public async Task<Game?> DeserializeAsync(Stream input)
    {
        if (input == null || input.Length == 0)
        {
            throw new GrapevineProviderException(ErrorConstants.FILE_CONTENTS_EMPTY);
        }
        try
        {
            return await JsonSerializer.DeserializeAsync<Game?>(input, options);
        }
        catch (JsonException ex)
        {
            throw new GrapevineProviderException(ErrorConstants.INVALID_GRAPEVINE_FILE, ex);
        }
    }

    public async Task SerializeAsync(Stream output, Game game)
    {
        await JsonSerializer.SerializeAsync(output, game, options);
    }
}
