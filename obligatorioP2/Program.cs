using System.Globalization;
using Dominio;
using Microsoft.VisualBasic;


namespace obligatorioP2
{
    internal class Program
    {
        static Sistema sistema = Sistema.Instancia;

        static void Main(string[] args)
        {
            Console.WriteLine("obligatorio p2");
            Console.WriteLine("Menu principal");
            Console.WriteLine(
                "1 - agregar vuelo \n" +
                "2 - Listar Pasajes en un rango de fechas \n" +
                "3 - Listar Pasajeros \n" +
                "4 - Dar de alta cliente ocasional.");

            int seleccion = -1;
            while (seleccion != 0)
            {
                seleccion = SolicitarInt("Seleccione que acción quiere realizar: " +
                "\n(0 para salir)", 8, 0);
                Console.Clear();

                switch (seleccion)
                {
                    case 4:
                        //PARTE C
                        DarDeAltaClienteOcasional(); //estamos llamando al metodo de program (el de sistema se llama igual, pero tiene parametros, por eso solo ponemos acá "DarDeAltaClienteOcasional" y no "sistema.DarDeAltaClienteOcasional".
                        break;
                    case 3:
                        //PARTE A
                        sistema.ListarPasajeros();
                        break;
                    case 2:
                        ListarPasajesSegunRangoDeFechas();
                        break;
                    case 1:
                        AgregarVuelo();
                        break;
                    case 0:
                        Console.WriteLine("Gracias por utilizar nuestro sistema, Nos vemos pronto!");
                        break;
                    default:
                        Console.WriteLine("Seleccion invalida");
                        break;
                }
            }

            //PARTE C --- DAR DE ALTA CLIENTE OCASIONAL

            static void DarDeAltaClienteOcasional()
            {

                Console.WriteLine("Alta de cliente Ocasional");
                Console.WriteLine("Ingrese nombre completo:");
                string nombre = Console.ReadLine();

                Console.WriteLine("Ingrese la nacionalidad:");
                string nacionalidad = Console.ReadLine();

                Console.WriteLine("Ingrese el documento de identidad (7 u 8 digitos, solo números):");
                string docIdentidad = Console.ReadLine();

                Console.WriteLine("Ingrese email:");
                string email = Console.ReadLine();

                Console.WriteLine("Ingrese contraseña:");
                string password = Console.ReadLine();


                try
                {
                    sistema.DarDeAltaClienteOcasional(nombre, nacionalidad, docIdentidad, password, email);
                    Console.WriteLine("\n Cliente ocasional dado de alta exitosamente.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error al dar de alta el cliente:");
                }
            }







            static void AgregarVuelo()
            {
                Console.Clear();
                Console.WriteLine("Agregar vuelo");
                Console.WriteLine("0 para salir");

                int seleccion = -1;
                while (seleccion != 0)
                {

                    int numViaje = SolicitarInt("Número de vuelo", 9999, 0);
                    Console.Clear();

                    Avion avion = new Avion("xa", "ax", 1, 1, 1, "a");
                    Ruta ruta = new Ruta(null, null, 1);

                    List<string> frecuencia = CrearListaFrecuencia();
                    Vuelo vuelo = new Vuelo(numViaje, avion, ruta, frecuencia);
                    vuelo.ToString();
                }
            }

            //PARTE D (SOLICITAR LAS DOS FECHAS).
            static void ListarPasajesSegunRangoDeFechas()
            {
                Console.Clear();
                DateTime fechaInicial = SolicitarDateTime("Ingrese la fecha inicial (formato dd-MM-yyyy).");
                Console.Clear();

                DateTime fechaFinal = SolicitarDateTime("Ingrese la final fecha (formato dd-MM-yyyy).");
                Console.Clear();

                List<Pasaje> pasajes = sistema.ObtenerPasajeEntre(fechaInicial, fechaFinal);

                if (pasajes.Count == 0)
                {
                    Console.WriteLine("No hay pasajes expedidos entre las fechas ingresadas");
                }
                else
                {
                    foreach (Pasaje pasajeEncontrado in pasajes)
                    {
                        Console.WriteLine($"Pasajes entre {fechaInicial} y {fechaFinal}: " + pasajeEncontrado);
                    }
                }
            }

            static List<string> CrearListaFrecuencia()
            {

                Console.WriteLine("Ingrese la frecuencia del vuelo (formato esperado: lunes)");
                bool ingresoSalir = false;
                List<string> frecuencia = new List<string>();
                int cantidadMaximaDeDiasIngresables = 7;
                int i = 0;
                while (!ingresoSalir && i < cantidadMaximaDeDiasIngresables)
                {
                    Console.WriteLine();

                    string stringIngresado = Console.ReadLine().ToLower().Trim();
                    if (stringIngresado != "salir")

                    {
                        frecuencia.Add(stringIngresado);
                    }
                    else
                    {
                        ingresoSalir = true;
                    }
                    i++;
                }
                return frecuencia;
            }


            //Metodos de solicitar

            static decimal SolicitarDecimal(string mensaje, decimal maximo, decimal minimo)
            {
                decimal retorno = -1;
                bool seleccionCorrecta = false;
                while (!seleccionCorrecta)
                {
                    try
                    {
                        Console.WriteLine(mensaje);
                        Console.WriteLine("El maximo permitido es " + maximo
                            + " y el minimo es " + minimo);
                        string seleccionString = Console.ReadLine();
                        retorno = decimal.Parse(seleccionString);
                        if (retorno < minimo || retorno > maximo)
                        {
                            Console.WriteLine("Numero incorrecto");
                        }
                        else
                        {
                            seleccionCorrecta = true;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Solo se aceptan numeros");
                    }
                }
                return retorno;
            }


            //Para menú e ingresar datos.
            static int SolicitarInt(string mensaje, int maximo, int minimo)
            {
                int retorno = -1;
                bool seleccionCorrecta = false;
                while (!seleccionCorrecta)
                {
                    try
                    {
                        Console.WriteLine(mensaje);
                        Console.WriteLine("El maximo permitido es " + maximo
                            + " y el minimo es " + minimo);
                        string seleccionString = Console.ReadLine();
                        retorno = int.Parse(seleccionString);
                        if (retorno < minimo || retorno > maximo)
                        {
                            Console.WriteLine("Numero incorrecto");
                        }
                        else
                        {
                            seleccionCorrecta = true;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Solo se aceptan numeros");
                    }
                }
                return retorno;
            }

            static DateTime SolicitarDateTime(string textoFecha)    //para cuando tenga que solicitarse dos fechas en la PARTE D.Se solicita la fecha una y otra vez en loop hasta que se ingrese de forma correcta.
            {
                bool esCorrecto = false;
                DateTime retorno = DateTime.Now;

                while (!esCorrecto)
                {
                    try
                    {
                        Console.WriteLine(textoFecha);
                        retorno = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", new CultureInfo("es-ES"));
                        esCorrecto = true;
                    }
                    catch (Exception e)
                    {

                        Console.WriteLine("Formato de fecha incorrecto. Ingrese el formato dd-MM-yyyy.");
                    }

                }
                return retorno;
            }
        }
    }
}