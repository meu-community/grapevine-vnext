using MEU.GV4.Data.Models;
using MEU.GV4.Data.Providers;
using System.Xml.Linq;

namespace MEU.GV4.Data.Tests.Providers
{
    public class GrapevineLegacyXMLReaderWerewolf
    {
        [Fact(DisplayName = "Can Load Werewolf Character Data")]
        public void CanLoadWerewolfCharacterData()
        {
            var xmlDoc = XDocument.Parse("""
                <?xml version="1.0"?>
                <werewolf  tribe="x" breed="y" auspice="z" rank="Fostern" pack="Pack" totem="American Dream" camp="Bane Tenders" position="Fool"
                    notoriety="2" rage="3" gnosis="3" honor="1" glory="1" wisdom="1"
                    temprage="0" tempgnosis="0" temphonor="0" tempglory="0" tempwisdom="0">
                    <traitlist name="Features" abc="yes" atomic="yes" display="5">
                        <trait name="Scar: Brain Damage"/>
                    </traitlist>
                    <traitlist name="Gifts" abc="no" atomic="yes" display="5">
                        <trait name="Homid: Jam Technology" val="3" note="basic"/>
                    </traitlist>
                    <traitlist name="Rites" abc="no" atomic="yes" display="5">
                        <trait name="Accord: Rite of Cleansing" val="2" note="basic"/>
                    </traitlist>
                    <traitlist name="Honor" abc="yes" display="1">
                        <trait name="Admirable"/>
                    </traitlist>
                    <traitlist name="Glory" abc="yes" display="1">
                        <trait name="Brash"/>
                    </traitlist>
                    <traitlist name="Wisdom" abc="yes" display="1">
                        <trait name="Pragmatic"/>
                    </traitlist>
                    <traitlist name="Locations" abc="yes" atomic="yes" display="5">
                        <trait name="London"/>
                    </traitlist>
                </werewolf>
                """);
            TraitList testFeatures = [new() { Name = "Scar: Brain Damage" }];
            TraitList testGifts = [new() { Name = "Homid: Jam Technology", Value = "3", Note = "basic" }];
            TraitList testRites = [new() { Name = "Accord: Rite of Cleansing", Value = "2", Note = "basic" }];
            TraitList testHonorList = [new() { Name = "Admirable" }];
            TraitList testGloryList = [new() { Name = "Brash" }];
            TraitList testWisdomList = [new() { Name = "Pragmatic" }];
#pragma warning disable CS8604 // Possible null reference argument.
            var result = GrapevineLegacyXMLReader.LoadWerewolf(xmlDoc.Root);
#pragma warning restore CS8604 // Possible null reference argument.
            Assert.NotNull(result);
            Assert.Equal("x", result.Tribe);
            Assert.Equal("y", result.Breed);
            Assert.Equal("z", result.Auspice);
            Assert.Equal("Fostern", result.Rank);
            Assert.Equal("Pack", result.Pack);
            Assert.Equal("American Dream", result.Totem);
            Assert.Equal("Bane Tenders", result.Camp);
            Assert.Equal("Fool", result.Position);
            Assert.Equal(2, result.Notoriety);
            Assert.Equal(3, result.Rage);
            Assert.Equal(3, result.Gnosis);
            Assert.Equal(1, result.Honor);
            Assert.Equal(1, result.Glory);
            Assert.Equal(1, result.Wisdom);
            Assert.Equal(0, result.TempRage);
            Assert.Equal(0, result.TempGnosis);
            Assert.Equal(0, result.TempHonor);
            Assert.Equal(0, result.TempGlory);
            Assert.Equal(0, result.TempWisdom);
            Assert.Equivalent(testFeatures, result.Features);
            Assert.Equivalent(testGifts, result.Gifts);
            Assert.Equivalent(testRites, result.Rites);
            Assert.Equivalent(testHonorList, result.HonorList);
            Assert.Equivalent(testGloryList, result.GloryList);
            Assert.Equivalent(testWisdomList, result.WisdomList);
        }
    }
}
