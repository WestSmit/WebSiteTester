using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteTester.BLL.Services.Interfaces
{
    public interface ISitemapProvider
    {
        Task<string> FindSitemapXML(string url);
        Task<string> GetTextResponse(string url);
        IEnumerable<string> GetSitemapUrls(string sitemapXML);
        IEnumerable<string> GetSitemapindexUrls(string sitemapXML);
        bool IsSitemap(string xml);
        bool IsSitemapindex(string xml);
    }
}
