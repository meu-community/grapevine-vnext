using MEU.GV4.Data.Models;
using MEU.GV4.Data.Helpers;
using System.Xml;

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
                Players = GetPlayers(root)
            };

            return gameData;
        }

        private static List<Player> GetPlayers(XmlElement root)
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
                    // When importing, there is no create date so we will set it the same as the modify date
                    CreateDate = XmlHelper.GetAttributeAsDateTimeOffset(el, "lastmodified"),
                    ModifyDate = XmlHelper.GetAttributeAsDateTimeOffset(el, "lastmodified")
                });
            }

            return playerList;
        }
    }
}
