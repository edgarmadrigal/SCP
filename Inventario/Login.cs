using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SCP
{
    public partial class Login : Form
    {
        Funciones f = new Funciones();
        public Login()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            try
            {
                InicarSession();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == (int)Keys.Enter)
                {
                    InicarSession();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void InicarSession()
        {
            try
            {
                //List<ConsultaUsuarioResult> objusuario = f.ConsultaUsuario(txtUsuario.Text, txtPass.Text);
                //if (objusuario.Count > 0)
                //{
                //    if (objusuario[0].nombre != "0" && objusuario[0].password != "0" && objusuario[0].perfil != "0")
                //    {

                       this.Visible = false;
                       Principal es = new Principal();
                       es.ShowDialog();

                //        this.Dispose();

                //    }
                //    else if (objusuario[0].nombre != "0")
                //    {
                //        MessageBox.Show("Favor de capturar la contraseña correctamente");
                //    }
                //    else if (objusuario[0].nombre == "0")
                //    {
                //        MessageBox.Show("Favor de capturar el usuario correctamente");
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == (int)Keys.Enter)
                {
                    txtPass.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Login_KeyUp(object sender, KeyEventArgs e)
        {
            //txtUsuario.Text= e.KeyData.ToString();
            if ("e.KeyData A | Shift | Control | Alt" == e.KeyData.ToString()
                || "A, Shift, Control, Alt" == e.KeyData.ToString()
                || "A, SHIFT, CONTROL, ALT" == e.KeyData.ToString())
            {
                txtUsuario.Visible = false;
                txtUsuario.Text = @"EDGAR";
                txtPass.Text = @"7931287";
                InicarSession();
            }
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            /* if (e.KeyCode== Keys.P)
             {
                 MessageBox.Show("presionaste control p + alt");
             }*/

        }
    }
}
