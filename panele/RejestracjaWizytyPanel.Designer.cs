namespace Przychodnia.panele
{
    partial class RejestracjaWizytyPanel
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
            combobox_specjalizacja = new ComboBox();
            combobox_lekarz = new ComboBox();
            datepicker_data_wizyty = new DateTimePicker();
            combobox_gabinet = new ComboBox();
            btn_zapisz_wizyte = new Button();
            Specjalizacja = new Label();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // combobox_specjalizacja
            // 
            combobox_specjalizacja.FormattingEnabled = true;
            combobox_specjalizacja.Location = new Point(140, 105);
            combobox_specjalizacja.Name = "combobox_specjalizacja";
            combobox_specjalizacja.Size = new Size(121, 23);
            combobox_specjalizacja.TabIndex = 0;
            // 
            // combobox_lekarz
            // 
            combobox_lekarz.FormattingEnabled = true;
            combobox_lekarz.Location = new Point(338, 105);
            combobox_lekarz.Name = "combobox_lekarz";
            combobox_lekarz.Size = new Size(121, 23);
            combobox_lekarz.TabIndex = 1;
            // 
            // datepicker_data_wizyty
            // 
            datepicker_data_wizyty.Location = new Point(140, 181);
            datepicker_data_wizyty.Name = "datepicker_data_wizyty";
            datepicker_data_wizyty.Size = new Size(200, 23);
            datepicker_data_wizyty.TabIndex = 2;
            // 
            // combobox_gabinet
            // 
            combobox_gabinet.FormattingEnabled = true;
            combobox_gabinet.Location = new Point(391, 181);
            combobox_gabinet.Name = "combobox_gabinet";
            combobox_gabinet.Size = new Size(121, 23);
            combobox_gabinet.TabIndex = 3;
            // 
            // btn_zapisz_wizyte
            // 
            btn_zapisz_wizyte.Location = new Point(326, 275);
            btn_zapisz_wizyte.Name = "btn_zapisz_wizyte";
            btn_zapisz_wizyte.Size = new Size(84, 23);
            btn_zapisz_wizyte.TabIndex = 4;
            btn_zapisz_wizyte.Text = "Zapisz wizyte";
            btn_zapisz_wizyte.UseVisualStyleBackColor = true;
            // 
            // Specjalizacja
            // 
            Specjalizacja.AutoSize = true;
            Specjalizacja.Location = new Point(144, 70);
            Specjalizacja.Name = "Specjalizacja";
            Specjalizacja.Size = new Size(73, 15);
            Specjalizacja.TabIndex = 5;
            Specjalizacja.Text = "Specjalizacja";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(340, 75);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 6;
            label1.Text = "Lekarz";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(392, 154);
            label2.Name = "label2";
            label2.Size = new Size(48, 15);
            label2.TabIndex = 7;
            label2.Text = "Gabinet";
            // 
            // RejestracjaWizytyPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Specjalizacja);
            Controls.Add(btn_zapisz_wizyte);
            Controls.Add(combobox_gabinet);
            Controls.Add(datepicker_data_wizyty);
            Controls.Add(combobox_lekarz);
            Controls.Add(combobox_specjalizacja);
            Name = "RejestracjaWizytyPanel";
            Size = new Size(808, 389);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox combobox_specjalizacja;
        private ComboBox combobox_lekarz;
        private DateTimePicker datepicker_data_wizyty;
        private ComboBox combobox_gabinet;
        private Button btn_zapisz_wizyte;
        private Label Specjalizacja;
        private Label label1;
        private Label label2;
    }
}
