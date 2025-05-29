using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_Obligatorio_P2.Controllers
{
    public class PasajeController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ListarPasajes()
        {
            return View(_sistema.Pasajes);

        }



    }
}

