using Przychodnia.formsy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Przychodnia
{
    public partial class LogowaniePanel : UserControl
    {
        private System.Windows.Forms.Timer timerBlokady;
        private DateTime czasOdblokowania;
        private string zablokowanyLogin = "";
        private Label label_komunikat_timera;

        public LogowaniePanel()
        {
            InitializeComponent();

            label_komunikat_timera = new Label();
            label_komunikat_timera.ForeColor = Color.Red;
            label_komunikat_timera.AutoSize = false;
            label_komunikat_timera.Width = 350;
            label_komunikat_timera.Height = 25;
            label_komunikat_timera.TextAlign = ContentAlignment.MiddleCenter;
            label_komunikat_timera.Font = new Font(label_komunikat_timera.Font, FontStyle.Bold);

            label_komunikat_timera.Location = new Point(btn_zaloguj.Location.X - 25, btn_zaloguj.Location.Y - 30);
            this.Controls.Add(label_komunikat_timera);

            timerBlokady = new System.Windows.Forms.Timer();
            timerBlokady.Interval = 1000;
            timerBlokady.Tick += TimerBlokady_Tick;

            textbox_login.TextChanged += (s, e) => TimerBlokady_Tick(null, null);
        }

        private void TimerBlokady_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(zablokowanyLogin) && textbox_login.Text.Trim().ToLower() == zablokowanyLogin.ToLower())
            {
                TimeSpan pozostalo = czasOdblokowania - DateTime.Now;
                if (pozostalo.TotalSeconds <= 0)
                {
                    timerBlokady.Stop();
                    label_komunikat_timera.Text = "";
                    zablokowanyLogin = "";
                }
                else
                {
                    label_komunikat_timera.Text = $"Login zablokowany. Odblokowanie za: {pozostalo.Seconds} s";
                }
            }
            else
            {
                label_komunikat_timera.Text = "";
            }
        }

        private void btn_zaloguj_Click(object sender, EventArgs e)
        {
            string email = textbox_login.Text.Trim();
            string haslo = textbox_haslo.Text;

            if (email == "" || haslo == "")
            {
                MessageBox.Show("Wprowadź login i hasło");
                return;
            }

            var wynik = BazaDanych.SprobujZalogowac(email, haslo);

            if (wynik.Sukces)
            {
                timerBlokady.Stop();
                label_komunikat_timera.Text = "";
                zablokowanyLogin = "";

                // -- MECHANIZM WYMUSZANIA ZMIANY HASŁA --
                if (wynik.WymagaZmiany)
                {
                    MessageBox.Show("Zalogowano używając hasła jednorazowego.\nZe względów bezpieczeństwa musisz natychmiast zmienić swoje hasło.", "Wymagana zmiana", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    ZmianaHasla oknoZmiany = new ZmianaHasla();
                    oknoZmiany.ShowDialog();

                    // Jeśli użytkownik zamknął okienko krzyżykiem bez poprawnej zmiany hasła:
                    if (BazaDanych.ZALOGOWANY_UZYTKOWNIK != null && BazaDanych.CzyNadalWymagaZmiany(BazaDanych.ZALOGOWANY_UZYTKOWNIK.Id))
                    {
                        MessageBox.Show("Nie zmieniono hasła. Dostęp zablokowany.", "Błąd bezpieczeństwa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        BazaDanych.ZALOGOWANY_UZYTKOWNIK = null;
                        return; // Przerywamy logowanie i zostajemy na ekranie logowania
                    }
                }

                // Jeśli hasło zostało zmienione poprawnie (albo w ogóle nie wymagało zmiany), wpuszczamy do systemu
                ((Form1)this.FindForm()).OdblokujSystemPoZalogowaniu();
            }
            else
            {
                if (wynik.ZablokowanyDo.HasValue)
                {
                    zablokowanyLogin = email;
                    czasOdblokowania = wynik.ZablokowanyDo.Value;
                    timerBlokady.Start();

                    TimerBlokady_Tick(null, null);

                    MessageBox.Show(wynik.Komunikat, "Dostęp zablokowany", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(wynik.Komunikat, "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void label_zapomnialem_hasla_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Tworzymy na szybko małe okienko do pobrania e-maila
            Form prompt = new Form()
            {
                Width = 400,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Resetowanie hasła",
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false
            };
            Label lbl = new Label() { Left = 20, Top = 20, Width = 340, Text = "Podaj adres e-mail przypisany do Twojego konta:" };
            TextBox txtEmail = new TextBox() { Left = 20, Top = 50, Width = 340 };
            Button btnWyslij = new Button() { Text = "Wyślij kod", Left = 260, Width = 100, Top = 90, DialogResult = DialogResult.OK };

            prompt.Controls.Add(lbl); prompt.Controls.Add(txtEmail); prompt.Controls.Add(btnWyslij);
            prompt.AcceptButton = btnWyslij;

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                var wynik = BazaDanych.ZresetujHaslo(txtEmail.Text.Trim());
                if (wynik.Sukces) MessageBox.Show(wynik.Komunikat, "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else MessageBox.Show(wynik.Komunikat, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
