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

        /*
        public IActionResult ListarPasajesPorPrecio()
        {
            List<Pasaje> listaOrdenada = 
            _sistema.OrdenarPasajesPorPrecio(_sistema.ObtenerListaDePasajero());//rOTOOOOOOOOOOOOOOOOOOOOOOOORETSWEIORTSWUERITSERT

            return View(); //para ordenar los pasajes emitidos por precio para CLIENTE. 

        }*/

        public IActionResult ListarPasajesPorFecha()
        {
            _sistema.OrdenarPasajes();
            return View(_sistema.Pasajes); //para ordenar los pasajes emitidos por fecha PARA ADMINISTRADOR. 

        }

        [HttpPost]

        //TODOOOOOOO 
        public IActionResult Add(int numVuelo, DateTime fecha, TipoEquipaje? tipoEquipaje)
        {
            try 
            {
                Vuelo vuelo = _sistema.ObtenerVueloPorNumVuelo(numVuelo);        
                Pasajero pasajeroHardcodeado = (Pasajero)_sistema.Usuarios[7];
                this.EsTipoEquipajeValido(tipoEquipaje);
                
                Pasaje nuevo = new Pasaje(vuelo, pasajeroHardcodeado, fecha , tipoEquipaje.Value);

                _sistema.AgregarPasaje(nuevo);

                return RedirectToAction("Index", "Vuelo", new { mensaje = $"Se compró el pasaje para la fecha " +
                    $"{fecha.ToString("dd MMM yyyy")}"});
                
            } catch (Exception ex)
            {

                return RedirectToAction("Index", "Vuelo", new { mensaje = ex.Message,});
            }; 

        }
        public void EsTipoEquipajeValido(TipoEquipaje? tipoEquipaje)
        {
            if(tipoEquipaje == null )
            {
                throw new Exception("Debe seleccionar un tipo de equipaje para comprar el pasaje.");
            }

        } 
    }
}

