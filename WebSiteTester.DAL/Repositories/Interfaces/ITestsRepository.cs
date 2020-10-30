using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebSiteTester.DAL.Models;

namespace WebSiteTester.DAL.Repositories.Interfaces
{
    public interface ITestsRepository
    {
        TestedSite GetTestedSite(string baseUrl);
        IQueryable<TestedSite> GetTestedSites();
        Task<bool> SaveAll();
        void AddTest(TestedSite site);
        IQueryable<TestedPage> GetTestedPages(int siteId);
        IEnumerable<TestResult> GetTestResults(int pageId);
    }
}
