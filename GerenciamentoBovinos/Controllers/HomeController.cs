using System.Web.Mvc;

namespace GerenciamentoBovinos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}