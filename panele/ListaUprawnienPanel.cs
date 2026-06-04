using Przychodnia.modele;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Przychodnia
{
    public partial class ListaUprawnienPanel : UserControl
    {
        public ListaUprawnienPanel()
        {
            InitializeComponent();
            ZaladujRoleDoWidoku();
        }
        private void ZaladujRoleDoWidoku()
        {
            var role = BazaDanych.PobierzWszystkieRole();
            listbox_uprawnienia.DataSource = role;
            listbox_uprawnienia.DisplayMember = "Nazwa";
            listbox_uprawnienia.ValueMember = "Id";
        }

        private Rola PobierzZaznaczonaRole()
        {
            if (listbox_uprawnienia.SelectedItem is Rola wybranaRola)
            {
                return wybranaRola;
            }
            MessageBox.Show("Najpierw zaznacz rolę na liście!");
            return null;
        }

        private void OtworzOkno(TrybZarzadzaniaRola tryb)
        {
            Rola rola = PobierzZaznaczonaRole();
            if (rola != null)
            {

                var wszyscyAktywni = BazaDanych.Uzytkownicy.Where(u => !u.CzyZarchiwizowany);
                if (tryb == TrybZarzadzaniaRola.Nadawanie)
                {
                    bool czyKtosNieMa = wszyscyAktywni.Any(u => !u.IdRol.Contains(rola.Id));
                    if (!czyKtosNieMa)
                    {
                        MessageBox.Show("Wszyscy użytkownicy mają już te role");
                        return;
                    } 
                }
                else
                {
                    bool czyKtosNieMa = wszyscyAktywni.Any(u => u.IdRol.Contains(rola.Id));
                    if (!czyKtosNieMa)
                    {
                        MessageBox.Show("Żaden z użytkowników nie ma tej roli");
                        return;
                    }
                }

                using (var okno = new ZarzadzajUprawnieniamiPanel(rola, tryb))
                {
                    okno.ShowDialog();
                }
            }
        }

        private void btn_wyswietl_uzytkownikow_Click(object sender, EventArgs e)
        {
            OtworzOkno(TrybZarzadzaniaRola.Wyswietlanie);
        }

        private void btn_nadaj_masowo_Click(object sender, EventArgs e)
        {
            OtworzOkno(TrybZarzadzaniaRola.Nadawanie);
        }

        private void btn_zabierz_masowo_Click(object sender, EventArgs e)
        {
            OtworzOkno(TrybZarzadzaniaRola.Odbieranie);
        }
    }
}
