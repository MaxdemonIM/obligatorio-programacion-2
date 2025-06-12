using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp_Obligatorio_P2.Filters
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            string logueado = context.HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(logueado))
                context.Result = new RedirectToActionResult("index","Home", new { mensaje = "Debe iniciar sesión para acceder a esa función." });

            base.OnActionExecuting(context);
        }

    }
}
