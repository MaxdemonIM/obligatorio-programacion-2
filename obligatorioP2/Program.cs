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
            Console.WriteLine();

            int seleccion = -1;
            while (seleccion != 0)
            {
                seleccion = SolicitarInt("Seleccione que acción quiere realizar: \n" +

                "1 - Listar Pasajeros. \n" +
                "2 - Listar vuelos respecto a un código IATA. \n" +
                "3 - Dar de alta cliente ocasional. \n" +
                "4 - Listar Pasajes en un rango de fechas. \n" +
                "(0 para salir)", 8, 0);

                switch (seleccion)
                {
                        //PARTE D
                    case 4:
                        ListarPasajesSegunRangoDeFechas();
                        break;
                    case 3:
                        //PARTE C
                        DarDeAltaClienteOcasional(); //estamos llamando al metodo de program (el de sistema se llama igual, pero tiene parametros, por eso solo ponemos acá "DarDeAltaClienteOcasional" y no "sistema.DarDeAltaClienteOcasional".
                        break;
                        //PARTE B
                    case 2:
                        ListarVuelosQueIncluyenIATACode();
                        break;
                    case 1:
                        //PARTE A
                        ListarPasajeros();
                        break;
                    case 0:
                        Console.WriteLine("Gracias por utilizar nuestro sistema, Nos vemos pronto!");
                        break;
                    default:
                        Console.WriteLine("Seleccion invalida");
                        break;
                }
            }


            //PARTE B

            static void ListarVuelosQueIncluyenIATACode()
            {
                Console.WriteLine("Ingrese el código IATA de el aeropuerto del cual quiere conocer los vuelos:");
                string IATACode = Console.ReadLine().ToUpper().Trim();

                sistema.ListarVuelosPorAeropuerto(IATACode);
            }


            //PARTE A

            static void ListarPasajeros() 
            {
                sistema.ListarPasajeros();
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
                    Console.WriteLine(e.Message);
                }
            }


            //PARTE D (SOLICITAR LAS DOS FECHAS).
            static void ListarPasajesSegunRangoDeFechas()
            {

                DateTime fechaInicial = SolicitarDateTime("Ingrese la fecha inicial (formato dd-MM-yyyy).");

                DateTime fechaFinal = SolicitarDateTime("Ingrese la final fecha (formato dd-MM-yyyy).");

                List<Pasaje> pasajes = sistema.ObtenerPasajeEntre(fechaInicial, fechaFinal);

                if (pasajes.Count == 0)
                {
                    Console.WriteLine("No hay pasajes expedidos entre las fechas ingresadas");
                }
                else
                {
                    Console.WriteLine($" Pasajes entre  {fechaInicial} y {fechaFinal}:\n");

                    foreach (Pasaje unPasaje in pasajes)
                    {
                        Console.WriteLine(unPasaje);
                    }
                }
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