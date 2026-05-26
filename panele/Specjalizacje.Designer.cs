namespace Przychodnia.panele
{
    partial class Specjalizacje
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
            cmbLekarze = new ComboBox();
            cmbSpecjalizacje = new ComboBox();
            btnZapisz = new Button();
            SuspendLayout();
            // 
            // cmbLekarze
            // 
            cmbLekarze.FormattingEnabled = true;
            cmbLekarze.Location = new Point(35, 58);
            cmbLekarze.Name = "cmbLekarze";
            cmbLekarze.Size = new Size(121, 23);
            cmbLekarze.TabIndex = 0;
            // 
            // cmbSpecjalizacje
            // 
            cmbSpecjalizacje.FormattingEnabled = true;
            cmbSpecjalizacje.Location = new Point(198, 58);
            cmbSpecjalizacje.Name = "cmbSpecjalizacje";
            cmbSpecjalizacje.Size = new Size(121, 23);
            cmbSpecjalizacje.TabIndex = 1;
            // 
            // btnZapisz
            // 
            btnZapisz.Location = new Point(134, 171);
            btnZapisz.Name = "btnZapisz";
            btnZapisz.Size = new Size(75, 22);
            btnZapisz.TabIndex = 2;
            btnZapisz.Text = "Zapisz";
            btnZapisz.UseVisualStyleBackColor = true;
            btnZapisz.Click += btnZapisz_Click;
            // 
            // Specjalizacje
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnZapisz);
            Controls.Add(cmbSpecjalizacje);
            Controls.Add(cmbLekarze);
            Name = "Specjalizacje";
            Size = new Size(401, 355);
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cmbLekarze;
        private ComboBox cmbSpecjalizacje;
        private Button btnZapisz;
    }
}
