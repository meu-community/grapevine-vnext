using MEU.GV4.Data.Models;
using MEU.GV4.Data.Helpers;
using System.Xml;
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
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(rawGameData);

            var root = xmlDoc.DocumentElement;

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

        internal static List<Player> LoadPlayers(XmlElement root)
        {
            var playerList = new List<Player>();
            foreach (XmlElement el in root.GetElementsByTagName("player"))
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

        internal static List<Character> LoadCharacters(XmlElement root)
        {
            var characters = new List<Character>();
            foreach (XmlElement el in root.GetElementsByTagName("vampire"))
            {
                characters.Add(LoadVampire(el));
            }
            return characters;
        }

        internal static Vampire LoadVampire(XmlElement el)
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

        internal static List<Boon> LoadBoons(XmlElement el)
        {
            var boons = new List<Boon>();
            foreach (XmlElement boon in el.GetElementsByTagName("boon"))
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

        internal static void LoadCommonTraits(METCharacter character, XmlElement el)
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

        internal static TraitList LoadTraitList(XmlElement el, string traitListName)
        {
            var traitList = new TraitList();
            var traits = el.SelectSingleNode($"traitlist[@name='{traitListName}']") as XmlElement;
            if (traits != null)
            {
                foreach (XmlElement trait in traits.GetElementsByTagName("trait"))
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
        internal static Experience LoadExperience(XmlElement element)
        {
            var experience = new Experience();
            var expElement = element.SelectSingleNode("experience") as XmlElement;
            if (expElement != null)
            {
                experience.Earned = XmlHelper.GetAttributeAsDecimal(expElement, "earned");
                experience.Unspent = XmlHelper.GetAttributeAsDecimal(expElement, "unspent");

                foreach (XmlElement entry in expElement.GetElementsByTagName("entry"))
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
