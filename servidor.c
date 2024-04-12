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

// Evita errores de threads
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

void *AtenderCliente(void *socket)
{
	int sock_conn;
	int *s;
	s = (int *) socket;
	sock_conn = *s;

	char peticion[512];
	char respuesta[512];

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

	conn = mysql_real_connect (conn, "localhost","root", "mysql", "BBDD", 0, NULL, 0);
	if (conn==NULL) 
	{
		printf ("Error al inicializar la conexion: %u %s\n",
		mysql_errno(conn), mysql_error(conn));
		exit (1);
	}

	char consulta[512]; // Consultas SQL se guardaran aqui

	int ret;
	int terminar = 0;
	// Entramos en un bucle para atender todas las peticiones de este cliente
	//hasta que se desconecte
	while (terminar == 0)
	{
		// Ahora recibimos la peticion
		ret=read(sock_conn,peticion, sizeof(peticion));
		printf ("Recibido\n");
		
		// Tenemos que a?adirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		peticion[ret]='\0';
		
		printf ("Peticion: %s\n",peticion);
		
		// vamos a ver que quiere el cliente
		char *p = strtok( peticion, "/");
		int codigo =  atoi (p);
		// Ya tenemos el codigo de la peticion
		char nombre[20];
		char password[20];

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
			terminar = 1;
			printf("Usuario se ha desconectado\n");
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
			int jugadores = row[0]; // numero de jugadores

			printf("Hay un total de %d jugadores registrados\n", jugadores);

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
				/*strcpy (consulta, "INSERT INTO Jugadores VALUES(");
				strcat (consulta, d);
				strcat (consulta, ",'");
				strcat (consulta, nombre);
				strcat (consulta, "','");
				strcat (consulta, password);
				strcat (consulta, "', 0, 0, 1000);"); // El jugador empieza con 0 partidas ganadas y 0 jugadas, 1000 de ELO */
				// Insertamos
				err = mysql_query(conn, consulta);
				if (err!=0) 
				{
					printf ("Error al introducir datos la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				sprintf(respuesta,"1");
			}
			else // Si ya existe el usuario con ese nombre 
			{
				sprintf(respuesta, "2");
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
				sprintf(respuesta, "3");
			}
			else // Devuelve datos de cliente
			{
				if(strcmp(row[0],password) == 0) // Coincide contraseña 
				{
					printf("Inicio sesion correctamente\n");
					sprintf(respuesta, "1");
				}
				else 
				{
					printf("Contrasena incorrecta\n");
					sprintf(respuesta, "2");
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
				sprintf(respuesta, "3");
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
						sprintf(respuesta,"1");
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
						sprintf(respuesta,"1");
					}
					
				}
				else{
					printf("Password incorrecto\n");
					sprintf(respuesta,"2");
				}
			}
		}
		else if(codigo == 4)// Jugador con mas partidas
		{
			strcpy (consulta,"SELECT * FROM Jugadores");
			
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
				int max = 0;

				while (row !=NULL) {
					// la columna 3 contiene el nombre del jugador
					if(atoi(row[3])>max)
					{
						max = atoi(row[3]);
						strcpy(nombre,row[1]);
						
					}
					
					// obtenemos la siguiente fila
					row = mysql_fetch_row (res);					
				}
				sprintf (respuesta,"%s es el jugador con mas partidas, con un total de %d",nombre, max);
			}	
		}
		else if(codigo == 5)
		{
			strcpy (consulta,"SELECT * FROM Jugadores WHERE Jugadores.ELO = (SELECT MAX(ELO) FROM Jugadores);");
			
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
				sprintf(respuesta, "%s es el jugador con mas elo, tiene %s",row[1],row[5]);
			}
		}
		else if(codigo == 6)
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
				sprintf(respuesta, "%s es el color de la carta +4",row[0]);
			}
		}
			// quieren saber si el nombre es bonito
			
		
		if (codigo != 0)
		{
			printf ("Respuesta: %s\n", respuesta);
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
	serv_adr.sin_port = htons(9050);
	
	// Forzamos el puerto, si no esta en uso anteriormente salta error con el comando fuser
	bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr));
	
	// Reiniciamos el puerto de escucha
    const char *comando = "fuser -k 9050/tcp";

    // Ejecutar el comando
    int resultado = system(comando);

    // Verificar si la ejecucion fue exitosa

    if (resultado == -1) 
	{
        perror("Error al ejecutar el comando\n");
    } 
	else 
	{
        printf("Puerto 9050 en TCP liberado.\n");
    }

	//Bind
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0) printf ("Posible error al bind\n");
	if (listen(sock_listen, 3) < 0) printf("Error en el Listen\n");
	
	contador = 0;
	int i;
	int sockets[100];
	pthread_t thread;

	// Bucle infinito
	for (;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		//sock_conn es el socket que usaremos para este cliente
		
		sockets[i] = sock_conn;
		pthread_create (&thread, NULL, AtenderCliente, &sockets[i]); 
	}
}
