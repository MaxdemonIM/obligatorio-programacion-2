namespace Dominio
{
    public class Usuario
    {
        private string _password;
        protected string _email;
        private List<Administrador> _administradores;
        private List<Pasajero> _pasajeros;

        public List<Pasajero> Pasajeros { get { return _pasajeros; } }

        public List<Administrador> Administradores { get { return _administradores; } }

        public Usuario(string password, string email)
        {
            this._password = password;
            this._email = email;
            this._administradores = new List<Administrador> { };
            this._pasajeros = new List<Pasajero> { };
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

            char caracter = _password[i];
            while (i < _password.Length || contadorMayusculas == cantidadMinima && contadorMinusculas == cantidadMinima && contadorSimbolo == cantidadMinima && contadorNumeros == cantidadMinima)
            {
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
        }

        public void ValidarEmail()
        {
            if (string.IsNullOrWhiteSpace(this._email))
            {
                throw new Exception("El valor no puede estar vacío o solo contener espacios.");
            }
        }

        public override string ToString() 
        {
            
            return _pasajeros.ToString() + $"{_email}";
        }
    }


}