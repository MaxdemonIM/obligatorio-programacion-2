using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp_Obligatorio_P2.Filters
{
    public class SoloAdmin : ActionFilterAttribute
    {
    
        public override void OnActionExecuting(ActionExecutingContext context)
            {
                string rol = context.HttpContext.Session.GetString("rol");
                if (rol != "Administrador")
                {
                    context.Result = new RedirectToActionResult("Index", "Home", new { mensaje = "Acceso no autorizado." });
                }
                base.OnActionExecuting(context);
            }
        }
}

