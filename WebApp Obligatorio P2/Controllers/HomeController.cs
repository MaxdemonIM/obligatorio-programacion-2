using System.Runtime.CompilerServices;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_Obligatorio_P2.Controllers
{
    public class HomeController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;
        public IActionResult Index( string mensaje)
        {
            if (string.IsNullOrEmpty(mensaje))  //para el error de mensaje ya logueado
            {
                mensaje = HttpContext.Session.GetString("mensaje");
                
            }
            ViewBag.Mensaje = mensaje;
            return View();
        }

        public IActionResult Login(string mensaje)
        {
            ViewBag.Mensaje = mensaje;
            return View();
        }
        [HttpPost]

        public IActionResult Login(string email, string password)
        {
            try
            {
                Usuario logueado = _sistema.Login(email, password);

                HttpContext.Session.SetString("email", email);

                HttpContext.Session.SetString("rol", logueado.GetType().Name);

                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Home", new { mensaje = ex.Message, });
            }
        }


        public IActionResult Logout()
        {
            HttpContext.Session.SetString("email", "");
            HttpContext.Session.Clear(); // Limpia toda la sesión para me deje ingresar de nuevo y no me lance la excepcion de ya estoy registrado o logueado.

            return RedirectToAction("Login");
        }

      




    }
}
