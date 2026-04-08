namespace Przychodnia
{
    partial class ZarzadzajUzytkownikiemPanel
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
            tableLayoutPanel1 = new TableLayoutPanel();
            lbl_haslo = new Label();
            label_uprawnienia = new Label();
            textbox_kod_pocztowy = new MaskedTextBox();
            textbox_numer_telefonu = new MaskedTextBox();
            label_kod_pocztowy = new Label();
            label_numer_posesji = new Label();
            label_miejscowosc = new Label();
            label_numer_telefonu = new Label();
            label_ulica = new Label();
            label_data_urodzenia = new Label();
            label_numer_lokalu = new Label();
            label_numer_pesel = new Label();
            label_plec = new Label();
            label_adres_email = new Label();
            label_nazwisko = new Label();
            label_imiona = new Label();
            label_login = new Label();
            textbox_login = new TextBox();
            textbox_email = new TextBox();
            textbox_imiona = new TextBox();
            textbox_nazwisko = new TextBox();
            textbox_pesel = new MaskedTextBox();
            combobox_plec = new ComboBox();
            textbox_miejscowosc = new TextBox();
            textbox_ulica = new TextBox();
            textbox_numer_posesji = new TextBox();
            textbox_numer_lokalu = new TextBox();
            datetimerpicker_data_urodzenia = new DateTimePicker();
            tableLayoutPanel2 = new TableLayoutPanel();
            btn_anuluj = new Button();
            btn_potwierdz = new Button();
            textbox_haslo = new TextBox();
            checkedlistbox_uprawnienia = new CheckedListBox();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 400F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(lbl_haslo, 1, 5);
            tableLayoutPanel1.Controls.Add(label_uprawnienia, 1, 10);
            tableLayoutPanel1.Controls.Add(textbox_kod_pocztowy, 2, 13);
            tableLayoutPanel1.Controls.Add(textbox_numer_telefonu, 2, 9);
            tableLayoutPanel1.Controls.Add(label_kod_pocztowy, 1, 13);
            tableLayoutPanel1.Controls.Add(label_numer_posesji, 1, 14);
            tableLayoutPanel1.Controls.Add(label_miejscowosc, 1, 11);
            tableLayoutPanel1.Controls.Add(label_numer_telefonu, 1, 9);
            tableLayoutPanel1.Controls.Add(label_ulica, 1, 12);
            tableLayoutPanel1.Controls.Add(label_data_urodzenia, 1, 8);
            tableLayoutPanel1.Controls.Add(label_numer_lokalu, 1, 15);
            tableLayoutPanel1.Controls.Add(label_numer_pesel, 1, 6);
            tableLayoutPanel1.Controls.Add(label_plec, 1, 7);
            tableLayoutPanel1.Controls.Add(label_adres_email, 1, 2);
            tableLayoutPanel1.Controls.Add(label_nazwisko, 1, 4);
            tableLayoutPanel1.Controls.Add(label_imiona, 1, 3);
            tableLayoutPanel1.Controls.Add(label_login, 1, 1);
            tableLayoutPanel1.Controls.Add(textbox_login, 2, 1);
            tableLayoutPanel1.Controls.Add(textbox_email, 2, 2);
            tableLayoutPanel1.Controls.Add(textbox_imiona, 2, 3);
            tableLayoutPanel1.Controls.Add(textbox_nazwisko, 2, 4);
            tableLayoutPanel1.Controls.Add(textbox_pesel, 2, 6);
            tableLayoutPanel1.Controls.Add(combobox_plec, 2, 7);
            tableLayoutPanel1.Controls.Add(textbox_miejscowosc, 2, 11);
            tableLayoutPanel1.Controls.Add(textbox_ulica, 2, 12);
            tableLayoutPanel1.Controls.Add(textbox_numer_posesji, 2, 14);
            tableLayoutPanel1.Controls.Add(textbox_numer_lokalu, 2, 15);
            tableLayoutPanel1.Controls.Add(datetimerpicker_data_urodzenia, 2, 8);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 17);
            tableLayoutPanel1.Controls.Add(textbox_haslo, 2, 5);
            tableLayoutPanel1.Controls.Add(checkedlistbox_uprawnienia, 2, 10);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 19;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.49763775F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.49763775F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.49763775F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.49763775F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.49763775F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.49763775F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.49763775F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.49763775F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.49998665F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.499863F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.49763775F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.49763775F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.49763775F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.49763775F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.49763775F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.49763775F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.627995F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3.405237F));
            tableLayoutPanel1.Size = new Size(880, 720);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lbl_haslo
            // 
            lbl_haslo.Dock = DockStyle.Fill;
            lbl_haslo.Font = new Font("Segoe UI", 12F);
            lbl_haslo.Location = new Point(143, 170);
            lbl_haslo.Name = "lbl_haslo";
            lbl_haslo.Size = new Size(194, 34);
            lbl_haslo.TabIndex = 33;
            lbl_haslo.Text = "Hasło";
            lbl_haslo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_uprawnienia
            // 
            label_uprawnienia.Dock = DockStyle.Fill;
            label_uprawnienia.Font = new Font("Segoe UI", 12F);
            label_uprawnienia.Location = new Point(143, 340);
            label_uprawnienia.Name = "label_uprawnienia";
            label_uprawnienia.Size = new Size(194, 100);
            label_uprawnienia.TabIndex = 32;
            label_uprawnienia.Text = "Uprawnienia";
            label_uprawnienia.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textbox_kod_pocztowy
            // 
            textbox_kod_pocztowy.BeepOnError = true;
            textbox_kod_pocztowy.Dock = DockStyle.Fill;
            textbox_kod_pocztowy.Font = new Font("Segoe UI", 12F);
            textbox_kod_pocztowy.Location = new Point(343, 511);
            textbox_kod_pocztowy.Mask = "00-000";
            textbox_kod_pocztowy.Name = "textbox_kod_pocztowy";
            textbox_kod_pocztowy.Size = new Size(394, 29);
            textbox_kod_pocztowy.TabIndex = 26;
            textbox_kod_pocztowy.Click += textbox_kod_pocztowy_Click;
            // 
            // textbox_numer_telefonu
            // 
            textbox_numer_telefonu.BeepOnError = true;
            textbox_numer_telefonu.Dock = DockStyle.Fill;
            textbox_numer_telefonu.Font = new Font("Segoe UI", 12F);
            textbox_numer_telefonu.Location = new Point(343, 309);
            textbox_numer_telefonu.Mask = "000000000";
            textbox_numer_telefonu.Name = "textbox_numer_telefonu";
            textbox_numer_telefonu.Size = new Size(394, 29);
            textbox_numer_telefonu.TabIndex = 25;
            textbox_numer_telefonu.Click += textbox_numer_telefonu_Click;
            // 
            // label_kod_pocztowy
            // 
            label_kod_pocztowy.Dock = DockStyle.Fill;
            label_kod_pocztowy.Font = new Font("Segoe UI", 12F);
            label_kod_pocztowy.Location = new Point(143, 508);
            label_kod_pocztowy.Name = "label_kod_pocztowy";
            label_kod_pocztowy.Size = new Size(194, 34);
            label_kod_pocztowy.TabIndex = 14;
            label_kod_pocztowy.Text = "Kod pocztowy";
            label_kod_pocztowy.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_numer_posesji
            // 
            label_numer_posesji.Dock = DockStyle.Fill;
            label_numer_posesji.Font = new Font("Segoe UI", 12F);
            label_numer_posesji.Location = new Point(143, 542);
            label_numer_posesji.Name = "label_numer_posesji";
            label_numer_posesji.Size = new Size(194, 34);
            label_numer_posesji.TabIndex = 13;
            label_numer_posesji.Text = "Numer posesji";
            label_numer_posesji.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_miejscowosc
            // 
            label_miejscowosc.Dock = DockStyle.Fill;
            label_miejscowosc.Font = new Font("Segoe UI", 12F);
            label_miejscowosc.Location = new Point(143, 440);
            label_miejscowosc.Name = "label_miejscowosc";
            label_miejscowosc.Size = new Size(194, 34);
            label_miejscowosc.TabIndex = 12;
            label_miejscowosc.Text = "Miejscowość";
            label_miejscowosc.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_numer_telefonu
            // 
            label_numer_telefonu.Dock = DockStyle.Fill;
            label_numer_telefonu.Font = new Font("Segoe UI", 12F);
            label_numer_telefonu.Location = new Point(143, 306);
            label_numer_telefonu.Name = "label_numer_telefonu";
            label_numer_telefonu.Size = new Size(194, 34);
            label_numer_telefonu.TabIndex = 11;
            label_numer_telefonu.Text = "Numer telefonu";
            label_numer_telefonu.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_ulica
            // 
            label_ulica.Dock = DockStyle.Fill;
            label_ulica.Font = new Font("Segoe UI", 12F);
            label_ulica.Location = new Point(143, 474);
            label_ulica.Name = "label_ulica";
            label_ulica.Size = new Size(194, 34);
            label_ulica.TabIndex = 10;
            label_ulica.Text = "Ulica";
            label_ulica.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_data_urodzenia
            // 
            label_data_urodzenia.Dock = DockStyle.Fill;
            label_data_urodzenia.Font = new Font("Segoe UI", 12F);
            label_data_urodzenia.Location = new Point(143, 272);
            label_data_urodzenia.Name = "label_data_urodzenia";
            label_data_urodzenia.Size = new Size(194, 34);
            label_data_urodzenia.TabIndex = 9;
            label_data_urodzenia.Text = "Data urodzenia";
            label_data_urodzenia.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_numer_lokalu
            // 
            label_numer_lokalu.Dock = DockStyle.Fill;
            label_numer_lokalu.Font = new Font("Segoe UI", 12F);
            label_numer_lokalu.Location = new Point(143, 576);
            label_numer_lokalu.Name = "label_numer_lokalu";
            label_numer_lokalu.Size = new Size(194, 34);
            label_numer_lokalu.TabIndex = 8;
            label_numer_lokalu.Text = "Numer lokalu";
            label_numer_lokalu.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_numer_pesel
            // 
            label_numer_pesel.Dock = DockStyle.Fill;
            label_numer_pesel.Font = new Font("Segoe UI", 12F);
            label_numer_pesel.Location = new Point(143, 204);
            label_numer_pesel.Name = "label_numer_pesel";
            label_numer_pesel.Size = new Size(194, 34);
            label_numer_pesel.TabIndex = 7;
            label_numer_pesel.Text = "Numer pesel";
            label_numer_pesel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_plec
            // 
            label_plec.Dock = DockStyle.Fill;
            label_plec.Font = new Font("Segoe UI", 12F);
            label_plec.Location = new Point(143, 238);
            label_plec.Name = "label_plec";
            label_plec.Size = new Size(194, 34);
            label_plec.TabIndex = 6;
            label_plec.Text = "Płeć";
            label_plec.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_adres_email
            // 
            label_adres_email.Dock = DockStyle.Fill;
            label_adres_email.Font = new Font("Segoe UI", 12F);
            label_adres_email.Location = new Point(143, 68);
            label_adres_email.Name = "label_adres_email";
            label_adres_email.Size = new Size(194, 34);
            label_adres_email.TabIndex = 5;
            label_adres_email.Text = "Adres e-mail";
            label_adres_email.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_nazwisko
            // 
            label_nazwisko.Dock = DockStyle.Fill;
            label_nazwisko.Font = new Font("Segoe UI", 12F);
            label_nazwisko.Location = new Point(143, 136);
            label_nazwisko.Name = "label_nazwisko";
            label_nazwisko.Size = new Size(194, 34);
            label_nazwisko.TabIndex = 4;
            label_nazwisko.Text = "Nazwisko";
            label_nazwisko.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_imiona
            // 
            label_imiona.Dock = DockStyle.Fill;
            label_imiona.Font = new Font("Segoe UI", 12F);
            label_imiona.Location = new Point(143, 102);
            label_imiona.Name = "label_imiona";
            label_imiona.Size = new Size(194, 34);
            label_imiona.TabIndex = 3;
            label_imiona.Text = "Imiona";
            label_imiona.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_login
            // 
            label_login.Dock = DockStyle.Fill;
            label_login.Font = new Font("Segoe UI", 12F);
            label_login.Location = new Point(143, 34);
            label_login.Name = "label_login";
            label_login.Size = new Size(194, 34);
            label_login.TabIndex = 2;
            label_login.Text = "Login";
            label_login.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textbox_login
            // 
            textbox_login.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textbox_login.Font = new Font("Segoe UI", 12F);
            textbox_login.Location = new Point(343, 37);
            textbox_login.Name = "textbox_login";
            textbox_login.Size = new Size(394, 29);
            textbox_login.TabIndex = 1;
            textbox_login.KeyPress += textbox_login_KeyPress;
            // 
            // textbox_email
            // 
            textbox_email.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textbox_email.Font = new Font("Segoe UI", 12F);
            textbox_email.Location = new Point(343, 71);
            textbox_email.Name = "textbox_email";
            textbox_email.Size = new Size(394, 29);
            textbox_email.TabIndex = 15;
            textbox_email.KeyPress += textbox_email_KeyPress;
            // 
            // textbox_imiona
            // 
            textbox_imiona.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textbox_imiona.Font = new Font("Segoe UI", 12F);
            textbox_imiona.Location = new Point(343, 105);
            textbox_imiona.Name = "textbox_imiona";
            textbox_imiona.Size = new Size(394, 29);
            textbox_imiona.TabIndex = 16;
            textbox_imiona.KeyPress += textbox_imiona_KeyPress;
            // 
            // textbox_nazwisko
            // 
            textbox_nazwisko.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textbox_nazwisko.Font = new Font("Segoe UI", 12F);
            textbox_nazwisko.Location = new Point(343, 139);
            textbox_nazwisko.Name = "textbox_nazwisko";
            textbox_nazwisko.Size = new Size(394, 29);
            textbox_nazwisko.TabIndex = 17;
            textbox_nazwisko.KeyPress += textbox_nazwisko_KeyPress;
            // 
            // textbox_pesel
            // 
            textbox_pesel.BeepOnError = true;
            textbox_pesel.Dock = DockStyle.Fill;
            textbox_pesel.Font = new Font("Segoe UI", 12F);
            textbox_pesel.Location = new Point(343, 207);
            textbox_pesel.Mask = "00000000000";
            textbox_pesel.Name = "textbox_pesel";
            textbox_pesel.Size = new Size(394, 29);
            textbox_pesel.TabIndex = 18;
            textbox_pesel.Click += textbox_pesel_Click;
            // 
            // combobox_plec
            // 
            combobox_plec.Dock = DockStyle.Fill;
            combobox_plec.DropDownStyle = ComboBoxStyle.DropDownList;
            combobox_plec.Font = new Font("Segoe UI", 12F);
            combobox_plec.FormattingEnabled = true;
            combobox_plec.Items.AddRange(new object[] { "Mężczyzna", "Kobieta" });
            combobox_plec.Location = new Point(343, 241);
            combobox_plec.Name = "combobox_plec";
            combobox_plec.Size = new Size(394, 29);
            combobox_plec.TabIndex = 19;
            // 
            // textbox_miejscowosc
            // 
            textbox_miejscowosc.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textbox_miejscowosc.Font = new Font("Segoe UI", 12F);
            textbox_miejscowosc.Location = new Point(343, 443);
            textbox_miejscowosc.Name = "textbox_miejscowosc";
            textbox_miejscowosc.Size = new Size(394, 29);
            textbox_miejscowosc.TabIndex = 20;
            textbox_miejscowosc.KeyPress += textbox_miejscowosc_KeyPress;
            // 
            // textbox_ulica
            // 
            textbox_ulica.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textbox_ulica.Font = new Font("Segoe UI", 12F);
            textbox_ulica.Location = new Point(343, 477);
            textbox_ulica.Name = "textbox_ulica";
            textbox_ulica.Size = new Size(394, 29);
            textbox_ulica.TabIndex = 21;
            textbox_ulica.KeyPress += textbox_ulica_KeyPress;
            // 
            // textbox_numer_posesji
            // 
            textbox_numer_posesji.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textbox_numer_posesji.Font = new Font("Segoe UI", 12F);
            textbox_numer_posesji.Location = new Point(343, 545);
            textbox_numer_posesji.Name = "textbox_numer_posesji";
            textbox_numer_posesji.Size = new Size(394, 29);
            textbox_numer_posesji.TabIndex = 22;
            textbox_numer_posesji.KeyPress += textbox_numer_posesji_KeyPress;
            // 
            // textbox_numer_lokalu
            // 
            textbox_numer_lokalu.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textbox_numer_lokalu.Font = new Font("Segoe UI", 12F);
            textbox_numer_lokalu.Location = new Point(343, 579);
            textbox_numer_lokalu.Name = "textbox_numer_lokalu";
            textbox_numer_lokalu.Size = new Size(394, 29);
            textbox_numer_lokalu.TabIndex = 23;
            textbox_numer_lokalu.KeyPress += textbox_numer_lokalu_KeyPress;
            // 
            // datetimerpicker_data_urodzenia
            // 
            datetimerpicker_data_urodzenia.Dock = DockStyle.Fill;
            datetimerpicker_data_urodzenia.Font = new Font("Segoe UI", 12F);
            datetimerpicker_data_urodzenia.Location = new Point(343, 275);
            datetimerpicker_data_urodzenia.Name = "datetimerpicker_data_urodzenia";
            datetimerpicker_data_urodzenia.Size = new Size(394, 29);
            datetimerpicker_data_urodzenia.TabIndex = 24;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel1.SetColumnSpan(tableLayoutPanel2, 2);
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Controls.Add(btn_anuluj, 0, 0);
            tableLayoutPanel2.Controls.Add(btn_potwierdz, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(143, 647);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(594, 47);
            tableLayoutPanel2.TabIndex = 27;
            // 
            // btn_anuluj
            // 
            btn_anuluj.Dock = DockStyle.Fill;
            btn_anuluj.Font = new Font("Segoe UI", 12F);
            btn_anuluj.Location = new Point(3, 3);
            btn_anuluj.Name = "btn_anuluj";
            btn_anuluj.Size = new Size(291, 41);
            btn_anuluj.TabIndex = 0;
            btn_anuluj.Text = "Anuluj";
            btn_anuluj.UseVisualStyleBackColor = true;
            btn_anuluj.Click += btn_anuluj_Click;
            // 
            // btn_potwierdz
            // 
            btn_potwierdz.Dock = DockStyle.Fill;
            btn_potwierdz.Font = new Font("Segoe UI", 12F);
            btn_potwierdz.Location = new Point(300, 3);
            btn_potwierdz.Name = "btn_potwierdz";
            btn_potwierdz.Size = new Size(291, 41);
            btn_potwierdz.TabIndex = 1;
            btn_potwierdz.Text = "Potwierdź";
            btn_potwierdz.UseVisualStyleBackColor = true;
            btn_potwierdz.Click += btn_potwierdz_Click;
            // 
            // textbox_haslo
            // 
            textbox_haslo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textbox_haslo.Font = new Font("Segoe UI", 12F);
            textbox_haslo.Location = new Point(343, 173);
            textbox_haslo.Name = "textbox_haslo";
            textbox_haslo.Size = new Size(394, 29);
            textbox_haslo.TabIndex = 29;
            // 
            // checkedlistbox_uprawnienia
            // 
            checkedlistbox_uprawnienia.ColumnWidth = 120;
            checkedlistbox_uprawnienia.Dock = DockStyle.Fill;
            checkedlistbox_uprawnienia.FormattingEnabled = true;
            checkedlistbox_uprawnienia.Location = new Point(343, 343);
            checkedlistbox_uprawnienia.MultiColumn = true;
            checkedlistbox_uprawnienia.Name = "checkedlistbox_uprawnienia";
            checkedlistbox_uprawnienia.Size = new Size(394, 94);
            checkedlistbox_uprawnienia.TabIndex = 30;
            // 
            // ZarzadzajUzytkownikiemPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "ZarzadzajUzytkownikiemPanel";
            Size = new Size(880, 720);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TextBox textbox_login;
        private Label label_login;
        private Label label_numer_pesel;
        private Label label_plec;
        private Label label_adres_email;
        private Label label_nazwisko;
        private Label label_imiona;
        private Label label_miejscowosc;
        private Label label_numer_telefonu;
        private Label label_ulica;
        private Label label_data_urodzenia;
        private Label label_numer_lokalu;
        private Label label_kod_pocztowy;
        private Label label_numer_posesji;
        private TextBox textbox_email;
        private TextBox textbox_imiona;
        private TextBox textbox_nazwisko;
        private MaskedTextBox textbox_pesel;
        private ComboBox combobox_plec;
        private TextBox textbox_miejscowosc;
        private TextBox textbox_ulica;
        private TextBox textbox_numer_posesji;
        private TextBox textbox_numer_lokalu;
        private DateTimePicker datetimerpicker_data_urodzenia;
        private MaskedTextBox textbox_kod_pocztowy;
        private MaskedTextBox textbox_numer_telefonu;
        private TableLayoutPanel tableLayoutPanel2;
        private Button btn_anuluj;
        private Button btn_potwierdz;
        private TextBox textbox_haslo;
        private CheckedListBox checkedlistbox_uprawnienia;
        private Label lbl_haslo;
        private Label label_uprawnienia;
    }
}
