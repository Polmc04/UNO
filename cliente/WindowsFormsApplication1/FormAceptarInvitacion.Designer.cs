namespace WindowsFormsApplication1
{
    partial class FormAceptarInvitacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonAceptar = new System.Windows.Forms.Button();
            this.buttonRechazar = new System.Windows.Forms.Button();
            this.labelSala = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonAceptar
            // 
            this.buttonAceptar.Location = new System.Drawing.Point(359, 212);
            this.buttonAceptar.Name = "buttonAceptar";
            this.buttonAceptar.Size = new System.Drawing.Size(75, 23);
            this.buttonAceptar.TabIndex = 0;
            this.buttonAceptar.Text = "Aceptar";
            this.buttonAceptar.UseVisualStyleBackColor = true;
            this.buttonAceptar.Click += new System.EventHandler(this.buttonAceptar_Click);
            // 
            // buttonRechazar
            // 
            this.buttonRechazar.Location = new System.Drawing.Point(458, 212);
            this.buttonRechazar.Name = "buttonRechazar";
            this.buttonRechazar.Size = new System.Drawing.Size(75, 23);
            this.buttonRechazar.TabIndex = 1;
            this.buttonRechazar.Text = "Rechazar";
            this.buttonRechazar.UseVisualStyleBackColor = true;
            this.buttonRechazar.Click += new System.EventHandler(this.buttonRechazar_Click);
            // 
            // labelSala
            // 
            this.labelSala.AutoSize = true;
            this.labelSala.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSala.Location = new System.Drawing.Point(34, 47);
            this.labelSala.Name = "labelSala";
            this.labelSala.Size = new System.Drawing.Size(193, 20);
            this.labelSala.TabIndex = 2;
            this.labelSala.Text = "Te han invitado a la Sala : ";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(38, 85);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(239, 150);
            this.dataGridView1.TabIndex = 3;
            // 
            // FormAceptarInvitacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 247);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.labelSala);
            this.Controls.Add(this.buttonRechazar);
            this.Controls.Add(this.buttonAceptar);
            this.Name = "FormAceptarInvitacion";
            this.Text = "Invitación a Sala";
            this.Load += new System.EventHandler(this.FormAceptarInvitacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAceptar;
        private System.Windows.Forms.Button buttonRechazar;
        private System.Windows.Forms.Label labelSala;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}