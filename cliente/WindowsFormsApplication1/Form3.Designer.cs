namespace WindowsFormsApplication1
{
    partial class FormMenu
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
            this.components = new System.ComponentModel.Container();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.labelDisclaimer = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioRemove = new System.Windows.Forms.RadioButton();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelUsuario = new System.Windows.Forms.Label();
            this.radioLogIn = new System.Windows.Forms.RadioButton();
            this.radioSingUp = new System.Windows.Forms.RadioButton();
            this.btnSend = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxNombre = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.labelChat = new System.Windows.Forms.Label();
            this.textBoxChat = new System.Windows.Forms.TextBox();
            this.richTextBoxChat = new System.Windows.Forms.RichTextBox();
            this.ColorCarta = new System.Windows.Forms.Button();
            this.MasELO = new System.Windows.Forms.Button();
            this.MasPartidas = new System.Windows.Forms.Button();
            this.textBoxJugador = new System.Windows.Forms.TextBox();
            this.textBoxInicio = new System.Windows.Forms.TextBox();
            this.textBoxFin = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxLogo.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxLogo.Image = global::WindowsFormsApplication1.Properties.Resources.UNO_Logo;
            this.pictureBoxLogo.Location = new System.Drawing.Point(468, 54);
            this.pictureBoxLogo.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxLogo.MaximumSize = new System.Drawing.Size(450, 488);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(375, 406);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 1;
            this.pictureBoxLogo.TabStop = false;
            this.pictureBoxLogo.Tag = "Hola";
            this.pictureBoxLogo.Click += new System.EventHandler(this.pictureBoxLogo_Click);
            // 
            // labelDisclaimer
            // 
            this.labelDisclaimer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelDisclaimer.AutoSize = true;
            this.labelDisclaimer.BackColor = System.Drawing.Color.Transparent;
            this.labelDisclaimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDisclaimer.Location = new System.Drawing.Point(402, 541);
            this.labelDisclaimer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDisclaimer.Name = "labelDisclaimer";
            this.labelDisclaimer.Size = new System.Drawing.Size(461, 17);
            this.labelDisclaimer.TabIndex = 2;
            this.labelDisclaimer.Text = "Disclaimer: No nos lucramos económicamente del uso de la marca UNO";
            this.labelDisclaimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDisconnect.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnDisconnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDisconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.Location = new System.Drawing.Point(1100, 72);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(147, 53);
            this.btnDisconnect.TabIndex = 13;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = false;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.radioRemove);
            this.groupBox1.Controls.Add(this.textBoxPassword);
            this.groupBox1.Controls.Add(this.labelPassword);
            this.groupBox1.Controls.Add(this.labelUsuario);
            this.groupBox1.Controls.Add(this.radioLogIn);
            this.groupBox1.Controls.Add(this.radioSingUp);
            this.groupBox1.Controls.Add(this.btnSend);
            this.groupBox1.Controls.Add(this.labelName);
            this.groupBox1.Controls.Add(this.textBoxNombre);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.groupBox1.Location = new System.Drawing.Point(10, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 204);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User Menu";
            // 
            // radioRemove
            // 
            this.radioRemove.AutoSize = true;
            this.radioRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioRemove.ForeColor = System.Drawing.SystemColors.HighlightText;
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
            this.textBoxPassword.Text = "1";
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.labelPassword.ForeColor = System.Drawing.SystemColors.HighlightText;
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
            this.labelUsuario.ForeColor = System.Drawing.SystemColors.HighlightText;
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
            this.radioLogIn.Checked = true;
            this.radioLogIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioLogIn.ForeColor = System.Drawing.SystemColors.HighlightText;
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
            this.radioSingUp.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.radioSingUp.Location = new System.Drawing.Point(28, 105);
            this.radioSingUp.Name = "radioSingUp";
            this.radioSingUp.Size = new System.Drawing.Size(88, 24);
            this.radioSingUp.TabIndex = 8;
            this.radioSingUp.TabStop = true;
            this.radioSingUp.Text = "Sing Up!";
            this.radioSingUp.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSend.Location = new System.Drawing.Point(241, 157);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(94, 39);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.labelName.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.labelName.Location = new System.Drawing.Point(24, 29);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(55, 20);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "Name";
            // 
            // textBoxNombre
            // 
            this.textBoxNombre.Location = new System.Drawing.Point(170, 34);
            this.textBoxNombre.Name = "textBoxNombre";
            this.textBoxNombre.Size = new System.Drawing.Size(164, 20);
            this.textBoxNombre.TabIndex = 3;
            this.textBoxNombre.Text = "Pol";
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.BackColor = System.Drawing.Color.LimeGreen;
            this.btnConnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnConnect.Location = new System.Drawing.Point(1100, 11);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(147, 53);
            this.btnConnect.TabIndex = 11;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // labelChat
            // 
            this.labelChat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelChat.BackColor = System.Drawing.Color.Transparent;
            this.labelChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelChat.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.labelChat.Location = new System.Drawing.Point(995, 398);
            this.labelChat.Name = "labelChat";
            this.labelChat.Size = new System.Drawing.Size(161, 31);
            this.labelChat.TabIndex = 32;
            this.labelChat.Text = "Chat global";
            this.labelChat.Visible = false;
            // 
            // textBoxChat
            // 
            this.textBoxChat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxChat.Location = new System.Drawing.Point(999, 564);
            this.textBoxChat.Name = "textBoxChat";
            this.textBoxChat.Size = new System.Drawing.Size(248, 20);
            this.textBoxChat.TabIndex = 31;
            this.textBoxChat.Visible = false;
            // 
            // richTextBoxChat
            // 
            this.richTextBoxChat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxChat.Location = new System.Drawing.Point(999, 432);
            this.richTextBoxChat.Name = "richTextBoxChat";
            this.richTextBoxChat.ReadOnly = true;
            this.richTextBoxChat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxChat.Size = new System.Drawing.Size(248, 126);
            this.richTextBoxChat.TabIndex = 30;
            this.richTextBoxChat.Text = "";
            this.richTextBoxChat.Visible = false;
            // 
            // ColorCarta
            // 
            this.ColorCarta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColorCarta.Location = new System.Drawing.Point(12, 449);
            this.ColorCarta.Name = "ColorCarta";
            this.ColorCarta.Size = new System.Drawing.Size(147, 79);
            this.ColorCarta.TabIndex = 35;
            this.ColorCarta.Text = "Resultado de partidas entre fechas:";
            this.ColorCarta.UseVisualStyleBackColor = true;
            this.ColorCarta.Click += new System.EventHandler(this.ColorCarta_Click);
            // 
            // MasELO
            // 
            this.MasELO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MasELO.Location = new System.Drawing.Point(12, 364);
            this.MasELO.Name = "MasELO";
            this.MasELO.Size = new System.Drawing.Size(147, 79);
            this.MasELO.TabIndex = 34;
            this.MasELO.Text = "Resultado de las partidas contra:";
            this.MasELO.UseVisualStyleBackColor = true;
            this.MasELO.Click += new System.EventHandler(this.MasELO_Click);
            // 
            // MasPartidas
            // 
            this.MasPartidas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MasPartidas.Location = new System.Drawing.Point(12, 279);
            this.MasPartidas.Name = "MasPartidas";
            this.MasPartidas.Size = new System.Drawing.Size(147, 79);
            this.MasPartidas.TabIndex = 33;
            this.MasPartidas.Text = "Listar jugadores contra los que has jugado";
            this.MasPartidas.UseVisualStyleBackColor = true;
            this.MasPartidas.Click += new System.EventHandler(this.MasPartidas_Click);
            // 
            // textBoxJugador
            // 
            this.textBoxJugador.Location = new System.Drawing.Point(165, 364);
            this.textBoxJugador.Name = "textBoxJugador";
            this.textBoxJugador.Size = new System.Drawing.Size(105, 20);
            this.textBoxJugador.TabIndex = 36;
            this.textBoxJugador.Text = "(Nombre del jugador)";
            this.textBoxJugador.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBoxInicio
            // 
            this.textBoxInicio.Location = new System.Drawing.Point(165, 449);
            this.textBoxInicio.Name = "textBoxInicio";
            this.textBoxInicio.Size = new System.Drawing.Size(154, 20);
            this.textBoxInicio.TabIndex = 37;
            this.textBoxInicio.Text = "Fecha de inicio (aaaa-mm-dd)";
            // 
            // textBoxFin
            // 
            this.textBoxFin.Location = new System.Drawing.Point(165, 475);
            this.textBoxFin.Name = "textBoxFin";
            this.textBoxFin.Size = new System.Drawing.Size(154, 20);
            this.textBoxFin.TabIndex = 38;
            this.textBoxFin.Text = "Fecha de final (aaaa-mm-dd)";
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources._cad35566_f31b_49ec_875d_bdb0f57f266e1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1257, 594);
            this.Controls.Add(this.textBoxFin);
            this.Controls.Add(this.textBoxInicio);
            this.Controls.Add(this.textBoxJugador);
            this.Controls.Add(this.ColorCarta);
            this.Controls.Add(this.MasELO);
            this.Controls.Add(this.MasPartidas);
            this.Controls.Add(this.labelChat);
            this.Controls.Add(this.textBoxChat);
            this.Controls.Add(this.richTextBoxChat);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.labelDisclaimer);
            this.Controls.Add(this.pictureBoxLogo);
            this.Name = "FormMenu";
            this.Text = "Menú";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMenu_FormClosing);
            this.Load += new System.EventHandler(this.FormMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelDisclaimer;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioRemove;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelUsuario;
        private System.Windows.Forms.RadioButton radioLogIn;
        private System.Windows.Forms.RadioButton radioSingUp;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxNombre;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label labelChat;
        private System.Windows.Forms.TextBox textBoxChat;
        private System.Windows.Forms.RichTextBox richTextBoxChat;
        private System.Windows.Forms.Button ColorCarta;
        private System.Windows.Forms.Button MasELO;
        private System.Windows.Forms.Button MasPartidas;
        private System.Windows.Forms.TextBox textBoxJugador;
        private System.Windows.Forms.TextBox textBoxInicio;
        private System.Windows.Forms.TextBox textBoxFin;
    }
}