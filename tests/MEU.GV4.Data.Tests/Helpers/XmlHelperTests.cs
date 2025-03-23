using MEU.GV4.Data.Helpers;
using System.Xml;

namespace MEU.GV4.Data.Tests.Helpers
{
    public class XmlHelperTests
    {
        [Fact(DisplayName = "Can read attribute from an element")]
        public void CanReadAttributeFromElement()
        {
            var testDoc = new XmlDocument();
            testDoc.LoadXml("""
                <?xml version="1.0"?>
                <test attr1="foo">
                </test>
                """);
            Assert.Equal("foo", XmlHelper.GetAttribute(testDoc.DocumentElement, "attr1"));
        }

        [Fact(DisplayName = "Can read CData value from child element")]
        public void CanReadCDataChildElement()
        {
            var testDoc = new XmlDocument();
            testDoc.LoadXml("""
                <?xml version="1.0"?>
                <test>
                    <foo>
                      <![CDATA[bar]]>
                    </foo>
                </test>
                """);
            Assert.Equal("bar", XmlHelper.GetCData(testDoc.DocumentElement, "foo"));
        }
    }
}
