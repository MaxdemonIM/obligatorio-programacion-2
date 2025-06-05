using Microsoft.AspNetCore.Mvc;

namespace WebApp_Obligatorio_P2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
