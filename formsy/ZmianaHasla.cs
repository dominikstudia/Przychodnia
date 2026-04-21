using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Przychodnia.formsy
{
    public partial class ZmianaHasla : Form
    {
        public ZmianaHasla()
        {
            InitializeComponent();
        }

        private void btn_potwierdz_Click(object sender, EventArgs e)
        {
            string stareHaslo = textbox_stare_haslo.Text;
            string noweHaslo = textbox_nowe_haslo.Text;
            string powtorzoneNowe = textbox_powtorzone_nowe_haslo.Text;

            if (stareHaslo == "" || noweHaslo == "" || powtorzoneNowe == "")
            {
                MessageBox.Show("Wprowadź dane do wszystkich pól");
                return;
            }

            if (stareHaslo != BazaDanych.ZALOGOWANY_UZYTKOWNIK.Haslo)
            {
                MessageBox.Show("Stare hasło jest nieprawidłowe");
                return;
            }
            
            if (stareHaslo == noweHaslo)
            {
                MessageBox.Show("Stare haslo nie moze byc takie same jak nowe haslo!");
                return;
            }

            if (noweHaslo != powtorzoneNowe)
            {
                MessageBox.Show("Pola nowych hasel nie są takie same");
                return;
            }

            if (!Regex.IsMatch(noweHaslo, RegexPatterny.HASLO))
            {
                MessageBox.Show("Hasło nie spełnia wymagań bezpieczeństwa.");
                return;
            }

            BazaDanych.ZALOGOWANY_UZYTKOWNIK.Haslo = noweHaslo;
            if (BazaDanych.DodajLubZaaktualizujUzytkownika(BazaDanych.ZALOGOWANY_UZYTKOWNIK))
            {
                MessageBox.Show("Hasło zostało pomyślnie zmienione.");
                this.Close();
            }
        }

        private void btn_anuluj_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
