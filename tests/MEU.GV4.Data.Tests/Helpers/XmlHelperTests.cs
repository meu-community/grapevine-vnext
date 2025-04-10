using MEU.GV4.Data.Helpers;
using System.Xml.Linq;

namespace MEU.GV4.Data.Tests.Helpers;

public class XmlHelperTests
{
    [Fact(DisplayName = "Can read attribute from an element")]
    public void CanReadAttributeFromElement()
    {
        var testDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <test attr1="foo">
            </test>
            """);
        Assert.Equal("foo", XmlHelper.GetAttribute(testDoc.Root, "attr1"));
    }

    [Fact(DisplayName = "Can read attribute as DateTimeOffset from an element")]
    public void CanReadAttributeAsDateTimeOffset()
    {
        var testDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <test attr1="1/1/2020">
            </test>
            """);
        Assert.Equal(DateTimeOffset.Parse("1/1/2020"), XmlHelper.GetAttributeAsDateTimeOffset(testDoc.Root, "attr1"));
    }

    [Fact(DisplayName = "Invalid Date returns as null")]
    public void InvalidDateReturnsAsNull()
    {
        var testDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <test attr1="foo">
            </test>
            """);
        Assert.Null(XmlHelper.GetAttributeAsDateTimeOffset(testDoc.Root, "attr1"));
    }

    [Fact(DisplayName = "Can read attribute as DateOnly from an element")]
    public void CanReadAttributeAsDateOnly()
    {
        var testDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <test attr1="1/1/2020">
            </test>
            """);
        Assert.Equal(DateOnly.Parse("1/1/2020"), XmlHelper.GetAttributeAsDateOnly(testDoc.Root, "attr1"));
    }

    [Fact(DisplayName = "Invalid DateOnly returns as null")]
    public void InvalidDateOnlyReturnsAsNull()
    {
        var testDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <test attr1="foo">
            </test>
            """);
        Assert.Null(XmlHelper.GetAttributeAsDateOnly(testDoc.Root, "attr1"));
    }

    [Fact(DisplayName = "Can read attribute as TimeOnly from an element")]
    public void CanReadAttributeAsTimeOnly()
    {
        var testDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <test attr1="4:00 PM">
            </test>
            """);
        Assert.Equal(TimeOnly.Parse("4:00 PM"), XmlHelper.GetAttributeAsTimeOnly(testDoc.Root, "attr1"));
    }

    [Fact(DisplayName = "Invalid TimeOnly returns as null")]
    public void InvalidTimeOnlyReturnsAsNull()
    {
        var testDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <test attr1="foo">
            </test>
            """);
        Assert.Null(XmlHelper.GetAttributeAsTimeOnly(testDoc.Root, "attr1"));
    }


    [Fact(DisplayName = "Can read attribute as decimal from an element")]
    public void CanReadAttributeAsDecimal()
    {
        var testDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <test attr1="3.14">
            </test>
            """);
        Assert.Equal(3.14M, XmlHelper.GetAttributeAsDecimal(testDoc.Root, "attr1"));
    }

    [Fact(DisplayName = "Invalid decimal returns 0")]
    public void InvalidDecimalReturnsZero()
    {
        var testDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <test attr1="foo">
            </test>
            """);
        Assert.Equal(0, XmlHelper.GetAttributeAsDecimal(testDoc.Root, "attr1"));
    }

    [Fact(DisplayName = "Can read attribute as int from an element")]
    public void CanReadAttributeAsInt()
    {
        var testDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <test attr1="42">
            </test>
            """);
        Assert.Equal(42, XmlHelper.GetAttributeAsInt(testDoc.Root, "attr1"));
    }

    [Fact(DisplayName = "Invalid int returns 0")]
    public void InvalidIntReturnsNull()
    {
        var testDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <test attr1="foo">
            </test>
            """);
        Assert.Null(XmlHelper.GetAttributeAsInt(testDoc.Root, "attr1"));
    }

    [Fact(DisplayName = "UnsetIntAttributeReturnsNull")]
    public void UnsetIntAttributeReturnsNull()
    {
        var testDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <test>
            </test>
            """);
        Assert.Null(XmlHelper.GetAttributeAsInt(testDoc.Root, "attr1"));
    }

    [Fact(DisplayName = "Can read CData value from child element")]
    public void CanReadCDataChildElement()
    {
        var testDoc = XDocument.Parse("""
            <?xml version="1.0"?>
            <test>
                <foo>
                  <![CDATA[bar]]>
                </foo>
            </test>
            """);
        Assert.Equal("bar", XmlHelper.GetCData(testDoc.Root, "foo"));
    }
}
