using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteTester.BLL.Dtos
{
    public class TestResultDto
    {
        public int Id { get; set; }
        public double ResponseTime { get; set; }
        public DateTime Date { get; set; }

    }
}
