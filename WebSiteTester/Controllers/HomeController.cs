
using ChartJS.Helpers.MVC;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

using WebSiteTester.BLL.Dtos;

using WebSiteTester.BLL.Services.Interfaces;

namespace WebSiteTester.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITesterService tester;
        public HomeController(ITesterService testerService)
        {
            tester = testerService;
            ViewBag.Urls = new List<string>();
            ViewBag.TestResult = new TestDto();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Start(string Url)
        {
            TestDto results;

            try
            {
                results = await tester.TestAllPages(Url);
                ChartTypeBar chart = new ChartTypeBar()
                {
                    Data = new BarData()
                    {
                        Labels = results.Pages.Select(x => x.Id.ToString()).ToArray(),
                        Datasets = new BarDataSets[]
                                    {
                                      new BarDataSets()
                                      {
                                          Label = "Response Time (ms)",
                                          BackgroundColor = "rgb(89, 207, 240)",
                                          BorderColor = "rgb(187, 89, 240)",
                                          BorderWidth = 0,
                                          LinearData = results.Pages.Select(x => (int)Math.Round(x.Results.Last().ResponseTime)).ToArray()
                                      }
                                    }
                    },
                    Options = new BarOptions()
                    {
                        Title = new ChartOptionsTitle()
                        {
                            Display = true,
                            Text = new string[] { results.Url }
                        },
                        Legend = new ChartOptionsLegend()
                        {
                            Position = ConstantPosition.TOP
                        },
                        BarPercentage = 0,
                        CategoryPercentage = 0,
                    }
                };
                ViewBag.ChartObj = chart;

                var ordered = results;
                ordered.Pages = ordered.Pages.OrderBy(p => p.Results.Last().ResponseTime).Reverse().ToList();

                return PartialView("TestResult", ordered);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return PartialView("TestResult");
            }
        }

    }
}