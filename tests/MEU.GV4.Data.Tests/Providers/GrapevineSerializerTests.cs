using MEU.GV4.Data.Providers;
using MEU.GV4.Data.Models.METClassic;

namespace MEU.GV4.Data.Tests.Providers;
public class GrapevineSerializerTests
{
    [Fact(DisplayName = "Can Serialize MET Game and retain type data")]
    public void CanSerializeMETGameAndRetainType()
    {
        var game = new METGame();
        var serializer = new GrapevineSerializer();
        var serialized = serializer.Serialize(game);
#pragma warning disable CS8604 // Possible null reference argument.
        var deserialized = serializer.Deserialize(serialized);
#pragma warning restore CS8604 // Possible null reference argument.
        Assert.IsType<METGame>(deserialized);
    }

    [Fact(DisplayName = "Deserialize Throws GrapevineProviderException when string is empty")]
    public void DeserializeThrowsGrapevineProviderExceptionWhenEmpty()
    {
        var testGameData = string.Empty;
        var serializer = new GrapevineSerializer();
        Assert.Throws<GrapevineProviderException>(() => serializer.Deserialize(testGameData));
    }

    [Fact(DisplayName = "Deserialize Throws GrapevineProviderException when string is whitespace")]
    public void DeserializeThrowsGrapevineProviderExceptionWhenWhitespace()
    {
        var testGameData = " ";
        var serializer = new GrapevineSerializer();
        Assert.Throws<GrapevineProviderException>(() => serializer.Deserialize(testGameData));
    }

    [Fact(DisplayName = "Deserialize Throws GrapevineProviderException when string is null")]
    public void DeserializeThrowsGrapevineProviderExceptionWhenNull()
    {
        var serializer = new GrapevineSerializer();
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.Throws<GrapevineProviderException>(() => serializer.Deserialize(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}
