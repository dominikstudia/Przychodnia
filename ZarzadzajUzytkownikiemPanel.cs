using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Przychodnia
{
    public partial class ZarzadzajUzytkownikiemPanel : UserControl
    {
        public ZarzadzajUzytkownikiemPanel()
        {
            InitializeComponent();
        }

        private void textbox_pesel_Click(object sender, EventArgs e)
        {
            textbox_pesel.SelectionStart = 0;
        }

        private void textbox_numer_telefonu_Click(object sender, EventArgs e)
        {
            textbox_numer_telefonu.SelectionStart = 0;
        }

        private void textbox_kod_pocztowy_Click(object sender, EventArgs e)
        {
            textbox_kod_pocztowy.SelectionStart = 0;
        }
    }
}
