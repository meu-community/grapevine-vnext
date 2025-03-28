using MEU.GV4.Data.Models.METClassic;
using MEU.GV4.Data.Models;
using MEU.GV4.Data.Providers;
using System.Xml;

namespace MEU.GV4.Data.Tests.Providers
{
    public class GrapevineLegacyXMLReaderVampire
    {
        [Fact(DisplayName = "Can Load Vampire Character Data")]
        public void CanLoadVampireCharacterData()
        {
            TraitList testStatus = [new() { Name = "Acknowleged" }, new() { Name = "Overrated" }];
            TraitList testDisciplines = [new() { Name = "Auspex: Heightened Senses", Value = "3", Note = "basic" }, new() { Name = "Dominate: Command", Value = "3", Note = "basic" }];
            TraitList testRituals = [new() { Name = "Basic: Blood Mead", Value = "2" }];
            TraitList testBonds = [new() { Name = "L. Flint", Value = "2" }];
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("""
                <?xml version="1.0"?>
                <vampire sire="Mr. Popo" coterie="The Cool Klub" clan="Foo" sect="Cami" generation="13" blood="10" conscience="1" selfcontrol="2" courage="3" path="Potato" pathtraits="5">
                    <traitlist name="Status" abc="yes" display="1">
                        <trait name="Acknowleged"/>
                        <trait name="Overrated"/>
                    </traitlist>
                    <traitlist name="Disciplines" abc="yes" display="5">
                        <trait name="Auspex: Heightened Senses" val="3" note="basic" />
                        <trait name="Dominate: Command" val="3" note="basic" />
                    </traitlist>
                    <traitlist name="Rituals" abc="no" atomic="yes" display="5">
                        <trait name="Basic: Blood Mead" val="2"/>
                    </traitlist>
                    <traitlist name="Bonds" abc="yes" display="1">
                        <trait name="L. Flint" val="2"/>
                    </traitlist>
                </vampire>
                """);
#pragma warning disable CS8604 // Possible null reference argument.
            var result = GrapevineLegacyXMLReader.LoadVampire(xmlDoc.DocumentElement);
#pragma warning restore CS8604 // Possible null reference argument.
            Assert.NotNull(result);
            Assert.Equal("Foo", result.Clan);
            Assert.Equal("Cami", result.Sect);
            Assert.Equal("Mr. Popo", result.Sire);
            Assert.Equal("The Cool Klub", result.Coterie);
            Assert.Equal("Potato", result.Path);
            Assert.Equal(5, result.PathTraits);
            Assert.Equal(1, result.Conscience);
            Assert.Equal(2, result.SelfControl);
            Assert.Equal(3, result.Courage);
            Assert.Equal(10, result.Blood);
            Assert.Equal(13, result.Generation);
            Assert.Equivalent(testStatus, result.KindredStatus);
            Assert.Equivalent(testDisciplines, result.Disciplines);
            Assert.Equivalent(testRituals, result.Rituals);
            Assert.Equivalent(testBonds, result.Bonds);
        }

        [Fact(DisplayName = "Can load boons for a vampire character")]
        public void CanLoadBoons()
        {
            var expected = new Vampire()
            {
                Name = "Vladymur",
                Boons =
                [
                    new () { Type = "foo", Owed = true, Partner = "Stu Padasso", CreateDate = DateTimeOffset.Parse("1/1/2020") },
                    new () { Type = "bar", Owed = false, Partner = "Santa Claus", CreateDate = DateTimeOffset.Parse("1/1/2020") }
                ],
                CreateDate = DateTimeOffset.Parse("1/1/2020"),
                ModifyDate = DateTimeOffset.Parse("1/2/2020 00:00:01 AM")
            };
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml("""
                <?xml version="1.0"?>
                <vampire name="Vladymur" startdate="1/1/2020" lastmodified="1/2/2020 00:00:01 AM">
                    <boon type="foo" partner="Stu Padasso" owed="yes" date="1/1/2020"/>
                    <boon type="bar" partner="Santa Claus" owed="no" date="1/1/2020"/>
                </vampire>
                """);
            var result = GrapevineLegacyXMLReader.LoadVampire(xmlDocument.DocumentElement);
            Assert.NotNull(result);
            Assert.Equivalent(expected, result);
        }

    }
}
