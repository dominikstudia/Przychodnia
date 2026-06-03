using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Przychodnia
{
    public partial class ZarzadzajPacjentemPanel : UserControl
    {
        private Uzytkownik _pacjent;
        public ZarzadzajPacjentemPanel(Uzytkownik pacjent = null)
        {
            _pacjent = pacjent;

            InitializeComponent();

            // Automatyczne podpięcie zabezpieczeń wpisywania znaków
            textbox_imie.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.POLSKIE_LITERY_SPACJA, e);
            textbox_nazwisko.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.POLSKIE_LITERY_MYSLNIK, e);
            textbox_email.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.LITERY_LICZBY_PODLOGA_KROPKA_MYSLNIK_PODLOGA, e);
            textbox_miejscowosc.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.POLSKIE_LITERY_SPACJA_MYSLNIK, e);
            textbox_ulica.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.POLSKIE_LITERY_SPACJA_MYSLNIK, e);
            textbox_nrDomu.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.LICZBY_LITERY_UKOSNIK, e);
            textbox_nrMieszkania.KeyPress += (s, e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.LICZBY_LITERY_UKOSNIK, e);

            if (_pacjent != null)
            {
                WypelnijDane();
                btnZarejestruj.Text = "Zapisz zmiany";
            }
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

            // Pobranie płci
            bool czyMezczyzna = (comboBox_plec.Text == "Mężczyzna");

            // 2. Walidacja logiki PESEL
            var walidacjaPesel = Narzedzia.SprawdzPesel(pesel, dateTimePicker_dataUrodzenia.Value, czyMezczyzna, false);
            if (!walidacjaPesel.Poprawny)
            {
                MessageBox.Show(walidacjaPesel.Komunikat, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Walidacja logiki Telefonu i Emaila
            // Poprawiono - nie ma potrzeby sprawdzania bo jest maska, komunikat 1;1 wziety ten sam
            if (string.IsNullOrWhiteSpace(textbox_telefon.Text.Trim()))
            {
                MessageBox.Show("Pole jest wymagane", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Poprawiono - nie ma potrzeby robic na to metody, choc nie rozumiem [ale wzialem 1;1 ten sam kod, zeby nie bylo niespojnosci] czemu jest blad jak jest email > 25 znakow
            if (!string.IsNullOrWhiteSpace(email) && (email.Length > 25 || !Regex.IsMatch(email, RegexPatterny.WALIDATOR_EMAIL))) {
                MessageBox.Show("Nieprawidłowy format adresu e-mail", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idUzytkownika = _pacjent != null ? _pacjent.Id : 0;
            if (BazaDanych.CzyPeselZajety(pesel, idUzytkownika))
            {
                MessageBox.Show("Pacjent o takich danych PESEL jest już zarejestrowany.", "Błąd unikalności", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrWhiteSpace(email) && BazaDanych.CzyEmailZajety(email, idUzytkownika))
            {
                MessageBox.Show("Pacjent o takich danych E-mail jest już zarejestrowany.", "Błąd unikalności", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ======== ZAPIS ========

            Uzytkownik pacjentDoZapisu = _pacjent ?? new Uzytkownik();

            pacjentDoZapisu.Imiona = textbox_imie.Text.Trim();
            pacjentDoZapisu.Nazwisko = textbox_nazwisko.Text.Trim();
            pacjentDoZapisu.Pesel = pesel;
            pacjentDoZapisu.DataUrodzenia = dateTimePicker_dataUrodzenia.Value;
            pacjentDoZapisu.CzyMezczyzna = czyMezczyzna;
            pacjentDoZapisu.Telefon = textbox_telefon.Text.Trim();
            pacjentDoZapisu.Email = email;
            pacjentDoZapisu.Miejscowosc = textbox_miejscowosc.Text.Trim();
            pacjentDoZapisu.KodPocztowy = textbox_kodPocztowy.Text.Trim();
            pacjentDoZapisu.Ulica = textbox_ulica.Text.Trim();
            pacjentDoZapisu.NumerPosesji = textbox_nrDomu.Text.Trim();
            pacjentDoZapisu.NumerLokalu = textbox_nrMieszkania.Text.Trim();

            if (_pacjent != null)
            {
                if (BazaDanych.DodajLubZaaktualizujUzytkownika(pacjentDoZapisu))
                {
                    MessageBox.Show("Dane pacjenta zostały zaktualizowane pomyślnie.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ZamknijPanel();
                }
                return;
            }
            string noweHaslo = Narzedzia.GenerujSilneHaslo();

            pacjentDoZapisu.Id = 0;
            pacjentDoZapisu.Login = "pacjent_" + pesel;
            pacjentDoZapisu.Haslo = noweHaslo;
            pacjentDoZapisu.IdRol.Add(Role.ZdobadzIdRoli(Role.PACJENT));

            if (BazaDanych.DodajLubZaaktualizujUzytkownika(pacjentDoZapisu))
            {
                MessageBox.Show($"Dodano pacjenta\n\nLogin: {pacjentDoZapisu.Login}\nHasło: {noweHaslo}\n\n(Hasło zostało skopiowane do schowka).",
                                "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                WyczyscPola();
            }
        }

        private void btnAnuluj_Click(object sender, EventArgs e)
        {
            ZamknijPanel();
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

        private void ZamknijPanel()
        {
            Form okno = this.FindForm();
            if (okno != null) okno.Close();
        }
    }
}