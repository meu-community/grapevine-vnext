using MEU.GV4.Data.Models;
using System.Xml;

namespace MEU.GV4.Data.Providers
{
    /// <summary>
    /// Reads the XML contents of a legacy Grapevine file into GV4 data.
    /// </summary>
    public class GrapevineLegacyXMLReader
    {
        public Game ReadData(string rawGameData)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(rawGameData);

            var gameData = new Game()
            {
                Title = xmlDoc?.DocumentElement?.Attributes?.GetNamedItem("chronicle")?.Value,
                Website = xmlDoc?.DocumentElement?.Attributes?.GetNamedItem("website")?.Value,
                EMail = xmlDoc?.DocumentElement?.Attributes?.GetNamedItem("email")?.Value,
                Phone = xmlDoc?.DocumentElement?.Attributes?.GetNamedItem("phone")?.Value,
                UsualTime = xmlDoc?.DocumentElement?.Attributes?.GetNamedItem("usualtime")?.Value,
                UsualPlace = xmlDoc?.DocumentElement?.SelectSingleNode("usualplace")?.FirstChild?.Value,
                Description = xmlDoc?.DocumentElement?.SelectSingleNode("description")?.FirstChild?.Value
            };

            return gameData;
        }
    }
}
