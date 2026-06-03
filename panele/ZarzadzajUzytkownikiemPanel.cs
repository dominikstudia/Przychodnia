using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Przychodnia
{
    public partial class ZarzadzajUzytkownikiemPanel : UserControl
    {
        private Uzytkownik _uzytkownik;

        public ZarzadzajUzytkownikiemPanel()
        {
            InitializeComponent();

            foreach (var rola in BazaDanych.PobierzWszystkieRole())
            {
                checkedlistbox_uprawnienia.Items.Add(rola);
            }

            bool czyAdmin = Role.SprawdzCzyMaRole(BazaDanych.ZALOGOWANY_UZYTKOWNIK, Role.ADMINISTRATOR);
            bool czyRecepcja = Role.SprawdzCzyMaRole(BazaDanych.ZALOGOWANY_UZYTKOWNIK, Role.RECEPCJONISTA);

            // 1. Logika uprawnień do widoku ról
            if (!czyAdmin)
            {
                checkedlistbox_uprawnienia.Visible = false;
                label_uprawnienia.Visible = false;
            }

            // 2. Blokada pól wrażliwych dla Recepcji (już na etapie tworzenia panelu)
            if (czyRecepcja)
            {
                textbox_login.ReadOnly = true;
                textbox_haslo.Enabled = false; // Recepcja nie może nawet kliknąć w hasło
                btn_wygeneruj.Visible = false;
            }
        }

        public void WypelnijDane(Uzytkownik uzytkownik, bool czyTylkoOdczyt)
        {
            _uzytkownik = uzytkownik;

            textbox_login.Text = uzytkownik.Login;
            textbox_email.Text = uzytkownik.Email;
            textbox_imiona.Text = uzytkownik.Imiona;
            textbox_nazwisko.Text = uzytkownik.Nazwisko;
            textbox_pesel.Text = uzytkownik.Pesel;
            combobox_plec.SelectedItem = (uzytkownik.CzyMezczyzna) ? "Mężczyzna" : "Kobieta";
            datetimerpicker_data_urodzenia.Value = uzytkownik.DataUrodzenia;
            textbox_numer_telefonu.Text = uzytkownik.Telefon;
            textbox_miejscowosc.Text = uzytkownik.Miejscowosc;
            textbox_ulica.Text = uzytkownik.Ulica;
            textbox_kod_pocztowy.Text = uzytkownik.KodPocztowy;
            textbox_numer_posesji.Text = uzytkownik.NumerPosesji;
            textbox_numer_lokalu.Text = uzytkownik.NumerLokalu;

            for (int i = 0; i < checkedlistbox_uprawnienia.Items.Count; i++)
            {
                var rola = (Rola)checkedlistbox_uprawnienia.Items[i];
                checkedlistbox_uprawnienia.SetItemChecked(i, uzytkownik.IdRol.Contains(rola.Id));
            }

            // Obsługa trybu "Tylko do odczytu" (podgląd)
            if (czyTylkoOdczyt)
            {
                ZablokujWszystkiePola(this.Controls);
                btn_wygeneruj.Visible = false;
                btn_anuluj.Text = "Powrót";
                btn_potwierdz.Visible = false;
            }
            else
            {
                // Jeśli to EDYCJA, a zalogowana jest Recepcja, upewniamy się, że login i hasło są zablokowane
                bool czyRecepcja = Role.SprawdzCzyMaRole(BazaDanych.ZALOGOWANY_UZYTKOWNIK, Role.RECEPCJONISTA);
                if (czyRecepcja)
                {
                    textbox_login.ReadOnly = true;
                    textbox_haslo.Enabled = false;
                    btn_wygeneruj.Visible = false;
                }
            }
        }

        private void ZablokujWszystkiePola(ControlCollection lista)
        {
            foreach (Control c in lista)
            {
                if (c is TextBox t) t.ReadOnly = true;
                if (c is ComboBox cb) cb.Enabled = false;
                if (c is MaskedTextBox m) m.Enabled = false;
                if (c is DateTimePicker dtp) dtp.Enabled = false;
                if (c is CheckedListBox clb) clb.Enabled = false;
                if (c.HasChildren) ZablokujWszystkiePola(c.Controls);
            }
        }

        private void btn_potwierdz_Click(object sender, EventArgs e)
        {
            bool czyAdmin = Role.SprawdzCzyMaRole(BazaDanych.ZALOGOWANY_UZYTKOWNIK, Role.ADMINISTRATOR);
            bool czyRecepcja = Role.SprawdzCzyMaRole(BazaDanych.ZALOGOWANY_UZYTKOWNIK, Role.RECEPCJONISTA);

            // Walidacja pól wymaganych
            var wymaganePolaTekstowe = new Dictionary<Control, Label>()
            {
                {textbox_login, label_login}, {textbox_email, label_adres_email}, {textbox_imiona, label_imiona},
                {textbox_nazwisko, label_nazwisko}, {textbox_pesel, label_numer_pesel}, {combobox_plec, label_plec},
                {textbox_numer_telefonu, label_numer_telefonu}, {textbox_miejscowosc, label_miejscowosc},
                {textbox_ulica, label_ulica}, {textbox_kod_pocztowy, label_kod_pocztowy}
            };

            foreach (var pole in wymaganePolaTekstowe)
            {
                if ((pole.Key is TextBox t && string.IsNullOrWhiteSpace(t.Text)) ||
                    (pole.Key is MaskedTextBox m && !m.MaskFull) ||
                    (pole.Key is ComboBox c && c.SelectedIndex == -1))
                {
                    MessageBox.Show($"Pole '{pole.Value.Text}' jest wymagane!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    pole.Key.Focus();
                    return;
                }
            }

            // Walidacja email
            if (!Regex.IsMatch(textbox_email.Text, RegexPatterny.WALIDATOR_EMAIL))
            {
                MessageBox.Show("Wprowadziłeś niepoprawny format emaila");
                return;
            }

            // Walidacja hasła (tylko jeśli pole jest aktywne i coś wpisano)
            if (textbox_haslo.Enabled && !string.IsNullOrEmpty(textbox_haslo.Text))
            {
                string loginUzytkownika = textbox_login.Text.Trim();
                string wpisaneHaslo = textbox_haslo.Text;

                var walidacja = Narzedzia.SprawdzSileHasla(wpisaneHaslo, loginUzytkownika);
                if (walidacja.CzySaBledy)
                {
                    MessageBox.Show(walidacja.Komunikat, "Hasło zbyt słabe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_uzytkownik != null && BazaDanych.CzyHasloByloUzyteOstatnio(_uzytkownik.Id, wpisaneHaslo))
                {
                    MessageBox.Show("Nie możesz ustawić hasła, którego ten użytkownik używał ostatnio (historia 3 haseł).", "Błąd historii haseł", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var walidacjaPesel = Narzedzia.SprawdzPesel(textbox_pesel.Text, datetimerpicker_data_urodzenia.Value, combobox_plec.SelectedItem.ToString() == "Mężczyzna", true);
            if (!walidacjaPesel.Poprawny)
            {
                MessageBox.Show(walidacjaPesel.Komunikat, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mapowanie danych na obiekt
            Uzytkownik uzytkownik = _uzytkownik ?? new Uzytkownik { Id = -1 };

            // Ochrona danych dla recepcji: jeśli to edycja i robi to recepcja, nie zmieniamy loginu
            if (czyRecepcja && _uzytkownik != null)
            {
                uzytkownik.Login = _uzytkownik.Login; // Przywracamy oryginał na wszelki wypadek
            }
            else
            {
                uzytkownik.Login = textbox_login.Text;
            }

            uzytkownik.Imiona = textbox_imiona.Text;
            uzytkownik.Nazwisko = textbox_nazwisko.Text;
            uzytkownik.Email = textbox_email.Text;
            uzytkownik.Pesel = textbox_pesel.Text;
            uzytkownik.CzyMezczyzna = combobox_plec.Text == "Mężczyzna";
            uzytkownik.DataUrodzenia = datetimerpicker_data_urodzenia.Value;
            uzytkownik.Telefon = textbox_numer_telefonu.Text.Replace("-", "").Replace(" ", "").Trim();
            uzytkownik.Miejscowosc = textbox_miejscowosc.Text;
            uzytkownik.Ulica = textbox_ulica.Text;
            uzytkownik.KodPocztowy = textbox_kod_pocztowy.Text;
            uzytkownik.NumerPosesji = textbox_numer_posesji.Text;
            uzytkownik.NumerLokalu = textbox_numer_lokalu.Text;

            // Hasło zmieniamy tylko jeśli pole było aktywne i coś wpisano
            if (textbox_haslo.Enabled && !string.IsNullOrEmpty(textbox_haslo.Text))
            {
                uzytkownik.Haslo = textbox_haslo.Text;
            }

            // Tylko Admin zarządza rolami
            if (czyAdmin)
            {
                uzytkownik.IdRol.Clear();
                foreach (Rola r in checkedlistbox_uprawnienia.CheckedItems)
                {
                    uzytkownik.IdRol.Add(r.Id);
                }

                if (uzytkownik.IdRol.Count == 0 && !uzytkownik.CzyZarchiwizowany)
                {
                    MessageBox.Show("Użytkownik musi mieć co najmniej jedno uprawnienie!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (BazaDanych.DodajLubZaaktualizujUzytkownika(uzytkownik))
            {
                MessageBox.Show(_uzytkownik == null ? "Pomyślnie dodano użytkownika" : "Pomyślnie zaaktualizowano użytkownika");
                btn_anuluj_Click(null, null);
            }
        }

        private void btn_wygeneruj_Click(object sender, EventArgs e)
        {
            string noweHaslo = Narzedzia.GenerujSilneHaslo();
            textbox_haslo.Text = noweHaslo;
            MessageBox.Show("Wygenerowano nowe, bezpieczne hasło.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_anuluj_Click(object sender, EventArgs e)
        {
            Form okno = this.FindForm();
            if (okno != null && okno.Name != "Form1") okno.Close();
            else this.Parent?.Controls.Remove(this);
        }

        private void textbox_pesel_Click(object sender, EventArgs e) => textbox_pesel.SelectionStart = 0;
        private void textbox_numer_telefonu_Click(object sender, EventArgs e) => textbox_numer_telefonu.SelectionStart = 0;
        private void textbox_kod_pocztowy_Click(object sender, EventArgs e) => textbox_kod_pocztowy.SelectionStart = 0;
        private void textbox_login_KeyPress(object sender, KeyPressEventArgs e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.MALE_LITERY_I_PODLOGA, e);
        private void textbox_email_KeyPress(object sender, KeyPressEventArgs e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.LITERY_LICZBY_PODLOGA_KROPKA_MYSLNIK_PODLOGA, e);
        private void textbox_imiona_KeyPress(object sender, KeyPressEventArgs e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.POLSKIE_LITERY_SPACJA, e);
        private void textbox_nazwisko_KeyPress(object sender, KeyPressEventArgs e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.POLSKIE_LITERY_MYSLNIK, e);
        private void textbox_miejscowosc_KeyPress(object sender, KeyPressEventArgs e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.POLSKIE_LITERY_SPACJA_MYSLNIK, e);
        private void textbox_ulica_KeyPress(object sender, KeyPressEventArgs e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.POLSKIE_LITERY_SPACJA_MYSLNIK, e);
        private void textbox_numer_posesji_KeyPress(object sender, KeyPressEventArgs e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.LICZBY_LITERY_UKOSNIK, e);
        private void textbox_numer_lokalu_KeyPress(object sender, KeyPressEventArgs e) => RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.LICZBY_LITERY_UKOSNIK, e);
    }
}