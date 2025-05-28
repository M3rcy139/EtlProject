using System.Xml.Linq;

namespace EtlProject.Business.Extensions;

public static class DictionaryExtensions
{
    public static IEnumerable<XElement> ToXmlElements(this Dictionary<string, string> attributes)
    {
        return attributes.Select(attr => new XElement(attr.Key, attr.Value));
    }
}