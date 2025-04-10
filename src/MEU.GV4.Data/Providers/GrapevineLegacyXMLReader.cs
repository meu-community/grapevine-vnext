using MEU.GV4.Data.Models;
using MEU.GV4.Data.Helpers;
using System.Xml.Linq;
using MEU.GV4.Data.Models.METClassic;

namespace MEU.GV4.Data.Providers;

/// <summary>
/// Reads the XML contents of a legacy Grapevine file into GV4 data.
/// </summary>
public class GrapevineLegacyXMLReader
{
    private const string FILE_CONTENTS_EMPTY = "File contents are empty";
    private const string INVALID_GRAPEVINE_FILE = "Invalid grapevine file";


    public Game ReadData(string rawGameData)
    {
        if (String.IsNullOrEmpty(rawGameData))
        {
            throw new GrapevineProviderException(FILE_CONTENTS_EMPTY);
        }
        var xmlDoc = XDocument.Parse(rawGameData);

        var root = xmlDoc.Root;

        if (root?.Name != "grapevine")
        {
            throw new GrapevineProviderException(INVALID_GRAPEVINE_FILE);
        }

        var gameData = new Game()
        {
            Title = XmlHelper.GetAttribute(root, "chronicle"),
            Website = XmlHelper.GetAttribute(root, "website"),
            EMail = XmlHelper.GetAttribute(root, "email"),
            Phone = XmlHelper.GetAttribute(root, "phone"),
            UsualTime = XmlHelper.GetAttribute(root, "usualtime"),
            UsualPlace = XmlHelper.GetCData(root, "usualplace"),
            Description = XmlHelper.GetCData(root, "description"),
            Players = LoadPlayers(root),
            Characters = LoadCharacters(root),
            Calendar = LoadCalendar(root)
        };

        return gameData;
    }

    internal static List<Player> LoadPlayers(XElement root)
    {
        var playerList = new List<Player>();
        foreach (var el in root.Elements("player"))
        {
            playerList.Add(new()
            {
                Name = XmlHelper.GetAttribute(el, "name"),
                ID = XmlHelper.GetAttribute(el, "id"),
                EMail = XmlHelper.GetAttribute(el, "email"),
                Phone = XmlHelper.GetAttribute(el, "phone"),
                Position = XmlHelper.GetAttribute(el, "position"),
                Status = XmlHelper.GetAttribute(el, "status"),
                Address = XmlHelper.GetCData(el, "address"),
                Notes = XmlHelper.GetCData(el, "notes"),
                PlayerExperience = LoadExperience(el),
                // When importing, there is no create date so we will set it the same as the modify date
                CreateDate = XmlHelper.GetAttributeAsDateTimeOffset(el, "lastmodified"),
                ModifyDate = XmlHelper.GetAttributeAsDateTimeOffset(el, "lastmodified")
            });
        }

        return playerList;
    }

    internal static List<CalendarEntry> LoadCalendar(XElement root)
    {
        var calendarList = new List<CalendarEntry>();
        var calendarElement = root?.Element("calendar");
        if (calendarElement != null)
        {
            foreach (var el in calendarElement.Elements("game"))
            {
                calendarList.Add(new()
                {
                    GameDate = XmlHelper.GetAttributeAsDateOnly(el, "date"),
                    GameTime = XmlHelper.GetAttributeAsTimeOnly(el, "time"),
                    Place = XmlHelper.GetCData(el, "place"),
                    Notes = XmlHelper.GetCData(el, "notes")
                });
            }
        }
        return calendarList;
    }

    internal static string[] GetSupportedTypes()
    {
        var types = Enum.GetNames(typeof(CharacterType));
        // Element names in grapevine xml files are stored in lower case
        return Array.ConvertAll(types, s => s.ToLower());
    }

    internal static List<Character> LoadCharacters(XElement root)
    {
        var types = GetSupportedTypes();
        var characters = new List<Character>();
        var selectedElements = root
            .Elements()
            .Where(e => types.Contains(e.Name.LocalName));

        foreach (var el in selectedElements)
        {
            switch (el.Name.LocalName)
            {
                case "vampire":
                    characters.Add(LoadVampire(el));
                    break;
                case "werewolf":
                    characters.Add(LoadWerewolf(el));
                    break;
                case "mortal":
                    characters.Add(LoadMortal(el));
                    break;
                case "hunter":
                    characters.Add(LoadHunter(el));
                    break;
                case "wraith":
                    characters.Add(LoadWraith(el));
                    break;
                case "changeling":
                    characters.Add(LoadChangeling(el));
                    break;
                case "mummy":
                    characters.Add(LoadMummy(el));
                    break;
                case "kueijin":
                    characters.Add(LoadKueiJin(el));
                    break;
                case "mage":
                    characters.Add(LoadMage(el));
                    break;
                case "demon":
                    characters.Add(LoadDemon(el));
                    break;
                case "various":
                    characters.Add(LoadVarious(el));
                    break;
                case "fera":
                    characters.Add(LoadFera(el));
                    break;
            }
        }
        return characters;
    }

    internal static Vampire LoadVampire(XElement el)
    {
        var vampire = new Vampire()
        {
            Clan = XmlHelper.GetAttribute(el, "clan"),
            Sect = XmlHelper.GetAttribute(el, "sect"),
            Coterie = XmlHelper.GetAttribute(el, "coterie"),
            Sire = XmlHelper.GetAttribute(el, "sire"),
            Generation = XmlHelper.GetAttributeAsInt(el, "generation"),
            Path = XmlHelper.GetAttribute(el, "path"),
            PathTraits = XmlHelper.GetAttributeAsInt(el, "pathtraits"),
            Conscience = XmlHelper.GetAttributeAsInt(el, "conscience"),
            SelfControl = XmlHelper.GetAttributeAsInt(el, "selfcontrol"),
            Courage = XmlHelper.GetAttributeAsInt(el, "courage"),
            Blood = XmlHelper.GetAttributeAsInt(el, "blood"),
            TempBlood = XmlHelper.GetAttributeAsInt(el,"tempblood"),
            TempConscience = XmlHelper.GetAttributeAsInt(el, "tempconscience"),
            TempCourage = XmlHelper.GetAttributeAsInt(el, "tempcourage"),
            TempSelfControl = XmlHelper.GetAttributeAsInt(el, "tempselfcontrol"),
            TempPathTraits = XmlHelper.GetAttributeAsInt(el, "temppathtraits"),
            Disciplines = LoadTraitList(el, "Disciplines"),
            Rituals = LoadTraitList(el, "Rituals"),
            KindredStatus = LoadTraitList(el, "Status"),
            Bonds = LoadTraitList(el, "Bonds"),
            Boons = LoadBoons(el)
        };
        LoadCommonTraits(vampire, el);
        return vampire;
    }

    internal static Werewolf LoadWerewolf(XElement el)
    {
        var werewolf = new Werewolf()
        {
            Tribe = XmlHelper.GetAttribute(el, "tribe"),
            Breed = XmlHelper.GetAttribute(el, "breed"),
            Auspice = XmlHelper.GetAttribute(el, "auspice"),
            Rank = XmlHelper.GetAttribute(el, "rank"),
            Pack = XmlHelper.GetAttribute(el, "pack"),
            Totem = XmlHelper.GetAttribute(el, "totem"),
            Camp = XmlHelper.GetAttribute(el, "camp"),
            Position = XmlHelper.GetAttribute(el, "position"),
            Notoriety = XmlHelper.GetAttributeAsInt(el, "notoriety"),
            Rage = XmlHelper.GetAttributeAsInt(el, "rage"),
            Gnosis = XmlHelper.GetAttributeAsInt(el, "gnosis"),
            Honor = XmlHelper.GetAttributeAsInt(el, "honor"),
            Glory = XmlHelper.GetAttributeAsInt(el, "glory"),
            Wisdom = XmlHelper.GetAttributeAsInt(el, "wisdom"),
            TempGlory = XmlHelper.GetAttributeAsInt(el, "tempglory"),
            TempGnosis = XmlHelper.GetAttributeAsInt(el, "tempgnosis"),
            TempHonor = XmlHelper.GetAttributeAsInt(el, "temphonor"),
            TempRage = XmlHelper.GetAttributeAsInt(el, "temprage"),
            TempWisdom = XmlHelper.GetAttributeAsInt(el, "tempwisdom"),
            Features = LoadTraitList(el, "Features"),
            Gifts = LoadTraitList(el, "Gifts"),
            Rites = LoadTraitList(el, "Rites"),
            HonorList = LoadTraitList(el, "Honor"),
            GloryList = LoadTraitList(el, "Glory"),
            WisdomList = LoadTraitList(el, "Wisdom")
        };
        LoadCommonTraits(werewolf, el);
        return werewolf;
    }

    internal static Mortal LoadMortal(XElement el)
    {
        var mortal = new Mortal()
        {
            Motivation = XmlHelper.GetAttribute(el, "motivation"),
            Association = XmlHelper.GetAttribute(el, "association"),
            Regnant = XmlHelper.GetAttribute(el, "regnant"),
            Humanity = XmlHelper.GetAttributeAsInt(el, "humanity"),
            Blood = XmlHelper.GetAttributeAsInt(el, "blood"),
            Conscience = XmlHelper.GetAttributeAsInt(el, "conscience"),
            SelfControl = XmlHelper.GetAttributeAsInt(el, "selfcontrol"),
            Courage = XmlHelper.GetAttributeAsInt(el, "courage"),
            TrueFaith = XmlHelper.GetAttributeAsInt(el, "truefaith"),
            TempBlood = XmlHelper.GetAttributeAsInt(el, "tempblood"),
            TempConscience = XmlHelper.GetAttributeAsInt(el, "tempconscience"),
            TempSelfControl = XmlHelper.GetAttributeAsInt(el,"tempselfcontrol"),
            TempHumanity = XmlHelper.GetAttributeAsInt(el,"temphumanity"),
            TempTrueFaith = XmlHelper.GetAttributeAsInt(el,"temptruefaith"),
            TempCourage = XmlHelper.GetAttributeAsInt(el, "tempcourage"),
            HumanityList = LoadTraitList(el, "Humanity"),
            NuminaList = LoadTraitList(el, "Numina")
        };
        LoadCommonTraits(mortal, el);
        return mortal;
    }

    internal static Hunter LoadHunter(XElement el)
    {
        var hunter = new Hunter()
        {
            Creed = XmlHelper.GetAttribute(el, "creed"),
            Camp = XmlHelper.GetAttribute(el, "camp"),
            Handle = XmlHelper.GetAttribute(el, "handle"),
            Conviction = XmlHelper.GetAttributeAsInt(el, "conviction"),
            Mercy = XmlHelper.GetAttributeAsInt(el, "mercy"),
            Vision = XmlHelper.GetAttributeAsInt(el, "vision"),
            Zeal = XmlHelper.GetAttributeAsInt(el, "zeal"),
            TempConviction = XmlHelper.GetAttributeAsInt(el,"tempconviction"),
            TempMercy = XmlHelper.GetAttributeAsInt(el, "tempmercy"),
            TempVision = XmlHelper.GetAttributeAsInt(el, "tempvision"),
            TempZeal = XmlHelper.GetAttributeAsInt(el,"tempzeal"),
            Edges = LoadTraitList(el, "Edges")
        };
        LoadCommonTraits(hunter, el);
        return hunter;
    }

    internal static Wraith LoadWraith(XElement el)
    {
        var wraith = new Wraith()
        {
            Ethnos = XmlHelper.GetAttribute(el, "ethnos"),
            Guild = XmlHelper.GetAttribute(el, "guild"),
            Faction = XmlHelper.GetAttribute(el, "faction"),
            Legion = XmlHelper.GetAttribute(el, "legion"),
            Rank = XmlHelper.GetAttribute(el, "rank"),
            Pathos = XmlHelper.GetAttributeAsInt(el, "pathos"),
            Corpus = XmlHelper.GetAttributeAsInt(el, "corpus"),
            ShadowArchetype = XmlHelper.GetAttribute(el, "shadowarchetype"),
            ShadowPlayer = XmlHelper.GetAttribute(el, "shadowplayer"),
            Angst = XmlHelper.GetAttributeAsInt(el, "angst"),
            TempAngst = XmlHelper.GetAttributeAsInt(el,"tempangst"),
            TempCorpus = XmlHelper.GetAttributeAsInt(el,"tempcorpus"),
            TempPathos = XmlHelper.GetAttributeAsInt(el, "temppathos"),
            Passions = XmlHelper.GetCData(el, "passions"),
            Fetters = XmlHelper.GetCData(el, "fetters"),
            Life = XmlHelper.GetCData(el, "life"),
            Death = XmlHelper.GetCData(el, "death"),
            Haunt = XmlHelper.GetCData(el, "haunt"),
            Regret = XmlHelper.GetCData(el, "regret"),
            DarkPassions = XmlHelper.GetCData(el, "darkpassions"),
            Arcanoi = LoadTraitList(el, "Arcanoi"),
            WraithStatus = LoadTraitList(el, "Status"),
            ThornList = LoadTraitList(el,"Thorns")
        };
        LoadCommonTraits(wraith, el);
        return wraith;
    }

    internal static Changeling LoadChangeling(XElement el)
    {
        var changeling = new Changeling()
        {
            SeelieLegacy = XmlHelper.GetAttribute(el, "seelie"),
            UnseelieLegacy = XmlHelper.GetAttribute(el, "unseelie"),
            Court = XmlHelper.GetAttribute(el, "court"),
            Kith = XmlHelper.GetAttribute(el, "kith"),
            Seeming = XmlHelper.GetAttribute(el, "seeming"),
            House = XmlHelper.GetAttribute(el, "house"),
            Threshold = XmlHelper.GetAttribute(el, "threshold"),
            Glamour = XmlHelper.GetAttributeAsInt(el, "glamour"),
            Banality = XmlHelper.GetAttributeAsInt(el, "banality"),
            TempGlamour = XmlHelper.GetAttributeAsInt(el,"tempglamour"),
            TempBanality = XmlHelper.GetAttributeAsInt(el, "tempbanality"),
            Oaths = XmlHelper.GetCData(el, "oaths"),
            ChangelingStatus = LoadTraitList(el, "Status"),
            Arts = LoadTraitList(el, "Arts"),
            Realms = LoadTraitList(el, "Realms")
        };
        LoadCommonTraits(changeling, el);
        return changeling;
    }
    internal static Mummy LoadMummy(XElement el)
    {
        var mummy = new Mummy()
        {
            Amenti = XmlHelper.GetAttribute(el, "amenti"),
            Sekhem = XmlHelper.GetAttributeAsInt(el, "sekhem"),
            Balance = XmlHelper.GetAttributeAsInt(el, "balance"),
            Memory = XmlHelper.GetAttributeAsInt(el, "memory"),
            Integrity = XmlHelper.GetAttributeAsInt(el, "integrity"),
            Joy = XmlHelper.GetAttributeAsInt(el, "joy"),
            Ba = XmlHelper.GetAttributeAsInt(el, "ba"),
            Ka = XmlHelper.GetAttributeAsInt(el, "ka"),
            TempSekhem = XmlHelper.GetAttributeAsInt(el, "tempsekhem"),
            TempBalance = XmlHelper.GetAttributeAsInt(el, "tempbalance"),
            TempMemory = XmlHelper.GetAttributeAsInt(el, "tempmemory"),
            TempIntegrity = XmlHelper.GetAttributeAsInt(el, "tempintegrity"),
            TempJoy = XmlHelper.GetAttributeAsInt(el, "tempjoy"),
            TempBa = XmlHelper.GetAttributeAsInt(el, "tempba"),
            TempKa = XmlHelper.GetAttributeAsInt(el, "tempka"),
            Inheritance = XmlHelper.GetCData(el, "inheritance"),
            Humanity = LoadTraitList(el, "Humanity"),
            MummyStatus = LoadTraitList(el, "Status"),
            Hekau = LoadTraitList(el, "Hekau"),
            Spells = LoadTraitList(el, "Spells"),
            Rituals = LoadTraitList(el, "Rituals")
        };
        LoadCommonTraits(mummy, el);
        return mummy;
    }

    internal static KueiJin LoadKueiJin(XElement el)
    {
        var kuejin = new KueiJin()
        {
            Dharma = XmlHelper.GetAttribute(el, "dharma"),
            Balance = XmlHelper.GetAttribute(el, "balance"),
            Direction = XmlHelper.GetAttribute(el, "direction"),
            Station = XmlHelper.GetAttribute(el, "station"),
            PoArchetype = XmlHelper.GetAttribute(el, "poarchetype"),
            Hun = XmlHelper.GetAttributeAsInt(el, "hun"),
            Po = XmlHelper.GetAttributeAsInt(el, "po"),
            YinChi = XmlHelper.GetAttributeAsInt(el, "yinchi"),
            YangChi = XmlHelper.GetAttributeAsInt(el, "yangchi"),
            DemonChi = XmlHelper.GetAttributeAsInt(el, "demonchi"),
            DharmaTraits = XmlHelper.GetAttributeAsInt(el, "dharmatraits"),
            TempHun = XmlHelper.GetAttributeAsInt(el, "temphun"),
            TempPo = XmlHelper.GetAttributeAsInt(el, "temppo"),
            TempYinChi = XmlHelper.GetAttributeAsInt(el, "tempyinchi"),
            TempYangChi = XmlHelper.GetAttributeAsInt(el, "tempyangchi"),
            TempDemonChi = XmlHelper.GetAttributeAsInt(el, "tempdemonchi"),
            TempDharmaTraits = XmlHelper.GetAttributeAsInt(el, "tempdharmatraits"),
            KuejinStatus = LoadTraitList(el, "Status"),
            Guanxi = LoadTraitList(el, "Guanxi"),
            Disciplines = LoadTraitList(el, "Disciplines"),
            Rites = LoadTraitList(el, "Rites")
        };
        LoadCommonTraits(kuejin, el);
        return kuejin;
    }

    internal static Mage LoadMage(XElement el)
    {
        var mage = new Mage()
        {
            Essence = XmlHelper.GetAttribute(el, "essence"),
            Tradition = XmlHelper.GetAttribute(el, "tradition"),
            Cabal = XmlHelper.GetAttribute(el, "cabal"),
            Rank = XmlHelper.GetAttribute(el, "rank"),
            Faction = XmlHelper.GetAttribute(el, "faction"),
            Arete = XmlHelper.GetAttributeAsInt(el, "arete"),
            Quintessence = XmlHelper.GetAttributeAsInt(el, "quintessence"),
            Paradox = XmlHelper.GetAttributeAsInt(el, "paradox"),
            TempArete = XmlHelper.GetAttributeAsInt(el, "temparete"),
            TempQuintessence =XmlHelper.GetAttributeAsInt(el,"tempquintessence"),
            TempParadox = XmlHelper.GetAttributeAsInt(el, "tempparadox"),
            Foci = XmlHelper.GetCData(el, "foci"),
            Resonance = LoadTraitList(el, "Resonance"),
            Reputation = LoadTraitList(el, "Reputation"),
            Spheres = LoadTraitList(el, "Spheres"),
            Rotes = LoadTraitList(el, "Rotes")
        };
        LoadCommonTraits(mage, el);
        return mage;
    }

    internal static Demon LoadDemon(XElement el)
    {
        var demon = new Demon()
        {
            House = XmlHelper.GetAttribute(el, "house"),
            Faction = XmlHelper.GetAttribute(el, "faction"),
            Torment = XmlHelper.GetAttributeAsInt(el, "torment"),
            Faith = XmlHelper.GetAttributeAsInt(el, "faith"),
            Conscience = XmlHelper.GetAttributeAsInt(el, "conscience"),
            Conviction = XmlHelper.GetAttributeAsInt(el, "conviction"),
            Courage = XmlHelper.GetAttributeAsInt(el, "courage"),
            TempFaith = XmlHelper.GetAttributeAsInt(el, "tempfaith"),
            TempConscience = XmlHelper.GetAttributeAsInt(el, "tempconscience"),
            TempConviction = XmlHelper.GetAttributeAsInt(el, "tempconviction"),
            TempCourage = XmlHelper.GetAttributeAsInt(el, "tempcourage"),
            Lores = LoadTraitList(el, "Lores"),
            Visage = LoadTraitList(el, "Apocalyptic Form")
        };
        LoadCommonTraits(demon, el);
        return demon;
    }

    internal static Fera LoadFera(XElement el)
    {
        var fera = new Fera()
        {
            FeraName = XmlHelper.GetAttribute(el, "fera"),
            Breed = XmlHelper.GetAttribute(el, "breed"),
            Auspice = XmlHelper.GetAttribute(el, "auspice"),
            Rank = XmlHelper.GetAttribute(el, "rank"),
            Pack = XmlHelper.GetAttribute(el, "pack"),
            Totem = XmlHelper.GetAttribute(el, "totem"),
            Camp = XmlHelper.GetAttribute(el, "camp"),
            Position = XmlHelper.GetAttribute(el, "position"),
            Notoriety = XmlHelper.GetAttributeAsInt(el, "notoriety"),
            Rage = XmlHelper.GetAttributeAsInt(el, "rage"),
            Gnosis = XmlHelper.GetAttributeAsInt(el, "gnosis"),
            Honor = XmlHelper.GetAttributeAsInt(el, "honor"),
            Glory = XmlHelper.GetAttributeAsInt(el, "glory"),
            Wisdom = XmlHelper.GetAttributeAsInt(el, "wisdom"),
            TempGlory = XmlHelper.GetAttributeAsInt(el, "tempglory"),
            TempGnosis = XmlHelper.GetAttributeAsInt(el, "tempgnosis"),
            TempHonor = XmlHelper.GetAttributeAsInt(el, "temphonor"),
            TempRage = XmlHelper.GetAttributeAsInt(el, "temprage"),
            TempWisdom = XmlHelper.GetAttributeAsInt(el, "tempwisdom"),
            Features = LoadTraitList(el, "Features"),
            Gifts = LoadTraitList(el, "Gifts"),
            Rites = LoadTraitList(el, "Rites"),
            HonorList = LoadTraitList(el, "Honor"),
            GloryList = LoadTraitList(el, "Glory"),
            WisdomList = LoadTraitList(el, "Wisdom")
        };
        LoadCommonTraits(fera, el);
        return fera;
    }

    internal static Various LoadVarious(XElement el)
    {
        var various = new Various()
        {
            Class = XmlHelper.GetAttribute(el, "class"),
            Subclass = XmlHelper.GetAttribute(el, "subclass"),
            Affinity = XmlHelper.GetAttribute(el,"affinity"),
            Plane = XmlHelper.GetAttribute(el, "plane"),
            Brood = XmlHelper.GetAttribute(el, "brood"),
            Other = XmlHelper.GetCData(el, "other"),
            Tempers = LoadTraitList(el, "Tempers"),
            Powers = LoadTraitList(el, "Powers")
        };
        LoadCommonTraits(various, el);
        return various;
    }

    internal static List<Boon> LoadBoons(XElement el)
    {
        var boons = new List<Boon>();
        foreach (var boon in el.Elements("boon"))
        {
            boons.Add(new()
            {
                Type = XmlHelper.GetAttribute(boon, "type"),
                Partner = XmlHelper.GetAttribute(boon, "partner"),
                Owed = XmlHelper.GetAttribute(boon, "owed") == "yes",
                CreateDate = XmlHelper.GetAttributeAsDateTimeOffset(boon, "date")
            });
        }
        return boons;
    }

    internal static void LoadCommonTraits(METCharacter character, XElement el)
    {
        character.Name = XmlHelper.GetAttribute(el, "name");
        character.ID = XmlHelper.GetAttribute(el, "id");
        character.Player = XmlHelper.GetAttribute(el, "player");
        character.IsNPC = XmlHelper.GetAttribute(el, "npc") == "yes";
        character.Title = XmlHelper.GetAttribute(el, "title");
        character.Nature = XmlHelper.GetAttribute(el, "nature");
        character.Demeanor = XmlHelper.GetAttribute(el, "demeanor");
        character.Willpower = XmlHelper.GetAttributeAsInt(el, "willpower");
        character.TempWillpower = XmlHelper.GetAttributeAsInt(el, "tempwillpower");
        character.Status = XmlHelper.GetAttribute(el, "status");
        character.PhysicalMax = XmlHelper.GetAttributeAsInt(el, "physicalmax");
        character.SocialMax = XmlHelper.GetAttributeAsInt(el, "socialmax");
        character.MentalMax = XmlHelper.GetAttributeAsInt(el, "mentalmax");
        character.CreateDate = XmlHelper.GetAttributeAsDateTimeOffset(el, "startdate");
        character.ModifyDate = XmlHelper.GetAttributeAsDateTimeOffset(el, "lastmodified");
        character.CharacterExperience = LoadExperience(el);
        character.PhysicalTraits = LoadTraitList(el, "Physical");
        character.SocialTraits = LoadTraitList(el, "Social");
        character.MentalTraits = LoadTraitList(el, "Mental");
        character.NegativePhysicalTraits = LoadTraitList(el, "Negative Physical");
        character.NegativeSocialTraits = LoadTraitList(el, "Negative Social");
        character.NegativeMentalTraits = LoadTraitList(el, "Negative Mental");
        character.Abilities = LoadTraitList(el, "Abilities");
        character.Influences = LoadTraitList(el, "Influences");
        character.Backgrounds = LoadTraitList(el, "Backgrounds");
        character.Merits = LoadTraitList(el, "Merits");
        character.Flaws = LoadTraitList(el, "Flaws");
        character.Health = LoadTraitList(el, "Health Levels");
        character.Equipment = LoadTraitList(el, "Equipment");
        character.Locations = LoadTraitList(el, "Locations");
        character.Miscellanious = LoadTraitList(el, "Miscellaneous");
        character.Derangements = LoadTraitList(el, "Derangements");
        character.Biography = XmlHelper.GetCData(el, "biography");
        character.Notes = XmlHelper.GetCData(el, "notes");
    }

    internal static TraitList LoadTraitList(XElement el, string traitListName)
    {
        var traitList = new TraitList();
        var traits = el.Elements()
            .Where(el => el.Name.LocalName == "traitlist" && el.Attribute("name")?.Value == traitListName);
        if (traits != null)
        {
            foreach (var trait in traits.Elements("trait"))
            {
                traitList.Add(new Trait()
                {
                    Name = XmlHelper.GetAttribute(trait, "name"),
                    Value = XmlHelper.GetAttribute(trait, "val"),
                    Note = XmlHelper.GetAttribute(trait, "note")
                });
            }
        }

        return traitList;
    }
    internal static Experience LoadExperience(XElement element)
    {
        var experience = new Experience();
        var expElement = element.Element("experience");
        if (expElement != null)
        {
            experience.Earned = XmlHelper.GetAttributeAsDecimal(expElement, "earned");
            experience.Unspent = XmlHelper.GetAttributeAsDecimal(expElement, "unspent");

            foreach (var entry in expElement.Elements("entry"))
            {
                experience.Entries.Add(new()
                {
                    EntryDate = XmlHelper.GetAttributeAsDateTimeOffset(entry, "date"),
                    Change = XmlHelper.GetAttributeAsDecimal(entry, "change"),
                    Type = Enum.Parse<ExperienceChangeType>(XmlHelper.GetAttribute(entry, "type")),
                    Reason = XmlHelper.GetAttribute(entry, "reason"),
                    Earned = XmlHelper.GetAttributeAsDecimal(entry, "earned"),
                    Unspent = XmlHelper.GetAttributeAsDecimal(entry, "unspent")
                });
            }
        }

        return experience;
    }
}
