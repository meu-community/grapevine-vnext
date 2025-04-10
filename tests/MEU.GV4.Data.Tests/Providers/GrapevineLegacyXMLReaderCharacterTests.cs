using MEU.GV4.Data.Models;
using MEU.GV4.Data.Models.METClassic;
using MEU.GV4.Data.Providers;
using System.Xml.Linq;

namespace MEU.GV4.Data.Tests.Providers;

public class GrapevineLegacyXMLReaderCharacterTests
{
    [Fact(DisplayName = "Can Load Changeling Character Data")]
    public void CanLoadChangelingCharacterData()
    {
        var xmlDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <changeling seelie="Sage" unseelie="Trickster" court="Unseelie" kith="Sidhe" seeming="Elder" house="Leanhaun" threshold="Create Calm" 
            glamour="4" banality="3" tempglamour="2" tempbanality="1">
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
        Assert.Equal(1, result.TempBanality);
        Assert.Equal(2, result.TempGlamour);
        Assert.Equal("Something something", result.Oaths);
        Assert.Equivalent(testStatus, result.ChangelingStatus);
        Assert.Equivalent(testArts, result.Arts);
        Assert.Equivalent(testRealms, result.Realms);
    }

    [Fact(DisplayName = "Can Load Demon Character Data")]
    public void CanLoadDemonCharacterData()
    {
        var xmlDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <demon house="Defiler" faction="Cryptic" torment="3" faith="3" conscience="2" conviction="3" courage="1"
                tempfaith="0" tempconscience="0" tempconviction="0" tempcourage="0">
                <traitlist name="Lores" abc="no" atomic="yes" display="5">
                    <trait name="Fundament: Manipulate Gravity" val="3" note="basic"/>
                </traitlist>
                <traitlist name="Apocalyptic Form" abc="no" atomic="yes" display="5">
                    <trait name="Ishhara, Longing: Enhanced Social Traits" val="0"/>
                </traitlist>
            </demon>
            """);
        TraitList testLores = [new() { Name = "Fundament: Manipulate Gravity", Value = "3", Note = "basic" }];
        TraitList testVisage = [new() { Name = "Ishhara, Longing: Enhanced Social Traits", Value = "0" }];
#pragma warning disable CS8604 // Possible null reference argument.
        var result = GrapevineLegacyXMLReader.LoadDemon(xmlDoc.Root);
#pragma warning restore CS8604 // Possible null reference argument.
        Assert.NotNull(result);
        Assert.Equal("Defiler", result.House);
        Assert.Equal("Cryptic", result.Faction);
        Assert.Equal(3, result.Torment);
        Assert.Equal(3, result.Faith);
        Assert.Equal(2, result.Conscience);
        Assert.Equal(3, result.Conviction);
        Assert.Equal(1, result.Courage);
        Assert.Equal(0, result.TempFaith);
        Assert.Equal(0, result.TempConscience);
        Assert.Equal(0, result.TempConviction);
        Assert.Equal(0, result.TempCourage);
        Assert.Equivalent(testLores, result.Lores);
        Assert.Equivalent(testVisage, result.Visage);
    }

    [Fact(DisplayName = "Can Load Fera Character Data")]
    public void CanLoadFeraCharacterData()
    {
        var xmlDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <fera fera="x" breed="y" auspice="z" rank="Fostern" pack="Pack" totem="American Dream" camp="Bane Tenders" position="Fool"
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
            </fera>
            """);
        TraitList testFeatures = [new() { Name = "Scar: Brain Damage" }];
        TraitList testGifts = [new() { Name = "Homid: Jam Technology", Value = "3", Note = "basic" }];
        TraitList testRites = [new() { Name = "Accord: Rite of Cleansing", Value = "2", Note = "basic" }];
        TraitList testHonorList = [new() { Name = "Admirable" }];
        TraitList testGloryList = [new() { Name = "Brash" }];
        TraitList testWisdomList = [new() { Name = "Pragmatic" }];
#pragma warning disable CS8604 // Possible null reference argument.
        var result = GrapevineLegacyXMLReader.LoadFera(xmlDoc.Root);
#pragma warning restore CS8604 // Possible null reference argument.
        Assert.NotNull(result);
        Assert.Equal("x", result.FeraName);
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

    [Fact(DisplayName = "Can Load Hunter Character Data")]
    public void CanLoadHunterCharacterData()
    {
        TraitList testEdges = [new() { Name = "Deviance: Impart", Value = "0", Note = "touched" }];
        var xmlDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <hunter creed="Avenger" camp="Idealist" handle="XYZ" conviction="3" mercy="2" vision="4" zeal="3"
              tempconviction="1" tempmercy="1" tempvision="1" tempzeal="1">
              <traitlist name="Edges" abc="no" atomic="yes" display="5">
                <trait name="Deviance: Impart" val="0" note="touched"/>
              </traitlist>
            </hunter>
            """);
#pragma warning disable CS8604 // Possible null reference argument.
        var result = GrapevineLegacyXMLReader.LoadHunter(xmlDoc.Root);
#pragma warning restore CS8604 // Possible null reference argument.
        Assert.NotNull(result);
        Assert.Equal("Avenger", result.Creed);
        Assert.Equal("Idealist", result.Camp);
        Assert.Equal("XYZ", result.Handle);
        Assert.Equal(3, result.Conviction);
        Assert.Equal(2, result.Mercy);
        Assert.Equal(4, result.Vision);
        Assert.Equal(3, result.Zeal);
        Assert.Equal(1, result.TempConviction);
        Assert.Equal(1, result.TempMercy);
        Assert.Equal(1, result.TempVision);
        Assert.Equal(1, result.TempZeal);
        Assert.Equivalent(testEdges, result.Edges);
    }

    [Fact(DisplayName = "Can Load KueiJin Character Data")]
    public void CanLoadKueiJinCharacterData()
    {
        var xmlDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <kueijin dharma="Song of the Shadow" balance="Yang" direction="Center" station="Arhat" poarchetype="Monkey"
              hun="2" po="3" yinchi="2" yangchi="2" demonchi="2" dharmatraits="3"
              temphun="0" temppo="0" tempyinchi="0" tempyangchi="0" tempdemonchi="0" tempdharmatraits="0">
              <traitlist name="Status" abc="yes" display="1">
                <trait name="Acknowledged"/>
              </traitlist>
              <traitlist name="Guanxi" abc="yes" display="1">
                <trait name="Vegeta"/>
              </traitlist>
              <traitlist name="Disciplines" abc="no" atomic="yes" display="5">
                <trait name="Black Wind: Level 1" val="3" note="basic"/>
              </traitlist>
              <traitlist name="Rites" abc="no" atomic="yes" display="5">
                <trait name="Devil-Tiger Li: Savage Joss" val="2" note="basic"/>
              </traitlist>
            </kueijin>
            """);
        TraitList testStatus = [new() { Name = "Acknowledged" }];
        TraitList testGuanxi = [new() { Name = "Vegeta" }];
        TraitList testDisciplines = [new() { Name = "Black Wind: Level 1", Value = "3", Note = "basic" }];
        TraitList testRites = [new() { Name = "Devil-Tiger Li: Savage Joss", Value = "2", Note = "basic" }];
#pragma warning disable CS8604 // Possible null reference argument.
        var result = GrapevineLegacyXMLReader.LoadKueiJin(xmlDoc.Root);
#pragma warning restore CS8604 // Possible null reference argument.
        Assert.NotNull(result);
        Assert.Equal("Song of the Shadow", result.Dharma);
        Assert.Equal("Yang", result.Balance);
        Assert.Equal("Center", result.Direction);
        Assert.Equal("Arhat", result.Station);
        Assert.Equal("Monkey", result.PoArchetype);
        Assert.Equal(2, result.Hun);
        Assert.Equal(3, result.Po);
        Assert.Equal(2, result.YinChi);
        Assert.Equal(2, result.YangChi);
        Assert.Equal(2, result.DemonChi);
        Assert.Equal(3, result.DharmaTraits);
        Assert.Equal(0, result.TempHun);
        Assert.Equal(0, result.TempPo);
        Assert.Equal(0, result.TempYinChi);
        Assert.Equal(0, result.TempYangChi);
        Assert.Equal(0, result.TempDemonChi);
        Assert.Equal(0, result.TempDharmaTraits);
        Assert.Equivalent(testStatus, result.KuejinStatus);
        Assert.Equivalent(testGuanxi, result.Guanxi);
        Assert.Equivalent(testDisciplines, result.Disciplines);
        Assert.Equivalent(testRites, result.Rites);
    }

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

    [Fact(DisplayName = "Can Load Mummy Character Data")]
    public void CanLoadMummyCharacterData()
    {
        var xmlDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <mummy amenti="Kher-Minu" sekhem="2" balance="2" memory="3" integrity="2" joy="2" ba="3" ka="2"
              tempsekhem="0" tempbalance="0" tempmemory="0" tempintegrity="0" tempjoy="0" tempba="0" tempka="0">
              <traitlist name="Humanity" abc="yes" display="1">
                <trait name="Benevolent"/>
                <trait name="Charitable"/>
              </traitlist>
              <traitlist name="Status" abc="yes" display="1">
                <trait name="Divine"/>
              </traitlist>
              <traitlist name="Hekau" abc="no" atomic="yes" display="5">
                <trait name="Alchemy: First Basic Path" val="3"/>
              </traitlist>
              <traitlist name="Spells" abc="no" atomic="yes" display="5">
                <trait name="Alchemy: Analyze Material" note="basic"/>
              </traitlist>
              <traitlist name="Rituals" abc="no" atomic="yes" display="5">
                <trait name="Alchemy: Blood of the Snake" note="basic"/>
              </traitlist>
              <inheritance>
                <![CDATA[My inheritance]]>
              </inheritance>
            </mummy>
            """);
        TraitList testHumanity = [new() { Name = "Benevolent" }, new() { Name = "Charitable" }];
        TraitList testStatus = [new() { Name = "Divine" }];
        TraitList testHekau = [new() { Name = "Alchemy: First Basic Path", Value = "3" }];
        TraitList testSpells = [new() { Name = "Alchemy: Analyze Material", Note = "basic" }];
        TraitList testRituals = [new() { Name = "Alchemy: Blood of the Snake", Note = "basic" }];
#pragma warning disable CS8604 // Possible null reference argument.
        var result = GrapevineLegacyXMLReader.LoadMummy(xmlDoc.Root);
#pragma warning restore CS8604 // Possible null reference argument.
        Assert.NotNull(result);
        Assert.Equal("Kher-Minu", result.Amenti);
        Assert.Equal(2, result.Sekhem);
        Assert.Equal(2, result.Balance);
        Assert.Equal(3, result.Memory);
        Assert.Equal(2, result.Integrity);
        Assert.Equal(2, result.Joy);
        Assert.Equal(3, result.Ba);
        Assert.Equal(2, result.Ka);
        Assert.Equal(0, result.TempSekhem);
        Assert.Equal(0, result.TempBalance);
        Assert.Equal(0, result.TempMemory);
        Assert.Equal(0, result.TempIntegrity);
        Assert.Equal(0, result.TempJoy);
        Assert.Equal(0, result.TempBa);
        Assert.Equal(0, result.TempKa);
        Assert.Equal("My inheritance", result.Inheritance);
        Assert.Equivalent(testHumanity, result.Humanity);
        Assert.Equivalent(testStatus, result.MummyStatus);
        Assert.Equivalent(testHekau, result.Hekau);
        Assert.Equivalent(testSpells, result.Spells);
        Assert.Equivalent(testRituals, result.Rituals);
    }

    [Fact(DisplayName = "Can Load Vampire Character Data")]
    public void CanLoadVampireCharacterData()
    {
        TraitList testStatus = [new() { Name = "Acknowleged" }, new() { Name = "Overrated" }];
        TraitList testDisciplines = [new() { Name = "Auspex: Heightened Senses", Value = "3", Note = "basic" }, new() { Name = "Dominate: Command", Value = "3", Note = "basic" }];
        TraitList testRituals = [new() { Name = "Basic: Blood Mead", Value = "2" }];
        TraitList testBonds = [new() { Name = "L. Flint", Value = "2" }];
        var xmlDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <vampire sire="Mr. Popo" coterie="The Cool Klub" clan="Foo" sect="Cami" generation="13" blood="10" 
                conscience="1" selfcontrol="2" courage="3" path="Potato" pathtraits="5"
                tempblood="4" tempconscience="0" tempselfcontrol="0" tempcourage="0" temppathtraits="3">
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
        var result = GrapevineLegacyXMLReader.LoadVampire(xmlDoc.Root);
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
        Assert.Equal(4, result.TempBlood);
        Assert.Equal(0, result.TempConscience);
        Assert.Equal(0, result.TempSelfControl);
        Assert.Equal(0, result.TempCourage);
        Assert.Equal(3, result.TempPathTraits);
        Assert.Equal(13, result.Generation);
        Assert.Equivalent(testStatus, result.KindredStatus);
        Assert.Equivalent(testDisciplines, result.Disciplines);
        Assert.Equivalent(testRituals, result.Rituals);
        Assert.Equivalent(testBonds, result.Bonds);
    }

    [Fact(DisplayName = "Can load boons for a vampire character")]
    public void CanLoadVampireBoons()
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
        var xmlDocument = XDocument.Parse("""
            <?xml version="1.0"?>
            <vampire name="Vladymur" startdate="1/1/2020" lastmodified="1/2/2020 00:00:01 AM">
                <boon type="foo" partner="Stu Padasso" owed="yes" date="1/1/2020"/>
                <boon type="bar" partner="Santa Claus" owed="no" date="1/1/2020"/>
            </vampire>
            """);
#pragma warning disable CS8604 // Possible null reference argument.
        var result = GrapevineLegacyXMLReader.LoadVampire(xmlDocument.Root);
#pragma warning restore CS8604 // Possible null reference argument.
        Assert.NotNull(result);
        Assert.Equivalent(expected, result);
    }

    [Fact(DisplayName = "Can Load Various Character Data")]
    public void CanLoadVariousCharacterData()
    {
        var xmlDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <various class="Bygone" subclass="Grey Man" affinity="City" plane="Earth" brood="Heron">
              <traitlist name="Tempers" abc="yes" display="2">
                <trait name="Essence"/>
                <trait name="Gnosis"/>
                <trait name="Rage"/>
                <trait name="Willpower"/>
              </traitlist>
              <traitlist name="Powers" abc="no" atomic="yes" display="5">
                <trait name="Fomori Powers: Armor"/>
              </traitlist>
              <other>
                <![CDATA[Other stuff]]>
              </other>
            </various>
            """);
        TraitList testTempers = [new() { Name = "Essence" }, new() { Name = "Gnosis" }, new() { Name = "Rage" }, new() { Name = "Willpower" }];
        TraitList testPowers = [new() { Name = "Fomori Powers: Armor" }];
#pragma warning disable CS8604 // Possible null reference argument.
        var result = GrapevineLegacyXMLReader.LoadVarious(xmlDoc.Root);
#pragma warning restore CS8604 // Possible null reference argument.
        Assert.NotNull(result);
        Assert.Equal("Bygone", result.Class);
        Assert.Equal("Grey Man", result.Subclass);
        Assert.Equal("City", result.Affinity);
        Assert.Equal("Earth", result.Plane);
        Assert.Equal("Heron", result.Brood);
        Assert.Equal("Other stuff", result.Other);
        Assert.Equivalent(testTempers, result.Tempers);
        Assert.Equivalent(testPowers, result.Powers);
    }

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

    [Fact(DisplayName = "Can Load Wraith Character Data")]
    public void CanLoadWraithCharacterData()
    {
        var xmlDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <wraith ethnos="Risen" guild="Spook" faction="Renegade" legion="The Emerald Legion" rank="Centurion"
              pathos="2" corpus="2" shadowarchetype="Anarchist" shadowplayer="That Guy" angst="3"
              temppathos="0" tempcorpus="0" tempangst="0">
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
        var result = GrapevineLegacyXMLReader.LoadWraith(xmlDoc.Root);
#pragma warning restore CS8604 // Possible null reference argument.
        Assert.NotNull(result);
        Assert.Equal("Risen", result.Ethnos);
        Assert.Equal("Spook", result.Guild);
        Assert.Equal("Renegade", result.Faction);
        Assert.Equal("The Emerald Legion", result.Legion);
        Assert.Equal("Centurion", result.Rank);
        Assert.Equal(2, result.Pathos);
        Assert.Equal(2, result.Corpus);
        Assert.Equal(3, result.Angst);
        Assert.Equal(0, result.TempPathos);
        Assert.Equal(0, result.TempCorpus);
        Assert.Equal(0, result.TempAngst);
        Assert.Equal("Anarchist", result.ShadowArchetype);
        Assert.Equal("That Guy", result.ShadowPlayer);
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
