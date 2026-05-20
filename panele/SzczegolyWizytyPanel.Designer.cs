namespace Przychodnia.panele
{
    partial class SzczegolyWizytyPanel
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
            lbl_status = new Label();
            label2 = new Label();
            label3 = new Label();
            txt_schorzenia = new TextBox();
            txt_zalecenia = new TextBox();
            btn_zapisz = new Button();
            SuspendLayout();
            // 
            // lbl_status
            // 
            lbl_status.AutoSize = true;
            lbl_status.Location = new Point(3, 5);
            lbl_status.Name = "lbl_status";
            lbl_status.Size = new Size(0, 15);
            lbl_status.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 41);
            label2.Name = "label2";
            label2.Size = new Size(291, 15);
            label2.TabIndex = 1;
            label2.Text = "Informacje o schorzeniach i dolegliwościach pacjenta:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 204);
            label3.Name = "label3";
            label3.Size = new Size(177, 15);
            label3.TabIndex = 2;
            label3.Text = "Zalecenia i przepisane lekarstwa:";
            // 
            // txt_schorzenia
            // 
            txt_schorzenia.Location = new Point(3, 59);
            txt_schorzenia.Multiline = true;
            txt_schorzenia.Name = "txt_schorzenia";
            txt_schorzenia.Size = new Size(312, 121);
            txt_schorzenia.TabIndex = 3;
            // 
            // txt_zalecenia
            // 
            txt_zalecenia.Location = new Point(3, 222);
            txt_zalecenia.Multiline = true;
            txt_zalecenia.Name = "txt_zalecenia";
            txt_zalecenia.Size = new Size(312, 118);
            txt_zalecenia.TabIndex = 4;
            // 
            // btn_zapisz
            // 
            btn_zapisz.Location = new Point(3, 346);
            btn_zapisz.Name = "btn_zapisz";
            btn_zapisz.Size = new Size(312, 44);
            btn_zapisz.TabIndex = 5;
            btn_zapisz.Text = "Zapisz dane wizyty";
            btn_zapisz.UseVisualStyleBackColor = true;
            btn_zapisz.Click += btn_zapisz_Click;
            // 
            // SzczegolyWizytyPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btn_zapisz);
            Controls.Add(txt_zalecenia);
            Controls.Add(txt_schorzenia);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(lbl_status);
            Name = "SzczegolyWizytyPanel";
            Size = new Size(320, 397);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbl_status;
        private Label label2;
        private Label label3;
        private TextBox txt_schorzenia;
        private TextBox txt_zalecenia;
        private Button btn_zapisz;
    }
}
