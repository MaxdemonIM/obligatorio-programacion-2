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
            return View();
        }

        [HttpPost]

        public IActionResult Index(string email, string password)
        {
            Usuario logueado = _sistema.Login(email, password);

            HttpContext.Session.SetString("email", email);

            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.SetString("email", "");

            return RedirectToAction("Index");
        }



    }
}
