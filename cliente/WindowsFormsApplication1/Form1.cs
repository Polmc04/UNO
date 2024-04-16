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


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        char Carita = Convert.ToChar(002); // Genera un emoji con ASCII
        bool conectado = false; // Guarda el estado de la conexion con el server
        Socket server;
        public Form1()
        {
            InitializeComponent();
        }
        private void Connect_Click(object sender, EventArgs e)
        {
            if (!conectado) // Si aun no estamos conectados
            {
                //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
                //al que deseamos conectarnos
                IPAddress direc = IPAddress.Parse("192.168.56.102");
                IPEndPoint ipep = new IPEndPoint(direc, 9050);

                //Creamos el socket 
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    server.Connect(ipep); // Intentamos conectar el socket
                    this.BackColor = Color.Green;
                    conectado = true;
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
                string passwordEncriptado = ObtenerHashSHA256(password.Text).Substring(0, 10);

                if (SingUp.Checked)
                {
                    // Construimos y enviamos mensaje
                    string mensaje = "1/" + nombre.Text + "/" + passwordEncriptado;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    //Recibimos la respuesta del servidor
                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];


                    if (mensaje == "1") // 1 si el server ha podido registrarlo
                        MessageBox.Show("Succesfully registered!" + Carita);
                    else if (mensaje == "2")
                        MessageBox.Show("Unable to create account, user " + nombre.Text + " alredy exists");
                }
                else if (LogIn.Checked)
                {
                    string mensaje = "2/" + nombre.Text + "/" + passwordEncriptado;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    //Recibimos la respuesta del servidor
                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                    if (mensaje == "1") // 1 si el server ha podido logearlo
                        MessageBox.Show("Logged In! " + Carita);
                    else if (mensaje == "2")
                        MessageBox.Show("Unable to Log In, password does not match!");
                    else if (mensaje == "3")
                        MessageBox.Show("Unable to Log In, user " + nombre.Text + " does not exist. Try to Sing Up!");

                }
                else if (Remove.Checked)
                {
                    // Enviamos peticion eliminar cuenta
                    string mensaje = "3/" + nombre.Text + "/" + passwordEncriptado;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    //Recibimos la respuesta del servidor
                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                    if (mensaje == "1") // 1 si el server ha podido eliminar la cuenta
                        MessageBox.Show("Account deleted " + Carita);
                    else if (mensaje == "2")
                        MessageBox.Show("Password does not match!");
                    else if (mensaje == "3")
                        MessageBox.Show("User " + nombre.Text + " does not exist.");
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
                this.BackColor = Color.Gray;
                server.Shutdown(SocketShutdown.Both);
                server.Close();

                conectado = false;

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

                //Recibimos el mensaje del servidor; 
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show(mensaje);

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

                //Recibimos el mensaje del servidor; 
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show(mensaje);
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

                //Recibimos el mensaje del servidor; 
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show(mensaje);
            }
            else
            {
                MessageBox.Show("You must be connected in order to send messages to the server");
            }
        }
        public string[] DameConectados()
        {
            // Envia mensaje al server pidiendo la cantidad y nombre de conectados
            string mensaje = "7/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos el mensaje del servidor; 
            byte[] msg2 = new byte[800];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            string[] partes = mensaje.Split('/');
            return partes;
        }
        private void DimeConectados_Click(object sender, EventArgs e)
        {
            if (conectado)
            {
                string[] partes = DameConectados();
                int numConectados = int.Parse(partes[0]);
                string nombres = "";
                for (int i = 1; i <= numConectados; i++)
                {
                    // Agregar cada nombre a la cadena de nombres
                    nombres += partes[i] + Environment.NewLine;
                }
                MessageBox.Show("Hay " + numConectados + " jugadores conectados: " + Environment.NewLine + nombres);
            }
            else
            {
                MessageBox.Show("You must be connected in order to send messages to the server");
            }
        }
        int DameCarta()
        {
            // Peticion al server de pedir una carta
            int carta;
            string mensaje = "8/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos el mensaje del servidor; 
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            return Convert.ToInt32(mensaje);
        }
        bool partidaEmpezada = false;
        private void EmpezarPartida_Click(object sender, EventArgs e)
        {
            if (conectado && !partidaEmpezada)
            {
                partidaEmpezada = true;
                labelYourCards.Visible = true; // Mostramos el label de las cartas del jugador
                if (nombre.Text == string.Empty) labelYourCards.Text = labelYourCards.Text + " ( noname )";
                else labelYourCards.Text = labelYourCards.Text + " (" + nombre.Text + ")";

                // Repartimos Cartas
                int[] cartas = new int[20]; // en este vector se guarda el id de la carta, max 20 cartas

                for (int i = 0; i < 7; i++) // Se escogen 7 cartas iniciales al azar
                {
                    cartas[i] = DameCarta(); // Pedimos carta al server
                    while (cartas[i] == 53 || cartas[i] == 40 || cartas[i] == 27 || cartas[i] == 14) // Aún no tenemos cartas 0
                    {
                        cartas[i] = DameCarta();
                    }
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
                /* ---------------------------------------------EN DESARROLLO--------------------------------------------------------------------
                // Mostramos a los otros jugadores
                string[] partes = DameConectados();
                int numConectados = int.Parse(partes[0]); // Contamos cuantos hay conectados
                string nombreUsuario = nombre.Text;
                int pos = -1; // posicion donde se encuentra el usuario
                bool encontrado = false;
                int i = 0; 
                while (!encontrado && i < numConectados)
                {
                    if (partes[i + 1] == nombreUsuario) // a partir de la posicion 1 hay nombres de usuarios
                    {
                        encontrado = true;
                        pos = i + 1; 
                    }
                }
                if (!encontrado)
                {
                    MessageBox.Show("Error al buscar usuario");
                }*/

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

        private void Player2_Click(object sender, EventArgs e)
        {

        }
    }
}
