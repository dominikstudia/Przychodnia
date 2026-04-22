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

            Uzytkownik zalogowany = BazaDanych.ZALOGOWANY_UZYTKOWNIK;


            if (zalogowany != null)
            {
                DodajRoleTest("Administrator");
                if (zalogowany.SprawdzCzyMaRole("Pacjent"))
                {

                }
                if (zalogowany.SprawdzCzyMaRole("Recepcjonista"))
                {
                    przycisk_wyszukaj_uzytkownika.Visible = true;
                }
                if (zalogowany.SprawdzCzyMaRole("Lekarz")) {
                    przycisk_wyszukaj_uzytkownika.Visible = true;
                }
              
                if (zalogowany.SprawdzCzyMaRole("Administrator"))
                {
                    przycisk_wyszukaj_uzytkownika.Visible = true;
                    przycisk_dodaj_uzytkownika.Visible = true;
                    przycisk_przeglad_uprawnien.Visible = true;
                }
            }
        }

        private void DodajRoleTest(String nazwa)
        {
            List<Rola> role = BazaDanych.PobierzWszystkieRole();
            Rola rola = role.FirstOrDefault(r => r.Nazwa == nazwa);

            if (rola != null && BazaDanych.ZALOGOWANY_UZYTKOWNIK != null)
            {
                BazaDanych.ZALOGOWANY_UZYTKOWNIK.IdRol.Clear();
                BazaDanych.ZALOGOWANY_UZYTKOWNIK.IdRol.Add(rola.Id);
            }
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
    }
}
