namespace Przychodnia
{
    partial class ListaUprawnienPanel
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
            listbox_uprawnienia = new ListBox();
            lbl_instrukcja = new Label();
            btn_wyswietl_uzytkownikow = new Button();
            btn_nadaj_masowo = new Button();
            btn_zabierz_masowo = new Button();
            SuspendLayout();
            // 
            // listbox_uprawnienia
            // 
            listbox_uprawnienia.FormattingEnabled = true;
            listbox_uprawnienia.Location = new Point(132, 125);
            listbox_uprawnienia.Name = "listbox_uprawnienia";
            listbox_uprawnienia.Size = new Size(566, 214);
            listbox_uprawnienia.TabIndex = 0;
            // 
            // lbl_instrukcja
            // 
            lbl_instrukcja.AutoSize = true;
            lbl_instrukcja.Font = new Font("Segoe UI", 18F);
            lbl_instrukcja.ForeColor = SystemColors.ControlDarkDark;
            lbl_instrukcja.Location = new Point(344, 41);
            lbl_instrukcja.Name = "lbl_instrukcja";
            lbl_instrukcja.Size = new Size(215, 32);
            lbl_instrukcja.TabIndex = 1;
            lbl_instrukcja.Text = "Wybierz rolę z listy";
            // 
            // btn_wyswietl_uzytkownikow
            // 
            btn_wyswietl_uzytkownikow.Location = new Point(70, 394);
            btn_wyswietl_uzytkownikow.Name = "btn_wyswietl_uzytkownikow";
            btn_wyswietl_uzytkownikow.Size = new Size(215, 55);
            btn_wyswietl_uzytkownikow.TabIndex = 3;
            btn_wyswietl_uzytkownikow.Text = "Wyświetl użytkowników z podanymi rolami";
            btn_wyswietl_uzytkownikow.UseVisualStyleBackColor = true;
            btn_wyswietl_uzytkownikow.Click += btn_wyswietl_uzytkownikow_Click;
            // 
            // btn_nadaj_masowo
            // 
            btn_nadaj_masowo.Location = new Point(344, 394);
            btn_nadaj_masowo.Name = "btn_nadaj_masowo";
            btn_nadaj_masowo.Size = new Size(215, 55);
            btn_nadaj_masowo.TabIndex = 4;
            btn_nadaj_masowo.Text = "Nadaj masowo role";
            btn_nadaj_masowo.UseVisualStyleBackColor = true;
            btn_nadaj_masowo.Click += btn_nadaj_masowo_Click;
            // 
            // btn_zabierz_masowo
            // 
            btn_zabierz_masowo.Location = new Point(592, 394);
            btn_zabierz_masowo.Name = "btn_zabierz_masowo";
            btn_zabierz_masowo.Size = new Size(215, 55);
            btn_zabierz_masowo.TabIndex = 5;
            btn_zabierz_masowo.Text = "Zabierz masowo role";
            btn_zabierz_masowo.UseVisualStyleBackColor = true;
            btn_zabierz_masowo.Click += btn_zabierz_masowo_Click;
            // 
            // ListaUprawnienPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btn_zabierz_masowo);
            Controls.Add(btn_nadaj_masowo);
            Controls.Add(btn_wyswietl_uzytkownikow);
            Controls.Add(lbl_instrukcja);
            Controls.Add(listbox_uprawnienia);
            Name = "ListaUprawnienPanel";
            Size = new Size(880, 487);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listbox_uprawnienia;
        private Label lbl_instrukcja;
        private Button btn_wyswietl_uzytkownikow;
        private Button btn_nadaj_masowo;
        private Button btn_zabierz_masowo;
    }
}
