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
    public partial class EntradaTela : Form
    {
        Funciones f = new Funciones();        
        DataTable tabla;
        List<ConsultaUsuarioResult> u = null;
        private int RenglonSelecionado;
        public EntradaTela(List<ConsultaUsuarioResult> usuario)
        {
            try
            {
                InitializeComponent();
                this.u = usuario;
                f.ConsultaTipoTela(cmbTipoTela);
                cmbTipoTela.SelectedIndex = 0;

                f.ConsultaProveedor(cmbProveedor);
                cmbProveedor.SelectedIndex = 0;

                f.ConsultaEstatus(cmbEstatus);
                cmbEstatus.SelectedIndex = 0;

                f.ConsultaEstilo(cmbEstilo);
                cmbEstilo.SelectedIndex = 0;

                f.ConsultaCliente(cmbCliente);
                cmbCliente.SelectedIndex = 0;

                f.ConsultaUbicacion(cmbUbicacion);
                cmbUbicacion.SelectedIndex = 0;

                tabla = new DataTable();
                tabla.Columns.Add("NoRollo", typeof(string));
                tabla.Columns.Add("Existencia", typeof(string));
                tabla.Columns.Add("LongMts", typeof(string));
                tabla.Columns.Add("Ubicacion", typeof(string));
                tabla.Columns.Add("idUbicacion", typeof(int));
                tabla.Columns.Add("Dubicacion", typeof(string));
                tabla.Columns.Add("entrada", typeof(string));



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


        }
        public EntradaTela()
        {
                InitializeComponent();
        }
        private void EntradaTela_Load(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGuardarPrepack_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ayudaToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Creado por Edgar Oswaldo Madrigal Reynoso\n" + "email:emadrigal@apparelinternational.com \n" + "Telefono:8718961844");

        }

        private void btnAgregaTalla_Click(object sender, EventArgs e)
        {
            Agregar();
        }

        public void Agregar()
        {
            try
            {
                if (validar())
                {
                    tabla.Rows.Add(txtNoRollo.Text.Replace(" ", String.Empty)
                                  , txtExistencia.Text.Replace(" ", String.Empty)
                                  , txtLongMts.Text.Replace(" ", String.Empty)
                                  , cmbUbicacion.Text
                                  ,cmbUbicacion.SelectedValue
                                  ,txtUbicacion.Text.Replace(" ", String.Empty)
                                  ,txtEntrada.Text.Replace(" ", String.Empty));
                    dgvDetalleEntrada.DataSource = tabla;
                    dgvDetalleEntrada.Columns["idUbicacion"].Visible = false;
                    cmbUbicacion.SelectedIndex = 0;
                    txtNoRollo.Text = "";
                    txtExistencia.Text = "";
                    txtLongMts.Text = "";
                    txtUbicacion.Text = "";
                    txtEntrada.Text = "";
                    txtNoRollo.Focus();
                }
                else
                {
                    MessageBox.Show("Favor de capturar todos los campos");
                    txtNoRollo.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool validar()
        {
            bool valida=false;
            if(string.IsNullOrEmpty(txtNoRollo.Text)  
            || string.IsNullOrEmpty(txtExistencia.Text) 
            || string.IsNullOrEmpty(txtUbicacion.Text)
            || string.IsNullOrEmpty(txtEntrada.Text)) 
            {
                valida = false;
            }
            else
            {
                valida = true;
            }

            return valida;

        }
        private void guardarToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaEntradaResult ce = new ConsultaEntradaResult();
                ce.release               = txtReleaseEntrada.Text;
                ce.idTipoTela            = Convert.ToInt32(cmbTipoTela.SelectedValue.ToString());
                ce.idProveedor           = Convert.ToInt32(cmbProveedor.SelectedValue.ToString());
                ce.idCliente             = Convert.ToInt32(cmbCliente.SelectedValue.ToString());
                ce.idEstilo              = Convert.ToInt32(cmbEstilo.SelectedValue.ToString());
                ce.idEstatus             = Convert.ToInt32(cmbEstatus.SelectedValue.ToString());
                ce.costo                 = Convert.ToDecimal((txtCosto.Text.Replace('$',' ')).Replace(" ", String.Empty));
                ce.ancho                 = Convert.ToDecimal((txtAncho.Text.Replace('$', ' ')).Replace(" ", String.Empty));
                ce.po                    = txtPO.Text;
                ce.camion                = txtCamion.Text;
                ce.costoFlete            = Convert.ToDecimal((txtCostoFlete.Text.Replace('$', ' ')).Replace(" ", String.Empty));
                ce.CostoImportacion      = Convert.ToDecimal((txtCostoImportacion.Text.Replace('$', ' ')).Replace(" ", String.Empty));
                ce.facturaAnterior       = dtpFacturaAnt.Value;
                ce.idUsuario_Creacion    = u[0].id;
                decimal totalEntradaYDS  = 0;

                if (dgvDetalleEntrada.RowCount > 0)
                {
                    foreach (DataGridViewRow Dgvrenglon in dgvDetalleEntrada.Rows)
                    {
                        double centimetros, pies, pulgadas, yarda;
                        float longMetros = float.Parse(Dgvrenglon.Cells[2].Value.ToString());// TOTAL DE METROS
                        centimetros = longMetros * 100;
                        pulgadas = centimetros / 2.54;
                        pies = pulgadas / 12;
                        yarda = pies / 3;

                        totalEntradaYDS = Convert.ToDecimal(yarda) + Convert.ToDecimal(totalEntradaYDS);
                    }
                }
                ce.totalYardasRelease    = totalEntradaYDS;
                ce.totalRollos           = dgvDetalleEntrada.RowCount;

                /*LA SUMA DE TODAS LAS YARDAS TE DEBE DE TRAER DEL DATAGRIDVIEW */

                //ed.totalExistenciaYDS = Convert.ToDecimal(txtTotalExistentesYDS.Text);
                //ed.totalEntradaYDS = Convert.ToDecimal(txtTotalEntradaYDS.Text);

                //ed.stiffness = txtStiffness.Text;
                //ed.onzas = txtOnzas.Text;
                int idEntradaTela = f.GuardarEntrada(ce);
                int idDetalleEntradaTela = 0;
                if (idEntradaTela > 0)
                {
                    if (dgvDetalleEntrada.RowCount > 0)
                    {
                        foreach (DataGridViewRow Dgvrenglon in dgvDetalleEntrada.Rows)
                        {
                            // AQUI UN FOREACH DEL DATAGRIDVIEW
                            ConsultaEntradaDetalleResult ed = new ConsultaEntradaDetalleResult();
                            ed.idEntradaTela = idEntradaTela;
                            // AQUI SE DEBE DE TRAER DEL DATAGRIDVIEW  CADA UNO EL NOROLLO LA EXISTENCIA LA LONGMETROS, LA UBICACION Y ENTRADA
                            ed.noRollo = Dgvrenglon.Cells[0].Value.ToString();
                            ed.existencia = Convert.ToDecimal(Dgvrenglon.Cells[1].Value.ToString().Replace(" ", String.Empty));
                            ed.logMetros = Convert.ToDecimal(Dgvrenglon.Cells[2].Value.ToString().Replace(" ", String.Empty));
                            ed.idUbicacion = Convert.ToInt32(Dgvrenglon.Cells[4].Value.ToString().Replace(" ", String.Empty));
                            ed.entrada = Convert.ToDecimal(Dgvrenglon.Cells[6].Value.ToString().Replace(" ", String.Empty));/*SUMAR TODOS LOS METROS DE CADA ROLLO PARA HACER LA CONVERSION*/
                            /*CONVERSION A YARDAS AUTOMATICO PERO SOLO DEL TOTAL DE METROS*/
                            double centimetros, pies, pulgadas, yarda;
                            float longMetros = float.Parse(Dgvrenglon.Cells[2].Value.ToString().Replace(" ", String.Empty));// TOTAL DE METROS
                            centimetros = longMetros * 100;
                            pulgadas = centimetros / 2.54;
                            pies = pulgadas / 12;
                            yarda = pies / 3;

                            ed.logYardas = Convert.ToDecimal(yarda);
                            ed.idUsuario_Creacion = u[0].id;
                            idDetalleEntradaTela = f.GuardarEntradaDetalle(ed);
                        }
                    }
                }
                if (idDetalleEntradaTela > 0)
                {
                    MessageBox.Show("Se guardo con Exito!");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void btnEditar_Click(object sender, EventArgs e)
        { try
            {
                if (dgvDetalleEntrada.RowCount > 0)
                {
                    RenglonSelecionado = dgvDetalleEntrada.CurrentCell.RowIndex;
                    DataGridViewRow newDatarow = dgvDetalleEntrada.Rows[RenglonSelecionado];
                    newDatarow.Cells[0].Value = txtNoRollo.Text;
                    newDatarow.Cells[1].Value = txtExistencia.Text;
                    newDatarow.Cells[2].Value = txtLongMts.Text;
                    newDatarow.Cells[3].Value = cmbUbicacion.Text;
                    newDatarow.Cells[4].Value = cmbUbicacion.SelectedValue;
                    newDatarow.Cells[5].Value = txtUbicacion.Text;
                    newDatarow.Cells[6].Value = txtEntrada.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnQuitaDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetalleEntrada.RowCount > 0)
                {
                    RenglonSelecionado = dgvDetalleEntrada.CurrentCell.RowIndex;
                    dgvDetalleEntrada.Rows.RemoveAt(RenglonSelecionado);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtEntrada_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void dgvDetalleEntrada_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                RenglonSelecionado = e.RowIndex;
                DataGridViewRow row = dgvDetalleEntrada.Rows[RenglonSelecionado];
                txtNoRollo.Text = row.Cells[0].Value.ToString();
                txtExistencia.Text = row.Cells[1].Value.ToString();
                txtLongMts.Text = row.Cells[2].Value.ToString();
                cmbUbicacion.Text = row.Cells[3].Value.ToString();
                cmbUbicacion.SelectedValue =Convert.ToInt32(row.Cells[4].Value.ToString());
                txtUbicacion.Text = row.Cells[5].Value.ToString();
                txtEntrada.Text = row.Cells[6].Value.ToString();
                //txtzCantidadCajas.Text = row.Cells[1].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtCostoEstilo_KeyUp(object sender, KeyEventArgs e)
        {


            //this.lblNumero.Text = this.txtCostoEstilo.MaskedTextProvider.ToDisplayString();

        }

        private void txtCostoEstilo_Enter(object sender, EventArgs e)
        {

        }

        private void txtCostoEstilo_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtCostoEstilo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtCostoEstilo_Enter_1(object sender, EventArgs e)
        {
            txtCostoEstilo.SelectionStart = 0;
        }

        private void txtExistencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtExistencia.Text = txtExistencia.Text;
        }

        private void txtExistencia_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // solo 1 punto decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtEntrada_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (int)Keys.Enter)
                {

                    Agregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
