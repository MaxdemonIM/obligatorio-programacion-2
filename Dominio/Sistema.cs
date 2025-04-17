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

        /* METODO QUE VAMOS UTILIZAR MAS ADELANTE POSIBLEMENTE 
        public Administrador EncontrarAdministradorPorNickname(string Nickname)
        {
            Administrador administradorEncontrado = null;

            foreach (Usuario unUsuario in _usuarios)
            {
                if (unUsuario is Administrador unAdministrador && unAdministrador.Nickname == Nickname)
                {
                        administradorEncontrado = unAdministrador;
                }
            }
                return administradorEncontrado;
        }*/


        //METODOS AUXILIARES PARA VALIDACION DE ALTA EN PARTE A:
        public bool ExistePasajeroCon(string unDocIdentidad)
        {
            foreach (Usuario unUsuario in _usuarios)
            {
                if (unUsuario is Pasajero unPasajero && unPasajero.DocIdentidad == unDocIdentidad)
                {
                    return true;
                }
            }
            return false;
        }



        //-------- PARTE A --------

        //Primero recorremos la lista que tenemos GENERAL de usuarios (_usuarios). Esta lista, va tener objetos que son Administrador y otros que son pasajeros (ocasional u premium). A nosotros,
        //solo nos importa mostrar estos últimos.  Dentro del if, vamos a preguntar dos cosas. Primero, pregunta si un objeto es un Pasajero (o una subclase de el, ya sea ocasional o premium).
        //Si llega a ser un pasajero, se muestra en consola aplicando poliformismo, ya que usando el ToString se va ejecutar el de Premium u Ocasional respectivamente

        public void ListarPasajeros()
        {
            foreach (Usuario unUsuario in _usuarios)
            {
                if (unUsuario is Pasajero unPasajero) //Determina si el objeto usuario es ademas un Pasajero
                {
                    Console.WriteLine(unPasajero);
                }
            }
        }


        //-------- PARTE B -------- LISTAR VUELOS INCLUYEN UN CODIGO DE AEROPUERTO

        public void ListarVuelosPorAeropuerto (string codigo)
        {
            foreach (Vuelo unVuelo in _vuelos)
            {
                string salida = unVuelo.Ruta.AeropuertoSalida.IATACode;
                string llegada = unVuelo.Ruta.AeropuertoLlegada.IATACode;

                if (salida == codigo || llegada == codigo)
                {
                    Console.WriteLine($"Vuelo: {unVuelo.NumVuelo} | Modelo: {unVuelo.Avion.Modelo} | Ruta: {salida}-{llegada} | Frecuencia: {unVuelo.Frecuencia}");
                }
            }
        }



        //-------- PARTE C --------DAR DE ALTA CLIENTE OCASIONAL-------------------

        //Primero vamos a recibir los datos del nuevo cliente a crear por parametros. Creamos la instancia de la clase Ocasional con el "Ocasional nuevo= new Ocasional", y por ultimo agregamos el
        //pasajero ocasional a la lista general de usuarios con el .add

        public void DarDeAltaClienteOcasional (string nombre, string nacionalidad, string docIdentidad, string password, string email)
        {
            Ocasional nuevo = new Ocasional(nacionalidad, docIdentidad, nombre, password, email);
            
            if (this.ExistePasajeroCon(docIdentidad))
            {
                throw new Exception("Pasajero con ese documento de identidad ya registrado.");
            }
            _usuarios.Add(nuevo);
        }


        //-------- PARTE D --------

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


        /*--------------PRECARGA------------------
        * PROMT: Necesito que hagas una precarga de 5 clientes premium, 5 ocasionales y 2 administradores (que sean lo mas realistas posible (cedulas coherentes, etc). También, de 4 aviones, 
        * 20 aeropuertos, 30 rutas, 30 vuelos y 25 pasajes. El formato a utilizar debe ser siempre _nombreDeLista.Add(Datos)*/

        public void PrecargarDatos()
        {
            // PRECARGA COMPLETA DEL SISTEMA

            // --- Administradores ---
            _usuarios.Add(new Administrador("admin1", "Admin123!", "admin1@correo.com"));
            _usuarios.Add(new Administrador("admin2", "ClaveSegura1$", "admin2@correo.com"));


            // --- Pasajeros Premium ---
            _usuarios.Add(new Premium("Uruguaya", "48123456", "Lucía Alonso", "Pass123$", "lucia.alonso@correo.com"));
            _usuarios.Add(new Premium("Argentina", "37111222", "Carlos Pérez", "Pass456$", "carlos.perez@correo.com"));
            _usuarios.Add(new Premium("Chilena", "16123456", "Ana Torres", "Pass789$", "ana.torres@correo.com"));
            _usuarios.Add(new Premium("Brasileña", "82112233", "Miguel Souza", "Pass321$", "miguel.souza@correo.com"));
            _usuarios.Add(new Premium("Paraguaya", "5912345", "Valeria Díaz", "Pass654$", "valeria.diaz@correo.com"));


            // --- Pasajeros Ocasionales ---
            _usuarios.Add(new Ocasional("Uruguaya", "48321670", "Julia Rodríguez", "Clave123$", "julia.rodriguez@correo.com"));
            _usuarios.Add(new Ocasional("Uruguaya", "58233489", "Diego Fernández", "Clave456$", "diego.fernandez@correo.com"));
            _usuarios.Add(new Ocasional("Argentina", "39201563", "María Gómez", "Clave789$", "maria.gomez@correo.com"));
            _usuarios.Add(new Ocasional("Chileno", "18765432", "Pedro Morales", "Clave321$", "pedro.morales@correo.com"));
            _usuarios.Add(new Ocasional("Brasileño", "70984321", "Luciano Alves", "Clave654$", "luciano.alves@correo.com"));


            // === AVIONES ===

            _aviones.Add(new Avion("Boeing", "737 MAX", 5000, 160, 15, "Pasajeros"));
            _aviones.Add(new Avion("Airbus", "A320neo", 4800, 180, 14, "Pasajeros"));
            _aviones.Add(new Avion("Embraer", "E195-E2", 4600, 132, 12, "Pasajeros"));
            _aviones.Add(new Avion("Bombardier", "CS300", 4700, 120, 11, "Pasajeros"));


            // === AEROPUERTOS ===
            _aeropuertos.Add(new Aeropuerto("MVD", "Montevideo", 150, 40));
            _aeropuertos.Add(new Aeropuerto("EZE", "Buenos Aires", 160, 42));
            _aeropuertos.Add(new Aeropuerto("GRU", "Sao Paulo", 170, 45));
            _aeropuertos.Add(new Aeropuerto("SCL", "Santiago", 155, 38));
            _aeropuertos.Add(new Aeropuerto("ASU", "Asunción", 140, 36));
            _aeropuertos.Add(new Aeropuerto("LIM", "Lima", 175, 47));
            _aeropuertos.Add(new Aeropuerto("BOG", "Bogotá", 180, 50));
            _aeropuertos.Add(new Aeropuerto("MEX", "Ciudad de México", 200, 55));
            _aeropuertos.Add(new Aeropuerto("MIA", "Miami", 250, 60));
            _aeropuertos.Add(new Aeropuerto("JFK", "Nueva York", 270, 70));
            _aeropuertos.Add(new Aeropuerto("MAD", "Madrid", 300, 65));
            _aeropuertos.Add(new Aeropuerto("CDG", "París", 310, 68));
            _aeropuertos.Add(new Aeropuerto("LHR", "Londres", 320, 70));
            _aeropuertos.Add(new Aeropuerto("FRA", "Fráncfort", 315, 67));
            _aeropuertos.Add(new Aeropuerto("AMS", "Ámsterdam", 305, 66));
            _aeropuertos.Add(new Aeropuerto("BCN", "Barcelona", 295, 64));
            _aeropuertos.Add(new Aeropuerto("LAX", "Los Ángeles", 280, 62));
            _aeropuertos.Add(new Aeropuerto("ORD", "Chicago", 265, 58));
            _aeropuertos.Add(new Aeropuerto("SYD", "Sídney", 350, 75));
            _aeropuertos.Add(new Aeropuerto("DXB", "Dubái", 360, 78));



            // === RUTAS ===
            _rutas.Add(new Ruta(_aeropuertos[0], _aeropuertos[1], 800));
            _rutas.Add(new Ruta(_aeropuertos[2], _aeropuertos[3], 1000));
            _rutas.Add(new Ruta(_aeropuertos[4], _aeropuertos[5], 1200));
            _rutas.Add(new Ruta(_aeropuertos[6], _aeropuertos[7], 1400));
            _rutas.Add(new Ruta(_aeropuertos[8], _aeropuertos[9], 1600));
            _rutas.Add(new Ruta(_aeropuertos[10], _aeropuertos[11], 1800));
            _rutas.Add(new Ruta(_aeropuertos[12], _aeropuertos[13], 2000));
            _rutas.Add(new Ruta(_aeropuertos[14], _aeropuertos[15], 2200));
            _rutas.Add(new Ruta(_aeropuertos[16], _aeropuertos[17], 2400));
            _rutas.Add(new Ruta(_aeropuertos[18], _aeropuertos[19], 2600));
            _rutas.Add(new Ruta(_aeropuertos[1], _aeropuertos[2], 700));
            _rutas.Add(new Ruta(_aeropuertos[3], _aeropuertos[4], 800));
            _rutas.Add(new Ruta(_aeropuertos[5], _aeropuertos[6], 900));
            _rutas.Add(new Ruta(_aeropuertos[7], _aeropuertos[8], 1000));
            _rutas.Add(new Ruta(_aeropuertos[9], _aeropuertos[10], 1100));
            _rutas.Add(new Ruta(_aeropuertos[11], _aeropuertos[12], 1200));
            _rutas.Add(new Ruta(_aeropuertos[13], _aeropuertos[14], 1300));
            _rutas.Add(new Ruta(_aeropuertos[15], _aeropuertos[16], 1400));
            _rutas.Add(new Ruta(_aeropuertos[17], _aeropuertos[18], 1500));
            _rutas.Add(new Ruta(_aeropuertos[19], _aeropuertos[0], 1600));
            _rutas.Add(new Ruta(_aeropuertos[0], _aeropuertos[2], 1000));
            _rutas.Add(new Ruta(_aeropuertos[4], _aeropuertos[6], 1200));
            _rutas.Add(new Ruta(_aeropuertos[8], _aeropuertos[10], 1400));
            _rutas.Add(new Ruta(_aeropuertos[12], _aeropuertos[14], 1600));
            _rutas.Add(new Ruta(_aeropuertos[16], _aeropuertos[18], 1800));
            _rutas.Add(new Ruta(_aeropuertos[1], _aeropuertos[3], 900));
            _rutas.Add(new Ruta(_aeropuertos[5], _aeropuertos[7], 1100));
            _rutas.Add(new Ruta(_aeropuertos[9], _aeropuertos[11], 1300));
            _rutas.Add(new Ruta(_aeropuertos[13], _aeropuertos[15], 1500));
            _rutas.Add(new Ruta(_aeropuertos[17], _aeropuertos[19], 1700));




            // --- Vuelos ---

            _vuelos.Add(new Vuelo(2001, _aviones[0], _rutas[0], "lunes"));
            _vuelos.Add(new Vuelo(2002, _aviones[0], _rutas[1], "martes"));
            _vuelos.Add(new Vuelo(2003, _aviones[0], _rutas[2], "miércoles"));
            _vuelos.Add(new Vuelo(2004, _aviones[0], _rutas[3], "jueves"));
            _vuelos.Add(new Vuelo(2005, _aviones[0], _rutas[4], "viernes"));
            _vuelos.Add(new Vuelo(2006, _aviones[0], _rutas[5], "sábado"));
            _vuelos.Add(new Vuelo(2007, _aviones[0], _rutas[6], "domingo"));
            _vuelos.Add(new Vuelo(2008, _aviones[1], _rutas[7], "lunes"));
            _vuelos.Add(new Vuelo(2009, _aviones[1], _rutas[8], "martes"));
            _vuelos.Add(new Vuelo(2010, _aviones[1], _rutas[9], "miércoles"));
            _vuelos.Add(new Vuelo(2011, _aviones[1], _rutas[10], "jueves"));
            _vuelos.Add(new Vuelo(2012, _aviones[1], _rutas[11], "viernes"));
            _vuelos.Add(new Vuelo(2013, _aviones[1], _rutas[12], "sábado"));
            _vuelos.Add(new Vuelo(2014, _aviones[1], _rutas[13], "domingo"));
            _vuelos.Add(new Vuelo(2015, _aviones[2], _rutas[14], "lunes"));
            _vuelos.Add(new Vuelo(2016, _aviones[2], _rutas[15], "martes"));
            _vuelos.Add(new Vuelo(2017, _aviones[2], _rutas[16], "miércoles"));
            _vuelos.Add(new Vuelo(2018, _aviones[2], _rutas[17], "jueves"));
            _vuelos.Add(new Vuelo(2019, _aviones[2], _rutas[18], "viernes"));
            _vuelos.Add(new Vuelo(2020, _aviones[2], _rutas[19], "sábado"));
            _vuelos.Add(new Vuelo(2021, _aviones[3], _rutas[20], "domingo"));
            _vuelos.Add(new Vuelo(2022, _aviones[3], _rutas[21], "lunes"));
            _vuelos.Add(new Vuelo(2023, _aviones[3], _rutas[22], "martes"));
            _vuelos.Add(new Vuelo(2024, _aviones[3], _rutas[23], "miércoles"));
            _vuelos.Add(new Vuelo(2025, _aviones[3], _rutas[24], "jueves"));
            _vuelos.Add(new Vuelo(2026, _aviones[3], _rutas[25], "viernes"));
            _vuelos.Add(new Vuelo(2027, _aviones[3], _rutas[26], "sábado"));
            _vuelos.Add(new Vuelo(2028, _aviones[3], _rutas[27], "domingo"));
            _vuelos.Add(new Vuelo(2029, _aviones[3], _rutas[28], "lunes"));
            _vuelos.Add(new Vuelo(2030, _aviones[3], _rutas[29], "martes"));


            // --- Pasajes ---
            _pasajes.Add(new Pasaje(_vuelos[0], (Pasajero)_usuarios[2], new DateTime(2025, 4, 7), "LIGHT", 150));
            _pasajes.Add(new Pasaje(_vuelos[1], (Pasajero)_usuarios[3], new DateTime(2025, 4, 8), "CABINA", 160));
            _pasajes.Add(new Pasaje(_vuelos[2], (Pasajero)_usuarios[4], new DateTime(2025, 4, 9), "BODEGA", 170));
            _pasajes.Add(new Pasaje(_vuelos[3], (Pasajero)_usuarios[5], new DateTime(2025, 4, 10), "LIGHT", 180));
            _pasajes.Add(new Pasaje(_vuelos[4], (Pasajero)_usuarios[6], new DateTime(2025, 4, 11), "CABINA", 190));
            _pasajes.Add(new Pasaje(_vuelos[5], (Pasajero)_usuarios[7], new DateTime(2025, 4, 12), "BODEGA", 200));
            _pasajes.Add(new Pasaje(_vuelos[6], (Pasajero)_usuarios[8], new DateTime(2025, 4, 13), "LIGHT", 210));
            _pasajes.Add(new Pasaje(_vuelos[7], (Pasajero)_usuarios[9], new DateTime(2025, 4, 14), "CABINA", 220));
            _pasajes.Add(new Pasaje(_vuelos[8], (Pasajero)_usuarios[2], new DateTime(2025, 4, 15), "BODEGA", 230));
            _pasajes.Add(new Pasaje(_vuelos[9], (Pasajero)_usuarios[3], new DateTime(2025, 4, 16), "LIGHT", 240));
            _pasajes.Add(new Pasaje(_vuelos[10], (Pasajero)_usuarios[4], new DateTime(2025, 4, 17), "CABINA", 250));
            _pasajes.Add(new Pasaje(_vuelos[11], (Pasajero)_usuarios[5], new DateTime(2025, 4, 18), "BODEGA", 260));
            _pasajes.Add(new Pasaje(_vuelos[12], (Pasajero)_usuarios[6], new DateTime(2025, 4, 19), "LIGHT", 270));
            _pasajes.Add(new Pasaje(_vuelos[13], (Pasajero)_usuarios[7], new DateTime(2025, 4, 20), "CABINA", 280));
            _pasajes.Add(new Pasaje(_vuelos[14], (Pasajero)_usuarios[8], new DateTime(2025, 4, 21), "BODEGA", 290));
            _pasajes.Add(new Pasaje(_vuelos[15], (Pasajero)_usuarios[9], new DateTime(2025, 4, 22), "LIGHT", 300));
            _pasajes.Add(new Pasaje(_vuelos[16], (Pasajero)_usuarios[2], new DateTime(2025, 4, 23), "CABINA", 310));
            _pasajes.Add(new Pasaje(_vuelos[17], (Pasajero)_usuarios[3], new DateTime(2025, 4, 24), "BODEGA", 320));
            _pasajes.Add(new Pasaje(_vuelos[18], (Pasajero)_usuarios[4], new DateTime(2025, 4, 25), "LIGHT", 330));
            _pasajes.Add(new Pasaje(_vuelos[19], (Pasajero)_usuarios[5], new DateTime(2025, 4, 26), "CABINA", 340));
            _pasajes.Add(new Pasaje(_vuelos[20], (Pasajero)_usuarios[6], new DateTime(2025, 4, 27), "BODEGA", 350));
            _pasajes.Add(new Pasaje(_vuelos[21], (Pasajero)_usuarios[7], new DateTime(2025, 4, 28), "LIGHT", 360));
            _pasajes.Add(new Pasaje(_vuelos[22], (Pasajero)_usuarios[8], new DateTime(2025, 4, 29), "CABINA", 370));
            _pasajes.Add(new Pasaje(_vuelos[23], (Pasajero)_usuarios[9], new DateTime(2025, 4, 30), "BODEGA", 380));
            _pasajes.Add(new Pasaje(_vuelos[24], (Pasajero)_usuarios[2], new DateTime(2025, 5, 1), "LIGHT", 390));


        }

    }

}

