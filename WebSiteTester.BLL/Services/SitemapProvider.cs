using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using WebSiteTester.BLL.Models;
using WebSiteTester.BLL.Services.Interfaces;

namespace WebSiteTester.BLL.Services
{
    public class SitemapProvider : ISitemapProvider
    {
        private readonly HttpClient client = new HttpClient();

        public async Task<string> FindSitemapXML(string url)
        {
            var site = url;

            if (site.Last() != '/')
            {
                site = site + '/';
            }

            var robotsTxT = await GetTextResponse(site + "robots.txt");

            string sitemapUrl;

            if (robotsTxT.Contains("Sitemap"))
            {
                Regex regex = new Regex(@"Sitemap: .*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                sitemapUrl = regex.Match(robotsTxT).Value.Split(' ').Last();
            }
            else
            {
                sitemapUrl = site + "sitemap.xml";
            }

            return await GetTextResponse(sitemapUrl);
        }

        public async Task<string> GetTextResponse(string url)
        {
            var response = await client.GetAsync(url);

            //response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode) throw new Exception("Invalid request");

            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

        public bool IsSitemap(string xml)
        {

            var sitemapError = false;

            using (TextReader reader = new StringReader(xml))
            {
                XmlTextReader tr = new XmlTextReader(reader);
                XmlValidatingReader vr = new XmlValidatingReader(tr);
                vr.Schemas.Add("http://www.sitemaps.org/schemas/sitemap/0.9", "https://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd");
                vr.ValidationType = ValidationType.Schema;
                vr.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);
                while (vr.Read() && !sitemapError) ;
            }

            void ValidationHandler(object sender, ValidationEventArgs args)
            {
                sitemapError = true;
            }

            return !sitemapError;
        }

        public bool IsSitemapindex(string xml)
        {

            var sitemapindexError = false;

            using (TextReader reader = new StringReader(xml))
            {
                XmlTextReader tr = new XmlTextReader(reader);
                XmlValidatingReader vr = new XmlValidatingReader(tr);
                vr.Schemas.Add("http://www.sitemaps.org/schemas/sitemap/0.9", "https://www.sitemaps.org/schemas/sitemap/0.9/siteindex.xsd");
                vr.ValidationType = ValidationType.Schema;
                vr.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);
                while (vr.Read() && !sitemapindexError) ;
            }

            void ValidationHandler(object sender, ValidationEventArgs args)
            {
                sitemapindexError = true;
            }

            return !sitemapindexError;
        }

        public IEnumerable<string> GetSitemapUrls(string sitemapXML)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Urlset));

            var result = new Urlset();

            using (TextReader reader = new StringReader(sitemapXML))
            {
                try
                {
                    result = (Urlset)serializer.Deserialize(reader);
                }
                catch (System.InvalidOperationException)
                {
                    throw new Exception("Invalid sitemap format");
                }                
            }

            return result.Urls.Select(url => url.Loc);
        }

        public IEnumerable<string> GetSitemapindexUrls(string sitemapindexXML)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Sitemapindex));

            var result = new Sitemapindex();

            using (TextReader reader = new StringReader(sitemapindexXML))
            {
                try
                {
                    result = (Sitemapindex)serializer.Deserialize(reader);
                }
                catch (System.InvalidOperationException)
                {
                    throw new Exception("Invalid sitemapindex format");
                }
            }

            return result.Urls.Select(url => url.Loc);
        }

    }
}
