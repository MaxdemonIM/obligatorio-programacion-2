﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Aeropuerto
    {

        private string _IATACode;
        private string _ciudad;
        private decimal _costoOpp;
        private decimal _costoTasas;

        public string IATACode
        {
            get { return _IATACode; }
        }

        public decimal CostoOpp { get { return _costoOpp; } }

        public Aeropuerto(string IATACode, string ciudad, decimal costoOpp, decimal costoTasas)
        {
            IATACode = IATACode.ToUpper(); //Forzamos que este en mayúscula desde un comienzo para no hacer validación. 
            this._IATACode = IATACode;
            this._ciudad = ciudad;
            this._costoOpp = costoOpp;
            this._costoTasas = costoTasas;
            Validar();

        }

        public void Validar()
        {
            this.ValidarCiudad();
            this.ValidarCostoOpp();
            this.ValidarCostoTasas();
            ValidarIATA(this.IATACode);
        }


        public static void ValidarIATA(string IATA)
        {
            if (IATA.Length != 3)
            {
                throw new Exception("El código IATA debe tener exactamente 3 caracteres.");
            }

            foreach (char caracter in IATA)
            {
                if (!char.IsLetter(caracter))
                {
                    throw new Exception("Solo ingresar letras en el código IATA, no números, espacios en blanco ni simbolos.");
                }
            }
        }

        public void ValidarCiudad()
        {
            if (string.IsNullOrWhiteSpace(this._ciudad))
            {
                throw new Exception("La el nombre de la ciudad no puede estar vacía o solo contener espacios.");
            }

        }

        public void ValidarCostoOpp()
        {
            if (this._costoOpp <= 0)
            {
                throw new Exception("El costo de operación no puede tener un valor negativo o igual a 0.");
            }

        }

        public void ValidarCostoTasas()
        {
            if (this._costoTasas <= 0)
            {
                throw new Exception("Las tasas no pueden ser 0 o un número negativo");
            }
        }



        public override string ToString()
        {
            return $"{_IATACode}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Aeropuerto)) return false;
            Aeropuerto otro = (Aeropuerto)obj;
            return this.IATACode.Equals(otro.IATACode);
        }

    }
}
