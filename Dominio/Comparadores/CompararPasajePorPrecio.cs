﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comparadores
{
    //NOS SIRVE PARA ORDENAR LOS PASAJES POR PRECIO DESCENDENTE PARA EL CLIENTE
    public class CompararPasajePorPrecio : IComparer<Pasaje>
    {
        public int Compare(Pasaje? este, Pasaje? otro)
        {
            return este.CalcularPrecioDelPasaje().CompareTo(otro.CalcularPrecioDelPasaje()) * -1;
        }
    }
}
