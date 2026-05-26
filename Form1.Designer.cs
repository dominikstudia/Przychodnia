namespace Przychodnia
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel_boczny = new Panel();
            przycisk_lista_wizyt = new Button();
            przycisk_dodaj_pacjenta = new Button();
            btn_zmien_haslo = new Button();
            btn_wyloguj = new Button();
            przycisk_przeglad_uprawnien = new Button();
            przycisk_wyszukaj_uzytkownika = new Button();
            przycisk_dodaj_uzytkownika = new Button();
            panel_informacyjny = new Panel();
            label_ogolny = new Label();
            panel_edycji = new Panel();
            panel_boczny.SuspendLayout();
            panel_informacyjny.SuspendLayout();
            SuspendLayout();
            // 
            // panel_boczny
            // 
            panel_boczny.BackColor = SystemColors.ControlDark;
            panel_boczny.Controls.Add(przycisk_lista_wizyt);
            panel_boczny.Controls.Add(przycisk_dodaj_pacjenta);
            panel_boczny.Controls.Add(btn_zmien_haslo);
            panel_boczny.Controls.Add(btn_wyloguj);
            panel_boczny.Controls.Add(przycisk_przeglad_uprawnien);
            panel_boczny.Controls.Add(przycisk_wyszukaj_uzytkownika);
            panel_boczny.Controls.Add(przycisk_dodaj_uzytkownika);
            panel_boczny.Dock = DockStyle.Left;
            panel_boczny.Location = new Point(0, 100);
            panel_boczny.Name = "panel_boczny";
            panel_boczny.Size = new Size(200, 581);
            panel_boczny.TabIndex = 0;
            // 
            // przycisk_lista_wizyt
            // 
            przycisk_lista_wizyt.Dock = DockStyle.Top;
            przycisk_lista_wizyt.Location = new Point(0, 210);
            przycisk_lista_wizyt.Name = "przycisk_lista_wizyt";
            przycisk_lista_wizyt.Size = new Size(200, 70);
            przycisk_lista_wizyt.TabIndex = 6;
            przycisk_lista_wizyt.Text = "Lista Wizyt";
            przycisk_lista_wizyt.UseVisualStyleBackColor = true;
            przycisk_lista_wizyt.Visible = false;
            przycisk_lista_wizyt.Click += btn_dodaj_wizyte_Click;
            // 
            // przycisk_dodaj_pacjenta
            // 
            przycisk_dodaj_pacjenta.Location = new Point(0, 286);
            przycisk_dodaj_pacjenta.Name = "przycisk_dodaj_pacjenta";
            przycisk_dodaj_pacjenta.Size = new Size(200, 70);
            przycisk_dodaj_pacjenta.TabIndex = 5;
            przycisk_dodaj_pacjenta.Text = "Zarejestruj pacjenta";
            przycisk_dodaj_pacjenta.UseVisualStyleBackColor = true;
            przycisk_dodaj_pacjenta.Click += przycisk_dodaj_pacjenta_Click;
            // 
            // btn_zmien_haslo
            // 
            btn_zmien_haslo.Dock = DockStyle.Bottom;
            btn_zmien_haslo.Location = new Point(0, 441);
            btn_zmien_haslo.Name = "btn_zmien_haslo";
            btn_zmien_haslo.Size = new Size(200, 70);
            btn_zmien_haslo.TabIndex = 4;
            btn_zmien_haslo.Text = "Zmień hasło";
            btn_zmien_haslo.UseVisualStyleBackColor = true;
            btn_zmien_haslo.Click += btn_zmien_haslo_Click;
            // 
            // btn_wyloguj
            // 
            btn_wyloguj.Dock = DockStyle.Bottom;
            btn_wyloguj.Location = new Point(0, 511);
            btn_wyloguj.Name = "btn_wyloguj";
            btn_wyloguj.Size = new Size(200, 70);
            btn_wyloguj.TabIndex = 3;
            btn_wyloguj.Text = "Wyloguj";
            btn_wyloguj.UseVisualStyleBackColor = true;
            btn_wyloguj.Click += btn_wyloguj_Click;
            // 
            // przycisk_przeglad_uprawnien
            // 
            przycisk_przeglad_uprawnien.Dock = DockStyle.Top;
            przycisk_przeglad_uprawnien.Location = new Point(0, 140);
            przycisk_przeglad_uprawnien.Name = "przycisk_przeglad_uprawnien";
            przycisk_przeglad_uprawnien.Size = new Size(200, 70);
            przycisk_przeglad_uprawnien.TabIndex = 2;
            przycisk_przeglad_uprawnien.Text = "Lista Uprawnien";
            przycisk_przeglad_uprawnien.UseVisualStyleBackColor = true;
            przycisk_przeglad_uprawnien.Visible = false;
            przycisk_przeglad_uprawnien.Click += przycisk_przeglad_uprawnien_Click;
            // 
            // przycisk_wyszukaj_uzytkownika
            // 
            przycisk_wyszukaj_uzytkownika.Dock = DockStyle.Top;
            przycisk_wyszukaj_uzytkownika.Location = new Point(0, 70);
            przycisk_wyszukaj_uzytkownika.Name = "przycisk_wyszukaj_uzytkownika";
            przycisk_wyszukaj_uzytkownika.Size = new Size(200, 70);
            przycisk_wyszukaj_uzytkownika.TabIndex = 1;
            przycisk_wyszukaj_uzytkownika.Text = "Wyszukaj użytkownika";
            przycisk_wyszukaj_uzytkownika.UseVisualStyleBackColor = true;
            przycisk_wyszukaj_uzytkownika.Visible = false;
            przycisk_wyszukaj_uzytkownika.Click += przycisk_wyszukaj_uzytkownika_Click;
            // 
            // przycisk_dodaj_uzytkownika
            // 
            przycisk_dodaj_uzytkownika.Dock = DockStyle.Top;
            przycisk_dodaj_uzytkownika.Location = new Point(0, 0);
            przycisk_dodaj_uzytkownika.Name = "przycisk_dodaj_uzytkownika";
            przycisk_dodaj_uzytkownika.Size = new Size(200, 70);
            przycisk_dodaj_uzytkownika.TabIndex = 0;
            przycisk_dodaj_uzytkownika.Text = "Dodaj użytkownika";
            przycisk_dodaj_uzytkownika.UseVisualStyleBackColor = true;
            przycisk_dodaj_uzytkownika.Visible = false;
            przycisk_dodaj_uzytkownika.Click += przycisk_dodaj_uzytkownika_Click;
            // 
            // panel_informacyjny
            // 
            panel_informacyjny.BackColor = SystemColors.ControlDark;
            panel_informacyjny.Controls.Add(label_ogolny);
            panel_informacyjny.Dock = DockStyle.Top;
            panel_informacyjny.Location = new Point(0, 0);
            panel_informacyjny.Name = "panel_informacyjny";
            panel_informacyjny.Size = new Size(1064, 100);
            panel_informacyjny.TabIndex = 1;
            // 
            // label_ogolny
            // 
            label_ogolny.Dock = DockStyle.Fill;
            label_ogolny.Font = new Font("Arial", 32.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_ogolny.ImageAlign = ContentAlignment.TopCenter;
            label_ogolny.Location = new Point(0, 0);
            label_ogolny.Name = "label_ogolny";
            label_ogolny.Size = new Size(1064, 100);
            label_ogolny.TabIndex = 0;
            label_ogolny.Text = "PRZYCHODNIA";
            label_ogolny.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel_edycji
            // 
            panel_edycji.Dock = DockStyle.Fill;
            panel_edycji.Location = new Point(200, 100);
            panel_edycji.Name = "panel_edycji";
            panel_edycji.Size = new Size(864, 581);
            panel_edycji.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1064, 681);
            Controls.Add(panel_edycji);
            Controls.Add(panel_boczny);
            Controls.Add(panel_informacyjny);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Testowanie oprogramowania";
            panel_boczny.ResumeLayout(false);
            panel_informacyjny.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel_boczny;
        private Button przycisk_wyszukaj_uzytkownika;
        private Button przycisk_dodaj_uzytkownika;
        private Panel panel_informacyjny;
        private Label label_ogolny;
        private Panel panel_edycji;
        private Button przycisk_przeglad_uprawnien;
        private Button btn_wyloguj;
        private Button btn_zmien_haslo;
        private Button przycisk_dodaj_pacjenta;
        private Button przycisk_lista_wizyt;
    }
}
