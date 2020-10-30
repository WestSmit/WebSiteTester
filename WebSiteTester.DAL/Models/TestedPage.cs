using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteTester.DAL.Models
{
    public class TestedPage
    {
        public int Id { get; set; }
        public TestedSite Site { get; set; }
        public string Url { get; set; }
        public ICollection<TestResult> Results { get; set; }
    }
}
