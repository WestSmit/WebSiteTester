using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteTester.BLL.Dtos
{
    public class TestDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public ICollection<TestItemDto> Pages { get; set; } = new List<TestItemDto>();
    }
}
