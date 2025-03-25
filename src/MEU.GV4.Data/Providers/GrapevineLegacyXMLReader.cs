﻿using MEU.GV4.Data.Models;
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

        private static List<Player> LoadPlayers(XmlElement root)
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

        private static List<Character> LoadCharacters(XmlElement root)
        {
            var characters = new List<Character>();
            foreach (XmlElement el in root.GetElementsByTagName("vampire"))
            {
                characters.Add(LoadVampire(el));
            }
            return characters;
        }

        private static Vampire LoadVampire(XmlElement el)
        {
            var vampire = new Vampire();
            LoadCommonTraits(vampire, el);

            return vampire;
        }

        private static void LoadCommonTraits(METCharacter character, XmlElement el)
        {
            character.Name = XmlHelper.GetAttribute(el, "name");
            character.Player = XmlHelper.GetAttribute(el, "player");
            character.Nature = XmlHelper.GetAttribute(el, "nature");
            character.Demeanor = XmlHelper.GetAttribute(el, "demeanor");
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
        }

        private static TraitList LoadTraitList(XmlElement el, string traitListName)
        {
            var traitList = new TraitList();
            var traits = el.SelectSingleNode($"traitlist[@name={traitListName}") as XmlElement;
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
        private static Experience? LoadExperience(XmlElement element)
        {
            var expElement = element.SelectSingleNode("experience") as XmlElement;
            if (expElement != null)
            {
                var experience = new Experience()
                {
                    Earned = XmlHelper.GetAttributeAsDecimal(expElement, "earned"),
                    Unspent = XmlHelper.GetAttributeAsDecimal(expElement, "unspent"),
                    Entries = []
                };

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

                return experience;
            }

            return null;
        }
    }
}
