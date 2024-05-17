using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Security.Cryptography;
using System.IO;
using System.Threading;
using System.Reflection.Emit;



namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        char Carita = Convert.ToChar(002); // Genera un emoji con ASCII
        bool conectado = false; // Guarda el estado de la conexion con el server
        Thread atender;
        Socket server;
        
        int numCartaRandom;
        bool nuevaCarta = false;

        string usuario;
        string password;
        bool sesionIniciada = false;

        int sala;
        bool enSala;
        string[] jugadores;

        public Form1()
        {
            InitializeComponent();

            // Suscribirse al evento CellDoubleClick del DataGridView
            dataGridViewConectados.CellDoubleClick += DataGridViewConectados_CellDoubleClick;

            // Evento Intro en el textBox del chat
            textBoxChat.KeyDown += textBoxChat_KeyDown;
        }
        private void AtenderServidor()
        {
            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[800];
                server.Receive(msg2);
                string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                //for (int i = 0; i < trozos.Length; i++)
                //{
                //    MessageBox.Show(trozos[i]);
                //}
                int codigo = Convert.ToInt32(trozos[0]);
                string mensaje = trozos[1].Split('\0')[0];
                switch (codigo)
                {
                    case 1:  // Sign up

                        if (mensaje == "1") // 1 si el server ha podido registrarlo
                            MessageBox.Show("Succesfully registered!" + Carita);
                        else if (mensaje == "2")
                            MessageBox.Show("Unable to create account, user " + textBoxNombre.Text + " alredy exists");
                        break;
                    case 2:  // Log in

                        if (mensaje == "1") // Server ha podido logearlo
                        {
                            MessageBox.Show("Logged In! " + Carita);

                            sesionIniciada = true;

                            // Como no el objeto no es creado por el mismo thread tenemos que usar este metodo
                            dataGridViewConectados.Invoke(new Action(() =>
                            {
                                // Escodemos menu para sign up y log in
                                labelName.Visible = false;
                                labelPassword.Visible = false;
                                textBoxNombre.Visible = false;
                                textBoxPassword.Visible = false;

                                radioSingUp.Visible = false;
                                radioLogIn.Visible = false;

                                // Mostramos menu de usuario conectados
                                labelConectados.Visible = true;
                                dataGridViewConectados.Visible = true;
                                labelUsuario.Text = "Usuario: " + usuario; // mostramos que usuario está conectado
                                labelUsuario.Location = new Point(24, 29);
                                labelUsuario.Visible = true;

                                buttonCrearSala.Visible = true;
                                buttonCrearSala.Location = new Point(560, 26);
                            }));
                        }
                        else if (mensaje == "2") // Pasword incorrecto
                            MessageBox.Show("Unable to Log In, password does not match!");
                        else if (mensaje == "3") // No existe el usuario
                            MessageBox.Show("Unable to Log In, user " + textBoxNombre.Text + " does not exist. Try to Sing Up!");
                        else if(mensaje == "4") // Usuario ya ha iniciado sesion
                            MessageBox.Show("Unable to Log In, user " + textBoxNombre.Text + " has already logged with another client!");
                        else if (mensaje == "5") // Usuario ya ha iniciado sesion
                            MessageBox.Show("The server is full");

                        break;
                    case 3: // Remove

                        if (mensaje == "1") // 1 si el server ha podido eliminar la cuenta
                        {
                            MessageBox.Show("Account deleted " + Carita);

                            labelName.Invoke(new Action(() =>
                            {
                                // Mostramos menu para sign up y log in
                                labelName.Visible = true;
                                labelPassword.Visible = true;
                                textBoxNombre.Visible = true;
                                textBoxPassword.Visible = true;

                                radioSingUp.Visible = true;
                                radioLogIn.Visible = true;

                                // Mostramos menu de usuario conectados
                                labelConectados.Visible = false;
                                dataGridViewConectados.Visible = false;
                                labelUsuario.Visible = false;
                                sesionIniciada = false;
                            }
                            ));
                        }
                        else if (mensaje == "2")
                            MessageBox.Show("Password does not match!");
                        else if (mensaje == "3")
                            MessageBox.Show("User " + textBoxNombre.Text + " does not exist.");
                        break;
                    case 4: // SELECT mas partidas

                        MessageBox.Show(mensaje);
                        break;
                    case 5: // SELECT mas ELO

                        MessageBox.Show(mensaje);
                        break;
                    case 6: // SELECT color carta

                        MessageBox.Show(mensaje);
                        break;
                    case 8: // Dame numero random

                        GuardaCarta(mensaje);
                        break;
                    case 9: // Recibimos notificacion

                        int numConectados = int.Parse(trozos[1]); // numero de conectados en la segunda posicion del vector trozos p. ej:  9/1/Manolo
                        string[] nombresVector = new string[numConectados];

                        for (int i = 0; i < numConectados; i++)
                        {
                            nombresVector[i] = trozos[i+2];
                        }

                        // Crear una nueva instancia de DataTable
                        var dt = new System.Data.DataTable();

                        // Agregar una columna llamada "Nombres" al DataTable
                        dt.Columns.Add("Nombres", typeof(string));

                        // Iterar a través del vector de nombres y agregar cada nombre como una nueva fila en el DataTable
                        foreach (var nombre in nombresVector)
                        {
                            dt.Rows.Add(nombre);
                        }

                        // Como no el objeto no es creado por el mismo thread tenemos que usar este metodo
                        dataGridViewConectados.Invoke(new Action(() =>
                        {
                            dataGridViewConectados.DataSource = dt;
                        }));
                        
                        break;
                    case 10: // Sala que nos asigna el server

                        if (Convert.ToInt32(trozos[1]) == -1) MessageBox.Show("Servidor lleno, no se ha podido crear sala");
                        else
                        {
                            labelSala.Invoke(new Action(() =>
                            {
                                labelSala.Text = "Sala: " + trozos[1];
                                labelSala.Visible = true;
                                buttonCrearSala.Visible = false; // Ya no podemos crear sala si nos metemos en una
                                buttonAbandonar.Visible = true;
                                buttonAbandonar.Location = new Point(560, 26);
                                textBoxChat.Visible = true;
                                richTextBoxChat.Visible = true;
                            }));
                        }
                        break;

                    case 11: // Recibimos invitacion a sala

                        sala = Convert.ToInt32(trozos[1]);
                        int numJugadores = Convert.ToInt32(trozos[2]); // En la segunda posicion nos pasan cuantos jugadores hay en la sala

                        string[] nombres = new string[numJugadores];
                        for (int i = 0; i<numJugadores; i++)
                        {
                            nombres[i] = trozos[i+3];
                        }

                        FormAceptarInvitacion form = new FormAceptarInvitacion(sala, nombres);
                        form.AceptarClicked += Form_AceptarClicked; 
                        // Mostrar el formulario
                        form.ShowDialog();
                        break;

                    case 12: // Confirmacion de abandono de sala

                        break;

                    case 13: // Confirmacion entrada a sala

                        break;
                    case 14: // Notificacion Jugadores en sala
                        // 14/(int num sala)/(int num jugadores)/Pol/Joan/Alonso
                        sala = Convert.ToInt32(trozos[1]);
                        numJugadores = Convert.ToInt32(trozos[2]); // En la segunda posicion nos pasan cuantos jugadores hay en la sala

                        jugadores = new string[numJugadores];

                        for (int i = 0; i < numJugadores; i++)
                        {
                            jugadores[i] = trozos[i + 3];
                        }
                        if (numJugadores > 0)
                        {
                            labelYourCards.Invoke(new Action(() =>
                            {
                                labelYourCards.Text = jugadores[0];
                                labelYourCards.Visible = true;
                                labelPlayer2.Visible = false;
                                labelPlayer3.Visible = false;
                                labelPlayer4.Visible = false;
                            }));
                            if (numJugadores > 1)
                            {
                                labelYourCards.Invoke(new Action(() =>
                                {
                                    labelPlayer2.Text = jugadores[1];
                                    labelPlayer2.Visible = true;
                                    labelPlayer3.Visible = false;
                                    labelPlayer4.Visible = false;
                                }));
                                if (numJugadores > 2)
                                {
                                    labelYourCards.Invoke(new Action(() =>
                                    {
                                        labelPlayer3.Text = jugadores[2];
                                        labelPlayer3.Visible = true;
                                        labelPlayer4.Visible = false;

                                    }));
                                    if (numJugadores == 4)
                                    {
                                        labelYourCards.Invoke(new Action(() =>
                                        {
                                            labelPlayer4.Text = jugadores[4];
                                            labelPlayer4.Visible = true;

                                        }));
                                    }
                                }
                            }
                        }
                        break;

                    case 15:
                        richTextBoxChat.Invoke(new Action(() =>
                        {
                            richTextBoxChat.Clear(); // Reiniciamos el chat
                        }));
                        if (trozos.Length > 0)
                        {
                            // Obtenemos numero de mensajes
                            int numMensajes = Convert.ToInt32(trozos[1]);

                            for (int i = 1; i <= numMensajes; i++)
                            {
                                // Agrega cada mensaje al RichTextBox
                                richTextBoxChat.Invoke(new Action(() =>
                                {
                                    // Los mensajes empiezan en trozos[2]
                                    richTextBoxChat.AppendText(trozos[i + 1] + "\n"); // Agrega un salto de línea después de cada mensaje
                                }));
                            }
                        }
                        else
                        {
                            // El mensaje recibido está vacío
                            richTextBoxChat.Invoke(new Action(() =>
                            {
                                richTextBoxChat.AppendText("Mensaje vacío: no se recibieron datos del chat\n");
                            }));
                        }

                        break;
                }
            }
        }
        private void Form_AceptarClicked(object sender, EventArgs e)
        {
            // Usuario acepta invitación a sala
            // Enviamos mensaje al servidor para entrar a esa sala

            string mensaje = "13/" + usuario + "/" + sala; // Pasamos la sala a la que nos unimos y nuestro nombre
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            buttonCrearSala.Invoke(new Action(() =>
            { 
                buttonCrearSala.Visible = false; // Ya no podemos crear sala si nos metemos en una
                buttonAbandonar.Location = new Point(560, 26);
                buttonAbandonar.Visible = true;
                labelSala.Text = "Sala: " + sala.ToString();
                labelSala.Visible = true;
                richTextBoxChat.Visible = true;
                textBoxChat.Visible = true;
                MessageBox.Show("Ya podeis empezar partida!");
            }));
            enSala = true;
        }
        private void Connect_Click(object sender, EventArgs e)
        {
            if (!conectado) // Si aun no estamos conectados
            {
                //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
                //al que deseamos conectarnos
                //IPAddress direc = IPAddress.Parse("10.4.119.5"); // Producción
                IPAddress direc = IPAddress.Parse("192.168.56.102"); // Desarrollo

                //IPEndPoint ipep = new IPEndPoint(direc, 50010); // Producción
                IPEndPoint ipep = new IPEndPoint(direc, 9050); // Desarrollo

                //Creamos el socket 
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    server.Connect(ipep); // Intentamos conectar el socket
                    this.BackColor = Color.Green;
                    conectado = true;
                    MessageBox.Show("Conectado");

                    // Creamos el thread que atenderá los mensajes del servidor
                    ThreadStart ts = delegate { AtenderServidor(); };
                    atender = new Thread(ts);
                    atender.Start();
                }
                catch (SocketException ex)
                {
                    //Si hay excepcion imprimimos error y salimos del programa con return 
                    MessageBox.Show("No he podido conectar con el servidor");
                    return;
                }
            }
            else MessageBox.Show("Ya estabas conectado!");
        }
        static string ObtenerHashSHA256(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convertir la contraseña en una matriz de bytes
                byte[] bytes = Encoding.UTF8.GetBytes(password);

                // Calcular el hash SHA256 de los bytes
                byte[] hashBytes = sha256.ComputeHash(bytes);

                // Convertir el hash en una cadena de caracteres hexadecimal
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
        private void Send_Click(object sender, EventArgs e)
        {
            if (conectado)
            {
                // Encriptamos el password
                string passwordEncriptado = ObtenerHashSHA256(textBoxPassword.Text).Substring(0, 10);

                if (radioSingUp.Checked)
                {
                    // Construimos y enviamos mensaje
                    string mensaje = "1/" + textBoxNombre.Text + "/" + passwordEncriptado;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                else if (radioLogIn.Checked)
                {
                    string mensaje;
                    byte[] msg;
                    // Tenemos que desconectar al usuario que había antes conectado
                    // sin cerrar por completo la conexión con el servidor

                    usuario = textBoxNombre.Text; // Guardamos el nombre del usuario que inicia sesión
                    password = passwordEncriptado; // Guardamos el password del usuario que inicia sesión
                    mensaje = "2/" + textBoxNombre.Text + "/" + passwordEncriptado;
                    msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                else if (radioRemove.Checked)
                {
                    // Enviamos peticion eliminar cuenta
                    string mensaje = "3/" + textBoxNombre.Text + "/" + passwordEncriptado;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
            }
            else
            {
                MessageBox.Show("You must be connected in order to send messages to the server");
            }
        }
        private void Disconnect_Click(object sender, EventArgs e)
        {
            if (conectado)
            {
                //Mensaje de desconexión
                string mensaje = "0/";

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                // Nos desconectamos
                atender.Abort();
                this.BackColor = Color.Gray;
                server.Shutdown(SocketShutdown.Both);
                server.Close();

                conectado = false;

                // Mostramos menu para sign up y log in
                labelName.Visible = true;
                labelPassword.Visible = true;
                textBoxNombre.Visible = true;
                textBoxPassword.Visible = true;

                radioSingUp.Visible = true;
                radioLogIn.Visible = true;

                // Mostramos menu de usuario conectados
                labelConectados.Visible = false;
                dataGridViewConectados.Visible = false;
                labelUsuario.Visible = false;
                sesionIniciada = false;
                buttonPedirCarta.Visible = false;
                buttonEmpezarPartida.Visible = false;
            }
            else
            {
                MessageBox.Show("You must be connected in order to send messages to the server");
            }
        }

        private void MasPartidas_Click(object sender, EventArgs e)
        {
            if (conectado)
            {
                string mensaje = "4/";

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else
            {
                MessageBox.Show("You must be connected in order to send messages to the server");
            }
        }

        private void MasELO_Click(object sender, EventArgs e)
        {
            if (conectado)
            {
                string mensaje = "5/";

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else
            {
                MessageBox.Show("You must be connected in order to send messages to the server");
            }
        }

        private void ColorCarta_Click(object sender, EventArgs e)
        {
            if (conectado)
            {
                string mensaje = "6/";

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else
            {
                MessageBox.Show("You must be connected in order to send messages to the server");
            }
        }
        void PideCarta()
        {
            // Peticion al server de pedir una carta
            string mensaje = "8/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
        public void GuardaCarta(string mensaje)
        {
            numCartaRandom = Convert.ToInt32(mensaje);
            nuevaCarta = true;
        }
        bool partidaEmpezada = false;
        int[] cartas = new int[20];
        private void EmpezarPartida_Click(object sender, EventArgs e)
        {
            if (conectado && !partidaEmpezada && enSala)
            {
                partidaEmpezada = true;
                labelYourCards.Text = usuario;
                labelPlayer2.Text = invitado;
                labelYourCards.Visible = true; // Mostramos el label de las cartas del jugador
                buttonPedirCarta.Visible = true; // Mostramos boton chupar

                // Repartimos Cartas Iniciales

                for (int i = 0; i < 7; i++) // Se escogen 7 cartas iniciales al azar
                {
                    PideCarta(); // Pedimos carta al server
                    while (!nuevaCarta) // hasta que no nos llegue una nueva carta
                    {
                        cartas[i] = numCartaRandom;
                    }
                    cartas[i] = numCartaRandom;
                    nuevaCarta = false;
                }

                // Mostramos al cliente las cartas que le han tocado
                for (int i = 0; i < 7; i++)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Location = new Point(i * 100 + 450, 600); // Alinear horizontalmente
                    pictureBox.Size = new Size(80, 120); // Tamaño de la carta
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Ajustar tamaño de imagen
                    Controls.Add(pictureBox);

                    // Cargar la imagen de la carta
                    string rutaImagen = Path.Combine("Images", "cards", cartas[i] + ".png");
                    if (File.Exists(rutaImagen))
                    {
                        pictureBox.Image = Image.FromFile(rutaImagen);
                    }
                    else
                    {
                        // Si la imagen no se encuentra, mostrar un mensaje en el PictureBox
                        pictureBox.BackColor = Color.Gray;
                        pictureBox.BorderStyle = BorderStyle.FixedSingle;
                        pictureBox.Text = "Imagen no encontrada";
                    }
                }
                

                // Mostramos boton para pedir carta
                buttonPedirCarta.Visible = true;
            }
            else if (partidaEmpezada)
            {
                MessageBox.Show("Partida ya empezada");
            }
            else if (!conectado)
            {
                MessageBox.Show("You must be connected in order to send messages to the server");
            }
        }
        string invitado;
        private void DataGridViewConectados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si se hizo doble clic en una celda válida
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && enSala)
            {
                invitado = (dataGridViewConectados.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()).TrimEnd('\0');
                FormInvitar formInvitar = new FormInvitar(invitado);
                formInvitar.InvitarClicked += FormInvitar_InvitarClicked;
                formInvitar.ShowDialog();
            }
            else if (!enSala) MessageBox.Show("Debes estar en sala para poder invitar");
        }
        private void FormInvitar_InvitarClicked(object sender, EventArgs e)
        {
            // Si queremos invitar al usuario que hemos seleccionado enviamos invitación

            string mensaje = "11/" + invitado + "/" + sala; // Un miembro de la sala invita a otro usuario
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
        private void buttonAbandonar_Click(object sender, EventArgs e)
        {
            string mensaje = "12/" + usuario; // El usuario abandona la sala
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            buttonAbandonar.Visible = false;
            buttonCrearSala.Visible = true;
            labelSala.Visible = false;
            labelYourCards.Visible = false;
            labelPlayer2.Visible = false;
            labelPlayer3.Visible = false;
            labelPlayer4.Visible = false;
        }
        private void buttonCrearSala_Click(object sender, EventArgs e)
        {
            // Enviamos al server peticion para unirnos a una sala

            string mensaje = "10/" + usuario; // Pasamos nombre y sala
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            enSala = true;

            buttonEmpezarPartida.Visible = true;
        }
        private void textBoxChat_KeyDown(object sender, KeyEventArgs e)
        {
            // Verifica si se presionó la tecla "Enter"
            if (e.KeyCode == Keys.Enter)
            {
                // Eliminar sonido
                e.SuppressKeyPress = true;

                // Envía el texto al servidor
                string mensaje = "15/" + usuario + "/" + textBoxChat.Text; // Pasamos mensaje
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                // Limpia el TextBox
                textBoxChat.Clear(); 

                // Indica que el evento ha sido manejado
                e.Handled = true;
            }
        }

        private void richTextBoxChat_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonPedirCarta_Click(object sender, EventArgs e)
        {
            if (conectado && partidaEmpezada && enSala)
            {
                // Contador para llevar el registro del número de posiciones no nulas
                int posicion = 0;

                // Recorre el vector y cuenta las posiciones no nulas
                for (int i = 0; i < cartas.Length; i++)
                {
                    if (cartas[i] != 0) // Verifica si la posición no es nula
                    {
                        posicion++; // Incrementa el contador
                    }
                }

                PideCarta(); // Pedimos carta al server
                while (!nuevaCarta) // hasta que no nos llegue una nueva carta
                {
                    cartas[posicion] = numCartaRandom;
                }
                nuevaCarta = false;

                // Mostramos al cliente la carta que le ha tocado

                PictureBox pictureBox = new PictureBox();
                pictureBox.Location = new Point(posicion * 100 + 450, 600); // Alinear horizontalmente
                pictureBox.Size = new Size(80, 120); // Tamaño de la carta
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Ajustar tamaño de imagen
                Controls.Add(pictureBox);

                // Cargar la imagen de la carta
                string rutaImagen = Path.Combine("Images", "cards", cartas[posicion] + ".png");
                if (File.Exists(rutaImagen))
                {
                    pictureBox.Image = Image.FromFile(rutaImagen);
                }
                else
                {
                    // Si la imagen no se encuentra, mostrar un mensaje en el PictureBox
                    pictureBox.BackColor = Color.Gray;
                    pictureBox.BorderStyle = BorderStyle.FixedSingle;
                    pictureBox.Text = "Imagen no encontrada";
                }
            }
            else if (!partidaEmpezada)
            {
                MessageBox.Show("No has empezado partida");
            }
            else if (!conectado)
            {
                MessageBox.Show("You must be connected in order to send messages to the server");
            }
        }
    }
}
