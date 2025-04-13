using System.Xml.Linq;

namespace MEU.GV4.Base.Helpers;

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

    public static DateOnly? GetAttributeAsDateOnly(XElement? element, string attributeName)
    {
        var attributeValue = GetAttribute(element, attributeName);
        DateOnly returnValue;
        if (DateOnly.TryParse(attributeValue, out returnValue))
        {
            return returnValue;
        }
        return null;
    }

    public static TimeOnly? GetAttributeAsTimeOnly(XElement? element, string attributeName)
    {
        var attributeValue = GetAttribute(element, attributeName);
        TimeOnly returnValue;
        if (TimeOnly.TryParse(attributeValue, out returnValue))
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
        if (cdataValue != string.Empty)
        {
            return cdataValue;
        }
        return null;
    }
}
