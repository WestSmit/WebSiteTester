using System;
using System.Xml.Serialization;

namespace WebSiteTester.BLL.Models
{
    [Serializable]
    public class Url
    {
        [XmlElement("loc")]
        public string Loc { get; set; }
        [XmlElement("lastmod")]
        public string Lastmod;
    }
}
