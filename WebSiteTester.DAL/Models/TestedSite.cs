using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteTester.DAL.Models
{
    public class TestedSite
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public ICollection<TestedPage> Pages { get; set; }

    }
}
