using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSiteTester.DAL.Entities
{
    public class TestResult
    {
        [Key]
        public int Id { get; set; }
        public double ResponseTime { get; set; }
        public TestedPage Page { get; set; }
        public DateTime Date { get; set; }

        public TestResult()
        {
            Date = DateTime.Now;
        }

        public TestResult(double time, TestedPage page)
        {
            Date = DateTime.Now;
            ResponseTime = time;
            Page = page;
        }
    }
}
