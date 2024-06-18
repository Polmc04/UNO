#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <pthread.h>
#include <stdbool.h> // Incluir para usar bool
#include <time.h>


#define MAX_MENSAJES 10000 // Máximo número de mensajes del chat
#define MAX_CARTAS 52  // El número máximo de cartas por jugador es 52
#define MAX_CARTAS_BARAJA 500 // El número máximo de cartas en la baraja central


int contador;

//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

int i;
int sockets[100];

// Estructura del chat
typedef struct {
    char *mensajes[MAX_MENSAJES]; // Puntero definido para poder tener mensajes de longitud variable
    int numMensajes;
} Chat;

// Lista de Concetados
typedef struct
{
    char nombre[20];
    int socket;
} Conectado;
typedef struct
{
	Chat chatGlobal;
    Conectado conectados [800];
    int num;
} ListaConectados;

ListaConectados miListaConectados;


// Lista de Salas
typedef struct
{
    char nombre[20];
    int socket;
	int cartas[MAX_CARTAS]; // Vector de enteros para los identificadores de las cartas que tiene cada jugador
    int numCartas;          // Número de cartas actualmente en posesión
} Jugador;
typedef struct
{
    Jugador jugadores[4]; // Máximo 4 jugadores por sala
    int num;
	int cartasBaraja[MAX_CARTAS_BARAJA]; // Vector de enteros para los identificadores de las cartas que hay en la baraja central
	int numCartasBaraja;   // Número de cartas actualmente en la baraja central
	int partidaEmpezada;
	Chat chat;
} Sala;
typedef struct
{
    Sala salas [20]; // Hay 20 salas como mucho
    int num;
} ListaSalas;

ListaSalas miListaSalas;

void insertarPartida(MYSQL *conn, char *jugadores, const char *ganador) {
    MYSQL_RES *res;
    MYSQL_ROW row;
    char query[1024];

    // Obtener la fecha actual
    time_t t = time(NULL);
    struct tm *tm_info = localtime(&t);
    char fecha_actual[11]; // YYYY-MM-DD
    strftime(fecha_actual, sizeof(fecha_actual), "%Y-%m-%d", tm_info);

    // Valor predeterminado para la hora y la duración
    const char *hora_predeterminada = "00:00:00";
    const int duracion_predeterminada = 0;

    // Iniciar una transacción
    if (mysql_query(conn, "START TRANSACTION")) {
        finish_with_error(conn);
    }

    // Obtener el identificador del ganador
    snprintf(query, sizeof(query), "SELECT Identificador FROM Jugadores WHERE Nombre='%s'", ganador);
    if (mysql_query(conn, query)) {
        finish_with_error(conn);
    }

    res = mysql_store_result(conn);
    if (res == NULL) {
        finish_with_error(conn);
    }

    row = mysql_fetch_row(res);
    if (row == NULL) {
        fprintf(stderr, "Ganador no encontrado\n");
        mysql_free_result(res);
        mysql_query(conn, "ROLLBACK");
        mysql_close(conn);
        exit(1);
    }
    int ganador_id = atoi(row[0]);
    mysql_free_result(res);

    // Insertar la nueva partida
    snprintf(query, sizeof(query),
             "INSERT INTO Partida (Fecha, Hora, Duracion, Ganador) VALUES ('%s', '%s', %d, %d)",
             fecha_actual, hora_predeterminada, duracion_predeterminada, ganador_id);

    if (mysql_query(conn, query)) {
        finish_with_error(conn);
    }

    // Obtener el identificador de la partida recién insertada
    int partida_id = mysql_insert_id(conn);

    // Copiar jugadores a jugadores_copy
    char jugadores_copy[strlen(jugadores) + 1];
	strcpy(jugadores_copy, jugadores);

    char *token = strtok(jugadores_copy, "/");
    
    // Obtener el número de jugadores
    int num_jugadores = atoi(token);
    if (num_jugadores <= 0) {
        fprintf(stderr, "Número de jugadores inválido\n");
        mysql_query(conn, "ROLLBACK");
        mysql_close(conn);
        exit(1);
    }

    token = strtok(NULL, "/"); // Avanzar al primer nombre de jugador

    // Insertar cada jugador en la tabla Partida_Jugadores
    for (int i = 0; i < num_jugadores; i++) {
        if (token == NULL) {
            fprintf(stderr, "Número de jugadores no coincide con la cantidad de nombres proporcionados\n");
            mysql_query(conn, "ROLLBACK");
            mysql_close(conn);
            exit(1);
        }

        // Obtener el identificador del jugador
        snprintf(query, sizeof(query), "SELECT Identificador FROM Jugadores WHERE Nombre='%s'", token);
        if (mysql_query(conn, query)) {
            finish_with_error(conn);
        }

        res = mysql_store_result(conn);
        if (res == NULL) {
            finish_with_error(conn);
        }

        row = mysql_fetch_row(res);
        if (row == NULL) {
            fprintf(stderr, "Jugador %s no encontrado\n", token);
            mysql_free_result(res);
            mysql_query(conn, "ROLLBACK");
            mysql_close(conn);
            exit(1);
        }
        int jugador_id = atoi(row[0]);
        mysql_free_result(res);

        // Insertar en Partida_Jugadores
        snprintf(query, sizeof(query), 
                 "INSERT INTO Partida_Jugadores (PartidaID, JugadorID) VALUES (%d, %d)",
                 partida_id, jugador_id);

        if (mysql_query(conn, query)) {
            finish_with_error(conn);
        }

        token = strtok(NULL, "/");
    }

    // Confirmar la transacción
    if (mysql_query(conn, "COMMIT")) {
        finish_with_error(conn);
    }

    printf("Partida insertada correctamente con ID %d\n", partida_id);
}
void finish_with_error(MYSQL *con) {
    fprintf(stderr, "%s\n", mysql_error(con));
    mysql_close(con);
    exit(1);
}
void obtenerPartidasEnPeriodo(MYSQL *conn, const char *nombre_jugador, const char *fecha_inicio, const char *fecha_fin, char *respuesta, size_t respuesta_size) {
    MYSQL_RES *res;
    MYSQL_ROW row;
    char query[1024];

    // Formatear la consulta SQL para seleccionar las partidas en el periodo dado
    snprintf(query, sizeof(query),
             "SELECT p.Identificador AS PartidaID, p.Fecha, p.Hora, p.Duracion, j2.Nombre AS Ganador "
             "FROM Partida p "
             "JOIN Partida_Jugadores pj ON p.Identificador = pj.PartidaID "
             "JOIN Jugadores j1 ON pj.JugadorID = j1.Identificador "
             "JOIN Jugadores j2 ON p.Ganador = j2.Identificador "
             "WHERE j1.Nombre = '%s' "
             "AND p.Fecha BETWEEN '%s' AND '%s';",
             nombre_jugador, fecha_inicio, fecha_fin);

    if (mysql_query(conn, query)) {
        finish_with_error(conn);
    }

    MYSQL_RES *result = mysql_store_result(conn);

    if (result == NULL) {
        finish_with_error(conn);
    }

    int num_fields = mysql_num_fields(result);
	
	respuesta[0] = '\0';
	strcpy(respuesta, "6/");

	while ((row = mysql_fetch_row(result))) {
        char row_data[1024];
        snprintf(row_data, sizeof(row_data), "%s %s %s %s %s\n", 
                 row[0] ? row[0] : "NULL", 
                 row[1] ? row[1] : "NULL", 
                 row[2] ? row[2] : "NULL",
                 row[3] ? row[3] : "NULL", 
                 row[4] ? row[4] : "NULL");
        strncat(respuesta, row_data, 8192 - strlen(respuesta) - 1); // Concatenate the row data to the response
    }

    mysql_free_result(result);
    mysql_close(conn);
}
void obtenerPartidasJugadas(MYSQL *conn, const char *nombre_jugador, const char *nombre_amigo, char *respuesta, size_t respuesta_size) {
    MYSQL_RES *res;
    MYSQL_ROW row;
    char query[1024];

    // Formatear la consulta SQL
    snprintf(query, sizeof(query), 
        "SELECT P.Identificador, P.Fecha, P.Hora, "
        "CASE WHEN P.Ganador = J1.Identificador THEN 'Sí' ELSE 'No' END AS Ganado "
        "FROM Partida P "
        "JOIN Partida_Jugadores PJ1 ON P.Identificador = PJ1.PartidaID "
        "JOIN Jugadores J1 ON PJ1.JugadorID = J1.Identificador "
        "JOIN Partida_Jugadores PJ2 ON P.Identificador = PJ2.PartidaID "
        "JOIN Jugadores J2 ON PJ2.JugadorID = J2.Identificador "
        "WHERE J1.Nombre = '%s' AND J2.Nombre = '%s';",
        nombre_jugador, nombre_amigo);

    // Ejecutar la consulta
    if (mysql_query(conn, query)) {
        fprintf(stderr, "Error al ejecutar la consulta: %s\n", mysql_error(conn));
        return;
    }

    // Obtener los resultados de la consulta
    res = mysql_store_result(conn);
    if (res == NULL) {
        fprintf(stderr, "Error al obtener los resultados: %s\n", mysql_error(conn));
        return;
    }

    // Inicializar la cadena respuesta
    respuesta[0] = '\0';
    int total_partidas = 0;

    // Procesar los resultados
    while ((row = mysql_fetch_row(res)) != NULL) {
        char resultado[256];
        snprintf(resultado, sizeof(resultado), "Partida %s: %s a las %s - Ganado: %s\n", row[0], row[1], row[2], row[3]);
        strncat(respuesta, resultado, respuesta_size - strlen(respuesta) - 1);
        total_partidas++;
    }

    // Agregar el total de partidas al inicio de la respuesta
    char total[50];
    snprintf(total, sizeof(total), "5/Total de partidas: %d\n", total_partidas);
    memmove(respuesta + strlen(total), respuesta, strlen(respuesta) + 1); // Mover la cadena actual
    memcpy(respuesta, total, strlen(total)); // Copiar el total al inicio

    // Liberar los resultados
    mysql_free_result(res);
}
void obtenerJugadoresConocidos(MYSQL *conn, const char *nombre, char *respuesta, size_t respuesta_size) {
    MYSQL_RES *res;
    MYSQL_ROW row;
    char query[512];
    char nombres[respuesta_size];
    int contador = 0;

    // Inicializar la cadena nombres
    nombres[0] = '\0';

    // Formatear la consulta SQL
    snprintf(query, sizeof(query), 
        "SELECT DISTINCT J2.Nombre "
        "FROM Jugadores J1 "
        "JOIN Partida_Jugadores PJ1 ON J1.Identificador = PJ1.JugadorID "
        "JOIN Partida_Jugadores PJ2 ON PJ1.PartidaID = PJ2.PartidaID "
        "JOIN Jugadores J2 ON PJ2.JugadorID = J2.Identificador "
        "WHERE J1.Nombre = '%s' AND J1.Identificador != J2.Identificador;", 
        nombre);

    // Ejecutar la consulta
    if (mysql_query(conn, query)) {
        fprintf(stderr, "Error al ejecutar la consulta: %s\n", mysql_error(conn));
        return;
    }

    // Obtener los resultados de la consulta
    res = mysql_store_result(conn);
    if (res == NULL) {
        fprintf(stderr, "Error al obtener los resultados: %s\n", mysql_error(conn));
        return;
    }

    // Procesar los resultados
    while ((row = mysql_fetch_row(res)) != NULL) {
        if (contador > 0) {
            strncat(nombres, "-", respuesta_size - strlen(nombres) - 1);
        }
        strncat(nombres, row[0], respuesta_size - strlen(nombres) - 1);
        contador++;
    }

    // Liberar los resultados
    mysql_free_result(res);

    // Construir la respuesta final
    snprintf(respuesta, respuesta_size, "%d/%s", contador, nombres);
}

void NotificarCambioBaraja(ListaSalas* listaSalas, int numSala, char* notificacion) 
{
    if (numSala >= listaSalas->num || numSala < 0) 
	{
        return; // Sala no válida
    }
    
    Sala* sala = &listaSalas->salas[numSala];
    char buffer[256];

    // Empieza la notificación con "7/(sala)/(num cartas en baraja central)"
    snprintf(notificacion, sizeof(buffer), "7/%d/%d", numSala, sala->numCartasBaraja);

    // Añade las cartas de la baraja central
    for (int i = 0; i < sala->numCartasBaraja; i++) // Para cada carta en la baraja central 
	{
        snprintf(buffer, sizeof(buffer), "-%d", sala->cartasBaraja[i]);
        strncat(notificacion, buffer, sizeof(buffer) - strlen(buffer) - 1);
    }

    // Añade el número de jugadores
    snprintf(buffer, sizeof(buffer), "/%d", sala->num);
    strncat(notificacion, buffer, sizeof(buffer) - strlen(buffer) - 1);

    // Añade el número de cartas de cada jugador
    for (int i = 0; i < sala->num; i++) // Para cada jugador en la sala 
	{
        snprintf(buffer, sizeof(buffer), "-%s-%d", sala->jugadores[i].nombre, sala->jugadores[i].numCartas);
        strncat(notificacion, buffer, sizeof(buffer) - strlen(buffer) - 1);
    }
}
int manejarCartaBaraja(ListaSalas* listaSalas, int numSala, const char* nombreJugador, int carta, int accion) 
{
    if (numSala >= listaSalas->num || numSala < 0) 
	{
        return -1; // Sala no válida
    }
    
    Sala* sala = &listaSalas->salas[numSala];
    
	if (sala->numCartasBaraja >= MAX_CARTAS_BARAJA) // Comprobar si la baraja central está llena
	{
		return -2; 
	}

    for (int i = 0; i < sala->num; i++) // Para todos los jugadores
	{
        if (strcmp(sala->jugadores[i].nombre, nombreJugador) == 0) // Si coincide el nombre
		{
            Jugador* jugador = &sala->jugadores[i];

			if (accion == 1) // Agregar carta a la baraja central y sacarla del jugador
			{
				int found = 0;
				for (int j = 0; j < jugador->numCartas; j++) // Para todas las cartas del jugador
				{
					if (jugador->cartas[j] == carta) // Si coincide la carta
					{
						found = 1;
						// Mover las cartas restantes para llenar el hueco
						for (int k = j; k < jugador->numCartas - 1; k++) 
						{
							jugador->cartas[k] = jugador->cartas[k + 1];
						}
						jugador->numCartas--;
						break;
					}
				}

				if (!found)
				{
					return -3; // Carta no encontrada en la mano del jugador
				}
				// Agregar la carta a la baraja central
				sala->cartasBaraja[sala->numCartasBaraja] = carta;
				sala->numCartasBaraja++;

				printf("Se ha metido la carta %d en la baraja\n", carta);
				return 0;
        	}
			else if (accion == 2) // Sacar carta de la baraja central y devolverla al jugador
			{
                if (sala->numCartasBaraja == 0)
				{
                    return -4; // La baraja central está vacía
                }

                int found = 0;
                for (int j = 0; j < sala->numCartasBaraja; j++) // Para cada carta de la baraja central
				{
                    if (sala->cartasBaraja[j] == carta) // Si coincide la carta
					{
                        found = 1;
                        // Mover las cartas restantes para llenar el hueco
                        for (int k = j; k < sala->numCartasBaraja - 1; k++) {
                            sala->cartasBaraja[k] = sala->cartasBaraja[k + 1];
                        }
                        sala->numCartasBaraja--;
                        break; // Salir del bucle una vez que la carta ha sido encontrada y eliminada
                    }
                }
                if (!found) 
				{
                    return -5; // Carta no encontrada en la baraja central
                }

                if (jugador->numCartas >= MAX_CARTAS) 
				{
                    return -6; // La mano del jugador está llena
                }

                // Devolver la carta a la mano del jugador
                jugador->cartas[jugador->numCartas] = carta;
                jugador->numCartas++;

				printf("Se ha sacado la carta %d de la baraja\n", carta);
                return 0; // Éxito
            } 
			else 
			{
                return -7; // Acción no válida
            }
		}
    }
    
    return -4; // Jugador no encontrado en la sala
}
void agregarCarta(ListaSalas *lista, int idCarta, char nombre[20], int sala) {
    // Verifica si el número de sala está dentro del rango
    if (sala >= 0 && sala < lista->num) {
        Sala *salaActual = &lista->salas[sala];

        // Busca el jugador por nombre en la sala
        for (int i = 0; i < salaActual->num; i++) 
		{
            if (strcmp(salaActual->jugadores[i].nombre, nombre) == 0) 
			{
                // Verifica si el jugador no tiene demasiadas cartas
                if (salaActual->jugadores[i].numCartas < MAX_CARTAS) 
				{
                    // Agrega la carta al jugador
                    salaActual->jugadores[i].cartas[salaActual->jugadores[i].numCartas] = idCarta;
                    salaActual->jugadores[i].numCartas++;
                    printf("Carta %d agregada al jugador %s en la sala %d\n", idCarta, nombre, sala);
                } 
				else 
				{
                    printf("El jugador %s ya tiene el número máximo de cartas\n", nombre);
                }
                return;
            }
        }
        printf("Jugador %s no encontrado en la sala %d\n", nombre, sala);
    } 
	else 
	{
        printf("Número de sala %d no válido\n", sala);
    }
}
void AgregarMensajeGlobal(ListaConectados *lista, char nombre[20], char mensaje[100]) 
{
	// Verifica si el chat de la sala no está lleno
	if (lista->chatGlobal.numMensajes < MAX_MENSAJES) {
        // Formatea el mensaje completo con el nombre del usuario
        char mensajeCompleto[150];
        snprintf(mensajeCompleto, sizeof(mensajeCompleto), "%s: %s", nombre, mensaje);

        // Asigna memoria para el nuevo mensaje
        lista->chatGlobal.mensajes[lista->chatGlobal.numMensajes] = malloc(strlen(mensajeCompleto) + 1);
        
        // Copia el mensaje formateado al chat global
        strcpy(lista->chatGlobal.mensajes[lista->chatGlobal.numMensajes], mensajeCompleto);
        
        // Incrementa el número de mensajes en el chat global
        lista->chatGlobal.numMensajes++;
    } 
	else 
	{
        printf("El chat global está lleno\n");
    }
}
void DameChatGlobal(ListaConectados *lista, char chat[1000000])
{
	// Inicializa el vector chat
    chat[0] = '\0';

    // Añade el número de mensajes al inicio del vector
    char numMensajesStr[12];
    sprintf(numMensajesStr, "%d", lista->chatGlobal.numMensajes);
    strcat(chat, numMensajesStr);
    strcat(chat, "/");

    // Añade cada mensaje al vector, separado por "/"
    for (int i = 0; i < lista->chatGlobal.numMensajes; i++) {
        strcat(chat, lista->chatGlobal.mensajes[i]);
        strcat(chat, "/");
    }
}
void AgregarMensaje(ListaSalas *lista, char nombre[20], char mensaje[100], int sala) 
{
    // Verifica si el número de sala está dentro del rango
    // Verifica si el número de sala está dentro del rango
    if (sala >= 0 && sala < lista->num) {
        // Verifica si el chat de la sala no está lleno
        if (lista->salas[sala].chat.numMensajes < MAX_MENSAJES) {
            // Agrega el mensaje al chat de la sala de la forma "Nombre: mensaje"

			// Cambiamos el mensaje con el formato correcto
			char mensajeBuff[100];
			strcpy(mensajeBuff,mensaje);
			sprintf(mensaje, "%s: %s", nombre, mensajeBuff);
            Chat *chatSala = &lista->salas[sala].chat;
            chatSala->mensajes[chatSala->numMensajes] = malloc(strlen(mensaje) + 1); // Gestión de memoria
            strcpy(chatSala->mensajes[chatSala->numMensajes], mensaje);
			printf("El mensaje %s se ha introducido en el chat de la sala %d\n", chatSala->mensajes[chatSala->numMensajes], sala);
            chatSala->numMensajes++;
        } else {
            printf("El chat de la sala %s está lleno\n", nombre);
        }
    } else {
        printf("La sala %d no existe\n", sala);
    }
}
void DameChat(ListaSalas *lista, char chat[1000000], int sala)
{
	// Devuelve el chat en forma (num mensajes)/(mensaje 1)/(mensaje 2)

	// Verifica si el índice de la sala está dentro del rango
    if (sala >= 0 && sala < lista->num) {
        // Obtiene el chat de la sala específica
        Chat *chatSala = &lista->salas[sala].chat;

        // Inicializa el chat como una cadena vacía
        chat[0] = '\0';
		
		// Concatena el número de mensajes al inicio de la cadena
        sprintf(chat, "%d/", chatSala->numMensajes);

        // Concatena todos los mensajes del chat en la cadena proporcionada
        for (int i = 0; i < chatSala->numMensajes; i++) {
            strcat(chat, chatSala->mensajes[i]); // Metemos el siguiente mensaje
            strcat(chat, "/"); // Agrega un separador "/" después de cada mensaje
        }
    } else {
        printf("La sala no existe\n");
    }
}
int CreaSala(ListaSalas *lista, char usuario[20])
{
	// Crea una sala
	// -1 si no hay espacio para mas salas
	// x si ha podido crear la sala siendo x el numero de sala

	int posicion;
	if (lista->num == 20) return -1; // No se pueden crear más salas
	else
	{
		posicion = lista->num; // Posicion en el vector de salas en la que vamos a insertar la nueva sala
		strcpy(lista->salas[posicion].jugadores[0].nombre, usuario); // El usuario que crea la sala
		printf("Usuario: %s se ha metido en la sala %d\n", lista->salas[posicion].jugadores[0].nombre, posicion);
		lista->salas[posicion].num = 1; // Hay un jugador en la nueva sala
		lista->salas[posicion].partidaEmpezada = 0; // La partida no se ha empezado
		lista->num++; // Incrementamos en 1 el numero de salas que hay
	}
	return posicion;
}
int EliminarSala(ListaSalas *lista, int sala)
{
	// Elimina la sala que le pasemos
	// -1 si no existe
	// 0 si la elimina

	lista->salas[sala] = lista->salas[sala+1];
	lista->num--; // Restamos 1 al numero de salas
	return 0;
}
int PonUsuarioSala(ListaSalas *lista, char usuario[20], int sala)
{
	// Mete a un usuario en una sala
	// 0 si lo mete
	// -1 si sala llena
	// -2 si lista de salas llena

	if (lista->salas[sala].num == 4) return -1; // Sala llena
	else if (lista->num == 20) return -2;// Servidor lleno, no pueden haber más salas

	int posicion = lista->salas[sala].num; // Buscamos la posicion en la que hay que meter a cada jugador dentro de la sala
	strcpy(lista->salas[sala].jugadores[posicion].nombre,usuario); // Guardamos el nombre
	lista->salas[sala].num++; // Sumamos 1 al numero de jugadores en esa sala

	printf("Se ha metido a %s en la sala %d\n", usuario, sala);
	return 0;
}
int SacaUsuarioSala(ListaSalas *lista, char usuario[20], int sala)
{
	// Saca a un jugador de la sala en la que esté
	// -1 no encuentra al jugador
	// 0 si lo saca

	int posicion = BuscaUsuario(lista, usuario, sala);
    printf("posicion en SacaUsuarioSala: %d\n", posicion);
	if (sala == -1)
	{
		printf("El jugador no esta en ninguna sala\n");
		return -1; // El jugador no está en ninguna sala
	}
	else
	{
		for (int i = posicion; i < lista->salas[sala].num; i++) // Recorremos jugadores en sala
        {
            // En la posicion del Eliminado copiamos la información del siguiente en la lista
            lista->salas[sala].jugadores[i] = lista->salas[sala].jugadores[i + 1];
        }
		miListaSalas.salas[sala].num--;// Se reduce en 1 el numero de jugadores en esa sala
		printf("Se ha sacado al jugador %s de la sala\n", usuario);
	}
	return 0;
}
int BuscaSalaUsuario(ListaSalas *lista, char usuario[20])
{
	// Busca la sala en la que se encuentra un usuario
	// -1 si no lo encuentra
	// x si lo encuentra, siendo x la sala en la que está

	int encontrado = 0;
	int i = 0;
	printf("Hay %d salas\n", lista->num);
	while (i < lista->num && !encontrado) // Buscamos en todas las salas
	{
		int j = 0;
		printf("Hay %d jugadores en la sala %d\n", lista->salas[i].num, i);
		while (j < lista->salas[i].num && !encontrado) // Buscamos entre todos los jugadores
		{
			if (strcmp(lista->salas[i].jugadores[j].nombre, usuario) == 0) encontrado = 1;
			j++;
		}
		if (!encontrado) i++; // si no lo encontramos pasamos a la siguiente sala
	}

	if (encontrado) return i;
	else return -1;
}
int BuscaUsuario (ListaSalas *lista, char usuario[20], int sala)
{
	// Busca dentro de una sala a un usuario
	// -1 si no lo encuentra
	// x si lo encuentra donde x es la posicion
	
	int encontrado = 0;
	int i = 0;
	while (i < lista->salas[sala].num && !encontrado) // Buscamos entre todos los jugadores de una misma sala
	{
		if (strcmp(lista->salas[sala].jugadores[i].nombre, usuario) == 0) encontrado = 1;
		if (!encontrado) i++; // si no lo encontramos pasamos a la siguiente sala
	}

	if (encontrado) return i;
	else return -1;

}
void DameJugadores(ListaSalas *lista, char jugadores[800], int sala)
{
    // Escribe en jugadores el número de jugadores y sus nombres de una sala
    // "3/Pol/Joan/Alonso"

	jugadores[0] = '\0'; // Reiniciamos el vector

    sprintf(jugadores, "%d", lista->salas[sala].num);
    if (lista->num == 0) printf("No queda nadie en la sala %d\n", sala);
    for (int i = 0; i < lista->salas[sala].num; i++)
    {
		sprintf(jugadores, "%s/%s",jugadores, lista->salas[sala].jugadores[i].nombre);
		printf("%s esta en sala\n", lista->salas[sala].jugadores[i].nombre);
    }
	printf("DameJugadores: %s\n",jugadores);
}

int Pon(ListaConectados *lista, char nombre[20], int socket)
{
	// Añade nuevo conectado
    // Return 0 si OK
    //       -1 si no hay sitio en la lista
	//		 -2 si ya estaba en la lista de conectados
    if (lista->num == 800) return -1;
	else if (DamePosicion(lista, nombre) != -1/*Si no devuelve -1 significa que ha encontrado a un usuario con ese nombre*/) return -2;
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
	printf("Hay %d conectados\n", lista->num);
    while(!encontrado && i < lista->num)
    {
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
        if (lista->conectados[i].socket == socket) encontrado = 1;
        if (!encontrado) i++;
    }
    if (encontrado) return i;
    else return -1;
}
int Elimina(ListaConectados *lista, char nombre[20])
{
    // Return 0 si elimina
    //       -1 si no está en la lista
	
    int pos = DamePosicion (lista, nombre);
    if (pos == -1) return -1;
    else
    {
        for (int i = pos; i<lista->num;i++)
        {
            lista->conectados[i] = lista->conectados[i+1];
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
        for (int i = pos; i<lista->num;i++)
        {
            lista->conectados[i] = lista->conectados[i+1];
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
	char notificacion[1024];
	char notificacionInvitacion[800];
	char notificacionSala[800];

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
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "M03_BBDD", 0, NULL, 0); // Desarrollo
	if (conn==NULL) 
	{
		printf ("Error al inicializar la conexion: %u %s\n",
		mysql_errno(conn), mysql_error(conn));
		exit (1);
	}

	char consulta[512]; // Consultas SQL se guardaran aqui

	int ret;
	int terminar = 0;

	char nombre[20] = "-1";
	char password[20];

	int sala;
	char mensajeChat[100] = "\0";

	char conectados[800];
	char jugadores[800];


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
		printf("Codigo: %d\n",codigo);
		
		if (codigo != 0 && codigo < 7  || codigo == 10)
		{
			p = strtok( NULL, "/");
			strcpy (nombre, p);

			printf("Nombre: %s\n", nombre);

			if (codigo != 0 && codigo < 4)
			{
				p = strtok( NULL, "/");
				strcpy (password, p);
				printf ("Password: %s\n", password);
			}
		}
		else if(codigo >= 11 && codigo != 16 || codigo == 8 || codigo == 7)
		{
			p = strtok( NULL, "/");
			sala = atoi(p);
			printf("Sala: %d\n", sala);
			if(codigo != 17)
			{
				p = strtok( NULL, "/");
				strcpy (nombre, p);
				if (codigo == 15)
				{
					p = strtok( NULL, "/");
					strcpy (mensajeChat, p);
					printf("Mensaje Chat: %s\n", mensajeChat);
				}
			}
		}
		else if (codigo == 16)
		{
			p = strtok( NULL, "/");
			strcpy (nombre, p);
			p = strtok( NULL, "/");
			strcpy (mensajeChat, p);
			printf("Mensaje Chat Global: %s\n", mensajeChat);
		}
		if (codigo == 0) // Peticion de desconexion
		{
			terminar = 1;
			
			// Encuentra el índice del socket a eliminar
			int index_to_remove = -1;
			for (int j = 0; j < i; j++) // Para todos los sockets abiertos 
			{
				if (sockets[j] == sock_conn) // Si el socket coincide con el del cliente 
				{
					index_to_remove = j;
					break;
				}
			}

			if (index_to_remove != -1) // Si encontramos el socket lo eliminamos
			{
				for (int j = index_to_remove; j < i; j++) // Para todos los sockets abiertos
				{
					sockets[j] = sockets[j + 1];
				}
				i--;  // Reducimos el tamaño del array de sockets en 1
			} 
			else 
			{
				printf("Socket no encontrado.\n");
			}

			// Quitar de la lista de conectados si se ha conectado
			if (!(strcmp(nombre, "-1") == 0)) // Si hay nombre
			{
				pthread_mutex_lock( &mutex);
				int resultado = EliminaSocket(&miListaConectados, sock_conn); // Quitamos de conectados
				pthread_mutex_unlock( &mutex);

				printf("Desconectando al usuario: %s\n", nombre);
				if(resultado == -1) printf("Error al quitar de la lista de conectados (Posiblemente no hayas logged in)\n");
				else printf("Se ha quitado a %s de la lista de conectados\n", nombre);

				// Notificacion automatica de conectados
				pthread_mutex_lock( &mutex);
				DameConectados(&miListaConectados, conectados);
				pthread_mutex_unlock( &mutex);
				sprintf(notificacion, "9/%s", conectados);
				printf("Notificacion Conectados: %s\n", notificacion);
				
				for (int j=0; j < i; j++) // Para cada cliente conectado
				{
					write (sockets[j],notificacion, strlen(notificacion));
				}
			}
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
					// Intentamos insertar en la lista de conectados
					pthread_mutex_lock( &mutex);
					int resultado = Pon(&miListaConectados, nombre, sock_conn/*Socket en el que se conecta*/);
					pthread_mutex_unlock( &mutex);
					if (resultado == -1)
					{
						printf("No se ha podido insertar a %s en la lista de conectados (Lista llena)\n", nombre);
						sprintf(respuesta, "2/5");
					}
					else if (resultado == -2)
					{
						printf("No se ha podido insertar a %s en la lista de conectados (ya estaba conectado)\n", nombre);
						sprintf(respuesta, "2/4");
					} 
					else
					{
						printf("Inicio sesion correctamente\n");
						sprintf(respuesta, "2/1");

						printf("Se ha insertado a %s en conectados\n", nombre);

						// Notificacion automatica de conectados
						pthread_mutex_lock( &mutex);
						DameConectados(&miListaConectados, conectados);
						pthread_mutex_unlock( &mutex);
						sprintf(notificacion, "9/%s", conectados);
						printf("Notificacion Conectados: %s\n", notificacion);
						
						for (int j=0; j < i; j++) // Para cada cliente conectado
						{
							write (sockets[j],notificacion, strlen(notificacion));
						}
					}
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

						// Lo sacamos de todas las salas en las que este jugando

						int z = 0;
						while(z < miListaSalas.num) // Para cada sala que haya
						{	
							pthread_mutex_lock( &mutex);
							int posicion = BuscaUsuario(&miListaSalas, nombre, z);
							pthread_mutex_unlock( &mutex);
							if (posicion != -1)
							{
								pthread_mutex_lock( &mutex);
								SacaUsuarioSala(&miListaSalas, nombre, z); // Lo sacamos
								pthread_mutex_unlock( &mutex);

								// Confirmamos al jugador que lo hemos sacado de la sala
								sprintf(notificacion, "12/%d", z);
								write (sock_conn, notificacion, strlen(notificacion));

								if (miListaSalas.salas[z].partidaEmpezada == 0) // Si la partida no se ha empezado
								{
									for (int i = 0; i < miListaSalas.salas[z].num; i++) // Para cada jugador en la sala
									{
										printf("Buscamos al jugador %s\n", miListaSalas.salas[z].jugadores[i].nombre);
										pthread_mutex_lock( &mutex);
										int posicion = DamePosicion(&miListaConectados, &miListaSalas.salas[z].jugadores[i].nombre); // Buscamos jugador en conectados
										pthread_mutex_unlock( &mutex);
										int socketJugador = miListaConectados.conectados[posicion].socket; // Buscamos su socket

										pthread_mutex_lock( &mutex);
										DameJugadores(&miListaSalas, jugadores, z);
										pthread_mutex_unlock( &mutex);

										sprintf(notificacionSala, "14/%d", z);
										sprintf(notificacionSala, "%s/%s", notificacionSala, jugadores);
										printf("Notificacion sala: %s", notificacionSala);

										if(sock_conn != socketJugador) // El usuario que sale no necesita los jugadores que hay en sala
										{
											write (socketJugador, notificacionSala, strlen(notificacionSala));
										}
									}
									
								}
								else // Si ya se ha empezado la partida
								{
									// Notificamos a los jugadores de la sala los cambios
									pthread_mutex_lock( &mutex);
									NotificarCambioBaraja(&miListaSalas, z, notificacion);
									pthread_mutex_unlock( &mutex);
									printf("Notificacion: %s\n", notificacion);
									jugadores[0] = '\0'; // Reiniciamos vector

									pthread_mutex_lock( &mutex);
									DameJugadores(&miListaSalas, jugadores, z);
									pthread_mutex_unlock( &mutex);

									// Para cada jugador encontramos su socket y le enviamos la notificacion
									char *t = strtok( jugadores, "/");
									int numJugadores =  atoi (t);
									char jugador[20];
									for (int i = 0; i < numJugadores; i++)
									{
										// Obtenemos nombre del jugador
										t = strtok( NULL, "/");
										strcpy (jugador, t);

										// Buscamos el socket del jugador
										printf("Buscamos al jugador %s\n", miListaSalas.salas[z].jugadores[i]);
										pthread_mutex_lock( &mutex);
										int posicion = DamePosicion(&miListaConectados, &miListaSalas.salas[z].jugadores[i]); // Buscamos jugador en conectados
										pthread_mutex_unlock( &mutex);
										int socketJugador = miListaConectados.conectados[posicion].socket; // Buscamos su socket

										if(sock_conn != socketJugador) // Para no saturar al usuario que pide carta
										{
											write (socketJugador, notificacion, strlen(notificacion));
										}
										// Reiniciamos nombre
										jugador[0] = '\0';
									}
								}
							}
						}

						// Lo quitamos de la lista de conectados
						pthread_mutex_lock( &mutex);
						int resultado = Elimina (&miListaConectados, nombre);
						pthread_mutex_unlock( &mutex);
						if(resultado == -1) printf("Error al quitar de la lista de conectados (Posiblemente no hayas logged in)\n");
						else printf("Se ha quitado a %s de la lista de conectados\n", nombre);

						// Notificacion automatica de conectados
						char notificacion[1024];
						pthread_mutex_lock( &mutex);
						DameConectados(&miListaConectados, conectados);
						pthread_mutex_unlock( &mutex);
						sprintf(notificacion, "9/%s", conectados);
						printf("Notificacion Conectados: %s\n", conectados);

						for (int j=0; j < i; j++) // Para cada cliente conectado
							write (sockets[j],notificacion, strlen(notificacion));
						
					}
				}
				else
				{
					printf("Password incorrecto\n");
					sprintf(respuesta,"3/2");
				}
			}
		}
		else if(codigo == 4)// Listado de jugadores con los que he echado alguna partida
		{
			obtenerJugadoresConocidos(conn, nombre, respuesta, sizeof(respuesta));
		}
		else if(codigo == 5) // Resultados de las partidas que jugué con un jugador (o jugadores) determinado.
		{
			// Obtener nombres de los amigos
			char nombre_amigo[1000];
			p = strtok( NULL, "/");
			strcpy (nombre_amigo, p);
			printf("Jugador con el que has jugado la partida: %s\n", nombre_amigo);

			obtenerPartidasJugadas(conn, nombre, nombre_amigo, respuesta, sizeof(respuesta));
		}
		else if(codigo == 6) // Lista de partidas jugadas en un periodo de tiempo dado.
		{
			char fecha_inicio[100];
			char fecha_fin[100];

			p = strtok( NULL, "/");
			strcpy (fecha_inicio, p);
			printf("Fecha inicio: %s\n", fecha_inicio);
			p = strtok( NULL, "/");
			strcpy (fecha_fin, p);
			printf("Fecha fin: %s\n", fecha_fin);

			obtenerPartidasEnPeriodo(conn, nombre, fecha_inicio, fecha_fin, respuesta, sizeof(respuesta)); 
		}
		else if (codigo == 7) // Recibimos carta tirada por jugador
		// 7/(sala)/(nombre)/(tirar o recoger)/(id carta)
		// Si el jugador quiere tirar envia 1, si quiere recoger envia 2
		{
			p = strtok( NULL, "/");
			int accion = atoi(p);
			printf("Accion: %d\n", accion);

			p = strtok( NULL, "/");
			int id = atoi(p);
			printf("Id carta: %d\n", id);

			// Metemos o sacamos la carta en la sala correspondiente
			pthread_mutex_lock( &mutex);
			manejarCartaBaraja(&miListaSalas, sala, nombre, id, accion);
			pthread_mutex_unlock( &mutex);

			// Notificamos a los jugadores de la sala los cambios
			pthread_mutex_lock( &mutex);
			NotificarCambioBaraja(&miListaSalas, sala, notificacion);
			pthread_mutex_unlock( &mutex);
			printf("Notificacion: %s\n", notificacion);

			jugadores[0] = '\0'; // Reiniciamos vector
			pthread_mutex_lock( &mutex);
			DameJugadores(&miListaSalas, jugadores, sala);
			pthread_mutex_unlock( &mutex);

			// Para cada jugador encontramos su socket y le enviamos la notificacion
			char *z = strtok( jugadores, "/");
			int numJugadores =  atoi (z);
			char jugador[20];

			for (int i = 0; i < numJugadores; i++)
			{
				// Obtenemos nombre del jugador
				z = strtok( NULL, "/");
				strcpy (jugador, z);

				// Buscamos el socket del jugador
				printf("Buscamos al jugador %s\n", miListaSalas.salas[sala].jugadores[i]);
				pthread_mutex_lock( &mutex);
				int posicion = DamePosicion(&miListaConectados, &miListaSalas.salas[sala].jugadores[i]); // Buscamos jugador en conectados
				pthread_mutex_unlock( &mutex);
				int socketJugador = miListaConectados.conectados[posicion].socket; // Buscamos su socket

				if(sock_conn != socketJugador) // Para no saturar al usuario que mete o saca la carta
				{
					write (socketJugador, notificacion, strlen(notificacion));
				}
				// Reiniciamos nombre
				jugador[0] = '\0';
			}
		}  
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

    		// Generar un número aleatorio entre 1 y 54
    		int rnd = (rand() % 54) + 1;
			
			// Imprimir el número aleatorio guardado en el string
    		printf("El numero aleatorio es: %d\n", rnd);

			pthread_mutex_lock( &mutex);
			agregarCarta(&miListaSalas, rnd, nombre, sala);
			pthread_mutex_unlock( &mutex);

    		// Guardar el número aleatorio en Respuesta
    		sprintf(respuesta, "8/%d/%d", sala, rnd);

			printf("Respuesta: %s\n", respuesta);
			// Enviamos la carta al cliente
			write (sock_conn,respuesta, strlen(respuesta));

			// Notificamos a los jugadores de la sala los cambios
			pthread_mutex_lock( &mutex);
			NotificarCambioBaraja(&miListaSalas, sala, notificacion);
			pthread_mutex_unlock( &mutex);
			printf("Notificacion: %s\n", notificacion);
			jugadores[0] = '\0'; // Reiniciamos vector

			pthread_mutex_lock( &mutex);
			DameJugadores(&miListaSalas, jugadores, sala);
			pthread_mutex_unlock( &mutex);

			// Para cada jugador encontramos su socket y le enviamos la notificacion
			char *z = strtok( jugadores, "/");
			int numJugadores =  atoi (z);
			char jugador[20];
			for (int i = 0; i < numJugadores; i++)
			{
				// Obtenemos nombre del jugador
				z = strtok( NULL, "/");
				strcpy (jugador, z);

				// Buscamos el socket del jugador
				printf("Buscamos al jugador %s\n", miListaSalas.salas[sala].jugadores[i]);
				pthread_mutex_lock( &mutex);
				int posicion = DamePosicion(&miListaConectados, &miListaSalas.salas[sala].jugadores[i]); // Buscamos jugador en conectados
				pthread_mutex_unlock( &mutex);
				int socketJugador = miListaConectados.conectados[posicion].socket; // Buscamos su socket

				if(sock_conn != socketJugador) // Para no saturar al usuario que pide carta
				{
					write (socketJugador, notificacion, strlen(notificacion));
				}
				// Reiniciamos nombre
				jugador[0] = '\0';
			}
		}
		else if (codigo == 9) // Lista de conectados
		{
			pthread_mutex_lock( &mutex);
			DameConectados(&miListaConectados, conectados);
			pthread_mutex_unlock( &mutex);
			sprintf(respuesta, "9/%s", conectados);
			printf(conectados);
			printf("\n");
		}
		else if (codigo == 10) // Crear sala
		{
			// Usuario crea una sala en la que va a poder invitar a conectados
			pthread_mutex_lock( &mutex);
			sala = CreaSala(&miListaSalas, nombre);
			pthread_mutex_unlock( &mutex);
			if (sala == -1) sprintf(respuesta, "10/-1");
			else sprintf(respuesta, "10/%d", sala);
		}
		else if (codigo == 11) // Invitar a sala
		{
			// Enviamos notificación de invitación al invitado con todos los nombres de los jugadores en la sala
			// 11/(int numero de sala)/(int numero de jugadores)/Pol/Joan/Alonso
			pthread_mutex_lock( &mutex);
			int posicion = DamePosicion(&miListaConectados, nombre); // Buscamos al usuario invitado
			pthread_mutex_unlock( &mutex);
			int socketInvitado = miListaConectados.conectados[posicion].socket;

			pthread_mutex_lock( &mutex);
			DameJugadores(&miListaSalas, jugadores, sala);
			pthread_mutex_unlock( &mutex);

			sprintf(notificacionInvitacion, "11/%d", sala);
			sprintf(notificacionInvitacion, "%s/%s", notificacionInvitacion, jugadores);
			write (socketInvitado, notificacionInvitacion, strlen(notificacionInvitacion));
		}
		else if (codigo == 12) // Abandonar sala
		{
			// Sacamos al jugador de la sala

			pthread_mutex_lock( &mutex);
			SacaUsuarioSala(&miListaSalas, nombre, sala);
			pthread_mutex_unlock( &mutex);

			// Confirmamos al jugador que lo hemos sacado de la sala
			sprintf(respuesta, "12/%d", sala);

			// Enviamos notificacion a todos los usuarios conectados a esa sala
			// Les damos los nombres de los jugadores que hay en sala

			if (miListaSalas.salas[sala].partidaEmpezada == 0) // Si la partida no se ha empezado
			{
				for (int i = 0; i < miListaSalas.salas[sala].num; i++) // Para cada jugador en la sala
				{
					printf("Buscamos al jugador %s\n", miListaSalas.salas[sala].jugadores[i].nombre);
					pthread_mutex_lock( &mutex);
					int posicion = DamePosicion(&miListaConectados, &miListaSalas.salas[sala].jugadores[i].nombre); // Buscamos jugador en conectados
					pthread_mutex_unlock( &mutex);
					int socketJugador = miListaConectados.conectados[posicion].socket; // Buscamos su socket

					pthread_mutex_lock( &mutex);
					DameJugadores(&miListaSalas, jugadores, sala);
					pthread_mutex_unlock( &mutex);

					sprintf(notificacionSala, "14/%d", sala);
					sprintf(notificacionSala, "%s/%s", notificacionSala, jugadores);
					printf("Notificacion sala: %s", notificacionSala);

					if(sock_conn != socketJugador) // El usuario que sale no necesita los jugadores que hay en sala
					{
						write (socketJugador, notificacionSala, strlen(notificacionSala));
					}
				}
				
			}
			else // Si ya se ha empezado la partida
			{
				// Notificamos a los jugadores de la sala los cambios
				pthread_mutex_lock( &mutex);
				NotificarCambioBaraja(&miListaSalas, sala, notificacion);
				pthread_mutex_unlock( &mutex);
				printf("Notificacion: %s\n", notificacion);
				jugadores[0] = '\0'; // Reiniciamos vector

				pthread_mutex_lock( &mutex);
				DameJugadores(&miListaSalas, jugadores, sala);
				pthread_mutex_unlock( &mutex);

				// Para cada jugador encontramos su socket y le enviamos la notificacion
				char *z = strtok( jugadores, "/");
				int numJugadores =  atoi (z);
				char jugador[20];
				for (int i = 0; i < numJugadores; i++)
				{
					// Obtenemos nombre del jugador
					z = strtok( NULL, "/");
					strcpy (jugador, z);

					// Buscamos el socket del jugador
					printf("Buscamos al jugador %s\n", miListaSalas.salas[sala].jugadores[i]);
					pthread_mutex_lock( &mutex);
					int posicion = DamePosicion(&miListaConectados, &miListaSalas.salas[sala].jugadores[i]); // Buscamos jugador en conectados
					pthread_mutex_unlock( &mutex);
					int socketJugador = miListaConectados.conectados[posicion].socket; // Buscamos su socket

					if(sock_conn != socketJugador) // Para no saturar al usuario que pide carta
					{
						write (socketJugador, notificacion, strlen(notificacion));
					}
					// Reiniciamos nombre
					jugador[0] = '\0';
				}
			}
			
		}
		else if (codigo == 13) // Entrar a sala
		{
			PonUsuarioSala(&miListaSalas, nombre, sala);

			// Enviamos notificacion a todos los usuarios conectados a esa sala
			// Les damos los nombres de los jugadores que hay en sala

			for (int i = 0; i < miListaSalas.salas[sala].num; i++) // Para cada jugador en la sala
			{
				printf("Buscamos al jugador %s\n", miListaSalas.salas[sala].jugadores[i].nombre);
				int posicion = DamePosicion(&miListaConectados, &miListaSalas.salas[sala].jugadores[i].nombre); // Buscamos jugador en conectados
				int socketJugador = miListaConectados.conectados[posicion].socket; // Buscamos su socket

				DameJugadores(&miListaSalas, jugadores, sala);
				
				sprintf(notificacionSala, "14/%d", sala);
				sprintf(notificacionSala, "%s/%s", notificacionSala, jugadores);

				// Enviamos notificacion de jugadores en esa sala a todos los clientes
				// Ellos ya sabrán si mostrar la notificacion si están en esa sala o no

				write (socketJugador, notificacionSala, strlen(notificacionSala));
			}
			
			// Enviamos el chat
			/*
			char jugador[20];
			char chat[1000000]; // El chat contiene muchos char
			chat[0] = '\0'; // Reiniciamos el chat
			DameChat(&miListaSalas, chat, sala); // guardamos el chat de la sala en la variable
			sprintf(notificacion, "15/%s", chat);
			printf("Notificacion: %s\n", notificacion);

			write (sock_conn, notificacion, strlen(notificacion));*/
		}
		else if (codigo == 15) // Mensaje de chat sala
		{
			// El usuario nos envia 15/(sala)/(nombre)/(mensaje)
			
			// Guardamos el mensaje en la sala
			pthread_mutex_lock( &mutex);
			AgregarMensaje(&miListaSalas, nombre, mensajeChat, sala);
			pthread_mutex_unlock( &mutex);
			// Buscamos el nombre de todos los jugadores
			jugadores[0] = '\0'; // Reiniciamos vector
			pthread_mutex_lock( &mutex);
			DameJugadores(&miListaSalas, jugadores, sala);
			pthread_mutex_unlock( &mutex);
			// Para cada jugador encontramos su socket y le enviamos la notificacion
			char *z = strtok( jugadores, "/");
			int numJugadores =  atoi (z);
			printf("Hay: %d jugadores en la sala %d\n" ,numJugadores, sala);

			char jugador[20];
			char chat[1000000]; // El chat contiene muchos char
			chat[0] = '\0'; // Reiniciamos el chat
			pthread_mutex_lock( &mutex);
			DameChat(&miListaSalas, chat, sala); // guardamos el chat de la sala en la variable
			pthread_mutex_unlock( &mutex);
			sprintf(notificacion, "15/%d/%s", sala, chat);
			printf("Notificacion: %s\n", notificacion);
			for (int i = 0; i < numJugadores; i++)
			{
				// Obtenemos nombre del jugador
				z = strtok( NULL, "/");
				strcpy (jugador, z);

				// Buscamos el socket del jugador
				printf("Buscamos al jugador %s\n", miListaSalas.salas[sala].jugadores[i]);
				pthread_mutex_lock( &mutex);
				int posicion = DamePosicion(&miListaConectados, &miListaSalas.salas[sala].jugadores[i]); // Buscamos jugador en conectados
				pthread_mutex_unlock( &mutex);
				int socketJugador = miListaConectados.conectados[posicion].socket; // Buscamos su socket

				write (socketJugador, notificacion, strlen(notificacion));
				// Reiniciamos nombre
				jugador[0] = '\0';
			}
		}
		else if (codigo == 16) // Mensaje de chat Global
		{
			// El usuario nos envia 16/(nombre)/(mensaje)
			
			// Guardamos el mensaje en la sala
			pthread_mutex_lock( &mutex);
			AgregarMensajeGlobal(&miListaConectados, nombre, mensajeChat);
			pthread_mutex_unlock( &mutex);
			char jugador[20];
			char chat[1000000]; // El chat contiene muchos char
			chat[0] = '\0'; // Reiniciamos el chat
			pthread_mutex_lock( &mutex);
			DameChatGlobal(&miListaConectados, chat);
			pthread_mutex_unlock( &mutex);
			sprintf(notificacion, "16/%s", chat);
			printf("Notificacion: %s\n", notificacion);
			for (int j=0; j < i; j++) // Para cada cliente conectado
			{
				write (sockets[j],notificacion, strlen(notificacion));
			}			
		}
		else if (codigo == 17) // Empezar partida
		{
			// El usuario nos envia 17/(sala)

			miListaSalas.salas[sala].partidaEmpezada = 1; // Empieza la partida
			// Buscamos el nombre de todos los jugadores en la sala y les enviamos notificacion de partida empezada

			jugadores[0] = '\0'; // Reiniciamos vector
			
			pthread_mutex_lock( &mutex);
			DameJugadores(&miListaSalas, jugadores, sala);
			pthread_mutex_unlock( &mutex);
		
			insertarPartida(conn, jugadores, nombre); 

			pthread_mutex_lock( &mutex);
			DameJugadores(&miListaSalas, jugadores, sala);
			pthread_mutex_unlock( &mutex);

			// Para cada jugador encontramos su socket y le enviamos la notificacion
			char *z = strtok( jugadores, "/");
			int numJugadores =  atoi (z);
			printf("Hay: %d jugadores en la sala %d\n" ,numJugadores, sala);

			char jugador[20];
			sprintf(notificacion, "17/%d", sala);
			printf("Notificacion empieza partida: %s\n", notificacion);
			for (int i = 0; i < numJugadores; i++)
			{
				// Obtenemos nombre del jugador
				z = strtok( NULL, "/");
				strcpy (jugador, z);

				// Buscamos el socket del jugador
				printf("Buscamos al jugador %s\n", miListaSalas.salas[sala].jugadores[i]);
				pthread_mutex_lock( &mutex);
				int posicion = DamePosicion(&miListaConectados, &miListaSalas.salas[sala].jugadores[i]); // Buscamos jugador en conectados
				pthread_mutex_unlock( &mutex);
				int socketJugador = miListaConectados.conectados[posicion].socket; // Buscamos su socket
				printf("Socket del jugador %d\n", socketJugador);

				write (socketJugador, notificacion, strlen(notificacion));
				// Reiniciamos nombre
				jugador[0] = '\0';
			}
		}
		if (codigo != 0 && codigo != 7 && codigo != 8 && codigo != 11 && codigo != 13 && codigo != 15 && codigo != 16 && codigo != 17)
		{
			printf("Respuesta: %s\n", respuesta);
			// Enviamos respuesta
			write (sock_conn,respuesta, strlen(respuesta));
		}
		//if (codigo == 0 || codigo == 2 || codigo == 3) printf("Notificacion: %s\n", notificacion);
		
	}
	// Se acabo el servicio para este cliente
	close(sock_conn);
}
int main(int argc, char *argv[])
{
	int sock_conn = -1;
	int sock_listen, ret;
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

	//Bind
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
	{
		printf ("Error al Bind, espera 1 min y reinicia el server\n");
		// Reiniciamos el puerto de escucha
		//const char *comando = "fuser -k -n tcp 50010"; // fuser -k -n tcp 50010 o tambien fuser -k -n 50010/tcp ---- Producción
		const char *comando = "fuser -k -n tcp 9050"; // fuser -k -n tcp 9050 o tambien fuser -k -n 9050/tcp ---- Desarrollo

		// Ejecutar el comando
		int resultado = system(comando);

		// Verificar si la ejecucion fue exitosa

		if (resultado == -1) 
		{
			perror("Error al ejecutar el comando\n");
		} 
		else
		{
			//printf("Puerto 50010 en TCP liberado.\n"); // Producción
			printf("Puerto 9050 en TCP liberado.\n"); // Desarrollo
		}
	}

	if (listen(sock_listen, 3) < 0) printf("Error en el Listen\n"); // Si tarda demasiado en responder
	
	contador = 0;
	i = 0;
	pthread_t thread;

	// Bucle infinito
	for (;;)
	{
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");

		

		int socketYaSeUsa = 0;
		int posicion = -1;
		
		//sock_conn es el socket que usaremos para este cliente
		for(int j = 0; j < i; j++) // Comprobamos que el socket no este en el vector
		{
			if (sock_conn == sockets[j])
			{
				posicion = j; // Guardamos la posicion del socket que se usaba anteriormente
				socketYaSeUsa = 1; // Marcamos socket ya usado
			}
		}

		if (!socketYaSeUsa)
		{
			sockets[i] = sock_conn;
			posicion = i; // Si el socket no estaba en uso anteriormente asignamos la posicion como el numero de sockets que se usan
			i++; // Incrementamos en 1 el numero de sockets en uso
		}

		pthread_create (&thread, NULL, AtenderCliente, &sockets[posicion]);
	}
}
