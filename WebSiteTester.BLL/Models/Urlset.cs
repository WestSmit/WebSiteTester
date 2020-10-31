using System.Xml.Serialization;

namespace WebSiteTester.BLL.Models
{
    [XmlRoot("urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class Urlset
    {
        [XmlElement("url")]
        public Url[] Urls { get; set; }
    }
}
