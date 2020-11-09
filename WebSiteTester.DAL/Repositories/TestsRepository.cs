using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using WebSiteTester.DAL.Data;
using WebSiteTester.DAL.Entities;
using WebSiteTester.DAL.Repositories.Interfaces;

namespace WebSiteTester.DAL.Repositories
{
    public class TestsRepository : ITestsRepository
    {
        private readonly DataContext data;
        public TestsRepository(string connectionString)
        {
            data = new DataContext(connectionString);
        }

        public async Task<TestedSite> GetTestedSiteAsync(string baseUrl)
        {
            return await data.TestedSites.Include("Pages.Results").FirstOrDefaultAsync(t => t.Url == baseUrl);
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
            var page = data.TestedPages.Include("Results").FirstOrDefault(x => x.Id == pageId);

            if (page == null)
            {
                throw new Exception("Page not Found");
            }

            return page.Results;
        }

        public void AddTestedSite(TestedSite site)
        {
            if (site != null)
            {
                data.TestedSites.Add(site);
            }
        }

        public void AddTestedPage(TestedPage page)
        {
            if (page != null)
            {
                data.TestedPages.Add(page);
            }
        }

        public void Update<T>(T item)
        {
            data.Entry(item).State = EntityState.Modified;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await data.SaveChangesAsync() > 0;
        }
    }
}
