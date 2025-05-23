using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Pasajero : Usuario , IValidable
    {
        private string _nacionalidad;
        private string _docIdentidad;
        private string _nombre;

        public string DocIdentidad { get { return _docIdentidad; } }

        public string Nombre { get { return _nombre; } }


        public Pasajero(string nacionalidad, string docIdentidad, string nombre, string password, string email) : base(password, email)
        {
            this._nacionalidad = nacionalidad;
            this._docIdentidad = docIdentidad;
            this._nombre = nombre;
            this.Validar();
        }

        public void Validar()
        {
            this.ValidarNombre();
            this.ValidarNacionalidad();
            this.ValidarDocumentoDeIdentidad();
        }

        public void ValidarNombre()
        {
            if (string.IsNullOrWhiteSpace(this._nombre))
            {
                throw new Exception("El nombre no puede estar vacío ni contener espacios en blanco.");
            }
        }

        public void ValidarNacionalidad()
        {
            if (string.IsNullOrWhiteSpace(this._nacionalidad))
            {

                throw new Exception("La nacionalidad no puede estar vacío ni contener espacios en blanco.");
            }
        }

        public void ValidarDocumentoDeIdentidad()
        {
            if (string.IsNullOrWhiteSpace(this._docIdentidad))
            {
                throw new Exception("El documento de identidad no puede estar vacío ni contener espacios en blanco.");
            }

            const int LIMITE_DOCUMENTO = 15;

            if (_docIdentidad.Length >= LIMITE_DOCUMENTO)
            {
                throw new Exception($"El documento de identidad debe tener menos de {LIMITE_DOCUMENTO} caracteres.");
            }


            for (int i = 0; i < this._docIdentidad.Length; i++)
            {
                char caracter = this._docIdentidad[i];

                if (caracter < '0' || caracter > '9')
                {
                    throw new Exception("El documento de identidad debe contener solo números.");
                }
            }
        }

        // Metodo ABSTRACT POLIMORFICO para calculo pasaje. Lo hacemos acá ya que esta es la clase base, y se va aplicar override en las subclases.
        // Le pasamos por parámetro Vuelo, que recibe costo por asiento (precio base) y tipo de equipaje que es lo que necesitamos para aplicar las distintas casuisticas de la letra.

        public abstract decimal CalcularPrecioPasajeSegunTipoDeCliente(Vuelo vuelo, TipoEquipaje tipoEquipaje);


 

        public override string ToString()
        {
            return $"Nombre: {this._nombre} | Email: {this._email} | Nacionalidad: {this._nacionalidad} | Cedula Identidad: {this._docIdentidad}";
        }
    }
}
