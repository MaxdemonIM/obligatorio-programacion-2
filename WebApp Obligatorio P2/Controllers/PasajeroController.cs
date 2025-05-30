using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_Obligatorio_P2.Controllers
{
    public class PasajeroController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListarPasajeros()
        {

            return View(_sistema.ListarPasajeros());

        }
    }
}
