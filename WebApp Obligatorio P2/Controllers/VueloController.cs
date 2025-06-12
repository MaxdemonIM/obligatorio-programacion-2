using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_Obligatorio_P2.Controllers
{
    public class VueloController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;
        public IActionResult Index(string mensaje)
        {

            ViewBag.Mensaje = mensaje;
            ViewBag.Aeropuertos = _sistema.Aeropuertos;
            return View(_sistema.Vuelos);
        }

        [HttpPost]

        public IActionResult Index(string IATAsalida, string IATAllegada)
        {
            try
            {
                ViewBag.Aeropuertos = _sistema.Aeropuertos;
                List<Vuelo> vuelos = _sistema.ListarVuelosPorAeropuerto(IATAsalida, IATAllegada);


                return View(vuelos);
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index", "Vuelo", new { mensaje = ex.Message, });
            }
            ;
        }
      
           

        public IActionResult Details(int id) 
        {
            Vuelo vuelo = _sistema.ObtenerVueloPorNumVuelo(id);
            return View(vuelo);
        }
    }
}
