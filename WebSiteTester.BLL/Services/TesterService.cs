using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebSiteTester.BLL.Dtos;
using WebSiteTester.BLL.Services.Interfaces;

using WebSiteTester.DAL.Entities;
using WebSiteTester.DAL.Repositories.Interfaces;

namespace WebSiteTester.BLL.Services
{
    public class TesterService : ITesterService
    {
        private readonly ISitemapProvider sitemapProvider;
        private readonly IResponseTimeTester responseTimeTester;
        private readonly ITestsRepository repo;
        private readonly IMapper mapper;
        public TesterService(ISitemapProvider smProvider, IResponseTimeTester tester, ITestsRepository testsRepository, IMapper map)
        {
            sitemapProvider = smProvider;
            responseTimeTester = tester;
            repo = testsRepository;
            mapper = map;
        }

        public async Task<IEnumerable<string>> GetPages(string baseUrl)
        {
            var xml = await sitemapProvider.FindSitemapXML(baseUrl);
            var urls = new List<string>();

            if (sitemapProvider.IsSitemap(xml))
            {
                urls.AddRange(sitemapProvider.GetSitemapUrls(xml));
            }
            else if (sitemapProvider.IsSitemapindex(xml))
            {
                var sitemaps = sitemapProvider.GetSitemapindexUrls(xml);
                foreach (var url in sitemaps)
                {
                    var sitemapXml = await sitemapProvider.GetTextResponse(url);
                    urls.AddRange(sitemapProvider.GetSitemapUrls(sitemapXml));
                }
            }
            else
            {
                throw new Exception("Invalid Data");
            }

            return urls;
        }

        public async Task<TestDto> TestPage(string baseUrl, string url)
        {
            var site = repo.GetTestedSite(baseUrl);
            if (site == null)
            {
                site = new TestedSite();
                site.Url = baseUrl;
                site.Pages = new List<TestedPage>();
                repo.AddTest(site);
                if (!await repo.SaveAll())
                {
                    throw new Exception("Save error");
                }
            }

            var page = site.Pages.SingleOrDefault(item => item.Url == url);

            if (page == null)
            {
                var newPage = new TestedPage();
                newPage.Site = site;
                newPage.Url = url;

                var responseTime = responseTimeTester.Test(url);
                newPage.Results = new List<TestResult>();
                newPage.Results.Add(new TestResult() { ResponseTime = responseTimeTester.Test(url), Page = newPage });

                site.Pages.Add(newPage);
                if (!await repo.SaveAll())
                {
                    throw new Exception("Save error");
                }
            }
            else
            {
                page.Results.Add((new TestResult() { ResponseTime = responseTimeTester.Test(url), Page = page }));
                if (!await repo.SaveAll())
                {
                    throw new Exception("Save error");
                }
            }

            return mapper.Map<TestDto>(site);
        }

        public async Task<TestDto> TestAllPages(string baseUrl)
        {
            var urls = await GetPages(baseUrl);

            var result = new TestDto();
            foreach (var item in urls)
            {
                result = await TestPage(baseUrl, item);
            }

            return result;
        }

        public IEnumerable<TestDto> GetTestedSites()
        {
            return mapper.Map<IEnumerable<TestDto>>(repo.GetTestedSites());
        }

        public IEnumerable<TestItemDto> GetTestedPages(int siteId)
        {
            return mapper.Map<IEnumerable<TestItemDto>>(repo.GetTestedPages(siteId));
        }

        public IEnumerable<TestResultDto> GetTestResults(int pageId)
        {
            return mapper.Map<IEnumerable<TestResultDto>>(repo.GetTestResults(pageId));
        }

    }
}
