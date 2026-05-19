namespace Przychodnia
{
    partial class ListaUzytkownikowPanel
    {
        /// <summary> 
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod wygenerowany przez Projektanta składników

        /// <summary> 
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować 
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            checkedlistbox_filtr_roli = new CheckedListBox();
            checkbox_uwzglednienie_zarchwizowanych = new CheckBox();
            btn_szukaj = new Button();
            textbox_wyszukiwanie = new TextBox();
            panel2 = new Panel();
            przycisk_zarejestruj_wizyte = new Button();
            btn_edytuj = new Button();
            btn_podglad_szczegolow = new Button();
            btn_archiwizuj = new Button();
            btn_dodaj = new Button();
            datagrid_uzytkownicy = new DataGridView();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)datagrid_uzytkownicy).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(checkedlistbox_filtr_roli);
            panel1.Controls.Add(checkbox_uwzglednienie_zarchwizowanych);
            panel1.Controls.Add(btn_szukaj);
            panel1.Controls.Add(textbox_wyszukiwanie);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(941, 96);
            panel1.TabIndex = 0;
            // 
            // checkedlistbox_filtr_roli
            // 
            checkedlistbox_filtr_roli.FormattingEnabled = true;
            checkedlistbox_filtr_roli.Location = new Point(395, 13);
            checkedlistbox_filtr_roli.Name = "checkedlistbox_filtr_roli";
            checkedlistbox_filtr_roli.Size = new Size(120, 76);
            checkedlistbox_filtr_roli.TabIndex = 5;
            // 
            // checkbox_uwzglednienie_zarchwizowanych
            // 
            checkbox_uwzglednienie_zarchwizowanych.AutoSize = true;
            checkbox_uwzglednienie_zarchwizowanych.Font = new Font("Segoe UI", 9F);
            checkbox_uwzglednienie_zarchwizowanych.Location = new Point(20, 48);
            checkbox_uwzglednienie_zarchwizowanych.Name = "checkbox_uwzglednienie_zarchwizowanych";
            checkbox_uwzglednienie_zarchwizowanych.Size = new Size(268, 19);
            checkbox_uwzglednienie_zarchwizowanych.TabIndex = 2;
            checkbox_uwzglednienie_zarchwizowanych.Text = "Uwzględnij zaarchiwizowanych użytkowników";
            checkbox_uwzglednienie_zarchwizowanych.UseVisualStyleBackColor = true;
            // 
            // btn_szukaj
            // 
            btn_szukaj.Font = new Font("Segoe UI", 12F);
            btn_szukaj.Location = new Point(537, 13);
            btn_szukaj.Name = "btn_szukaj";
            btn_szukaj.Size = new Size(200, 76);
            btn_szukaj.TabIndex = 1;
            btn_szukaj.Text = "Szukaj";
            btn_szukaj.UseVisualStyleBackColor = true;
            btn_szukaj.Click += btn_szukaj_Click;
            // 
            // textbox_wyszukiwanie
            // 
            textbox_wyszukiwanie.Font = new Font("Segoe UI", 12F);
            textbox_wyszukiwanie.Location = new Point(20, 13);
            textbox_wyszukiwanie.Name = "textbox_wyszukiwanie";
            textbox_wyszukiwanie.Size = new Size(365, 29);
            textbox_wyszukiwanie.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(przycisk_zarejestruj_wizyte);
            panel2.Controls.Add(btn_edytuj);
            panel2.Controls.Add(btn_podglad_szczegolow);
            panel2.Controls.Add(btn_archiwizuj);
            panel2.Controls.Add(btn_dodaj);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 408);
            panel2.Name = "panel2";
            panel2.Size = new Size(941, 81);
            panel2.TabIndex = 1;
            // 
            // przycisk_zarejestruj_wizyte
            // 
            przycisk_zarejestruj_wizyte.Location = new Point(743, 14);
            przycisk_zarejestruj_wizyte.Name = "przycisk_zarejestruj_wizyte";
            przycisk_zarejestruj_wizyte.Size = new Size(168, 54);
            przycisk_zarejestruj_wizyte.TabIndex = 4;
            przycisk_zarejestruj_wizyte.Text = "Zarejestruj Wizyte";
            przycisk_zarejestruj_wizyte.UseVisualStyleBackColor = true;
            przycisk_zarejestruj_wizyte.Click += przycisk_zarejestruj_wizyte_Click;
            // 
            // btn_edytuj
            // 
            btn_edytuj.Location = new Point(396, 14);
            btn_edytuj.Name = "btn_edytuj";
            btn_edytuj.Size = new Size(168, 54);
            btn_edytuj.TabIndex = 3;
            btn_edytuj.Text = "Edytuj użytkownika";
            btn_edytuj.UseVisualStyleBackColor = true;
            btn_edytuj.Visible = false;
            btn_edytuj.Click += btn_edytuj_Click;
            // 
            // btn_podglad_szczegolow
            // 
            btn_podglad_szczegolow.Location = new Point(569, 14);
            btn_podglad_szczegolow.Name = "btn_podglad_szczegolow";
            btn_podglad_szczegolow.Size = new Size(168, 54);
            btn_podglad_szczegolow.TabIndex = 2;
            btn_podglad_szczegolow.Text = "Podgląd szczegółów";
            btn_podglad_szczegolow.UseVisualStyleBackColor = true;
            btn_podglad_szczegolow.Click += btn_podglad_szczegolow_Click;
            // 
            // btn_archiwizuj
            // 
            btn_archiwizuj.Location = new Point(221, 14);
            btn_archiwizuj.Name = "btn_archiwizuj";
            btn_archiwizuj.Size = new Size(168, 54);
            btn_archiwizuj.TabIndex = 1;
            btn_archiwizuj.Text = "Archiwzuj użytkownika";
            btn_archiwizuj.UseVisualStyleBackColor = true;
            btn_archiwizuj.Visible = false;
            btn_archiwizuj.Click += btn_archiwizuj_Click;
            // 
            // btn_dodaj
            // 
            btn_dodaj.Font = new Font("Segoe UI", 9F);
            btn_dodaj.Location = new Point(20, 14);
            btn_dodaj.Name = "btn_dodaj";
            btn_dodaj.Size = new Size(141, 54);
            btn_dodaj.TabIndex = 0;
            btn_dodaj.Text = "Dodaj użytkownika";
            btn_dodaj.UseVisualStyleBackColor = true;
            btn_dodaj.Visible = false;
            btn_dodaj.Click += btn_dodaj_Click;
            // 
            // datagrid_uzytkownicy
            // 
            datagrid_uzytkownicy.AllowUserToAddRows = false;
            datagrid_uzytkownicy.AllowUserToDeleteRows = false;
            datagrid_uzytkownicy.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datagrid_uzytkownicy.BackgroundColor = SystemColors.Control;
            datagrid_uzytkownicy.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            datagrid_uzytkownicy.Dock = DockStyle.Fill;
            datagrid_uzytkownicy.Location = new Point(0, 96);
            datagrid_uzytkownicy.MultiSelect = false;
            datagrid_uzytkownicy.Name = "datagrid_uzytkownicy";
            datagrid_uzytkownicy.ReadOnly = true;
            datagrid_uzytkownicy.RowHeadersVisible = false;
            datagrid_uzytkownicy.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            datagrid_uzytkownicy.Size = new Size(941, 312);
            datagrid_uzytkownicy.TabIndex = 2;
            // 
            // ListaUzytkownikowPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(datagrid_uzytkownicy);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "ListaUzytkownikowPanel";
            Size = new Size(941, 489);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)datagrid_uzytkownicy).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private CheckBox checkbox_uwzglednienie_zarchwizowanych;
        private Button btn_szukaj;
        private TextBox textbox_wyszukiwanie;
        private Panel panel2;
        private Button btn_edytuj;
        private Button btn_podglad_szczegolow;
        private Button btn_archiwizuj;
        private Button btn_dodaj;
        private DataGridView datagrid_uzytkownicy;
        private CheckedListBox checkedlistbox_filtr_roli;
        private Button przycisk_zarejestruj_wizyte;
    }
}
