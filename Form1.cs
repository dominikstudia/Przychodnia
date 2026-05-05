using Przychodnia.formsy;
using System.Data;

namespace Przychodnia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PokazEkranLogowania();
        }

        private void PokazEkranLogowania()
        {
            panel_boczny.Enabled = false;
            panel_boczny.Visible = false;
            panel_edycji.Controls.Clear();

            LogowaniePanel formularz = new LogowaniePanel();
            formularz.Dock = DockStyle.Fill;
            panel_edycji.Controls.Add(formularz);
            label_ogolny.Text = "Logowanie do systemu";
        }

        public void OdblokujSystemPoZalogowaniu()
        {
            panel_boczny.Enabled = true;
            panel_boczny.Visible = true;
            panel_edycji.Controls.Clear();

            label_ogolny.Text = "PRZYCHODNIA";

            bool czyAdmin = BazaDanych.ZALOGOWANY_UZYTKOWNIK.IdRol.Contains(1);
            bool czyRecepcjonista = BazaDanych.ZALOGOWANY_UZYTKOWNIK.IdRol.Contains(3);

            przycisk_dodaj_uzytkownika.Visible = czyAdmin;
            przycisk_przeglad_uprawnien.Visible = czyAdmin;
            przycisk_dodaj_pacjenta.Visible = czyRecepcjonista;
            przycisk_wyszukaj_uzytkownika.Visible = (czyAdmin || czyRecepcjonista);

            przycisk_wyszukaj_uzytkownika.Text = czyAdmin ? "Lista użytkowników" : "Wyszukaj pacjenta";
        }

        private void przycisk_dodaj_uzytkownika_Click(object sender, EventArgs e)
        {
            panel_edycji.Controls.Clear();

            ZarzadzajUzytkownikiemPanel formularz = new ZarzadzajUzytkownikiemPanel();
            formularz.Dock = DockStyle.Fill;
            panel_edycji.Controls.Add(formularz);
            label_ogolny.Text = "Dodawanie użytkownika";
        }

        private void przycisk_wyszukaj_uzytkownika_Click(object sender, EventArgs e)
        {
            panel_edycji.Controls.Clear();

            ListaUzytkownikowPanel formularz = new ListaUzytkownikowPanel();
            formularz.Dock = DockStyle.Fill;
            panel_edycji.Controls.Add(formularz);
            label_ogolny.Text = "Lista użytkowników";
        }

        private void przycisk_przeglad_uprawnien_Click(object sender, EventArgs e)
        {
            panel_edycji.Controls.Clear();

            ListaUprawnienPanel formularz = new ListaUprawnienPanel();
            formularz.Dock = DockStyle.Fill;
            panel_edycji.Controls.Add(formularz);
            label_ogolny.Text = "Lista uprawnień";
        }

        private void btn_wyloguj_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Czy na pewno chcesz sie wylogować?", "Potwierdzenie wylogowania", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                PokazEkranLogowania();
                BazaDanych.ZALOGOWANY_UZYTKOWNIK = null;
            }
        }

        private void btn_zmien_haslo_Click(object sender, EventArgs e)
        {
            using (var okno = new ZmianaHasla())
            {
                okno.ShowDialog();
            }
        }

        private void przycisk_dodaj_pacjenta_Click(object sender, EventArgs e)
        {
            panel_edycji.Controls.Clear();

            DodajPacjentaPanel formularz = new DodajPacjentaPanel();
            formularz.Dock = DockStyle.Fill;
            panel_edycji.Controls.Add(formularz);

            label_ogolny.Text = "Rejestracja nowego pacjenta";
        }
    }
}
