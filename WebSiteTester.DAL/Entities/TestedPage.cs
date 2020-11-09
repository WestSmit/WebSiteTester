using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSiteTester.DAL.Entities
{
    public class TestedPage
    {
        public int Id { get; set; }
        public TestedSite Site { get; set; }
        public string Url { get; set; }
        public ICollection<TestResult> Results { get; set; }
    }
}
