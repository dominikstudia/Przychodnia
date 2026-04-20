namespace Przychodnia
{
    partial class LogowaniePanel
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
            textbox_email = new TextBox();
            btn_zaloguj = new Button();
            lbl_email = new Label();
            lbl_haslo = new Label();
            textbox_haslo = new TextBox();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // textbox_email
            // 
            textbox_email.Font = new Font("Segoe UI", 14F);
            textbox_email.Location = new Point(21, 63);
            textbox_email.Name = "textbox_email";
            textbox_email.Size = new Size(481, 32);
            textbox_email.TabIndex = 0;
            // 
            // btn_zaloguj
            // 
            btn_zaloguj.Font = new Font("Segoe UI", 13F);
            btn_zaloguj.Location = new Point(21, 204);
            btn_zaloguj.Name = "btn_zaloguj";
            btn_zaloguj.Size = new Size(481, 66);
            btn_zaloguj.TabIndex = 1;
            btn_zaloguj.Text = "Zaloguj";
            btn_zaloguj.UseVisualStyleBackColor = true;
            btn_zaloguj.Click += btn_zaloguj_Click;
            // 
            // lbl_email
            // 
            lbl_email.AutoSize = true;
            lbl_email.Font = new Font("Segoe UI", 14F);
            lbl_email.Location = new Point(21, 35);
            lbl_email.Name = "lbl_email";
            lbl_email.Size = new Size(59, 25);
            lbl_email.TabIndex = 2;
            lbl_email.Text = "Login";
            lbl_email.Click += lbl_email_Click;
            // 
            // lbl_haslo
            // 
            lbl_haslo.AutoSize = true;
            lbl_haslo.Font = new Font("Segoe UI", 14F);
            lbl_haslo.Location = new Point(21, 118);
            lbl_haslo.Name = "lbl_haslo";
            lbl_haslo.Size = new Size(59, 25);
            lbl_haslo.TabIndex = 3;
            lbl_haslo.Text = "Hasło";
            // 
            // textbox_haslo
            // 
            textbox_haslo.Font = new Font("Segoe UI", 14F);
            textbox_haslo.Location = new Point(21, 146);
            textbox_haslo.Name = "textbox_haslo";
            textbox_haslo.Size = new Size(481, 32);
            textbox_haslo.TabIndex = 4;
            textbox_haslo.UseSystemPasswordChar = true;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.None;
            panel1.Controls.Add(textbox_haslo);
            panel1.Controls.Add(lbl_haslo);
            panel1.Controls.Add(lbl_email);
            panel1.Controls.Add(btn_zaloguj);
            panel1.Controls.Add(textbox_email);
            panel1.Location = new Point(137, 25);
            panel1.Name = "panel1";
            panel1.Size = new Size(517, 318);
            panel1.TabIndex = 0;
            // 
            // LogowaniePanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "LogowaniePanel";
            Size = new Size(800, 388);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox textbox_email;
        private Button btn_zaloguj;
        private Label lbl_email;
        private Label lbl_haslo;
        private TextBox textbox_haslo;
        private Panel panel1;
    }
}