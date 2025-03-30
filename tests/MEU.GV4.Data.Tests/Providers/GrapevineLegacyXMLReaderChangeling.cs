using MEU.GV4.Data.Models;
using MEU.GV4.Data.Providers;
using System.Xml.Linq;

namespace MEU.GV4.Data.Tests.Providers
{
    public class GrapevineLegacyXMLReaderChangeling
    {
        [Fact(DisplayName = "Can Load Changeling Character Data")]
        public void CanLoadChangelingCharacterData()
        {
            var xmlDoc = XDocument.Parse("""
                <?xml version="1.0"?>
                <changeling seelie="Sage" unseelie="Trickster" court="Unseelie" kith="Sidhe" seeming="Elder" house="Leanhaun" threshold="Create Calm" 
                glamour="4" banality="3">
                    <traitlist name="Status" abc="yes" display="1">
                        <trait name="Cherished"/>
                        <trait name="Noble"/>
                    </traitlist>
                    <traitlist name="Arts" abc="no" atomic="yes" display="5">
                        <trait name="Chicanery: Fuddle" val="3" note="basic"/>
                    </traitlist>
                    <traitlist name="Realms" abc="no" atomic="yes" display="5">
                        <trait name="Fae: Dweomer of Glamour" val="2"/>
                    </traitlist>
                    <oaths>
                        <![CDATA[Something something]]>
                    </oaths>
                </changeling>
                """);
            TraitList testStatus = [new() { Name = "Cherished" }, new() { Name = "Noble" }];
            TraitList testArts = [new() { Name = "Chicanery: Fuddle", Value = "3", Note = "basic" }];
            TraitList testRealms = [new() { Name = "Fae: Dweomer of Glamour", Value = "2" }];
#pragma warning disable CS8604 // Possible null reference argument.
            var result = GrapevineLegacyXMLReader.LoadChangeling(xmlDoc.Root);
#pragma warning restore CS8604 // Possible null reference argument.
            Assert.NotNull(result);
            Assert.Equal("Sage", result.SeelieLegacy);
            Assert.Equal("Trickster", result.UnseelieLegacy);
            Assert.Equal("Unseelie", result.Court);
            Assert.Equal("Sidhe", result.Kith);
            Assert.Equal("Elder", result.Seeming);
            Assert.Equal("Leanhaun", result.House);
            Assert.Equal("Create Calm", result.Threshold);
            Assert.Equal(4, result.Glamour);
            Assert.Equal(3, result.Banality);
            Assert.Equal("Something something", result.Oaths);
            Assert.Equivalent(testStatus, result.ChangelingStatus);
            Assert.Equivalent(testArts, result.Arts);
            Assert.Equivalent(testRealms, result.Realms);
        }
    }
}
