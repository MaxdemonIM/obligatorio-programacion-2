﻿using Dominio;
using Microsoft.AspNetCore.Mvc;
using WebApp_Obligatorio_P2.Filters;

namespace WebApp_Obligatorio_P2.Controllers
{
    
    public class OcasionalController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;

        public IActionResult RegistrarOcasional()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("email")))
            {
                HttpContext.Session.SetString("mensaje", "Ya estás registrado/a y logueado/a.");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        [HttpPost]
        public IActionResult RegistrarOcasional(Ocasional ocasional)
        {
            try
            {
                _sistema.DarDeAltaUsuario(ocasional); 

                HttpContext.Session.SetString("email", ocasional.Email);
                HttpContext.Session.SetString("password", ocasional.Password);
                HttpContext.Session.SetString("rol", ocasional.GetType().Name); // para cuando se registre guarde el rol en la sesión
               
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
