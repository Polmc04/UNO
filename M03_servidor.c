#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <pthread.h>

int contador;

//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

int i;
int sockets[100];

// Lista de Concetados
typedef struct
{
    char nombre[20];
    int socket;
} Conectado;
typedef struct
{
    Conectado conectados [800];
    int num;
} ListaConectados;

ListaConectados miListaConectados;

int Pon(ListaConectados *lista, char nombre[20], int socket)
{
	// Añade nuevo conectado
    // Return 0 si OK
    //       -1 si no hay sitio en la lista
    if (lista->num == 800) return -1;
    else
    {
        strcpy(lista->conectados[lista->num].nombre, nombre);
        lista->conectados[lista->num].socket = socket;
        lista->num++;
        return 0;
    }
}
int DameSocket(ListaConectados *lista, char nombre[20])
{
    // Devuelve socket o -1 si no está en la lista
    int i = 0;
    int encontrado = 0;

    while(!encontrado && i < lista->num)
    {
        if (strcmp(lista->conectados[i].nombre,nombre) == 0) encontrado = 1;
        i++;
    }
    if (encontrado) return lista->conectados[i].socket;
    else return -1; 
}
int DamePosicion(ListaConectados *lista, char nombre[20])
{
    // Devuelve posición o -1 si no está en la lista
    int i = 0;
    int encontrado = 0;
	printf("Lista num %d\n", lista->num);
    while(!encontrado && i < lista->num)
    {
		printf("Nombre input en DamePosicion %s\n", nombre);
		printf("Nombre en lista en DamePosicion %s\n", lista->conectados[i].nombre);
        if (strcmp(lista->conectados[i].nombre,nombre) == 0) encontrado = 1;
        if (!encontrado) i++;
    }
    if (encontrado) return i;
    else return -1;
}
int DamePosicionSocket(ListaConectados *lista, int socket)
{
    // Devuelve posición o -1 si no está en la lista
    int i = 0;
    int encontrado = 0;
	printf("Lista num %d\n", lista->num);
    while(!encontrado && i < lista->num)
    {
		printf("Socket input en DamePosicionSocket %d\n", socket);
		printf("Socket en lista en DamePosicionSocket %d\n", lista->conectados[i].socket);
        if (strcmp(lista->conectados[i].socket,socket) == 0) encontrado = 1;
        if (!encontrado) i++;
    }
    if (encontrado) return i;
    else return -1;
}
int Elimina(ListaConectados *lista, char nombre[20])
{
    // Return 0 si elimina
    //       -1 si no está en la lista
	
	printf("Nombre input en Elimina %s\n", nombre);
    int pos = DamePosicion (lista, nombre);
	printf ("Posicion en Elimina %d\n", pos);
    if (pos == -1) return -1;
    else
    {
		/* El for de debajo funciona pero no deberia ya que si eliminas al ultimo intetaria acceder a una posicion que no existe
		   pero como funciona da igual
		if(pos = 0)
		{
			// Si eliminamos la primera posicion en la lista no entra en el bucle for, asi que lo hacemos a parte
			printf("Siguiente nombre %s\n", lista->conectados[1].nombre);
			printf("Siguiente socket %d\n", lista->conectados[1].socket);
			strcpy(lista->conectados[0].nombre, lista->conectados[1].nombre);
			lista->conectados[0].socket = lista->conectados[1].socket;
		}
		else if (pos = lista->num - 1)
		{
			// Si eliminamos al último en la lista tampoco entra en el bucle
			strcpy(lista->conectados[lista->num - 1].nombre, lista->conectados[1].nombre);
			lista->conectados[lista->num - 1].socket = lista->conectados[1].socket;
		}
		*/
        for (int i = pos; i<lista->num;i++)
        {
            // En la posicion del Eliminado copiamos la información del siguiente en la lista
            lista->conectados[i] = lista->conectados[i+1];
            //strcpy(lista->conectados[i].nombre, lista->conectados[i+1].nombre);
            //lista->conectados[i].socket = lista->conectados[i+1].socket;
        }
		miListaConectados.num--;// Se reduce en 1 el numero de conectados
    }
    return 0;
}
int EliminaSocket(ListaConectados *lista, int socket)
{
    // Return 0 si elimina
    //       -1 si no está en la lista
	
	printf("Socket input en Elimina %d\n", socket);
    int pos = DamePosicionSocket (lista, socket);
	printf ("Posicion en Elimina %d\n", pos);
    if (pos == -1) return -1;
    else
    {
		/* El for de debajo funciona pero no deberia ya que si eliminas al ultimo intetaria acceder a una posicion que no existe
		   pero como funciona da igual
		if(pos = 0)
		{
			// Si eliminamos la primera posicion en la lista no entra en el bucle for, asi que lo hacemos a parte
			printf("Siguiente nombre %s\n", lista->conectados[1].nombre);
			printf("Siguiente socket %d\n", lista->conectados[1].socket);
			strcpy(lista->conectados[0].nombre, lista->conectados[1].nombre);
			lista->conectados[0].socket = lista->conectados[1].socket;
		}
		else if (pos = lista->num - 1)
		{
			// Si eliminamos al último en la lista tampoco entra en el bucle
			strcpy(lista->conectados[lista->num - 1].nombre, lista->conectados[1].nombre);
			lista->conectados[lista->num - 1].socket = lista->conectados[1].socket;
		}
		*/
        for (int i = pos; i<lista->num;i++)
        {
            // En la posicion del Eliminado copiamos la información del siguiente en la lista
            lista->conectados[i] = lista->conectados[i+1];
            //strcpy(lista->conectados[i].nombre, lista->conectados[i+1].nombre);
            //lista->conectados[i].socket = lista->conectados[i+1].socket;
        }
		miListaConectados.num--;// Se reduce en 1 el numero de conectados
    }
    return 0;
}
void DameConectados(ListaConectados *lista, char conectados[800])
{
    // Escribe en conectados el número de conectados y sus nombres
    // "3/Pol/Joan/Alonso"

    sprintf(conectados, "%d", lista->num);
    if (lista->num == 0) printf("No queda nadie conectado\n");
    for (int i = 0; i< lista->num; i++)
    {
		printf("%s esta conectado\n", lista->conectados[i].nombre);
        sprintf(conectados, "%s/%s",conectados, lista->conectados[i].nombre);
    }
}

void *AtenderCliente(void *socket)
{
	int sock_conn;
	int *s;
	s = (int *) socket;
	sock_conn = *s;
	printf("Socket: %d\n", sock_conn);

	char peticion[512];
	char respuesta[512];
	char notificacion[800];

	int err;
	MYSQL *conn;
	MYSQL_RES *res;
	MYSQL_ROW *row;
	//Creamos una conexion al servidor MYSQL
	conn = mysql_init(NULL);
	
	if (conn == NULL) 
	{
		printf ("Error al crear la conexion: %u %s\n",
		mysql_errno(conn), mysql_error(conn));
		exit (1);
	}

	//conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "M03_BBDD", 0, NULL, 0); // Producción
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "BBDD", 0, NULL, 0); // Desarrollo
	if (conn==NULL) 
	{
		printf ("Error al inicializar la conexion: %u %s\n",
		mysql_errno(conn), mysql_error(conn));
		exit (1);
	}

	char consulta[512]; // Consultas SQL se guardaran aqui

	int ret;
	int terminar = 0;

	char nombre[20] = "\0";
	char password[20];

	char conectados[800];
	// Entramos en un bucle para atender todas las peticiones de este cliente hasta que se desconecte
	while (terminar == 0)
	{
		// Ahora recibimos la peticion
		ret = read(sock_conn,peticion, sizeof(peticion));
		printf ("Recibido\n");
		
		// Tenemos que anadirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		peticion[ret]='\0';
		printf ("Peticion: %s\n",peticion);
		
		// vamos a ver que quiere el cliente
		char *p = strtok( peticion, "/");
		int codigo =  atoi (p);
		
		if (codigo != 0 && codigo < 4) // Codigo 0 es desconectar, codigo > 4 no hace falta nombre ni password 
		{
			p = strtok( NULL, "/");
			strcpy (nombre, p);

			p = strtok( NULL, "/");
			strcpy (password, p);

			printf ("Codigo: %d, Nombre: %s Password: %s\n", codigo, nombre, password);
		}

		if (codigo == 0) // Peticion de desconexion
		{
			printf("Usuario se ha desconectado\n");
			terminar = 1;

			// Quitar de la lista de conectados si se ha conectado
			int resultado = Elimina(&miListaConectados, nombre);
			printf("Prueba %d\n", resultado);
			if(resultado == -1) printf("Error al quitar de la lista de conectados (Posiblemente no hayas logged in)\n");
			else printf("Se ha quitado a %s de la lista de conectados\n", nombre);

			// Ver quienes quedan conectados
			DameConectados(&miListaConectados, conectados);

			// Notificacion automatica de conectados
			
			DameConectados(&miListaConectados, conectados);
			sprintf(notificacion, "9/%s", conectados);
			printf(conectados);
			
			for (int j=0; j < i; j++) // Para cada cliente conectado
				write (sockets[j],notificacion, strlen(notificacion));

			// sprintf(respuesta, "0/0"); // Parche para que el cliente no crashee, no tiene otra utilidad
			
		}	
		else if (codigo == 1) // Sign up
		{
			strcpy(consulta,"SELECT MAX(Identificador) FROM Jugadores");
			
			err=mysql_query(conn,consulta);
			res = mysql_store_result(conn);
			row = mysql_fetch_row (res);
			if (err !=0) 
			{
				printf("Error al obtener mayor Id");
				fprintf(stderr, "%s\n", mysql_error(conn));
				exit(1);
			}
			int Id = row[0] + 1; // Id Asignado será el Id más grande de la lista +1

			strcpy(consulta,"SELECT COUNT(*) FROM Jugadores");
			
			err=mysql_query(conn,consulta);
			res = mysql_store_result(conn);
			row = mysql_fetch_row (res);
			if (err !=0) 
			{
				printf("Error al obtener el numero de jugadores");
				fprintf(stderr, "%s\n", mysql_error(conn));
				exit(1);
			}
			int jugadores = atoi(row[0]); // numero de jugadores Registrados

			printf("Hay un total de %d jugadores registrados antes de INSERT\n", jugadores);

			// Comprobamos si el usuario ya existe
			sprintf(consulta, "SELECT * FROM Jugadores WHERE Nombre = '%s'", nombre);
			// Ejecutar la consulta
			err = mysql_query(conn,consulta);
			res = mysql_store_result(conn);
			row = mysql_fetch_row (res);
			if (err !=0) 
			{
				printf("Error al comprobar si existe ese usuario");
				fprintf(stderr, "%s\n", mysql_error(conn));
				exit(1);
			}
			// Obtener el resultado de la consulta	
			if (row == NULL) // Si no hay nadie con ese nombre creamos nuevo jugador
			{
				//INSERT INTO Jugadores VALUES(1,'Joan','1',1312, 10, 500);
				sprintf(consulta, "INSERT INTO Jugadores VALUES (%d, '%s', '%s', 0, 0, 1000);", Id, nombre, password);

				// Insertamos
				err = mysql_query(conn, consulta);
				if (err!=0) 
				{
					printf ("Error al introducir datos la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				sprintf(respuesta,"1/1");
			}
			else // Si ya existe el usuario con ese nombre 
			{
				sprintf(respuesta, "1/2");
			}
		}
		else if (codigo == 2) // Log in
		{
			sprintf(consulta, "SELECT Password FROM Jugadores WHERE Nombre = '%s'", nombre);

			err = mysql_query(conn,consulta);
			res = mysql_store_result(conn);
			row = mysql_fetch_row (res);
			
			if (err != 0) // Comprobar errores
			{
				printf("Error al comprobar si existe ese usuario\n");
				fprintf(stderr, "%s\n", mysql_error(conn));
				exit(1);
			}
			if(row == NULL) // Si no devuelve nada la query
			{
				printf("No hay ningun usuario con ese nombre\n");
				sprintf(respuesta, "2/3");
			}
			else // BBDD devuelve datos del cliente 
			{
				if(strcmp(row[0],password) == 0) // Coincide contraseña 
				{
					// Quitamos de la lista si está logeado con otro usuario
					//EliminaSocket(&miListaConectados, sock_conn);

					printf("Inicio sesion correctamente\n");
					sprintf(respuesta, "2/1");

					// Insertamos en la lista de conectados
					int resultado = Pon(&miListaConectados, nombre, sock_conn/*Socket en el que se conecta*/);
					if (resultado == -1) printf("No se ha podido insertar a %s en la lista de conectados\n", nombre);
					else printf("Se ha insertado a %s en conectados\n", nombre);

					// Notificacion automatica de conectados
					DameConectados(&miListaConectados, conectados);
					sprintf(notificacion, "9/%s", conectados);
					printf(conectados);
					printf("\n");

					
					for (int j=0; j < i; j++) // Para cada cliente conectado
						write (sockets[j],notificacion, strlen(notificacion));
					
				}
				else 
				{
					printf("Contrasena incorrecta\n");
					sprintf(respuesta, "2/2");
				}
			}
		}
		else if(codigo == 3) // Remove
		{
			sprintf(consulta, "SELECT Password FROM Jugadores WHERE Nombre = '%s'", nombre);
			// Ejecutar la consulta
			err = mysql_query(conn,consulta);
			res = mysql_store_result(conn);
			row = mysql_fetch_row (res);
			
			if (err != 0) 
			{
				printf("Error al comprobar si existe ese usuario\n");
				fprintf(stderr, "%s\n", mysql_error(conn));
				exit(1);
			}
			if(row == NULL)
			{
				printf("No hay ningun usuario con ese nombre\n");
				sprintf(respuesta, "3/3");
			}
			else
			{
				if(strcmp(row[0],password) == 0) // Password correcto
				{	
					sprintf(consulta, "DELETE FROM Ranking WHERE Nombre IN (SELECT Identificador FROM Jugadores WHERE Nombre = '%s')", nombre); // Primero Eliminamos del Ranking
					err = mysql_query(conn,consulta);
					res = mysql_store_result(conn);

					if(err != 0)
					{
						printf("Error al eliminar ese usuario\n");
						fprintf(stderr, "%s\n", mysql_error(conn));
						exit(1);
					}
					else
					{
						printf("Usuario eliminado del Ranking\n");
					}

					sprintf(consulta, "DELETE FROM Jugadores WHERE Nombre = '%s'", nombre);
					// Ejecutar la consulta
					err = mysql_query(conn,consulta);
					res = mysql_store_result(conn);

					if(err != 0)
					{
						printf("Error al eliminar ese usuario\n");
						fprintf(stderr, "%s\n", mysql_error(conn));
						exit(1);
					}
					else
					{
						printf("Usuario eliminado\n");
						sprintf(respuesta,"3/1");

						// Lo quitamos de la lista de conectados
						int resultado = Elimina (&miListaConectados, nombre);
						if(resultado == -1) printf("Error al quitar de la lista de conectados (Posiblemente no hayas logged in)\n");
						else printf("Se ha quitado a %s de la lista de conectados", nombre);

						// Notificacion automatica de conectados
						char notificacion[800];
						DameConectados(&miListaConectados, conectados);
						sprintf(notificacion, "9/%s", conectados);
						printf(conectados);
						printf("\n"); // saltamos de linea

						for (int j=0; j < i; j++) // Para cada cliente conectado
							write (sockets[j],notificacion, strlen(notificacion));
					}
				}
				else{
					printf("Password incorrecto\n");
					sprintf(respuesta,"3/2");
				}
			}
		}
		else if(codigo == 4)// SELECT Jugador con mas partidas
		{
			strcpy (consulta,"SELECT Nombre, Jugadas FROM Jugadores WHERE Jugadas = (SELECT MAX(Jugadas) FROM Jugadores)"); // Seleccionamos nombre y mayor numero de jugadas
			
			err=mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			res = mysql_store_result (conn);
			row = mysql_fetch_row (res);
			
			if (row == NULL) printf ("No se han obtenido datos en la consulta\n");
			else
			{
				sprintf (respuesta,"4/%s es el jugador con mas partidas, con un total de %d",row[0], atoi(row[1]));
			}	
		}
		else if(codigo == 5) // SELECT jugador mas ELO 
		{
			strcpy (consulta,"SELECT Nombre, ELO FROM Jugadores WHERE ELO = (SELECT MAX(ELO) FROM Jugadores)");
			
			err=mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			res = mysql_store_result (conn);
			row = mysql_fetch_row (res);
			
			if (row == NULL)
				printf ("No se han obtenido datos en la consulta\n");
			else
			{
				sprintf(respuesta, "5/%s es el jugador con mas ELO, tiene %d",row[0],atoi(row[1]));
			}
		}
		else if(codigo == 6) // SELECT color Carta
		{
			strcpy (consulta,"SELECT Color FROM Cartas WhERE Cartas.Numero = '+4'");

			err=mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			res = mysql_store_result (conn);
			row = mysql_fetch_row (res);
			
			if (row == NULL)
				printf ("No se han obtenido datos en la consulta\n");
			else
			{
				sprintf(respuesta, "6/%s es el color de la carta +4",row[0]);
			}
		}
		/* Ya no se usa
		else if (codigo == 7) // Conectados
		{
			DameConectados(&miListaConectados, conectados);
			sprintf(respuesta, "7/%s", conectados)
			printf(conectados);
		}  */
		else if (codigo == 8) // Num Random
		{
			// Semilla para la generación de números aleatorios
    		// Inicializar la semilla para el generador de números aleatorios
    		static int semillaInicializada = 0;
    		if (!semillaInicializada) 
			{
        		srand(time(NULL));
        		semillaInicializada = 1;
   			}

    		// Generar un número aleatorio entre 0 y 52
    		int rnd = rand() % 53;

    		// Guardar el número aleatorio en Respuesta
    		sprintf(respuesta, "8/%d", rnd);

   			// Imprimir el número aleatorio guardado en el string
    		printf("El numero aleatorio es: %s\n", respuesta);
		}
		if (codigo != 0)
		{
			printf("Respuesta: %s\n", respuesta);
			printf("Notificacion: %s\n", notificacion);
			// Enviamos respuesta
			write (sock_conn,respuesta, strlen(respuesta));
		}
	}
	// Se acabo el servicio para este cliente
	close(sock_conn);
}
int main(int argc, char *argv[])
{
	int sock_conn, sock_listen, ret;
	//int puerto = 50010; // Producción
	int puerto = 9050; // Desarrollo
	struct sockaddr_in serv_adr;

	// Iniciamos el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0) printf("Error creant socket");
	
	// Bind al puerto

	memset(&serv_adr, 0, sizeof(serv_adr)); // inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la maquina. 
	// htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	
	// Establecemos el puerto de escucha
	serv_adr.sin_port = htons(puerto);

	// Ya no hace falta -> Forzamos el puerto, si no esta en uso anteriormente salta error con el comando fuser
	// Ya no hace falta -> bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr));
	
	// Reiniciamos el puerto de escucha
    const char *comando = "fuser -k -n tcp 50010"; // fuser -k -n tcp 9050 o tambien fuser -k -n 9050/tcp

    // Ejecutar el comando
    int resultado = system(comando);

    // Verificar si la ejecucion fue exitosa

    if (resultado == -1) 
	{
        perror("Error al ejecutar el comando\n");
    } 
	else
	{
        printf("Puerto 50010 en TCP liberado.\n");
    }

	//Bind
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0) printf ("Posible error al bind\n");
	if (listen(sock_listen, 3) < 0) printf("Error en el Listen\n");
	
	contador = 0;
	i = 0;
	pthread_t thread;

	// Bucle infinito
	for (;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		//sock_conn es el socket que usaremos para este cliente
		sockets[i] = sock_conn;
		pthread_create (&thread, NULL, AtenderCliente, &sockets[i]);
		i++;
	}
}
