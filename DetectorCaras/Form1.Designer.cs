namespace DetectorCaras
{
    partial class Form1
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
            this.btnRecargar = new System.Windows.Forms.Button();
            this.tbLimite = new System.Windows.Forms.TextBox();
            this.txtMatrizOutput = new System.Windows.Forms.RichTextBox();
            this.btnCargarImagen = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.btnReconocer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRecargar
            // 
            this.btnRecargar.Location = new System.Drawing.Point(28, 176);
            this.btnRecargar.Margin = new System.Windows.Forms.Padding(2);
            this.btnRecargar.Name = "btnRecargar";
            this.btnRecargar.Size = new System.Drawing.Size(56, 56);
            this.btnRecargar.TabIndex = 11;
            this.btnRecargar.Text = "Cambiar Limite de Brillo";
            this.btnRecargar.UseVisualStyleBackColor = true;
            this.btnRecargar.Click += new System.EventHandler(this.btnRecargar_Click_1);
            // 
            // tbLimite
            // 
            this.tbLimite.Location = new System.Drawing.Point(19, 118);
            this.tbLimite.Margin = new System.Windows.Forms.Padding(2);
            this.tbLimite.Name = "tbLimite";
            this.tbLimite.Size = new System.Drawing.Size(76, 20);
            this.tbLimite.TabIndex = 10;
            // 
            // txtMatrizOutput
            // 
            this.txtMatrizOutput.Location = new System.Drawing.Point(134, 38);
            this.txtMatrizOutput.Margin = new System.Windows.Forms.Padding(2);
            this.txtMatrizOutput.Name = "txtMatrizOutput";
            this.txtMatrizOutput.Size = new System.Drawing.Size(912, 500);
            this.txtMatrizOutput.TabIndex = 9;
            this.txtMatrizOutput.Text = "";
            // 
            // btnCargarImagen
            // 
            this.btnCargarImagen.Location = new System.Drawing.Point(28, 38);
            this.btnCargarImagen.Margin = new System.Windows.Forms.Padding(2);
            this.btnCargarImagen.Name = "btnCargarImagen";
            this.btnCargarImagen.Size = new System.Drawing.Size(56, 43);
            this.btnCargarImagen.TabIndex = 8;
            this.btnCargarImagen.Text = "Cargar Imagen";
            this.btnCargarImagen.UseVisualStyleBackColor = true;
            this.btnCargarImagen.Click += new System.EventHandler(this.btnCargarImagen_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(28, 276);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(67, 56);
            this.btnGuardar.TabIndex = 12;
            this.btnGuardar.Text = "Guardar Escaneo";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(19, 361);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(2);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(76, 20);
            this.txtNombre.TabIndex = 13;
            // 
            // btnReconocer
            // 
            this.btnReconocer.Location = new System.Drawing.Point(28, 421);
            this.btnReconocer.Margin = new System.Windows.Forms.Padding(2);
            this.btnReconocer.Name = "btnReconocer";
            this.btnReconocer.Size = new System.Drawing.Size(67, 56);
            this.btnReconocer.TabIndex = 14;
            this.btnReconocer.Text = "Reconocer Rostro";
            this.btnReconocer.UseVisualStyleBackColor = true;
            this.btnReconocer.Click += new System.EventHandler(this.btnReconocer_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 573);
            this.Controls.Add(this.btnReconocer);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnRecargar);
            this.Controls.Add(this.tbLimite);
            this.Controls.Add(this.txtMatrizOutput);
            this.Controls.Add(this.btnCargarImagen);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRecargar;
        private System.Windows.Forms.TextBox tbLimite;
        private System.Windows.Forms.RichTextBox txtMatrizOutput;
        private System.Windows.Forms.Button btnCargarImagen;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Button btnReconocer;
    }
}