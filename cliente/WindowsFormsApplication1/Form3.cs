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
using System.Threading;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;

namespace WindowsFormsApplication1
{
    public partial class FormMenu : Form
    {
        Socket server;
        Thread atender;

        delegate void DelegadoParaPonerTexto(string texto);

        List<Form1> formularios = new List<Form1>();
        int[] salaForm = new int[20];

        bool conectado;

        string usuario;
        string password;
        bool sesionIniciada;
        bool master;

        bool enSala;
        int sala;

        int numCartaRandom;
        bool nuevaCarta = false;

        int numConectados;
        string[] nombresVector;

        public FormMenu()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; // provisional
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            CenterPictureBox(pictureBoxLogo);
            toolTip1.SetToolTip(pictureBoxLogo, "Inicia Sesión para crear sala");
            // Inicializamos salas
            for (int i = 0; i < salaForm.Length; i++)
            {
                salaForm[i] = -1;
            }
            // Evento Intro en el textBox del chat
            textBoxChat.KeyDown += textBoxChat_KeyDown;
        }

        private void CenterPictureBox(PictureBox pictureBox)
        {
            // Calcular las nuevas posiciones X e Y
            int newX = (this.ClientSize.Width / 2) - (pictureBox.Width / 2);
            int newY = (this.ClientSize.Height / 2) - (pictureBox.Height / 2);

            // Asignar las nuevas posiciones al PictureBox
            pictureBox.Location = new Point(newX, newY);
        }

        private void pictureBoxLogo_Click(object sender, EventArgs e)
        {
            // Metemos al usuario en una sala
            // El servidor nos devolverá el numero de sala
            if (sesionIniciada)
            {
                string mensaje = "10/" + usuario; // Pasamos nombre y sala
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else
            {
                MessageBox.Show("Inicia sesión antes de crear sala!");
            }
        }
        int DameForm(int sala)
        {
            // Devuelve el formulario en el que se está jugando la sala
            return Array.IndexOf(salaForm, sala);
        }
        void ReodenaSalas(int posicion)
        {
            // Elimina la sala del form cerrado
            for (int i = posicion; i < salaForm.Length - 1; i++)
            {
                salaForm[i] = salaForm[i + 1]; 
            }
        }
        private void AtenderServidor()
        {
            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[800];
                server.Receive(msg2);
                string[] parteUtil = Encoding.ASCII.GetString(msg2).Split('\0');
                string[] trozos = parteUtil[0].Split('/');
                int codigo = Convert.ToInt32(trozos[0]);
                string mensaje = trozos[1].Split('\0')[0];
                switch (codigo)
                {
                    case 1:  // Sign up

                        if (mensaje == "1") // 1 si el server ha podido registrarlo
                            MessageBox.Show("Succesfully registered!");
                        else if (mensaje == "2")
                            MessageBox.Show("Unable to create account, user " + textBoxNombre.Text + " alredy exists");
                        break;
                    case 2:  // Log in

                        if (mensaje == "1") // Server ha podido logearlo
                        {
                            MessageBox.Show("Logged In!");

                            sesionIniciada = true;

                            // Como no el objeto no es creado por el mismo thread tenemos que usar este metodo
                            labelName.Invoke(new Action(() =>
                            {
                                // Escodemos menu para sign up y log in
                                labelName.Visible = false;
                                labelPassword.Visible = false;
                                textBoxNombre.Visible = false;
                                textBoxPassword.Visible = false;

                                radioSingUp.Visible = false;
                                radioLogIn.Visible = false;

                                // Mostramos menu de usuario conectados
                                labelUsuario.Text = "Usuario: " + usuario; // mostramos que usuario está conectado
                                labelUsuario.Location = new Point(24, 29);
                                labelUsuario.Visible = true;
                                labelChat.Visible = true;
                                richTextBoxChat.Visible = true;
                                textBoxChat.Visible = true;
                                toolTip1.SetToolTip(pictureBoxLogo, "Haz click para crear sala!");
                            }));

                            // Mensaje de conexión
                            string mensajeChat = "16/" + usuario + "/se ha conectado";

                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensajeChat);
                            server.Send(msg);
                        }
                        else if (mensaje == "2") // Pasword incorrecto
                            MessageBox.Show("Unable to Log In, password does not match!");
                        else if (mensaje == "3") // No existe el usuario
                            MessageBox.Show("Unable to Log In, user " + textBoxNombre.Text + " does not exist. Try to Sing Up!");
                        else if (mensaje == "4") // Usuario ya ha iniciado sesion
                            MessageBox.Show("Unable to Log In, user " + textBoxNombre.Text + " has already logged with another client!");
                        else if (mensaje == "5") // Servidor lleno
                            MessageBox.Show("The server is full");

                        break;
                    case 3: // Remove

                        if (mensaje == "1") // 1 si el server ha podido eliminar la cuenta
                        {
                            MessageBox.Show("Account deleted");

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
                                labelUsuario.Visible = false;
                                sesionIniciada = false;
                            }
                            ));
                        }
                        else if (mensaje == "2") // Contraseña incorrecta
                            MessageBox.Show("Password does not match!");
                        else if (mensaje == "3") // Usuario no existe
                            MessageBox.Show("User " + textBoxNombre.Text + " does not exist.");
                        break;

                    case 4: // SELECT mas partidas

                        MessageBox.Show(mensaje);
                        break;

                    case 5: // SELECT mas ELO

                        MessageBox.Show(parteUtil[0].Substring(2));
                        break;

                    case 6: // SELECT color carta

                        MessageBox.Show(mensaje);
                        break;

                    case 7: // Recibimos notificacion cartas de sala

                        // Enviamos al form de la sala que corresponda
                        sala = Convert.ToInt32(trozos[1]);
                        formularios[DameForm(sala)].MostrarCartas(trozos);

                        break;

                    case 8: // Dame numero random

                        // 8/(num sala)/(num carta random)

                        // Enviamos al form de la sala que corresponda
                        sala = Convert.ToInt32(trozos[1]);
                        formularios[DameForm(sala)].GuardaCarta(trozos[2]);
                        break;

                    case 9: // Recibimos notificacion de jugadores conectados

                        // 9 / (num conectados) / conectado1 / conectado2 ...

                        // Ponemos a los conectados en la lista de conectados
                        numConectados = int.Parse(trozos[1]); // numero de conectados en la segunda posicion del vector trozos p. ej:  9/1/Manolo
                        nombresVector = new string[numConectados];

                        for (int i = 0; i < numConectados; i++)
                        {
                            nombresVector[i] = trozos[i + 2];
                        }

                        for (int i = 0; i < formularios.Count; i++) // Esta notificacion la enviamos a todas las salas
                        {
                            formularios[i].PasaConcetados(nombresVector);
                        }

                        break;
                    case 10: // Sala que nos asigna el server

                        if (Convert.ToInt32(trozos[1]) == -1) MessageBox.Show("Servidor lleno, no se ha podido crear sala");
                        else // Recibimos sala y abrimos nuevo form
                        {
                            master = true; // Si creamos la sala somos master
                            enSala = true;

                            sala = Convert.ToInt32(trozos[1]);
                            salaForm[formularios.Count] = sala;

                            ThreadStart ts = delegate { PonerEnMarchaFormulario(); };
                            Thread T = new Thread(ts);
                            T.Start();

                            // Pasamos lista conectados por si se conecta alguien antes de abrir sala
                            for (int i = 0; i < formularios.Count; i++) // Esta notificacion la enviamos a todas las salas
                            {
                                formularios[i].PasaConcetados(nombresVector);
                            }
                        }
                        break;

                    case 11: // Recibimos invitacion a sala

                        // 11 / (num sala) / (num jugadores) / jugador 1 / jugador 2 ...

                        // Mostramos en el menú la invitación  a una sala

                        sala = Convert.ToInt32(trozos[1]);
                        int numJugadores = Convert.ToInt32(trozos[2]); // En la segunda posicion nos pasan cuantos jugadores hay en la sala

                        string[] nombres = new string[numJugadores];
                        for (int i = 0; i < numJugadores; i++)
                        {
                            nombres[i] = trozos[i + 3];
                        }

                        FormAceptarInvitacion form = new FormAceptarInvitacion(sala, nombres);
                        form.AceptarClicked += Form_AceptarClicked;
                        form.ShowDialog(); // Mostrar el formulario
                        break;

                    case 12: // Confirmacion de abandono de sala

                        sala = Convert.ToInt32(trozos[1]);
                        // Desconectamos y cerremos el form correspondiente
                        formularios[DameForm(sala)].Desconecta();
                        // Eliminamos el formulario donde se estaba jugando esa sala
                        formularios.RemoveAt(DameForm(sala));
                        ReodenaSalas(DameForm(sala)); // Eliminamos la sala del vector auxiliar

                        break;

                    case 13: // Confirmacion entrada a sala

                        break;

                    case 14: // Notificacion Jugadores en sala

                        // Le pasamos a la sala correspondiente los datos de la notificacion

                        sala = Convert.ToInt32(trozos[1]);
                        formularios[DameForm(sala)].PasaJugadores(trozos);
                        break;

                    case 15:

                        sala = Convert.ToInt32(trozos[1]);
                        formularios[DameForm(sala)].PasaChat(trozos);

                        break;

                    case 16:
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

                    case 17:
                        sala = Convert.ToInt32(trozos[1]);
                        formularios[DameForm(sala)].EmpezarPartida();
                        break;
                }
            }
        }
        private void Form_AceptarClicked(object sender, EventArgs e)
        {
            // Usuario acepta invitación a sala
            // Enviamos mensaje al servidor para entrar a esa sala

            string mensaje = "13/" + sala + "/" + usuario; // Pasamos la sala a la que nos unimos y nuestro nombre
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            salaForm[formularios.Count] = sala;

            master = false; // Si no creamos la sala no somos master

            ThreadStart ts = delegate { PonerEnMarchaFormulario(); };
            Thread T = new Thread(ts);
            T.Start();

            enSala = true;
        }
        private void PonerEnMarchaFormulario()
        {
            int cont = formularios.Count;
            Form1 f = new Form1(sala, server, usuario, master, cont);
            formularios.Add(f);
            f.ShowDialog();
        }

        private void FormMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (conectado)
            {
                string mensaje = "0/";

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                atender.Abort();
                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
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
                    conectado = true;

                    // Creamos el thread que atenderá los mensajes del servidor
                    ThreadStart ts = delegate { AtenderServidor(); };
                    atender = new Thread(ts);
                    atender.Start();

                    // Mostramos menu para sign up y log in
                    labelName.Visible = true;
                    labelPassword.Visible = true;
                    textBoxNombre.Visible = true;
                    textBoxPassword.Visible = true;

                    radioSingUp.Visible = true;
                    radioLogIn.Visible = true;

                    // Mostramos menu de usuario conectados
                    labelUsuario.Visible = false;
                    sesionIniciada = false;

                    groupBox1.Visible = true;

                    btnConnect.BackColor = Color.WhiteSmoke;
                    btnDisconnect.BackColor = Color.Red;

                    MessageBox.Show("Conectado");
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

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (conectado)
            {
                //Mensaje de desconexión
                string mensaje = "0/";

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                // Nos desconectamos
                atender.Abort();
                server.Shutdown(SocketShutdown.Both);
                server.Close();

                sesionIniciada = false;
                conectado = false;

                // Ocultamos menu para sign up y log in
                labelName.Visible = false;
                labelPassword.Visible = false;
                textBoxNombre.Visible = false;
                textBoxNombre.Text = "";
                textBoxPassword.Visible = false;
                textBoxPassword.Text = "";

                radioSingUp.Visible = false;
                radioLogIn.Visible = false;

                // Mostramos menu de usuario conectados
                labelUsuario.Visible = false;
                sesionIniciada = false;

                groupBox1.Visible = false;

                btnConnect.BackColor = Color.LimeGreen;
                btnDisconnect.BackColor = Color.WhiteSmoke;
                MessageBox.Show("Te has desconectado");
            }
            else
            {
                MessageBox.Show("You must be connected in order to send messages to the server");
            }
        }

        private void MasPartidas_Click(object sender, EventArgs e)
        {
            if (conectado && sesionIniciada)
            {
                string mensaje = "4/" + usuario;

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else
            {
                MessageBox.Show("Debes iniciar sesión para enviar querys");
            }
        }

        private void MasELO_Click(object sender, EventArgs e)
        {
            if (conectado && sesionIniciada)
            {
                string mensaje = "5/" + usuario + "/" + textBoxJugador.Text;

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else
            {
                MessageBox.Show("Debes iniciar sesión para enviar querys");
            }
        }

        private void ColorCarta_Click(object sender, EventArgs e)
        {
            if (conectado && sesionIniciada)
            {
                string mensaje = "6/" + usuario + "/" + textBoxInicio.Text + "/" + textBoxFin.Text;

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else
            {
                MessageBox.Show("Debes iniciar sesión para enviar querys");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
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
        private void textBoxChat_KeyDown(object sender, KeyEventArgs e)
        {
            // Verifica si se presionó la tecla "Enter"
            if (e.KeyCode == Keys.Enter)
            {
                // Eliminar sonido
                e.SuppressKeyPress = true;

                // Verifica si el texto contiene '/'
                if (textBoxChat.Text.Contains('/'))
                {
                    // Muestra un mensaje de error
                    MessageBox.Show("El mensaje no puede contener el carácter '/'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Sale del método sin enviar el mensaje
                }

                // Envía el texto al servidor
                string mensaje = "16/" + usuario + "/" + textBoxChat.Text; // Pasamos mensaje
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                // Limpia el TextBox
                textBoxChat.Clear();

                // Indica que el evento ha sido manejado
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
