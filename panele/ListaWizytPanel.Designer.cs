namespace Przychodnia.panele
{
    partial class ListaWizytPanel
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
            txt_szukanaFraza = new TextBox();
            btn_szukaj = new Button();
            dtp_dataOd = new DateTimePicker();
            label1 = new Label();
            dtp_dataDo = new DateTimePicker();
            label_imie_nazwisko_pesel = new Label();
            label_zakres_dat = new Label();
            label_lekarz = new Label();
            label_specjalizacja = new Label();
            cmb_lekarz = new ComboBox();
            cmb_specjalizacja = new ComboBox();
            panel1 = new Panel();
            lbl_komunikat = new Label();
            datagridview_wizyty = new DataGridView();
            btn_szczegoly = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)datagridview_wizyty).BeginInit();
            SuspendLayout();
            // 
            // txt_szukanaFraza
            // 
            txt_szukanaFraza.Font = new Font("Segoe UI", 12F);
            txt_szukanaFraza.Location = new Point(12, 38);
            txt_szukanaFraza.Name = "txt_szukanaFraza";
            txt_szukanaFraza.Size = new Size(425, 29);
            txt_szukanaFraza.TabIndex = 2;
            // 
            // btn_szukaj
            // 
            btn_szukaj.Font = new Font("Segoe UI", 12F);
            btn_szukaj.Location = new Point(650, 96);
            btn_szukaj.Name = "btn_szukaj";
            btn_szukaj.Size = new Size(199, 37);
            btn_szukaj.TabIndex = 3;
            btn_szukaj.Text = "Szukaj";
            btn_szukaj.UseVisualStyleBackColor = true;
            btn_szukaj.Click += btn_szukaj_Click;
            // 
            // dtp_dataOd
            // 
            dtp_dataOd.Location = new Point(12, 101);
            dtp_dataOd.Name = "dtp_dataOd";
            dtp_dataOd.Size = new Size(200, 23);
            dtp_dataOd.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F);
            label1.Location = new Point(218, 96);
            label1.Name = "label1";
            label1.Size = new Size(22, 30);
            label1.TabIndex = 5;
            label1.Text = "-";
            // 
            // dtp_dataDo
            // 
            dtp_dataDo.Location = new Point(237, 101);
            dtp_dataDo.Name = "dtp_dataDo";
            dtp_dataDo.Size = new Size(200, 23);
            dtp_dataDo.TabIndex = 6;
            // 
            // label_imie_nazwisko_pesel
            // 
            label_imie_nazwisko_pesel.AutoSize = true;
            label_imie_nazwisko_pesel.Font = new Font("Segoe UI", 12F);
            label_imie_nazwisko_pesel.Location = new Point(12, 14);
            label_imie_nazwisko_pesel.Name = "label_imie_nazwisko_pesel";
            label_imie_nazwisko_pesel.Size = new Size(182, 21);
            label_imie_nazwisko_pesel.TabIndex = 8;
            label_imie_nazwisko_pesel.Text = "Imię i nazwisko lub pesel";
            // 
            // label_zakres_dat
            // 
            label_zakres_dat.AutoSize = true;
            label_zakres_dat.Font = new Font("Segoe UI", 12F);
            label_zakres_dat.Location = new Point(12, 77);
            label_zakres_dat.Name = "label_zakres_dat";
            label_zakres_dat.Size = new Size(146, 21);
            label_zakres_dat.TabIndex = 9;
            label_zakres_dat.Text = "Zakres dat (od - do)";
            // 
            // label_lekarz
            // 
            label_lekarz.AutoSize = true;
            label_lekarz.Font = new Font("Segoe UI", 12F);
            label_lekarz.Location = new Point(443, 14);
            label_lekarz.Name = "label_lekarz";
            label_lekarz.Size = new Size(55, 21);
            label_lekarz.TabIndex = 10;
            label_lekarz.Text = "Lekarz";
            // 
            // label_specjalizacja
            // 
            label_specjalizacja.AutoSize = true;
            label_specjalizacja.Font = new Font("Segoe UI", 12F);
            label_specjalizacja.Location = new Point(650, 13);
            label_specjalizacja.Name = "label_specjalizacja";
            label_specjalizacja.Size = new Size(97, 21);
            label_specjalizacja.TabIndex = 11;
            label_specjalizacja.Text = "Specjalizacja";
            // 
            // cmb_lekarz
            // 
            cmb_lekarz.Font = new Font("Segoe UI", 12F);
            cmb_lekarz.FormattingEnabled = true;
            cmb_lekarz.Location = new Point(443, 37);
            cmb_lekarz.Name = "cmb_lekarz";
            cmb_lekarz.Size = new Size(199, 29);
            cmb_lekarz.TabIndex = 12;
            // 
            // cmb_specjalizacja
            // 
            cmb_specjalizacja.Font = new Font("Segoe UI", 12F);
            cmb_specjalizacja.FormattingEnabled = true;
            cmb_specjalizacja.Location = new Point(650, 37);
            cmb_specjalizacja.Name = "cmb_specjalizacja";
            cmb_specjalizacja.Size = new Size(199, 29);
            cmb_specjalizacja.TabIndex = 13;
            // 
            // panel1
            // 
            panel1.Controls.Add(btn_szczegoly);
            panel1.Controls.Add(btn_szukaj);
            panel1.Controls.Add(label_imie_nazwisko_pesel);
            panel1.Controls.Add(cmb_specjalizacja);
            panel1.Controls.Add(txt_szukanaFraza);
            panel1.Controls.Add(cmb_lekarz);
            panel1.Controls.Add(label_zakres_dat);
            panel1.Controls.Add(label_specjalizacja);
            panel1.Controls.Add(label_lekarz);
            panel1.Controls.Add(lbl_komunikat);
            panel1.Controls.Add(dtp_dataDo);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(dtp_dataOd);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(880, 145);
            panel1.TabIndex = 14;
            // 
            // lbl_komunikat
            // 
            lbl_komunikat.AutoSize = true;
            lbl_komunikat.ForeColor = Color.Coral;
            lbl_komunikat.Location = new Point(443, 77);
            lbl_komunikat.Name = "lbl_komunikat";
            lbl_komunikat.Size = new Size(65, 15);
            lbl_komunikat.TabIndex = 1;
            lbl_komunikat.Text = "Komunikat";
            // 
            // datagridview_wizyty
            // 
            datagridview_wizyty.BackgroundColor = SystemColors.ActiveBorder;
            datagridview_wizyty.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            datagridview_wizyty.Location = new Point(12, 151);
            datagridview_wizyty.Name = "datagridview_wizyty";
            datagridview_wizyty.RowHeadersVisible = false;
            datagridview_wizyty.Size = new Size(837, 418);
            datagridview_wizyty.TabIndex = 0;
            // 
            // btn_szczegoly
            // 
            btn_szczegoly.Font = new Font("Segoe UI", 12F);
            btn_szczegoly.Location = new Point(445, 96);
            btn_szczegoly.Name = "btn_szczegoly";
            btn_szczegoly.Size = new Size(199, 37);
            btn_szczegoly.TabIndex = 14;
            btn_szczegoly.Text = "Szczegóły wizyty";
            btn_szczegoly.UseVisualStyleBackColor = true;
            btn_szczegoly.Click += btn_szczegoly_Click;
            // 
            // ListaWizytPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Controls.Add(datagridview_wizyty);
            Name = "ListaWizytPanel";
            Size = new Size(880, 587);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)datagridview_wizyty).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView datagridview_wizyty;
        private TextBox txt_szukanaFraza;
        private Button btn_szukaj;
        private DateTimePicker dtp_dataOd;
        private Label label1;
        private DateTimePicker dtp_dataDo;
        private Label label_imie_nazwisko_pesel;
        private Label label_zakres_dat;
        private Label label_lekarz;
        private Label label_specjalizacja;
        private ComboBox cmb_lekarz;
        private ComboBox cmb_specjalizacja;
        private Panel panel1;
        private Label lbl_komunikat;
        private Button btn_szczegoly;
    }
}
