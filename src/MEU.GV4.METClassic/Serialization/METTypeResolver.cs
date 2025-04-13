using MEU.GV4.Base.Models;
using MEU.GV4.METClassic.Models;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace MEU.GV4.METClassic.Serialization;
public class METTypeResolver : DefaultJsonTypeInfoResolver
{
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);

        Type baseGameType = typeof(Game);
        Type baseCharacterType = typeof(Character);
        if (jsonTypeInfo.Type == baseGameType)
        {
            jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
            {
                TypeDiscriminatorPropertyName = "$type",
                IgnoreUnrecognizedTypeDiscriminators = true,
                UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
                DerivedTypes =
                {
                    new JsonDerivedType(typeof(METGame), nameof(METGame).ToLowerInvariant())
                }
            };
        }
        else if (jsonTypeInfo.Type == baseCharacterType)
        {
            jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
            {
                TypeDiscriminatorPropertyName = "$type",
                IgnoreUnrecognizedTypeDiscriminators = true,
                UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization
            };
            var types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(METCharacter)));
            foreach (var t in types.Select(t => new JsonDerivedType(t, t.Name.ToLowerInvariant())))
                jsonTypeInfo.PolymorphismOptions.DerivedTypes.Add(t);
        }

        return jsonTypeInfo;
    }
}
