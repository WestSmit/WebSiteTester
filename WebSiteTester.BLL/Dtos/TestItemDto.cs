using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteTester.BLL.Dtos
{
    public class TestItemDto
    {
        public int Id { get; set; }
        public string BaseUrl { get; set; }
        public string Url { get; set; }
        public ICollection<TestResultDto> Results { get; set; }

    }
}
