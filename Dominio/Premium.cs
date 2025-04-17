using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Premium : Pasajero
    {
        private int _puntos;

        public int Puntos { get { return _puntos; } }

        public Premium(string nacionalidad, string docIdentidad, string nombre, string password, string email) : base(nacionalidad, docIdentidad, nombre, password, email)
        {
            this._puntos = 0;
            this.Validar();
        }

        public override string ToString()
        {
            return base.ToString() + $" Puntos: {_puntos}";
        }
    }



}

