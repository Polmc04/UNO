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
                MessageBox.Show("You must be connected in order to Send messages to the server");
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
                MessageBox.Show("You must be connected in order to Send messages to the server");
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
                MessageBox.Show("You must be connected in order to Send messages to the server");
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
                MessageBox.Show("You must be connected in order to Send messages to the server");
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
                MessageBox.Show("You must be connected in order to Send messages to the server");
            }
        }

        private void DimeConectados_Click(object sender, EventArgs e)
        {
            if (conectado)
            {
                string mensaje = "7/";

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos el mensaje del servidor; 
                byte[] msg2 = new byte[800];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                string[] partes = mensaje.Split('/');
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
                MessageBox.Show("You must be connected in order to Send messages to the server");
            }
        }
    }
}
