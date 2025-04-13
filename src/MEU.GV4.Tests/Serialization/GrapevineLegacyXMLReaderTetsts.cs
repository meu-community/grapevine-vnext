using MEU.GV4.METClassic.Models;
using System.Xml.Linq;
using MEU.GV4.METClassic.Serialization;
using MEU.GV4.Base.Models;
using MEU.GV4.Base.Serialization;

namespace MEU.GV4.Tests.Serialization;

public class GrapevineLegacyXMLReaderTetsts
{
    [Fact(DisplayName = "Can Load Basic Game Data")]
    public void CanLoadBasicGameData()
    {
        var expected = new METGame()
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
        var expected = new METGame()
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

    [Fact(DisplayName = "Can Load Calendar Data")]
    public void CanLoadCalendarData()
    {
        List<CalendarEntry> expected =
        [
            new() { GameDate = DateOnly.Parse("1/1/2020"), GameTime = TimeOnly.Parse("4:00 PM"), Place = "xyz" },
            new() { GameDate = DateOnly.Parse("2/1/2020"), GameTime = TimeOnly.Parse("4:00 PM"), Place = "xyz", Notes = "foo" },
        ];

        var testGameData = """
            <?xml version="1.0"?>
            <grapevine version="3">
                <calendar>
                  <game date="1/1/2020" time="4:00 PM">
                    <place>
                      <![CDATA[xyz]]>
                    </place>
                  </game>
                  <game date="2/1/2020" time="4:00 PM">
                    <place>
                      <![CDATA[xyz]]>
                    </place>
                    <notes>
                      <![CDATA[foo]]>
                    </notes>
                  </game>
                </calendar>
            </grapevine>
            """;
        var reader = new GrapevineLegacyXMLReader();
        var result = reader.ReadData(testGameData);
        Assert.NotNull(result);
        Assert.Equivalent(expected, result.Calendar);
    }

    [Fact(DisplayName = "Can Load Calendar Data With Invalid Time")]
    public void CanLoadCalendarDataWithInvalidTime()
    {
        List<CalendarEntry> expected =
        [
            new() { GameDate = DateOnly.Parse("1/1/2020"), Place = "xyz" }
        ];

        var testGameData = """
            <?xml version="1.0"?>
            <grapevine version="3">
                <calendar>
                  <game date="1/1/2020" time="??">
                    <place>
                      <![CDATA[xyz]]>
                    </place>
                  </game>
                </calendar>
            </grapevine>
            """;
        var reader = new GrapevineLegacyXMLReader();
        var result = reader.ReadData(testGameData);
        Assert.NotNull(result);
        Assert.Equivalent(expected, result.Calendar);
    }

    [Fact(DisplayName = "Can Load Item Data")]
    public void CanLoadItemData()
    {
        var testGameData = """
            <?xml version="1.0"?>
            <grapevine version="3">
                <item name="Foobar" type="Charm" subtype="Enhancement" level="2" bonus="2" damage="Bashing" amount="2" conceal="None" lastmodified="1/1/2020">
                  <traitlist name="Tempers" abc="yes" display="2">
                    <trait name="Arete" val="2"/>
                  </traitlist>
                  <traitlist name="Abilities" abc="yes" display="1">
                    <trait name="Destroy Shield"/>
                  </traitlist>
                  <traitlist name="Negatives" abc="yes" negative="yes" display="1">
                    <trait name="Slow"/>
                  </traitlist>
                  <traitlist name="Availability" abc="yes" display="1">
                    <trait name="Scholarly" val="4"/>
                  </traitlist>
                  <powers>
                    <![CDATA[Has the power of foo]]>
                  </powers>
                  <appearance>
                    <![CDATA[Looks ugly]]>
                  </appearance>
                  <notes>
                    <![CDATA[what is this?]]>
                  </notes>
                </item>
            </grapevine>
            """;
        TraitList testTempers = [new() { Name = "Arete", Value = "2" }];
        TraitList testAbilities = [new() { Name = "Destroy Shield" }];
        TraitList testNegatives = [new() { Name = "Slow" }];
        TraitList testAvailability = [new() { Name = "Scholarly", Value = "4" }];
        var reader = new GrapevineLegacyXMLReader();
        var result = reader.ReadData(testGameData).Items[0];
        Assert.NotNull(result);
        Assert.Equal("Foobar", result.Name);
        Assert.Equal("Charm", result.Type);
        Assert.Equal("Enhancement", result.SubType);
        Assert.Equal(2, result.Level);
        Assert.Equal(2, result.Bonus);
        Assert.Equal("Bashing", result.Damage);
        Assert.Equal(2, result.Amount);
        Assert.Equal("None", result.Conceal);
        Assert.Equivalent(testTempers, result.Tempers);
        Assert.Equivalent(testAbilities, result.Abilities);
        Assert.Equivalent(testNegatives, result.Negatives);
        Assert.Equivalent(testAvailability, result.Availability);
        Assert.Equal("Has the power of foo", result.Powers);
        Assert.Equal("Looks ugly", result.Appearance);
        Assert.Equal("what is this?", result.Notes);
        Assert.Equal(DateTimeOffset.Parse("1/1/2020"), result.CreateDate);
        Assert.Equal(DateTimeOffset.Parse("1/1/2020"), result.ModifyDate);
    }

    [Fact(DisplayName = "Can Load Rotes")]
    public void CanLoadRotes()
    {
        var testGameData = """
            <?xml version="1.0"?>
            <grapevine version="3">
                <rote name="Foo" level="1" lastmodified="1/1/2020">
                  <traitlist name="Spheres" abc="no" atomic="yes" display="5">
                    <trait name="Correspondence: Apprentice" val="5" note="basic"/>
                  </traitlist>
                  <description>
                    <![CDATA[foo]]>
                  </description>
                  <grades>
                    <![CDATA[foo]]>
                  </grades>
                </rote>
            </grapevine>
            """;
        TraitList testSpheres = [new() { Name = "Correspondence: Apprentice", Value = "5", Note = "basic" }];
        var reader = new GrapevineLegacyXMLReader();
        var result = reader.ReadData(testGameData).Rotes[0];
        Assert.NotNull(result);
        Assert.Equal("Foo", result.Name);
        Assert.Equal(1, result.Level);
        Assert.Equal("foo", result.Description);
        Assert.Equal("foo", result.Grades);
        Assert.Equivalent(testSpheres, result.Spheres);
        Assert.Equal(DateTimeOffset.Parse("1/1/2020"), result.CreateDate);
        Assert.Equal(DateTimeOffset.Parse("1/1/2020"), result.ModifyDate);
    }

    [Fact(DisplayName = "Throws provider exception when data is empty")]
    public void ThrowsProviderExceptionWhenDataIsEmpty()
    {
        var reader = new GrapevineLegacyXMLReader();
        Assert.Throws<GrapevineProviderException>(() => reader.ReadData(string.Empty));
    }


    [Fact(DisplayName = "Throws provider exception when data is white space")]
    public void ThrowsProviderExceptionWhenDataIsWhitespace()
    {
        var reader = new GrapevineLegacyXMLReader();
        Assert.Throws<GrapevineProviderException>(() => reader.ReadData(" "));
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
        var expected = new METGame()
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
                    Address = "111 Elm St\nSomewhere, XY 12345", // Xml for Linq parses line breaks as \n instead of \r\n
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

        var xmlDoc = XDocument.Parse("""
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
        var result = GrapevineLegacyXMLReader.LoadExperience(xmlDoc.Root);
#pragma warning restore CS8604 // Possible null reference argument.
        Assert.Equivalent(expected, result);
    }

    [Fact(DisplayName = "Can Load All Supported Character Types")]
    public void CanLoadAllSupportedCharacterTypes()
    {
        var expectedTypes = GrapevineLegacyXMLReader.GetSupportedTypes();

        var xmlDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <foo>
            </foo>
            """);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var type in expectedTypes)
        {
            xmlDoc.Root.Add(XElement.Parse($"<{type}/>"));
        }
#pragma warning disable CS8604 // Possible null reference argument.
        var result = GrapevineLegacyXMLReader.LoadCharacters(xmlDoc.Root);
#pragma warning restore CS8604 // Possible null reference argument.
        var parsedTypes = new string[expectedTypes.Length];
        for (int i = 0; i < result.Count; i++)
        {
            parsedTypes[i] = result[i].GetType().Name.ToLower();
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        Assert.Equivalent(expectedTypes, parsedTypes);
    }

    [Fact(DisplayName = "Can load trait list by name")]
    public void CanLoadTraitListByName()
    {
        TraitList expected =
        [
            new() { Name = "a" }, new() { Name = "b", Value = "2" }, new() { Name = "c", Note = "foo" }
        ];

        var xmlDoc = XDocument.Parse("""
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
        var result = GrapevineLegacyXMLReader.LoadTraitList(xmlDoc.Root, "foo");
#pragma warning disable CS8604 // Possible null reference argument.
        Assert.Equivalent(expected, result);
    }

    [Fact(DisplayName = "Can mark IsNPC as true when set in xml as yes")]
    public void CanMarkNPCWhenSetInXml()
    {
        var expected = new METGame()
        {
            Title = "TEST CHRONICLE",
            Players = [],
            Characters =
            [
                new Vampire()
                {
                    Name = "Vladymur",
                    IsNPC = true,
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
                <vampire name="Vladymur" npc="yes" startdate="1/1/2020" lastmodified="1/2/2020 00:00:01 AM" />
            </grapevine>
            """;
        var reader = new GrapevineLegacyXMLReader();
        var result = reader.ReadData(testGameData);
        Assert.NotNull(result);
        Assert.Equivalent(expected, result);
    }
}
