using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Administrador : Usuario
    {
        static Sistema sistema = Sistema.Instancia;

        private string _nickname;

        public Administrador(string nickname, string password, string email) : base (password, email)
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


        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Administrador)) return false;
             Administrador otro = (Administrador)obj;
            return this._nickname.Equals(otro._nickname);
        }

        /*

       //Podemos llegar a necesitar



       public void ModificarPuntosCliente()
       {

       }

       */
    }
}
