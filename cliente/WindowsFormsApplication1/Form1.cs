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
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Reflection;



namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        bool conectado = true; // Guarda el estado de la conexion con el server
        Thread atender;
        Socket server;
        
        int numCartaRandom;
        bool nuevaCarta = false;

        bool master = false; // El jugador que crea la sala es el que decide cuando empezar la partida
        string usuario;
        string password;
        bool sesionIniciada = false;

        int sala;
        int numForm;
        bool enSala = true;
        string[] jugadores;
        int numJugadores;
        int posicionJugador;

        int clientWidth;
        int clientHeight;

        int cartasReves2 = 0;
        int cartasReves3 = 0;
        int cartasReves4 = 0;

        // Vector para almacenar el número de cartas en la baraja del jugador
        List<int> cartasJugador = new List<int>();
        private PictureBox[] Cartas;

        // Vector para almacenar el número de cartas en la baraja central
        List<int> cartasEnBaraja = new List<int>();
        private PictureBox[] CartasEnBaraja;

        // Variables para almacenar el número de jugadores en la sala y sus cartas
        List<string> nombresJugadores = new List<string>();
        List<int> cartasPorJugador = new List<int>();

        private PictureBox[] CartasReves2;
        private PictureBox[] CartasReves3;
        private PictureBox[] CartasReves4;


        public Form1(int sala, Socket server, string usuario, bool master, int numForm)
        {
            InitializeComponent();

            int pictureboxcount = 112;
            Cartas = new PictureBox[pictureboxcount];
            CartasEnBaraja = new PictureBox[pictureboxcount];
            CartasReves2 = new PictureBox[pictureboxcount];
            CartasReves3 = new PictureBox[pictureboxcount];
            CartasReves4 = new PictureBox[pictureboxcount];

            this.sala = sala;
            this.server = server;
            this.usuario = usuario;
            this.master = master;
            this.numForm = numForm;

            if(master)
            {
                buttonEmpezarPartida.Visible = true; // Si es el master puede empezar la partida
            }

            // Suscribirse al evento CellDoubleClick del DataGridView
            dataGridViewConectados.CellDoubleClick += DataGridViewConectados_CellDoubleClick;

            // Evento Intro en el textBox del chat
            textBoxChat.KeyDown += textBoxChat_KeyDown;

            labelUsuario.Text += " " + usuario;
        }
        private void ReordenarPictureBox()
        {
            // Recoloca las cartas del jugador despues de lanzar o recoger una de la baraja central
            int i = 0;
            clientWidth = this.ClientSize.Width;
            clientHeight = this.ClientSize.Height;

            while (Cartas[i] != null)
            {
                if (i == 0)
                {
                    Cartas[i].Location = new Point(clientWidth * 2 / 10, clientHeight * 2/ 3);
                }
                else
                {
                    int x = Cartas[i - 1].Location.X;
                    int y = Cartas[i - 1].Location.Y;
                    if (x > clientWidth*7/10)
                    {
                        Cartas[i].Location = new Point(clientWidth * 2 / 10, y + 170); // Alinear horizontalmente
                    }
                    else
                    {
                        Cartas[i].Location = new Point(x + 100, y); // Alinear horizontalmente
                    }
                }
                i++;
            }
        }
        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            // Detecta cuando se hace click en una carta
            PictureBox seleccionado = sender as PictureBox;

            // Diferenciar entre carta de baraja Central o carta de Jugador
            bool cartaJugador = false;
            if (seleccionado.Tag == "Jugador") cartaJugador = true;

            if (seleccionado != null && cartaJugador) // Si el jugador hace click en sus cartas
            {
                // Enviamos mensaje al servidor
                string mensaje = "7/" + sala + "/" + usuario + "/1/" + seleccionado.Name; // Queremos METER la carta seleccionada
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                // Ponemos la carta en la baraja central
                int i = 0;
                while (CartasEnBaraja[i] != null)
                {
                    i++;
                }
                CartasEnBaraja[i] = seleccionado;
                CartasEnBaraja[i].Tag = "Central";
                CartasEnBaraja[i].MouseDown += PictureBox_MouseDown; // Subscribimos al metodo para clickar

                Cartas = Cartas.Where(pb => pb != seleccionado).ToArray(); // Quitamos el picturebox seleccionado del vector de cartas del jugador

                // Quitamos carta de la baraja del jugador
                cartasJugador.Remove(Convert.ToInt32(seleccionado.Name));
                seleccionado.MouseDown -= PictureBox_MouseDown;
                Controls.Remove(seleccionado);

                // Detectar el centro de la pantalla
                Size formsize = this.ClientSize;
                Point clickPoint = CartasEnBaraja[i].PointToClient(Cursor.Position);
                int x = (formsize.Width - CartasEnBaraja[i].Width) / 2;
                int y = ((formsize.Height - CartasEnBaraja[i].Height) / 2) - 150;
                CartasEnBaraja[i].Location = new Point(x, y); // Movemos la carta al frente de la baraja central
                Controls.Add(CartasEnBaraja[i]);
                CartasEnBaraja[i].BringToFront();
               
                ReordenarPictureBox(); // Reordenamos las cartas de la baraja del jugador

                //MessageBox.Show($"Picturebox {seleccionado.Name} seleccionado");
            }
            else if(seleccionado != null) // El jugador hace click en la baraja central
            {
                // Enviamos mensaje al servidor
                string mensaje = "7/" + sala + "/" + usuario + "/2/" + seleccionado.Name; // Queremos SACAR la carta seleccionada
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                // Ponemos la carta en la baraja del Jugador
                int i = 0;
                while (Cartas[i] != null)
                {
                    i++;
                }
                Cartas[i] = seleccionado;
                Cartas[i].Tag = "Jugador";
                Cartas[i].MouseDown += PictureBox_MouseDown; // Subscribimos al metodo para clickar

                Controls.Remove(seleccionado); // Quitamos el picturebox seleccionado del formulario
                CartasEnBaraja = CartasEnBaraja.Where(pb => pb != seleccionado).ToArray(); // Quitamos el picturebox seleccionado del vector
                seleccionado.MouseDown -= PictureBox_MouseDown; // Desuscribimos evento click

                // Metemos carta de la baraja del jugador
                cartasJugador.Add(Convert.ToInt32(seleccionado.Name));
                Controls.Add(Cartas[i]);

                ReordenarPictureBox(); // Reordenamos las cartas de la baraja del jugador
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            labelSala.Text += sala.ToString();
            string mensaje = "9/"; // Pedimos al server los conectados
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
        
        public void PasaConcetados(string[] nombresVector)
        {
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
        }
        public void PasaJugadores(string[] trozos)
        {
            // 14/(int num sala)/(int num jugadores)/Pol/Joan/Alonso

            // Ponemos a los jugadores conectados en sala en los labels correspondientes

            numJugadores = Convert.ToInt32(trozos[2]); // En la 2a posicion nos pasan cuantos jugadores hay en la sala
            jugadores = new string[numJugadores];

            int j = 0;
            for (int i = 0; i < numJugadores; i++)
            {
                if (trozos[i + 3] != usuario)
                {
                    jugadores[j] = trozos[i + 3]; // Metemos a los jugadores de la sala en el vector
                    j++;
                }
            }
            if (numJugadores - 1 > 0)
            {
                labelYourCards.Invoke(new Action(() =>
                {
                    labelPlayer2.Text = jugadores[0];
                    labelPlayer2.Visible = true;
                    labelPlayer3.Visible = false;
                    labelPlayer4.Visible = false;
                }));
                if (numJugadores - 1 > 1)
                {
                    labelYourCards.Invoke(new Action(() =>
                    {
                        labelPlayer3.Text = jugadores[1];
                        labelPlayer3.Visible = true;
                        labelPlayer4.Visible = false;
                    }));
                    if (numJugadores - 1 > 2)
                    {
                        labelYourCards.Invoke(new Action(() =>
                        {
                            labelPlayer4.Text = jugadores[2];
                            labelPlayer4.Visible = true;

                        }));
                    }
                }
            }
        }
        public void PasaChat(string[] trozos)
        {
            // 15 / (sala) / (num mensajes) / (mensaje 1) / (mensaje 2)
            // Escribimos el chat de la sala

            richTextBoxChat.Invoke(new Action(() =>
            {
                richTextBoxChat.Clear(); // Reiniciamos el chat
            }));
            if (trozos.Length > 0)
            {
                // Obtenemos numero de mensajes
                int numMensajes = Convert.ToInt32(trozos[2]);

                for (int i = 1; i <= numMensajes; i++)
                {
                    // Agrega cada mensaje al RichTextBox
                    richTextBoxChat.Invoke(new Action(() =>
                    {
                        // Los mensajes empiezan en trozos[3]
                        richTextBoxChat.AppendText(trozos[i + 2] + "\n"); // Agrega un salto de línea después de cada mensaje
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
        }
        void PideCarta()
        {
            // Peticion al server de pedir una carta
            string mensaje = "8/" + sala + "/" + usuario;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
        public void GuardaCarta(string mensaje)
        {
            numCartaRandom = Convert.ToInt32(mensaje);
            cartasJugador.Add(numCartaRandom);

            int i = cartasJugador.Count() - 1;
            labelYourCards.Invoke(new Action(() =>
            {
                Cartas[i] = new PictureBox(); // Añadimos pictureBox al vector
                // Determinamos posicion
                int x = 0, y = 0;
                if (i >= 1)
                {
                    x = Cartas[i - 1].Location.X;
                    y = Cartas[i - 1].Location.Y;
                }
                else
                {
                    x = clientWidth*2/10;
                    y = clientHeight*2/3;
                }
                if (x > clientWidth*7/10)
                {
                    Cartas[i].Location = new Point(clientWidth * 2 / 10, y + 170); // Alinear horizontalmente
                }
                else
                {
                    Cartas[i].Location = new Point(x + 100, y); // Alinear horizontalmente
                }
                Cartas[i].Size = new Size(80, 120); // Tamaño de la carta
                Cartas[i].SizeMode = PictureBoxSizeMode.StretchImage; // Ajustar tamaño de imagen

                // Cargar la imagen de la carta
                string rutaImagen = Path.Combine("Images", "cards", numCartaRandom + ".png");
                if (File.Exists(rutaImagen))
                {
                    Cartas[i].Name = Convert.ToString(numCartaRandom); // Guardamos el id como el nombre del pictureBox
                    Cartas[i].Tag = "Jugador";
                    Cartas[i].Image = Image.FromFile(rutaImagen);
                }
                else
                {
                    // Si la imagen no se encuentra, mostrar un mensaje en el PictureBox
                    Cartas[i].BackColor = Color.Gray;
                    Cartas[i].BorderStyle = BorderStyle.FixedSingle;
                    Cartas[i].Text = "Imagen no encontrada";
                }

                Cartas[i].MouseDown += PictureBox_MouseDown; // Subscribimos al metodo para clickar
                Controls.Add(Cartas[i]); // Metemos la carta en el formulario
            }));
        }

        public void MostrarCartas(string[] trozos)
        {
            // 7/(sala)/(num cartas baraja)-(carta1)-(carta2)-.../(num jugadores)-(nombre jugador1)-(num cartas jugador 1)-(nombre jugador 2)-...

            this.BeginInvoke((MethodInvoker)delegate {
                // Divide la cadena trozos[2] en subcadenas usando el guión "-"
                string[] subTrozosCartas = trozos[2].Split('-');

                cartasEnBaraja.Clear(); // Reiniciamos vector id cartas baraja central
                                        // Acumula el ID de las cartas en la baraja
                int idCarta;
                for (int i = 1; i < subTrozosCartas.Length; i++) // Las cartas empiezan en la posicion 1
                {
                    if (int.TryParse(subTrozosCartas[i], out idCarta)) // Obtenemos id del mensajes
                    {
                        cartasEnBaraja.Add(idCarta); // Añadimos id
                    }
                }
                int numServidor = Convert.ToInt32(subTrozosCartas[0]); // Numero de cartas en la baraja central que nos manda el servidor
                int numCliente = 0; // Numero de cartas en la baraja central que tiene el cliente en el formulario
                int z = 0;
                bool parar = false;
                while (!parar)
                {
                    if (CartasEnBaraja[z] == null) parar = true;
                    else z++;
                }
                numCliente = z;
                if (numServidor > numCliente) // Si se añade una carta nueva a la baraja
                {
                    // Metemos carta en Baraja
                    CartasEnBaraja[numCliente] = new PictureBox(); // Añadimos pictureBox al vector
                    CartasEnBaraja[numCliente].Size = new Size(80, 120); // Tamaño de la carta
                    CartasEnBaraja[numCliente].SizeMode = PictureBoxSizeMode.StretchImage; // Ajustar tamaño de imagen
                    CartasEnBaraja[numCliente].Tag = "Central";

                    // Cargar la imagen de la carta
                    string rutaImagen2 = Path.Combine("Images", "cards", cartasEnBaraja[numCliente] + ".png");
                    if (File.Exists(rutaImagen2))
                    {
                        CartasEnBaraja[numCliente].Name = Convert.ToString(cartasEnBaraja[numCliente]); // Guardamos el id como el nombre del pictureBox
                        CartasEnBaraja[numCliente].Image = Image.FromFile(rutaImagen2);
                    }
                    else
                    {
                        // Si la imagen no se encuentra, mostrar un mensaje en el PictureBox
                        CartasEnBaraja[numCliente].BackColor = Color.Gray;
                        CartasEnBaraja[numCliente].BorderStyle = BorderStyle.FixedSingle;
                        CartasEnBaraja[numCliente].Text = "Imagen no encontrada";
                    }

                    // Posicion de la baraja central
                    Size formsize = this.ClientSize;
                    int x = (formsize.Width - CartasEnBaraja[numCliente].Width) / 2;
                    int y = ((formsize.Height - CartasEnBaraja[numCliente].Height) / 2) - 150;
                    CartasEnBaraja[numCliente].Location = new Point(x, y); // Movemos la carta al frente de la baraja central

                    CartasEnBaraja[numCliente].MouseDown += PictureBox_MouseDown; // Subscribimos al metodo para clickar

                    Controls.Add(CartasEnBaraja[numCliente]); // Metemos la carta en el formulario
                    CartasEnBaraja[numCliente].BringToFront(); // Ponemos al frente
                }
                else // Si se elimina una carta a la baraja central 
                {
                    Controls.Remove(CartasEnBaraja[numServidor]); // Quitamos la carta del formulario
                    CartasEnBaraja[numServidor] = null; // Borramos la carta
                }

                // Divide la cadena trozos[3] en subcadenas usando el guión "-"
                string[] subTrozosJugadores = trozos[3].Split('-');

                int numJugadores = Convert.ToInt32(subTrozosJugadores[0]);

                // Almacena los nombres de los jugadores y el número de cartas de cada uno
                nombresJugadores.Clear();
                cartasPorJugador.Clear();

                clientWidth = this.ClientSize.Width;
                clientHeight = this.ClientSize.Height;

                string rutaImagen = Path.Combine("Images", "cards", "99" + ".png");

                for (int i = 0; i < numJugadores; i++)
                {
                    if (subTrozosJugadores[2 * i + 1] != usuario) // Si no es el propio jugador
                    {
                        nombresJugadores.Add(subTrozosJugadores[2 * i + 1]); // Obtenemos nombre de cada jugador
                        cartasPorJugador.Add(Convert.ToInt32(subTrozosJugadores[2 * i + 2]));  // Obtenemos el numero de cartas de cada jugador
                    }
                }
                int j = 0;
                for (int i = 0; i < numJugadores - 1; i++)
                {
                    if (nombresJugadores[i] != usuario)
                    {
                        jugadores[j] = nombresJugadores[i]; // Metemos a los jugadores de la sala en el vector
                        j++;
                    }
                }
                // Quitamos cartas de jugadores en sala
                int m = 0;
                while (CartasReves2[m] != null)
                {
                    Controls.Remove(CartasReves2[m]);
                    CartasReves2[m] = null;
                    m++;
                }
                m = 0;
                while (CartasReves3[m] != null)
                {
                    Controls.Remove(CartasReves3[m]);
                    CartasReves3[m] = null;
                    m++;
                }
                m = 0;
                while (CartasReves4[m] != null)
                {
                    Controls.Remove(CartasReves4[m]);
                    CartasReves4[m] = null;
                    m++;
                }
                if (numJugadores - 1 > 0)
                {
                    labelPlayer2.Text = jugadores[0] + " - " + cartasPorJugador[0];
                    labelPlayer2.Visible = true;
                    labelPlayer3.Visible = false;
                    labelPlayer4.Visible = false;

                    for (int i = 0; i < cartasPorJugador[0]; i++)
                    {
                        CartasReves2[i] = new PictureBox();
                        CartasReves2[i].Visible = true;
                        CartasReves2[i].SizeMode = PictureBoxSizeMode.StretchImage;

                        Bitmap original = new Bitmap(rutaImagen);
                        Bitmap rotada = new Bitmap(original.Height, original.Width);
                        using (Graphics g = Graphics.FromImage(rotada))
                        {
                            g.TranslateTransform((float)rotada.Width / 2, (float)rotada.Height / 2);
                            g.RotateTransform(90);
                            g.TranslateTransform(-(float)original.Width / 2, -(float)original.Height / 2);
                            g.DrawImage(original, new Point(0, 0));
                        }

                        CartasReves2[i].Image = rotada;
                        this.Controls.Add(CartasReves2[i]);
                        CartasReves2[i].Location = new Point(clientWidth * 1 / 4, clientHeight * 1 / 3 + i * 20);
                        CartasReves2[i].Size = new Size(120, 80);
                    }
                    if (numJugadores - 1 > 1)
                    {
                        labelPlayer3.Text = jugadores[1] + " - " + cartasPorJugador[1];
                        labelPlayer3.Visible = true;
                        labelPlayer4.Visible = false;

                        for (int i = 0; i < cartasPorJugador[1]; i++) 
                        {
                            CartasReves3[i] = new PictureBox();
                            CartasReves3[i].Visible = true;
                            CartasReves3[i].SizeMode = PictureBoxSizeMode.StretchImage;

                            Bitmap original = new Bitmap(rutaImagen);
                            Bitmap rotada = new Bitmap(original.Height, original.Width);
                            using (Graphics g = Graphics.FromImage(rotada))
                            {
                                g.TranslateTransform((float)rotada.Width / 2, (float)rotada.Height / 2);
                                g.RotateTransform(90);
                                g.TranslateTransform(-(float)original.Width / 2, -(float)original.Height / 2);
                                g.DrawImage(original, new Point(0, 0));
                            }

                            CartasReves3[i].Image = rotada;
                            this.Controls.Add(CartasReves3[i]);
                            CartasReves3[i].Location = new Point(clientWidth * 1 / 4, clientHeight * 1 / 3 + i * 20);
                            CartasReves3[i].Size = new Size(120, 80);
                        }
                        if (numJugadores - 1 > 2)
                        {
                            labelPlayer4.Text = jugadores[2] + " - " + cartasPorJugador[2];
                            labelPlayer4.Visible = true;

                            for (int i = 0; i < cartasPorJugador[2]; i++) 
                            {
                                CartasReves4[i] = new PictureBox();

                                CartasReves4[i].Visible = true;
                                CartasReves4[i].SizeMode = PictureBoxSizeMode.StretchImage;

                                Bitmap original = new Bitmap(rutaImagen);
                                Bitmap rotada = new Bitmap(original.Height, original.Width);
                                using (Graphics g = Graphics.FromImage(rotada))
                                {
                                    g.TranslateTransform((float)rotada.Width / 2, (float)rotada.Height / 2);
                                    g.RotateTransform(90);
                                    g.TranslateTransform(-(float)original.Width / 2, -(float)original.Height / 2);
                                    g.DrawImage(original, new Point(0, 0));
                                }

                                CartasReves4[i].Image = rotada;
                                this.Controls.Add(CartasReves4[i]);
                                CartasReves4[i].Location = new Point(clientWidth * 1 / 4, clientHeight * 1 / 3 + i * 20);
                                CartasReves4[i].Size = new Size(120, 80);
                            }
                        }
                    }
                }
                else // Si no hay más jugadores a parte del usuario
                {
                    labelPlayer2.Visible = false;
                    labelPlayer3.Visible = false;
                    labelPlayer4.Visible = false;
                }
            });
        }
        async public void EmpezarPartida()
        {
            buttonPedirCarta.Invoke(new Action(() =>
            {
                dataGridViewConectados.Visible = false;
                labelConectados.Visible = false;

                clientWidth = this.ClientSize.Width;
                clientHeight = this.ClientSize.Height;

                labelPlayer2.Location = new Point(clientWidth * 1 / 4 - clientWidth * 1 / 25, clientHeight * 1 / 3);
                labelPlayer3.Location = new Point(clientWidth * 40 / 100, clientHeight * 1 / 4 - clientHeight * 1/15);
                labelPlayer4.Location = new Point(clientWidth * 60 / 100 + clientWidth * 1 / 13, clientHeight * 1 / 3);
            }));

            // Forzamos delay para no saturar al servidor

            // Utiliza la codificación UTF-8 para obtener los bytes del string
            byte[] bytes = Encoding.UTF8.GetBytes(usuario + master + numForm);

            // Contar los bits con valor '1' con reinicio cada 50 bits
            int totalBits = 0;
            foreach (byte b in bytes)
            {
                totalBits += CountBits(b);
                if (totalBits > 50)
                {
                    totalBits = totalBits % 51; // Resetea a 0 si supera 50 (usamos 51 porque el resto de 51 es 0)
                }
            }

            Random random = new Random();

            // Genera un delay aleatorio entre 1000 ms (1 segundo) y 5000 ms (5 segundos)
            int delayMilliseconds = random.Next(100, 500) + totalBits * 10; // Añadimos aleatoriedad por si un mismo usuario (ordenador) juga con varios usuarios

            await Task.Delay(delayMilliseconds);


            partidaEmpezada = true;
            for(int i = 0; i < 6; i++)
            {
                PideCarta();
                await Task.Delay(500);
            }
            buttonPedirCarta.Invoke(new Action(() =>
            {
                buttonPedirCarta.Visible = true;
            }));
        }
        static int CountBits(byte b)
        {
            // Función para contar los bits con valor '1' en un byte

            int count = 0;
            while (b != 0)
            {
                count += b & 1;  // Incrementa el contador si el bit menos significativo es 1
                b >>= 1;         // Desplaza los bits a la derecha
            }
            return count;
        }
        bool partidaEmpezada = false;
        int[] cartas = new int[20];
        private void EmpezarPartida_Click(object sender, EventArgs e)
        {
            if (conectado && !partidaEmpezada && enSala)
            {
                // Enviamos al servidor el mensaje de empezar la partida
                string mensaje = "17/" + sala;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
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

            string mensaje = "11/" + sala + "/" + invitado; // Un miembro de la sala invita a otro usuario
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
        bool desconectar = false;
        private void buttonAbandonar_Click(object sender, EventArgs e)
        {
            string mensaje = "12/" + sala + "/" + usuario; // El usuario abandona la sala
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            buttonAbandonar.Visible = false;
            labelSala.Visible = false;
            labelYourCards.Visible = false;
            labelPlayer2.Visible = false;
            labelPlayer3.Visible = false;
            labelPlayer4.Visible = false;
            desconectar = true;
        }
        public void Desconecta()
        {
            this.Invoke((MethodInvoker)delegate { // Cerramos el form de la sala
                this.Close();
            });
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
                string mensaje = "15/" + sala + "/" + usuario + "/" + textBoxChat.Text; // Pasamos mensaje
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                // Limpia el TextBox
                textBoxChat.Clear(); 

                // Indica que el evento ha sido manejado
                e.Handled = true;
            }
        }

       

        private void buttonPedirCarta_Click(object sender, EventArgs e)
        {
            if (conectado &&/* partidaEmpezada &&*/ enSala) // ==============================-----------------------------------=================== DESCOMENTAAAAAAAAAAAAAAAAAAAAAAAR!!!!!!!!!!!!
            {
                PideCarta(); // Pedimos carta al server
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!desconectar)
            {
                string mensaje = "12/" + sala + "/" + usuario; // El usuario abandona la sala
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }

        }
        //private PictureBox DetectarPictureBox(Point posicion)
        //{
        //    PictureBox objetivo = null;

        //    int i = 0;

        //    while (CartasEnBaraja[i] != null)
        //    {
        //        i++;    
        //    }

        //    if (CartasEnBaraja[i - 1].Location == posicion)
        //    {
        //        objetivo = CartasEnBaraja[i - 1];
        //        return objetivo;
        //    }
        //    else
        //    { return null; }
        //}
    }
}
