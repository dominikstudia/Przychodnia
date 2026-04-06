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
            SuspendLayout();
            // 
            // listbox_uprawnienia
            // 
            listbox_uprawnienia.FormattingEnabled = true;
            listbox_uprawnienia.Location = new Point(22, 22);
            listbox_uprawnienia.Name = "listbox_uprawnienia";
            listbox_uprawnienia.Size = new Size(546, 394);
            listbox_uprawnienia.TabIndex = 0;
            // 
            // ListaUprawnienPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(listbox_uprawnienia);
            Name = "ListaUprawnienPanel";
            Size = new Size(597, 444);
            ResumeLayout(false);
        }

        #endregion

        private ListBox listbox_uprawnienia;
    }
}
