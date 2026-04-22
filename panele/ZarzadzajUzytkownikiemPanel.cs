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

            if (czyTylkoOdczyt) checkedlistbox_uprawnienia.Enabled = false;

            if (czyTylkoOdczyt)
            {
                ZablokujWszystkiePola(this.Controls);
                btn_wygeneruj.Visible = false;
                btn_anuluj.Text = "Powrót";
                btn_potwierdz.Visible = false;
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
                if (c.HasChildren) ZablokujWszystkiePola(c.Controls);
            }
        }

        private Boolean SprawdzPesel(String pesel, Boolean czyMezczyzna, DateTime data)
        {
            if (pesel == null || !Regex.IsMatch(pesel, @"^\d{11}$")) return false;

            int rok = data.Year;
            int sformatowanyMiesiac = data.Month;

            if (rok < 1900 || rok > 2100) return false;
            if (rok >= 2000) sformatowanyMiesiac += 20;

            string oczekiwanyPoczatek = $"{rok % 100:D2}{sformatowanyMiesiac:D2}{data.Day:D2}";
            if (!pesel.StartsWith(oczekiwanyPoczatek)) return false;

            bool czyPeselWskazujeMezczyzne = int.Parse(pesel[9].ToString()) % 2 != 0;
            if (czyPeselWskazujeMezczyzne != czyMezczyzna) return false;

            int[] wagi = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int suma = 0;

            for (int i = 0; i < 10; i++)
            {
                suma += int.Parse(pesel[i].ToString()) * wagi[i];
            }
            int cyfraKontrolna = (10 - (suma % 10)) % 10;

            return cyfraKontrolna == int.Parse(pesel[10].ToString());
        }

        ///

        private void textbox_pesel_Click(object sender, EventArgs e)
        {
            textbox_pesel.SelectionStart = 0;
        }

        private void textbox_numer_telefonu_Click(object sender, EventArgs e)
        {
            textbox_numer_telefonu.SelectionStart = 0;
        }

        private void textbox_kod_pocztowy_Click(object sender, EventArgs e)
        {
            textbox_kod_pocztowy.SelectionStart = 0;
        }

        private void textbox_login_KeyPress(object sender, KeyPressEventArgs e)
        {
            RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.MALE_LITERY_I_PODLOGA, e);
        }

        private void textbox_email_KeyPress(object sender, KeyPressEventArgs e)
        {
            RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.LITERY_LICZBY_PODLOGA_KROPKA_MYSLNIK_PODLOGA, e);
        }

        private void textbox_imiona_KeyPress(object sender, KeyPressEventArgs e)
        {
            RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.POLSKIE_LITERY_SPACJA, e);
        }

        private void textbox_nazwisko_KeyPress(object sender, KeyPressEventArgs e)
        {
            RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.POLSKIE_LITERY_MYSLNIK, e);
        }

        private void textbox_miejscowosc_KeyPress(object sender, KeyPressEventArgs e)
        {
            RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.POLSKIE_LITERY_SPACJA_MYSLNIK, e);
        }

        private void textbox_ulica_KeyPress(object sender, KeyPressEventArgs e)
        {
            RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.POLSKIE_LITERY_SPACJA_MYSLNIK, e);
        }

        private void textbox_numer_posesji_KeyPress(object sender, KeyPressEventArgs e)
        {
            RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.LICZBY_LITERY_UKOSNIK, e);
        }

        private void textbox_numer_lokalu_KeyPress(object sender, KeyPressEventArgs e)
        {
            RegexPatterny.SprawdzCzyMoznaKliknacPrzycisk(RegexPatterny.LICZBY_LITERY_UKOSNIK, e);
        }

        private void btn_anuluj_Click(object sender, EventArgs e)
        {
            Form okno = this.FindForm();
            if (okno != null && okno is not Form1) okno.Close();
            else this.Parent?.Controls.Remove(this);
        }

        private void btn_potwierdz_Click(object sender, EventArgs e)
        {
            var wymaganePolaTekstowe = new Dictionary<Control, Label>()
            {
                {textbox_login, label_login}, {textbox_email, label_adres_email}, {textbox_imiona, label_imiona}, {textbox_nazwisko, label_nazwisko}, { textbox_pesel, label_numer_pesel},
                {combobox_plec, label_plec}, {textbox_numer_telefonu, label_numer_telefonu}, {textbox_miejscowosc, label_miejscowosc}, {textbox_ulica, label_ulica}, {textbox_kod_pocztowy, label_kod_pocztowy}
            };
            foreach (var pole in wymaganePolaTekstowe)
            {
                if ((pole.Key is TextBox t && string.IsNullOrWhiteSpace(t.Text)) || (pole.Key is MaskedTextBox m && !m.MaskFull) || (pole.Key is ComboBox c && c.SelectedIndex == -1))
                {
                    MessageBox.Show($"Pole '{pole.Value.Text}' jest wymagane!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    pole.Key.Focus();
                    return;
                }
            }

            if (!Regex.IsMatch(textbox_email.Text, RegexPatterny.WALIDATOR_EMAIL))
            {
                MessageBox.Show("Wprowadziłeś niepoprawny format emaila");
                return;
            }

            if (!Regex.IsMatch(textbox_haslo.Text, RegexPatterny.HASLO))
            {
                MessageBox.Show("Hasło nie spełnia wymagań bezpieczeństwa." +
                    "\nHasło powinno mieć:" +
                    "\n1. Przynajmniej jedna mała litere (od a do z)" +
                    "\n2. Przynajmniej jedna wielka litera (od A do Z)" +
                    "\n3. Przynajmniej jedną cyfre (od 0 do 9)" +
                    "\n4. Przynajmniej jeden znak specjalny (!@#$%^&*()_-+={[}]|:;\"'<,>.?/)");
                return;
            }


            if (checkedlistbox_uprawnienia.CheckedItems.Count == 0 && (_uzytkownik == null || !_uzytkownik.CzyZarchiwizowany))
            {
                MessageBox.Show("Użytkownik musi mieć co najmniej jedno uprawnienie!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime data = datetimerpicker_data_urodzenia.Value;
            if (!SprawdzPesel(textbox_pesel.Text, combobox_plec.SelectedItem.ToString() == "Mężczyzna", data))
            {
                MessageBox.Show("Pesel nie zgadza się z datą urodzenia użytkownika lub płcią!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Uzytkownik uzytkownik;

            if (_uzytkownik != null)
            {
                uzytkownik = _uzytkownik;
            }
            else
            {
                uzytkownik = new Uzytkownik();
                uzytkownik.Id = -1;
            }

            uzytkownik.Login = textbox_login.Text;
            uzytkownik.Imiona = textbox_imiona.Text;
            uzytkownik.Nazwisko = textbox_nazwisko.Text;
            uzytkownik.Haslo = textbox_haslo.Text;
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

            uzytkownik.IdRol.Clear();
            foreach (Rola r in checkedlistbox_uprawnienia.CheckedItems)
            {
                uzytkownik.IdRol.Add(r.Id);
            }

            if (BazaDanych.DodajLubZaaktualizujUzytkownika(uzytkownik))
            {
                if (_uzytkownik == null)
                {
                    MessageBox.Show("Pomyślnie dodano użytkownika");
                }
                else
                {
                    MessageBox.Show("Pomyślnie zaaktualizowano użytkownika");
                }
            }
            btn_anuluj_Click(null, null);
        }

        private void btn_wygeneruj_Click(object sender, EventArgs e)
        {
            string litery = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string cyfry = "0123456789";
            string znakiSpecjalne = "!@#$%^&*()_+-=[]{}|;:,.<>?";

            Random random = new Random();

            StringBuilder haslo = new StringBuilder();
            for (int i = 0; i < 3; i++) haslo.Append(litery.ToUpper()[random.Next(litery.Length)]);
            for (int i = 0; i < 3; i++) haslo.Append(litery.ToLower()[random.Next(litery.Length)]);
            for (int i = 0; i < 2; i++) haslo.Append(cyfry[random.Next(cyfry.Length)]);
            for (int i = 0; i < 2; i++) haslo.Append(znakiSpecjalne[random.Next(znakiSpecjalne.Length)]);

            textbox_haslo.Text = haslo.ToString();
            Clipboard.SetText(haslo.ToString());

            MessageBox.Show("Wygenerowano nowe hasło i skopiowno je do schowka.");
        }
    }
}
