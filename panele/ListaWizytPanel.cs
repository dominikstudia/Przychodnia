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

            btn_szczegoly.Visible = _zalogowanyUzytkownik.IdRol.Contains(2);
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

                datagridview_wizyty.Columns["Schorzenia"].Visible = false;
                datagridview_wizyty.Columns["Zalecenia"].Visible = false;
                datagridview_wizyty.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Opcjonalne: Ładne dopasowanie szerokości kolumn w DataGridView
                datagridview_wizyty.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                datagridview_wizyty.AllowUserToAddRows = false; // Blokada dodawania pustych wierszy przez kliknięcie
                datagridview_wizyty.ReadOnly = true; // Przegląd w trybie tylko do odczytu
            }
        }

        private void btn_szczegoly_Click(object sender, EventArgs e)
        {
            if (datagridview_wizyty.SelectedRows.Count > 0)
            {
                System.Data.DataRowView wierszWidoku = (System.Data.DataRowView)datagridview_wizyty.SelectedRows[0].DataBoundItem;
                System.Data.DataRow wiersz = wierszWidoku.Row;

                Wizyta zaznaczonaWizyta = new Wizyta
                {
                    IdWizyty = Convert.ToInt32(wiersz["ID Wizyty"]),
                    Status = wiersz["Status Wizyty"].ToString(),
                    Schorzenia = wiersz["Schorzenia"].ToString(),
                    Zalecenia = wiersz["Zalecenia"].ToString()
                };

                Form oknoWizyty = new Form();
                oknoWizyty.Text = "Szczegóły wizyty medycznej";
                oknoWizyty.Size = new System.Drawing.Size(600, 500);
                oknoWizyty.StartPosition = FormStartPosition.CenterScreen;
                oknoWizyty.FormBorderStyle = FormBorderStyle.FixedDialog;
                oknoWizyty.MaximizeBox = false;

                var panelSzczegolow = new Przychodnia.panele.SzczegolyWizytyPanel(zaznaczonaWizyta);
                panelSzczegolow.Dock = DockStyle.Fill;

                oknoWizyty.Controls.Add(panelSzczegolow);
                oknoWizyty.ShowDialog();

                OdswiezListeWizyt();
            }
            else
            {
                MessageBox.Show("Najpierw zaznacz wizytę na liście.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}