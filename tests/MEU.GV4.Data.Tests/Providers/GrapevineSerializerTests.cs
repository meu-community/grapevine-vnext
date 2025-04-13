using MEU.GV4.Data.Providers;
using MEU.GV4.Data.Models.METClassic;
using System.Text;

namespace MEU.GV4.Data.Tests.Providers;
public class GrapevineSerializerTests
{
    private METTypeResolver metResolver = new METTypeResolver();

    [Fact(DisplayName = "Can Serialize MET Game and retain type data")]
    public async Task CanSerializeMETGameAndRetainType()
    {
        var game = new METGame();
        var serializer = new GrapevineSerializer(metResolver);
        using (var memStream = new MemoryStream())
        {
            await serializer.SerializeAsync(memStream, game);
            memStream.Position = 0;
#pragma warning disable CS8604 // Possible null reference argument.
            var deserialized = await serializer.DeserializeAsync(memStream);
#pragma warning restore CS8604 // Possible null reference argument.
            Assert.IsType<METGame>(deserialized);
        }
    }

    [Fact(DisplayName = "Can Serialize MET characters and retain type data")]
    public async Task CanSerializeMETCharactersAndRetainType()
    {
        var game = new METGame()
        {
            Characters =
            [
                new Changeling(),
                new Demon(),
                new Fera(),
                new Hunter(),
                new KueiJin(),
                new Mage(),
                new Mortal(),
                new Mummy(),
                new Vampire(),
                new Various(),
                new Werewolf(),
                new Wraith()
            ]
        };
        var serializer = new GrapevineSerializer(metResolver);
        using (var memStream = new MemoryStream())
        {

            await serializer.SerializeAsync(memStream, game);
            memStream.Position = 0;
#pragma warning disable CS8604 // Possible null reference argument.
            var deserialized = await serializer.DeserializeAsync(memStream);
#pragma warning restore CS8604 // Possible null reference argument.
            Assert.Equivalent(game.Characters, deserialized?.Characters);
        }
    }

    [Fact(DisplayName = "Deserialize Throws GrapevineProviderException when stream is empty")]
    public async Task DeserializeThrowsGrapevineProviderExceptionWhenEmpty()
    {
        using (var memStream = new MemoryStream())
        {
            var serializer = new GrapevineSerializer(metResolver);
            await Assert.ThrowsAsync<GrapevineProviderException>(async () => await serializer.DeserializeAsync(memStream));
        }
    }

    [Fact(DisplayName = "Deserialize Throws GrapevineProviderException when stream is whitespace")]
    public async Task DeserializeThrowsGrapevineProviderExceptionWhenWhitespace()
    {
        using (var memStream = new MemoryStream(Encoding.UTF8.GetBytes(" ")))
        {
            var serializer = new GrapevineSerializer(metResolver);
            await Assert.ThrowsAsync<GrapevineProviderException>(async () => await serializer.DeserializeAsync(memStream));
        }
    }

    [Fact(DisplayName = "Deserialize Throws GrapevineProviderException when stream is null")]
    public async Task DeserializeThrowsGrapevineProviderExceptionWhenNull()
    {
        var serializer = new GrapevineSerializer(metResolver);
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        await Assert.ThrowsAsync<GrapevineProviderException>(async () => await serializer.DeserializeAsync(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}
