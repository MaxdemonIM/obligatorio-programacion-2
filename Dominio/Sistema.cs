using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Sistema
    {
        private static Sistema s_instancia;
        private List<Usuario> _usuarios;
        private List<Vuelo> _vuelos;
        private List<Ruta> _rutas;
        private List<Aeropuerto> _aeropuertos;
        private List<Pasaje> _pasajes;
        private List<Avion> _aviones;


        private Sistema()
        {
            this._usuarios = new List<Usuario>();
            this._vuelos = new List<Vuelo>();
            this._rutas = new List<Ruta>();
            this._aeropuertos = new List<Aeropuerto>();
            this._pasajes = new List<Pasaje>();
            this._aviones = new List<Avion>();
            this.PrecargarDatos();

        }


        //SINGLETON

        public static Sistema Instancia { get { if (Sistema.s_instancia == null)
                {
                    Sistema.s_instancia = new Sistema();
                }
                return s_instancia;
            }
        }

        //METODOS

        public List<Pasaje> ObtenerPasajeEntre(DateTime fechaInicial, DateTime fechaFinal)
        {
            List<Pasaje> listaDePasajes = new List<Pasaje>();

            foreach (Pasaje pasaje in _pasajes)
            {
                if (pasaje.Fecha >= fechaInicial && pasaje.Fecha <= fechaFinal)
                {
                    listaDePasajes.Add(pasaje);
                }
            }

            return listaDePasajes;
        }

        public Administrador EncontrarAdministradorPorNickname(string Nickname)
        {
            Administrador administradorEncontrado = null;

            foreach (Administrador admin in _usuarios)
            {
                if (this._usuarios.Contains(admin)) 
                {
                    administradorEncontrado = admin;
                }
            }

                return administradorEncontrado;
        }



        //-----------------------------PARTE A-------------------------

        //Primero recorremos la lista que tenemos GENERAL de usuarios (_usuarios). Esta lista, va tener objetos que son Administrador y otros que son pasajeros (ocasional u premium). A nosotros,
        //solo nos importa mostrar estos últimos.  Dentro del if, vamos a preguntar dos cosas. Primero, pregunta si un objeto es un Pasajero (o una subclase de el, ya sea ocasional o premium).
        //Si llega a ser un pasajero, se muestra en consola aplicando poliformismo, ya que usando el ToString se va ejecutar el de Premium u Ocasional respectivamente
       
        public void ListarPasajeros()
        {
            foreach (Usuario unUsuario in _usuarios)
            {
                if (unUsuario is Pasajero unPasajero) //Determina si el objeto usuario es ademas un Pasajero
                {

                    Console.WriteLine(unPasajero.ToString());
                }
            }
        }







        //----------------PARTE C----------DAR DE ALTA CLIENTE OCASIONAL-------------------

        //Primero vamos a recibir los datos del nuevo cliente a crear por parametros. Creamos la instancia de la clase Ocasional con el "Ocasional nuevo= new Ocasional", y por ultimo agregamos el
        //pasajero ocasional a la lista general de usuarios con el .add

        public void DarDeAltaClienteOcasional (string nombre, string nacionalidad, string docIdentidad, string password, string email)
        {

            Ocasional nuevo = new Ocasional(nacionalidad, docIdentidad, nombre, password, email);
            _usuarios.Add(nuevo);

        }



        //--------------PRECARGA------------------

        public void PrecargarDatos() {

            // --- Administradores ---
            _usuarios.Add(new Administrador("admin1", "admin1@correo.com", "1234"));
            _usuarios.Add(new Administrador("admin2", "admin2@correo.com", "1234"));



            // --- Pasajeros Premium ---
            _usuarios.Add(new Premium("Uruguaya", "45326780", "Lucía Alonso", "pass1", "lucia.alonso@correo.com"));
            _usuarios.Add(new Premium("Argentina", "37219845", "Carlos Pérez", "pass2", "carlos.perez@correo.com"));
            _usuarios.Add(new Premium("Chilena", "16230456", "Ana Torres", "pass3", "ana.torres@correo.com"));
            _usuarios.Add(new Premium("Brasileña", "82345672", "Miguel Souza", "pass4", "miguel.souza@correo.com"));
            _usuarios.Add(new Premium("Paraguaya", "5912345", "Valeria Díaz", "pass5", "valeria.diaz@correo.com"));

            // --- Pasajeros Ocasionales ---
            _usuarios.Add(new Ocasional("Uruguaya", "48321670", "Julia Rodríguez", "pass6", "julia.rodriguez@correo.com"));
            _usuarios.Add(new Ocasional("Uruguaya", "58233489", "Diego Fernández", "pass7", "diego.fernandez@correo.com"));
            _usuarios.Add(new Ocasional("Argentina", "39201563", "María Gómez", "pass8", "maria.gomez@correo.com"));
            _usuarios.Add(new Ocasional("Chileno", "18765432", "Pedro Morales", "pass9", "pedro.morales@correo.com"));
            _usuarios.Add(new Ocasional("Brasileño", "70984321", "Luciano Alves", "pass10", "luciano.alves@correo.com"));

        }


       

        /*         
         ******************************************************************************************************************************************************* 
         ******************************************************************************************************************************************************* 
         *******************************************************************************************************************************************************  
         *******************************************************************************************************************************************************  
         ******************************************* DOCUMENTAR PROMPT CUANDO HAGAMOS LA PRECARGA FINAL ********************************************************
         *******************************************************************************************************************************************************  
         *******************************************************************************************************************************************************  
         *******************************************************************************************************************************************************
         ******************************************************************************************************************************************************* 
         ******************************************************************************************************************************************************* 
         */


        //PRECARGA DATOS

        /*
        public void PrecargarDatos()
        {

            // --- Aviones ---
            _aviones = new List<Avion>
    {
        new Avion("Boeing", "737", 4000, 180, 5, "Comercial"),
        new Avion("Airbus", "A320", 4500, 200, 4.5m, "Comercial"),
        new Avion("Embraer", "E195", 3000, 120, 3.2m, "Regional"),
        new Avion("Bombardier", "CRJ900", 2800, 90, 2.8m, "Regional")
            };

            //---Pasaje---
            _pasajes = new List<Pasaje>();

            _pasajes.Add(new Pasaje(_vuelos[0],  _pasajeros[0], new DateTime(2025, 4, 7), "LIGHT", 150));
            _pasajes.Add(new Pasaje(_vuelos[1],  _pasajeros[1], new DateTime(2025, 4, 8), "CABINA", 160));
            _pasajes.Add(new Pasaje(_vuelos[2],  _pasajeros[2], new DateTime(2025, 4, 9), "BODEGA", 170));
            _pasajes.Add(new Pasaje(_vuelos[3],  _pasajeros[3], new DateTime(2025, 4, 10), "LIGHT", 180));
            _pasajes.Add(new Pasaje(_vuelos[4],  _pasajeros[4], new DateTime(2025, 4, 11), "CABINA", 190));
            _pasajes.Add(new Pasaje(_vuelos[5],  _pasajeros[5], new DateTime(2025, 4, 12), "BODEGA", 200));
            _pasajes.Add(new Pasaje(_vuelos[6],  _pasajeros[6], new DateTime(2025, 4, 13), "LIGHT", 210));
            _pasajes.Add(new Pasaje(_vuelos[7],  _pasajeros[7], new DateTime(2025, 4, 14), "CABINA", 220));
            _pasajes.Add(new Pasaje(_vuelos[8],  _pasajeros[8], new DateTime(2025, 4, 15), "BODEGA", 230));
            _pasajes.Add(new Pasaje(_vuelos[9],  _pasajeros[9], new DateTime(2025, 4, 16), "LIGHT", 240));
            _pasajes.Add(new Pasaje(_vuelos[10], _pasajeros[0], new DateTime(2025, 4, 17), "CABINA", 250));
            _pasajes.Add(new Pasaje(_vuelos[11], _pasajeros[1], new DateTime(2025, 4, 18), "BODEGA", 260));
            _pasajes.Add(new Pasaje(_vuelos[12], _pasajeros[2], new DateTime(2025, 4, 19), "LIGHT", 270));
            _pasajes.Add(new Pasaje(_vuelos[13], _pasajeros[3], new DateTime(2025, 4, 20), "CABINA", 280));
            _pasajes.Add(new Pasaje(_vuelos[14], _pasajeros[4], new DateTime(2025, 4, 21), "BODEGA", 290));
            _pasajes.Add(new Pasaje(_vuelos[15], _pasajeros[5], new DateTime(2025, 4, 22), "LIGHT", 300));
            _pasajes.Add(new Pasaje(_vuelos[16], _pasajeros[6], new DateTime(2025, 4, 23), "CABINA", 310));
            _pasajes.Add(new Pasaje(_vuelos[17], _pasajeros[7], new DateTime(2025, 4, 24), "BODEGA", 320));
            _pasajes.Add(new Pasaje(_vuelos[18], _pasajeros[8], new DateTime(2025, 4, 25), "LIGHT", 330));
            _pasajes.Add(new Pasaje(_vuelos[19], _pasajeros[9], new DateTime(2025, 4, 26), "CABINA", 340));
            _pasajes.Add(new Pasaje(_vuelos[20], _pasajeros[0], new DateTime(2025, 4, 27), "BODEGA", 350));
            _pasajes.Add(new Pasaje(_vuelos[21], _pasajeros[1], new DateTime(2025, 4, 28), "LIGHT", 360));
            _pasajes.Add(new Pasaje(_vuelos[22], _pasajeros[2], new DateTime(2025, 4, 29), "CABINA", 370));
            _pasajes.Add(new Pasaje(_vuelos[23], _pasajeros[3], new DateTime(2025, 4, 30), "BODEGA", 380));
            _pasajes.Add(new Pasaje(_vuelos[24], _pasajeros[4], new DateTime(2025, 5, 1), "LIGHT", 390));

            //--Vuelos--

            _vuelos = new List<Vuelo>();

            List<string> frecuencia = new List<string> { "lunes", "miércoles", "viernes" }; // guatteeeejeeeelll

            _vuelos.Add(new Vuelo(1001, _aviones[0], _rutas[0], frecuencia));
            _vuelos.Add(new Vuelo(1002, _aviones[1], _rutas[1], frecuencia));
            _vuelos.Add(new Vuelo(1003, _aviones[2], _rutas[2], frecuencia));
            _vuelos.Add(new Vuelo(1004, _aviones[3], _rutas[3], frecuencia));
            _vuelos.Add(new Vuelo(1005, _aviones[0], _rutas[4], frecuencia));
            _vuelos.Add(new Vuelo(1006, _aviones[1], _rutas[5], frecuencia));
            _vuelos.Add(new Vuelo(1007, _aviones[2], _rutas[6], frecuencia));
            _vuelos.Add(new Vuelo(1008, _aviones[3], _rutas[7], frecuencia));
            _vuelos.Add(new Vuelo(1009, _aviones[0], _rutas[8], frecuencia));
            _vuelos.Add(new Vuelo(1010, _aviones[1], _rutas[9], frecuencia));
            _vuelos.Add(new Vuelo(1011, _aviones[2], _rutas[10], frecuencia));
            _vuelos.Add(new Vuelo(1012, _aviones[3], _rutas[11], frecuencia));
            _vuelos.Add(new Vuelo(1013, _aviones[0], _rutas[12], frecuencia));
            _vuelos.Add(new Vuelo(1014, _aviones[1], _rutas[13], frecuencia));
            _vuelos.Add(new Vuelo(1015, _aviones[2], _rutas[14], frecuencia));
            _vuelos.Add(new Vuelo(1016, _aviones[3], _rutas[15], frecuencia));
            _vuelos.Add(new Vuelo(1017, _aviones[0], _rutas[16], frecuencia));
            _vuelos.Add(new Vuelo(1018, _aviones[1], _rutas[17], frecuencia));
            _vuelos.Add(new Vuelo(1019, _aviones[2], _rutas[18], frecuencia));
            _vuelos.Add(new Vuelo(1020, _aviones[3], _rutas[19], frecuencia));
            _vuelos.Add(new Vuelo(1021, _aviones[0], _rutas[20], frecuencia));
            _vuelos.Add(new Vuelo(1022, _aviones[1], _rutas[21], frecuencia));
            _vuelos.Add(new Vuelo(1023, _aviones[2], _rutas[22], frecuencia));
            _vuelos.Add(new Vuelo(1024, _aviones[3], _rutas[23], frecuencia));
            _vuelos.Add(new Vuelo(1025, _aviones[0], _rutas[24], frecuencia));
            _vuelos.Add(new Vuelo(1026, _aviones[1], _rutas[25], frecuencia));
            _vuelos.Add(new Vuelo(1027, _aviones[2], _rutas[26], frecuencia));
            _vuelos.Add(new Vuelo(1028, _aviones[3], _rutas[27], frecuencia));
            _vuelos.Add(new Vuelo(1029, _aviones[0], _rutas[28], frecuencia));
            _vuelos.Add(new Vuelo(1030, _aviones[1], _rutas[29], frecuencia));

        }
        */

    }
}
