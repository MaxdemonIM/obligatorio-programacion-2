using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Dominio
{
    public class Pasaje
    {
        private static int s_ultimoId = -1;
        private int _id;
        private Vuelo _vuelo;
        private Pasajero _pasajero;
        private DateTime _fecha;
        private string _equipaje;
        private decimal _precio;

        public DateTime Fecha { get { return _fecha ; } }

        public Pasaje(Vuelo vuelo, Pasajero pasajero, DateTime fecha, string equipaje, decimal precio)
        {
            
            //aumenta el valor de id cada vez que se crea la instancia
            this._id = s_ultimoId++;
            this._vuelo = vuelo;
            this._pasajero = pasajero;
            this._fecha = fecha;
            this._equipaje = equipaje;
            this._precio = precio;
            this.Validar();
        }

        //el get para poder ver el valor del ultimo id asignado a la instancia anterior (NO NECESARIO, PERO PODEMOS USARLO PARA TESTING).
        public static int UltimoId
        {
            get { return s_ultimoId; }

        }

        public void Validar()
        {
            this.ValidarFechaCorrespondeFrecuencia();
        }


        public void ValidarFechaCorrespondeFrecuencia()
        {
            string dia = _fecha.ToString("dddd", new CultureInfo("es-ES")).ToLower(); //agregamos globalizacion, para que cualquier dato de dia se transforme al español. 
         
            if (_vuelo.Frecuencia.IndexOf(dia) == -1)
            {
                throw new Exception("La fecha del pasaje no coincide con la frecuencia del vuelo.");
            }
        }



        public override string ToString()
        {
            return $"Datos del Pasaje: ID: {_id} | Pasajero: {_pasajero.Nombre} | Fecha: {_fecha:dd-MM-yyyy} | Precio: ${_precio} | Vuelo: {_vuelo.NumVuelo}";
        }


        //FALTAN LOS METODOS!!!!!!!!




    }


}

