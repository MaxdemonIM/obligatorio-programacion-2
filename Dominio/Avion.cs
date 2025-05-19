using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Avion : IValidable
    {
        private string _fabricante;
        private string _modelo;  //determinante to do
        private int _alcance;
        private int _cantAsientos;
        private decimal _costoXKm;
        private string _tipoAeronave;

        public int Alcance { get { return this._alcance; } } //Para validar que el avion pueda hacer el vuelo.

        public decimal CostoXKm { get { return this._costoXKm;} }

        public int CantAsientos { get { return this._cantAsientos; } }

        public string Modelo { get { return this._modelo; } }

        public Avion(string fabricante, string modelo, int alcance, int cantAsientos, decimal costoXKm, string tipoAeronave)
        {
            this._fabricante = fabricante;
            this._modelo = modelo;
            this._alcance = alcance;
            this._cantAsientos = cantAsientos;
            this._costoXKm = costoXKm;
            this._tipoAeronave = tipoAeronave;
            this.Validar();
        }

        public void Validar()
        {
            this.ValidarFabricante();
            this.ValidarModelo();
            this.ValidarAlcance();
            this.ValidarCantAsientos();
            this.ValidarCostXKm();
        }

        public void ValidarFabricante()
        {
            if (string.IsNullOrWhiteSpace(this._fabricante))
            {
                throw new Exception("El valor no puede estar vacío o solo contener espacios.");
            }
        }


        public void ValidarModelo()
        {
            if (string.IsNullOrWhiteSpace(this._modelo))
            {
                throw new Exception("El valor no puede estar vacío o solo contener espacios.");
            }

        }

        public void ValidarAlcance()
        {
            if(this._alcance <= 0)
            {
                throw new Exception("El avión no puede tener un alcance negativo o igual a 0.");
            }

        }

        public void ValidarCantAsientos()
        {
            if (this._alcance <= 0)
            {
                throw new Exception("La cantidad de asientos no puede ser 0 o un número negativo");
            }

        }

        public void ValidarCostXKm()
        {
            if (this._alcance <= 0)
            {
                throw new Exception("El costo no puede ser 0 o un número negativo");
            }

        }

        public override string ToString()
        {
            return $"{this._modelo}";
        }
    }
}
