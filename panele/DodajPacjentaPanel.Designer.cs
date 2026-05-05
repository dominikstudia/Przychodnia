namespace Przychodnia
{
    partial class DodajPacjentaPanel
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
            textbox_imie = new TextBox();
            textbox_nazwisko = new TextBox();
            textbox_pesel = new TextBox();
            textbox_telefon = new TextBox();
            dateTimePicker_dataUrodzenia = new DateTimePicker();
            textbox_email = new TextBox();
            textbox_miejscowosc = new TextBox();
            textbox_kodPocztowy = new TextBox();
            textbox_ulica = new TextBox();
            textbox_nrMieszkania = new TextBox();
            textbox_nrDomu = new TextBox();
            comboBox_plec = new ComboBox();
            btnZarejestruj = new Button();
            btnAnuluj = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            SuspendLayout();
            // 
            // textbox_imie
            // 
            textbox_imie.Location = new Point(119, 3);
            textbox_imie.Name = "textbox_imie";
            textbox_imie.Size = new Size(200, 23);
            textbox_imie.TabIndex = 0;
            // 
            // textbox_nazwisko
            // 
            textbox_nazwisko.Location = new Point(119, 32);
            textbox_nazwisko.Name = "textbox_nazwisko";
            textbox_nazwisko.Size = new Size(200, 23);
            textbox_nazwisko.TabIndex = 1;
            // 
            // textbox_pesel
            // 
            textbox_pesel.Location = new Point(119, 61);
            textbox_pesel.Name = "textbox_pesel";
            textbox_pesel.Size = new Size(200, 23);
            textbox_pesel.TabIndex = 2;
            // 
            // textbox_telefon
            // 
            textbox_telefon.Location = new Point(119, 148);
            textbox_telefon.Name = "textbox_telefon";
            textbox_telefon.Size = new Size(200, 23);
            textbox_telefon.TabIndex = 3;
            // 
            // dateTimePicker_dataUrodzenia
            // 
            dateTimePicker_dataUrodzenia.Location = new Point(119, 90);
            dateTimePicker_dataUrodzenia.Name = "dateTimePicker_dataUrodzenia";
            dateTimePicker_dataUrodzenia.Size = new Size(200, 23);
            dateTimePicker_dataUrodzenia.TabIndex = 4;
            // 
            // textbox_email
            // 
            textbox_email.Location = new Point(119, 177);
            textbox_email.Name = "textbox_email";
            textbox_email.Size = new Size(200, 23);
            textbox_email.TabIndex = 5;
            // 
            // textbox_miejscowosc
            // 
            textbox_miejscowosc.Location = new Point(119, 206);
            textbox_miejscowosc.Name = "textbox_miejscowosc";
            textbox_miejscowosc.Size = new Size(200, 23);
            textbox_miejscowosc.TabIndex = 6;
            // 
            // textbox_kodPocztowy
            // 
            textbox_kodPocztowy.Location = new Point(119, 235);
            textbox_kodPocztowy.Name = "textbox_kodPocztowy";
            textbox_kodPocztowy.Size = new Size(200, 23);
            textbox_kodPocztowy.TabIndex = 7;
            // 
            // textbox_ulica
            // 
            textbox_ulica.Location = new Point(119, 264);
            textbox_ulica.Name = "textbox_ulica";
            textbox_ulica.Size = new Size(200, 23);
            textbox_ulica.TabIndex = 8;
            // 
            // textbox_nrMieszkania
            // 
            textbox_nrMieszkania.Location = new Point(119, 322);
            textbox_nrMieszkania.Name = "textbox_nrMieszkania";
            textbox_nrMieszkania.Size = new Size(200, 23);
            textbox_nrMieszkania.TabIndex = 9;
            // 
            // textbox_nrDomu
            // 
            textbox_nrDomu.Location = new Point(119, 293);
            textbox_nrDomu.Name = "textbox_nrDomu";
            textbox_nrDomu.Size = new Size(200, 23);
            textbox_nrDomu.TabIndex = 10;
            // 
            // comboBox_plec
            // 
            comboBox_plec.FormattingEnabled = true;
            comboBox_plec.Items.AddRange(new object[] { "Mężczyzna", "Kobieta" });
            comboBox_plec.Location = new Point(119, 119);
            comboBox_plec.Name = "comboBox_plec";
            comboBox_plec.Size = new Size(200, 23);
            comboBox_plec.TabIndex = 11;
            // 
            // btnZarejestruj
            // 
            btnZarejestruj.Location = new Point(4, 351);
            btnZarejestruj.Name = "btnZarejestruj";
            btnZarejestruj.Size = new Size(150, 43);
            btnZarejestruj.TabIndex = 14;
            btnZarejestruj.Text = "Zarejestruj";
            btnZarejestruj.UseVisualStyleBackColor = true;
            btnZarejestruj.Click += btnZarejestruj_Click;
            // 
            // btnAnuluj
            // 
            btnAnuluj.Location = new Point(169, 351);
            btnAnuluj.Name = "btnAnuluj";
            btnAnuluj.Size = new Size(150, 43);
            btnAnuluj.TabIndex = 15;
            btnAnuluj.Text = "Anuluj";
            btnAnuluj.UseVisualStyleBackColor = true;
            btnAnuluj.Click += btnAnuluj_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F);
            label1.Location = new Point(4, 6);
            label1.Name = "label1";
            label1.Size = new Size(55, 20);
            label1.TabIndex = 16;
            label1.Text = "Imiona";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F);
            label2.Location = new Point(4, 35);
            label2.Name = "label2";
            label2.Size = new Size(72, 20);
            label2.TabIndex = 17;
            label2.Text = "Nazwisko";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F);
            label3.Location = new Point(4, 64);
            label3.Name = "label3";
            label3.Size = new Size(42, 20);
            label3.TabIndex = 18;
            label3.Text = "Pesel";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F);
            label4.Location = new Point(4, 93);
            label4.Name = "label4";
            label4.Size = new Size(111, 20);
            label4.TabIndex = 19;
            label4.Text = "Data urodzenia";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11.25F);
            label5.Location = new Point(4, 122);
            label5.Name = "label5";
            label5.Size = new Size(36, 20);
            label5.TabIndex = 20;
            label5.Text = "Płeć";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11.25F);
            label6.Location = new Point(4, 151);
            label6.Name = "label6";
            label6.Size = new Size(113, 20);
            label6.TabIndex = 21;
            label6.Text = "Numer telefonu";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 11.25F);
            label7.Location = new Point(4, 180);
            label7.Name = "label7";
            label7.Size = new Size(94, 20);
            label7.TabIndex = 22;
            label7.Text = "Adres e-mail";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 11.25F);
            label8.Location = new Point(4, 209);
            label8.Name = "label8";
            label8.Size = new Size(93, 20);
            label8.TabIndex = 23;
            label8.Text = "Miejscowość";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 11.25F);
            label9.Location = new Point(4, 238);
            label9.Name = "label9";
            label9.Size = new Size(104, 20);
            label9.TabIndex = 24;
            label9.Text = "Kod pocztowy";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 11.25F);
            label10.Location = new Point(4, 267);
            label10.Name = "label10";
            label10.Size = new Size(42, 20);
            label10.TabIndex = 25;
            label10.Text = "Ulica";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 11.25F);
            label11.Location = new Point(4, 296);
            label11.Name = "label11";
            label11.Size = new Size(104, 20);
            label11.TabIndex = 26;
            label11.Text = "Numer posesji";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 11.25F);
            label12.Location = new Point(4, 325);
            label12.Name = "label12";
            label12.Size = new Size(98, 20);
            label12.TabIndex = 27;
            label12.Text = "Numer lokalu";
            // 
            // DodajPacjentaPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnAnuluj);
            Controls.Add(btnZarejestruj);
            Controls.Add(comboBox_plec);
            Controls.Add(textbox_nrDomu);
            Controls.Add(textbox_nrMieszkania);
            Controls.Add(textbox_ulica);
            Controls.Add(textbox_kodPocztowy);
            Controls.Add(textbox_miejscowosc);
            Controls.Add(textbox_email);
            Controls.Add(dateTimePicker_dataUrodzenia);
            Controls.Add(textbox_telefon);
            Controls.Add(textbox_pesel);
            Controls.Add(textbox_nazwisko);
            Controls.Add(textbox_imie);
            Name = "DodajPacjentaPanel";
            Size = new Size(329, 401);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textbox_imie;
        private TextBox textbox_nazwisko;
        private TextBox textbox_pesel;
        private TextBox textbox_telefon;
        private DateTimePicker dateTimePicker_dataUrodzenia;
        private TextBox textbox_email;
        private TextBox textbox_miejscowosc;
        private TextBox textbox_kodPocztowy;
        private TextBox textbox_ulica;
        private TextBox textbox_nrMieszkania;
        private TextBox textbox_nrDomu;
        private ComboBox comboBox_plec;
        private Button btnZarejestruj;
        private Button btnAnuluj;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
    }
}