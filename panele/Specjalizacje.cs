using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
// Upewnij się, że masz tu odpowiedni namespace dla klasy Uzytkownik, np.:
// using Przychodnia.Modele; 

namespace Przychodnia.panele
{
    public partial class Specjalizacje : UserControl
    {
        public Specjalizacje()
        {
            InitializeComponent();

            // Rejestrujemy zdarzenie Load, by wypełnić listy przy otwarciu panelu
            this.Load += Specjalizacje_Load;
        }

        private void Specjalizacje_Load(object sender, EventArgs e)
        {
            ZaladujDaneDoFormularza();
        }

        private void ZaladujDaneDoFormularza()
        {
            // 1. Ładowanie lekarzy (zakładamy, że rola Lekarza to ID = 2)
            // Używamy BazaDanych.Uzytkownicy, która jest już w pamięci
            var listaLekarzy = BazaDanych.Uzytkownicy
                .Where(u => u.IdRol.Contains(2) && !u.CzyZarchiwizowany)
                .ToList();

            cmbLekarze.DataSource = listaLekarzy;
            cmbLekarze.DisplayMember = "Nazwisko"; // Możesz tu ustawić inną właściwość z klasy Uzytkownik, np. stworzyć właściwość "PelneImie"
            cmbLekarze.ValueMember = "Id";

            // 2. Ładowanie specjalizacji za pomocą nowej metody
            var listaSpecjalizacji = BazaDanych.PobierzWszystkieSpecjalizacjeZId();

            cmbSpecjalizacje.DataSource = listaSpecjalizacji;
            cmbSpecjalizacje.DisplayMember = "Nazwa";
            cmbSpecjalizacje.ValueMember = "Id";

            // Czyszczenie zaznaczenia na starcie
            cmbLekarze.SelectedIndex = -1;
            cmbSpecjalizacje.SelectedIndex = -1;
        }

        private void btnZapisz_Click(object sender, EventArgs e)
        {
            // Walidacja czy użytkownik cokolwiek wybrał
            if (cmbLekarze.SelectedValue == null || cmbSpecjalizacje.SelectedValue == null)
            {
                MessageBox.Show("Proszę wybrać lekarza oraz specjalizację.", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idLekarza = (int)cmbLekarze.SelectedValue;
            int idSpecjalizacji = (int)cmbSpecjalizacje.SelectedValue;

            bool sukces = BazaDanych.PrzypiszSpecjalizacjeLekarzowi(idLekarza, idSpecjalizacji);

            if (sukces)
            {
                MessageBox.Show("Pomyślnie przypisano specjalizację do lekarza.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Opcjonalnie resetujemy formularz po zapisie
                cmbLekarze.SelectedIndex = -1;
                cmbSpecjalizacje.SelectedIndex = -1;
            }
        }
    }
}