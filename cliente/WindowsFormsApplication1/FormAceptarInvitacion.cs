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
    public partial class FormAceptarInvitacion : Form
    {
        public FormAceptarInvitacion(int sala, string[] nombres)
        {
            InitializeComponent();

            // Hacer algo con el número y los nombres recibidos, por ejemplo, mostrarlos en etiquetas o en algún control del formulario
            labelSala.Text = "Te han invitado a la Sala: " + sala.ToString();

            // Configurar el DataGridView para mostrar los nombres
            dataGridView1.ColumnCount = 1; // Definir el número de columnas
            dataGridView1.Columns[0].Name = "Nombre"; // Nombre de la columna

            // Agregar los nombres al DataGridView
            foreach (string nombre in nombres)
            {
                dataGridView1.Rows.Add(nombre);
            }
        }

        private void FormAceptarInvitacion_Load(object sender, EventArgs e)
        {

        }

        public event EventHandler AceptarClicked;
        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            // Cuando se haga clic en buttonAceptar, activa el evento AceptarClicked
            AceptarClicked?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void buttonRechazar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
