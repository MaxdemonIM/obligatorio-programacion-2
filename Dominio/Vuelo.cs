using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Dominio.Interfaces;

namespace Dominio
{
    public class Vuelo : IValidable
    {

        private int _numVuelo;
        private Avion _avion;    //SE PASA OBJETO
        private Ruta _ruta;
        private List<DayOfWeek> _frecuencia;
        private decimal _costoXAsiento;

        public int NumVuelo { get { return _numVuelo; } }

        public List<DayOfWeek> Frecuencia { get { return new List<DayOfWeek>(_frecuencia); } } //para pueda ser accesible desde pasaje que lo vamos usar. 

        public Ruta Ruta { get { return this._ruta; } } //para sean publicas para la PARTE B

        public Avion Avion { get { return this._avion; } } ////para sean publicas para la PARTE B

        public decimal CostoXAsiento { get { return _costoXAsiento; } }


        public Vuelo(int numVuelo, Avion avion, Ruta ruta, List<DayOfWeek> frecuencia)
        {
            this._numVuelo = numVuelo;
            this._avion = avion;
            this._ruta = ruta;
            this._frecuencia = frecuencia;
            this._costoXAsiento = this.CalcularCostoPorAsiento();
            this.Validar();
        }

        public void Validar()
        {
            this.ValidarAvionPuedeCompletarRuta();
            this.ValidarFrecuencia();
        }

        public void ValidarAvionPuedeCompletarRuta()
        {
            if (_avion.Alcance < _ruta.Distancia)
            {
                throw new Exception("El avión no puede recorrer la totalidad de la distancia de la ruta");
            }
        }

        public void ValidarFrecuencia()
        {
            if (_frecuencia == null || _frecuencia.Count == 0)
                throw new Exception("La frecuencia del vuelo no puede estar vacía.");
        }

        public decimal CalcularCostoPorAsiento()
        {
            decimal costoXKm = _avion.CostoXKm;
            decimal distanciaRuta = _ruta.Distancia;
            decimal costoOppAeropuertos = _ruta.AeropuertoSalida.CostoOpp + _ruta.AeropuertoLlegada.CostoOpp;
            int cantAsientos = _avion.CantAsientos;
            return ((costoXKm * distanciaRuta) + costoOppAeropuertos) / cantAsientos;
        }

        public string ObtenerFrecuenciaFormateada() //para poder mostrar la frecuencia como string en el programa, porque se guarda en formato DayOfWeek
        {
            string resultado = "";
            CultureInfo cultura = new CultureInfo("es-ES");

            for (int i = 0; i < _frecuencia.Count; i++)
            {
                string diaEnEspanol = cultura.DateTimeFormat.GetDayName(_frecuencia[i]);

                diaEnEspanol = char.ToUpper(diaEnEspanol[0]) + diaEnEspanol.Substring(1);

                resultado += diaEnEspanol;

                if (i < _frecuencia.Count - 1)
                {
                    resultado += " - ";
                }
            }

            return resultado;
        }

        //Metodo AUXILIAR para obtener el costo de las tasas portuarias de cada aeropuerto involucrado, sumarlas y obtener el total.
        //Luego lo llamamos en cada una de las subclases, para sumar el valor en la operación, dependiendo si es ocasional o premium. 
        public decimal ObtenerCostoTasasPortuarias()
        {
            decimal costoAeropuertoSalida = this._ruta.AeropuertoSalida.CostoTasas;
            decimal costoAeropuertoLlegada = this._ruta.AeropuertoLlegada.CostoTasas;

            decimal costoTasasTotal = costoAeropuertoLlegada + costoAeropuertoSalida;

            return costoTasasTotal;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Vuelo)) return false;
            Vuelo otro = (Vuelo)obj;
            return this._avion.Equals(otro._avion) && this._ruta.Equals(otro._ruta);
        }


        public override string ToString()
        {
            return $"Vuelo # {this._numVuelo} | Modelo de avion:  {this._avion} | Ruta: {this._ruta} Frecuencia: {this.ObtenerFrecuenciaFormateada()}\n";
        }

    }
}
