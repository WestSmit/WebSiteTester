using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebSiteTester.BLL.Dtos;
using WebSiteTester.BLL.Models;

namespace WebSiteTester.BLL.Services.Interfaces
{
    public interface ITesterService
    {
        Task<IEnumerable<string>> GetPages(string baseUrl);
        Task<TestDto> TestPage(string baseUrl, string url);
        Task<TestDto> TestAllPages(string baseUrl);
        IEnumerable<TestDto> GetTestedSites();
        IEnumerable<TestItemDto> GetTestedPages(int siteId);
        IEnumerable<TestResultDto> GetTestResults(int pageId);
    }
}
