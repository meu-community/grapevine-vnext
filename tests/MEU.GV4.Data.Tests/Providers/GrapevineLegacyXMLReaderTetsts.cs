using MEU.GV4.Data.Models;
using MEU.GV4.Data.Models.METClassic;
using MEU.GV4.Data.Providers;
using System.Xml;

namespace MEU.GV4.Data.Tests.Providers
{
    public class GrapevineLegacyXMLReaderTetsts
    {
        [Fact(DisplayName = "Can Load Basic Game Data")]
        public void CanLoadBasicGameData()
        {
            var expected = new Game()
            {
                Title = "TEST CHRONICLE",
                Website = "https://example.com",
                EMail = "test@example.com",
                Phone = "000-000-0000",
                UsualTime = "4:00 PM",
                UsualPlace = "That place",
                Description = "TEST DESCRIPTION",
                Players = new(),
                Characters = new()
            };
            var testGameData = """
                <?xml version="1.0"?>
                <grapevine version="3" chronicle="TEST CHRONICLE"
                    website="https://example.com"
                    email="test@example.com"
                    phone="000-000-0000"
                    usualtime="4:00 PM"
                    randomtraits="7,5,3,5,5,5,5">
                    <usualplace>
                      <![CDATA[That place]]>
                    </usualplace>
                    <description>
                      <![CDATA[TEST DESCRIPTION]]>
                    </description>
                </grapevine>
                """;
            var reader = new GrapevineLegacyXMLReader();
            var result = reader.ReadData(testGameData);
            Assert.NotNull(result);
            Assert.Equivalent(expected, result);
        }

        [Fact(DisplayName = "Can Load With Empty Values")]
        public void CanLoadWithEmptyValues()
        {
            var expected = new Game()
            {
                Title = "TEST CHRONICLE",
                Website = null,
                EMail = null,
                Phone = null,
                UsualTime = null,
                UsualPlace = null,
                Description = null,
                Players = new(),
                Characters = new()
            };
            var testGameData = """
                <?xml version="1.0"?>
                <grapevine version="3" chronicle="TEST CHRONICLE">
                    <usualplace>
                    </usualplace>
                    <description>
                    </description>
                </grapevine>
                """;
            var reader = new GrapevineLegacyXMLReader();
            var result = reader.ReadData(testGameData);
            Assert.NotNull(result);
            Assert.Equivalent(expected, result);
        }

        [Fact(DisplayName = "Throws provider exception when data is empty")]
        public void ThrowsProviderExceptionWhenDataIsEmpty()
        {
            var reader = new GrapevineLegacyXMLReader();
            Assert.Throws<GrapevineProviderException>(() => reader.ReadData(String.Empty));
        }

        [Fact(DisplayName = "Throws provider exception when data is null")]
        public void ThrowsProviderExceptionWhenDataIsNull()
        {
            string? testGameData = null;
            var reader = new GrapevineLegacyXMLReader();
#pragma warning disable CS8604 // Possible null reference argument.
            Assert.Throws<GrapevineProviderException>(() => reader.ReadData(testGameData));
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [Fact(DisplayName = "Throws provider exception when root element is incorrect")]
        public void ThrowsProviderExceptionWhenRootIsIncorrect()
        {
            var testGameData = """
                <?xml version="1.0"?>
                <foo>
                </foo>
                """;
            var reader = new GrapevineLegacyXMLReader();
            Assert.Throws<GrapevineProviderException>(() => reader.ReadData(testGameData));
        }

        [Fact(DisplayName = "Can Load PlayerData")]
        public void CanLoadPlayerData()
        {
            var expected = new Game()
            {
                Title = "TEST CHRONICLE",
                Players =
                [
                    new () {
                        Name = "Leeroy Jenkins",
                        ID = "12345",
                        EMail = "test@example.com",
                        Phone = "000-000-0000",
                        Position = "Player",
                        Status = "Active",
                        Address = """
                        111 Elm St
                        Somewhere, XY 12345
                        """,
                        Notes = "Player notes",
                        PlayerExperience = new () { Entries = new () },
                        CreateDate = DateTimeOffset.Parse("1/1/2020 00:00:01 AM"),
                        ModifyDate = DateTimeOffset.Parse("1/1/2020 00:00:01 AM")
                    }
                ],
                Characters = []
            };
            var testGameData = """
                <?xml version="1.0"?>
                <grapevine version="3" chronicle="TEST CHRONICLE">
                    <usualplace>
                    </usualplace>
                    <description>
                    </description>
                  <player name="Leeroy Jenkins" id="12345" email="test@example.com" phone="000-000-0000" position="Player" status="Active" lastmodified="1/1/2020 00:00:01 AM">
                    <experience unspent="0" earned="0" />
                    <address>
                      <![CDATA[111 Elm St
                Somewhere, XY 12345]]>
                    </address>
                    <notes>
                      <![CDATA[Player notes]]>
                    </notes>
                  </player>
                </grapevine>
                """;
            var reader = new GrapevineLegacyXMLReader();
            var result = reader.ReadData(testGameData);
            Assert.NotNull(result);
            Assert.Equivalent(expected, result);
        }

        [Fact(DisplayName = "Can load experience data")]
        public void CanLoadExperienceData()
        {
            var expected = new Experience()
            {
                Unspent = 1,
                Earned = 2,
                Entries =
                [
                    new () { EntryDate = DateTimeOffset.Parse("1/1/2020"), Change = 1, Type = ExperienceChangeType.Earned, Reason = "test", Earned = 1, Unspent = 1 },
                    new () { EntryDate = DateTimeOffset.Parse("1/1/2020"), Change = 1, Type = ExperienceChangeType.Earned, Reason = "test 2", Earned = 2, Unspent = 2 },
                    new () { EntryDate = DateTimeOffset.Parse("1/1/2020"), Change = 1, Type = ExperienceChangeType.Spent, Reason = "test spend", Earned = 2, Unspent = 1 }
                ]
            };

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("""
                <?xml version="1.0"?>
                <foo>
                    <experience unspent="1" earned="2">
                        <entry date="1/1/2020" change="1" type="0" reason="test" earned="1" unspent="1"/>
                        <entry date="1/1/2020" change="1" type="0" reason="test 2" earned="2" unspent="2"/>
                        <entry date="1/1/2020" change="1" type="3" reason="test spend" earned="2" unspent="1"/>
                    </experience>
                </foo>
                """);

#pragma warning disable CS8604 // Possible null reference argument.
            var result = GrapevineLegacyXMLReader.LoadExperience(xmlDoc.DocumentElement);
#pragma warning restore CS8604 // Possible null reference argument.
            Assert.Equivalent(expected, result);
        }

        [Fact(DisplayName = "Can load trait list by name")]
        public void CanLoadTraitListByName()
        {
            TraitList expected =
            [
                new() { Name = "a" }, new() { Name = "b", Value = "2" }, new() { Name = "c", Note = "foo" }
            ];

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("""
                <?xml version="1.0"?>
                <foo>
                    <traitlist name="bar" abc="yes" display="1">
                        <trait name="a" />
                    </traitlist>
                    <traitlist name="foo" abc="yes" display="1">
                        <trait name="a" />
                        <trait name="b" val="2" />
                        <trait name="c" note="foo" />
                    </traitlist>
                </foo>
                """);
#pragma warning disable CS8604 // Possible null reference argument.
            var result = GrapevineLegacyXMLReader.LoadTraitList(xmlDoc.DocumentElement, "foo");
#pragma warning disable CS8604 // Possible null reference argument.
            Assert.Equivalent(expected, result);
        }

        [Fact(DisplayName = "Can Load Vampire Character Data")]
        public void CanLoadVampireCharacterData()
        {
            var expected = new Game()
            {
                Title = "TEST CHRONICLE",
                Players = [],
                Characters =
                [
                    new Vampire()
                    {
                        Name = "Vladymur",
                        Player = "Fred Smith",
                        Status = "Active",
                        Clan = "Foo",
                        Sect = "Cami",
                        Nature = "foo",
                        Demeanor = "bar",
                        Path = "Potato",
                        PathTraits = 5,
                        Conscience = 1,
                        SelfControl = 2,
                        Courage = 3,
                        Willpower = 3,
                        Blood = 10,
                        PhysicalMax = 10,
                        Notes = "My notes",
                        Biography = "Born and raised in South Transylvania (actually Detroit, but don't tell him that)",
                        PhysicalTraits = [ new () { Name = "a"}, new () { Name = "b", Value = "2"}, new () { Name = "c"} ],
                        NegativePhysicalTraits = [ new () { Name = "a"} ],
                        SocialTraits = [ new () { Name = "a"}, new () { Name = "b", Value = "2"}, new () { Name = "c"} ],
                        NegativeSocialTraits = [ new () { Name = "a"} ],
                        MentalTraits = [ new () { Name = "a"}, new () { Name = "b", Value = "2"}, new () { Name = "c"}],
                        NegativeMentalTraits = [ new () { Name = "a"} ],
                        Abilities = [ new () { Name = "Driving", Value = "3", Note = "Fast" }, new () { Name = "Lore: Bacon", Value = "2" }],
                        Generation = 13,
                        KindredStatus = [ new () { Name = "Acknowleged" }, new () { Name = "Overrated" } ],
                        Disciplines = [ new () { Name = "Auspex: Heigthened Senses", Value = "3", Note = "basic"}, new () { Name = "Dominate: Command", Value = "3", Note = "basic"} ],
                        CreateDate = DateTimeOffset.Parse("1/1/2020"),
                        ModifyDate = DateTimeOffset.Parse("1/2/2020 00:00:01 AM")
                    }
                ]
            };
            var testGameData = """
                <?xml version="1.0"?>
                <grapevine version="3" chronicle="TEST CHRONICLE">
                    <usualplace>
                    </usualplace>
                    <description>
                    </description>
                    <vampire name="Vladymur" nature="foo" demeanor="bar" clan="Foo" sect="Cami" generation="13" blood="10" willpower="3" conscience="1" selfcontrol="2" courage="3" path="Potato" pathtraits="5" physicalmax="10" player="Fred Smith" status="Active" startdate="1/1/2020" lastmodified="1/2/2020 00:00:01 AM">
                        <experience unspent="0" earned="0" />
                        <traitlist name="Physical" abc="yes" display="1">
                            <trait name="a" />
                            <trait name="b" val="2" />
                            <trait name="c" />
                        </traitlist>
                        <traitlist name="Social" abc="yes" display="1">
                            <trait name="a" />
                            <trait name="b" val="2" />
                            <trait name="c" />
                        </traitlist>
                        <traitlist name="Mental" abc="yes" display="1">
                            <trait name="a" />
                            <trait name="b" val="2" />
                            <trait name="c" />
                        </traitlist>
                        <traitlist name="Negative Physical" abc="yes" negative="yes" display="1">
                            <trait name="a" />
                        </traitlist>
                        <traitlist name="Negative Social" abc="yes" negative="yes" display="1">
                            <trait name="a" />
                        </traitlist>
                        <traitlist name="Negative Mental" abc="yes" negative="yes" display="1">
                            <trait name="a" />
                        </traitlist>
                        <traitlist name="Status" abc="yes" display="1">
                          <trait name="Acknowledged"/>
                          <trait name="Overrated"/>
                        </traitlist>
                        <traitlist name="Abilities" abc="yes" display="1">
                          <trait name="Driving" val="3" note="Fast" />
                          <trait name="Lore: Bacon" val="2"/>
                        </traitlist>
                        <traitlist name="Disciplines" abc="yes" display="5">
                            <trait name="Auspex: Heightened Senses" val="3" note="basic" />
                            <trait name="Dominate: Command" val="3" note="basic" />
                        </traitlist>
                        <biography>
                          <![CDATA[Born and raised in South Transylvania (actually Detroit, but don't tell him that)]]>
                        </biography>
                        <notes>
                            <![CDATA[My notes]]>
                        </notes>
                    </vampire>
                </grapevine>
                """;
            var reader = new GrapevineLegacyXMLReader();
            var result = reader.ReadData(testGameData);
            Assert.NotNull(result);
            Assert.Equivalent(expected, result);
        }
    }
}
