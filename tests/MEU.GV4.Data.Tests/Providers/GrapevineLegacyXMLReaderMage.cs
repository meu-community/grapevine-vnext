using MEU.GV4.Data.Models;
using MEU.GV4.Data.Providers;
using System.Xml.Linq;

namespace MEU.GV4.Data.Tests.Providers
{
    public class GrapevineLegacyXMLReaderMage
    {
        [Fact(DisplayName = "Can Load Mage Character Data")]
        public void CanLoadMageCharacterData()
        {
            var xmlDoc = XDocument.Parse("""
                <?xml version="1.0"?>
                <mage essence="Dynamic" tradition="Order of Hermes" cabal="Foo" rank="Apprentice" faction="House Flambeau" 
                arete="3" quintessence="3" paradox="1"
                temparete="0" tempquintessence="0" tempparadox="0">
                  <traitlist name="Resonance" abc="yes" display="1">
                    <trait name="Dynamic"/>
                    <trait name="Entropic"/>
                  </traitlist>
                  <traitlist name="Reputation" abc="yes" display="1">
                    <trait name="Accepted"/>
                  </traitlist>
                  <traitlist name="Spheres" abc="no" atomic="yes" display="5">
                    <trait name="Correspondence: Apprentice" val="5" note="basic"/>
                  </traitlist>
                  <traitlist name="Rotes" abc="yes" atomic="yes" display="5">
                    <trait name="Foo" note="Lv. 1"/>
                  </traitlist>
                  <foci>
                    <![CDATA[My foci]]>
                  </foci>
                </mage>
                """);
            TraitList testResonance = [new() { Name = "Dynamic" }, new() { Name = "Entropic" }];
            TraitList testReputation = [new() { Name = "Accepted" }];
            TraitList testSpheres = [new() { Name = "Correspondence: Apprentice", Value = "5", Note = "basic" }];
            TraitList testRotes = [new() { Name = "Foo", Note = "Lv. 1" }];
#pragma warning disable CS8604 // Possible null reference argument.
            var result = GrapevineLegacyXMLReader.LoadMage(xmlDoc.Root);
#pragma warning restore CS8604 // Possible null reference argument.
            Assert.NotNull(result);
            Assert.Equal("Dynamic", result.Essence);
            Assert.Equal("Order of Hermes", result.Tradition);
            Assert.Equal("Foo", result.Cabal);
            Assert.Equal("Apprentice", result.Rank);
            Assert.Equal("House Flambeau", result.Faction);
            Assert.Equal(3, result.Arete);
            Assert.Equal(3, result.Quintessence);
            Assert.Equal(1, result.Paradox);
            Assert.Equal(0, result.TempArete);
            Assert.Equal(0, result.TempQuintessence);
            Assert.Equal(0, result.TempParadox);
            Assert.Equal("My foci", result.Foci);
            Assert.Equivalent(testResonance, result.Resonance);
            Assert.Equivalent(testReputation, result.Reputation);
            Assert.Equivalent(testSpheres, result.Spheres);
            Assert.Equivalent(testRotes, result.Rotes);
        }
    }
}
