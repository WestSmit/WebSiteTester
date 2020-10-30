using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteTester.BLL.Services.Interfaces
{
    public interface IResponseTimeTester
    {
        double Test(string url);
    }
}
