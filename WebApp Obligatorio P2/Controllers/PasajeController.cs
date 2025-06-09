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
        public IActionResult Add(int numVuelo, DateTime fecha, TipoEquipaje tipoEquipajeString)
        {
            try 
            {
                Vuelo vuelo = _sistema.ObtenerVueloPorNumVuelo(numVuelo);        
                Pasajero pasajeroHardcodeado = (Pasajero)_sistema.Usuarios[7];
               // ValidarTipoEquipaje(tipoEquipajeString);
                //TipoEquipaje tipoEquipaje = (TipoEquipaje)Enum.Parse(typeof(TipoEquipaje), tipoEquipajeString); // Recibe el string del formulario de la VIEW y lo convierte en TipoEquipaje (ENUM) para poder crear el pasaje. De lo contrario no se podría por si solo con lo que se manda de la VIEW por que es un STRING. 
                Pasaje nuevo = new Pasaje(vuelo, pasajeroHardcodeado, fecha , tipoEquipajeString);

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
        public void ValidarTipoEquipaje(string tipoEquipaje)
        {
            if(tipoEquipaje == "")
            {
                throw new Exception("Debe seleccionar un tipo de equipaje para comprar el pasaje.");
            }
        } 
    }
}

