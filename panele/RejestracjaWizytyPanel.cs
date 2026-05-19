using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Przychodnia.panele
{
    public partial class RejestracjaWizytyPanel : UserControl
    {
        private Uzytkownik _pacjent;

        public RejestracjaWizytyPanel(Uzytkownik pacjent)
        {
            InitializeComponent();
            _pacjent = pacjent;
            this.Load += RejestracjaWizytyPanel_Load;

            // Reagowanie na zmianę specjalizacji
            combobox_specjalizacja.SelectedIndexChanged += Combobox_specjalizacja_SelectedIndexChanged;

            // !!! DOPISZ TĘ LINIJKĘ, ABY POWIĄZAĆ PRZYCISK ZE ZDARZENIEM !!!
            btn_zapisz_wizyte.Click += btn_zapisz_wizyte_Click;
        }

        private void RejestracjaWizytyPanel_Load(object sender, EventArgs e)
        {
            // Wymaganie niefunkcjonalne: Interfejs musi wymuszać wprowadzanie daty wyłącznie za pomocą kalendarza [cite: 2]
            datepicker_data_wizyty.MinDate = DateTime.Today; // Blokada dat wstecznych (Scenariusz E1) [cite: 2]
            datepicker_data_wizyty.Format = DateTimePickerFormat.Custom;
            datepicker_data_wizyty.CustomFormat = "dd.MM.yyyy HH:mm";

            // Ładowanie specjalizacji z bazy danych [cite: 2]
            combobox_specjalizacja.DataSource = BazaDanych.PobierzSpecjalizacje();
            combobox_specjalizacja.SelectedIndex = -1; // Ustawia na puste, dopóki użytkownik czegoś nie wybierze

            // Pobranie dostępnych gabinetów z bazy danych [cite: 2]
            combobox_gabinet.DataSource = BazaDanych.PobierzGabinety();
            combobox_gabinet.SelectedIndex = -1;

            // Blokujemy ComboBox lekarza, dopóki nie zostanie wybrana specjalizacja [cite: 2]
            combobox_lekarz.Enabled = false;
        }

        private void Combobox_specjalizacja_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Wymaganie funkcjonalne: Pola wyboru są dynamicznie zależne od siebie [cite: 2]
            if (combobox_specjalizacja.SelectedItem != null && combobox_specjalizacja.SelectedIndex != -1)
            {
                string wybranaSpecjalizacja = combobox_specjalizacja.SelectedItem.ToString();

                // Pobieramy przefiltrowaną listę lekarzy [cite: 2]
                List<Uzytkownik> lekarze = BazaDanych.PobierzLekarzyPoSpecjalizacji(wybranaSpecjalizacja);

                // Konfigurujemy sposób wyświetlania lekarza w ComboBox
                combobox_lekarz.DataSource = lekarze;
                combobox_lekarz.DisplayMember = "PelneNazwisko"; // Zmień na "Nazwisko" lub dodaj właściwość w klasie Uzytkownik, jeśli nie masz pełnego imienia i nazwiska razem
                combobox_lekarz.ValueMember = "Id";

                combobox_lekarz.Enabled = true;
                combobox_lekarz.SelectedIndex = -1;
            }
            else
            {
                combobox_lekarz.Enabled = false;
                combobox_lekarz.DataSource = null;
            }
        }

        private void btn_zapisz_wizyte_Click(object sender, EventArgs e)
        {
            // 1. Pobranie danych z kontrolek
            DateTime dataWizyty = datepicker_data_wizyty.Value;
            var wybranyLekarz = combobox_lekarz.SelectedItem as Uzytkownik;
            string wybranyGabinet = combobox_gabinet.SelectedItem?.ToString();

            // Walidacja podstawowa formularza
            if (_pacjent == null)
            {
                MessageBox.Show("Nie wybrano poprawnego pacjenta.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (wybranyLekarz == null)
            {
                MessageBox.Show("Proszę wybrać lekarza.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(wybranyGabinet))
            {
                MessageBox.Show("Proszę wybrać gabinet.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Wywołanie metody biznesowej z bazy danych, która weryfikuje E1, E2, E3 oraz dokonuje zapisu [cite: 2]
            var wynik = BazaDanych.ZarejestrujWizyte(_pacjent.Id, wybranyLekarz.Id, wybranyGabinet, dataWizyty);

            if (wynik.Sukces)
            {
                // Sukces — system zapisał rekord i nadał status „Zarejestrowana” [cite: 2]
                MessageBox.Show(wynik.Komunikat, "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Zamknięcie okna po sukcesie
                this.ParentForm?.Close();
            }
            else
            {
                // Obsługa wyjątków biznesowych (E1, E2, E3) bezpośrednio zwracanych przez bazę danych [cite: 2]
                MessageBox.Show(wynik.Komunikat, "Błąd walidacji", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}