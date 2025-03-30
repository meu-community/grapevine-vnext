using MEU.GV4.Data.Models;
using MEU.GV4.Data.Providers;
using System.Xml.Linq;

namespace MEU.GV4.Data.Tests.Providers
{
    public class GrapevineLegacyXMLReaderHunter
    {
        [Fact(DisplayName = "Can Load Hunter Character Data")]
        public void CanLoadHunterCharacterData()
        {
            TraitList testEdges = [new() { Name = "Deviance: Impart", Value = "0", Note="touched" }];
            var xmlDoc = XDocument.Parse("""
                <?xml version="1.0"?>
                <hunter creed="Avenger" camp="Idealist" handle="XYZ" conviction="3" mercy="2" vision="4" zeal="3"
                  tempconviction="1" tempmercy="1" tempvision="1" tempzeal="1">
                  <traitlist name="Edges" abc="no" atomic="yes" display="5">
                    <trait name="Deviance: Impart" val="0" note="touched"/>
                  </traitlist>
                </hunter>
                """);
#pragma warning disable CS8604 // Possible null reference argument.
            var result = GrapevineLegacyXMLReader.LoadHunter(xmlDoc.Root);
#pragma warning restore CS8604 // Possible null reference argument.
            Assert.NotNull(result);
            Assert.Equal("Avenger", result.Creed);
            Assert.Equal("Idealist", result.Camp);
            Assert.Equal("XYZ", result.Handle);
            Assert.Equal(3, result.Conviction);
            Assert.Equal(2, result.Mercy);
            Assert.Equal(4, result.Vision);
            Assert.Equal(3, result.Zeal);
            Assert.Equal(1, result.TempConviction);
            Assert.Equal(1, result.TempMercy);
            Assert.Equal(1, result.TempVision);
            Assert.Equal(1, result.TempZeal);
            Assert.Equivalent(testEdges, result.Edges);
        }
    }
}
