using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteTester.DAL.Models
{
    public class TestResult
    {
        public int Id { get; set; }
        public double ResponseTime { get; set; }
        public TestedPage Page { get; set; }
        public DateTime Date { get; set; }

        public TestResult()
        {
            Date = DateTime.Now;
        }
    }
}
