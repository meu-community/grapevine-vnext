using System.Xml.Linq;

namespace MEU.GV4.Data.Helpers
{
    public class XmlHelper
    {
        public static string? GetAttribute(XElement? element, string attributeName)
        {
            return element?.Attribute(attributeName)?.Value;
        }

        public static DateTimeOffset? GetAttributeAsDateTimeOffset(XElement? element, string attributeName)
        {
            var attributeValue = GetAttribute(element, attributeName);
            DateTimeOffset returnValue;
            if (DateTimeOffset.TryParse(attributeValue, out returnValue))
            {
                return returnValue;
            }
            return null;
        }

        public static decimal GetAttributeAsDecimal(XElement? element, string attributeName)
        {
            var attributeValue = GetAttribute(element, attributeName);
            decimal returnValue = 0;
            decimal.TryParse(attributeValue, out returnValue);
            return returnValue;
        }

        public static int? GetAttributeAsInt(XElement? element, string attributeName)
        {
            int returnValue;
            var attributeValue = GetAttribute(element, attributeName);
            return int.TryParse(attributeValue, out returnValue) ? returnValue : null;
        }

        public static string? GetCData(XElement? element, string elementName)
        {
            var cdataValue = element?.Element(elementName)?.Value;
            if (cdataValue != String.Empty)
            {
                return cdataValue;
            }
            return null;
        }
    }
}
