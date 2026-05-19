using Przychodnia.panele;
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
                przycisk_zarejestruj_wizyte.Visible = true;

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

            // 1. Sprawdzamy, kto jest zalogowany
            Uzytkownik zalogowany = BazaDanych.ZALOGOWANY_UZYTKOWNIK;
            bool czyAdmin = zalogowany != null && zalogowany.IdRol.Contains(1);
            bool czyRecepcja = zalogowany != null && zalogowany.IdRol.Contains(3) && !czyAdmin;

            // 2. Rozbijamy szukaną frazę na słowa (żeby zadziałało np. "Adam Nowak" albo "Łódź Piotrkowska")
            var slowaKluczowe = szukanaFraza.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // 3. Wyszukiwanie podstawowe (każde wpisane słowo musi być w danych użytkownika)
            var przefiltrowani = BazaDanych.Uzytkownicy.Where(u =>
                (czyPokazacZarchiwizowanych || !u.CzyZarchiwizowany) &&
                (slowaKluczowe.Length == 0 || slowaKluczowe.All(slowo => u.PobierzWszystkieDane().Contains(slowo)))
            ).ToList();

            // 4. Nakładanie filtra ról w zależności od tego, kto używa systemu
            if (czyRecepcja)
            {
                // Jeśli to recepcja, "na twardo" wywalamy z wyników każdego, kto nie ma roli Pacjent
                przefiltrowani = przefiltrowani.Where(u => u.IdRol.Contains(4)).ToList();
            }
            else if (czyAdmin && wybraneRoleIds.Count > 0)
            {
                // Jeśli to admin, używa zaznaczonych checkboxów
                przefiltrowani = przefiltrowani.Where(u => u.IdRol.Any(id => wybraneRoleIds.Contains(id))).ToList();
            }

            // 5. Wyświetlenie wyników
            UstawDataGrid(new BindingList<Uzytkownik>(przefiltrowani));

            if (przefiltrowani.Count == 0 && !string.IsNullOrEmpty(szukanaFraza))
            {
                MessageBox.Show("Brak wyników spełniających podane kryteria.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void przycisk_zarejestruj_wizyte_Click(object sender, EventArgs e)
        {
            // 1. Sprawdzamy, czy wybrano jakikolwiek wiersz
            if (datagrid_uzytkownicy.SelectedRows.Count == 0)
            {
                MessageBox.Show("Proszę najpierw wybrać pacjenta z listy.", "Brak wyboru", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Pobieramy obiekt użytkownika
            if (datagrid_uzytkownicy.SelectedRows[0].DataBoundItem is not Uzytkownik wybranyPacjent) return;

            // 3. Zabezpieczenie: czy to na pewno pacjent? (Zakładając, że rola 4 to Pacjent)
            if (!wybranyPacjent.IdRol.Contains(4))
            {
                MessageBox.Show("Wizytę można zaplanować wyłącznie dla pacjenta.", "Niedozwolona operacja", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4. Tworzymy i wyświetlamy okno rejestracji wizyty
            Form oknoRejestracji = new Form();
            oknoRejestracji.Text = $"Rejestracja wizyty: {wybranyPacjent.Imiona} {wybranyPacjent.Nazwisko}";
            oknoRejestracji.StartPosition = FormStartPosition.CenterScreen;
            oknoRejestracji.Size = new Size(450, 400);

            // Przekazujemy wybranego pacjenta do nowego panelu (który zaraz stworzymy)
            RejestracjaWizytyPanel panel = new RejestracjaWizytyPanel(wybranyPacjent);
            panel.Dock = DockStyle.Fill;

            oknoRejestracji.Controls.Add(panel);
            oknoRejestracji.ShowDialog();
        }
    }
}