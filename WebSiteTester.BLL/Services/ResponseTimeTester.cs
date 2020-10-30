using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

using WebSiteTester.BLL.Services.Interfaces;

namespace WebSiteTester.BLL.Services
{
    public class ResponseTimeTester : IResponseTimeTester
    {
        public double Test(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            Stopwatch timer = new Stopwatch();
            timer.Start();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Close();

            timer.Stop();
            return timer.Elapsed.TotalMilliseconds;
        }
    }
}
