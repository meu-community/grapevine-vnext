using MEU.GV4.Data.Models;
using MEU.GV4.METClassic.Serialization;
using System.Xml.Linq;

namespace MEU.GV4.Data.Tests.Providers;

/// <summary>
/// Tests loading of traits that are common to all MET characters
/// </summary>
public class GrapevineLegacyXMLReaderCommonTraitsTests
{
    [Fact(DisplayName = "Can Load Common Traits")]
    public void CanLoadCommonTraits()
    {
        var xmlDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <vampire name="Vladymur" title="Drama Queen" id="12345" nature="foo" demeanor="bar" willpower="3" tempwillpower="2" physicalmax="10" socialmax="10" mentalmax="10" player="Fred Smith" status="Active" startdate="1/1/2020" lastmodified="1/2/2020 00:00:01 AM">
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
                    <trait name="Acknowleged"/>
                    <trait name="Overrated"/>
                </traitlist>
                <traitlist name="Abilities" abc="yes" display="1">
                    <trait name="Driving" val="3" note="Fast" />
                    <trait name="Lore: Bacon" val="2"/>
                </traitlist>
                <traitlist name="Influences" abc="yes" display="1">
                  <trait name="Street" val="2"/>
                </traitlist>
                <traitlist name="Backgrounds" abc="yes" display="1">
                  <trait name="Generation" val="2"/>
                  <trait name="Resources" val="3"/>
                </traitlist>
                <traitlist name="Equipment" abc="yes" display="1">
                    <trait name="Flame Thrower" note="+0, 2 Aggravated, Heavy, Hot"/>
                </traitlist>
                <traitlist name="Locations" abc="yes" atomic="yes" display="5">
                    <trait name="The Crypt"/>
                </traitlist>
                <traitlist name="Merits" abc="yes" atomic="yes" display="4">
                  <trait name="Light Sleeper" val="2"/>
                </traitlist>
                <traitlist name="Flaws" abc="yes" atomic="yes" negative="yes" display="4">
                    <trait name="Bad Breath" val="3"/>
                </traitlist>
                <traitlist name="Miscellaneous" abc="no" display="1">
                    <trait name="FOO"/>
                </traitlist>
                <traitlist name="Derangements" abc="yes" atomic="yes" negative="yes" display="5">
                    <trait name="Something"/>
                </traitlist>
                <traitlist name="Health Levels" abc="no" display="1">
                  <trait name="Healthy" val="2"/>
                  <trait name="Bruised" val="3"/>
                  <trait name="Wounded" val="2"/>
                  <trait name="Incapacitated"/>
                  <trait name="Torpor"/>
                </traitlist>
                <biography>
                    <![CDATA[Born and raised in South Transylvania (actually Detroit, but don't tell him that)]]>
                </biography>
                <notes>
                    <![CDATA[My notes]]>
                </notes>
            </vampire>
            """);
        TraitList testTraitList = [new() { Name = "a" }, new() { Name = "b", Value = "2" }, new() { Name = "c" }];
        TraitList testNegativeTraits = [new() { Name = "a" }];
        TraitList testAbilities = [new() { Name = "Driving", Value = "3", Note = "Fast" }, new() { Name = "Lore: Bacon", Value = "2" }];
        TraitList testEquipment = [new() { Name = "Flame Thrower", Note = "+0, 2 Aggravated, Heavy, Hot" }];
        TraitList testLocations = [new() { Name = "The Crypt" }];
        TraitList testInfluences = [new() { Name = "Street", Value = "2" }];
        TraitList testBackgrounds = [new() { Name = "Generation", Value = "2" }, new() { Name = "Resources", Value = "3"}];
        TraitList testMerits = [new() { Name = "Light Sleeper", Value = "2" }];
        TraitList testFlaws = [new() { Name = "Bad Breath", Value = "3" }];
        TraitList testMisc = [new() { Name = "FOO" }];
        TraitList testDerangements = [new() { Name = "Something" }];
        TraitList testHealth = [new() { Name = "Healthy", Value = "2" }, new() { Name = "Bruised", Value = "3" }, new() { Name = "Wounded", Value = "2" }, new() { Name = "Incapacitated" }, new() { Name = "Torpor" }];
#pragma warning disable CS8604 // Possible null reference argument.
        var result = GrapevineLegacyXMLReader.LoadVampire(xmlDoc.Root);
#pragma warning restore CS8604 // Possible null reference argument.
        Assert.NotNull(result);
        Assert.Equal("Vladymur", result.Name);
        Assert.Equal("Drama Queen", result.Title);
        Assert.Equal("12345", result.ID);
        Assert.Equal("Fred Smith", result.Player);
        Assert.Equal("foo", result.Nature);
        Assert.Equal("bar", result.Demeanor);
        Assert.Equal(3, result.Willpower);
        Assert.Equal(2, result.TempWillpower);
        Assert.Equal(10, result.PhysicalMax);
        Assert.Equal(10, result.SocialMax);
        Assert.Equal(10, result.MentalMax);
        Assert.Equal("My notes", result.Notes);
        Assert.Equal("Born and raised in South Transylvania (actually Detroit, but don't tell him that)", result.Biography);
        Assert.Equivalent(testTraitList, result.PhysicalTraits);
        Assert.Equivalent(testNegativeTraits, result.NegativePhysicalTraits);
        Assert.Equivalent(testTraitList, result.SocialTraits);
        Assert.Equivalent(testNegativeTraits, result.NegativeSocialTraits);
        Assert.Equivalent(testTraitList, result.MentalTraits);
        Assert.Equivalent(testNegativeTraits, result.NegativeMentalTraits);
        Assert.Equivalent(testAbilities, result.Abilities);
        Assert.Equivalent(testEquipment, result.Equipment);
        Assert.Equivalent(testLocations, result.Locations);
        Assert.Equivalent(testInfluences, result.Influences);
        Assert.Equivalent(testBackgrounds, result.Backgrounds);
        Assert.Equivalent(testMerits, result.Merits);
        Assert.Equivalent(testFlaws, result.Flaws);
        Assert.Equivalent(testMisc, result.Miscellanious);
        Assert.Equivalent(testDerangements, result.Derangements);
        Assert.Equivalent(testHealth, result.Health);
        Assert.Equal(DateTimeOffset.Parse("1/1/2020"), result.CreateDate);
        Assert.Equal(DateTimeOffset.Parse("1/2/2020 00:00:01 AM"), result.ModifyDate);
    }

}
