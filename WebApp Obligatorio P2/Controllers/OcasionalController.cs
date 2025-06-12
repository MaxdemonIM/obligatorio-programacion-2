using Dominio;
using Microsoft.AspNetCore.Mvc;
using WebApp_Obligatorio_P2.Filters;

namespace WebApp_Obligatorio_P2.Controllers
{
    
    public class OcasionalController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;

        public IActionResult RegistrarOcasional()
        {

            
            return View();
        }


        [HttpPost]
        public IActionResult RegistrarOcasional(Ocasional ocasional)
        {
            try
            {
                HttpContext.Session.SetString("email", ocasional.email);
                HttpContext.Session.SetString("password", ocasional.password);
                _sistema.DarDeAltaUsuario(ocasional);
                return RedirectToAction("Index","Home");

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            
            
        }

    }
}
