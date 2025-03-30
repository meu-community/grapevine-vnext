using MEU.GV4.Data.Models;
using MEU.GV4.Data.Helpers;
using System.Xml.Linq;
using MEU.GV4.Data.Models.METClassic;

namespace MEU.GV4.Data.Providers
{
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
                Characters = LoadCharacters(root)
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

        internal static List<Character> LoadCharacters(XElement root)
        {
            var types = Enum.GetNames(typeof(CharacterType));
            // Element names in grapevine xml files are stored in lower case
            types = Array.ConvertAll(types, s => s.ToLower());
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
                }
            }
            return characters;
        }

        internal static Vampire LoadVampire(XElement el)
        {
            var vampire = new Vampire();
            LoadCommonTraits(vampire, el);
            vampire.Clan = XmlHelper.GetAttribute(el, "clan");
            vampire.Sect = XmlHelper.GetAttribute(el, "sect");
            vampire.Coterie = XmlHelper.GetAttribute(el, "coterie");
            vampire.Sire = XmlHelper.GetAttribute(el, "sire");
            vampire.Generation = XmlHelper.GetAttributeAsInt(el, "generation");
            vampire.Path = XmlHelper.GetAttribute(el, "path");
            vampire.PathTraits = XmlHelper.GetAttributeAsInt(el, "pathtraits");
            vampire.Conscience = XmlHelper.GetAttributeAsInt(el, "conscience");
            vampire.SelfControl = XmlHelper.GetAttributeAsInt(el, "selfcontrol");
            vampire.Courage = XmlHelper.GetAttributeAsInt(el, "courage");
            vampire.Blood = XmlHelper.GetAttributeAsInt(el, "blood");
            vampire.Disciplines = LoadTraitList(el, "Disciplines");
            vampire.Rituals = LoadTraitList(el, "Rituals");
            vampire.KindredStatus = LoadTraitList(el, "Status");
            vampire.Bonds = LoadTraitList(el, "Bonds");
            vampire.Boons = LoadBoons(el);
            return vampire;
        }

        internal static Werewolf LoadWerewolf(XElement el)
        {
            var werewolf = new Werewolf();
            LoadCommonTraits(werewolf, el);
            werewolf.Tribe = XmlHelper.GetAttribute(el, "tribe");
            werewolf.Breed = XmlHelper.GetAttribute(el, "breed");
            werewolf.Auspice = XmlHelper.GetAttribute(el, "auspice");
            werewolf.Rank = XmlHelper.GetAttribute(el, "rank");
            werewolf.Pack = XmlHelper.GetAttribute(el, "pack");
            werewolf.Totem = XmlHelper.GetAttribute(el, "totem");
            werewolf.Camp = XmlHelper.GetAttribute(el, "camp");
            werewolf.Position = XmlHelper.GetAttribute(el, "position");
            werewolf.Notoriety = XmlHelper.GetAttributeAsInt(el, "notoriety");
            werewolf.Rage = XmlHelper.GetAttributeAsInt(el, "rage");
            werewolf.Gnosis = XmlHelper.GetAttributeAsInt(el, "gnosis");
            werewolf.Honor = XmlHelper.GetAttributeAsInt(el, "honor");
            werewolf.Glory = XmlHelper.GetAttributeAsInt(el, "glory");
            werewolf.Wisdom = XmlHelper.GetAttributeAsInt(el, "wisdom");
            werewolf.Features = LoadTraitList(el, "Features");
            werewolf.Gifts = LoadTraitList(el, "Gifts");
            werewolf.Rites = LoadTraitList(el, "Rites");
            werewolf.HonorList = LoadTraitList(el, "Honor");
            werewolf.GloryList = LoadTraitList(el, "Glory");
            werewolf.WisdomList = LoadTraitList(el, "Wisdom");
            return werewolf;
        }

        internal static Mortal LoadMortal(XElement el)
        {
            var mortal = new Mortal();
            LoadCommonTraits(mortal, el);
            mortal.Motivation = XmlHelper.GetAttribute(el, "motivation");
            mortal.Association = XmlHelper.GetAttribute(el, "association");
            mortal.Regnant = XmlHelper.GetAttribute(el, "regnant");
            mortal.Humanity = XmlHelper.GetAttributeAsInt(el, "humanity");
            mortal.Blood = XmlHelper.GetAttributeAsInt(el, "blood");
            mortal.Conscience = XmlHelper.GetAttributeAsInt(el, "conscience");
            mortal.SelfControl = XmlHelper.GetAttributeAsInt(el, "selfcontrol");
            mortal.Courage = XmlHelper.GetAttributeAsInt(el, "courage");
            mortal.TrueFaith = XmlHelper.GetAttributeAsInt(el, "truefaith");
            mortal.HumanityList = LoadTraitList(el, "Humanity");
            mortal.NuminaList = LoadTraitList(el, "Numina");
            return mortal;
        }

        internal static Hunter LoadHunter(XElement el)
        {
            var hunter = new Hunter();
            LoadCommonTraits(hunter, el);
            hunter.Creed = XmlHelper.GetAttribute(el, "creed");
            hunter.Camp = XmlHelper.GetAttribute(el, "camp");
            hunter.Handle = XmlHelper.GetAttribute(el, "handle");
            hunter.Conviction = XmlHelper.GetAttributeAsInt(el, "conviction");
            hunter.Mercy = XmlHelper.GetAttributeAsInt(el, "mercy");
            hunter.Vision = XmlHelper.GetAttributeAsInt(el, "vision");
            hunter.Zeal = XmlHelper.GetAttributeAsInt(el, "zeal");
            hunter.Edges = LoadTraitList(el, "Edges");
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
                Oaths = XmlHelper.GetCData(el, "oaths"),
                ChangelingStatus = LoadTraitList(el, "Status"),
                Arts = LoadTraitList(el, "Arts"),
                Realms = LoadTraitList(el, "Realms")
            };
            LoadCommonTraits(changeling, el);
            return changeling;
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
}
