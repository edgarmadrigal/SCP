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
    public partial class wfcatEstilo : Form
    {
        public wfcatEstilo()
        {
            InitializeComponent();
        }

        private void btnEditarPrePack_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                if (dgvPrePack2.RowCount > 0)
                {
                    //RengloSelecionado = dgvPrePack2.CurrentCell.RowIndex;
                    DataGridViewRow newDatarow = dgvPrePack2.Rows[RengloSelecionado];
                    newDatarow.Cells[0].Value = cmbTallaPrepack.Text;
                    newDatarow.Cells[1].Value = txtCantidadPrepack.Text;
                    newDatarow.Cells[2].Value = txtCodigoupcPrepack.Text;
                    newDatarow.Cells[3].Value = cmbTallaPrepack.SelectedValue.ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
        }

        private void btnGuardarPrepack_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                if (validar() == 1)
                {
                    if (dgvPrePack2.RowCount > 0)
                    {
                        Prepack p = new Prepack();
                        int idPrepack = 0;

                        p.estilo = txtEstiloPrepack.Text;
                        p.po_numero = Convert.ToDecimal(txtPoPrepack.Text);

                        idPrepack = f.GuardarPrePack(p);

                        foreach (DataGridViewRow renglon in dgvPrePack2.Rows)
                        {
                            int cantidad = Convert.ToInt32(renglon.Cells[1].Value.ToString());

                            for (int i = cantidad; i > 0; i--)
                            {
                                PrepackDetalle pd = new PrepackDetalle();
                                pd.idPrepack = idPrepack;
                                pd.size = renglon.Cells[0].Value.ToString();
                                pd.cantidad = 1;
                                pd.upc = renglon.Cells[2].Value.ToString();
                                pd.idusuario = this.usu[0].id;
                                pd.idSize = Convert.ToInt32(renglon.Cells[3].Value.ToString());
                                f.GuardarPrePackDetalle(pd);
                            }

                        }

                        LimpiarCampos();

                        // MessageBox.Show("PREPACK: "+ idPrepack);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
        }

        private void btnBorrarRenglonPrePack_Click(object sender, EventArgs e)
        {

            //LimpiarCampos();
        }

        private void btnAgregaTalla_Click(object sender, EventArgs e)
        {

            AgregaTalla();
        }

        public void AgregaTalla()
        {
            /*
            try
            {
                if (txtCantidadPrepack.Text != "" && txtCodigoupcPrepack.Text != "")
                {
                    tablaPrepack.Rows.Add(cmbTallaPrepack.Text, txtCantidadPrepack.Text, txtCodigoupcPrepack.Text, cmbTallaPrepack.SelectedValue.ToString());
                    dgvPrePack2.DataSource = tablaPrepack;
                    dgvPrePack2.Columns["idSize"].Visible = false;
                    cmbTallaPrepack.SelectedIndex = 0;
                    txtCodigoupcPrepack.Text = "";
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
        }

        private void btnQuitaTalla_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                if (dgvPrePack2.RowCount > 0)
                {
                    RengloSelecionado = dgvPrePack2.CurrentCell.RowIndex;
                    dgvPrePack2.Rows.RemoveAt(RengloSelecionado);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
        }

        private void dgvPrePack2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            try
            {
                RengloSelecionado = e.RowIndex;
                DataGridViewRow row = dgvPrePack2.Rows[RengloSelecionado];

                cmbTallaPrepack.Text = row.Cells[0].Value.ToString();
                txtCantidadPrepack.Text = row.Cells[1].Value.ToString();
                txtCodigoupcPrepack.Text = row.Cells[2].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
        }

        

        private void btnAgregaTalla_Click_1(object sender, EventArgs e)
        {

        }
    }
}
