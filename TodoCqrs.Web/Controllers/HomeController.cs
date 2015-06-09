using System.Web.Mvc;

namespace TodoCqrs.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
