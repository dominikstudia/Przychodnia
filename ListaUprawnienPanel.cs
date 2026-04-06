using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Przychodnia
{
    public partial class ListaUprawnienPanel : UserControl
    {
        public ListaUprawnienPanel()
        {
            InitializeComponent();
            ZaladujRoleDoWidoku();
        }
        private void ZaladujRoleDoWidoku()
        {
            var role = BazaDanych.PobierzWszystkieRole();
            listbox_uprawnienia.DataSource = role;
            listbox_uprawnienia.DisplayMember = "Nazwa";
            listbox_uprawnienia.ValueMember = "Id";
        }
    }
}
