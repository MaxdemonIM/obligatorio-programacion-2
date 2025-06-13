using Dominio;
using Microsoft.AspNetCore.Mvc;
using WebApp_Obligatorio_P2.Filters;

namespace WebApp_Obligatorio_P2.Controllers
{
    [Authentication]
    public class PasajeController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;

        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("rol") == "Administrador")
            {
                return RedirectToAction("VerTodosLosPasajes");
            }
            else
            {
                return RedirectToAction("VerPasajesUsuario");
            }

        }

        [SoloPasajero]
        public IActionResult VerPasajesUsuario()
        {
            string mailLogueado = HttpContext.Session.GetString("email");
            Pasajero pasajeroLogueado = (Pasajero)_sistema.ObtenerUsuarioPorMail(mailLogueado);

            _sistema.OrdenarPasajesPorPrecio();
            return View("Index", _sistema.ObtenerListaPasajeDeUsuario(pasajeroLogueado));
        }


        [SoloAdmin]
        public IActionResult VerTodosLosPasajes()
        {

          
            _sistema.OrdenarPasajes();
            return View("Index", _sistema.Pasajes);
        }
        

        [HttpPost]
        [SoloPasajero]

        public IActionResult Add(int numVuelo, DateTime fecha, TipoEquipaje? tipoEquipaje)
        {
            try 
            {
                Vuelo vuelo = _sistema.ObtenerVueloPorNumVuelo(numVuelo);
                string mailLogueado = HttpContext.Session.GetString("email");
                Pasajero logueado = (Pasajero)_sistema.ObtenerUsuarioPorMail(mailLogueado);
                Pasaje nuevo = new Pasaje(vuelo, logueado, fecha, tipoEquipaje.Value);
                _sistema.EsTipoEquipajeValido(tipoEquipaje);


                _sistema.AgregarPasaje(nuevo);
              



                return RedirectToAction("Index", "Vuelo", new { mensaje = $"Se compró el pasaje para la fecha " +
                    $"{fecha.ToString("dd MMM yyyy")}"});
                
            } catch (Exception ex)
            {

                return RedirectToAction("Index", "Vuelo", new { mensaje = ex.Message,});
            }; 

        }

    }
}

