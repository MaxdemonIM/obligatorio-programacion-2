using System.Linq.Expressions;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_Obligatorio_P2.Controllers
{
    public class PasajeroController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;
        public IActionResult Index(string mensaje)
        {
            ViewBag.Mensaje = mensaje;
            _sistema.OrdenarPasajesPorPrecio();
            return View(_sistema.ListarPasajeros());
        }

        [HttpPost]
        public IActionResult Index(int puntos, string elegible, string pasajeroEmail)
        {
            try
            {
                foreach (Usuario usuario in _sistema.Usuarios) {

                    if (usuario.email == pasajeroEmail && usuario is Premium)
                    {
                        _sistema.ActualizarPuntosPremium(puntos, pasajeroEmail);
                        return RedirectToAction("Index", new { mensaje = "Puntos actualizados correctamente" });

                    }
                    else if(usuario.email == pasajeroEmail && usuario is Ocasional)
                    {
                        bool esElegible = elegible == "true";
                        _sistema.ActualizarElegibilidadOcasional(pasajeroEmail, esElegible);
                        return RedirectToAction("Index", new { mensaje = "Elegibilidad actualizada correctamente" });

                    }
                    
                }
                return View();
            } catch (Exception ex)
            {
                return RedirectToAction("Index", new { mensaje = ex.Message });
            }
        
        }

    }
           
}

    

