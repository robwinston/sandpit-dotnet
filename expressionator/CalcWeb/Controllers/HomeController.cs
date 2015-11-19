using System.Web.Mvc;

namespace CalcWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Simple Expression Evaluator";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Thom's Challenge Demo Application.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Cottage Industries PLC";

            return View();
        }

        public ActionResult Aspnet()
        {
            ViewBag.Message = "ASP.NET Info";

            return View();
        }


    }
}