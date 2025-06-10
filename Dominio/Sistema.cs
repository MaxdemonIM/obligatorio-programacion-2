using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Dominio.Comparadores;

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

        //GET PARA ACCEDER A LISTAS DESDE EL MVC

        public List<Usuario> Usuarios { get { return _usuarios; } }
        public List<Vuelo> Vuelos { get { return _vuelos; } }

        public List<Ruta> Rutas { get { return _rutas; } }
        public List<Aeropuerto> Aeropuertos { get { return _aeropuertos; } }

        public List<Pasaje> Pasajes { get { return _pasajes; } }
        public List<Avion> Avion { get { return _aviones; } }



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

        //-------- PARTE A --------

        //Primero recorremos la lista que tenemos GENERAL de usuarios (_usuarios). Esta lista, va tener objetos que son Administrador y otros que son pasajeros (ocasional u premium). A nosotros,
        //solo nos importa mostrar estos últimos.  Dentro del if, vamos a preguntar dos cosas. Primero, pregunta si un objeto es un Pasajero (o una subclase de el, ya sea ocasional o premium).
        //Si llega a ser un pasajero, se muestra en consola aplicando poliformismo, ya que usando el ToString se va ejecutar el de Premium u Ocasional respectivamente

        public List<Pasajero> ListarPasajeros()
        {
            List<Pasajero> lista = new List<Pasajero>();

            foreach (Usuario unUsuario in _usuarios)
            {
                if (unUsuario is Pasajero unPasajero)
                {
                    lista.Add(unPasajero);
                }
            }

            if (lista.Count == 0)
            {
                throw new Exception("No hay pasajeros en el sistema");
            }
            return lista;
        }


        //-------- PARTE B -------- LISTAR VUELOS INCLUYEN UN CODIGO DE AEROPUERTO

        public List<Vuelo> ListarVuelosPorAeropuerto (string IATAfiltro, string IATAfiltro2)
        {
            List<Vuelo> vuelosQueSeVanAListar = new List<Vuelo>();
            foreach (Vuelo unVuelo in _vuelos)
            {
                string IATAsalida = unVuelo.Ruta.ObtenerIATAAeropuertoDeSalida();
                string IATAllegada = unVuelo.Ruta.ObtenerIATAAeropuertoDeLlegada();

                if (IATAsalida == IATAfiltro && IATAllegada == IATAfiltro2 || IATAsalida == IATAfiltro2 && IATAllegada == IATAfiltro)
                {
                    vuelosQueSeVanAListar.Add(unVuelo);
                }
            }

            if (vuelosQueSeVanAListar.Count == 0)
            {
                throw new Exception("No hay vuelos para el codigo IATA ingresados");
            }

            return vuelosQueSeVanAListar;
        }





        //-------- PARTE C --------DAR DE ALTA USUARIO-------------------

        //Primero vamos a recibir el nuevo usuario. Se verifica que no haya sido dado de alta previamente, se valida que lleguen correctos los datos segun lo que definimos en la clase y
        //por ultimo agregamos el usuario a la lista general de usuarios con el .add

 
        public void DarDeAltaUsuario(Ocasional nuevoUsuario)
        {
            this.ValidarExisteUsuario(nuevoUsuario);
            nuevoUsuario.Validar();
            _usuarios.Add(nuevoUsuario);
        }
        public void DarDeAltaUsuario(Administrador nuevoUsuario)
        {
            this.ValidarExisteUsuario(nuevoUsuario);
            nuevoUsuario.Validar();
            _usuarios.Add(nuevoUsuario);
        }

        public void DarDeAltaUsuario(Premium nuevoUsuario)
        {
            this.ValidarExisteUsuario(nuevoUsuario);
            nuevoUsuario.Validar();
            _usuarios.Add(nuevoUsuario);
        }

      // para login 

        public Usuario Login(string email, string password)
        {


            foreach (Usuario usuario in this._usuarios) { 
                if (usuario.email== email)
                {

                    if(usuario.password == password)
                    {
                        return usuario;
                    } else
                    {

                        throw new Exception("Contraseña incorrecta");
                    }
                }
            }
            throw new Exception("Nombre de usuario o contraseña Incorrecta.");

        }


        public Vuelo ObtenerVueloPorNumVuelo(int numVuelo)
        {
            foreach (Vuelo unVuelo in this._vuelos)
            {
                if (unVuelo.NumVuelo == numVuelo)
                {
                
                    return unVuelo;

                }
            }
            throw new Exception($"No existe vuelo con el número: {numVuelo}");
        }

        public string ObtenerTipoUsuario(Usuario usuario)
        {
            string tipoUsuario = "";
            foreach (Usuario unUsuario in this._usuarios)
            {
                if (usuario is Pasajero)
                {

                    tipoUsuario = "Pasajero";

                }
                else 
                { 
                    tipoUsuario = "Administrador";
                }
            }
            return tipoUsuario;
        }

        //Para editar

        public void ActualizarPuntosPremium(int puntos, string email)
        {
            
            foreach (Usuario usuario in this._usuarios)
            {
                if (usuario is Premium premium && premium.email == email)
                {
                    premium.ValidarPuntos(puntos);
                    premium.Puntos = puntos;
                    return;

                }
            }
            throw new Exception ("No existe premium con el email:" + email);
        }

        public void ActualizarElegibilidadOcasional( string email, bool nuevoEstado)
        {
            foreach (Usuario usuario in this._usuarios)
            {
                if (usuario is Ocasional ocasional && ocasional.email == email)
                {

                    ocasional.elegible = nuevoEstado;
                    return;
                }
            }
            throw new Exception("No existe ocasional con el email:" + email);
        }

        public List<Pasaje> ObtenerListaDePasajero()
        {
            List<Pasaje> listaFiltrada = new List<Pasaje>();

            foreach (Pasaje unPasaje in this._pasajes)
            {
                if (unPasaje.Pasajero == this.Usuarios[0])//hay que cambiarlo por el usuario logueado cuando hagamos el login
                {
                    listaFiltrada.Add(unPasaje);
                }
            }
            return listaFiltrada;
        }

        public List<Pasaje> ObtenerPasajeEntre(DateTime fechaInicial, DateTime fechaFinal)
        {
            List<Pasaje> listaDePasajes = new List<Pasaje>();

            if(fechaFinal < fechaInicial)
            {
                DateTime aux = fechaFinal;
                fechaFinal = fechaInicial;
                fechaInicial = aux;
            }

            foreach (Pasaje pasaje in _pasajes)
            {
                if (pasaje.Fecha >= fechaInicial && pasaje.Fecha <= fechaFinal)
                {
                    listaDePasajes.Add(pasaje);
                }
            }

            if (listaDePasajes.Count == 0)
            {
                throw new Exception("No hay pasajes expedidos entre las fechas ingresadas");
            }

            return listaDePasajes;
        }


       
        //Usuario CLIENTE

        //ver pasajes ordenados por FECHA desc(ya hacemos uso del COMPARE TO en pasajes)

        public void OrdenarPasajes() 
        {
            this._pasajes.Sort();
        }



        //Usuario ANONIMO


        //Usuario ADMINISTRADOR

        //Ver pasajes PARA CLIENTE, ordenados por PRECIO (usamos la clase COMPARADORA CompararPasajePorPrecio creada). 

        /*

        public List<Pasaje> OrdenarPasajesPorPrecio(List<Pasaje> listaDesordenada)
        {
            List<Pasaje> listaOrdenada = new List<Pasaje>(this.Pasajes.Sort(new CompararPasajePorPrecio())); //ROTOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO

            return listaOrdenada;
        }

        */

        //--------------PRECARGA------------------
        /* PROMT: Necesito que hagas una precarga mediante el metodo "public void PrecargarDatos()" de 5 clientes premium, 5 ocasionales y 2 administradores (que sean lo mas realistas posible (cedulas coherentes, etc) y validos. También, de 4 aviones,  
         20 aeropuertos, 30 rutas, 30 vuelos y 25 pasajes. Antes de agregar el objeto a la lista se debe crear en una variable el objeto y validarlo con objetoCualquiera.Validar(), 
        El formato a utilizar para agregarlo debe ser siempre _nombreDeLista.Add(objetoCualquiera)*/



        //METODOS AUXILIARES:


        public void ValidarExisteUsuario(Usuario nuevo)
        {
            if (_usuarios.Contains(nuevo))
            {
                throw new Exception("No puede registrarse ya que el email ingresado se encuentra en uso.");
            }
        }

        public void ValidarExisteAvion(Avion nuevo)
        {
            if (_aviones.Contains(nuevo))
            {
                throw new Exception("El avion no se pudo registrar porque ya fue registrado previamente.");
            }
        }

        public void ValidarExisteAeropuerto(Aeropuerto nuevo)
        {
            if (_aeropuertos.Contains(nuevo))
            {
                throw new Exception("El aeropuerto no se pudo registrar porque ya fue registrado previamente.");
            }
        }

        public void ValidarExisteRuta(Ruta nuevo)
        {
            if (_rutas.Contains(nuevo))
            {
                throw new Exception("La ruta no se pudo registrar porque ya fue registrada previamente.");
            }
        }
        public void ValidarExisteVuelo(Vuelo nuevo)
        {
            if (_vuelos.Contains(nuevo))
            {
                throw new Exception("El vuelo no se pudo registrar porque ya fue registrado previamente.");
            }
        }
        public void ValidarExistePasaje(Pasaje nuevo)
        {
            if (_pasajes.Contains(nuevo))
            {
                throw new Exception("El pasaje no se pudo registrar porque ya fue registrado previamente.");
            }
        }



        public void AgregarAvion(Avion unAvion)
        {
            this.ValidarExisteAvion(unAvion);
            unAvion.Validar();
            _aviones.Add(unAvion);
        }
        public void AgregarAeropuerto(Aeropuerto unAeropuerto)
        {
            this.ValidarExisteAeropuerto(unAeropuerto);
            unAeropuerto.Validar();
            _aeropuertos.Add(unAeropuerto);
        }
        public void AgregarRuta(Ruta unRuta)
        {
            this.ValidarExisteRuta(unRuta);
            unRuta.Validar();
            _rutas.Add(unRuta);
        }
        public void AgregarVuelo(Vuelo unVuelo)
        {
            this.ValidarExisteVuelo(unVuelo);
            unVuelo.Validar();
            _vuelos.Add(unVuelo);
        }
        public void AgregarPasaje(Pasaje unPasaje)
        {
            this.ValidarExistePasaje(unPasaje);
            unPasaje.Validar();
            _pasajes.Add(unPasaje);
        }



        public void PrecargarDatos()
        {

            // ----- ADMINISTRADORES -----
            DarDeAltaUsuario(new Administrador("admin1", "Admin123@", "admin1@empresa.com"));
            DarDeAltaUsuario(new Administrador("admin2", "Admin456@", "admin2@empresa.com"));

            // ----- PASAJEROS PREMIUM -----
            DarDeAltaUsuario(new Premium("Uruguaya", "12345678", "Lucía González", "Password1@", "lucia.gonzalez@mail.com"));
            DarDeAltaUsuario(new Premium("Argentina", "23456789", "Carlos Pérez", "Password2@", "carlos.perez@mail.com"));
            DarDeAltaUsuario(new Premium("Chilena", "34567890", "Ana Torres", "Password3@", "ana.torres@mail.com"));
            DarDeAltaUsuario(new Premium("Brasilera", "45678901", "Mateo Fernández", "Password4@", "mateo.fernandez@mail.com"));
            DarDeAltaUsuario(new Premium("Colombiana", "56789012", "Sofía Ramírez", "Password5@", "sofia.ramirez@mail.com"));

            // ----- PASAJEROS OCASIONALES -----
            DarDeAltaUsuario(new Ocasional("Uruguaya", "22334455", "Tomás López", "Password6@", "tomas.lopez@mail.com"));
            DarDeAltaUsuario(new Ocasional("Paraguaya", "33445566", "Camila García", "Password7@", "camila.garcia@mail.com"));
            DarDeAltaUsuario(new Ocasional("Mexicana", "44556677", "Joaquín Sánchez", "Password8@", "joaquin.sanchez@mail.com"));
            DarDeAltaUsuario(new Ocasional("Venezolana", "55667788", "Valentina Rodríguez", "Password9@", "valentina.rodriguez@mail.com"));
            DarDeAltaUsuario(new Ocasional("Boliviana", "66778899", "Martín Martínez", "Password10@", "martin.martinez@mail.com"));
      
            // ----- AVIONES -----
            AgregarAvion(new Avion("Boeing", "737", 5000, 180, 10.5m, "Narrow-body"));
            AgregarAvion(new Avion("Airbus", "A320", 4800, 170, 9.8m, "Narrow-body"));
            AgregarAvion(new Avion("Embraer", "E190", 4000, 100, 7.2m, "Regional"));
            AgregarAvion(new Avion("Bombardier", "CRJ900", 3700, 90, 6.9m, "Regional"));

            // ----- AEROPUERTOS -----
            AgregarAeropuerto(new Aeropuerto("MVD", "Montevideo", 250, 100));
            AgregarAeropuerto(new Aeropuerto("EZE", "Buenos Aires", 255, 102));
            AgregarAeropuerto(new Aeropuerto("SCL", "Santiago", 260, 104));
            AgregarAeropuerto(new Aeropuerto("LIM", "Lima", 265, 106));
            AgregarAeropuerto(new Aeropuerto("BOG", "Bogotá", 270, 108));
            AgregarAeropuerto(new Aeropuerto("GIG", "Río", 275, 110));
            AgregarAeropuerto(new Aeropuerto("GRU", "São Paulo", 280, 112));
            AgregarAeropuerto(new Aeropuerto("UIO", "Quito", 285, 114));
            AgregarAeropuerto(new Aeropuerto("CCS", "Caracas", 290, 116));
            AgregarAeropuerto(new Aeropuerto("PTY", "Panamá", 295, 118));
            AgregarAeropuerto(new Aeropuerto("ASU", "Asunción", 300, 120));
            AgregarAeropuerto(new Aeropuerto("LPB", "La Paz", 305, 122));
            AgregarAeropuerto(new Aeropuerto("MEX", "Ciudad de México", 310, 124));
            AgregarAeropuerto(new Aeropuerto("MIA", "Miami", 315, 126));
            AgregarAeropuerto(new Aeropuerto("MAD", "Madrid", 320, 128));
            AgregarAeropuerto(new Aeropuerto("FCO", "Roma", 325, 130));
            AgregarAeropuerto(new Aeropuerto("CDG", "París", 330, 132));
            AgregarAeropuerto(new Aeropuerto("LIS", "Lisboa", 335, 134));
            AgregarAeropuerto(new Aeropuerto("LHR", "Londres", 340, 136));
            AgregarAeropuerto(new Aeropuerto("YYZ", "Toronto", 345, 138));


            // ----- RUTAS -----
            AgregarRuta(new Ruta(_aeropuertos[0], _aeropuertos[1], 2000));
            AgregarRuta(new Ruta(_aeropuertos[2], _aeropuertos[3], 1500));
            AgregarRuta(new Ruta(_aeropuertos[4], _aeropuertos[5], 1700));
            AgregarRuta(new Ruta(_aeropuertos[6], _aeropuertos[7], 1400));
            AgregarRuta(new Ruta(_aeropuertos[8], _aeropuertos[9], 1800));
            AgregarRuta(new Ruta(_aeropuertos[10], _aeropuertos[11], 1900));
            AgregarRuta(new Ruta(_aeropuertos[12], _aeropuertos[13], 2200));
            AgregarRuta(new Ruta(_aeropuertos[14], _aeropuertos[15], 2400));
            AgregarRuta(new Ruta(_aeropuertos[16], _aeropuertos[17], 2300));
            AgregarRuta(new Ruta(_aeropuertos[18], _aeropuertos[19], 2100));
            AgregarRuta(new Ruta(_aeropuertos[0], _aeropuertos[2], 1300));
            AgregarRuta(new Ruta(_aeropuertos[3], _aeropuertos[5], 1600));
            AgregarRuta(new Ruta(_aeropuertos[6], _aeropuertos[9], 2500));
            AgregarRuta(new Ruta(_aeropuertos[10], _aeropuertos[13], 2700));
            AgregarRuta(new Ruta(_aeropuertos[12], _aeropuertos[15], 2800));
            AgregarRuta(new Ruta(_aeropuertos[1], _aeropuertos[4], 2900));
            AgregarRuta(new Ruta(_aeropuertos[7], _aeropuertos[10], 2600));
            AgregarRuta(new Ruta(_aeropuertos[14], _aeropuertos[17], 3100));
            AgregarRuta(new Ruta(_aeropuertos[8], _aeropuertos[11], 3300));
            AgregarRuta(new Ruta(_aeropuertos[5], _aeropuertos[18], 3000));
            AgregarRuta(new Ruta(_aeropuertos[2], _aeropuertos[6], 2700));
            AgregarRuta(new Ruta(_aeropuertos[3], _aeropuertos[7], 2800));
            AgregarRuta(new Ruta(_aeropuertos[1], _aeropuertos[8], 3000));
            AgregarRuta(new Ruta(_aeropuertos[4], _aeropuertos[10], 2700));
            AgregarRuta(new Ruta(_aeropuertos[13], _aeropuertos[15], 3400));
            AgregarRuta(new Ruta(_aeropuertos[16], _aeropuertos[19], 3600));
            AgregarRuta(new Ruta(_aeropuertos[11], _aeropuertos[18], 3800));
            AgregarRuta(new Ruta(_aeropuertos[9], _aeropuertos[17], 3500));
            AgregarRuta(new Ruta(_aeropuertos[12], _aeropuertos[14], 3200));
            AgregarRuta(new Ruta(_aeropuertos[0], _aeropuertos[19], 3900));

            // ----- VUELOS -----
            /*
                        AgregarVuelo(new Vuelo(1, _aviones[0], _rutas[0], new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday }));
                        AgregarVuelo(new Vuelo(2, _aviones[1], _rutas[1], new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday }));
                        AgregarVuelo(new Vuelo(3, _aviones[2], _rutas[2], new List<DayOfWeek> { DayOfWeek.Friday }));
                        AgregarVuelo(new Vuelo(4, _aviones[3], _rutas[3], new List<DayOfWeek> { DayOfWeek.Sunday }));
                        AgregarVuelo(new Vuelo(5, _aviones[0], _rutas[4], new List<DayOfWeek> { DayOfWeek.Monday }));
                        AgregarVuelo(new Vuelo(6, _aviones[1], _rutas[5], new List<DayOfWeek> { DayOfWeek.Tuesday }));
                        AgregarVuelo(new Vuelo(7, _aviones[2], _rutas[6], new List<DayOfWeek> { DayOfWeek.Wednesday }));
                        AgregarVuelo(new Vuelo(8, _aviones[3], _rutas[7], new List<DayOfWeek> { DayOfWeek.Thursday }));
                        AgregarVuelo(new Vuelo(9, _aviones[0], _rutas[8], new List<DayOfWeek> { DayOfWeek.Friday }));
                        AgregarVuelo(new Vuelo(10, _aviones[1], _rutas[9], new List<DayOfWeek> { DayOfWeek.Saturday }));
                        AgregarVuelo(new Vuelo(11, _aviones[2], _rutas[10], new List<DayOfWeek> { DayOfWeek.Sunday }));
                        AgregarVuelo(new Vuelo(12, _aviones[3], _rutas[11], new List<DayOfWeek> { DayOfWeek.Monday }));
                        AgregarVuelo(new Vuelo(13, _aviones[0], _rutas[12], new List<DayOfWeek> { DayOfWeek.Tuesday }));
                        AgregarVuelo(new Vuelo(14, _aviones[1], _rutas[13], new List<DayOfWeek> { DayOfWeek.Wednesday }));
                        AgregarVuelo(new Vuelo(15, _aviones[2], _rutas[14], new List<DayOfWeek> { DayOfWeek.Thursday }));
                        AgregarVuelo(new Vuelo(16, _aviones[3], _rutas[15], new List<DayOfWeek> { DayOfWeek.Friday }));
                        AgregarVuelo(new Vuelo(17, _aviones[0], _rutas[16], new List<DayOfWeek> { DayOfWeek.Saturday }));
                        AgregarVuelo(new Vuelo(18, _aviones[1], _rutas[17], new List<DayOfWeek> { DayOfWeek.Sunday }));
                        AgregarVuelo(new Vuelo(19, _aviones[2], _rutas[18], new List<DayOfWeek> { DayOfWeek.Monday }));
                        AgregarVuelo(new Vuelo(20, _aviones[3], _rutas[19], new List<DayOfWeek> { DayOfWeek.Tuesday }));
                        AgregarVuelo(new Vuelo(21, _aviones[0], _rutas[20], new List<DayOfWeek> { DayOfWeek.Wednesday }));
                        AgregarVuelo(new Vuelo(22, _aviones[1], _rutas[21], new List<DayOfWeek> { DayOfWeek.Thursday }));
                        AgregarVuelo(new Vuelo(23, _aviones[2], _rutas[22], new List<DayOfWeek> { DayOfWeek.Friday }));
                        AgregarVuelo(new Vuelo(24, _aviones[3], _rutas[23], new List<DayOfWeek> { DayOfWeek.Saturday }));
                        AgregarVuelo(new Vuelo(25, _aviones[0], _rutas[24], new List<DayOfWeek> { DayOfWeek.Sunday }));
                        AgregarVuelo(new Vuelo(26, _aviones[1], _rutas[25], new List<DayOfWeek> { DayOfWeek.Monday }));
                        AgregarVuelo(new Vuelo(27, _aviones[2], _rutas[26], new List<DayOfWeek> { DayOfWeek.Tuesday }));
                        AgregarVuelo(new Vuelo(28, _aviones[3], _rutas[27], new List<DayOfWeek> { DayOfWeek.Wednesday }));
                        AgregarVuelo(new Vuelo(29, _aviones[0], _rutas[28], new List<DayOfWeek> { DayOfWeek.Thursday }));
                        AgregarVuelo(new Vuelo(30, _aviones[1], _rutas[29], new List<DayOfWeek> { DayOfWeek.Friday }));
            */
            AgregarVuelo(new Vuelo(1, _aviones[0], _rutas[0], new List<DayOfWeek> { DayOfWeek.Tuesday }));      // 2026-05-05
            AgregarVuelo(new Vuelo(2, _aviones[1], _rutas[1], new List<DayOfWeek> { DayOfWeek.Wednesday }));    // 2026-05-06
            AgregarVuelo(new Vuelo(3, _aviones[2], _rutas[2], new List<DayOfWeek> { DayOfWeek.Saturday }));     // 2026-05-09
            AgregarVuelo(new Vuelo(4, _aviones[3], _rutas[3], new List<DayOfWeek> { DayOfWeek.Monday }));       // 2026-05-11
            AgregarVuelo(new Vuelo(5, _aviones[0], _rutas[4], new List<DayOfWeek> { DayOfWeek.Tuesday }));      // 2026-05-12
            AgregarVuelo(new Vuelo(6, _aviones[1], _rutas[5], new List<DayOfWeek> { DayOfWeek.Wednesday }));    // 2026-05-13
            AgregarVuelo(new Vuelo(7, _aviones[2], _rutas[6], new List<DayOfWeek> { DayOfWeek.Thursday }));     // 2026-05-14
            AgregarVuelo(new Vuelo(8, _aviones[3], _rutas[7], new List<DayOfWeek> { DayOfWeek.Friday }));       // 2026-05-15
            AgregarVuelo(new Vuelo(9, _aviones[0], _rutas[8], new List<DayOfWeek> { DayOfWeek.Saturday }));     // 2026-05-16
            AgregarVuelo(new Vuelo(10, _aviones[1], _rutas[9], new List<DayOfWeek> { DayOfWeek.Sunday }));      // 2026-05-17
            AgregarVuelo(new Vuelo(11, _aviones[2], _rutas[10], new List<DayOfWeek> { DayOfWeek.Monday }));     // 2026-05-18
            AgregarVuelo(new Vuelo(12, _aviones[3], _rutas[11], new List<DayOfWeek> { DayOfWeek.Tuesday }));    // 2026-05-19
            AgregarVuelo(new Vuelo(13, _aviones[0], _rutas[12], new List<DayOfWeek> { DayOfWeek.Wednesday }));  // 2026-05-20
            AgregarVuelo(new Vuelo(14, _aviones[1], _rutas[13], new List<DayOfWeek> { DayOfWeek.Thursday }));   // 2026-05-21
            AgregarVuelo(new Vuelo(15, _aviones[2], _rutas[14], new List<DayOfWeek> { DayOfWeek.Friday }));     // 2026-05-22
            AgregarVuelo(new Vuelo(16, _aviones[3], _rutas[15], new List<DayOfWeek> { DayOfWeek.Saturday }));   // 2026-05-23
            AgregarVuelo(new Vuelo(17, _aviones[0], _rutas[16], new List<DayOfWeek> { DayOfWeek.Sunday }));     // 2026-05-24
            AgregarVuelo(new Vuelo(18, _aviones[1], _rutas[17], new List<DayOfWeek> { DayOfWeek.Monday }));     // 2026-05-25
            AgregarVuelo(new Vuelo(19, _aviones[2], _rutas[18], new List<DayOfWeek> { DayOfWeek.Tuesday }));    // 2026-05-26
            AgregarVuelo(new Vuelo(20, _aviones[3], _rutas[19], new List<DayOfWeek> { DayOfWeek.Wednesday }));  // 2026-05-27
            AgregarVuelo(new Vuelo(21, _aviones[0], _rutas[20], new List<DayOfWeek> { DayOfWeek.Thursday }));   // 2026-05-28
            AgregarVuelo(new Vuelo(22, _aviones[1], _rutas[21], new List<DayOfWeek> { DayOfWeek.Friday }));     // 2026-05-29
            AgregarVuelo(new Vuelo(23, _aviones[2], _rutas[22], new List<DayOfWeek> { DayOfWeek.Saturday }));   // 2026-05-30
            AgregarVuelo(new Vuelo(24, _aviones[3], _rutas[23], new List<DayOfWeek> { DayOfWeek.Sunday }));     // 2026-05-31
            AgregarVuelo(new Vuelo(25, _aviones[0], _rutas[24], new List<DayOfWeek> { DayOfWeek.Monday }));     // 2026-06-01


            // ----- PASAJES -----
            AgregarPasaje(new Pasaje(_vuelos[0], (Pasajero)_usuarios[2], new DateTime(2026, 5, 5), TipoEquipaje.CABINA));
            AgregarPasaje(new Pasaje(_vuelos[1], (Pasajero)_usuarios[3], new DateTime(2026, 5, 6), TipoEquipaje.LIGHT));
            AgregarPasaje(new Pasaje(_vuelos[2], (Pasajero)_usuarios[4], new DateTime(2026, 5, 9), TipoEquipaje.BODEGA));
            AgregarPasaje(new Pasaje(_vuelos[3], (Pasajero)_usuarios[5], new DateTime(2026, 5, 11), TipoEquipaje.CABINA));
            AgregarPasaje(new Pasaje(_vuelos[4], (Pasajero)_usuarios[6], new DateTime(2026, 5, 12), TipoEquipaje.LIGHT));
            AgregarPasaje(new Pasaje(_vuelos[5], (Pasajero)_usuarios[7], new DateTime(2026, 5, 13), TipoEquipaje.BODEGA));
            AgregarPasaje(new Pasaje(_vuelos[6], (Pasajero)_usuarios[8], new DateTime(2026, 5, 14), TipoEquipaje.CABINA));
            AgregarPasaje(new Pasaje(_vuelos[7], (Pasajero)_usuarios[9], new DateTime(2026, 5, 15), TipoEquipaje.LIGHT));
            AgregarPasaje(new Pasaje(_vuelos[8], (Pasajero)_usuarios[10], new DateTime(2026, 5, 16), TipoEquipaje.BODEGA));
            AgregarPasaje(new Pasaje(_vuelos[9], (Pasajero)_usuarios[2], new DateTime(2026, 5, 17), TipoEquipaje.CABINA));
            AgregarPasaje(new Pasaje(_vuelos[10], (Pasajero)_usuarios[3], new DateTime(2026, 5, 18), TipoEquipaje.LIGHT));
            AgregarPasaje(new Pasaje(_vuelos[11], (Pasajero)_usuarios[4], new DateTime(2026, 5, 19), TipoEquipaje.BODEGA));
            AgregarPasaje(new Pasaje(_vuelos[12], (Pasajero)_usuarios[5], new DateTime(2026, 5, 20), TipoEquipaje.CABINA));
            AgregarPasaje(new Pasaje(_vuelos[13], (Pasajero)_usuarios[6], new DateTime(2026, 5, 21), TipoEquipaje.LIGHT));
            AgregarPasaje(new Pasaje(_vuelos[14], (Pasajero)_usuarios[7], new DateTime(2026, 5, 22), TipoEquipaje.BODEGA));
            AgregarPasaje(new Pasaje(_vuelos[15], (Pasajero)_usuarios[8], new DateTime(2026, 5, 23), TipoEquipaje.CABINA));
            AgregarPasaje(new Pasaje(_vuelos[16], (Pasajero)_usuarios[9], new DateTime(2026, 5, 24), TipoEquipaje.LIGHT));
            AgregarPasaje(new Pasaje(_vuelos[17], (Pasajero)_usuarios[10], new DateTime(2026, 5, 25), TipoEquipaje.BODEGA));
            AgregarPasaje(new Pasaje(_vuelos[18], (Pasajero)_usuarios[2], new DateTime(2026, 5, 26), TipoEquipaje.CABINA));
            AgregarPasaje(new Pasaje(_vuelos[19], (Pasajero)_usuarios[3], new DateTime(2026, 5, 27), TipoEquipaje.LIGHT));
            AgregarPasaje(new Pasaje(_vuelos[20], (Pasajero)_usuarios[4], new DateTime(2026, 5, 28), TipoEquipaje.BODEGA));
            AgregarPasaje(new Pasaje(_vuelos[21], (Pasajero)_usuarios[5], new DateTime(2026, 5, 29), TipoEquipaje.CABINA));
            AgregarPasaje(new Pasaje(_vuelos[22], (Pasajero)_usuarios[6], new DateTime(2026, 5, 30), TipoEquipaje.LIGHT));
            AgregarPasaje(new Pasaje(_vuelos[23], (Pasajero)_usuarios[7], new DateTime(2026, 5, 31), TipoEquipaje.BODEGA));
            AgregarPasaje(new Pasaje(_vuelos[24], (Pasajero)_usuarios[8], new DateTime(2026, 6, 1), TipoEquipaje.CABINA));

        }
    }
}

