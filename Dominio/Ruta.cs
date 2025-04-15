using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Ruta
    {

        private static int s_ultimoId = -1;
        private int _id;
        private Aeropuerto _aeropuertoLlegada;
        private Aeropuerto _aeropuertoSalida;
        private decimal _distancia;

        public decimal Distancia { get  { return this._distancia; } } //Para validar que el avion pueda hacer el vuelo.

        public Aeropuerto AeropuertoLlegada { get { return this._aeropuertoLlegada ; } }

        public Aeropuerto AeropuertoSalida { get { return _aeropuertoSalida ; } }

        public Ruta(Aeropuerto aeropuertoLlegada, Aeropuerto aeropuertoSalida, decimal distancia)
        {
            //aumenta el valor de id cada vez que se crea la instancia
            this._id = s_ultimoId++;
            this._aeropuertoLlegada = aeropuertoLlegada;
            this._aeropuertoSalida = aeropuertoSalida;
            this._distancia = distancia;
            this.Validar();
        }

        //el get para poder ver el valor del ultimo id asignado a la instancia anterior
        public static int UltimoIdRuta
        {
            get { return s_ultimoId; }

        }

        public void Validar()
        {
            this.ValidarDistancia();
       

        }

        public void ValidarDistancia()
        {
            if (this._distancia <= 0)
            {
                throw new Exception("La ruta no puede tener una distancia negativa o igual a 0.");
            }

        }

        public void ValidarQueAeropuertoNoSeRepita()
        {
            if (_aeropuertoLlegada.Equals(_aeropuertoSalida)) 
            {
                throw new Exception("El aeropuerto de salida no puede ser el mismo que el de llegada");
            }
        }




    }
}
