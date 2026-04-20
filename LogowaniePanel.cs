using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Przychodnia
{
    public partial class LogowaniePanel : UserControl
    {
        public LogowaniePanel()
        {
            InitializeComponent();
        }

        private void btn_zaloguj_Click(object sender, EventArgs e)
        {
            string email = textbox_email.Text;
            string haslo = textbox_haslo.Text;

            if (BazaDanych.SprobujZalogowac(email, haslo))
            {
                Form1 glowneOkno = (Form1)this.FindForm();
                glowneOkno.OdblokujSystemPoZalogowaniu();
            }
            else
            {
                MessageBox.Show("Dane logowania są niepoprawne.");
            }
        }

        private void lbl_email_Click(object sender, EventArgs e)
        {

        }
    }
}
