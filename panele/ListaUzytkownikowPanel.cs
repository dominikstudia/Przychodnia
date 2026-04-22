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

            var roleDoFiltru = BazaDanych.PobierzWszystkieRole();
            roleDoFiltru.Insert(0, new Rola { Id = 0, Nazwa = "Wszystkie role" });
            combobox_filtr_roli.DataSource = roleDoFiltru;

            combobox_filtr_roli.SelectedIndexChanged += btn_szukaj_Click;

            checkbox_uwzglednienie_zarchwizowanych.Checked = true;
            UstawDataGrid(BazaDanych.Uzytkownicy);

            Uzytkownik zalogowany = BazaDanych.ZALOGOWANY_UZYTKOWNIK;
            if (zalogowany != null && zalogowany.SprawdzCzyMaRole("Administrator"))
            {
                btn_archiwizuj.Visible = true;
                btn_dodaj.Visible = true;
                btn_edytuj.Visible = true;
            }
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

            UstawDataGrid(BazaDanych.Uzytkownicy);
        }

        private void UstawDataGrid(BindingList<Uzytkownik> lista)
        {
            datagrid_uzytkownicy.DataSource = lista;
            datagrid_uzytkownicy.Columns["Haslo"].Visible = false;
            datagrid_uzytkownicy.Columns["NumerPosesji"].Visible = false;
            datagrid_uzytkownicy.Columns["NumerLokalu"].Visible = false;
            datagrid_uzytkownicy.Columns["KodPocztowy"].Visible = false;
            datagrid_uzytkownicy.Columns["CzyMezczyzna"].Visible = false;

            datagrid_uzytkownicy.Columns["CzyZarchiwizowany"].HeaderText = "Zarchiwizowany";
            datagrid_uzytkownicy.Columns["DataUrodzenia"].HeaderText = "Data urodzenia";

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

            int wybraneUprawnienieId = 0;
            if (combobox_filtr_roli.SelectedItem is Rola r)
            {
                wybraneUprawnienieId = r.Id;
            }

            var przefiltrowani = BazaDanych.Uzytkownicy.Where(u =>
                (czyPokazacZarchiwizowanych || !u.CzyZarchiwizowany) &&
                (string.IsNullOrEmpty(szukanaFraza) || u.PobierzWszystkieDane().Contains(szukanaFraza)) &&
                (wybraneUprawnienieId == 0 || u.IdRol.Contains(wybraneUprawnienieId))
            ).ToList();

            UstawDataGrid(new BindingList<Uzytkownik>(przefiltrowani));

            if (przefiltrowani.Count == 0 && wybraneUprawnienieId != 0)
            {
                MessageBox.Show("Brak użytkowników posiadających to uprawnienie", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
