using System.Collections.Generic;

namespace WebSiteTester.DAL.Entities
{
    public class TestedSite
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public ICollection<TestedPage> Pages { get; set; }

    }
}
