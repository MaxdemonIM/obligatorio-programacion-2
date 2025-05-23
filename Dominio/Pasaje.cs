using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pasaje : IValidable , IComparable<Pasaje>
    {
        private static int s_ultimoId = 0;
        private int _id;
        private Vuelo _vuelo;
        private Pasajero _pasajero;
        private DateTime _fecha;
        private TipoEquipaje _tipoEquipaje;
        private decimal _precio;

        public DateTime Fecha { get { return _fecha ; } }

        public Pasaje(Vuelo vuelo, Pasajero pasajero, DateTime fecha, TipoEquipaje tipoEquipaje)
        {
            //aumenta el valor de id cada vez que se crea la instancia
            this._id = s_ultimoId++;
            this._vuelo = vuelo;
            this._pasajero = pasajero;
            this._fecha = fecha;
            this._tipoEquipaje = tipoEquipaje;
            this._precio = this.CalcularPrecioDelPasaje();
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
            if (!_vuelo.Frecuencia.Contains(_fecha.DayOfWeek))
            {
                throw new Exception("La fecha del pasaje no coincide con la frecuencia del vuelo.");
            }
        }

        //Asigna en el atributo de precio, el valor obtenido mediante metodo CalcularPrecioPasaje realizado en Pasajero para guardarlo. 
        public decimal CalcularPrecioDelPasaje()
        {
            decimal precio = this._pasajero.CalcularPrecioPasajeSegunTipoDeCliente(this._vuelo, this._tipoEquipaje);

            return precio;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Pasaje)) return false;
            Pasaje otro = (Pasaje)obj;
            return this._pasajero.Equals(otro._pasajero) && this._vuelo.Equals(otro._vuelo) && this._fecha.Equals(otro._fecha);
        }


        public override string ToString()
        {
            return $"Datos del Pasaje: ID: {_id} | Pasajero: {_pasajero.Nombre} | Fecha: {_fecha:dd-MM-yyyy} | Precio: ${_precio.ToString("F2")} | Vuelo número: {_vuelo.NumVuelo}";
        }

        public int CompareTo(Pasaje? other)
        {
            return this._precio.CompareTo(other._precio) * -1;
        }
    }
}

