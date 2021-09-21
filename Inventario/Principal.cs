using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SCP
{
    public partial class Principal : Form
    {

        public Principal()
        {
            InitializeComponent();
        }

        private void eNTRADATELAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EntradaTela et = new EntradaTela();
            et.MdiParent = this;
           et.Dock = DockStyle.Fill; ///MAXIMIZADO POR DEFAULT
            et.Show();
        }

        private void tIPOSALIDAToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sALIRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Seguro que deseas Cerrar el programa", "Cerrar", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void Principal_Load(object sender, EventArgs e)
        {

        }

        private void sALIDADETELAToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
