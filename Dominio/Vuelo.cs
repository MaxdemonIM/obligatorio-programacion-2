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
        private List<string> _frecuencia;
        private decimal _costoXAsiento;

        public int NumVuelo { get { return _numVuelo; } }

        public List<string> Frecuencia { get { return this._frecuencia; } } //para pueda ser accesible desde pasaje que lo vamos usar. 



        public Vuelo(int numVuelo, Avion avion, Ruta ruta, List<string> frecuencia)
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
            this.ValidarFrecuencia();
            this.ValidarAvionPuedeCompletarRuta();
        }

        public void ValidarAvionPuedeCompletarRuta()
        {
            if(_avion.Alcance < _ruta.Distancia)
            {
                throw new Exception("El avión no puede recorrer la totalidad de la distancia de la ruta");
            }
        }

        public void ValidarFrecuencia() //para que sea un día de la semana. 
        {
            List<string> diasValidos = ["lunes", "martes", "miercoles", "jueves", "viernes", "sabado", "domingo"];


            // List<string> _frecuencia = ["lunes", "martes", "jueves", "sabado", "feriado"];

            int i = 0;
            bool esValido = true;

            while (esValido && i < this._frecuencia.Count)
            {
                string diaDeFrecuencia = this._frecuencia[i];

                bool encontrado = false;
                int j = 0;

                while (!encontrado && j < diasValidos.Count)
                {
                    if (diaDeFrecuencia == diasValidos[j])
                    {
                        encontrado = true;
                    }
                    j++;
                }

                if (!encontrado)
                {
                    esValido = false;
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
            decimal costoPorAsiento = 0;
            return costoPorAsiento = ((costoXKm * distanciaRuta) + costoOppAeropuertos) / cantAsientos;
        }


        
        //FALTA LO DE LISTAR VUELOENAEROPUERTO.

        
        public override string ToString()
        {
            return $"{this._numVuelo} {this._avion} {this._ruta} {this._frecuencia}\n";
        }

    }
}
