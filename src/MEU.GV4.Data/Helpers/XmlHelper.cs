using System.Xml;

namespace MEU.GV4.Data.Helpers
{
    public class XmlHelper
    {
        public static string? GetAttribute(XmlElement? element, string attributeName)
        {
            return element?.Attributes?.GetNamedItem(attributeName)?.Value;
        }

        public static string? GetCData(XmlElement? element, string elementName)
        {
            return element?.SelectSingleNode(elementName)?.FirstChild?.Value;
        }
    }
}
