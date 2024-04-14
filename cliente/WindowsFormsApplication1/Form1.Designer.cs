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
            this.Connect = new System.Windows.Forms.Button();
            this.Send = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Remove = new System.Windows.Forms.RadioButton();
            this.password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LogIn = new System.Windows.Forms.RadioButton();
            this.SingUp = new System.Windows.Forms.RadioButton();
            this.Disconnect = new System.Windows.Forms.Button();
            this.MasPartidas = new System.Windows.Forms.Button();
            this.MasELO = new System.Windows.Forms.Button();
            this.ColorCarta = new System.Windows.Forms.Button();
            this.DimeConectados = new System.Windows.Forms.Button();
            this.EmpezarPartida = new System.Windows.Forms.Button();
            this.Player2 = new System.Windows.Forms.Label();
            this.Player4 = new System.Windows.Forms.Label();
            this.labelYourCards = new System.Windows.Forms.Label();
            this.Player3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
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
            // Connect
            // 
            this.Connect.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Connect.Location = new System.Drawing.Point(1547, 12);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(147, 53);
            this.Connect.TabIndex = 4;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // Send
            // 
            this.Send.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Send.Location = new System.Drawing.Point(241, 157);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(94, 39);
            this.Send.TabIndex = 5;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.Remove);
            this.groupBox1.Controls.Add(this.password);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.LogIn);
            this.groupBox1.Controls.Add(this.SingUp);
            this.groupBox1.Controls.Add(this.Send);
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
            // Disconnect
            // 
            this.Disconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Disconnect.Location = new System.Drawing.Point(1547, 98);
            this.Disconnect.Name = "Disconnect";
            this.Disconnect.Size = new System.Drawing.Size(147, 53);
            this.Disconnect.TabIndex = 10;
            this.Disconnect.Text = "Disconnect";
            this.Disconnect.UseVisualStyleBackColor = true;
            this.Disconnect.Click += new System.EventHandler(this.Disconnect_Click);
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
            // DimeConectados
            // 
            this.DimeConectados.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DimeConectados.Location = new System.Drawing.Point(12, 486);
            this.DimeConectados.Name = "DimeConectados";
            this.DimeConectados.Size = new System.Drawing.Size(147, 79);
            this.DimeConectados.TabIndex = 14;
            this.DimeConectados.Text = "Dime Conectados";
            this.DimeConectados.UseVisualStyleBackColor = true;
            this.DimeConectados.Click += new System.EventHandler(this.DimeConectados_Click);
            // 
            // EmpezarPartida
            // 
            this.EmpezarPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmpezarPartida.Location = new System.Drawing.Point(1494, 779);
            this.EmpezarPartida.Name = "EmpezarPartida";
            this.EmpezarPartida.Size = new System.Drawing.Size(201, 53);
            this.EmpezarPartida.TabIndex = 15;
            this.EmpezarPartida.Text = "Empezar partida";
            this.EmpezarPartida.UseVisualStyleBackColor = true;
            this.EmpezarPartida.Click += new System.EventHandler(this.EmpezarPartida_Click);
            // 
            // Player2
            // 
            this.Player2.AutoSize = true;
            this.Player2.Location = new System.Drawing.Point(507, 382);
            this.Player2.Name = "Player2";
            this.Player2.Size = new System.Drawing.Size(45, 13);
            this.Player2.TabIndex = 16;
            this.Player2.Text = "Player 2";
            this.Player2.Visible = false;
            // 
            // Player4
            // 
            this.Player4.AutoSize = true;
            this.Player4.Location = new System.Drawing.Point(1237, 382);
            this.Player4.Name = "Player4";
            this.Player4.Size = new System.Drawing.Size(45, 13);
            this.Player4.TabIndex = 17;
            this.Player4.Text = "Player 4";
            this.Player4.Visible = false;
            // 
            // labelYourCards
            // 
            this.labelYourCards.AutoSize = true;
            this.labelYourCards.Location = new System.Drawing.Point(863, 522);
            this.labelYourCards.Name = "labelYourCards";
            this.labelYourCards.Size = new System.Drawing.Size(59, 13);
            this.labelYourCards.TabIndex = 18;
            this.labelYourCards.Text = "Your Cards";
            this.labelYourCards.Visible = false;
            // 
            // Player3
            // 
            this.Player3.AutoSize = true;
            this.Player3.Location = new System.Drawing.Point(863, 231);
            this.Player3.Name = "Player3";
            this.Player3.Size = new System.Drawing.Size(45, 13);
            this.Player3.TabIndex = 19;
            this.Player3.Text = "Player 3";
            this.Player3.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1707, 844);
            this.Controls.Add(this.Player3);
            this.Controls.Add(this.labelYourCards);
            this.Controls.Add(this.Player4);
            this.Controls.Add(this.Player2);
            this.Controls.Add(this.EmpezarPartida);
            this.Controls.Add(this.DimeConectados);
            this.Controls.Add(this.ColorCarta);
            this.Controls.Add(this.MasELO);
            this.Controls.Add(this.MasPartidas);
            this.Controls.Add(this.Disconnect);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Connect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton LogIn;
        private System.Windows.Forms.RadioButton SingUp;
        private System.Windows.Forms.Button Disconnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.RadioButton Remove;
        private System.Windows.Forms.Button MasPartidas;
        private System.Windows.Forms.Button MasELO;
        private System.Windows.Forms.Button ColorCarta;
        private System.Windows.Forms.Button DimeConectados;
        private System.Windows.Forms.Button EmpezarPartida;
        private System.Windows.Forms.Label Player2;
        private System.Windows.Forms.Label Player4;
        private System.Windows.Forms.Label labelYourCards;
        private System.Windows.Forms.Label Player3;
    }
}

