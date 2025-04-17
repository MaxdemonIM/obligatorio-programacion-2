using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Vuelo
    {

        private int _numVuelo;
        private Avion _avion;    //SE PASA OBJETO
        private Ruta _ruta;
        private string _frecuencia;
        private decimal _costoXAsiento;

        public int NumVuelo { get { return _numVuelo; } }

        public string Frecuencia { get { return this._frecuencia; } } //para pueda ser accesible desde pasaje que lo vamos usar. 

        public Ruta Ruta { get { return this._ruta; } } //para sean publicas para la PARTE B

        public Avion Avion { get { return this._avion; } } ////para sean publicas para la PARTE B


        public Vuelo(int numVuelo, Avion avion, Ruta ruta, string frecuencia)
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

        public void ValidarFrecuencia() //para que los dias ingresados correspondan a un día de la semana. 
        {
            List<string> diasValidos = ["lunes", "martes", "miercoles", "miércoles", "jueves", "viernes", "sabado", "sábado", "domingo"];

            int i = 0;
            bool esValido = false;
            while (!esValido && i < diasValidos.Count)
            {
                if (this._frecuencia == diasValidos[i])
                {
                    esValido = true;
                }
                i++;
            }

            if (!esValido)
            {
                throw new Exception($"Valor inválido.\n" + "Se esperaba el nombre de un día de la semana (ejemplo: lunes).");
            }
        }

        public decimal CalcularCostoPorAsiento()
        {
            decimal costoXKm = _avion.CostoXKm;
            decimal distanciaRuta = _ruta.Distancia;
            decimal costoOppAeropuertos = _ruta.AeropuertoSalida.CostoOpp + _ruta.AeropuertoLlegada.CostoOpp;
            int cantAsientos = _avion.CantAsientos;
            return ((costoXKm * distanciaRuta) + costoOppAeropuertos) / cantAsientos;
        }

        public override string ToString()
        {
            return $"Vuelo # {this._numVuelo} - {this._avion} - {this._ruta} - {this._frecuencia}\n";
        }

    }
}
