using Microsoft.AspNetCore.Mvc;
using Dominio;

namespace WebApp_Obligatorio_P2.Controllers
    
{
  
    public class AdminController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult verPasajes()
        {
            try
            {
            return View(_sistema.Pasajes);
            
            }
            catch (Exception ex) 
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }



    }
}
