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


        public IActionResult ListarPasajesPorPrecio()
        {
            _sistema.OrdenarPasajesPorPrecio();
            return View(_sistema.Pasajes); //para ordenar los pasajes emitidos por precio para CLIENTE. 

        }

        public IActionResult ListarPasajesPorFecha()
        {
            _sistema.OrdenarPasajes();
            return View(_sistema.Pasajes); //para ordenar los pasajes emitidos por fecha PARA ADMINISTRADOR. 

        }

        [HttpPost]

        public IActionResult Add()
        {

            
            return View(_sistema.Pasajes); //para ordenar los pasajes emitidos por fecha PARA ADMINISTRADOR. 

        }


    }
}

