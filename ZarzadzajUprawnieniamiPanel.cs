using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Przychodnia
{

    public enum TrybZarzadzaniaRola
    {
        Wyswietlanie,
        Nadawanie,
        Odbieranie
    }

    public partial class ZarzadzajUprawnieniamiPanel : Form
    {

        private Rola _rola;
        private TrybZarzadzaniaRola _tryb;

        private List<Uzytkownik> _pelnaLista = new List<Uzytkownik>();
        private HashSet<int> _zaznaczeniId = new HashSet<int>();

        public ZarzadzajUprawnieniamiPanel(Rola rola, TrybZarzadzaniaRola tryb)
        {
            InitializeComponent();
            _rola = rola;
            _tryb = tryb;

            datagrid_uzytkownicy.AutoGenerateColumns = false;

            datagrid_uzytkownicy.SelectionChanged += (s, e) => datagrid_uzytkownicy.ClearSelection();
            datagrid_uzytkownicy.DataBindingComplete += (s, e) => OdtworzZaznaczonych();
            datagrid_uzytkownicy.EditMode = DataGridViewEditMode.EditProgrammatically;

            KonfigurujTabele();
            KonfigurujWyglad();
            ZaladujPoczatkowaListe();
        }

        private void KonfigurujWyglad()
        {
            if (_tryb == TrybZarzadzaniaRola.Wyswietlanie)
            {
                lbl_tytul_panelu.Text = $"Podgląd użytkowników: {_rola.Nazwa}";
                btn_akcja.Text = "Zamknij";
            }
            else if (_tryb == TrybZarzadzaniaRola.Nadawanie)
            {
                lbl_tytul_panelu.Text = $"Nadawanie masowe roli: {_rola.Nazwa}";
                btn_akcja.Text = "Nadaj zaznaczonym";
            }
            else if (_tryb == TrybZarzadzaniaRola.Odbieranie)
            {
                lbl_tytul_panelu.Text = $"Odbieranie masowe roli: {_rola.Nazwa}";
                btn_akcja.Text = "Zabierz zaznaczonym";
            }
        }

        private void ZaladujPoczatkowaListe()
        {
            var wszyscy = BazaDanych.Uzytkownicy.Where(u => !u.CzyZarchiwizowany).ToList();

            if (_tryb == TrybZarzadzaniaRola.Nadawanie)
            {
                _pelnaLista = wszyscy.Where(u => !u.IdRol.Contains(_rola.Id)).ToList();
            }
            else
            {
                _pelnaLista = wszyscy.Where(u => u.IdRol.Contains(_rola.Id)).ToList();
            }
            datagrid_uzytkownicy.DataSource = new BindingList<Uzytkownik>(_pelnaLista);
        }

        private void KonfigurujTabele()
        {
            datagrid_uzytkownicy.AutoGenerateColumns = false;
            datagrid_uzytkownicy.AllowUserToAddRows = false;
            datagrid_uzytkownicy.RowHeadersVisible = false;
            datagrid_uzytkownicy.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            datagrid_uzytkownicy.Columns.Clear();

            if (_tryb != TrybZarzadzaniaRola.Wyswietlanie)
            {
                datagrid_uzytkownicy.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Zaznacz", HeaderText = "Zaznacz", Width = 40, ReadOnly = true });
            }
            datagrid_uzytkownicy.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Login", Name = "Login", HeaderText = "Login", Width = 100, ReadOnly = true });
            datagrid_uzytkownicy.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Imiona", Name = "Imiona", HeaderText = "Imię", Width = 120, ReadOnly = true });
            datagrid_uzytkownicy.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nazwisko", Name = "Nazwisko", HeaderText = "Nazwisko", Width = 150, ReadOnly = true });
            datagrid_uzytkownicy.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Email", Name = "Email", HeaderText = "E-mail", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });
        }

        private void OdtworzZaznaczonych()
        {
            if (_tryb == TrybZarzadzaniaRola.Wyswietlanie) return;

            foreach (DataGridViewRow wiersz in datagrid_uzytkownicy.Rows)
            {
                wiersz.Cells["Zaznacz"].Value = wiersz.DataBoundItem is Uzytkownik u && _zaznaczeniId.Contains(u.Id) ? true : false;
            }
        }

        private void btn_szukaj_Click(object sender, EventArgs e)
        {
            string szukanaFraza = textbox_wyszukiwanie.Text.ToLower().Trim();

            var przefiltrowani = _pelnaLista.Where(u => _zaznaczeniId.Contains(u.Id) || string.IsNullOrEmpty(szukanaFraza) || u.PobierzWszystkieDane().Contains(szukanaFraza)).OrderByDescending(u => _zaznaczeniId.Contains(u.Id)).ThenBy(u => u.Nazwisko).ToList();
            datagrid_uzytkownicy.DataSource = new BindingList<Uzytkownik>(przefiltrowani);

            OdtworzZaznaczonych();
        }

        private void btn_akcja_Click(object sender, EventArgs e)
        {
            if (_tryb == TrybZarzadzaniaRola.Wyswietlanie)
            {
                this.Close();
                return;
            }
            List<Uzytkownik> zaznaczeni = _pelnaLista.Where(u => _zaznaczeniId.Contains(u.Id)).ToList();

            if (zaznaczeni.Count == 0)
            {
                MessageBox.Show("Nie wybrano żadnych użytkowników!", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_tryb == TrybZarzadzaniaRola.Nadawanie)
            {
                Cursor.Current = Cursors.WaitCursor;
                BazaDanych.MasowoNadajRole(zaznaczeni, _rola.Id);
                Cursor.Current = Cursors.Default;

                MessageBox.Show($"Pomyślnie nadano uprawnienia dla {zaznaczeni.Count} osób.");
                this.Close();
            }
            else if (_tryb == TrybZarzadzaniaRola.Odbieranie)
            {

                Cursor.Current = Cursors.WaitCursor;
                BazaDanych.MasowoZabierzRole(zaznaczeni, _rola.Id);
                Cursor.Current = Cursors.Default;

                MessageBox.Show($"Pomyślnie odebrano uprawnienia {zaznaczeni.Count} osobom.");
                this.Close();
            }
        }
        private void datagrid_uzytkownicy_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && _tryb != TrybZarzadzaniaRola.Wyswietlanie && datagrid_uzytkownicy.Rows[e.RowIndex].DataBoundItem is Uzytkownik uzytkownik)
            {
                bool nowyStan = !_zaznaczeniId.Contains(uzytkownik.Id);

                if (nowyStan)
                {
                    _zaznaczeniId.Add(uzytkownik.Id);
                }
                else
                {
                    _zaznaczeniId.Remove(uzytkownik.Id);
                }

                datagrid_uzytkownicy.Rows[e.RowIndex].Cells["Zaznacz"].Value = nowyStan;
            }
        }
    }
}
