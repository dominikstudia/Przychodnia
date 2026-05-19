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

        public ListaWizytPanel()
        {
            InitializeComponent();

            this.Load += ListaWizytPanel_Load;
        }

        private void ListaWizytPanel_Load(object sender, EventArgs e)
        {

            bool czyAdmin = BazaDanych.ZALOGOWANY_UZYTKOWNIK.IdRol.Contains(1);
            bool czyRecepcjonista = BazaDanych.ZALOGOWANY_UZYTKOWNIK.IdRol.Contains(3);
            bool czyLekarz = BazaDanych.ZALOGOWANY_UZYTKOWNIK.IdRol.Contains(2);

            if (!czyAdmin && !czyRecepcjonista)
            {
                cmb_lekarz.Visible = false;
                cmb_specjalizacja.Visible = false;

                label_lekarz.Visible = false;
                label_specjalizacja.Visible = false;
            }

            btn_szczegoly.Visible = czyLekarz || czyAdmin;

            InicjalizujFiltry();
            OdswiezListeWizyt();
        }

        private void SkonfigurujDataGrid()
        {
            datagridview_wizyty.Columns["PESEL"].Visible = false;
            datagridview_wizyty.Columns["Schorzenia"].Visible = false;
            datagridview_wizyty.Columns["Zalecenia"].Visible = false;
            datagridview_wizyty.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void InicjalizujFiltry()
        {
            DataTable wizyty = BazaDanych.PobierzListeWizyt(BazaDanych.ZALOGOWANY_UZYTKOWNIK);

            if (wizyty != null && wizyty.Rows.Count > 0)
            {
                var wszystkieDaty = wizyty.AsEnumerable().Select(r => r.Field<DateTime>("Data i Godzina"));
                dtp_dataOd.Value = wszystkieDaty.Min().Date;
                dtp_dataDo.Value = wszystkieDaty.Max().Date;
            }

            var wszyscyLekarze = BazaDanych.Uzytkownicy
                .Where(u => u.IdRol.Contains(2) && !u.CzyZarchiwizowany)
                .Select(u => u.Imiona + " " + u.Nazwisko)
                .OrderBy(nazwa => nazwa)
                .ToArray();

            cmb_lekarz.Items.Clear();
            cmb_lekarz.Items.Add("");
            cmb_lekarz.Items.AddRange(wszyscyLekarze);
            cmb_lekarz.SelectedIndex = 0;

            cmb_specjalizacja.Items.Clear();
            cmb_specjalizacja.Items.Add("");
            foreach (var spec in BazaDanych.PobierzSpecjalizacje())
            {
                cmb_specjalizacja.Items.Add(spec);
            }
            cmb_specjalizacja.SelectedIndex = 0;
        }

        public void OdswiezListeWizyt()
        {
            DataTable wizyty = BazaDanych.PobierzListeWizyt(BazaDanych.ZALOGOWANY_UZYTKOWNIK);

            if (wizyty.Rows.Count == 0)
            {
                datagridview_wizyty.DataSource = null;
                lbl_komunikat.Text = "Brak zaplanowanych wizyt do wyświetlenia.";
                lbl_komunikat.Visible = true;
                return;
            }
            lbl_komunikat.Visible = false;
            datagridview_wizyty.DataSource = wizyty;

            datagridview_wizyty.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datagridview_wizyty.AllowUserToAddRows = false;
            datagridview_wizyty.ReadOnly = true;

            SkonfigurujDataGrid();
        }

        private void btn_szukaj_Click(object sender, EventArgs e)
        {
            string fraza = txt_szukanaFraza.Text.Trim().ToLower();
            DateTime dataOd = dtp_dataOd.Value.Date;
            DateTime dataDo = dtp_dataDo.Value.Date;

            string lekarz = cmb_lekarz.SelectedItem != null ? cmb_lekarz.SelectedItem.ToString() : null;
            string specjalizacja = cmb_specjalizacja.SelectedItem != null ? cmb_specjalizacja.SelectedItem.ToString() : null;

            DataTable wszystkieWizyty = BazaDanych.PobierzListeWizyt(BazaDanych.ZALOGOWANY_UZYTKOWNIK);

            var przefiltrowane = wszystkieWizyty.AsEnumerable().Where(row =>
            {
                if (DateTime.TryParse(row["Data i Godzina"].ToString(), out DateTime dataWizyty))
                {
                    if (dataWizyty.Date < dataOd || dataWizyty.Date > dataDo) return false;
                }

                if (!string.IsNullOrEmpty(fraza))
                {
                    string pacjent = row["Imię i Nazwisko Pacjenta"].ToString().ToLower();
                    string pesel = row["PESEL"].ToString();

                    if (!pacjent.Contains(fraza) && !pesel.Contains(fraza))
                    {
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(lekarz) && row["Imię i Nazwisko Lekarza"].ToString() != lekarz)
                    return false;

                if (!string.IsNullOrEmpty(specjalizacja) && row["Specjalizacja"].ToString() != specjalizacja)
                    return false;

                return true;
            });

            if (przefiltrowane.Any())
            {
                lbl_komunikat.Visible = false;
                datagridview_wizyty.DataSource = przefiltrowane.CopyToDataTable();
                SkonfigurujDataGrid();
                return;
            }
            datagridview_wizyty.DataSource = null;
            lbl_komunikat.Text = "Nie znaleziono wizyt spełniających kryteria wyszukiwania.";
            lbl_komunikat.Visible = true;
        }


        // kod kakura (skopiowane z commita, bo musialem overridowac)
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