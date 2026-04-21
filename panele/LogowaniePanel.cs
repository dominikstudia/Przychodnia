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

        private int liczbaNieudanychProb = 0;

        public LogowaniePanel()
        {
            InitializeComponent();
        }

        private void btn_zaloguj_Click(object sender, EventArgs e)
        {
            string email = textbox_login.Text;
            string haslo = textbox_haslo.Text;

            if (email == "" || haslo == "")
            {
                MessageBox.Show("Wprowadź login i hasło");
                return;
            }

            if (BazaDanych.SprobujZalogowac(email, haslo))
            {
                liczbaNieudanychProb = 0;
                ((Form1)this.FindForm()).OdblokujSystemPoZalogowaniu();
            }
            else
            {
                liczbaNieudanychProb++;

                if (liczbaNieudanychProb == 3)
                {
                    MessageBox.Show("Przekroczono limit nieudanych prób logowania");
                    Application.Exit();
                    return;
                }
                MessageBox.Show("Dane logowania są niepoprawne.");
            }
        }
    }
}
