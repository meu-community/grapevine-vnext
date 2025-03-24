using MEU.GV4.Data.Models;
using MEU.GV4.Data.Providers;

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
                Players = new()
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
                Players = new()
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
                        PlayerExperience = new ()
                        {
                            Unspent = 1,
                            Earned = 2,
                            Entries =
                            [
                                new () { EntryDate = DateTimeOffset.Parse("1/1/2020"), Change = 1, Type = ExperienceChangeType.Earned, Reason = "test", Earned = 1, Unspent = 1 },
                                new () { EntryDate = DateTimeOffset.Parse("1/1/2020"), Change = 1, Type = ExperienceChangeType.Earned, Reason = "test 2", Earned = 2, Unspent = 2 },
                                new () { EntryDate = DateTimeOffset.Parse("1/1/2020"), Change = 1, Type = ExperienceChangeType.Spent, Reason = "test spend", Earned = 2, Unspent = 1 }
                            ]
                        },
                        CreateDate = DateTimeOffset.Parse("1/1/2020 00:00:01 AM"),
                        ModifyDate = DateTimeOffset.Parse("1/1/2020 00:00:01 AM")
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
                  <player name="Leeroy Jenkins" id="12345" email="test@example.com" phone="000-000-0000" position="Player" status="Active" lastmodified="1/1/2020 00:00:01 AM">
                    <experience unspent="1" earned="2">
                        <entry date="1/1/2020" change="1" type="0" reason="test" earned="1" unspent="1"/>
                        <entry date="1/1/2020" change="1" type="0" reason="test 2" earned="2" unspent="2"/>
                        <entry date="1/1/2020" change="1" type="3" reason="test spend" earned="2" unspent="1"/>
                    </experience>
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
    }
}
