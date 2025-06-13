using System.Linq.Expressions;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using WebApp_Obligatorio_P2.Filters;

namespace WebApp_Obligatorio_P2.Controllers
{
    [Authentication]
    public class PasajeroController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;

        [SoloAdmin]
        public IActionResult Index(string mensaje)
        {

          
            ViewBag.Mensaje = mensaje;
            return View(_sistema.ListarPasajeros());
        }

        [SoloPasajero]
        public IActionResult VerPerfil() 
        {
            string mailLogueado = HttpContext.Session.GetString("email");
            Pasajero logueado = (Pasajero)_sistema.ObtenerUsuarioPorMail(mailLogueado);
            return View(logueado);
        }


        [HttpPost]
        [SoloAdmin]
        public IActionResult Index(int puntos, string elegible, string pasajeroEmail)
        {
            try
            {
                foreach (Usuario usuario in _sistema.Usuarios) {

                    if (usuario.Email == pasajeroEmail && usuario is Premium)
                    {
                        _sistema.ActualizarPuntosPremium(puntos, pasajeroEmail);
                        return RedirectToAction("Index", new { mensaje = "Puntos actualizados correctamente" });

                    }
                    else if(usuario.Email == pasajeroEmail && usuario is Ocasional)
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

    

