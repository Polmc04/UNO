using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FormInvitar : Form
    {
        public event EventHandler InvitarClicked;
        private string usuario;
        public FormInvitar(string textoCelda)
        {
            InitializeComponent();

            this.usuario = textoCelda;   
            labelInvitar.Text = "¿Quieres invitar al usuario " + usuario + " a la sala?";
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void labelInvitar_Click(object sender, EventArgs e)
        {
            InvitarClicked?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void buttonCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
