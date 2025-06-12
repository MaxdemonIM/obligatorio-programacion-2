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

            return RedirectToAction("Login");
        }

      




    }
}
