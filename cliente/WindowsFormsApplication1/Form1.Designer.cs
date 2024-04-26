namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Remove = new System.Windows.Forms.RadioButton();
            this.password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LogIn = new System.Windows.Forms.RadioButton();
            this.SingUp = new System.Windows.Forms.RadioButton();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.MasPartidas = new System.Windows.Forms.Button();
            this.MasELO = new System.Windows.Forms.Button();
            this.ColorCarta = new System.Windows.Forms.Button();
            this.btnEmpezarPartida = new System.Windows.Forms.Button();
            this.labelPlayer2 = new System.Windows.Forms.Label();
            this.labelPlayer4 = new System.Windows.Forms.Label();
            this.labelYourCards = new System.Windows.Forms.Label();
            this.labelPlayer3 = new System.Windows.Forms.Label();
            this.labelConectados = new System.Windows.Forms.Label();
            this.dataGridViewConectados = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConectados)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name";
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(169, 34);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(164, 20);
            this.nombre.TabIndex = 3;
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Location = new System.Drawing.Point(1284, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(147, 53);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(241, 157);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(94, 39);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.Send_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.Remove);
            this.groupBox1.Controls.Add(this.password);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.LogIn);
            this.groupBox1.Controls.Add(this.SingUp);
            this.groupBox1.Controls.Add(this.btnSend);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nombre);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 204);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Peticion";
            // 
            // Remove
            // 
            this.Remove.AutoSize = true;
            this.Remove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Remove.Location = new System.Drawing.Point(28, 165);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(189, 24);
            this.Remove.TabIndex = 11;
            this.Remove.TabStop = true;
            this.Remove.Text = "Remove my account =(";
            this.Remove.UseVisualStyleBackColor = true;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(170, 60);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(164, 20);
            this.password.TabIndex = 10;
            this.password.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "Password";
            // 
            // LogIn
            // 
            this.LogIn.AutoSize = true;
            this.LogIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogIn.Location = new System.Drawing.Point(28, 135);
            this.LogIn.Name = "LogIn";
            this.LogIn.Size = new System.Drawing.Size(72, 24);
            this.LogIn.TabIndex = 7;
            this.LogIn.TabStop = true;
            this.LogIn.Text = "Log In";
            this.LogIn.UseVisualStyleBackColor = true;
            // 
            // SingUp
            // 
            this.SingUp.AutoSize = true;
            this.SingUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SingUp.Location = new System.Drawing.Point(28, 105);
            this.SingUp.Name = "SingUp";
            this.SingUp.Size = new System.Drawing.Size(88, 24);
            this.SingUp.TabIndex = 8;
            this.SingUp.TabStop = true;
            this.SingUp.Text = "Sing Up!";
            this.SingUp.UseVisualStyleBackColor = true;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDisconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.Location = new System.Drawing.Point(1284, 74);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(147, 53);
            this.btnDisconnect.TabIndex = 10;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // MasPartidas
            // 
            this.MasPartidas.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MasPartidas.Location = new System.Drawing.Point(12, 231);
            this.MasPartidas.Name = "MasPartidas";
            this.MasPartidas.Size = new System.Drawing.Size(147, 79);
            this.MasPartidas.TabIndex = 11;
            this.MasPartidas.Text = "Jugador con mas partidas";
            this.MasPartidas.UseVisualStyleBackColor = true;
            this.MasPartidas.Click += new System.EventHandler(this.MasPartidas_Click);
            // 
            // MasELO
            // 
            this.MasELO.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MasELO.Location = new System.Drawing.Point(12, 316);
            this.MasELO.Name = "MasELO";
            this.MasELO.Size = new System.Drawing.Size(147, 79);
            this.MasELO.TabIndex = 12;
            this.MasELO.Text = "Jugador con mas ELO";
            this.MasELO.UseVisualStyleBackColor = true;
            this.MasELO.Click += new System.EventHandler(this.MasELO_Click);
            // 
            // ColorCarta
            // 
            this.ColorCarta.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColorCarta.Location = new System.Drawing.Point(12, 401);
            this.ColorCarta.Name = "ColorCarta";
            this.ColorCarta.Size = new System.Drawing.Size(147, 79);
            this.ColorCarta.TabIndex = 13;
            this.ColorCarta.Text = "Color de la carta +4";
            this.ColorCarta.UseVisualStyleBackColor = true;
            this.ColorCarta.Click += new System.EventHandler(this.ColorCarta_Click);
            // 
            // btnEmpezarPartida
            // 
            this.btnEmpezarPartida.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEmpezarPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmpezarPartida.Location = new System.Drawing.Point(1230, 725);
            this.btnEmpezarPartida.Name = "btnEmpezarPartida";
            this.btnEmpezarPartida.Size = new System.Drawing.Size(201, 53);
            this.btnEmpezarPartida.TabIndex = 15;
            this.btnEmpezarPartida.Text = "Empezar partida";
            this.btnEmpezarPartida.UseVisualStyleBackColor = true;
            this.btnEmpezarPartida.Click += new System.EventHandler(this.EmpezarPartida_Click);
            // 
            // labelPlayer2
            // 
            this.labelPlayer2.AutoSize = true;
            this.labelPlayer2.Location = new System.Drawing.Point(470, 316);
            this.labelPlayer2.Name = "labelPlayer2";
            this.labelPlayer2.Size = new System.Drawing.Size(36, 13);
            this.labelPlayer2.TabIndex = 16;
            this.labelPlayer2.Text = "Player";
            this.labelPlayer2.Visible = false;
            this.labelPlayer2.Click += new System.EventHandler(this.Player2_Click);
            // 
            // labelPlayer4
            // 
            this.labelPlayer4.AutoSize = true;
            this.labelPlayer4.Location = new System.Drawing.Point(470, 249);
            this.labelPlayer4.Name = "labelPlayer4";
            this.labelPlayer4.Size = new System.Drawing.Size(36, 13);
            this.labelPlayer4.TabIndex = 17;
            this.labelPlayer4.Text = "Player";
            this.labelPlayer4.Visible = false;
            // 
            // labelYourCards
            // 
            this.labelYourCards.AutoSize = true;
            this.labelYourCards.Location = new System.Drawing.Point(715, 522);
            this.labelYourCards.Name = "labelYourCards";
            this.labelYourCards.Size = new System.Drawing.Size(59, 13);
            this.labelYourCards.TabIndex = 18;
            this.labelYourCards.Text = "Your Cards";
            this.labelYourCards.Visible = false;
            // 
            // labelPlayer3
            // 
            this.labelPlayer3.AutoSize = true;
            this.labelPlayer3.Location = new System.Drawing.Point(470, 283);
            this.labelPlayer3.Name = "labelPlayer3";
            this.labelPlayer3.Size = new System.Drawing.Size(36, 13);
            this.labelPlayer3.TabIndex = 19;
            this.labelPlayer3.Text = "Player";
            this.labelPlayer3.Visible = false;
            // 
            // labelConectados
            // 
            this.labelConectados.AutoSize = true;
            this.labelConectados.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConectados.Location = new System.Drawing.Point(172, 234);
            this.labelConectados.Name = "labelConectados";
            this.labelConectados.Size = new System.Drawing.Size(183, 18);
            this.labelConectados.TabIndex = 21;
            this.labelConectados.Text = "Jugadores Conectados";
            this.labelConectados.Visible = false;
            // 
            // dataGridViewConectados
            // 
            this.dataGridViewConectados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewConectados.Location = new System.Drawing.Point(175, 266);
            this.dataGridViewConectados.Name = "dataGridViewConectados";
            this.dataGridViewConectados.Size = new System.Drawing.Size(172, 214);
            this.dataGridViewConectados.TabIndex = 22;
            this.dataGridViewConectados.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 790);
            this.Controls.Add(this.dataGridViewConectados);
            this.Controls.Add(this.labelConectados);
            this.Controls.Add(this.labelPlayer3);
            this.Controls.Add(this.labelYourCards);
            this.Controls.Add(this.labelPlayer4);
            this.Controls.Add(this.labelPlayer2);
            this.Controls.Add(this.btnEmpezarPartida);
            this.Controls.Add(this.ColorCarta);
            this.Controls.Add(this.MasELO);
            this.Controls.Add(this.MasPartidas);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConectados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton LogIn;
        private System.Windows.Forms.RadioButton SingUp;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.RadioButton Remove;
        private System.Windows.Forms.Button MasPartidas;
        private System.Windows.Forms.Button MasELO;
        private System.Windows.Forms.Button ColorCarta;
        private System.Windows.Forms.Button btnEmpezarPartida;
        private System.Windows.Forms.Label labelPlayer2;
        private System.Windows.Forms.Label labelPlayer4;
        private System.Windows.Forms.Label labelYourCards;
        private System.Windows.Forms.Label labelPlayer3;
        private System.Windows.Forms.Label labelConectados;
        private System.Windows.Forms.DataGridView dataGridViewConectados;
    }
}

