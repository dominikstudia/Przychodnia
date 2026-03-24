namespace Przychodnia
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel_boczny = new Panel();
            przycisk_wyszukaj_uzytkownika = new Button();
            przycisk_dodaj_uzytkownika = new Button();
            panel_informacyjny = new Panel();
            label_ogolny = new Label();
            panel_edycji = new Panel();
            panel_boczny.SuspendLayout();
            panel_informacyjny.SuspendLayout();
            SuspendLayout();
            // 
            // panel_boczny
            // 
            panel_boczny.BackColor = SystemColors.ControlDark;
            panel_boczny.Controls.Add(przycisk_wyszukaj_uzytkownika);
            panel_boczny.Controls.Add(przycisk_dodaj_uzytkownika);
            panel_boczny.Dock = DockStyle.Left;
            panel_boczny.Location = new Point(0, 100);
            panel_boczny.Name = "panel_boczny";
            panel_boczny.Size = new Size(200, 581);
            panel_boczny.TabIndex = 0;
            // 
            // przycisk_wyszukaj_uzytkownika
            // 
            przycisk_wyszukaj_uzytkownika.Dock = DockStyle.Top;
            przycisk_wyszukaj_uzytkownika.Location = new Point(0, 70);
            przycisk_wyszukaj_uzytkownika.Name = "przycisk_wyszukaj_uzytkownika";
            przycisk_wyszukaj_uzytkownika.Size = new Size(200, 70);
            przycisk_wyszukaj_uzytkownika.TabIndex = 1;
            przycisk_wyszukaj_uzytkownika.Text = "Wyszukaj użytkownika";
            przycisk_wyszukaj_uzytkownika.UseVisualStyleBackColor = true;
            przycisk_wyszukaj_uzytkownika.Click += przycisk_wyszukaj_uzytkownika_Click;
            // 
            // przycisk_dodaj_uzytkownika
            // 
            przycisk_dodaj_uzytkownika.Dock = DockStyle.Top;
            przycisk_dodaj_uzytkownika.Location = new Point(0, 0);
            przycisk_dodaj_uzytkownika.Name = "przycisk_dodaj_uzytkownika";
            przycisk_dodaj_uzytkownika.Size = new Size(200, 70);
            przycisk_dodaj_uzytkownika.TabIndex = 0;
            przycisk_dodaj_uzytkownika.Text = "Dodaj użytkownika";
            przycisk_dodaj_uzytkownika.UseVisualStyleBackColor = true;
            przycisk_dodaj_uzytkownika.Click += przycisk_dodaj_uzytkownika_Click;
            // 
            // panel_informacyjny
            // 
            panel_informacyjny.BackColor = SystemColors.ControlDark;
            panel_informacyjny.Controls.Add(label_ogolny);
            panel_informacyjny.Dock = DockStyle.Top;
            panel_informacyjny.Location = new Point(0, 0);
            panel_informacyjny.Name = "panel_informacyjny";
            panel_informacyjny.Size = new Size(1064, 100);
            panel_informacyjny.TabIndex = 1;
            // 
            // label_ogolny
            // 
            label_ogolny.Dock = DockStyle.Fill;
            label_ogolny.Font = new Font("Arial", 32.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_ogolny.ImageAlign = ContentAlignment.TopCenter;
            label_ogolny.Location = new Point(0, 0);
            label_ogolny.Name = "label_ogolny";
            label_ogolny.Size = new Size(1064, 100);
            label_ogolny.TabIndex = 0;
            label_ogolny.Text = "PRZYCHODNIA";
            label_ogolny.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel_edycji
            // 
            panel_edycji.Dock = DockStyle.Fill;
            panel_edycji.Location = new Point(200, 100);
            panel_edycji.Name = "panel_edycji";
            panel_edycji.Size = new Size(864, 581);
            panel_edycji.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1064, 681);
            Controls.Add(panel_edycji);
            Controls.Add(panel_boczny);
            Controls.Add(panel_informacyjny);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Testowanie oprogramowania";
            panel_boczny.ResumeLayout(false);
            panel_informacyjny.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel_boczny;
        private Button przycisk_wyszukaj_uzytkownika;
        private Button przycisk_dodaj_uzytkownika;
        private Panel panel_informacyjny;
        private Label label_ogolny;
        private Panel panel_edycji;
    }
}
