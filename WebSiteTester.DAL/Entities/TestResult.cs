using System;

namespace WebSiteTester.DAL.Entities
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
