#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>



int main(int argc, char *argv[])
{
	int err;
	MYSQL *conn;
	MYSQL_RES *res;
	MYSQL_ROW *row;
	//Creamos una conexion al servidor MYSQL
	conn = mysql_init(NULL);
	char consulta[512];
	
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

	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	char peticion[512];
	char respuesta[512];
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
	
	// Forzamos el puerto, si no est� en uso anteriormente salta error con el comando fuser
	bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr));
	
	// Reiniciamos el puerto de escucha

    const char *comando = "fuser -k 9050/tcp";

    // Ejecutar el comando
    int resultado = system(comando);

    // Verificar si la ejecucion fue exitosa

    if (resultado == -1) {
        perror("Error al ejecutar el comando");
    } else {
        printf("Puerto 9050 en TCP liberado.\n");
    }

	//Bind

	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0) printf ("Posible error al bind\n");
	if (listen(sock_listen, 3) < 0) printf("Error en el Listen");
	
	int i;
	// Bucle infinito
	for (;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		//sock_conn es el socket que usaremos para este cliente
		
		int terminar =0;
		// Entramos en un bucle para atender todas las peticiones de este cliente
		//hasta que se desconecte
		while (terminar ==0)
		{
			// Ahora recibimos la petici?n
			ret=read(sock_conn,peticion, sizeof(peticion));
			printf ("Recibido\n");
			
			// Tenemos que a?adirle la marca de fin de string 
			// para que no escriba lo que hay despues en el buffer
			peticion[ret]='\0';
			
			
			printf ("Peticion: %s\n",peticion);
			
			// vamos a ver que quieren
			char *p = strtok( peticion, "/");
			int codigo =  atoi (p);
			// Ya tenemos el c?digo de la peticion
			char nombre[20];
			char password[20];

			if (codigo != 0 && codigo <4)
			{
				p = strtok( NULL, "/");
				strcpy (nombre, p);

				p = strtok( NULL, "/");
				strcpy (password, p);

				printf ("Codigo: %d, Nombre: %s Password: %s\n", codigo, nombre, password);
			}
			
			if (codigo == 0)
			{
				// Peticion de desconexion
				terminar = 1;
				printf("Usuario se ha desconectado\n");
			}	
			else if (codigo ==1) // Sign up
			{
				strcpy(consulta,"SELECT * FROM Jugadores");
				
				err=mysql_query(conn,consulta);
				res = mysql_store_result(conn);
				row = mysql_fetch_row (res);
				int i = 0;
				while(row!=NULL)
				{
					i++;
					row = mysql_fetch_row (res);
					
				}
				
				
				printf("Hay un total de %d jugadores registrados\n",i);
				
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
					int z = i+1;
					char d[20];
					sprintf(d,"%d",z);
					
					
					//INSERT INTO Jugadores VALUES(1,'Joan','1',1312, 10, 500);
					strcpy (consulta, " INSERT INTO Jugadores VALUES(");
					strcat (consulta, d );
					strcat (consulta, ",'");
					strcat (consulta, nombre);
					strcat (consulta, "','");
					strcat (consulta, password);
					
					strcat (consulta, "', 0, 0, 1000);"); // El jugador empieza con 0 partidas ganadas y 0 jugadas, 1000 de ELO
					
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
					sprintf(respuesta,"%s","2");
				}
			}
			else if (codigo == 2)//Log in
			{
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
				if(row==NULL)
				{
					printf("No hay ningun usuario con ese nombre");
					sprintf(respuesta, "3");
				}else
				{
					
					
					if(strcmp(row[2],password)==0)
					{
						printf("Inicio sesion correctamente");
						sprintf(respuesta, "1");
					}else
					{
						printf("Contrase�a incorrecta");
						sprintf(respuesta, "2");
					}
				}
			// Log in
			}else if(codigo ==3)//remove
			{
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
				
				if(row==NULL)
				{
					printf("No hay ningun usuario con ese nombre");
					sprintf(respuesta, "3");
				}else{
					if(strcmp(row[2],password)==0)
					{
						sprintf(consulta, "DELETE FROM Jugadores WHERE Nombre = '%s'", nombre);
						// Ejecutar la consulta
						err = mysql_query(conn,consulta);
						res = mysql_store_result(conn);
						
						
						if(err !=0)
						{
							printf("Error al eliminar ese usuario");
							fprintf(stderr, "%s\n", mysql_error(conn));
							exit(1);
						}else{
							printf("Usuario eliminado");
							sprintf(respuesta,"1");
							
							
						}
						
					}else{
						printf("las contrase�as no coinciden");
						sprintf(respuesta,"2");
					}
				}
			}else if(codigo == 4)//Jugador con mas partidas
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
				
				if (row == NULL)
					printf ("No se han obtenido datos en la consulta\n");
				else{
					
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
			}else if(codigo == 5)
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
				else{
					sprintf(respuesta, "%s es el jugador con mas elo, tiene %s",row[1],row[5]);
				}
			}else if(codigo == 6)
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
				else{
					sprintf(respuesta, "%s es el color de la carta +4",row[0]);
				}
			}
				// quieren saber si el nombre es bonito
				
			
			if (codigo !=0)
			{
				printf ("Respuesta: %s\n", respuesta);
				// Enviamos respuesta
				write (sock_conn,respuesta, strlen(respuesta));
			}
		}
		// Se acabo el servicio para este cliente
		close(sock_conn); 
	}
	
}
