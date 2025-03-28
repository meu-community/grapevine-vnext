using MEU.GV4.Data.Models;
using MEU.GV4.Data.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MEU.GV4.Data.Tests.Providers
{
    public class GrapevineLegacyXMLReaderHunter
    {
        [Fact(DisplayName = "Can Load Hunter Character Data")]
        public void CanLoadHunterCharacterData()
        {
            TraitList testEdges = [new() { Name = "Deviance: Impart", Value = "0", Note="touched" }];
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("""
                <?xml version="1.0"?>
                <hunter creed="Avenger" camp="Idealist" handle="XYZ" conviction="3" mercy="2" vision="4" zeal="3">
                  <traitlist name="Edges" abc="no" atomic="yes" display="5">
                    <trait name="Deviance: Impart" val="0" note="touched"/>
                  </traitlist>
                </hunter>
                """);
#pragma warning disable CS8604 // Possible null reference argument.
            var result = GrapevineLegacyXMLReader.LoadHunter(xmlDoc.DocumentElement);
#pragma warning restore CS8604 // Possible null reference argument.
            Assert.NotNull(result);
            Assert.Equal("Avenger", result.Creed);
            Assert.Equal("Idealist", result.Camp);
            Assert.Equal("XYZ", result.Handle);
            Assert.Equal(3, result.Conviction);
            Assert.Equal(2, result.Mercy);
            Assert.Equal(4, result.Vision);
            Assert.Equal(3, result.Zeal);
            Assert.Equivalent(testEdges, result.Edges);
        }
    }
}
