using System;
using System.Collections;
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


        //METODOS AUXILIARES PARA VALIDACION DE ALTA EN PARTE A:
        public void ValidarExisteUsuario(Usuario nuevo)
        {
                if (_usuarios.Contains(nuevo))
                {
                    throw new Exception("Usuario no se puede registrar porque ya fue registrado previamente.");
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

        public List<Vuelo> ListarVuelosPorAeropuerto (string IATAfiltro)
        {

            List<Vuelo> vuelosQueSeVanAListar = new List<Vuelo>();
            foreach (Vuelo unVuelo in _vuelos)
            {
                string IATAsalida = unVuelo.Ruta.ObtenerIATAAeropuertoDeSalida();
                string IATAllegada = unVuelo.Ruta.ObtenerIATAAeropuertoDeLlegada();

                if (IATAsalida == IATAfiltro || IATAllegada == IATAfiltro)
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



        //-------- PARTE C --------DAR DE ALTA CLIENTE OCASIONAL-------------------

        //Primero vamos a recibir los datos del nuevo cliente a crear por parametros. Creamos la instancia de la clase Ocasional con el "Ocasional nuevo= new Ocasional", y por ultimo agregamos el
        //pasajero ocasional a la lista general de usuarios con el .add

        public void DarDeAltaClienteOcasional (Usuario nuevo)
        {
            this.ValidarExisteUsuario(nuevo);
            nuevo.Validar();
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

            if (listaDePasajes.Count == 0)
            {
                throw new Exception("No hay pasajes expedidos entre las fechas ingresadas");
            }

            return listaDePasajes;
        }


        //--------------PRECARGA------------------
        /* PROMT: Necesito que hagas una precarga mediante el metodo "public void PrecargarDatos()" de 5 clientes premium, 5 ocasionales y 2 administradores (que sean lo mas realistas posible (cedulas coherentes, etc) y validos. También, de 4 aviones,  
         20 aeropuertos, 30 rutas, 30 vuelos y 25 pasajes. Antes de agregar el objeto a la lista se debe crear en una variable el objeto y validarlo con objetoCualquiera.Validar(), 
        El formato a utilizar para agregarlo debe ser siempre _nombreDeLista.Add(objetoCualquiera)*/

        public void PrecargarDatos()
        {
            try
            {

                // ----- ADMINISTRADORES -----
                Administrador admin1 = new Administrador("admin1", "Admin123@", "admin1@empresa.com");
                admin1.Validar();
                _usuarios.Add(admin1);

                Administrador admin2 = new Administrador("admin2", "Admin456@", "admin2@empresa.com");
                admin2.Validar();
                _usuarios.Add(admin2);

                // ----- PASAJEROS PREMIUM -----
                Premium p1 = new Premium("Uruguaya", "12345678", "Lucía González", "Pass1@", "lucia.gonzalez@mail.com");
                p1.Validar();
                _usuarios.Add(p1);

                Premium p2 = new Premium("Argentina", "23456789", "Carlos Pérez", "Pass2@", "carlos.perez@mail.com");
                p2.Validar();
                _usuarios.Add(p2);

                Premium p3 = new Premium("Chilena", "34567890", "Ana Torres", "Pass3@", "ana.torres@mail.com");
                p3.Validar();
                _usuarios.Add(p3);

                Premium p4 = new Premium("Brasilera", "45678901", "Mateo Fernández", "Pass4@", "mateo.fernandez@mail.com");
                p4.Validar();
                _usuarios.Add(p4);

                Premium p5 = new Premium("Colombiana", "56789012", "Sofía Ramírez", "Pass5@", "sofia.ramirez@mail.com");
                p5.Validar();
                _usuarios.Add(p5);

                // ----- PASAJEROS OCASIONALES -----
                Ocasional o1 = new Ocasional("Uruguaya", "22334455", "Tomás López", "Pass6@", "tomas.lopez@mail.com");
                o1.Validar();
                _usuarios.Add(o1);

                Ocasional o2 = new Ocasional("Paraguaya", "33445566", "Camila García", "Pass7@", "camila.garcia@mail.com");
                o2.Validar();
                _usuarios.Add(o2);

                Ocasional o3 = new Ocasional("Mexicana", "44556677", "Joaquín Sánchez", "Pass8@", "joaquin.sanchez@mail.com");
                o3.Validar();
                _usuarios.Add(o3);

                Ocasional o4 = new Ocasional("Venezolana", "55667788", "Valentina Rodríguez", "Pass9@", "valentina.rodriguez@mail.com");
                o4.Validar();
                _usuarios.Add(o4);

                Ocasional o5 = new Ocasional("Boliviana", "66778899", "Martín Martínez", "Pass10@", "martin.martinez@mail.com");
                o5.Validar();
                _usuarios.Add(o5);

                // ----- AVIONES -----
                Avion a1 = new Avion("Boeing", "737", 5000, 180, 10.5m, "Narrow-body");
                a1.Validar();
                _aviones.Add(a1);

                Avion a2 = new Avion("Airbus", "A320", 4800, 170, 9.8m, "Narrow-body");
                a2.Validar();
                _aviones.Add(a2);

                Avion a3 = new Avion("Embraer", "E190", 4000, 100, 7.2m, "Regional");
                a3.Validar();
                _aviones.Add(a3);

                Avion a4 = new Avion("Bombardier", "CRJ900", 3700, 90, 6.9m, "Regional");
                a4.Validar();
                _aviones.Add(a4);

                // ----- AEROPUERTOS -----
                Aeropuerto aero1 = new Aeropuerto("MVD", "Montevideo", 250, 100);
                aero1.Validar();
                _aeropuertos.Add(aero1);

                Aeropuerto aero2 = new Aeropuerto("EZE", "Buenos Aires", 255, 102);
                aero2.Validar();
                _aeropuertos.Add(aero2);

                Aeropuerto aero3 = new Aeropuerto("SCL", "Santiago", 260, 104);
                aero3.Validar();
                _aeropuertos.Add(aero3);

                Aeropuerto aero4 = new Aeropuerto("LIM", "Lima", 265, 106);
                aero4.Validar();
                _aeropuertos.Add(aero4);

                Aeropuerto aero5 = new Aeropuerto("BOG", "Bogotá", 270, 108);
                aero5.Validar();
                _aeropuertos.Add(aero5);

                Aeropuerto aero6 = new Aeropuerto("GIG", "Río", 275, 110);
                aero6.Validar();
                _aeropuertos.Add(aero6);

                Aeropuerto aero7 = new Aeropuerto("GRU", "São Paulo", 280, 112);
                aero7.Validar();
                _aeropuertos.Add(aero7);

                Aeropuerto aero8 = new Aeropuerto("UIO", "Quito", 285, 114);
                aero8.Validar();
                _aeropuertos.Add(aero8);

                Aeropuerto aero9 = new Aeropuerto("CCS", "Caracas", 290, 116);
                aero9.Validar();
                _aeropuertos.Add(aero9);

                Aeropuerto aero10 = new Aeropuerto("PTY", "Panamá", 295, 118);
                aero10.Validar();
                _aeropuertos.Add(aero10);

                Aeropuerto aero11 = new Aeropuerto("ASU", "Asunción", 300, 120);
                aero11.Validar();
                _aeropuertos.Add(aero11);

                Aeropuerto aero12 = new Aeropuerto("LPB", "La Paz", 305, 122);
                aero12.Validar();
                _aeropuertos.Add(aero12);

                Aeropuerto aero13 = new Aeropuerto("MEX", "Ciudad de México", 310, 124);
                aero13.Validar();
                _aeropuertos.Add(aero13);

                Aeropuerto aero14 = new Aeropuerto("MIA", "Miami", 315, 126);
                aero14.Validar();
                _aeropuertos.Add(aero14);

                Aeropuerto aero15 = new Aeropuerto("MAD", "Madrid", 320, 128);
                aero15.Validar();
                _aeropuertos.Add(aero15);

                Aeropuerto aero16 = new Aeropuerto("FCO", "Roma", 325, 130);
                aero16.Validar();
                _aeropuertos.Add(aero16);

                Aeropuerto aero17 = new Aeropuerto("CDG", "París", 330, 132);
                aero17.Validar();
                _aeropuertos.Add(aero17);

                Aeropuerto aero18 = new Aeropuerto("LIS", "Lisboa", 335, 134);
                aero18.Validar();
                _aeropuertos.Add(aero18);

                Aeropuerto aero19 = new Aeropuerto("LHR", "Londres", 340, 136);
                aero19.Validar();
                _aeropuertos.Add(aero19);

                Aeropuerto aero20 = new Aeropuerto("YYZ", "Toronto", 345, 138);
                aero20.Validar();
                _aeropuertos.Add(aero20);


                // ----- RUTAS -----
                Ruta r1 = new Ruta(_aeropuertos[0], _aeropuertos[1], 2000);
                r1.Validar();
                _rutas.Add(r1);

                Ruta r2 = new Ruta(_aeropuertos[2], _aeropuertos[3], 1500);
                r2.Validar();
                _rutas.Add(r2);

                Ruta r3 = new Ruta(_aeropuertos[4], _aeropuertos[5], 1700);
                r3.Validar();
                _rutas.Add(r3);

                Ruta r4 = new Ruta(_aeropuertos[6], _aeropuertos[7], 1400);
                r4.Validar();
                _rutas.Add(r4);

                Ruta r5 = new Ruta(_aeropuertos[8], _aeropuertos[9], 1800);
                r5.Validar();
                _rutas.Add(r5);

                Ruta r6 = new Ruta(_aeropuertos[10], _aeropuertos[11], 1900);
                r6.Validar();
                _rutas.Add(r6);

                Ruta r7 = new Ruta(_aeropuertos[12], _aeropuertos[13], 2200);
                r7.Validar();
                _rutas.Add(r7);

                Ruta r8 = new Ruta(_aeropuertos[14], _aeropuertos[15], 2400);
                r8.Validar();
                _rutas.Add(r8);

                Ruta r9 = new Ruta(_aeropuertos[16], _aeropuertos[17], 2300);
                r9.Validar();
                _rutas.Add(r9);

                Ruta r10 = new Ruta(_aeropuertos[18], _aeropuertos[19], 2100);
                r10.Validar();
                _rutas.Add(r10);

                Ruta r11 = new Ruta(_aeropuertos[0], _aeropuertos[2], 1300);
                r11.Validar();
                _rutas.Add(r11);

                Ruta r12 = new Ruta(_aeropuertos[3], _aeropuertos[5], 1600);
                r12.Validar();
                _rutas.Add(r12);

                Ruta r13 = new Ruta(_aeropuertos[6], _aeropuertos[9], 2500);
                r13.Validar();
                _rutas.Add(r13);

                Ruta r14 = new Ruta(_aeropuertos[10], _aeropuertos[13], 2700);
                r14.Validar();
                _rutas.Add(r14);

                Ruta r15 = new Ruta(_aeropuertos[12], _aeropuertos[15], 2800);
                r15.Validar();
                _rutas.Add(r15);

                Ruta r16 = new Ruta(_aeropuertos[1], _aeropuertos[4], 2900);
                r16.Validar();
                _rutas.Add(r16);

                Ruta r17 = new Ruta(_aeropuertos[7], _aeropuertos[10], 2600);
                r17.Validar();
                _rutas.Add(r17);

                Ruta r18 = new Ruta(_aeropuertos[14], _aeropuertos[17], 3100);
                r18.Validar();
                _rutas.Add(r18);

                Ruta r19 = new Ruta(_aeropuertos[8], _aeropuertos[11], 3300);
                r19.Validar();
                _rutas.Add(r19);

                Ruta r20 = new Ruta(_aeropuertos[5], _aeropuertos[18], 3000);
                r20.Validar();
                _rutas.Add(r20);

                Ruta r21 = new Ruta(_aeropuertos[2], _aeropuertos[6], 2700);
                r21.Validar();
                _rutas.Add(r21);

                Ruta r22 = new Ruta(_aeropuertos[3], _aeropuertos[7], 2800);
                r22.Validar();
                _rutas.Add(r22);

                Ruta r23 = new Ruta(_aeropuertos[1], _aeropuertos[8], 3000);
                r23.Validar();
                _rutas.Add(r23);

                Ruta r24 = new Ruta(_aeropuertos[4], _aeropuertos[10], 2700);
                r24.Validar();
                _rutas.Add(r24);

                Ruta r25 = new Ruta(_aeropuertos[13], _aeropuertos[15], 3400);
                r25.Validar();
                _rutas.Add(r25);

                Ruta r26 = new Ruta(_aeropuertos[16], _aeropuertos[19], 3600);
                r26.Validar();
                _rutas.Add(r26);

                Ruta r27 = new Ruta(_aeropuertos[11], _aeropuertos[18], 3800);
                r27.Validar();
                _rutas.Add(r27);

                Ruta r28 = new Ruta(_aeropuertos[9], _aeropuertos[17], 3500);
                r28.Validar();
                _rutas.Add(r28);

                Ruta r29 = new Ruta(_aeropuertos[12], _aeropuertos[14], 3200);
                r29.Validar();
                _rutas.Add(r29);

                Ruta r30 = new Ruta(_aeropuertos[0], _aeropuertos[19], 3900);
                r30.Validar();
                _rutas.Add(r30);

                // ----- VUELOS -----
                Vuelo v1 = new Vuelo(1, _aviones[0], _rutas[0], new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday });
                v1.Validar();
                _vuelos.Add(v1);

                Vuelo v2 = new Vuelo(2, _aviones[1], _rutas[1], new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday });
                v2.Validar();
                _vuelos.Add(v2);

                Vuelo v3 = new Vuelo(3, _aviones[2], _rutas[2], new List<DayOfWeek> { DayOfWeek.Friday });
                v3.Validar();
                _vuelos.Add(v3);

                Vuelo v4 = new Vuelo(4, _aviones[3], _rutas[3], new List<DayOfWeek> { DayOfWeek.Sunday });
                v4.Validar();
                _vuelos.Add(v4);

                Vuelo v5 = new Vuelo(5, _aviones[0], _rutas[4], new List<DayOfWeek> { DayOfWeek.Monday });
                v5.Validar();
                _vuelos.Add(v5);

                Vuelo v6 = new Vuelo(6, _aviones[1], _rutas[5], new List<DayOfWeek> { DayOfWeek.Tuesday });
                v6.Validar();
                _vuelos.Add(v6);

                Vuelo v7 = new Vuelo(7, _aviones[2], _rutas[6], new List<DayOfWeek> { DayOfWeek.Wednesday });
                v7.Validar();
                _vuelos.Add(v7);

                Vuelo v8 = new Vuelo(8, _aviones[3], _rutas[7], new List<DayOfWeek> { DayOfWeek.Thursday });
                v8.Validar();
                _vuelos.Add(v8);

                Vuelo v9 = new Vuelo(9, _aviones[0], _rutas[8], new List<DayOfWeek> { DayOfWeek.Friday });
                v9.Validar();
                _vuelos.Add(v9);

                Vuelo v10 = new Vuelo(10, _aviones[1], _rutas[9], new List<DayOfWeek> { DayOfWeek.Saturday });
                v10.Validar();
                _vuelos.Add(v10);

                Vuelo v11 = new Vuelo(11, _aviones[2], _rutas[10], new List<DayOfWeek> { DayOfWeek.Sunday });
                v11.Validar();
                _vuelos.Add(v11);

                Vuelo v12 = new Vuelo(12, _aviones[3], _rutas[11], new List<DayOfWeek> { DayOfWeek.Monday });
                v12.Validar();
                _vuelos.Add(v12);

                Vuelo v13 = new Vuelo(13, _aviones[0], _rutas[12], new List<DayOfWeek> { DayOfWeek.Tuesday });
                v13.Validar();
                _vuelos.Add(v13);

                Vuelo v14 = new Vuelo(14, _aviones[1], _rutas[13], new List<DayOfWeek> { DayOfWeek.Wednesday });
                v14.Validar();
                _vuelos.Add(v14);

                Vuelo v15 = new Vuelo(15, _aviones[2], _rutas[14], new List<DayOfWeek> { DayOfWeek.Thursday });
                v15.Validar();
                _vuelos.Add(v15);

                Vuelo v16 = new Vuelo(16, _aviones[3], _rutas[15], new List<DayOfWeek> { DayOfWeek.Friday });
                v16.Validar();
                _vuelos.Add(v16);

                Vuelo v17 = new Vuelo(17, _aviones[0], _rutas[16], new List<DayOfWeek> { DayOfWeek.Saturday });
                v17.Validar();
                _vuelos.Add(v17);

                Vuelo v18 = new Vuelo(18, _aviones[1], _rutas[17], new List<DayOfWeek> { DayOfWeek.Sunday });
                v18.Validar();
                _vuelos.Add(v18);

                Vuelo v19 = new Vuelo(19, _aviones[2], _rutas[18], new List<DayOfWeek> { DayOfWeek.Monday });
                v19.Validar();
                _vuelos.Add(v19);

                Vuelo v20 = new Vuelo(20, _aviones[3], _rutas[19], new List<DayOfWeek> { DayOfWeek.Tuesday });
                v20.Validar();
                _vuelos.Add(v20);

                Vuelo v21 = new Vuelo(21, _aviones[0], _rutas[20], new List<DayOfWeek> { DayOfWeek.Wednesday });
                v21.Validar();
                _vuelos.Add(v21);

                Vuelo v22 = new Vuelo(22, _aviones[1], _rutas[21], new List<DayOfWeek> { DayOfWeek.Thursday });
                v22.Validar();
                _vuelos.Add(v22);

                Vuelo v23 = new Vuelo(23, _aviones[2], _rutas[22], new List<DayOfWeek> { DayOfWeek.Friday });
                v23.Validar();
                _vuelos.Add(v23);

                Vuelo v24 = new Vuelo(24, _aviones[3], _rutas[23], new List<DayOfWeek> { DayOfWeek.Saturday });
                v24.Validar();
                _vuelos.Add(v24);

                Vuelo v25 = new Vuelo(25, _aviones[0], _rutas[24], new List<DayOfWeek> { DayOfWeek.Sunday });
                v25.Validar();
                _vuelos.Add(v25);

                Vuelo v26 = new Vuelo(26, _aviones[1], _rutas[25], new List<DayOfWeek> { DayOfWeek.Monday });
                v26.Validar();
                _vuelos.Add(v26);

                Vuelo v27 = new Vuelo(27, _aviones[2], _rutas[26], new List<DayOfWeek> { DayOfWeek.Tuesday });
                v27.Validar();
                _vuelos.Add(v27);

                Vuelo v28 = new Vuelo(28, _aviones[3], _rutas[27], new List<DayOfWeek> { DayOfWeek.Wednesday });
                v28.Validar();
                _vuelos.Add(v28);

                Vuelo v29 = new Vuelo(29, _aviones[0], _rutas[28], new List<DayOfWeek> { DayOfWeek.Thursday });
                v29.Validar();
                _vuelos.Add(v29);

                Vuelo v30 = new Vuelo(30, _aviones[1], _rutas[29], new List<DayOfWeek> { DayOfWeek.Friday });
                v30.Validar();
                _vuelos.Add(v30);

                // ----- PASAJES -----
                Pasaje pasaje1 = new Pasaje(v1, (Pasajero)_usuarios[2], new DateTime(2025, 5, 5), TipoEquipaje.CABINA, 150);
                pasaje1.Validar();
                _pasajes.Add(pasaje1);

                Pasaje pasaje2 = new Pasaje(v2, (Pasajero)_usuarios[3], new DateTime(2025, 5, 6), TipoEquipaje.LIGHT, 120);
                pasaje2.Validar();
                _pasajes.Add(pasaje2);

                Pasaje pasaje3 = new Pasaje(v3, (Pasajero)_usuarios[4], new DateTime(2025, 5, 9), TipoEquipaje.BODEGA, 180);
                pasaje3.Validar();
                _pasajes.Add(pasaje3);

                Pasaje pasaje4 = new Pasaje(v4, (Pasajero)_usuarios[5], new DateTime(2025, 5, 11), TipoEquipaje.CABINA, 145);
                pasaje4.Validar();
                _pasajes.Add(pasaje4);

                Pasaje pasaje5 = new Pasaje(v5, (Pasajero)_usuarios[6], new DateTime(2025, 5, 12), TipoEquipaje.LIGHT, 110);
                pasaje5.Validar();
                _pasajes.Add(pasaje5);

                Pasaje pasaje6 = new Pasaje(v6, (Pasajero)_usuarios[7], new DateTime(2025, 5, 13), TipoEquipaje.BODEGA, 160);
                pasaje6.Validar();
                _pasajes.Add(pasaje6);

                Pasaje pasaje7 = new Pasaje(v7, (Pasajero)_usuarios[8], new DateTime(2025, 5, 14), TipoEquipaje.CABINA, 130);
                pasaje7.Validar();
                _pasajes.Add(pasaje7);

                Pasaje pasaje8 = new Pasaje(v8, (Pasajero)_usuarios[9], new DateTime(2025, 5, 15), TipoEquipaje.LIGHT, 100);
                pasaje8.Validar();
                _pasajes.Add(pasaje8);

                Pasaje pasaje9 = new Pasaje(v9, (Pasajero)_usuarios[10], new DateTime(2025, 5, 16), TipoEquipaje.BODEGA, 170);
                pasaje9.Validar();
                _pasajes.Add(pasaje9);

                Pasaje pasaje10 = new Pasaje(v10, (Pasajero)_usuarios[2], new DateTime(2025, 5, 17), TipoEquipaje.CABINA, 140);
                pasaje10.Validar();
                _pasajes.Add(pasaje10);

                Pasaje pasaje11 = new Pasaje(v11, (Pasajero)_usuarios[3], new DateTime(2025, 5, 18), TipoEquipaje.LIGHT, 125);
                pasaje11.Validar();
                _pasajes.Add(pasaje11);

                Pasaje pasaje12 = new Pasaje(v12, (Pasajero)_usuarios[4], new DateTime(2025, 5, 19), TipoEquipaje.BODEGA, 135);
                pasaje12.Validar();
                _pasajes.Add(pasaje12);

                Pasaje pasaje13 = new Pasaje(v13, (Pasajero)_usuarios[5], new DateTime(2025, 5, 20), TipoEquipaje.CABINA, 155);
                pasaje13.Validar();
                _pasajes.Add(pasaje13);

                Pasaje pasaje14 = new Pasaje(v14, (Pasajero)_usuarios[6], new DateTime(2025, 5, 21), TipoEquipaje.LIGHT, 115);
                pasaje14.Validar();
                _pasajes.Add(pasaje14);

                Pasaje pasaje15 = new Pasaje(v15, (Pasajero)_usuarios[7], new DateTime(2025, 5, 22), TipoEquipaje.BODEGA, 165);
                pasaje15.Validar();
                _pasajes.Add(pasaje15);

                Pasaje pasaje16 = new Pasaje(v16, (Pasajero)_usuarios[8], new DateTime(2025, 5, 23), TipoEquipaje.CABINA, 175);
                pasaje16.Validar();
                _pasajes.Add(pasaje16);

                Pasaje pasaje17 = new Pasaje(v17, (Pasajero)_usuarios[9], new DateTime(2025, 5, 24), TipoEquipaje.LIGHT, 185);
                pasaje17.Validar();
                _pasajes.Add(pasaje17);

                Pasaje pasaje18 = new Pasaje(v18, (Pasajero)_usuarios[10], new DateTime(2025, 5, 25), TipoEquipaje.BODEGA, 195);
                pasaje18.Validar();
                _pasajes.Add(pasaje18);

                Pasaje pasaje19 = new Pasaje(v19, (Pasajero)_usuarios[2], new DateTime(2025, 5, 26), TipoEquipaje.CABINA, 205);
                pasaje19.Validar();
                _pasajes.Add(pasaje19);

                Pasaje pasaje20 = new Pasaje(v20, (Pasajero)_usuarios[3], new DateTime(2025, 5, 27), TipoEquipaje.LIGHT, 215);
                pasaje20.Validar();
                _pasajes.Add(pasaje20);

                Pasaje pasaje21 = new Pasaje(v21, (Pasajero)_usuarios[4], new DateTime(2025, 5, 28), TipoEquipaje.BODEGA, 225);
                pasaje21.Validar();
                _pasajes.Add(pasaje21);

                Pasaje pasaje22 = new Pasaje(v22, (Pasajero)_usuarios[5], new DateTime(2025, 5, 29), TipoEquipaje.CABINA, 235);
                pasaje22.Validar();
                _pasajes.Add(pasaje22);

                Pasaje pasaje23 = new Pasaje(v23, (Pasajero)_usuarios[6], new DateTime(2025, 5, 30), TipoEquipaje.LIGHT, 245);
                pasaje23.Validar();
                _pasajes.Add(pasaje23);

                Pasaje pasaje24 = new Pasaje(v24, (Pasajero)_usuarios[7], new DateTime(2025, 6, 1), TipoEquipaje.BODEGA, 255);
                pasaje24.Validar();
                _pasajes.Add(pasaje24);

                Pasaje pasaje25 = new Pasaje(v25, (Pasajero)_usuarios[8], new DateTime(2025, 6, 2), TipoEquipaje.CABINA, 265);
                pasaje25.Validar();
                _pasajes.Add(pasaje25);
        
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

    }
}

