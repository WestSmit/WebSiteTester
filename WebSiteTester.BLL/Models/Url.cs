using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
