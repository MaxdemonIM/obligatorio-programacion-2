using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_Obligatorio_P2.Filters
{
        public class SoloPasajero : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                string rol = context.HttpContext.Session.GetString("rol");

                if (rol != "Ocasional" && rol != "Premium")
                {
                    context.Result = new RedirectToActionResult("Index", "Home", new { mensaje = "Solo los pasajeros pueden acceder a esta función." });
                }

                base.OnActionExecuting(context);
            }
        }
    }



