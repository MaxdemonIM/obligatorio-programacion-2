using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Premium : Pasajero, IValidable
    {
        private int _puntos;

        public int Puntos { get { return _puntos; } set { _puntos = value; } }

        public Premium(string nacionalidad, string docIdentidad, string nombre, string password, string email) : base(nacionalidad, docIdentidad, nombre, password, email)
        {
            this._puntos = 0;
        }


        public void Validar()
        {
            base.Validar();
            this.ValidarPuntos(this._puntos);
        }
        public override string ToString()
        {
            return base.ToString() + $" Puntos: {_puntos}";
        }

        //METODO POLIMORFICO QUE SOBRESCRIBE METODO DE CLASE BASE (PASAJERO)
        public override decimal CalcularPrecioEquipaje(TipoEquipaje tipoEquipaje)
        {
           
            switch (tipoEquipaje)
            {
                case TipoEquipaje.BODEGA:

                    return 1.05m; //porcentaje de equipaje BODEGA;

                default:
                    return 1m;

            }
        }

        public void ValidarPuntos(int puntos)
        {
            if (puntos < 0)
            {
                throw new Exception("Los puntos asignados no pueden ser negativos.");
            }



        }


    }



}

