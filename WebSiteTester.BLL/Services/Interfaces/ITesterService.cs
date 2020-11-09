using System.Collections.Generic;
using System.Threading.Tasks;

using WebSiteTester.BLL.Dtos;

namespace WebSiteTester.BLL.Services.Interfaces
{
    public interface ITesterService
    {
        Task<IEnumerable<string>> GetPages(string baseUrl);
        Task TestPage(string siteUrl, string url);
        Task<TestDto> TestAllPages(string baseUrl);
        IEnumerable<TestDto> GetTestedSites();
        IEnumerable<TestItemDto> GetTestedPages(int siteId);
        IEnumerable<TestResultDto> GetTestResults(int pageId);
    }
}
