using System.Web.Mvc;

namespace SignalRDemo.ServerTwo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}