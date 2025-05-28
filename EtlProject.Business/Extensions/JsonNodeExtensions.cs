using System.Text.Json.Nodes;

namespace EtlProject.Business.Extensions;

public static class JsonNodeExtensions
{
    public static Dictionary<string, string> ToAttributeDictionary(this JsonNode? rootNode)
    {
        var result = new Dictionary<string, string>();

        var attributeArray = rootNode?["attributes"]?["attribute"]?.AsArray();
        if (attributeArray is null)
            return result;

        foreach (var attrNode in attributeArray)
        {
            var code = attrNode?["code"]?.GetValue<string>();
            var value = attrNode?["attribute"]?.GetValue<string>();
            if (!string.IsNullOrEmpty(code) && value is not null)
            {
                result[code] = value;
            }
        }

        return result;
    }
}