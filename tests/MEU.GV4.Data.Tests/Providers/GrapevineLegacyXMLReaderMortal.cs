using MEU.GV4.Data.Models;
using MEU.GV4.Data.Providers;
using System.Xml.Linq;

namespace MEU.GV4.Data.Tests.Providers
{
    public class GrapevineLegacyXMLReaderMortal
    {
        [Fact(DisplayName = "Can Load Mortal Character Data")]
        public void CanLoadMortalCharacterData()
        {
            TraitList testHumanity = [new() { Name = "Benevolent" }, new() { Name = "Honorable" }];
            TraitList testNumina = [new() { Name = "Psychic: Cyberkinesis: Switch", Value = "3", Note = "basic" }];
            var xmlDoc = XDocument.Parse("""
                <?xml version="1.0"?>
                <mortal motivation="Ennui" association="Local Police" regnant="Joe"
                    humanity="1" blood="2" conscience="1" selfcontrol="2" courage="3" truefaith="6"
                    temphumanity="1" tempblood="1" tempconscience="1" tempselfcontrol="1" tempcourage="1" temptruefaith="1">
                    <traitlist name="Humanity" abc="yes" display="1">
                      <trait name="Benevolent"/>
                      <trait name="Honorable"/>
                    </traitlist>
                    <traitlist name="Numina" abc="no" atomic="yes" display="5">
                      <trait name="Psychic: Cyberkinesis: Switch" val="3" note="basic"/>
                    </traitlist>
                </mortal>
                """);
#pragma warning disable CS8604 // Possible null reference argument.
            var result = GrapevineLegacyXMLReader.LoadMortal(xmlDoc.Root);
#pragma warning restore CS8604 // Possible null reference argument.
            Assert.NotNull(result);
            Assert.Equal("Ennui", result.Motivation);
            Assert.Equal("Local Police", result.Association);
            Assert.Equal("Joe", result.Regnant);
            Assert.Equal(1, result.Humanity);
            Assert.Equal(2, result.Blood);
            Assert.Equal(1, result.Conscience);
            Assert.Equal(2, result.SelfControl);
            Assert.Equal(3, result.Courage);
            Assert.Equal(6, result.TrueFaith);
            Assert.Equal(1, result.TempBlood);
            Assert.Equal(1, result.TempConscience);
            Assert.Equal(1, result.TempSelfControl);
            Assert.Equal(1, result.TempCourage);
            Assert.Equal(1, result.TempHumanity);
            Assert.Equal(1, result.TempTrueFaith);
            Assert.Equivalent(testHumanity, result.HumanityList);
            Assert.Equivalent(testNumina, result.NuminaList);
        }
    }
}
