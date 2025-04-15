Version - 14042025

-Override de Equals en Administrador que determina que dos Administradores son el mismo si tienen el mismo nickname

-Se implemento el metodo en sistema ListarPasajeros() que muestra en consola: Nombre, email, nacionalidad, documento de 
identidad y puntos o elegibilidad (dependiendo si es ocacional o premium) de cada pasajero.



Version - 13042025

-Cambio en UML en el tipo de dato del atributo _equipaje de Enum a TIPO_EQUIPAJE ya que era un error conceptual


Version - 12042025


-Agregamos precarga provisoria que aun no funciona

-Herencias hechas con constructor

-Metodo en program SolicitarDateTime(textoFecha : string) : DateTime

-Metodo en Sistema ObtenerPasajeEntre(fechaInicial : DateTime, fechaFinal : DateTime) : List<Pasaje> que luego vamos a usar para listarlos en program

-Metodo en program ListarPasajesSegunRangoDeFechas() : void recibe del usuario 2 fechas, manda los datos a sistema y recibe de sistema la lista de los pasajes, luego los lista

-En el menu principal agregamos case 2 en el switch de program para acceder a la funcion ListarPasajesSegunRangoDeFechas()

-Cambiamos ToString() en Pasaje para mostrar: id, Nombre pasajero, Fecha, precio, NumVuelo de vuelo

-Elegibilidad con criterio random quedo 70% de chances de ser elegible, esta implementado

-Metodo en administrador ValidarNickname() : void
