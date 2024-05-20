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

namespace WindowsFormsApplication1
{
    public partial class FormMenu : Form
    {
        Socket server;
        Thread atender;

        delegate void DelegadoParaPonerTexto(string texto);

        List<Form1> formularios = new List<Form1>();

        public FormMenu()
        {
            InitializeComponent();
            toolTip1.SetToolTip(pictureBoxLogo, "Haz click para empezar a jugar");
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            CenterPictureBox(pictureBoxLogo);
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
            Form1 form1 = new Form1();
            form1.Show();
        }





        //private void AtenderServidor()
        //{
        //    while (true)
        //    {
        //        //Recibimos mensaje del servidor
        //        byte[] msg2 = new byte[80];
        //        server.Receive(msg2);
        //        string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
        //        int codigo = Convert.ToInt32(trozos[0]);
        //        string mensaje;

        //        int nform;
        //        switch (codigo)
        //        {
        //            case 1:  // respuesta a longitud

        //                nform = Convert.ToInt32(trozos[1]);
        //                mensaje = trozos[2].Split('\0')[0];
        //                formularios[nform].TomaRespuesta1(mensaje);

        //                break;
        //            case 2:      //respuesta a si mi nombre es bonito

        //                nform = Convert.ToInt32(trozos[1]);
        //                mensaje = trozos[2].Split('\0')[0];
        //                formularios[nform].TomaRespuesta2(mensaje);

        //                break;
        //            case 3:       //Recibimos la respuesta de si soy alto
        //                nform = Convert.ToInt32(trozos[1]);
        //                mensaje = trozos[2].Split('\0')[0];
        //                formularios[nform].TomaRespuesta3(mensaje);
        //                break;

        //            case 4:     //Recibimos notificacion


        //                mensaje = trozos[1].Split('\0')[0];

        //                //Haz tu lo que no me dejas hacer a mi
        //                contLbl.Invoke(new Action(() =>
        //                {
        //                    contLbl.Text = mensaje;
        //                }));

        //                break;
        //        }
        //    }
        //}
    }
}
