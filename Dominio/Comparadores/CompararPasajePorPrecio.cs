using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comparadores
{
    public class CompararPasajePorPrecio : IComparer<Pasaje>
    {
        public int Compare(Pasaje? x, Pasaje? y)
        {
            return x.CalcularPrecioDelPasaje().CompareTo(y.CalcularPrecioDelPasaje());
        }
    }
}
