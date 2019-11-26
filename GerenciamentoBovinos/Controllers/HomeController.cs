using System.Web.Mvc;

namespace GerenciamentoBovinos.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Gera a view com a tela inicial
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}