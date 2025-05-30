using Dominio;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Ocasional : Pasajero, IValidable
    {

        private bool _elegible;

        public bool elegible { get { return _elegible; } }
        public Ocasional(string nacionalidad, string docIdentidad, string nombre, string password, string email) : base(nacionalidad, docIdentidad, nombre, password, email)
        {
            this._elegible = CalcularElegibilidad();
        }

        public Ocasional() { }
        private bool CalcularElegibilidad()
        {
            //si el random cae en el numero limite o antes es elegible, si es mayor no es elegible
            int chance = 50;
            Random numeroRandom = new Random();
            int numero = numeroRandom.Next(0, 100);

            return numero <= chance;
        }

        //METODO POLIMORFICO QUE SOBRESCRIBE METODO DE CLASE BASE (PASAJERO)
        public override decimal CalcularPrecioEquipaje(TipoEquipaje tipoEquipaje)
        {
            switch (tipoEquipaje)
            {
                case TipoEquipaje.CABINA:

                    return 1.10m; //porcentaje de equipaje CABINA;
                    
                case TipoEquipaje.BODEGA:
                    return 1.20m; //porcentaje de equipaje CABINA;
                    
                default:
                    return 1m;
                    
            }


         
        }


        public override string ToString()
        {
            return base.ToString() + $" Elegible: {(_elegible ? "Sí" : "No")}";
        }
    }
}
