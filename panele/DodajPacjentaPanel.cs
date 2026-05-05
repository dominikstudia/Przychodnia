using System;
using System.Windows.Forms;

namespace Przychodnia
{
    public partial class DodajPacjentaPanel : UserControl
    {
        public DodajPacjentaPanel()
        {
            InitializeComponent();

            // Automatyczne podpięcie zabezpieczeń wpisywania znaków
            textbox_imie.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.POLSKIE_LITERY_SPACJA, e);
            textbox_nazwisko.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.POLSKIE_LITERY_MYSLNIK, e);
            textbox_email.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.LITERY_LICZBY_PODLOGA_KROPKA_MYSLNIK_PODLOGA, e);
            textbox_miejscowosc.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.POLSKIE_LITERY_SPACJA_MYSLNIK, e);
            textbox_ulica.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.POLSKIE_LITERY_SPACJA_MYSLNIK, e);
            textbox_nrDomu.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.LICZBY_LITERY_UKOSNIK, e);
            textbox_nrMieszkania.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.LICZBY_LITERY_UKOSNIK, e);
        }

        private void btnZarejestruj_Click(object sender, EventArgs e)
        {
            // 1. Walidacja PÓL WYMAGANYCH
            if (string.IsNullOrWhiteSpace(textbox_imie.Text) ||
                string.IsNullOrWhiteSpace(textbox_nazwisko.Text) ||
                string.IsNullOrWhiteSpace(textbox_pesel.Text) ||
                string.IsNullOrWhiteSpace(textbox_miejscowosc.Text) ||
                string.IsNullOrWhiteSpace(textbox_kodPocztowy.Text) ||
                string.IsNullOrWhiteSpace(textbox_nrDomu.Text) ||
                string.IsNullOrWhiteSpace(textbox_telefon.Text) ||
                comboBox_plec.SelectedIndex == -1)
            {
                MessageBox.Show("Pole jest wymagane", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string pesel = textbox_pesel.Text.Trim();
            string email = textbox_email.Text.Trim();

            // Pobranie płci
            bool czyMezczyzna = (comboBox_plec.Text == "Mężczyzna");

            // 2. Walidacja logiki PESEL
            var walidacjaPesel = Walidator.SprawdzPesel(pesel, dateTimePicker_dataUrodzenia.Value, czyMezczyzna);
            if (!walidacjaPesel.Poprawny)
            {
                MessageBox.Show(walidacjaPesel.Komunikat, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Walidacja logiki Telefonu i Emaila
            var walidacjaTel = Walidator.SprawdzTelefon(textbox_telefon.Text.Trim());
            if (!walidacjaTel.Poprawny)
            {
                MessageBox.Show(walidacjaTel.Komunikat, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var walidacjaEmail = Walidator.SprawdzEmail(email);
            if (!walidacjaEmail.Poprawny)
            {
                MessageBox.Show(walidacjaEmail.Komunikat, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 4. Unikalność w Bazie
            if (BazaDanych.CzyPeselZajety(pesel, 0))
            {
                MessageBox.Show("Pacjent o takich danych PESEL jest już zarejestrowany", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrWhiteSpace(email) && BazaDanych.CzyEmailZajety(email, 0))
            {
                MessageBox.Show("Pacjent o takich danych E-mail jest już zarejestrowany", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ======== WSZYSTKO POPRAWNE - ZAPISUJEMY ========

            // Korzystamy z metody do wygenerowania bezpiecznego hasła dla nowego pacjenta
            string noweHaslo = BazaDanych.GenerujSilneHaslo();

            Uzytkownik nowyPacjent = new Uzytkownik
            {
                Id = 0,
                Imiona = textbox_imie.Text.Trim(),
                Nazwisko = textbox_nazwisko.Text.Trim(),
                Pesel = pesel,
                DataUrodzenia = dateTimePicker_dataUrodzenia.Value,
                CzyMezczyzna = czyMezczyzna,
                Telefon = textbox_telefon.Text.Trim(),
                Email = email,
                Miejscowosc = textbox_miejscowosc.Text.Trim(),
                KodPocztowy = textbox_kodPocztowy.Text.Trim(),
                Ulica = textbox_ulica.Text.Trim(),
                NumerPosesji = textbox_nrDomu.Text.Trim(),
                NumerLokalu = textbox_nrMieszkania.Text.Trim(),

                Login = "pacjent_" + pesel,
                Haslo = noweHaslo
            };

            nowyPacjent.IdRol.Add(4);

            if (BazaDanych.DodajLubZaaktualizujUzytkownika(nowyPacjent))
            {
                MessageBox.Show($"Dodano pacjenta\n\nLogin: {nowyPacjent.Login}\nHasło: {noweHaslo}\n\n(Hasło zostało skopiowane do schowka).",
                                "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                WyczyscPola();
            }
        }

        private void btnAnuluj_Click(object sender, EventArgs e)
        {
            var wynik = MessageBox.Show("Czy na pewno chcesz przerwać rejestrację? Wprowadzone dane zostaną utracone.",
                                        "Potwierdzenie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (wynik == DialogResult.Yes)
            {
                WyczyscPola();
            }
        }

        private void WyczyscPola()
        {
            textbox_imie.Clear();
            textbox_nazwisko.Clear();
            textbox_pesel.Clear();
            textbox_telefon.Clear();
            textbox_email.Clear();
            textbox_miejscowosc.Clear();
            textbox_kodPocztowy.Clear();
            textbox_ulica.Clear();
            textbox_nrDomu.Clear();
            textbox_nrMieszkania.Clear();

            comboBox_plec.SelectedIndex = -1;
            dateTimePicker_dataUrodzenia.Value = DateTime.Now;
        }
    }
}