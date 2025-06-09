using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_Obligatorio_P2.Controllers
{
    public class PasajeController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;

        public IActionResult Index()
        {
            return View();

        }


        public IActionResult ListarPasajesPorPrecio()
        {
            _sistema.OrdenarPasajesPorPrecio();
            return View(_sistema.Pasajes); //para ordenar los pasajes emitidos por precio para CLIENTE. 

        }

        public IActionResult ListarPasajesPorFecha()
        {
            _sistema.OrdenarPasajes();
            return View(_sistema.Pasajes); //para ordenar los pasajes emitidos por fecha PARA ADMINISTRADOR. 

        }

        [HttpPost]

        //TODOOOOOOO 
        public IActionResult Add(int numVuelo, DateTime fecha, TipoEquipaje tipoEquipaje)
        {
            try 
            {
                Vuelo vuelo = _sistema.ObtenerVueloPorNumVuelo(numVuelo);
                string tipoUsuario = _sistema.ObtenerTipoUsuario(_sistema.Usuarios[7]);
                Pasajero pasajeroHardcodeado = (Pasajero)_sistema.Usuarios[7];

                Pasaje nuevo = new Pasaje(vuelo, pasajeroHardcodeado, fecha , tipoEquipaje);

                _sistema.AgregarPasaje(nuevo);

                return RedirectToAction("Index", "Vuelo",

                    new
                    {
                        mensaje = $"Se compró el pasaje para la fecha " +
                    $"{fecha.ToString("dd MMM yyyy")}"

                    });
            } catch (Exception ex)
            {
                return RedirectToAction("Index", "Vuelo", new { mensaje = ex.Message,});
            }; 
        }
    }
}

