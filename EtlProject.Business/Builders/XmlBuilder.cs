using System.Xml.Linq;
using EtlProject.Business.Extensions;
using EtlProject.Core.Dtos;

namespace EtlProject.Business.Builders;

public class XmlBuilder
{
    public string BuildXml(InvoiceRequest invoice)
    {
        var elements = new List<XElement>
        {
            new XElement("id", invoice.Id),
            new XElement("debit", invoice.DebitAccount),
            new XElement("credit", invoice.CreditAccount),
            new XElement("amount", invoice.Amount),
            new XElement("currency", invoice.Currency),
            new XElement("details", invoice.Details),
        };

        elements.AddRange(invoice.Attributes.ToXmlElements());

        var doc = new XDocument(new XElement("invoice_payment", elements));

        return doc.ToString();
    }
}