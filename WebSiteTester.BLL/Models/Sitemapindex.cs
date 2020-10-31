using System.Xml.Serialization;

namespace WebSiteTester.BLL.Models
{
    [XmlRoot("sitemapindex", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class Sitemapindex
    {
        [XmlElement("sitemap")]
        public Url[] Urls { get; set; }
    }
}
