using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using WebSiteTester.DAL.Data;
using WebSiteTester.DAL.Entities;
using WebSiteTester.DAL.Repositories.Interfaces;

namespace WebSiteTester.DAL.Repositories
{
    public class TestsRepository: ITestsRepository
    {
        private readonly DataContext data;
        public TestsRepository(string connectionString)
        {
            data = new DataContext(connectionString);
        }

        public TestedSite GetTestedSite(string baseUrl)
        {
            return data.TestedSites.Include("Pages.Results").SingleOrDefault(t => t.Url == baseUrl);
        }

        public IQueryable<TestedSite> GetTestedSites()
        {
            return data.TestedSites.Include("Pages.Results");
        }

        public IQueryable<TestedPage> GetTestedPages(int siteId)
        {
            return data.TestedPages.Where(x => x.Site.Id == siteId);
        }

        public IEnumerable<TestResult> GetTestResults(int pageId)
        {
            var page = data.TestedPages.Include("Results").SingleOrDefault(x => x.Id == pageId);

            if (page == null)
            {
                throw new Exception("Page not Found");
            }

            return page.Results;
        }

        public void AddTest(TestedSite site)
        {
            if (site != null)
            {
                data.TestedSites.Add(site);
            }
        }

        public async Task<bool> SaveAll()
        {
            return await data.SaveChangesAsync() > 0;
        }
    }
}
