using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebSiteTester.DAL.Entities;

namespace WebSiteTester.DAL.Repositories.Interfaces
{
    public interface ITestsRepository
    {
        Task<TestedSite> GetTestedSiteAsync(string baseUrl);
        IQueryable<TestedSite> GetTestedSites();
        Task<bool> SaveAllAsync();
        void AddTestedSite(TestedSite site);
        IQueryable<TestedPage> GetTestedPages(int siteId);
        IEnumerable<TestResult> GetTestResults(int pageId);
        void AddTestedPage(TestedPage page);
        void Update<T>(T item);
    }
}
