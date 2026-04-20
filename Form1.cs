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
            BazaDanych.ZaladujBazeDanych();

            panel_boczny.Enabled = true;
            panel_boczny.Visible = true;
            panel_edycji.Controls.Clear();

            label_ogolny.Text = "PRZYCHODNIA";
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
    }
}
