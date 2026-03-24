using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Przychodnia
{
    public partial class ListaUzytkownikowPanel : UserControl
    {
        public ListaUzytkownikowPanel()
        {
            InitializeComponent();

            checkbox_uwzglednienie_zarchwizowanych.Checked = true;
            datagrid_uzytkownicy.DataSource = BazaDanych.Uzytkownicy;
        }

        private void StworzOknoFormularza(string naglowek, Uzytkownik? uzytkownik, bool czyTylkoOdczyt)
        {
            Form okno = new Form();
            okno.Text = naglowek;
            okno.StartPosition = FormStartPosition.CenterScreen;

            ZarzadzajUzytkownikiemPanel panel = new ZarzadzajUzytkownikiemPanel();
            panel.Dock = DockStyle.Fill;

            okno.ClientSize = new System.Drawing.Size(panel.Width, panel.Height);
            if (uzytkownik != null) panel.WypelnijDane(uzytkownik, czyTylkoOdczyt);

            okno.Controls.Add(panel);
            okno.ShowDialog();

            datagrid_uzytkownicy.DataSource = BazaDanych.Uzytkownicy;
        }

        ///

        private void btn_dodaj_Click(object sender, EventArgs e)
        {
            StworzOknoFormularza("Dodawanie nowego uzytkownika", null, false);
        }

        private void btn_szukaj_Click(object sender, EventArgs e)
        {
            string szukanaFraza = textbox_wyszukiwanie.Text.ToLower().Trim();
            bool czyPokazacZarchiwizowanych = checkbox_uwzglednienie_zarchwizowanych.Checked;

            var przefiltrowani = BazaDanych.Uzytkownicy.Where(u => (czyPokazacZarchiwizowanych || !u.CzyZarchiwizowany) && (string.IsNullOrEmpty(szukanaFraza) || u.PobierzWszystkieDane().Contains(szukanaFraza))).ToList();

            datagrid_uzytkownicy.DataSource = new BindingList<Uzytkownik>(przefiltrowani);
        }

        private void btn_archiwizuj_Click(object sender, EventArgs e)
        {
            if (datagrid_uzytkownicy.SelectedRows.Count == 0) return;
            if (datagrid_uzytkownicy.SelectedRows[0].DataBoundItem is not Uzytkownik wybrany) return;
            if (wybrany.CzyZarchiwizowany)
            {
                MessageBox.Show("Ten uzytkownik jest juz zaarchiwizowany");
                return;
            }

            var wynik = MessageBox.Show($"Czy na pewno chcesz zarchiwizować profil: {wybrany.Imiona} {wybrany.Nazwisko}?", "Potwierdzenie", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (wynik == DialogResult.Yes && BazaDanych.ZaarchiwizujUzytkownika(wybrany))
            {
                MessageBox.Show("Profil został zarchiwizowany");
                btn_szukaj_Click(null, null);
            }
        }

        private void btn_edytuj_Click(object sender, EventArgs e)
        {
            if (datagrid_uzytkownicy.SelectedRows.Count == 0) return;
            if (datagrid_uzytkownicy.SelectedRows[0].DataBoundItem is not Uzytkownik wybrany) return;
            if (wybrany.CzyZarchiwizowany)
            {
                MessageBox.Show("Ten uzytkownik jest zaarchiwizowany, wiec nie mozna edytowac jego danych");
                return;
            }

            StworzOknoFormularza("Edycja uzytkownika", wybrany, false);
        }

        private void btn_podglad_szczegolow_Click(object sender, EventArgs e)
        {
            if (datagrid_uzytkownicy.SelectedRows.Count == 0) return;
            if (datagrid_uzytkownicy.SelectedRows[0].DataBoundItem is not Uzytkownik wybrany) return;

            StworzOknoFormularza("Szczegóły", wybrany, true);
        }

    }
}
