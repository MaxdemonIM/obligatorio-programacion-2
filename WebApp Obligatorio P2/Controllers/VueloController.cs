using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_Obligatorio_P2.Controllers
{
    public class VueloController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListarVuelos()
        {
            return View(_sistema.Vuelos);
        }

        public IActionResult Details(int id) 
        {
            Vuelo vuelo = _sistema.ObtenerVueloPorNumVuelo(id);
            return View(vuelo);
        }
    }
}
