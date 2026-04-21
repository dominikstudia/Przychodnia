namespace Przychodnia.formsy
{
    partial class ZmianaHasla
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
            lbl_stare_haslo = new Label();
            textbox_stare_haslo = new TextBox();
            btn_anuluj = new Button();
            btn_potwierdz = new Button();
            lbl_nowe_haslo = new Label();
            textbox_nowe_haslo = new TextBox();
            lbl_powtorzone_nowe_haslo = new Label();
            textbox_powtorzone_nowe_haslo = new TextBox();
            lbl_zmiana_hasla = new Label();
            SuspendLayout();
            // 
            // lbl_stare_haslo
            // 
            lbl_stare_haslo.AutoSize = true;
            lbl_stare_haslo.Font = new Font("Segoe UI", 13F);
            lbl_stare_haslo.Location = new Point(198, 90);
            lbl_stare_haslo.Name = "lbl_stare_haslo";
            lbl_stare_haslo.Size = new Size(99, 25);
            lbl_stare_haslo.TabIndex = 0;
            lbl_stare_haslo.Text = "Stare hasło";
            // 
            // textbox_stare_haslo
            // 
            textbox_stare_haslo.Font = new Font("Segoe UI", 12F);
            textbox_stare_haslo.Location = new Point(198, 118);
            textbox_stare_haslo.Name = "textbox_stare_haslo";
            textbox_stare_haslo.Size = new Size(380, 29);
            textbox_stare_haslo.TabIndex = 1;
            textbox_stare_haslo.UseSystemPasswordChar = true;
            // 
            // btn_anuluj
            // 
            btn_anuluj.Font = new Font("Segoe UI", 12F);
            btn_anuluj.Location = new Point(186, 350);
            btn_anuluj.Name = "btn_anuluj";
            btn_anuluj.Size = new Size(161, 51);
            btn_anuluj.TabIndex = 2;
            btn_anuluj.Text = "Anuluj";
            btn_anuluj.UseVisualStyleBackColor = true;
            btn_anuluj.Click += btn_anuluj_Click;
            // 
            // btn_potwierdz
            // 
            btn_potwierdz.Font = new Font("Segoe UI", 12F);
            btn_potwierdz.Location = new Point(372, 350);
            btn_potwierdz.Name = "btn_potwierdz";
            btn_potwierdz.Size = new Size(206, 51);
            btn_potwierdz.TabIndex = 3;
            btn_potwierdz.Text = "Potwierdź";
            btn_potwierdz.UseVisualStyleBackColor = true;
            btn_potwierdz.Click += btn_potwierdz_Click;
            // 
            // lbl_nowe_haslo
            // 
            lbl_nowe_haslo.AutoSize = true;
            lbl_nowe_haslo.Font = new Font("Segoe UI", 13F);
            lbl_nowe_haslo.Location = new Point(198, 177);
            lbl_nowe_haslo.Name = "lbl_nowe_haslo";
            lbl_nowe_haslo.Size = new Size(105, 25);
            lbl_nowe_haslo.TabIndex = 4;
            lbl_nowe_haslo.Text = "Nowe haslo";
            // 
            // textbox_nowe_haslo
            // 
            textbox_nowe_haslo.Font = new Font("Segoe UI", 12F);
            textbox_nowe_haslo.Location = new Point(198, 205);
            textbox_nowe_haslo.Name = "textbox_nowe_haslo";
            textbox_nowe_haslo.Size = new Size(380, 29);
            textbox_nowe_haslo.TabIndex = 5;
            textbox_nowe_haslo.UseSystemPasswordChar = true;
            // 
            // lbl_powtorzone_nowe_haslo
            // 
            lbl_powtorzone_nowe_haslo.AutoSize = true;
            lbl_powtorzone_nowe_haslo.Font = new Font("Segoe UI", 13F);
            lbl_powtorzone_nowe_haslo.Location = new Point(198, 237);
            lbl_powtorzone_nowe_haslo.Name = "lbl_powtorzone_nowe_haslo";
            lbl_powtorzone_nowe_haslo.Size = new Size(202, 25);
            lbl_powtorzone_nowe_haslo.TabIndex = 6;
            lbl_powtorzone_nowe_haslo.Text = "Powtórzone nowe hasło";
            // 
            // textbox_powtorzone_nowe_haslo
            // 
            textbox_powtorzone_nowe_haslo.Font = new Font("Segoe UI", 12F);
            textbox_powtorzone_nowe_haslo.Location = new Point(198, 265);
            textbox_powtorzone_nowe_haslo.Name = "textbox_powtorzone_nowe_haslo";
            textbox_powtorzone_nowe_haslo.Size = new Size(380, 29);
            textbox_powtorzone_nowe_haslo.TabIndex = 7;
            textbox_powtorzone_nowe_haslo.UseSystemPasswordChar = true;
            // 
            // lbl_zmiana_hasla
            // 
            lbl_zmiana_hasla.AutoSize = true;
            lbl_zmiana_hasla.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
            lbl_zmiana_hasla.Location = new Point(249, 9);
            lbl_zmiana_hasla.Name = "lbl_zmiana_hasla";
            lbl_zmiana_hasla.Size = new Size(290, 47);
            lbl_zmiana_hasla.TabIndex = 8;
            lbl_zmiana_hasla.Text = "ZMIANA HASŁA";
            // 
            // ZmianaHasla
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lbl_zmiana_hasla);
            Controls.Add(textbox_powtorzone_nowe_haslo);
            Controls.Add(lbl_powtorzone_nowe_haslo);
            Controls.Add(textbox_nowe_haslo);
            Controls.Add(lbl_nowe_haslo);
            Controls.Add(btn_potwierdz);
            Controls.Add(btn_anuluj);
            Controls.Add(textbox_stare_haslo);
            Controls.Add(lbl_stare_haslo);
            Name = "ZmianaHasla";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ZmianaHasla";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbl_stare_haslo;
        private TextBox textbox_stare_haslo;
        private Button btn_anuluj;
        private Button btn_potwierdz;
        private Label lbl_nowe_haslo;
        private TextBox textbox_nowe_haslo;
        private Label lbl_powtorzone_nowe_haslo;
        private TextBox textbox_powtorzone_nowe_haslo;
        private Label lbl_zmiana_hasla;
    }
}