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

        public int Puntos { get { return _puntos; } }

        public Premium(string nacionalidad, string docIdentidad, string nombre, string password, string email) : base(nacionalidad, docIdentidad, nombre, password, email)
        {
            this._puntos = 0;
        }

        public override string ToString()
        {
            return base.ToString() + $" Puntos: {_puntos}";
        }
    }



}

