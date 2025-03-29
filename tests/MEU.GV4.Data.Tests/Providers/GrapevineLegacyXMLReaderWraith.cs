using MEU.GV4.Data.Models;
using MEU.GV4.Data.Providers;
using System.Xml;

namespace MEU.GV4.Data.Tests.Providers
{
    public class GrapevineLegacyXMLReaderWraith
    {
        [Fact(DisplayName = "Can Load Wraith Character Data")]
        public void CanLoadWraithCharacterData()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("""
                <?xml version="1.0"?>
                <wraith ethnos="Risen" guild="Spook" faction="Renegade" legion="The Emerald Legion" rank="Centurion" pathos="2" corpus="2" shadowarchetype="Anarchist" shadowplayer="That Guy" angst="3">
                  <traitlist name="Status" abc="yes" display="1">
                    <trait name="Heretic" val="2"/>
                    <trait name="Renegade"/>
                  </traitlist>
                  <passions>
                    <![CDATA[None]]>
                  </passions>
                  <fetters>
                    <![CDATA[Something something]]>
                  </fetters>
                  <life>
                    <![CDATA[No life]]>
                  </life>
                  <death>
                    <![CDATA[Boring]]>
                  </death>
                  <haunt>
                    <![CDATA[Yes]]>
                  </haunt>
                  <regret>
                    <![CDATA[No regerts]]>
                  </regret>
                  <traitlist name="Arcanoi" abc="no" atomic="yes" display="5">
                    <trait name="Behest: Link" val="0" note="innate"/>
                  </traitlist>
                  <darkpassions>
                    <![CDATA[Stuff]]>
                  </darkpassions>
                  <traitlist name="Thorns" abc="yes" atomic="yes" negative="yes" display="4">
                    <trait name="Devil's Dare" val="5"/>
                  </traitlist>
                </wraith>
                """);
            TraitList testStatus = [new() { Name = "Heretic", Value = "2" }, new() { Name = "Renegade" }];
            TraitList testArcanoi = [new() { Name = "Behest: Link", Value = "0", Note = "innate" }];
            TraitList testThorns = [new() { Name = "Devil's Dare", Value = "5" }];
#pragma warning disable CS8604 // Possible null reference argument.
            var result = GrapevineLegacyXMLReader.LoadWraith(xmlDoc.DocumentElement);
#pragma warning restore CS8604 // Possible null reference argument.
            Assert.NotNull(result);
            Assert.Equal("Risen", result.Ethnos);
            Assert.Equal("Spook", result.Guild);
            Assert.Equal("Renegade", result.Faction);
            Assert.Equal("The Emerald Legion", result.Legion);
            Assert.Equal("Centurion", result.Rank);
            Assert.Equal(2, result.Pathos);
            Assert.Equal(2, result.Corpus);
            Assert.Equal("Anarchist", result.ShadowArchetype);
            Assert.Equal("That Guy", result.ShadowPlayer);
            Assert.Equal(3, result.Angst);
            Assert.Equal("None", result.Passions);
            Assert.Equal("Something something", result.Fetters);
            Assert.Equal("No life", result.Life);
            Assert.Equal("Boring", result.Death);
            Assert.Equal("Yes", result.Haunt);
            Assert.Equal("No regerts", result.Regret);
            Assert.Equal("Stuff", result.DarkPassions);
            Assert.Equivalent(testArcanoi, result.Arcanoi);
            Assert.Equivalent(testStatus, result.WraithStatus);
            Assert.Equivalent(testThorns, result.ThornList);
        }
    }
}
