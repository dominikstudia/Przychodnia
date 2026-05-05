using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq; // Dodane dla obsługi Where i Any
using System.Text;
using System.Windows.Forms;

namespace Przychodnia
{
    public partial class ListaUzytkownikowPanel : UserControl
    {
        public ListaUzytkownikowPanel()
        {
            InitializeComponent();

            // Przypisujemy zdarzenie Load
            this.Load += ListaUzytkownikowPanel_Load;

            // Zdarzenie zmiany checkboxa - z zabezpieczeniem IsHandleCreated
            checkedlistbox_filtr_roli.ItemCheck += (s, e) =>
            {
                if (this.IsHandleCreated)
                {
                    this.BeginInvoke((MethodInvoker)(() => btn_szukaj_Click(null, null)));
                }
            };
        }

        private void ListaUzytkownikowPanel_Load(object sender, EventArgs e)
        {
            // 1. Pobieramy role do listy
            checkedlistbox_filtr_roli.Items.Clear();
            foreach (var rola in BazaDanych.PobierzWszystkieRole())
            {
                checkedlistbox_filtr_roli.Items.Add(rola);
            }

            // 2. Sprawdzamy uprawnienia zalogowanego użytkownika
            Uzytkownik zalogowany = BazaDanych.ZALOGOWANY_UZYTKOWNIK;
            bool czyAdmin = zalogowany != null && zalogowany.IdRol.Contains(1);
            bool czyRecepcja = zalogowany != null && zalogowany.IdRol.Contains(3);

            checkbox_uwzglednienie_zarchwizowanych.Checked = true;

            // 3. Konfiguracja interfejsu na podstawie roli
            if (czyAdmin)
            {
                btn_archiwizuj.Visible = true;
                btn_dodaj.Visible = true;
                btn_edytuj.Visible = true;
                checkedlistbox_filtr_roli.Visible = true;
            }
            else if (czyRecepcja)
            {
                btn_edytuj.Visible = true;
                btn_archiwizuj.Visible = false;
                btn_dodaj.Visible = false;

                // Ukrywamy listę, ale zaznaczamy w niej Pacjentów (ID 4)
                checkedlistbox_filtr_roli.Visible = false;

                for (int i = 0; i < checkedlistbox_filtr_roli.Items.Count; i++)
                {
                    var rola = checkedlistbox_filtr_roli.Items[i] as Rola;
                    if (rola != null && rola.Id == 4)
                    {
                        checkedlistbox_filtr_roli.SetItemChecked(i, true);
                    }
                }
            }

            // 4. Pierwsze ładowanie danych
            btn_szukaj_Click(null, null);
        }

        private void UstawDataGrid(BindingList<Uzytkownik> lista)
        {
            datagrid_uzytkownicy.DataSource = lista;

            // Ukrywanie zbędnych kolumn
            string[] doUkrycia = { "Haslo", "NumerPosesji", "NumerLokalu", "KodPocztowy", "CzyMezczyzna" };
            foreach (var col in doUkrycia)
            {
                if (datagrid_uzytkownicy.Columns[col] != null)
                    datagrid_uzytkownicy.Columns[col].Visible = false;
            }

            if (datagrid_uzytkownicy.Columns["Plec"] != null)
            {
                datagrid_uzytkownicy.Columns["Plec"].HeaderText = "Płeć";
                datagrid_uzytkownicy.Columns["Plec"].DisplayIndex = 7;
            }

            if (datagrid_uzytkownicy.Columns["CzyZarchiwizowany"] != null)
                datagrid_uzytkownicy.Columns["CzyZarchiwizowany"].HeaderText = "Zarchiwizowany";

            if (datagrid_uzytkownicy.Columns["DataUrodzenia"] != null)
                datagrid_uzytkownicy.Columns["DataUrodzenia"].HeaderText = "Data urodzenia";
        }

        private void btn_szukaj_Click(object sender, EventArgs e)
        {
            string szukanaFraza = textbox_wyszukiwanie.Text.ToLower().Trim();
            bool czyPokazacZarchiwizowanych = checkbox_uwzglednienie_zarchwizowanych.Checked;

            List<int> wybraneRoleIds = new List<int>();
            foreach (Rola r in checkedlistbox_filtr_roli.CheckedItems)
            {
                wybraneRoleIds.Add(r.Id);
            }

            var przefiltrowani = BazaDanych.Uzytkownicy.Where(u =>
                (czyPokazacZarchiwizowanych || !u.CzyZarchiwizowany) &&
                (string.IsNullOrEmpty(szukanaFraza) || u.PobierzWszystkieDane().Contains(szukanaFraza)) &&
                (wybraneRoleIds.Count == 0 || u.IdRol.Any(id => wybraneRoleIds.Contains(id)))
            ).ToList();

            UstawDataGrid(new BindingList<Uzytkownik>(przefiltrowani));
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

            // Odśwież po zamknięciu okna
            btn_szukaj_Click(null, null);
        }

        private void btn_dodaj_Click(object sender, EventArgs e) =>
            StworzOknoFormularza("Dodawanie nowego uzytkownika", null, false);

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

        private void btn_podglad_szczegolow_Click(object sender, EventArgs e)
        {
            if (datagrid_uzytkownicy.SelectedRows.Count == 0) return;
            if (datagrid_uzytkownicy.SelectedRows[0].DataBoundItem is not Uzytkownik wybrany) return;

            StworzOknoFormularza("Szczegóły", wybrany, true);
        }
    }
}