using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Ocasional : Pasajero
    {

        private bool _elegible;

        public Ocasional(string nacionalidad, string docIdentidad, string nombre, string password, string email) : base(nacionalidad, docIdentidad, nombre, password, email)
        {
            this._elegible = CalcularElegibilidad();
        }


        private bool CalcularElegibilidad()
        {
            //si el random cae en el numero limite o antes es elegible, si es mayor no es elegible
            int chance = 50;
            Random numeroRandom = new Random();
            int numero = numeroRandom.Next(0, 100);

            return numero <= chance;
        }


        public override string ToString()
        {
            return base.ToString() + $" Elegible: {(_elegible ? "Sí" : "No")}";
        }
    }
}
