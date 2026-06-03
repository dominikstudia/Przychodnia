using System;
using System.Windows.Forms;

namespace Przychodnia
{
    public partial class EdycjaPacjentaPanel : UserControl
    {
        private Uzytkownik _pacjent;

        public EdycjaPacjentaPanel(Uzytkownik pacjent)
        {
            InitializeComponent();
            _pacjent = pacjent;

            // Automatyczne podpięcie zabezpieczeń wpisywania znaków
            textbox_imie.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.POLSKIE_LITERY_SPACJA, e);
            textbox_nazwisko.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.POLSKIE_LITERY_MYSLNIK, e);
            textbox_email.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.LITERY_LICZBY_PODLOGA_KROPKA_MYSLNIK_PODLOGA, e);
            textbox_miejscowosc.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.POLSKIE_LITERY_SPACJA_MYSLNIK, e);
            textbox_ulica.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.POLSKIE_LITERY_SPACJA_MYSLNIK, e);
            textbox_nrDomu.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.LICZBY_LITERY_UKOSNIK, e);
            textbox_nrMieszkania.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.LICZBY_LITERY_UKOSNIK, e);

            WypelnijDane();
        }

        private void WypelnijDane()
        {
            textbox_imie.Text = _pacjent.Imiona;
            textbox_nazwisko.Text = _pacjent.Nazwisko;
            textbox_pesel.Text = _pacjent.Pesel;

            if (_pacjent.CzyMezczyzna)
                comboBox_plec.SelectedItem = "Mężczyzna";
            else
                comboBox_plec.SelectedItem = "Kobieta";

            dateTimePicker_dataUrodzenia.Value = _pacjent.DataUrodzenia;
            textbox_telefon.Text = _pacjent.Telefon;
            textbox_email.Text = _pacjent.Email;
            textbox_miejscowosc.Text = _pacjent.Miejscowosc;
            textbox_kodPocztowy.Text = _pacjent.KodPocztowy;
            textbox_ulica.Text = _pacjent.Ulica;
            textbox_nrDomu.Text = _pacjent.NumerPosesji;
            textbox_nrMieszkania.Text = _pacjent.NumerLokalu;
        }

        private void btnZarejestruj_Click(object sender, EventArgs e)
        {
            // 1. Walidacja PÓL WYMAGANYCH
            if (string.IsNullOrWhiteSpace(textbox_imie.Text) ||
                string.IsNullOrWhiteSpace(textbox_nazwisko.Text) ||
                string.IsNullOrWhiteSpace(textbox_pesel.Text) ||
                string.IsNullOrWhiteSpace(textbox_miejscowosc.Text) ||
                string.IsNullOrWhiteSpace(textbox_ulica.Text) ||
                string.IsNullOrWhiteSpace(textbox_kodPocztowy.Text.Replace("-", "").Replace("_", "").Replace(" ", "")) ||
                string.IsNullOrWhiteSpace(textbox_nrDomu.Text) ||
                string.IsNullOrWhiteSpace(textbox_telefon.Text) ||
                string.IsNullOrWhiteSpace(textbox_email.Text) ||
                comboBox_plec.SelectedIndex == -1)
            {
                MessageBox.Show("Pole jest wymagane", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string pesel = textbox_pesel.Text.Trim();
            string email = textbox_email.Text.Trim();
            bool czyMezczyzna = (comboBox_plec.Text == "Mężczyzna");

            // 2. Walidacja logiki PESEL
            var walidacjaPesel = Narzedzia.SprawdzPesel(pesel, dateTimePicker_dataUrodzenia.Value, czyMezczyzna);
            if (!walidacjaPesel.Poprawny)
            {
                MessageBox.Show(walidacjaPesel.Komunikat, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Walidacja logiki Telefonu i Emaila
            var walidacjaTel = Narzedzia.SprawdzTelefon(textbox_telefon.Text.Trim());
            if (!walidacjaTel.Poprawny)
            {
                MessageBox.Show(walidacjaTel.Komunikat, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                var walidacjaEmail = Narzedzia.SprawdzEmail(email);
                if (!walidacjaEmail.Poprawny)
                {
                    MessageBox.Show(walidacjaEmail.Komunikat, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // 4. Unikalność w Bazie
            if (BazaDanych.CzyPeselZajety(pesel, _pacjent.Id))
            {
                MessageBox.Show("Pacjent o takich danych PESEL jest już zarejestrowany.", "Błąd unikalności", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrWhiteSpace(email) && BazaDanych.CzyEmailZajety(email, _pacjent.Id))
            {
                MessageBox.Show("Pacjent o takich danych E-mail jest już zarejestrowany.", "Błąd unikalności", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ======== WSZYSTKO POPRAWNE - AKTUALIZUJEMY ========

            _pacjent.Imiona = textbox_imie.Text.Trim();
            _pacjent.Nazwisko = textbox_nazwisko.Text.Trim();
            _pacjent.Pesel = pesel;
            _pacjent.DataUrodzenia = dateTimePicker_dataUrodzenia.Value;
            _pacjent.CzyMezczyzna = czyMezczyzna;
            _pacjent.Telefon = textbox_telefon.Text.Trim();
            _pacjent.Email = email;
            _pacjent.Miejscowosc = textbox_miejscowosc.Text.Trim();
            _pacjent.KodPocztowy = textbox_kodPocztowy.Text.Trim();
            _pacjent.Ulica = textbox_ulica.Text.Trim();
            _pacjent.NumerPosesji = textbox_nrDomu.Text.Trim();
            _pacjent.NumerLokalu = textbox_nrMieszkania.Text.Trim();

            // Zapis do bazy
            if (BazaDanych.AktualizujDanePacjenta(_pacjent))
            {
                MessageBox.Show("Dane pacjenta zostały zaktualizowane pomyślnie.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ZamknijPanel();
            }
        }

        private void btnAnuluj_Click(object sender, EventArgs e)
        {
            ZamknijPanel();
        }

        private void ZamknijPanel()
        {
            Form okno = this.FindForm();
            if (okno != null) okno.Close();
        }

    }
}