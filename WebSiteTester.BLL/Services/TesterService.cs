using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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

        public async Task TestPage(string siteUrl, string url)
        {
            var responseTime = responseTimeTester.Test(url);

            var site = await repo.GetTestedSiteAsync(siteUrl);

            var page = site.Pages.SingleOrDefault(item => item.Url == url);

            if (page == null)
            {
                page = new TestedPage
                {
                    Url = url,
                    Site = site,
                    Results = new List<TestResult>()
                };

                repo.AddTestedPage(page);

                if (!await repo.SaveAllAsync())
                {
                    throw new Exception("Save error");
                }
            }

            page.Results.Add(new TestResult(responseTime, page));
            repo.Update(page);

            if (!await repo.SaveAllAsync())
            {
                throw new Exception("Save error");
            }
        }

        public async Task<TestDto> TestAllPages(string baseUrl)
        {
            var siteUrl = baseUrl;

            if (siteUrl.Last() != '/')
            {
                siteUrl = siteUrl + '/';
            }

            var urls = await GetPages(siteUrl);

            var site = await repo.GetTestedSiteAsync(siteUrl);

            if (site == null)
            {
                site = new TestedSite
                {
                    Url = siteUrl,
                    Pages = new List<TestedPage>()
                };

                repo.AddTestedSite(site);

                if (!await repo.SaveAllAsync())
                {
                    throw new Exception("Save error");
                }
            }

            foreach (var item in urls)
            {
                await TestPage(site.Url, item);
            }

            return mapper.Map<TestDto>(site);
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
