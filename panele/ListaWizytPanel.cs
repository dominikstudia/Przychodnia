using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Przychodnia.panele
{
    public partial class ListaWizytPanel : UserControl
    {
        private Uzytkownik _zalogowanyUzytkownik;

        public ListaWizytPanel(Uzytkownik zalogowanyUzytkownik)
        {
            InitializeComponent();
            _zalogowanyUzytkownik = zalogowanyUzytkownik;
            this.Load += ListaWizytPanel_Load;
        }

        private void ListaWizytPanel_Load(object sender, EventArgs e)
        {
            OdswiezListeWizyt();
        }

        /// <summary>
        /// Ładuje dane z bazy danych i ustawia je w DataGridView zgodnie z wymaganiami UC_WIZ_02.
        /// </summary>
        public void OdswiezListeWizyt()
        {
            if (_zalogowanyUzytkownik == null) return;

            // Pobieramy dane przygotowane przez metodę z klasy BazaDanych
            DataTable wizyty = BazaDanych.PobierzListeWizyt(_zalogowanyUzytkownik);

            // Scenariusz wyjątku E1: Pusty grafik
            if (wizyty.Rows.Count == 0)
            {
                datagridview_wizyty.DataSource = null;
                lbl_komunikat.Text = "Brak zaplanowanych wizyt do wyświetlenia.";
                lbl_komunikat.Visible = true;
            }
            else
            {
                lbl_komunikat.Visible = false;
                datagridview_wizyty.DataSource = wizyty;

                // Opcjonalne: Ładne dopasowanie szerokości kolumn w DataGridView
                datagridview_wizyty.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                datagridview_wizyty.AllowUserToAddRows = false; // Blokada dodawania pustych wierszy przez kliknięcie
                datagridview_wizyty.ReadOnly = true; // Przegląd w trybie tylko do odczytu
            }
        }
    }
}