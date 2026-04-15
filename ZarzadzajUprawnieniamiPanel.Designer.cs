namespace Przychodnia
{
    partial class ZarzadzajUprawnieniamiPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lbl_tytul_panelu = new Label();
            textbox_wyszukiwanie = new TextBox();
            btn_szukaj = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            btn_akcja = new Button();
            datagrid_uzytkownicy = new DataGridView();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)datagrid_uzytkownicy).BeginInit();
            SuspendLayout();
            // 
            // lbl_tytul_panelu
            // 
            lbl_tytul_panelu.AutoSize = true;
            lbl_tytul_panelu.Font = new Font("Segoe UI", 12F);
            lbl_tytul_panelu.Location = new Point(48, 23);
            lbl_tytul_panelu.Name = "lbl_tytul_panelu";
            lbl_tytul_panelu.Size = new Size(252, 21);
            lbl_tytul_panelu.TabIndex = 0;
            lbl_tytul_panelu.Text = "Zarządzanie rolą (rola dynamiczna)";
            // 
            // textbox_wyszukiwanie
            // 
            textbox_wyszukiwanie.Font = new Font("Segoe UI", 12F);
            textbox_wyszukiwanie.Location = new Point(12, 55);
            textbox_wyszukiwanie.Name = "textbox_wyszukiwanie";
            textbox_wyszukiwanie.Size = new Size(365, 29);
            textbox_wyszukiwanie.TabIndex = 1;
            // 
            // btn_szukaj
            // 
            btn_szukaj.Font = new Font("Segoe UI", 12F);
            btn_szukaj.Location = new Point(456, 23);
            btn_szukaj.Name = "btn_szukaj";
            btn_szukaj.Size = new Size(298, 48);
            btn_szukaj.TabIndex = 2;
            btn_szukaj.Text = "Szukaj";
            btn_szukaj.UseVisualStyleBackColor = true;
            btn_szukaj.Click += btn_szukaj_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(lbl_tytul_panelu);
            panel1.Controls.Add(btn_szukaj);
            panel1.Controls.Add(textbox_wyszukiwanie);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 100);
            panel1.TabIndex = 3;
            // 
            // panel2
            // 
            panel2.Controls.Add(btn_akcja);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 350);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 100);
            panel2.TabIndex = 5;
            // 
            // btn_akcja
            // 
            btn_akcja.Location = new Point(270, 24);
            btn_akcja.Name = "btn_akcja";
            btn_akcja.Size = new Size(232, 55);
            btn_akcja.TabIndex = 0;
            btn_akcja.Text = "Akcja (tekst dynamiczny)";
            btn_akcja.UseVisualStyleBackColor = true;
            btn_akcja.Click += btn_akcja_Click;
            // 
            // datagrid_uzytkownicy
            // 
            datagrid_uzytkownicy.AllowUserToAddRows = false;
            datagrid_uzytkownicy.AllowUserToDeleteRows = false;
            datagrid_uzytkownicy.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datagrid_uzytkownicy.BackgroundColor = SystemColors.Control;
            datagrid_uzytkownicy.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            datagrid_uzytkownicy.Dock = DockStyle.Fill;
            datagrid_uzytkownicy.Location = new Point(0, 100);
            datagrid_uzytkownicy.MultiSelect = false;
            datagrid_uzytkownicy.Name = "datagrid_uzytkownicy";
            datagrid_uzytkownicy.RowHeadersVisible = false;
            datagrid_uzytkownicy.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            datagrid_uzytkownicy.Size = new Size(800, 250);
            datagrid_uzytkownicy.TabIndex = 6;
            datagrid_uzytkownicy.CellMouseDown += datagrid_uzytkownicy_CellMouseDown;
            // 
            // ZarzadzajUprawnieniamiPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(datagrid_uzytkownicy);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "ZarzadzajUprawnieniamiPanel";
            Text = "ZarzadzajUprawnieniamiPanel";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)datagrid_uzytkownicy).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lbl_tytul_panelu;
        private TextBox textbox_wyszukiwanie;
        private Button btn_szukaj;
        private Panel panel1;
        private Panel panel2;
        private Button btn_akcja;
        private DataGridView datagrid_uzytkownicy;
    }
}