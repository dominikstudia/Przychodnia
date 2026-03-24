namespace Przychodnia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
    }
}
