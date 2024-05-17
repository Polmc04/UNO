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
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxNombre = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioRemove = new System.Windows.Forms.RadioButton();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelUsuario = new System.Windows.Forms.Label();
            this.radioLogIn = new System.Windows.Forms.RadioButton();
            this.radioSingUp = new System.Windows.Forms.RadioButton();
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
            this.labelSala = new System.Windows.Forms.Label();
            this.buttonAbandonar = new System.Windows.Forms.Button();
            this.buttonCrearSala = new System.Windows.Forms.Button();
            this.richTextBoxChat = new System.Windows.Forms.RichTextBox();
            this.textBoxChat = new System.Windows.Forms.TextBox();
            this.labelChat = new System.Windows.Forms.Label();
            this.buttonPedirCarta = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConectados)).BeginInit();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.labelName.Location = new System.Drawing.Point(24, 29);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(55, 20);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "Name";
            // 
            // textBoxNombre
            // 
            this.textBoxNombre.Location = new System.Drawing.Point(169, 34);
            this.textBoxNombre.Name = "textBoxNombre";
            this.textBoxNombre.Size = new System.Drawing.Size(164, 20);
            this.textBoxNombre.TabIndex = 3;
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
            this.groupBox1.Controls.Add(this.radioRemove);
            this.groupBox1.Controls.Add(this.textBoxPassword);
            this.groupBox1.Controls.Add(this.labelPassword);
            this.groupBox1.Controls.Add(this.labelUsuario);
            this.groupBox1.Controls.Add(this.radioLogIn);
            this.groupBox1.Controls.Add(this.radioSingUp);
            this.groupBox1.Controls.Add(this.btnSend);
            this.groupBox1.Controls.Add(this.labelName);
            this.groupBox1.Controls.Add(this.textBoxNombre);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 204);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Peticion";
            // 
            // radioRemove
            // 
            this.radioRemove.AutoSize = true;
            this.radioRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioRemove.Location = new System.Drawing.Point(28, 165);
            this.radioRemove.Name = "radioRemove";
            this.radioRemove.Size = new System.Drawing.Size(189, 24);
            this.radioRemove.TabIndex = 11;
            this.radioRemove.TabStop = true;
            this.radioRemove.Text = "Remove my account =(";
            this.radioRemove.UseVisualStyleBackColor = true;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(170, 60);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(164, 20);
            this.textBoxPassword.TabIndex = 10;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.labelPassword.Location = new System.Drawing.Point(24, 60);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(86, 20);
            this.labelPassword.TabIndex = 9;
            this.labelPassword.Text = "Password";
            // 
            // labelUsuario
            // 
            this.labelUsuario.AutoSize = true;
            this.labelUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsuario.Location = new System.Drawing.Point(252, 109);
            this.labelUsuario.Name = "labelUsuario";
            this.labelUsuario.Size = new System.Drawing.Size(81, 20);
            this.labelUsuario.TabIndex = 23;
            this.labelUsuario.Text = "Usuario: ";
            this.labelUsuario.Visible = false;
            // 
            // radioLogIn
            // 
            this.radioLogIn.AutoSize = true;
            this.radioLogIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioLogIn.Location = new System.Drawing.Point(28, 135);
            this.radioLogIn.Name = "radioLogIn";
            this.radioLogIn.Size = new System.Drawing.Size(72, 24);
            this.radioLogIn.TabIndex = 7;
            this.radioLogIn.TabStop = true;
            this.radioLogIn.Text = "Log In";
            this.radioLogIn.UseVisualStyleBackColor = true;
            // 
            // radioSingUp
            // 
            this.radioSingUp.AutoSize = true;
            this.radioSingUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioSingUp.Location = new System.Drawing.Point(28, 105);
            this.radioSingUp.Name = "radioSingUp";
            this.radioSingUp.Size = new System.Drawing.Size(88, 24);
            this.radioSingUp.TabIndex = 8;
            this.radioSingUp.TabStop = true;
            this.radioSingUp.Text = "Sing Up!";
            this.radioSingUp.UseVisualStyleBackColor = true;
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
            this.labelPlayer2.Location = new System.Drawing.Point(374, 124);
            this.labelPlayer2.Name = "labelPlayer2";
            this.labelPlayer2.Size = new System.Drawing.Size(36, 13);
            this.labelPlayer2.TabIndex = 16;
            this.labelPlayer2.Text = "Player";
            this.labelPlayer2.Visible = false;
            // 
            // labelPlayer4
            // 
            this.labelPlayer4.AutoSize = true;
            this.labelPlayer4.Location = new System.Drawing.Point(374, 66);
            this.labelPlayer4.Name = "labelPlayer4";
            this.labelPlayer4.Size = new System.Drawing.Size(36, 13);
            this.labelPlayer4.TabIndex = 17;
            this.labelPlayer4.Text = "Player";
            this.labelPlayer4.Visible = false;
            // 
            // labelYourCards
            // 
            this.labelYourCards.AutoSize = true;
            this.labelYourCards.Location = new System.Drawing.Point(374, 154);
            this.labelYourCards.Name = "labelYourCards";
            this.labelYourCards.Size = new System.Drawing.Size(59, 13);
            this.labelYourCards.TabIndex = 18;
            this.labelYourCards.Text = "Your Cards";
            this.labelYourCards.Visible = false;
            // 
            // labelPlayer3
            // 
            this.labelPlayer3.AutoSize = true;
            this.labelPlayer3.Location = new System.Drawing.Point(374, 94);
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
            this.dataGridViewConectados.AllowUserToAddRows = false;
            this.dataGridViewConectados.AllowUserToDeleteRows = false;
            this.dataGridViewConectados.AllowUserToResizeRows = false;
            this.dataGridViewConectados.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewConectados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewConectados.Location = new System.Drawing.Point(175, 266);
            this.dataGridViewConectados.Name = "dataGridViewConectados";
            this.dataGridViewConectados.ReadOnly = true;
            this.dataGridViewConectados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewConectados.Size = new System.Drawing.Size(172, 214);
            this.dataGridViewConectados.TabIndex = 22;
            this.dataGridViewConectados.Visible = false;
            // 
            // labelSala
            // 
            this.labelSala.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSala.Location = new System.Drawing.Point(373, 26);
            this.labelSala.Name = "labelSala";
            this.labelSala.Size = new System.Drawing.Size(92, 20);
            this.labelSala.TabIndex = 24;
            this.labelSala.Text = "Sala:";
            this.labelSala.Visible = false;
            // 
            // buttonAbandonar
            // 
            this.buttonAbandonar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAbandonar.Location = new System.Drawing.Point(560, 67);
            this.buttonAbandonar.Name = "buttonAbandonar";
            this.buttonAbandonar.Size = new System.Drawing.Size(108, 35);
            this.buttonAbandonar.TabIndex = 25;
            this.buttonAbandonar.Text = "Abandonar";
            this.buttonAbandonar.UseVisualStyleBackColor = true;
            this.buttonAbandonar.Visible = false;
            this.buttonAbandonar.Click += new System.EventHandler(this.buttonAbandonar_Click);
            // 
            // buttonCrearSala
            // 
            this.buttonCrearSala.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCrearSala.Location = new System.Drawing.Point(560, 26);
            this.buttonCrearSala.Name = "buttonCrearSala";
            this.buttonCrearSala.Size = new System.Drawing.Size(108, 35);
            this.buttonCrearSala.TabIndex = 26;
            this.buttonCrearSala.Text = "Crear Sala";
            this.buttonCrearSala.UseVisualStyleBackColor = true;
            this.buttonCrearSala.Visible = false;
            this.buttonCrearSala.Click += new System.EventHandler(this.buttonCrearSala_Click);
            // 
            // richTextBoxChat
            // 
            this.richTextBoxChat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxChat.Location = new System.Drawing.Point(1183, 510);
            this.richTextBoxChat.Name = "richTextBoxChat";
            this.richTextBoxChat.ReadOnly = true;
            this.richTextBoxChat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxChat.Size = new System.Drawing.Size(248, 126);
            this.richTextBoxChat.TabIndex = 27;
            this.richTextBoxChat.Text = "";
            this.richTextBoxChat.Visible = false;
            this.richTextBoxChat.TextChanged += new System.EventHandler(this.richTextBoxChat_TextChanged);
            // 
            // textBoxChat
            // 
            this.textBoxChat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxChat.Location = new System.Drawing.Point(1183, 642);
            this.textBoxChat.Name = "textBoxChat";
            this.textBoxChat.Size = new System.Drawing.Size(248, 20);
            this.textBoxChat.TabIndex = 28;
            this.textBoxChat.Visible = false;
            // 
            // labelChat
            // 
            this.labelChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelChat.Location = new System.Drawing.Point(1179, 477);
            this.labelChat.Name = "labelChat";
            this.labelChat.Size = new System.Drawing.Size(92, 20);
            this.labelChat.TabIndex = 29;
            this.labelChat.Text = "Chat";
            this.labelChat.Visible = false;
            // 
            // buttonPedirCarta
            // 
            this.buttonPedirCarta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPedirCarta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPedirCarta.Location = new System.Drawing.Point(1183, 425);
            this.buttonPedirCarta.Name = "buttonPedirCarta";
            this.buttonPedirCarta.Size = new System.Drawing.Size(108, 35);
            this.buttonPedirCarta.TabIndex = 30;
            this.buttonPedirCarta.Text = "Pedir Carta";
            this.buttonPedirCarta.UseVisualStyleBackColor = true;
            this.buttonPedirCarta.Click += new System.EventHandler(this.buttonPedirCarta_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 790);
            this.Controls.Add(this.buttonPedirCarta);
            this.Controls.Add(this.labelChat);
            this.Controls.Add(this.textBoxChat);
            this.Controls.Add(this.richTextBoxChat);
            this.Controls.Add(this.buttonCrearSala);
            this.Controls.Add(this.buttonAbandonar);
            this.Controls.Add(this.labelSala);
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
            this.Text = "UNO";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConectados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxNombre;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioLogIn;
        private System.Windows.Forms.RadioButton radioSingUp;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.RadioButton radioRemove;
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
        private System.Windows.Forms.Label labelUsuario;
        private System.Windows.Forms.Label labelSala;
        private System.Windows.Forms.Button buttonAbandonar;
        private System.Windows.Forms.Button buttonCrearSala;
        private System.Windows.Forms.RichTextBox richTextBoxChat;
        private System.Windows.Forms.TextBox textBoxChat;
        private System.Windows.Forms.Label labelChat;
        private System.Windows.Forms.Button buttonPedirCarta;
    }
}

