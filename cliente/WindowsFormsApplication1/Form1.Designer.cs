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
            this.buttonEmpezarPartida = new System.Windows.Forms.Button();
            this.labelPlayer2 = new System.Windows.Forms.Label();
            this.labelPlayer4 = new System.Windows.Forms.Label();
            this.labelYourCards = new System.Windows.Forms.Label();
            this.labelPlayer3 = new System.Windows.Forms.Label();
            this.labelConectados = new System.Windows.Forms.Label();
            this.dataGridViewConectados = new System.Windows.Forms.DataGridView();
            this.labelSala = new System.Windows.Forms.Label();
            this.buttonAbandonar = new System.Windows.Forms.Button();
            this.richTextBoxChat = new System.Windows.Forms.RichTextBox();
            this.textBoxChat = new System.Windows.Forms.TextBox();
            this.labelChat = new System.Windows.Forms.Label();
            this.buttonPedirCarta = new System.Windows.Forms.Button();
            this.labelUsuario = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConectados)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonEmpezarPartida
            // 
            this.buttonEmpezarPartida.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEmpezarPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEmpezarPartida.Location = new System.Drawing.Point(1230, 725);
            this.buttonEmpezarPartida.Name = "buttonEmpezarPartida";
            this.buttonEmpezarPartida.Size = new System.Drawing.Size(201, 53);
            this.buttonEmpezarPartida.TabIndex = 15;
            this.buttonEmpezarPartida.Text = "Empezar partida";
            this.buttonEmpezarPartida.UseVisualStyleBackColor = true;
            this.buttonEmpezarPartida.Visible = false;
            this.buttonEmpezarPartida.Click += new System.EventHandler(this.EmpezarPartida_Click);
            // 
            // labelPlayer2
            // 
            this.labelPlayer2.AutoSize = true;
            this.labelPlayer2.Location = new System.Drawing.Point(275, 145);
            this.labelPlayer2.Name = "labelPlayer2";
            this.labelPlayer2.Size = new System.Drawing.Size(36, 13);
            this.labelPlayer2.TabIndex = 16;
            this.labelPlayer2.Text = "Player";
            this.labelPlayer2.Visible = false;
            // 
            // labelPlayer4
            // 
            this.labelPlayer4.AutoSize = true;
            this.labelPlayer4.Location = new System.Drawing.Point(275, 97);
            this.labelPlayer4.Name = "labelPlayer4";
            this.labelPlayer4.Size = new System.Drawing.Size(36, 13);
            this.labelPlayer4.TabIndex = 17;
            this.labelPlayer4.Text = "Player";
            this.labelPlayer4.Visible = false;
            // 
            // labelYourCards
            // 
            this.labelYourCards.AutoSize = true;
            this.labelYourCards.Location = new System.Drawing.Point(275, 175);
            this.labelYourCards.Name = "labelYourCards";
            this.labelYourCards.Size = new System.Drawing.Size(59, 13);
            this.labelYourCards.TabIndex = 18;
            this.labelYourCards.Text = "Your Cards";
            this.labelYourCards.Visible = false;
            // 
            // labelPlayer3
            // 
            this.labelPlayer3.AutoSize = true;
            this.labelPlayer3.Location = new System.Drawing.Point(275, 120);
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
            this.labelConectados.Location = new System.Drawing.Point(9, 269);
            this.labelConectados.Name = "labelConectados";
            this.labelConectados.Size = new System.Drawing.Size(183, 18);
            this.labelConectados.TabIndex = 21;
            this.labelConectados.Text = "Jugadores Conectados";
            // 
            // dataGridViewConectados
            // 
            this.dataGridViewConectados.AllowUserToAddRows = false;
            this.dataGridViewConectados.AllowUserToDeleteRows = false;
            this.dataGridViewConectados.AllowUserToResizeRows = false;
            this.dataGridViewConectados.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewConectados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewConectados.Location = new System.Drawing.Point(12, 305);
            this.dataGridViewConectados.Name = "dataGridViewConectados";
            this.dataGridViewConectados.ReadOnly = true;
            this.dataGridViewConectados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewConectados.Size = new System.Drawing.Size(172, 214);
            this.dataGridViewConectados.TabIndex = 22;
            // 
            // labelSala
            // 
            this.labelSala.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSala.Location = new System.Drawing.Point(12, 26);
            this.labelSala.Name = "labelSala";
            this.labelSala.Size = new System.Drawing.Size(120, 35);
            this.labelSala.TabIndex = 24;
            this.labelSala.Text = "Sala: ";
            // 
            // buttonAbandonar
            // 
            this.buttonAbandonar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAbandonar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAbandonar.Location = new System.Drawing.Point(1323, 11);
            this.buttonAbandonar.Name = "buttonAbandonar";
            this.buttonAbandonar.Size = new System.Drawing.Size(108, 50);
            this.buttonAbandonar.TabIndex = 25;
            this.buttonAbandonar.Text = "Abandonar";
            this.buttonAbandonar.UseVisualStyleBackColor = true;
            this.buttonAbandonar.Click += new System.EventHandler(this.buttonAbandonar_Click);
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
            // 
            // textBoxChat
            // 
            this.textBoxChat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxChat.Location = new System.Drawing.Point(1183, 642);
            this.textBoxChat.Name = "textBoxChat";
            this.textBoxChat.Size = new System.Drawing.Size(248, 20);
            this.textBoxChat.TabIndex = 28;
            // 
            // labelChat
            // 
            this.labelChat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelChat.Location = new System.Drawing.Point(1179, 477);
            this.labelChat.Name = "labelChat";
            this.labelChat.Size = new System.Drawing.Size(145, 20);
            this.labelChat.TabIndex = 29;
            this.labelChat.Text = "Chat de Sala";
            // 
            // buttonPedirCarta
            // 
            this.buttonPedirCarta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPedirCarta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPedirCarta.Location = new System.Drawing.Point(1323, 401);
            this.buttonPedirCarta.Name = "buttonPedirCarta";
            this.buttonPedirCarta.Size = new System.Drawing.Size(108, 35);
            this.buttonPedirCarta.TabIndex = 30;
            this.buttonPedirCarta.Text = "Pedir Carta";
            this.buttonPedirCarta.UseVisualStyleBackColor = true;
            this.buttonPedirCarta.Visible = false;
            this.buttonPedirCarta.Click += new System.EventHandler(this.buttonPedirCarta_Click);
            // 
            // labelUsuario
            // 
            this.labelUsuario.AutoSize = true;
            this.labelUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsuario.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelUsuario.Location = new System.Drawing.Point(12, 61);
            this.labelUsuario.Name = "labelUsuario";
            this.labelUsuario.Size = new System.Drawing.Size(81, 20);
            this.labelUsuario.TabIndex = 34;
            this.labelUsuario.Text = "Usuario: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 790);
            this.Controls.Add(this.labelUsuario);
            this.Controls.Add(this.buttonPedirCarta);
            this.Controls.Add(this.labelChat);
            this.Controls.Add(this.textBoxChat);
            this.Controls.Add(this.richTextBoxChat);
            this.Controls.Add(this.buttonAbandonar);
            this.Controls.Add(this.labelSala);
            this.Controls.Add(this.dataGridViewConectados);
            this.Controls.Add(this.labelConectados);
            this.Controls.Add(this.labelPlayer3);
            this.Controls.Add(this.labelYourCards);
            this.Controls.Add(this.labelPlayer4);
            this.Controls.Add(this.labelPlayer2);
            this.Controls.Add(this.buttonEmpezarPartida);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UNO";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConectados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonEmpezarPartida;
        private System.Windows.Forms.Label labelPlayer2;
        private System.Windows.Forms.Label labelPlayer4;
        private System.Windows.Forms.Label labelYourCards;
        private System.Windows.Forms.Label labelPlayer3;
        private System.Windows.Forms.Label labelConectados;
        private System.Windows.Forms.DataGridView dataGridViewConectados;
        private System.Windows.Forms.Label labelSala;
        private System.Windows.Forms.Button buttonAbandonar;
        private System.Windows.Forms.RichTextBox richTextBoxChat;
        private System.Windows.Forms.TextBox textBoxChat;
        private System.Windows.Forms.Label labelChat;
        private System.Windows.Forms.Button buttonPedirCarta;
        private System.Windows.Forms.Label labelUsuario;
    }
}

