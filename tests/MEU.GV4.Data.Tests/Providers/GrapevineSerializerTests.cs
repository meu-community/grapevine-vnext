using MEU.GV4.Data.Providers;
using MEU.GV4.Data.Models.METClassic;
using System.Text;

namespace MEU.GV4.Data.Tests.Providers;
public class GrapevineSerializerTests
{
    [Fact(DisplayName = "Can Serialize MET Game and retain type data")]
    public async Task CanSerializeMETGameAndRetainType()
    {
        var game = new METGame();
        var serializer = new GrapevineSerializer();
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

    [Fact(DisplayName = "Can Serialize MET Vampire and retain type data")]
    public async Task CanSerializeMETVampireAndRetainType()
    {
        var game = new METGame()
        {
            Characters = [new Vampire()]
        };
        var serializer = new GrapevineSerializer();
        using (var memStream = new MemoryStream())
        {

            await serializer.SerializeAsync(memStream, game);
            memStream.Position = 0;
#pragma warning disable CS8604 // Possible null reference argument.
            var deserialized = await serializer.DeserializeAsync(memStream);
#pragma warning restore CS8604 // Possible null reference argument.
            Assert.IsType<Vampire>(deserialized?.Characters[0]);
        }
    }

    [Fact(DisplayName = "Deserialize Throws GrapevineProviderException when stream is empty")]
    public async Task DeserializeThrowsGrapevineProviderExceptionWhenEmpty()
    {
        using (var memStream = new MemoryStream())
        {
            var serializer = new GrapevineSerializer();
            await Assert.ThrowsAsync<GrapevineProviderException>(async () => await serializer.DeserializeAsync(memStream));
        }
    }

    [Fact(DisplayName = "Deserialize Throws GrapevineProviderException when stream is whitespace")]
    public async Task DeserializeThrowsGrapevineProviderExceptionWhenWhitespace()
    {
        using (var memStream = new MemoryStream(Encoding.UTF8.GetBytes(" ")))
        {
            var serializer = new GrapevineSerializer();
            await Assert.ThrowsAsync<GrapevineProviderException>(async () => await serializer.DeserializeAsync(memStream));
        }
    }

    [Fact(DisplayName = "Deserialize Throws GrapevineProviderException when stream is null")]
    public async Task DeserializeThrowsGrapevineProviderExceptionWhenNull()
    {
        var serializer = new GrapevineSerializer();
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        await Assert.ThrowsAsync<GrapevineProviderException>(async () => await serializer.DeserializeAsync(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}
