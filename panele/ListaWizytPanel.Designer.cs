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
            datagridview_wizyty = new DataGridView();
            lbl_komunikat = new Label();
            ((System.ComponentModel.ISupportInitialize)datagridview_wizyty).BeginInit();
            SuspendLayout();
            // 
            // datagridview_wizyty
            // 
            datagridview_wizyty.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            datagridview_wizyty.Location = new Point(3, 61);
            datagridview_wizyty.Name = "datagridview_wizyty";
            datagridview_wizyty.Size = new Size(734, 362);
            datagridview_wizyty.TabIndex = 0;
            // 
            // lbl_komunikat
            // 
            lbl_komunikat.AutoSize = true;
            lbl_komunikat.Location = new Point(290, 22);
            lbl_komunikat.Name = "lbl_komunikat";
            lbl_komunikat.Size = new Size(0, 15);
            lbl_komunikat.TabIndex = 1;
            // 
            // ListaWizytPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lbl_komunikat);
            Controls.Add(datagridview_wizyty);
            Name = "ListaWizytPanel";
            Size = new Size(740, 426);
            ((System.ComponentModel.ISupportInitialize)datagridview_wizyty).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView datagridview_wizyty;
        private Label lbl_komunikat;
    }
}
