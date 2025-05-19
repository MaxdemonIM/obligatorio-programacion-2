using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Administrador : Usuario, IValidable
    {

        private string _nickname;

        //public string Nickname { get { return _nickname; } }

        public Administrador(string nickname, string password, string email) : base(password, email)
        {
            this._nickname = nickname;
            this.Validar();
        }

        public void Validar()
        {
            this.ValidarNickname();
        }


        public void ValidarNickname()
        {
            if (string.IsNullOrWhiteSpace(this._nickname))
            {
                throw new Exception("El valor no puede estar vacío o solo contener espacios.");
            }
        }

    }
}
