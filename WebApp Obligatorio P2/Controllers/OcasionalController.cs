using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_Obligatorio_P2.Controllers
{

    public class OcasionalController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult RegistrarOcasional()
        {

            
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarOcasional(Ocasional ocasional)
        {
            try
            {
                _sistema.DarDeAltaUsuario(ocasional);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            
            
        }

    }
}
