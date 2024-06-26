DROP DATABASE IF EXISTS M03_BBDD;
CREATE DATABASE M03_BBDD;

USE M03_BBDD;

CREATE TABLE Jugadores (
    Identificador INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
    Nombre TEXT NOT NULL,
    Password TEXT NOT NULL,
    Jugadas INTEGER NOT NULL,
    Ganadas INTEGER NOT NULL,
    ELO INTEGER NOT NULL
) ENGINE = InnoDB;

CREATE TABLE Ranking (
	Posicion INTEGER NOT NULL,
	Nombre INTEGER NOT NULL,
	FOREIGN KEY (Nombre) REFERENCES Jugadores(Identificador)
)ENGINE = InnoDB;

CREATE TABLE Partida (
    Identificador INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
    Fecha DATE NOT NULL,
    Hora TIME NOT NULL,
    Duracion INTEGER NOT NULL,
    Ganador INTEGER NOT NULL,
    FOREIGN KEY (Ganador) REFERENCES Jugadores(Identificador)
) ENGINE = InnoDB;

CREATE TABLE Partida_Jugadores (
    PartidaID INTEGER NOT NULL,
    JugadorID INTEGER NOT NULL,
    PRIMARY KEY (PartidaID, JugadorID),
    FOREIGN KEY (PartidaID) REFERENCES Partida(Identificador),
    FOREIGN KEY (JugadorID) REFERENCES Jugadores(Identificador)
) ENGINE = InnoDB;

CREATE TABLE Cartas (
	Identificador INTEGER PRIMARY KEY NOT NULL,	
	Color TEXT NOT NULL,
	NUMERO TEXT NOT NULL
)ENGINE = InnoDB;

INSERT INTO Cartas VALUES(0,'Negro','+4');
INSERT INTO Cartas VALUES(1,'Negro','Cambia color');

INSERT INTO Cartas VALUES(2,'Rojo','+2');
INSERT INTO Cartas VALUES(3,'Rojo','Reverse');
INSERT INTO Cartas VALUES(4,'Rojo','Salto');
INSERT INTO Cartas VALUES(5,'Rojo','9');
INSERT INTO Cartas VALUES(6,'Rojo','8');
INSERT INTO Cartas VALUES(7,'Rojo','7');
INSERT INTO Cartas VALUES(8,'Rojo','6');
INSERT INTO Cartas VALUES(9,'Rojo','5');
INSERT INTO Cartas VALUES(10,'Rojo','4');
INSERT INTO Cartas VALUES(11,'Rojo','3');
INSERT INTO Cartas VALUES(12,'Rojo','2');
INSERT INTO Cartas VALUES(13,'Rojo','1');
INSERT INTO Cartas VALUES(14,'Rojo','0');

INSERT INTO Cartas VALUES(15,'Amarillo','+2');
INSERT INTO Cartas VALUES(16,'Amarillo','Reverse');
INSERT INTO Cartas VALUES(17,'Amarillo','Salto');
INSERT INTO Cartas VALUES(18,'Amarillo','9');
INSERT INTO Cartas VALUES(19,'Amarillo','8');
INSERT INTO Cartas VALUES(20,'Amarillo','7');
INSERT INTO Cartas VALUES(21,'Amarillo','6');
INSERT INTO Cartas VALUES(22,'Amarillo','5');
INSERT INTO Cartas VALUES(23,'Amarillo','4');
INSERT INTO Cartas VALUES(24,'Amarillo','3');
INSERT INTO Cartas VALUES(25,'Amarillo','2');
INSERT INTO Cartas VALUES(26,'Amarillo','1');
INSERT INTO Cartas VALUES(27,'Amarillo','0');

INSERT INTO Cartas VALUES(28,'Verde','+2');
INSERT INTO Cartas VALUES(29,'Verde','Reverse');
INSERT INTO Cartas VALUES(30,'Verde','Salto');
INSERT INTO Cartas VALUES(31,'Verde','9');
INSERT INTO Cartas VALUES(32,'Verde','8');
INSERT INTO Cartas VALUES(33,'Verde','7');
INSERT INTO Cartas VALUES(34,'Verde','6');
INSERT INTO Cartas VALUES(35,'Verde','5');
INSERT INTO Cartas VALUES(36,'Verde','4');
INSERT INTO Cartas VALUES(37,'Verde','3');
INSERT INTO Cartas VALUES(38,'Verde','2');
INSERT INTO Cartas VALUES(39,'Verde','1');
INSERT INTO Cartas VALUES(40,'Verde','0');

INSERT INTO Cartas VALUES(41,'Azul','+2');
INSERT INTO Cartas VALUES(42,'Azul','Reverse');
INSERT INTO Cartas VALUES(43,'Azul','Salto');
INSERT INTO Cartas VALUES(44,'Azul','9');
INSERT INTO Cartas VALUES(45,'Azul','8');
INSERT INTO Cartas VALUES(46,'Azul','7');
INSERT INTO Cartas VALUES(47,'Azul','6');
INSERT INTO Cartas VALUES(48,'Azul','5');
INSERT INTO Cartas VALUES(49,'Azul','4');
INSERT INTO Cartas VALUES(50,'Azul','3');
INSERT INTO Cartas VALUES(51,'Azul','2');
INSERT INTO Cartas VALUES(52,'Azul','1');
INSERT INTO Cartas VALUES(53,'Azul','0');

INSERT INTO Jugadores VALUES(1,'Joan','6b86b273ff',1312, 10, 500);
INSERT INTO Jugadores VALUES(2,'Pol','6b86b273ff',420, 420, 15400);
INSERT INTO Jugadores VALUES(3,'Nano','6b86b273ff', 69, 0, 250);
INSERT INTO Jugadores VALUES(4,'Angustias','6b86b273ff', 43, 42, 2250);
INSERT INTO Jugadores VALUES(5,'Dolores','6b86b273ff', 43, 1, 50);

INSERT INTO Partida VALUES(1,'2020-06-18','14:30:00',53,1);
INSERT INTO Partida VALUES(2,'2020-11-18','14:30:00',21,3);
INSERT INTO Partida VALUES(3,'2021-02-18','14:30:00',74,1);
INSERT INTO Partida VALUES(4,'2022-12-19','14:30:00',12,4);
INSERT INTO Partida VALUES(5,'2024-06-10','14:30:00',5,5);

INSERT INTO Ranking VALUES(1,1);
INSERT INTO Ranking VALUES(2,5);
INSERT INTO Ranking VALUES(3,4);
INSERT INTO Ranking VALUES(4,3);
INSERT INTO Ranking VALUES(5,2);

-- Partida 1: Jugadores 1, 2, 3, 4
INSERT INTO Partida_Jugadores VALUES (1, 1);
INSERT INTO Partida_Jugadores VALUES (1, 2);
INSERT INTO Partida_Jugadores VALUES (1, 3);
INSERT INTO Partida_Jugadores VALUES (1, 4);

-- Partida 2: Jugadores 2, 3, 4, 5
INSERT INTO Partida_Jugadores VALUES (2, 2);
INSERT INTO Partida_Jugadores VALUES (2, 3);
INSERT INTO Partida_Jugadores VALUES (2, 4);
INSERT INTO Partida_Jugadores VALUES (2, 5);

-- Partida 3: Jugadores 1, 3, 4, 5
INSERT INTO Partida_Jugadores VALUES (3, 1);
INSERT INTO Partida_Jugadores VALUES (3, 3);
INSERT INTO Partida_Jugadores VALUES (3, 4);
INSERT INTO Partida_Jugadores VALUES (3, 5);

-- Partida 4: Jugadores 1, 2, 3, 4
INSERT INTO Partida_Jugadores VALUES (4, 1);
INSERT INTO Partida_Jugadores VALUES (4, 2);
INSERT INTO Partida_Jugadores VALUES (4, 3);
INSERT INTO Partida_Jugadores VALUES (4, 4);

-- Partida 5: Jugadores 2, 3, 4, 5
INSERT INTO Partida_Jugadores VALUES (5, 2);
INSERT INTO Partida_Jugadores VALUES (5, 3);
INSERT INTO Partida_Jugadores VALUES (5, 4);
INSERT INTO Partida_Jugadores VALUES (5, 5);