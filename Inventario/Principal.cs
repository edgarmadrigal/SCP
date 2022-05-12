using System;
using System.Windows.Forms;
using WMPLib;
using System.IO;
using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace SCP
{
    public partial class Principal : Form
    {
        Funciones f = new Funciones();

        WindowsMediaPlayer sonido = new WindowsMediaPlayer();
        List<ConsultaUsuarioResult> usu = null;
        public Principal(List<ConsultaUsuarioResult> usuario)
        {
            try
            {
                InitializeComponent();
                this.usu = usuario;
                IsMdiContainer = true;

                if (usu[0].perfil == "4" || usu[0].perfil == "3" || usu[0].perfil == "8")
                {

                }
            }
            catch(Exception ex)
            {
                 MessageBox.Show(ex.Message.ToString());
            }


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
            wfcatTipoSalida ts = new wfcatTipoSalida();
            ts.MdiParent = this;
            ts.Show();
        }

        private void sALIRToolStripMenuItem_Click(object sender, EventArgs e)
        {
         //   WndProc(e);
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
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x10) // WM_CLOSE
            {
                // Process the form closing. Call the base method if required,
                // and return from the function if not.
                // For example:
                var ret = MessageBox.Show("Seguro que deseas Cerrar el programa?", "Cerrar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ret == System.Windows.Forms.DialogResult.No)
                    return;
            }
            base.WndProc(ref m);
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

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            DialogResult dialogResult = MessageBox.Show("Seguro que deseas Cerrar el programa", "Cerrar", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //Application.Exit();
            }
            else if (dialogResult == DialogResult.No)
            {
                e.Cancel = true;
                //do something else
            }*/
        }

        private void Principal_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void pROVEEDORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wfcatProveedor cp = new wfcatProveedor();
            cp.MdiParent = this;
            cp.Show();
        }

        private void eSTILOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wfcatEstilo ce = new wfcatEstilo();
            ce.MdiParent = this;
            ce.Show();
        }

        private void tIPODETELAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wfcatTipoTela ctt = new wfcatTipoTela();
            ctt.MdiParent = this;
            ctt.Show();
        }

        private void uBICACIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wfcatUbicacion u = new wfcatUbicacion();
            u.MdiParent = this;
            u.Show();
        }

        private void tIPODECIERREToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wfcatTipoCierre ctc = new wfcatTipoCierre();
            ctc.MdiParent = this;
            ctc.Show();
        }

        private void tIPOCORTEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wfcatTipoCorte tc = new wfcatTipoCorte();
            tc.MdiParent = this;
            tc.Show();
        }

        private void acercadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Creado por Edgar Oswaldo Madrigal Reynoso\n"+ "email:emadrigal@apparelinternational.com \n"+"Telefono:8718961844");

        }
    }
}
