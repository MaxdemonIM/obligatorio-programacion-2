using Dominio.Interfaces;

namespace Dominio
{
    public class Usuario : IValidable
    {
        private string _password;
        protected string _email;


        public Usuario(string password, string email)
        {
                this._password = password;
                this._email = email;
                this.Validar();
        }

        public void Validar()
        {
            this.ValidarPassword();
            this.ValidarEmail();
        }

        public void ValidarPassword()
        {
            int limiteDeCaracteres = 64;

            if (string.IsNullOrWhiteSpace(this._password))
            {
                throw new Exception("El valor no puede estar vacío o solo contener espacios.");
            }

            if(_password.Length > 64)
            {
                throw new Exception("La contraseña no puede ser mayor a 64 caracteres.");
            }

            int i = 0;
            int cantidadMinima = 1;

            int contadorMayusculas = 0;
            int contadorNumeros = 0;
            int contadorMinusculas = 0;
            int contadorSimbolo = 0;

            while (i < _password.Length || contadorMayusculas == cantidadMinima && contadorMinusculas == cantidadMinima && contadorSimbolo == cantidadMinima && contadorNumeros == cantidadMinima)
            {
                char caracter = _password[i];
                if (char.IsDigit(caracter))
                {
                    contadorNumeros++;
                }
                else if (char.IsLower(caracter))
                {
                    contadorMinusculas++;
                }
                else if (char.IsUpper(caracter))
                {
                    contadorMayusculas++;
                }
                else
                {
                    contadorSimbolo++;
                }
                i++;
            }
            if (contadorMayusculas < cantidadMinima || contadorMinusculas < cantidadMinima || contadorSimbolo < cantidadMinima || contadorNumeros < cantidadMinima)
            {
                throw new Exception("La contraseña debe contener al menos una mayúscula, una minúscula, un número y un símbolo.");
            }
        }

        public void ValidarEmail()
        {
            if (string.IsNullOrWhiteSpace(this._email))
            {
                throw new Exception("El email no puede estar vacío o solo contener espacios.");
            }

            bool tieneArroba = false;
            bool tienePunto = false;

            foreach (char c in this._email)
            {
                if (c == '@')
                {
                    tieneArroba = true;
                }
                else if (tieneArroba && c == '.')
                {
                    tienePunto = true;
                }
            }

            if (!tieneArroba || !tienePunto)
            {
                throw new Exception("El correo debe contener '@' y un '.' después del '@'.");
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Usuario)) return false;
            Usuario otro = (Usuario)obj;
            return this._email.Equals(otro._email);
        }

        public override string ToString() 
        {
            return this.ToString() + $"{_email}";
        }

    }


}