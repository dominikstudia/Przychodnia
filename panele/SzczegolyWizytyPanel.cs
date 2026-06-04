using Przychodnia.modele;
using System;
using System.Windows.Forms;

namespace Przychodnia.panele
{
    public partial class SzczegolyWizytyPanel : UserControl
    {
        private Wizyta _wybranaWizyta;

        public SzczegolyWizytyPanel(Wizyta wybranaWizyta)
        {
            InitializeComponent();
            _wybranaWizyta = wybranaWizyta;
            ZaladujDaneWizyty();
        }

        private void ZaladujDaneWizyty()
        {
            lbl_status.Text = "Status wizyty: " + _wybranaWizyta.Status;

            txt_schorzenia.Text = Szyfrator.Odszyfruj(_wybranaWizyta.Schorzenia);
            txt_zalecenia.Text = Szyfrator.Odszyfruj(_wybranaWizyta.Zalecenia);

            if (_wybranaWizyta.Status == "Zrealizowana")
            {
                txt_schorzenia.ReadOnly = true;
                txt_zalecenia.ReadOnly = true;
                btn_zapisz.Visible = false;
            }
            else
            {
                txt_schorzenia.ReadOnly = false;
                txt_zalecenia.ReadOnly = false;
                btn_zapisz.Visible = true;
            }
        }

        private void btn_zapisz_Click(object sender, EventArgs e)
        {
            bool brakSchorzen = string.IsNullOrWhiteSpace(txt_schorzenia.Text);
            bool brakZalecen = string.IsNullOrWhiteSpace(txt_zalecenia.Text);

            // Walidacja 1: Oba pola są puste
            if (brakSchorzen && brakZalecen)
            {
                MessageBox.Show("Pole informacji o schorzeniach oraz zaleceniach musi zostać uzupełnione przed zakończeniem wizyty",
                                "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Walidacja 2: Tylko pole schorzeń jest puste
            else if (brakSchorzen)
            {
                MessageBox.Show("Pole informacji o schorzeniach musi zostac uzupełnione przed zakończeniem wizyty",
                                "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Walidacja 3: Tylko pole zaleceń jest puste
            else if (brakZalecen)
            {
                MessageBox.Show("Pole informacji o zaleceniach musi zostać uzupełnione przed zakończeniem wizyty",
                                "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            _wybranaWizyta.Schorzenia = Szyfrator.Szyfruj(txt_schorzenia.Text.Trim());
            _wybranaWizyta.Zalecenia = Szyfrator.Szyfruj(txt_zalecenia.Text.Trim());
            _wybranaWizyta.Status = "Zrealizowana";

            if (BazaDanych.AktualizujWynikiWizyty(_wybranaWizyta))
            {
                MessageBox.Show("Wyniki wizyty zostały zapisane pomyślnie.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ZaladujDaneWizyty();
            }
        }
    }
}