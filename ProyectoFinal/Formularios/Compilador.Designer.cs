namespace ProyectoFinal.Formularios
{
    partial class Compilador
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
            this.btnGuardar = new System.Windows.Forms.Button();
            this.ListEntrada = new System.Windows.Forms.ListBox();
            this.ListSalida = new System.Windows.Forms.ListBox();
            this.ListPreservadas = new System.Windows.Forms.ListBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnCompilar = new System.Windows.Forms.Button();
            this.btnAbrirarchivo = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnCargar = new System.Windows.Forms.Button();
            this.btnRegistros = new System.Windows.Forms.Button();
            this.btnsaveexcel = new System.Windows.Forms.Button();
            this.btnsavecsv = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Gainsboro;
            this.btnGuardar.Font = new System.Drawing.Font("Arial Narrow", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(1026, 420);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(88, 28);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar txt";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // ListEntrada
            // 
            this.ListEntrada.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ListEntrada.FormattingEnabled = true;
            this.ListEntrada.ItemHeight = 16;
            this.ListEntrada.Location = new System.Drawing.Point(296, 57);
            this.ListEntrada.Margin = new System.Windows.Forms.Padding(4);
            this.ListEntrada.Name = "ListEntrada";
            this.ListEntrada.Size = new System.Drawing.Size(294, 452);
            this.ListEntrada.TabIndex = 1;
            // 
            // ListSalida
            // 
            this.ListSalida.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ListSalida.FormattingEnabled = true;
            this.ListSalida.ItemHeight = 16;
            this.ListSalida.Location = new System.Drawing.Point(726, 57);
            this.ListSalida.Margin = new System.Windows.Forms.Padding(4);
            this.ListSalida.Name = "ListSalida";
            this.ListSalida.Size = new System.Drawing.Size(294, 452);
            this.ListSalida.TabIndex = 2;
            // 
            // ListPreservadas
            // 
            this.ListPreservadas.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ListPreservadas.FormattingEnabled = true;
            this.ListPreservadas.ItemHeight = 16;
            this.ListPreservadas.Location = new System.Drawing.Point(11, 233);
            this.ListPreservadas.Margin = new System.Windows.Forms.Padding(4);
            this.ListPreservadas.Name = "ListPreservadas";
            this.ListPreservadas.Size = new System.Drawing.Size(256, 276);
            this.ListPreservadas.TabIndex = 3;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(18, 57);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(164, 24);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnCompilar
            // 
            this.btnCompilar.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCompilar.Font = new System.Drawing.Font("Arial Narrow", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompilar.Location = new System.Drawing.Point(597, 476);
            this.btnCompilar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCompilar.Name = "btnCompilar";
            this.btnCompilar.Size = new System.Drawing.Size(88, 28);
            this.btnCompilar.TabIndex = 5;
            this.btnCompilar.Text = "Compilar";
            this.btnCompilar.UseVisualStyleBackColor = false;
            this.btnCompilar.Click += new System.EventHandler(this.btnCompilar_Click);
            // 
            // btnAbrirarchivo
            // 
            this.btnAbrirarchivo.BackColor = System.Drawing.Color.Gainsboro;
            this.btnAbrirarchivo.Font = new System.Drawing.Font("Arial Narrow", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbrirarchivo.Location = new System.Drawing.Point(597, 420);
            this.btnAbrirarchivo.Margin = new System.Windows.Forms.Padding(4);
            this.btnAbrirarchivo.Name = "btnAbrirarchivo";
            this.btnAbrirarchivo.Size = new System.Drawing.Size(88, 28);
            this.btnAbrirarchivo.TabIndex = 6;
            this.btnAbrirarchivo.Text = "Abrir archivo";
            this.btnAbrirarchivo.UseVisualStyleBackColor = false;
            this.btnAbrirarchivo.Click += new System.EventHandler(this.btnAbrirarchivo_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCerrar.Font = new System.Drawing.Font("Arial Narrow", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(1026, 481);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(88, 28);
            this.btnCerrar.TabIndex = 7;
            this.btnCerrar.Text = "Cerrar Sesión";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnCargar
            // 
            this.btnCargar.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCargar.Font = new System.Drawing.Font("Arial Narrow", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargar.Location = new System.Drawing.Point(18, 90);
            this.btnCargar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(88, 28);
            this.btnCargar.TabIndex = 8;
            this.btnCargar.Text = "Seleccionar";
            this.btnCargar.UseVisualStyleBackColor = false;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // btnRegistros
            // 
            this.btnRegistros.BackColor = System.Drawing.Color.Gainsboro;
            this.btnRegistros.Font = new System.Drawing.Font("Arial Narrow", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistros.Location = new System.Drawing.Point(1026, 57);
            this.btnRegistros.Margin = new System.Windows.Forms.Padding(4);
            this.btnRegistros.Name = "btnRegistros";
            this.btnRegistros.Size = new System.Drawing.Size(88, 28);
            this.btnRegistros.TabIndex = 9;
            this.btnRegistros.Text = "Reportes";
            this.btnRegistros.UseVisualStyleBackColor = false;
            this.btnRegistros.Click += new System.EventHandler(this.btnRegistros_Click);
            // 
            // btnsaveexcel
            // 
            this.btnsaveexcel.BackColor = System.Drawing.Color.Gainsboro;
            this.btnsaveexcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnsaveexcel.Location = new System.Drawing.Point(747, 273);
            this.btnsaveexcel.Margin = new System.Windows.Forms.Padding(4);
            this.btnsaveexcel.Name = "btnsaveexcel";
            this.btnsaveexcel.Size = new System.Drawing.Size(101, 28);
            this.btnsaveexcel.TabIndex = 10;
            this.btnsaveexcel.Text = "Guardar Excel";
            this.btnsaveexcel.UseVisualStyleBackColor = false;
            this.btnsaveexcel.Click += new System.EventHandler(this.btnsaveexcel_Click);
            // 
            // btnsavecsv
            // 
            this.btnsavecsv.BackColor = System.Drawing.Color.Gainsboro;
            this.btnsavecsv.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnsavecsv.Location = new System.Drawing.Point(856, 273);
            this.btnsavecsv.Margin = new System.Windows.Forms.Padding(4);
            this.btnsavecsv.Name = "btnsavecsv";
            this.btnsavecsv.Size = new System.Drawing.Size(88, 28);
            this.btnsavecsv.TabIndex = 11;
            this.btnsavecsv.Text = "Guardar csv";
            this.btnsavecsv.UseVisualStyleBackColor = false;
            this.btnsavecsv.Click += new System.EventHandler(this.btnsavecsv_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(296, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 31);
            this.label1.TabIndex = 12;
            this.label1.Text = "Archivo de Entrada";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(727, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(202, 31);
            this.label2.TabIndex = 13;
            this.label2.Text = "Archivo de Salida";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Palatino Linotype", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 31);
            this.label3.TabIndex = 14;
            this.label3.Text = "Elegir lenguaje";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Palatino Linotype", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 180);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(228, 31);
            this.label4.TabIndex = 15;
            this.label4.Text = "Palabras Reservadas";
            // 
            // Compilador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1138, 544);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRegistros);
            this.Controls.Add(this.btnCargar);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnAbrirarchivo);
            this.Controls.Add(this.btnCompilar);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.ListPreservadas);
            this.Controls.Add(this.ListSalida);
            this.Controls.Add(this.ListEntrada);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnsavecsv);
            this.Controls.Add(this.btnsaveexcel);
            this.Font = new System.Drawing.Font("Arial Narrow", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Compilador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormPrincipal";
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ListBox ListEntrada;
        private System.Windows.Forms.ListBox ListSalida;
        private System.Windows.Forms.ListBox ListPreservadas;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnCompilar;
        private System.Windows.Forms.Button btnAbrirarchivo;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Button btnRegistros;
        private System.Windows.Forms.Button btnsaveexcel;
        private System.Windows.Forms.Button btnsavecsv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}