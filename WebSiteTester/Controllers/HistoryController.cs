using ChartJS.Helpers.MVC;

using System.Linq;
using System.Web.Mvc;

using WebSiteTester.BLL.Services.Interfaces;

namespace WebSiteTester.Controllers
{
    public class HistoryController : Controller
    {
        private readonly ITesterService tester;
        public HistoryController(ITesterService testerService)
        {
            tester = testerService;
        }

        public ActionResult Index()
        {
            var testedSites = tester.GetTestedSites();
            return View("History", testedSites);
        }
        
        public ActionResult Pages(int Id)
        {
            var testedPages = tester.GetTestedPages(Id);
            return View("Pages", testedPages);
        }

        public ActionResult Results(int Id)
        {
            var res = tester.GetTestResults(Id);

            ChartTypeLine chart = new ChartTypeLine()
            {
                Data = new LineData()
                {
                    Labels = res.Select(x => x.Id.ToString()).ToArray(),
                    Datasets = new LineDataSets[]
                   {
                        new LineDataSets()
                        {
                            Label = "Response Time (ms)",
                            BorderColor = "green",
                            BorderWidth = 2,
                            LinearData = res.Select(x=> (int)x.ResponseTime).ToArray()
                        }
                   }
                },
                Options = new LineOptions()
                {
                    Scales = new ChartOptionsScales()
                    {
                        XAxes = new ChartOptionsScalesAxes[]
                       {
                            new ChartOptionsScalesAxes()
                            {
                                Display = true,
                                ScaleLabel = new ChartScaleLabel()
                                {
                                    Display = true,
                                    LabelString = "ID"
                                }
                            }
                       },
                        YAxes = new ChartOptionsScalesAxes[]
                       {
                            new ChartOptionsScalesAxes()
                            {
                                Display = true,
                                ScaleLabel = new ChartScaleLabel()
                                {
                                    Display = true,
                                    LabelString = "t, ms"
                                }
                            }
                       }
                    },
                    Title = new ChartOptionsTitle()
                    {
                        Display = true,
                        Text = new string[] { "All Results" }
                    },
                    Legend = new ChartOptionsLegend()
                    {
                        Position = ConstantPosition.TOP,
                    },
                    Tooltips = new ChartOptionsTooltip()
                    {
                        Mode = ConstantMode.INDEX,
                        Intersect = false
                    },
                    Hover = new ChartOptionsHover()
                    {
                        Mode = ConstantMode.NEAREST,
                        Intersect = true
                    }
                }
            };

            ViewBag.chartObj = chart;
            
            return View("Results", res);
        }
    }
}