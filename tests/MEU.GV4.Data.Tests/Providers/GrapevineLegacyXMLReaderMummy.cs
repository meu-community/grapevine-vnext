using MEU.GV4.Data.Models;
using MEU.GV4.Data.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MEU.GV4.Data.Tests.Providers
{
    public class GrapevineLegacyXMLReaderMummy
    {
        [Fact(DisplayName = "Can Load Mummy Character Data")]
        public void CanLoadMummyCharacterData()
        {
            var xmlDoc = XDocument.Parse("""
                <?xml version="1.0"?>
                <mummy amenti="Kher-Minu" sekhem="2" balance="2" memory="3" integrity="2" joy="2" ba="3" ka="2"
                  tempsekhem="0" tempbalance="0" tempmemory="0" tempintegrity="0" tempjoy="0" tempba="0" tempka="0">
                  <traitlist name="Humanity" abc="yes" display="1">
                    <trait name="Benevolent"/>
                    <trait name="Charitable"/>
                  </traitlist>
                  <traitlist name="Status" abc="yes" display="1">
                    <trait name="Divine"/>
                  </traitlist>
                  <traitlist name="Hekau" abc="no" atomic="yes" display="5">
                    <trait name="Alchemy: First Basic Path" val="3"/>
                  </traitlist>
                  <traitlist name="Spells" abc="no" atomic="yes" display="5">
                    <trait name="Alchemy: Analyze Material" note="basic"/>
                  </traitlist>
                  <traitlist name="Rituals" abc="no" atomic="yes" display="5">
                    <trait name="Alchemy: Blood of the Snake" note="basic"/>
                  </traitlist>
                  <inheritance>
                    <![CDATA[My inheritance]]>
                  </inheritance>
                </mummy>
                """);
            TraitList testHumanity = [new() { Name = "Benevolent" }, new() { Name = "Charitable" }];
            TraitList testStatus = [new() { Name = "Divine" }];
            TraitList testHekau = [new() { Name = "Alchemy: First Basic Path", Value = "3" }];
            TraitList testSpells = [new() { Name = "Alchemy: Analyze Material", Note = "basic" }];
            TraitList testRituals = [new() { Name = "Alchemy: Blood of the Snake", Note = "basic" }];
#pragma warning disable CS8604 // Possible null reference argument.
            var result = GrapevineLegacyXMLReader.LoadMummy(xmlDoc.Root);
#pragma warning restore CS8604 // Possible null reference argument.
            Assert.NotNull(result);
            Assert.Equal("Kher-Minu", result.Amenti);
            Assert.Equal(2, result.Sekhem);
            Assert.Equal(2, result.Balance);
            Assert.Equal(3, result.Memory);
            Assert.Equal(2, result.Integrity);
            Assert.Equal(2, result.Joy);
            Assert.Equal(3, result.Ba);
            Assert.Equal(2, result.Ka);
            Assert.Equal(0, result.TempSekhem);
            Assert.Equal(0, result.TempBalance);
            Assert.Equal(0, result.TempMemory);
            Assert.Equal(0, result.TempIntegrity);
            Assert.Equal(0, result.TempJoy);
            Assert.Equal(0, result.TempBa);
            Assert.Equal(0, result.TempKa);
            Assert.Equal("My inheritance", result.Inheritance);
            Assert.Equivalent(testHumanity, result.Humanity);
            Assert.Equivalent(testStatus, result.MummyStatus);
            Assert.Equivalent(testHekau, result.Hekau);
            Assert.Equivalent(testSpells, result.Spells);
            Assert.Equivalent(testRituals, result.Rituals);
        }
    }
}
