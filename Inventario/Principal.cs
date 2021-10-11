using System;
using System.Windows.Forms;

namespace SCP
{
    public partial class Principal : Form
    {

        public Principal()
        {
            InitializeComponent();
            IsMdiContainer = true;
        }

        private void eNTRADATELAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EntradaTela et = new EntradaTela();
            et.MdiParent = this;
            // et.Dock = DockStyle.Fill; ///MAXIMIZADO POR DEFAULT
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
            SalidaTela et = new SalidaTela();
            et.MdiParent = this;
            //et.Dock = DockStyle.Fill; ///MAXIMIZADO POR DEFAULT
            et.Show();

        }

        private void dEVOLUCIONDETELAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DevolucionTela et = new DevolucionTela();
            et.MdiParent = this;
            //et.Dock = DockStyle.Fill; ///MAXIMIZADO POR DEFAULT
            et.Show();
        }

        private void cIERREToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Cierre et = new Cierre();
            et.MdiParent = this;
            //et.Dock = DockStyle.Fill; ///MAXIMIZADO POR DEFAULT
            et.Show();
        }

        private void cORTEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cortes et = new Cortes();
            et.MdiParent = this;
            //et.Dock = DockStyle.Fill; ///MAXIMIZADO POR DEFAULT
            et.Show();
        }

        private void mODULOSToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
