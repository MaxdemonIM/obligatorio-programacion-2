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

        public static decimal PORCENTAJE_GANANCIA = 0.25m;


        //GET para acceder desde MVC
        public int Id { get { return _id; } }
        public Vuelo Vuelo { get { return _vuelo; } }
        public Pasajero Pasajero { get { return _pasajero; } }
        public DateTime Fecha { get { return _fecha; } }
        public TipoEquipaje TipoEquipaje { get { return _tipoEquipaje; } }
        public decimal Precio { get { return _precio; } }



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

            decimal costoPorAsiento = _vuelo.CalcularCostoPorAsiento();

            decimal precioBase = costoPorAsiento * PORCENTAJE_GANANCIA;

            decimal cargoPorEquipaje = _pasajero.CalcularPrecioEquipaje(this._tipoEquipaje);

            decimal tasasPortuarias = _vuelo.ObtenerCostoTasasPortuarias();

            return costoPorAsiento * (PORCENTAJE_GANANCIA + cargoPorEquipaje) + tasasPortuarias;
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

        //ESTO ORDENA AL ADMINISTRADOR LOS PASAJES EMITIDOS POR FECHA
        public int CompareTo(Pasaje? other)
        {
            return this._fecha.CompareTo(other._fecha);
        }
    }
}

