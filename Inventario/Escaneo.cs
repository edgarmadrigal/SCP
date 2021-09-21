using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WMPLib;

namespace SCP
{
    public partial class Escaneo : Form
    {
        Funciones f = new Funciones();
        bool iniciando = true;
        string cantidad = "0";
        int cantidadAnterior = 0;
        string upc = string.Empty;
        WindowsMediaPlayer sonido = new WindowsMediaPlayer();
        List<ConsultaUsuarioResult> usu = null;
        int? id = 0;
        List<int?> anterior = new List<int?>();
        int contador = 0;
        bool agregar = false;
        int id_InventarioAnt = 0;
        int? CantidadDividir = 0;

        // ...

        public Escaneo(List<ConsultaUsuarioResult> usuario)
        {
            try
            {
                InitializeComponent();
                f.ConsultaPO(this.cmbPO);
                cmbPO.SelectedIndex = -1;
                f.ConsultaCliente(this.cmbCliente);
                cmbCliente.SelectedIndex = 0;
                f.ConsultaFactura(this.cmbFactura);

                cmbFactura.SelectedIndex = 0;
                f.ConsultaTerminado(this.cmbTerminado);
                cmbTerminado.SelectedIndex = 0;

                /**/
                f.ConsultaPO(this.cmbPOB);
                cmbPOB.SelectedIndex = 0;
                f.ConsultaCliente(this.cmbClienteB);
                cmbClienteB.SelectedIndex = 0;
                f.ConsultaFactura(this.cmbFacturacionB);
                cmbFacturacionB.SelectedIndex = 0;
                f.ConsultaTerminado(this.cmbTerminadoB);
                cmbTerminadoB.SelectedIndex = 0;


                f.ConsultaTallas(this.cmbSizeA);
                cmbSizeA.SelectedIndex = 0;
                f.ConsultaTipoCaja(this.cmbTipoCajaA);
                cmbTipoCajaA.SelectedIndex = 15;

                /**/
                f.ConsultaPO(this.cmbPoEntradaAlmacen);
                cmbPoEntradaAlmacen.SelectedIndex = 0;
                f.ConsultaCliente(this.cmbClienteEntradaAlmacen);
                cmbClienteEntradaAlmacen.SelectedIndex = 0;
                f.ConsultaTerminado(cmbTerminadoEntradaAlmacen);
                cmbTerminadoEntradaAlmacen.SelectedIndex = 0;
                f.ConsultaFactura(cmbFacturacionEntradaAlmacen);
                cmbFacturacionEntradaAlmacen.SelectedIndex = 0;
                f.ConsultaPO(cmbPOSalidaAlmacen);
                cmbPOSalidaAlmacen.SelectedIndex = 0;
                f.ConsultaTallas(clbT);

                this.usu = usuario;
                try
                {
                    lblVersion.Text = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                    lblVersion2.Text = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                catch (Exception ex) { }
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa

                if (usu[0].perfil == "4" || usu[0].perfil == "3")
                {
                    cmbPoEntradaAlmacen.Enabled = true;
                    cmbClienteEntradaAlmacen.Enabled = true;
                    cmbFacturacionEntradaAlmacen.Enabled = true;
                    cmbTerminadoEntradaAlmacen.Enabled = true;
                    txtUbicacionID.Enabled = true;
                    txtIDCaja.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnSeleccionarTodo.Enabled = true;
                    btnDeseleccionarTodo.Enabled = true;
                    clbT.Enabled = true;
                    cmbPOSalidaAlmacen.Enabled = true;
                    txtIDCajaSalida.Enabled = true;
                    btnGuardarSalida.Enabled = true;
                    btnTerminar.Enabled = true;
                    dgvSalida.Enabled = true;
                    txtNuevaUbicacion.Enabled = true;
                    txtCajaidMover.Enabled = true;
                    txtBajaCajaID.Enabled = true;
                    btnBajaAlmacen.Enabled = true;
                    btnGuardarMovimiento.Enabled = true;
                    dtpFechaInicioAlmacen.Enabled = true;
                    dtpFechaInicioAlmacen.Enabled = true;
                    btnBuscarAlmacen.Enabled = true;
                    dgvReporteAlmacen.Enabled = true;
                    dtpFechaInicioEmbarques.Enabled = true;
                    dtpFechaInicioEmbarques.Enabled = true;
                    btnBuscarEmbarques.Enabled = true;
                    dgReporteEmbarques.Enabled = true;
                    btnImprimirEtiquetaAlmacen.Enabled = true;
                    txtIDReimpresionAlmacen.Enabled = true;
                    txtUbicacionID.Enabled = true;
                    gcReporteAlmacen.Enabled = true;
                    dtpFechaFinalAlmacen.Enabled = true;
                    f.ConsultaUbicacion(txtUbicacionID);
                    txtUbicacionID.SelectedIndex = 0;
                    dtpFechaInicioEmbarques.Enabled = true;
                    dtpFechaFinalEmbarques.Enabled = true;
                    btnBuscarEmbarques.Enabled = true;
                    dgReporteEmbarques.Enabled = true;
                    txtCajaIDDividir.Enabled = true;
                    txtPiezas.Enabled = true;
                    btnGuardarDivision.Enabled = true;
                    txtIDReimpresionAlmacen.Enabled = true;
                    txtUbicacionID.Enabled = true;
                    btnActualizar.Enabled = true;
                }
                if (usu[0].perfil != "12")
                {
                    TabEsaneo.TabPages.RemoveAt(3);
                }
                else
                {

                    btnNuevo.Enabled = true;
                    //btnEliminar.Enabled = true;
                    //btnEditar.Enabled = true;
                    //btnGuardarA.Enabled = true;
                    btnVistaPrevia.Enabled = true;
                    //
                    txtCantidadA.Enabled = true;
                    txtPoNA.Enabled = true;
                    txtUPCA.Enabled = true;
                    txtPoItemNA.Enabled = true;
                    cmbTipoCajaA.Enabled = true;
                    txtPCA.Enabled = true;
                    cmbSizeA.Enabled = true;
                    txtID.Enabled = false;
                }
                iniciando = false;

                ConsultaCajasPO();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public Escaneo()
        {
            try
            {
                InitializeComponent();
                f.ConsultaPO(this.cmbPO);
                cmbPO.SelectedIndex = -1;
                iniciando = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
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

        private void cmbPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!iniciando)
                {
                    iniciando = true;
                    f.ConsultaPOItem(cmbPOItem, cmbPO.Text);
                    cmbPOItem.SelectedIndex = 0;
                    f.ConsultaProductCode(cmbProductCode, cmbPO.Text, cmbPOItem.Text);
                    cmbProductCode.SelectedIndex = 0;
                    f.ConsultaSizes(cmbSizes, cmbPO.Text, cmbPOItem.Text, cmbProductCode.Text);
                    cmbSizes.SelectedIndex = -1;
                    iniciando = false;
                    cmbSizes.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cmbPOItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!iniciando && cmbPO.SelectedIndex > -1 && cmbPOItem.SelectedIndex > -1)
                {
                    iniciando = true;
                    f.ConsultaProductCode(cmbProductCode, cmbPO.Text, cmbPOItem.Text);
                    cmbProductCode.SelectedIndex = 0;
                    cmbProductCode.Focus();
                    iniciando = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnIncompleteCarton_Click(object sender, EventArgs e)
        {
            try
            {
                if (id != 0 && Convert.ToInt32(txtUnitsScan.Text) > 0 && dgvEscan.RowCount > 0)
                {
                    txtUPCScann.Text = string.Empty;
                    txtUPCScann.Focus();
                    List<ConsultaEtiquetaResult> consulta = f.ConsultaEtiqueta(id);


                    List<EtiquetaCajaModificada> listClase = new List<EtiquetaCajaModificada>();
                    EtiquetaCajaModificada clase = new EtiquetaCajaModificada();

                    clase.id = consulta[0].id;
                    clase.po = consulta[0].po;
                    clase.poInCompleto = consulta[0].poInCompleto;
                    clase.poItem = consulta[0].poItem;
                    clase.ProductCode = consulta[0].ProductCode;
                    clase.Size = consulta[0].Size;
                    clase.size_derecho = consulta[0].size_derecho;
                    clase.size_izquierdo = consulta[0].size_izquierdo;
                    clase.TipoCarton = consulta[0].TipoCarton;
                    clase.upc = consulta[0].upc;
                    clase.Fecha = DateTime.Now;
                    clase.CartonLeft = consulta[0].CartonLeft;
                    clase.CartonRight = consulta[0].CartonRight;
                    clase.Cantidad = Convert.ToInt32(txtUnitsScan.Text);
                    clase.Carton = consulta[0].Carton;
                    clase.usuario = usu[0].nombre;
                    clase.id_cliente = cmbCliente.Text == "NA" ? 1 : Convert.ToInt32(cmbCliente.SelectedValue);
                    clase.id_factura = cmbFactura.Text == "NA" ? 1 : Convert.ToInt32(cmbFactura.SelectedValue);
                    clase.id_terminado = cmbTerminado.Text == "NA" ? 1 : Convert.ToInt32(cmbTerminado.SelectedValue);
                    clase.cliente = cmbCliente.Text;
                    clase.factura = cmbFactura.Text;
                    clase.terminado = cmbTerminado.Text;
                    clase.id_Inventario = f.GuardaInventario(clase, this.usu[0].id);
                    //clase.id_Inventario = 116;
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode("?id=" + clase.id_Inventario +
                                                                      "&po=" + consulta[0].po +
                                                                      "&cl=" + cmbCliente.Text +
                                                                      "&fa=" + cmbFactura.Text +
                                                                      "&te=" + cmbTerminado.Text +
                                                                      "&u=" + clase.usuario +
                                                                      "&pc=" + consulta[0].ProductCode +
                                                                      "&c=" + Convert.ToInt32(txtUnitsScan.Text) +
                                                                      "&sz=" + consulta[0].Size +
                                                                      "&fe=" + DateTime.Now,
                                                                      QRCodeGenerator.ECCLevel.Q);

                    cantidadAnterior = Convert.ToInt32(txtUnitsScan.Text);
                    QRCode qrCode = new QRCode(qrCodeData);
                    BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
                    Codigo.IncludeLabel = true;
                    Image codigoBarras = Codigo.Encode(BarcodeLib.TYPE.CODE39, clase.id_Inventario.ToString(), Color.Black, Color.White, 200, 100);
                    clase.qr = qrCode.GetGraphic(20);
                    clase.codigoBarras = codigoBarras;
                    listClase.Add(clase);
                    id_InventarioAnt = clase.id_Inventario;

                    ReporteCaja report = new ReporteCaja();
                    report.DataSource = listClase;
                    // Disable margins warning. 
                    report.PrintingSystem.ShowMarginsWarning = false;
                    ReportPrintTool tool = new ReportPrintTool(report);
                    //tool.ShowPreview();
                    //tool.ShowRibbonPreviewDialog(); // muestra el disenio 
                    //tool.PrintDialog(); //muestra a que impresora se va a mandar
                    tool.Print(); //imprime de golpe
                    contador = 0;
                    dgvEscan.Rows[0].Selected = true;
                    dgvEscan.FirstDisplayedScrollingRowIndex = 0;
                    txtUnitsScan.Text = (0).ToString();
                    txtUnitsRemai.Text = cantidad.ToString();
                    txtUPCScann.Text = string.Empty;
                    txtUPCScann.Focus();


                }
                else
                {
                    sonido.URL = Application.StartupPath + @"\mp3\error.mp3";
                    sonido.controls.play();
                    MessageBox.Show("Favor de escanear");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cmbProductCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!iniciando && cmbPO.SelectedIndex > -1 && cmbPOItem.SelectedIndex > -1 && cmbProductCode.SelectedIndex > -1)
                {
                    iniciando = true;
                    f.ConsultaSizes(cmbSizes, cmbPO.Text, cmbPOItem.Text, cmbProductCode.Text);
                    cmbSizes.SelectedIndex = -1;
                    cmbSizes.Focus();
                    iniciando = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cmbSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!iniciando && cmbPO.SelectedIndex > -1 && cmbPOItem.SelectedIndex > -1 && cmbProductCode.SelectedIndex > -1 && cmbSizes.SelectedIndex > -1)
                {
                    List<ConsultaProductosResult> x = f.ConsultaProductos(cmbPO.Text, cmbPOItem.Text, cmbProductCode.Text, cmbSizes.Text);
                    if (x.Count > 0)
                    {
                        contador = 0;
                        dgvEscan.DataSource = x;
                        txtCartonNumber.Text = x[0].CartonNumber.ToString();
                        txtCartonSize.Text = x[0].Size.ToString();
                        dgvEscan.Columns["Cantidad"].Visible = false;
                        dgvEscan.Columns["ProductCode"].Visible = false;
                        dgvEscan.Columns["ProductCode1"].Visible = false;
                        dgvEscan.Columns["id"].Visible = false;
                        txtSize.Text = x[0].Size.ToString();
                        txtProductCode.Text = x[0].ProductCode.ToString();
                        id = x[0].id;
                        anterior.Add(id);
                        upc = x[0].UPC.ToString();
                        cantidad = x[0].cantidad;
                        txtUnitsReq.Text = cantidad.ToString();
                        txtUnitsScan.Text = "0";
                        txtUnitsRemai.Text = cantidad.ToString();
                        txtCartonRq.Text = "1";
                        txtCartonsPacked.Text = "0";
                        txtCartonsReamaining.Text = "1";
                        txtUPCScann.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtUPCScann_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == (int)Keys.Enter)
                {
                    if (cmbProductCode.Text == "NA")
                    {
                        contador = contador + 1;
                        dgvEscan.Rows[(Convert.ToInt32(txtUnitsScan.Text))].Selected = true;
                        dgvEscan.FirstDisplayedScrollingRowIndex = (Convert.ToInt32(txtUnitsScan.Text));
                        txtUnitsScan.Text = (Convert.ToInt64(txtUnitsScan.Text) + 1).ToString();
                        txtUnitsRemai.Text = (Convert.ToInt64(txtUnitsRemai.Text) - 1).ToString();
                        txtUPCScann.Text = string.Empty;
                        txtUPCScann.Focus();
                    }
                    else
                    {
                        if (Convert.ToInt64(txtUnitsScan.Text) < Convert.ToInt64(txtUnitsReq.Text) && Convert.ToInt64(txtUnitsRemai.Text) != 0 && txtUPCScann.Text == upc)
                        {
                            contador = contador + 1;
                            dgvEscan.Rows[(Convert.ToInt32(txtUnitsScan.Text))].Selected = true;
                            dgvEscan.FirstDisplayedScrollingRowIndex = (Convert.ToInt32(txtUnitsScan.Text));
                            txtUnitsScan.Text = (Convert.ToInt64(txtUnitsScan.Text) + 1).ToString();
                            txtUnitsRemai.Text = (Convert.ToInt64(txtUnitsRemai.Text) - 1).ToString();
                            txtUPCScann.Text = string.Empty;
                            txtUPCScann.Focus();
                            if (Convert.ToInt64(txtUnitsScan.Text) == Convert.ToInt64(txtUnitsReq.Text))
                            {
                                List<ConsultaEtiquetaResult> consulta = f.ConsultaEtiqueta(id);
                                List<EtiquetaCajaModificada> listClase = new List<EtiquetaCajaModificada>();
                                EtiquetaCajaModificada clase = new EtiquetaCajaModificada();
                                clase.id = consulta[0].id;
                                clase.po = consulta[0].po;
                                clase.poInCompleto = consulta[0].poInCompleto;
                                clase.poItem = consulta[0].poItem;
                                clase.ProductCode = consulta[0].ProductCode;
                                clase.Size = consulta[0].Size;
                                clase.size_derecho = consulta[0].size_derecho;
                                clase.size_izquierdo = consulta[0].size_izquierdo;
                                clase.TipoCarton = consulta[0].TipoCarton;
                                clase.upc = consulta[0].upc;
                                clase.Fecha = DateTime.Now;
                                clase.CartonLeft = consulta[0].CartonLeft;
                                clase.CartonRight = consulta[0].CartonRight;
                                clase.Cantidad = consulta[0].Cantidad;
                                clase.Carton = consulta[0].Carton;
                                clase.usuario = usu[0].nombre;
                                clase.id_cliente = cmbCliente.Text == "NA" ? 1 : Convert.ToInt32(cmbCliente.SelectedValue);
                                clase.id_factura = cmbFactura.Text == "NA" ? 1 : Convert.ToInt32(cmbFactura.SelectedValue);
                                clase.id_terminado = cmbTerminado.Text == "NA" ? 1 : Convert.ToInt32(cmbTerminado.SelectedValue);
                                clase.cliente = cmbCliente.Text;
                                clase.factura = cmbFactura.Text;
                                clase.terminado = cmbTerminado.Text;
                                clase.id_Inventario = f.GuardaInventario(clase, this.usu[0].id);

                                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                                QRCodeData qrCodeData = qrGenerator.CreateQrCode("?id=" + clase.id_Inventario +
                                                                          "&po=" + consulta[0].po +
                                                                          "&cl=" + clase.cliente +
                                                                          "&fa=" + clase.factura +
                                                                          "&te=" + clase.terminado +
                                                                          "&u=" + clase.usuario +
                                                                          "&pc=" + consulta[0].ProductCode +
                                                                          "&c=" + Convert.ToInt32(txtUnitsScan.Text) +
                                                                          "&sz=" + consulta[0].Size +
                                                                          "&fe=" + DateTime.Now, QRCodeGenerator.ECCLevel.Q);
                                QRCode qrCode = new QRCode(qrCodeData);
                                BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
                                Codigo.IncludeLabel = true;
                                Image codigoBarras = Codigo.Encode(BarcodeLib.TYPE.CODE39, clase.id_Inventario.ToString(), Color.Black, Color.White, 200, 100);

                                clase.qr = qrCode.GetGraphic(20);
                                clase.codigoBarras = codigoBarras;
                                listClase.Add(clase);
                                id_InventarioAnt = clase.id_Inventario;


                                txtUnitsScan.Text = (0).ToString();
                                txtUnitsRemai.Text = cantidad.ToString();
                                txtUPCScann.Text = string.Empty;
                                txtUPCScann.Focus();

                                ReporteCaja report = new ReporteCaja();
                                report.DataSource = listClase;
                                // Disable margins warning. 
                                report.PrintingSystem.ShowMarginsWarning = false;
                                ReportPrintTool tool = new ReportPrintTool(report);
                                //tool.ShowPreview();
                                tool.Print();
                                //reportPrintTool.Print();
                                //printTool.ShowPreviewDialog();


                            }
                        }
                        else
                        if (Convert.ToInt64(txtUnitsScan.Text) == Convert.ToInt64(txtUnitsReq.Text) && txtUPCScann.Text == upc)
                        {
                            contador = 0;
                            txtUPCScann.Text = string.Empty;
                            txtUnitsReq.Text = cantidad.ToString();
                            txtUnitsScan.Text = "0";
                            txtUnitsRemai.Text = cantidad.ToString();
                            dgvEscan.Rows[(Convert.ToInt32(txtUnitsScan.Text))].Selected = true;
                            dgvEscan.FirstDisplayedScrollingRowIndex = (Convert.ToInt32(txtUnitsScan.Text));
                            txtUnitsScan.Text = (Convert.ToInt64(txtUnitsScan.Text) + 1).ToString();
                            txtUnitsRemai.Text = (Convert.ToInt64(txtUnitsRemai.Text) - 1).ToString();
                            txtCartonRq.Text = "1";
                            txtCartonsPacked.Text = "0";
                            txtCartonsReamaining.Text = "1";
                            txtUPCScann.Focus();
                        }
                        else if (txtUPCScann.Text != upc)
                        {
                            sonido.URL = Application.StartupPath + @"\mp3\error.mp3";
                            sonido.controls.play();
                            txtUPCScann.Text = string.Empty;
                            MessageBox.Show("Favor de Escanear la prenda correcta!");
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void TabEsaneo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.TabEsaneo.SelectedTab.Text.Trim() == "BajaPO")
            {
                if (usu[0].perfil == "1" || usu[0].perfil == "4")
                {
                    cmbPOB.Enabled = true;
                    cmbClienteB.Enabled = true;
                    cmbFacturacionB.Enabled = true;
                    cmbTerminadoB.Enabled = true;
                    btnBajaPO.Enabled = true;
                }
            }
            if (this.TabEsaneo.SelectedTab.Text.Trim() == "BajaCaja")
            {
                if (usu[0].perfil == "1" || usu[0].perfil == "4")
                {
                    txtCaja.Enabled = true;
                    btnBajaCaja.Enabled = true;
                }
            }
            if (this.TabEsaneo.SelectedTab.Text.Trim() == "ABC")
            {
                if (usu[0].perfil == "12")
                {
                    btnNuevo.Enabled = true;
                    //btnEliminar.Enabled = true;
                    //btnEditar.Enabled = true;
                    //btnGuardarA.Enabled = true;
                    btnVistaPrevia.Enabled = true;
                    //
                    txtCantidadA.Enabled = true;
                    txtPoNA.Enabled = true;
                    txtUPCA.Enabled = true;
                    txtPoItemNA.Enabled = true;
                    cmbTipoCajaA.Enabled = true;
                    txtPCA.Enabled = true;
                    cmbSizeA.Enabled = true;
                    txtID.Enabled = false;
                }
                else
                {
                    btnNuevo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnEditar.Enabled = false;
                    btnGuardarA.Enabled = false;
                    btnVistaPrevia.Enabled = false;
                    //
                    txtCantidadA.Enabled = false;
                    txtPoNA.Enabled = false;
                    txtUPCA.Enabled = false;
                    txtPoItemNA.Enabled = false;
                    cmbTipoCajaA.Enabled = false;
                    txtPCA.Enabled = false;
                    cmbSizeA.Enabled = false;
                    txtID.Enabled = false;

                }
            }
        }

        private void btnPrintLast_Click(object sender, EventArgs e)
        {
            try
            {
                //id_InventarioAnt = 102;
                //txtUnitsScan.Text = "5";
                if (id_InventarioAnt != 0 && Convert.ToInt32(txtUnitsScan.Text) > 0)
                {
                    txtUPCScann.Text = string.Empty;
                    txtUPCScann.Focus();
                    List<ConsultaInventarioIDResult> consulta = f.ConsultaInventarioID(id_InventarioAnt);
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();

                    List<EtiquetaCajaModificada> listClase = new List<EtiquetaCajaModificada>();
                    EtiquetaCajaModificada clase = new EtiquetaCajaModificada();

                    clase.po = consulta[0].po;
                    clase.poInCompleto = consulta[0].poInCompleto;
                    clase.poItem = consulta[0].poItem;
                    clase.ProductCode = consulta[0].ProductCode;
                    clase.Size = consulta[0].Size;
                    clase.size_derecho = consulta[0].size_derecho;
                    clase.size_izquierdo = consulta[0].size_izquierdo;
                    clase.TipoCarton = consulta[0].TipoCarton;
                    clase.upc = consulta[0].upc;
                    clase.Fecha = consulta[0].create_dtm;
                    clase.CartonLeft = consulta[0].CartonLeft;
                    clase.CartonRight = consulta[0].CartonRight;
                    clase.Cantidad = consulta[0].Cantidad;
                    clase.Carton = consulta[0].Carton;
                    clase.usuario = consulta[0].usuario;
                    clase.id_Inventario = consulta[0].id;
                    clase.id_cliente = Convert.ToInt32(consulta[0].id_cliente);
                    clase.id_factura = Convert.ToInt32(consulta[0].id_factura);
                    clase.id_terminado = Convert.ToInt32(consulta[0].id_terminado);
                    clase.cliente = consulta[0].cliente;
                    clase.factura = consulta[0].factura;
                    clase.terminado = consulta[0].terminado;
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode("?id=" + clase.id_Inventario +
                                                                      "&po=" + consulta[0].po +
                                                                      "&cl=" + clase.cliente +
                                                                      "&fa=" + clase.factura +
                                                                      "&te=" + clase.terminado +
                                                                      "&u=" + clase.usuario +
                                                                      "&pc=" + consulta[0].ProductCode +
                                                                      "&c=" + clase.Cantidad +
                                                                      "&sz=" + consulta[0].Size +
                                                                      "&fe=" + clase.Fecha, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
                    Codigo.IncludeLabel = true;
                    Image codigoBarras = Codigo.Encode(BarcodeLib.TYPE.CODE39, consulta[0].id.ToString(), Color.Black, Color.White, 200, 100);

                    clase.qr = qrCode.GetGraphic(20);
                    clase.codigoBarras = codigoBarras;


                    listClase.Add(clase);
                    ReporteCaja report = new ReporteCaja();
                    report.DataSource = listClase;
                    // Disable margins warning. 
                    report.PrintingSystem.ShowMarginsWarning = false;
                    ReportPrintTool tool = new ReportPrintTool(report);
                    tool.ShowPreview();
                    //tool.ShowRibbonPreviewDialog(); // muestra el disenio 
                    //tool.PrintDialog(); //muestra a que impresora se va a mandar
                    //tool.Print(); //imprime de golpe
                }
                else
                {
                    MessageBox.Show("Favor de Escanear");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Alta_Click(object sender, EventArgs e)
        {

        }
        #region Comentario
        /*Aqui empieza

        
        public void limpiaTodo()
        {
            try
            {
                txtNombre.Text = "";
                txtDireccion.Text = "";
                txtBuscar.Text = "";
                txtNumeroEstacion.Text = "";
                chbEstatusEstacion.Checked = false;
                dgvResultado.DataSource = null;
                imprimirObjeto = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void limpiaDatos()
        {
            try
            {
                txtNombre.Text = "";
                txtDireccion.Text = "";
                txtNumeroEstacion.Text = "";
                chbEstatusEstacion.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void activarDatos()
        {
            try
            {
                txtNombre.ReadOnly = false;
                txtDireccion.ReadOnly = false;
                chbEstatusEstacion.Enabled = true;

                txtNombre.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void desactivarDatos()
        {
            try
            {
                txtNombre.ReadOnly = true;
                txtDireccion.ReadOnly = true;
                chbEstatusEstacion.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool ValidateForm()
        {
            bool respuesta = false;

            try
            {
                var textBoxes = dgvAlta.Controls.Cast<Control>()
                                         .OfType<TextBox>()
                                         .OrderBy(control => control.TabIndex);

                foreach (var textBox in textBoxes)
                {

                    var fieldName4 = textBox.Name;

                    if (fieldName4 == "txtNumeroComodato"
                        || fieldName4 == "txtBuscar"
                        || fieldName4 == "txtObservaciones"
                        || fieldName4 == "ttxCalle"
                        || fieldName4 == "txtCalle"
                         || fieldName4 == "ttxColonia"
                        || fieldName4 == "txtColonia"
                        || fieldName4 == "txtCiudad"
                        || fieldName4 == "txtNoInterior"
                        || fieldName4 == "ttxNoInterior"
                        || fieldName4 == "txtNoExterior"
                           || fieldName4 == "ttxNoExterior"
                            || fieldName4 == "ttxCiudad"
                             || fieldName4 == "cbmEstado"
                             || fieldName4 == "cmbTipoTanque"
                             || fieldName4 == "cmbMarca"
                             || fieldName4 == "txtNoSerie"
                             || fieldName4 == "txtCapacidad"
                        ) { }

                    else if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        textBox.Focus();
                        var fieldName = textBox.Name.Substring(3);
                        MessageBox.Show((string.Format("Campo '{0}' no debe estar vacio.", fieldName)));
                        return false;
                    }
                }
                respuesta = true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                respuesta = false;
            }

            return respuesta;
        }

        public void buscarDatosBancos()
        {
            try
            {
                panelEstadoActual.Text = "Buscando Registro ...";
                // btnGuardar.Enabled = false;

                //Parametro de búsqueda del StoredProcedure
                string parametroBusqueda = txtBuscar.Text;
                
                    //Llamada al metodo ActualizaTabla
                    funciones.ConsultaTabla(dgvResultado, "spSIAF_mtto_ConsultarEstacion", "Descripcion", parametroBusqueda, empresa.empresa_idEmpresa);
                    if (dgvResultado.RowCount > 0)
                    {

                        panelEstadoActual.Text = "Registro (s) encontrado (s) ...";
                        limpiaDatos();
                    }
                    else
                    {
                        panelEstadoActual.Text = "No se encontró ningún registro ...";
                        limpiaDatos();
                    }
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (permisos.permisoModulo_consulta)
                {
                    buscarDatosBancos();
                }
                else
                {
                    panelEstadoActual.Text = "No tienes los permisos suficientes para realizar esta acción ...";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        public void mostrarDatos()
        {
            try
            {
                limpiaDatos();
                if (imprimirObjeto) { }
                else
                {
                    Modifico = false;
                    txtNumeroEstacion.Text = dgvResultado.CurrentRow.Cells["Numero"].Value.ToString();
                    txtNombre.Text = dgvResultado.CurrentRow.Cells["Nombre"].Value.ToString();
                    txtDireccion.Text = dgvResultado.CurrentRow.Cells["Direccion"].Value.ToString();
                    chbEstatusEstacion.Checked = Convert.ToBoolean(dgvResultado.CurrentRow.Cells["Estatus"].Value);
                    desactivarDatos();
                    panelEstadoActual.Text = "Vizualizando Registro ...";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void dgvEstaciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            mostrarDatos();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (permisos.permisoModulo_consulta)
                {
                    if ((int)e.KeyChar == (int)Keys.Enter)
                    {
                        buscarDatosBancos();
                    }
                }
                else
                {
                    panelEstadoActual.Text = "No tienes los permisos suficientes para realizar esta acción ...";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                Guardar();
            }
        }

        public void Nuevo()
        {
            try
            {
                if (permisos.permisoModulo_alta)
                {
                    limpiaTodo();
                    panelEstadoActual.Text = "Agregando Registro ...";
                    chbEstatusEstacion.Checked = true;
                    activarDatos();
                    Modifico = true;
                    quiereCerrar = false;


                }

                else
                {
                    panelEstadoActual.Text = "No tienes los permisos suficientes para realizar esta acción ...";
                    //txtBuscar.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        public void Editar()
        {
            try
            {
                if (permisos.permisoModulo_cambio)
                {

                    if (txtNumeroEstacion.Text != "")
                    {
                        txtNombre.Focus();
                        panelEstadoActual.Text = "Editando Registro ...";
                        activarDatos();
                        Modifico = true;
                        quiereCerrar = false;
                    }
                    else
                    {
                        txtBuscar.Focus();
                        panelEstadoActual.Text = "Necesita seleccionar un registro para realizar esta acción ...";
                    }


                }
                else
                {
                    panelEstadoActual.Text = "No tienes los permisos suficientes para realizar esta acción ...";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Guardar()
        {
            try
            {
                if (txtNombre.ReadOnly != true)
                {
                    if (ValidateForm())
                    {
                        estacion estacion = new estacion();



                        estacion.estacion_idEstacion = txtNumeroEstacion.Text == "" ? estacion.estacion_idEstacion = 0 : Convert.ToInt32(txtNumeroEstacion.Text);
                        if (chbEstatusEstacion.Checked == true) { estacion.estacion_estatus = true; } else { estacion.estacion_estatus = false; }
                        estacion.estacion_nombre = txtNombre.Text;
                        estacion.estacion_direccion = txtDireccion.Text;
                        estacion.estacion_idEmpresa = empresa.empresa_idEmpresa;

                        if (estacion.estacion_idEstacion == 0 && permisos.permisoModulo_alta)
                        {
                            bool respuesta = funciones.mttoAgregarEstacion(estacion);

                            if (respuesta)
                            {
                                panelEstadoActual.Text = "Registro Agregado Correctamente ...";
                                string parametroBusqueda = txtNombre.Text;
                                funciones.ConsultaTabla(dgvResultado, "spSIAF_mtto_ConsultarEstacion", "Descripcion", parametroBusqueda, empresa.empresa_idEmpresa);
                                limpiaDatos();
                                txtNombre.Focus();
                                Modifico = false;
                                if (quiereCerrar)
                                {
                                    this.Dispose();
                                }


                            }
                        }
                        else if (estacion.estacion_idEstacion != 0 && permisos.permisoModulo_cambio)
                        {


                            bool respuesta = funciones.mttoActualizarEstacion(estacion);

                            if (respuesta)
                            {
                               // panelEstadoActual.Text = "Registro Actualizado Correctamente ...";
                                string parametroBusqueda = txtNombre.Text;
                                funciones.ConsultaTabla(dgvResultado, "spSIAF_mtto_ConsultarEstacion", "Descripcion", parametroBusqueda, empresa.empresa_idEmpresa);
                                limpiaDatos();
                                desactivarDatos();
                                Modifico = false;

                            }

                        }
                        else
                        {
                        }

                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void NuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void EditarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void GuardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Guardar();
        }
        private void dgvResultado_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAlta.Focused)
                mostrarDatos();
        }
*/
        #endregion

        private void btnCancelCarton_Click(object sender, EventArgs e)
        {
            if (dgvEscan.RowCount > 0)
            {
                contador = 0;
                dgvEscan.Rows[0].Selected = true;
                dgvEscan.FirstDisplayedScrollingRowIndex = 0;
                txtUnitsScan.Text = (0).ToString();
                txtUnitsRemai.Text = cantidad.ToString();
                txtUPCScann.Text = string.Empty;
                txtUPCScann.Focus();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Buscar();
            Cursor.Current = Cursors.Default;
        }

        public void Buscar()
        {
            try
            {
                List<ConsultaInventarioResult> inv = f.ConsultaInventario(dtpFechaInicio.Value.Date, dtpFechaFinal.Value.Date);

                gcReporte.DataSource = inv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowGridPreview(DevExpress.XtraGrid.GridControl grid)
        {
            // Check whether or not the Grid Control can be printed. 
            if (!grid.IsPrintingAvailable)
            {
                MessageBox.Show("The 'DevExpress.XtraPrinting' Library is not found", "Error");
                return;
            }
            // Opens the Preview window. 

            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());

            link.Component = grid;

            link.Landscape = true;

            link.ShowPreview();

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ShowGridPreview(gcReporte);
        }

        private void btnImprimirRe_Click(object sender, EventArgs e)
        {
            try
            {
                txtUPCScann.Text = string.Empty;
                txtUPCScann.Focus();
                int idInv = 0;
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                try { idInv = Convert.ToInt32(txtIDReImpresion.Text); } catch (Exception ex) { idInv = 0; }
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa

                List<ConsultaInventarioIDResult> consulta = f.ConsultaInventarioID(idInv);
                if (consulta.Count > 0)
                {
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();

                    List<EtiquetaCajaModificada> listClase = new List<EtiquetaCajaModificada>();
                    EtiquetaCajaModificada clase = new EtiquetaCajaModificada();

                    clase.poInCompleto = consulta[0].poInCompleto;
                    clase.po = consulta[0].po;
                    clase.poItem = consulta[0].poItem;
                    clase.ProductCode = consulta[0].ProductCode;
                    clase.Size = consulta[0].Size;
                    clase.size_derecho = consulta[0].size_derecho;
                    clase.size_izquierdo = consulta[0].size_izquierdo;
                    clase.TipoCarton = consulta[0].TipoCarton;
                    clase.upc = consulta[0].upc;
                    clase.Fecha = consulta[0].create_dtm;
                    clase.CartonLeft = consulta[0].CartonLeft;
                    clase.CartonRight = consulta[0].CartonRight;
                    clase.Cantidad = consulta[0].Cantidad;
                    clase.Carton = consulta[0].Carton;
                    clase.usuario = consulta[0].usuario;
                    clase.id_Inventario = consulta[0].id;
                    clase.id_cliente = Convert.ToInt32(consulta[0].id_cliente);
                    clase.id_factura = Convert.ToInt32(consulta[0].id_factura);
                    clase.id_terminado = Convert.ToInt32(consulta[0].id_terminado);
                    clase.cliente = consulta[0].cliente == string.Empty ? "NA" : consulta[0].cliente;
                    clase.factura = consulta[0].factura == string.Empty ? "NA" : consulta[0].factura;
                    clase.terminado = consulta[0].terminado == string.Empty ? "NA" : consulta[0].terminado;
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode("?id=" + clase.id_Inventario +
                                                                      "&po=" + clase.po +
                                                                      "&cl=" + clase.cliente +
                                                                      "&fa=" + clase.factura +
                                                                      "&te=" + clase.terminado +
                                                                      "&u=" + clase.usuario +
                                                                      "&pc=" + clase.ProductCode +
                                                                      "&c=" + clase.Cantidad +
                                                                      "&sz=" + clase.Size +
                                                                      "&fe=" + clase.Fecha, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
                    Codigo.IncludeLabel = true;
                    Image codigoBarras = Codigo.Encode(BarcodeLib.TYPE.CODE39
                                                       , consulta[0].id.ToString()
                                                       , Color.Black
                                                       , Color.White, 200, 100);

                    clase.qr = qrCode.GetGraphic(20);
                    clase.codigoBarras = codigoBarras;


                    listClase.Add(clase);
                    ReporteCaja report = new ReporteCaja();
                    report.DataSource = listClase;
                    // Disable margins warning. 
                    report.PrintingSystem.ShowMarginsWarning = false;
                    ReportPrintTool tool = new ReportPrintTool(report);
                    //tool.ShowPreview();
                    //tool.ShowRibbonPreviewDialog(); // muestra el disenio 
                    //tool.PrintDialog(); //muestra a que impresora se va a mandar
                    tool.Print(); //imprime de golpe
                    if (cbLimpiar.Checked == true)
                    {
                        txtIDReImpresion.Text = string.Empty;
                        txtIDReImpresion.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Favor de ingresar el numero correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBajaPO_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Seguro que deseas dar de baja el PO " + cmbPOB.Text + " " + cmbClienteB.Text + " " + cmbFacturacionB.Text + " " + cmbTerminadoB.Text, "Baja PO", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    bool baja = f.BajaPO(cmbPOB.Text, Convert.ToInt32(cmbClienteB.SelectedValue), Convert.ToInt32(cmbFacturacionB.SelectedValue), Convert.ToInt32(cmbTerminadoB.SelectedValue), usu[0].id);
                    if (baja)
                    {
                        MessageBox.Show("se elimino correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("el po no existe en la base de datos.");
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Guardar()
        {
            EtiquetaCajaModificada clase = new EtiquetaCajaModificada();

            clase.po = Convert.ToInt32(txtPoNA.Text);
            clase.poItem = txtPoItemNA.Text;
            clase.Cantidad = Convert.ToInt32(txtCantidadA.Text);
            clase.Size = cmbSizeA.SelectedValue.ToString();
            clase.upc = txtUPCA.Text;
            clase.ProductCode = txtProductCode.Text;
            clase.TipoCarton = cmbTipoCajaA.Text;
            clase.Fecha = DateTime.Now;
            //clase.Carton = Convert.ToInt32(txtNumCajaA.Text);
            clase.usuario = usu[0].nombre;
            if (agregar)
            {
                clase.id_Inventario = f.GuardaInventario(clase, this.usu[0].id);


            }
        }

        private void btnBajaCaja_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Seguro que deseas dar de baja la caja " + txtCaja.Text.Trim(), "Baja Caja", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    bool baja = f.BajaCaja(txtCaja.Text, usu[0].id);
                    if (baja)
                    {
                        MessageBox.Show("se elimino correctamente.");
                        txtCaja.Text = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show("la caja no existe en la base de datos.");
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {

        }

        private void cmbPoEntradaAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!iniciando)
            {
                ConsultaCajasPO();
            }
        }
        public void ConsultaCajasPO()
        {
            try
            {
                Cursor.Current = Cursors.Default;
                List<clsParametro> parametro = new List<clsParametro>();
                parametro.Add(new clsParametro("@PO", cmbPoEntradaAlmacen.Text));
                parametro.Add(new clsParametro("@Cliente", cmbClienteEntradaAlmacen.Text == "NA" ? "1" : cmbClienteEntradaAlmacen.SelectedValue.ToString()));
                parametro.Add(new clsParametro("@Factura", cmbFacturacionEntradaAlmacen.Text == "NA" ? "1" : cmbFacturacionEntradaAlmacen.SelectedValue.ToString()));
                parametro.Add(new clsParametro("@Terminado", cmbTerminadoEntradaAlmacen.Text == "NA" ? "1" : cmbTerminadoEntradaAlmacen.SelectedValue.ToString()));
                parametro.Add(new clsParametro("@POSolamente", cbPOSolamente.Checked));

                dgvAlmacen.DataSource = f.ConsultaTablaGeneral("ubicacion_Entrada_ConsultaCajas", parametro);
                dgvAlmacen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                Cursor.Current = Cursors.WaitCursor;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void xtpSalida_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtUbicacionID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                consultaUbicacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtSeccionID_Leave(object sender, EventArgs e)
        {
            try
            {
                consultaUbicacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void consultaUbicacion()
        {
            try
            {
                int ubicacionID = 0;
                if (txtUbicacionID.SelectedValue.ToString() != string.Empty)
                {
                    ubicacionID = Convert.ToInt32(txtUbicacionID.SelectedValue);
                    if (ubicacionID > 0)
                    {
                        List<ubicacion_Entrada_ConsultaUbicacionIDResult> seccionid = f.ConsultaUbicacionID(ubicacionID);
                        if (seccionid.Count > 0)
                        {
                            txtNivel.Text = seccionid[0].nivel.ToString();
                            List<ubicacion_Entrada_ConsultaUbicacionDetalleIDResult> ubicacionDetalle = f.ConsultaUbicacionDetalleID(ubicacionID);
                            dgvUbicacion.DataSource = ubicacionDetalle;
                            dgvUbicacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                            //BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
                            //Codigo.IncludeLabel = true;
                            //Image codigoBarras = Codigo.Encode(BarcodeLib.TYPE.CODE39, txtUbicacionID.Text, Color.Black, Color.White, 350, 200);
                            //pictureBox3.BackgroundImage =codigoBarras;
                        }
                        else { txtNivel.Text = string.Empty; dgvUbicacion.DataSource = null; }
                    }
                }
                else
                {
                    txtNivel.Text = string.Empty;
                    dgvUbicacion.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbClienteEntradaAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!iniciando)
            {
                List<clsParametro> parametro = new List<clsParametro>();
                parametro.Add(new clsParametro("@PO", cmbPoEntradaAlmacen.Text));
                parametro.Add(new clsParametro("@Cliente", cmbClienteEntradaAlmacen.Text == "NA" ? "1" : cmbClienteEntradaAlmacen.SelectedValue.ToString()));
                parametro.Add(new clsParametro("@Factura", cmbFacturacionEntradaAlmacen.Text == "NA" ? "1" : cmbFacturacionEntradaAlmacen.SelectedValue.ToString()));
                parametro.Add(new clsParametro("@Terminado", cmbTerminadoEntradaAlmacen.Text == "NA" ? "1" : cmbTerminadoEntradaAlmacen.SelectedValue.ToString()));
                parametro.Add(new clsParametro("@POSolamente", cbPOSolamente.Checked));
                dgvAlmacen.DataSource = f.ConsultaTablaGeneral("ubicacion_Entrada_ConsultaCajas", parametro);
                dgvAlmacen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }
        }

        private void cmbFacturacionEntradaAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!iniciando)
            {
                List<clsParametro> parametro = new List<clsParametro>();
                parametro.Add(new clsParametro("@PO", cmbPoEntradaAlmacen.Text));
                parametro.Add(new clsParametro("@Cliente", cmbClienteEntradaAlmacen.Text == "NA" ? "1" : cmbClienteEntradaAlmacen.SelectedValue.ToString()));
                parametro.Add(new clsParametro("@Factura", cmbFacturacionEntradaAlmacen.Text == "NA" ? "1" : cmbFacturacionEntradaAlmacen.SelectedValue.ToString()));
                parametro.Add(new clsParametro("@Terminado", cmbTerminadoEntradaAlmacen.Text == "NA" ? "1" : cmbTerminadoEntradaAlmacen.SelectedValue.ToString()));
                parametro.Add(new clsParametro("@POSolamente", cbPOSolamente.Checked));
                dgvAlmacen.DataSource = f.ConsultaTablaGeneral("ubicacion_Entrada_ConsultaCajas", parametro);
                dgvAlmacen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }
        }

        private void cmbTerminadoEntradaAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!iniciando)
            {
                List<clsParametro> parametro = new List<clsParametro>();
                parametro.Add(new clsParametro("@PO", cmbPoEntradaAlmacen.Text));
                parametro.Add(new clsParametro("@Cliente", cmbClienteEntradaAlmacen.Text == "NA" ? "1" : cmbClienteEntradaAlmacen.SelectedValue.ToString()));
                parametro.Add(new clsParametro("@Factura", cmbFacturacionEntradaAlmacen.Text == "NA" ? "1" : cmbFacturacionEntradaAlmacen.SelectedValue.ToString()));
                parametro.Add(new clsParametro("@Terminado", cmbTerminadoEntradaAlmacen.Text == "NA" ? "1" : cmbTerminadoEntradaAlmacen.SelectedValue.ToString()));
                parametro.Add(new clsParametro("@POSolamente", cbPOSolamente.Checked));
                dgvAlmacen.DataSource = f.ConsultaTablaGeneral("ubicacion_Entrada_ConsultaCajas", parametro);
                dgvAlmacen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!iniciando)
            {
                guardarUbicacion();

            }
        }

        private void txtIDCaja_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == (int)Keys.Enter)
                {
                    guardarUbicacion();
                    txtIDCaja.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void guardarUbicacion()
        {
            try
            {
                if (txtUbicacionID.SelectedValue.ToString() != string.Empty && txtIDCaja.Text != string.Empty)
                {
                    if (txtNivel.Text != string.Empty)
                    {
                        int ubicacion_id = Convert.ToInt32(txtUbicacionID.SelectedValue.ToString());
                        int caja_id = Convert.ToInt32(txtIDCaja.Text);
                        int estaentablapo = 0;
                        bool? pOSolamente = false;
                        pOSolamente = cbPOSolamente.Checked;
                        //comprobar si esta en po la caja
                        estaentablapo = f.ComprobarCajaPO(cmbPoEntradaAlmacen.Text
                                         , cmbClienteEntradaAlmacen.Text == "NA" ? "1" : cmbClienteEntradaAlmacen.SelectedValue.ToString()
                                         , cmbFacturacionEntradaAlmacen.Text == "NA" ? "1" : cmbFacturacionEntradaAlmacen.SelectedValue.ToString()
                                         , cmbTerminadoEntradaAlmacen.Text == "NA" ? "1" : cmbTerminadoEntradaAlmacen.SelectedValue.ToString()
                                         , caja_id
                                         , pOSolamente);
                        if (estaentablapo == 1)
                        {
                            ubicacion_Entrada_ComprobarCajaResult comprobar = f.ComprobarCaja(caja_id);
                            if (comprobar.nombre != null)
                            {
                                DialogResult dialogResult =
                                MessageBox.Show("Esta caja " + txtIDCaja.Text + " esta en la ubicacion "
                                                + comprobar.nombre
                                                + " ¿deseas mover a la ubicacion R" + txtUbicacionID.SelectedValue.ToString() + "?"
                                                , "Confirmar Mover Caja"
                                                , MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    //GUARDA UBICACION DE LA CAJA
                                    int r = f.GuardaUbicacion(ubicacion_id
                                                              , caja_id
                                                              , cmbPoEntradaAlmacen.Text
                                                              , (cmbClienteEntradaAlmacen.Text == "NA" ? 1 : Convert.ToInt32(cmbClienteEntradaAlmacen.SelectedValue))
                                                              , (cmbFacturacionEntradaAlmacen.Text == "NA" ? 1 : Convert.ToInt32(cmbFacturacionEntradaAlmacen.SelectedValue))
                                                              , (cmbTerminadoEntradaAlmacen.Text == "NA" ? 1 : Convert.ToInt32(cmbTerminadoEntradaAlmacen.SelectedValue))
                                                              , usu[0].id);

                                    if (r == 0)
                                    {
                                        MessageBox.Show("La capacidad de la ubicacion esta llena!");
                                    }
                                    else
                                    {
                                        //RECARGA TABLA
                                        if (!iniciando)
                                        {
                                            List<clsParametro> parametro = new List<clsParametro>();
                                            parametro.Add(new clsParametro("@PO", cmbPoEntradaAlmacen.Text));
                                            parametro.Add(new clsParametro("@Cliente", cmbClienteEntradaAlmacen.Text == "NA" ? "1" : cmbClienteEntradaAlmacen.SelectedValue.ToString()));
                                            parametro.Add(new clsParametro("@Factura", cmbFacturacionEntradaAlmacen.Text == "NA" ? "1" : cmbFacturacionEntradaAlmacen.SelectedValue.ToString()));
                                            parametro.Add(new clsParametro("@Terminado", cmbTerminadoEntradaAlmacen.Text == "NA" ? "1" : cmbTerminadoEntradaAlmacen.SelectedValue.ToString()));

                                            parametro.Add(new clsParametro("@POSolamente", cbPOSolamente.Checked));
                                            dgvAlmacen.DataSource = f.ConsultaTablaGeneral("ubicacion_Entrada_ConsultaCajas", parametro);
                                            dgvAlmacen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                                            List<ubicacion_Entrada_ConsultaUbicacionDetalleIDResult> ubicacionDetalle = f.ConsultaUbicacionDetalleID(ubicacion_id);
                                            dgvUbicacion.DataSource = ubicacionDetalle;
                                            dgvUbicacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                                        }
                                    }
                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                }
                            }
                            else
                            {
                                //GUARDA UBICACION DE LA CAJA
                                int r = f.GuardaUbicacion(ubicacion_id
                                                          , caja_id
                                                          , cmbPoEntradaAlmacen.Text
                                                          , (cmbClienteEntradaAlmacen.Text == "NA" ? 1 : Convert.ToInt32(cmbClienteEntradaAlmacen.SelectedValue))
                                                          , (cmbFacturacionEntradaAlmacen.Text == "NA" ? 1 : Convert.ToInt32(cmbFacturacionEntradaAlmacen.SelectedValue))
                                                          , (cmbTerminadoEntradaAlmacen.Text == "NA" ? 1 : Convert.ToInt32(cmbTerminadoEntradaAlmacen.SelectedValue))
                                                          , usu[0].id);

                                if (r == 0)
                                {
                                    MessageBox.Show("La capacidad de la ubicacion esta llena!");
                                }
                                else
                                {
                                    //RECARGA TABLA
                                    if (!iniciando)
                                    {
                                        List<clsParametro> parametro = new List<clsParametro>();
                                        parametro.Add(new clsParametro("@PO", cmbPoEntradaAlmacen.Text));
                                        parametro.Add(new clsParametro("@Cliente", cmbClienteEntradaAlmacen.Text == "NA" ? "1" : cmbClienteEntradaAlmacen.SelectedValue.ToString()));
                                        parametro.Add(new clsParametro("@Factura", cmbFacturacionEntradaAlmacen.Text == "NA" ? "1" : cmbFacturacionEntradaAlmacen.SelectedValue.ToString()));
                                        parametro.Add(new clsParametro("@Terminado", cmbTerminadoEntradaAlmacen.Text == "NA" ? "1" : cmbTerminadoEntradaAlmacen.SelectedValue.ToString()));

                                        parametro.Add(new clsParametro("@POSolamente", cbPOSolamente.Checked));
                                        dgvAlmacen.DataSource = f.ConsultaTablaGeneral("ubicacion_Entrada_ConsultaCajas", parametro);
                                        dgvAlmacen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                                        List<ubicacion_Entrada_ConsultaUbicacionDetalleIDResult> ubicacionDetalle = f.ConsultaUbicacionDetalleID(ubicacion_id);
                                        dgvUbicacion.DataSource = ubicacionDetalle;
                                        dgvUbicacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("la Caja no es de este PO!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Favor de escanear o teclear una ubicacion correcta!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == (int)Keys.Enter)
                {
                    txtCajaidMover.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void txtCajaidMover_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == (int)Keys.Enter)
                {
                    //cambiar de ubicacion
                    MoverUbicacion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void clbT_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            if (!iniciando)
            {
                string talla = string.Empty;
                if (e.NewValue == CheckState.Checked)
                {
                    List<clsParametro> parametro = new List<clsParametro>();
                    parametro.Add(new clsParametro("@po", cmbPOSalidaAlmacen.Text));
                    foreach (string s in clbT.CheckedItems)
                    {
                        talla += s + ",";
                    }
                    talla += clbT.SelectedItem.ToString() + ",";
                    parametro.Add(new clsParametro("@talla", talla));
                    DataTable x = f.ConsultaTablaGeneral("ubicacion_Salida_ConsultaPOTallasCantidad", parametro);
                    if (x.Rows.Count > 0)
                    {
                        dgvSalida.DataSource = x;
                        dgvSalida.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
                else
                {
                    List<clsParametro> parametro = new List<clsParametro>();
                    parametro.Add(new clsParametro("@po", cmbPOSalidaAlmacen.Text));
                    List<Object> objChecked = clbT.CheckedItems.Cast<Object>().ToList();

                    objChecked.Remove(clbT.SelectedItem.ToString());

                    foreach (string s in objChecked)
                    {
                        talla += s + ",";
                    }
                    parametro.Add(new clsParametro("@talla", talla));
                    DataTable x = f.ConsultaTablaGeneral("ubicacion_Salida_ConsultaPOTallasCantidad", parametro);
                    if (x.Rows.Count > 0)
                    {
                        dgvSalida.DataSource = x;
                        dgvSalida.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                    else
                    {
                        dgvSalida.DataSource = null;

                    }
                }
            }
        }

        private void cmbPOSalidaAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!iniciando)
                {
                    string talla = string.Empty;
                    if (clbT.CheckedItems.Count > 0)
                    {
                        List<clsParametro> parametro = new List<clsParametro>();
                        parametro.Add(new clsParametro("@po", cmbPOSalidaAlmacen.Text));
                        foreach (string s in clbT.CheckedItems)
                        {
                            talla += s + ",";
                        }
                        parametro.Add(new clsParametro("@talla", talla));
                        DataTable x = f.ConsultaTablaGeneral("ubicacion_Salida_ConsultaPOTallasCantidad", parametro);
                        if (x.Rows.Count > 0)
                        {
                            dgvSalida.DataSource = x;
                            dgvSalida.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                        else
                        {
                            dgvSalida.DataSource = null;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSeleccionarTodo_Click(object sender, EventArgs e)
        {
            try
            {
                ActualizarTablaSalida();
                clbT.SelectedIndex = 0;
                Cursor.Current = Cursors.WaitCursor;
                for (int i = 0; i < clbT.Items.Count; i++)
                {
                    clbT.SetItemChecked(i, true);
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeseleccionarTodo_Click(object sender, EventArgs e)
        {
            try
            {
                ActualizarTablaSalida();
                Cursor.Current = Cursors.WaitCursor;
                for (int i = 0; i < clbT.Items.Count; i++)
                {
                    clbT.SetItemChecked(i, false);
                }
                dgvSalida.DataSource = null;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtIDCajaSalida_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == (int)Keys.Enter)
                {
                    guardarSalida();
                    txtIDCajaSalida.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void guardarSalida()
        {
            if (txtIDCajaSalida.Text != string.Empty)
            {
                int id_caja = txtIDCajaSalida.Text == string.Empty ? 0 : Convert.ToInt32(txtIDCajaSalida.Text);
                f.GuardarSalida(id_caja, usu[0].id);
                ActualizarTablaSalida();
            }
        }

        private void btnGuardarSalida_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                guardarSalida();
                txtIDCajaSalida.Text = string.Empty;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ActualizarTablaSalida()
        {
            try
            {
                if (!iniciando)
                {
                    string talla = string.Empty;
                    if (clbT.CheckedItems.Count > 0)
                    {
                        List<clsParametro> parametro = new List<clsParametro>();
                        parametro.Add(new clsParametro("@po", cmbPOSalidaAlmacen.Text));
                        foreach (string s in clbT.CheckedItems)
                        {
                            talla += s + ",";
                        }
                        parametro.Add(new clsParametro("@talla", talla));
                        DataTable x = f.ConsultaTablaGeneral("ubicacion_Salida_ConsultaPOTallasCantidad", parametro);
                        if (x.Rows.Count > 0)
                        {
                            dgvSalida.DataSource = x;
                            dgvSalida.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                        else
                        {
                            dgvSalida.DataSource = null;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTerminar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                f.Terminar(usu[0].id);
                ActualizarTablaSalida();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuardarMovimiento_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                MoverUbicacion();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void MoverUbicacion()
        {
            if (txtNuevaUbicacion.Text != string.Empty && txtCajaidMover.Text != string.Empty)
            {
                int ubicacionID = txtNuevaUbicacion.Text != string.Empty ? Convert.ToInt32(txtNuevaUbicacion.Text) : 0;
                int cajaid = txtCajaidMover.Text != string.Empty ? Convert.ToInt32(txtCajaidMover.Text) : 0;
                int x = f.MoverUbicacion(usu[0].id, ubicacionID, cajaid);
                if (x == 1)
                {
                    MessageBox.Show("Se cambio correctamente a la ubicacion: " + ubicacionID + " la caja: " + cajaid);
                }
                else
                {
                    MessageBox.Show("La capacidad de la ubicacion esta llena!");
                }
            }
        }

        private void btnBajaAlmacen_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Seguro que deseas dar de baja la caja " + txtBajaCajaID.Text.Trim(), "Baja Caja", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    bool baja = f.BajaCaja(txtBajaCajaID.Text, usu[0].id);
                    if (baja)
                    {
                        MessageBox.Show("se elimino correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("la caja no existe en la base de datos.");
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnImprimirEtiquetaAlmacen_Click(object sender, EventArgs e)
        {
            try
            {
                int id = 0;
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                try { id = Convert.ToInt32(txtIDReimpresionAlmacen.Text); } catch (Exception ex) { id = 0; }
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                List<ubicacion_Entrada_ConsultaUbicacionIDResult> consulta = f.ConsultaUbicacionID(id);

                if (consulta.Count > 0)
                {
                    string nivel = consulta[0].nivel.ToString();
                    BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
                    Codigo.IncludeLabel = true;
                    Image codigoBarras = Codigo.Encode(BarcodeLib.TYPE.CODE39
                                                       , consulta[0].id.ToString()
                                                       , Color.Black
                                                       , Color.White
                                                       , 370
                                                       , 520);
                    List<EtiquetaCajaModificada> lem = new List<EtiquetaCajaModificada>();
                    EtiquetaCajaModificada em = new EtiquetaCajaModificada();
                    em.codigoBarras = codigoBarras;
                    em.nivel = nivel;
                    lem.Add(em);

                    CodigoBarrasAlmacen report = new CodigoBarrasAlmacen();
                    report.DataSource = lem;
                    report.PrintingSystem.ShowMarginsWarning = false;
                    ReportPrintTool tool = new ReportPrintTool(report);
                    tool.ShowPreview();
                    //tool.ShowRibbonPreviewDialog(); // muestra el disenio 
                    //tool.PrintDialog(); //muestra a que impresora se va a mandar
                    //tool.Print(); //imprime de golpe
                    txtIDReimpresionAlmacen.Text = "";
                }
                else
                {
                    MessageBox.Show("Favor de ingresar el numero correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBuscarAlmacen_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                BuscarReporteAlmacen();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void BuscarReporteAlmacen()
        {
            try
            {
                List<ubicacion_ReporteAlmacen_ConsultaResult> inv = f.ConsultaAlmacen(dtpFechaInicioAlmacen.Value.Date, dtpFechaFinalAlmacen.Value.Date);
                gcReporteAlmacen.DataSource = inv;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtUbicacionID_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                consultaUbicacion();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (!iniciando)
                {
                    ConsultaCajasPO();
                    consultaUbicacion();
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtCaja_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if ((int)e.KeyChar == (int)Keys.Enter)
                {
                    DialogResult dialogResult = MessageBox.Show("Seguro que deseas dar de baja la caja " + txtCaja.Text.Trim(), "Baja Caja", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        bool baja = f.BajaCaja(txtCaja.Text, usu[0].id);
                        if (baja)
                        {
                            MessageBox.Show("se elimino correctamente.");
                            txtCaja.Text = string.Empty;
                        }
                        else
                        {
                            MessageBox.Show("la caja no existe en la base de datos.");
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtIDReImpresion_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == (int)Keys.Enter)
                {
                    txtUPCScann.Text = string.Empty;
                    txtUPCScann.Focus();
                    int idInv = 0;
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                    try { idInv = Convert.ToInt32(txtIDReImpresion.Text); } catch (Exception ex) { idInv = 0; }
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa

                    List<ConsultaInventarioIDResult> consulta = f.ConsultaInventarioID(idInv);
                    if (consulta.Count > 0)
                    {
                        QRCodeGenerator qrGenerator = new QRCodeGenerator();

                        List<EtiquetaCajaModificada> listClase = new List<EtiquetaCajaModificada>();
                        EtiquetaCajaModificada clase = new EtiquetaCajaModificada();

                        clase.poInCompleto = consulta[0].poInCompleto;
                        clase.po = consulta[0].po;
                        clase.poItem = consulta[0].poItem;
                        clase.ProductCode = consulta[0].ProductCode;
                        clase.Size = consulta[0].Size;
                        clase.size_derecho = consulta[0].size_derecho;
                        clase.size_izquierdo = consulta[0].size_izquierdo;
                        clase.TipoCarton = consulta[0].TipoCarton;
                        clase.upc = consulta[0].upc;
                        clase.Fecha = consulta[0].create_dtm;
                        clase.CartonLeft = consulta[0].CartonLeft;
                        clase.CartonRight = consulta[0].CartonRight;
                        clase.Cantidad = consulta[0].Cantidad;
                        clase.Carton = consulta[0].Carton;
                        clase.usuario = consulta[0].usuario;
                        clase.id_Inventario = consulta[0].id;
                        clase.id_cliente = Convert.ToInt32(consulta[0].id_cliente);
                        clase.id_factura = Convert.ToInt32(consulta[0].id_factura);
                        clase.id_terminado = Convert.ToInt32(consulta[0].id_terminado);
                        clase.cliente = consulta[0].cliente == string.Empty ? "NA" : consulta[0].cliente;
                        clase.factura = consulta[0].factura == string.Empty ? "NA" : consulta[0].factura;
                        clase.terminado = consulta[0].terminado == string.Empty ? "NA" : consulta[0].terminado;
                        QRCodeData qrCodeData = qrGenerator.CreateQrCode("?id=" + clase.id_Inventario +
                                                                          "&po=" + clase.po +
                                                                          "&cl=" + clase.cliente +
                                                                          "&fa=" + clase.factura +
                                                                          "&te=" + clase.terminado +
                                                                          "&u=" + clase.usuario +
                                                                          "&pc=" + clase.ProductCode +
                                                                          "&c=" + clase.Cantidad +
                                                                          "&sz=" + clase.Size +
                                                                          "&fe=" + clase.Fecha, QRCodeGenerator.ECCLevel.Q);
                        QRCode qrCode = new QRCode(qrCodeData);
                        BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
                        Codigo.IncludeLabel = true;
                        Image codigoBarras = Codigo.Encode(BarcodeLib.TYPE.CODE39
                                                           , consulta[0].id.ToString()
                                                           , Color.Black
                                                           , Color.White, 200, 100);

                        clase.qr = qrCode.GetGraphic(20);
                        clase.codigoBarras = codigoBarras;


                        listClase.Add(clase);
                        ReporteCaja report = new ReporteCaja();
                        report.DataSource = listClase;
                        // Disable margins warning. 
                        report.PrintingSystem.ShowMarginsWarning = false;
                        ReportPrintTool tool = new ReportPrintTool(report);
                        //tool.ShowPreview();
                        //tool.ShowRibbonPreviewDialog(); // muestra el disenio 
                        //tool.PrintDialog(); //muestra a que impresora se va a mandar
                        tool.Print(); //imprime de golpe
                        if (cbLimpiar.Checked == true)
                        {
                            txtIDReImpresion.Text = string.Empty;
                            txtIDReImpresion.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Favor de ingresar el numero correctamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBuscarEmbarques_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (!iniciando)
                {
                    ConsultaReporteEmbarques();
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ConsultaReporteEmbarques()
        {
            try
            {
                List<ubicacion_ReporteEmbarques_ConsultaResult> inv = f.ConsultaEmbarques(dtpFechaInicioEmbarques.Value.Date, dtpFechaFinalEmbarques.Value.Date);
                dgReporteEmbarques.DataSource = inv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtCajaIDDividir_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if ((int)e.KeyChar == (int)Keys.Enter)
                {
                    ConsultaDivision();
                    txtPiezas.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ConsultaDivision()
        {
            try
            {
                if (txtCajaIDDividir.Text != string.Empty)
                {
                    txtRestante.Text = string.Empty; txtPiezas.Text = string.Empty; CantidadDividir = 0;
                    int idCaja = Convert.ToInt32(txtCajaIDDividir.Text);
                    ubicacion_Dividir_ConsultaCajaIDResult r = f.ConsultaCajaID(idCaja);
                    if (r.cantidad > 0)
                    {
                        txtRestante.Text = r.cantidad.ToString();
                        txtPiezas.Text = string.Empty;
                        CantidadDividir = r.cantidad;
                    }
                    else { txtRestante.Text = string.Empty; txtPiezas.Text = string.Empty; CantidadDividir = 0; }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnGuardarDivision_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRestante.Text != "0" && txtRestante.Text != "")
                {
                    DialogResult dialogResult = MessageBox.Show("Seguro que deseas Dividir esta caja " + txtCajaIDDividir.Text, "Cerrar", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (txtPiezas.Text != string.Empty && txtRestante.Text != string.Empty && txtCajaIDDividir.Text != string.Empty)
                        {
                            int idCaja = Convert.ToInt32(txtCajaIDDividir.Text);
                            int piezas = Convert.ToInt32(txtPiezas.Text);
                            int restante = Convert.ToInt32(txtRestante.Text);
                            ubicacion_Dividir_CajaIDResult r = f.DividirCaja(idCaja, usu[0].id, piezas, restante);
                            if (r.Caja1 != null && r.Caja2 != null)
                            {
                                ImprimirEtiquetasDivididas(r.Caja1, r.Caja2);
                                txtCajaIDDividir.Text = "";
                                txtPiezas.Text = "";
                                txtRestante.Text = "";
                            }
                            else
                            {
                                MessageBox.Show("Esta Caja no se a dado entrada en Almacen");
                            }
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                    }
                }
                else
                {
                    MessageBox.Show("Esta Caja no se a dado entrada en Almacen");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ImprimirEtiquetasDivididas(int? caja1, int? caja2)
        {
            try
            {
                txtUPCScann.Text = string.Empty;
                txtUPCScann.Focus();
                int cajaid1 = Convert.ToInt32(caja1);
                int cajaid2 = Convert.ToInt32(caja2);
                List<ConsultaInventarioIDResult> consulta = f.ConsultaInventarioID(cajaid1);
                if (consulta.Count > 0)
                {
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();

                    List<EtiquetaCajaModificada> listClase = new List<EtiquetaCajaModificada>();
                    EtiquetaCajaModificada clase = new EtiquetaCajaModificada();

                    clase.poInCompleto = consulta[0].poInCompleto;
                    clase.po = consulta[0].po;
                    clase.poItem = consulta[0].poItem;
                    clase.ProductCode = consulta[0].ProductCode;
                    clase.Size = consulta[0].Size;
                    clase.size_derecho = consulta[0].size_derecho;
                    clase.size_izquierdo = consulta[0].size_izquierdo;
                    clase.TipoCarton = consulta[0].TipoCarton;
                    clase.upc = consulta[0].upc;
                    clase.Fecha = consulta[0].create_dtm;
                    clase.CartonLeft = consulta[0].CartonLeft;
                    clase.CartonRight = consulta[0].CartonRight;
                    clase.Cantidad = consulta[0].Cantidad;
                    clase.Carton = consulta[0].Carton;
                    clase.usuario = consulta[0].usuario;
                    clase.id_Inventario = consulta[0].id;
                    clase.id_cliente = Convert.ToInt32(consulta[0].id_cliente);
                    clase.id_factura = Convert.ToInt32(consulta[0].id_factura);
                    clase.id_terminado = Convert.ToInt32(consulta[0].id_terminado);
                    clase.cliente = consulta[0].cliente == string.Empty ? "NA" : consulta[0].cliente;
                    clase.factura = consulta[0].factura == string.Empty ? "NA" : consulta[0].factura;
                    clase.terminado = consulta[0].terminado == string.Empty ? "NA" : consulta[0].terminado;
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode("?id=" + clase.id_Inventario +
                                                                      "&po=" + clase.po +
                                                                      "&cl=" + clase.cliente +
                                                                      "&fa=" + clase.factura +
                                                                      "&te=" + clase.terminado +
                                                                      "&u=" + clase.usuario +
                                                                      "&pc=" + clase.ProductCode +
                                                                      "&c=" + clase.Cantidad +
                                                                      "&sz=" + clase.Size +
                                                                      "&fe=" + clase.Fecha, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
                    Codigo.IncludeLabel = true;
                    Image codigoBarras = Codigo.Encode(BarcodeLib.TYPE.CODE39
                                                       , consulta[0].id.ToString()
                                                       , Color.Black
                                                       , Color.White, 200, 100);

                    clase.qr = qrCode.GetGraphic(20);
                    clase.codigoBarras = codigoBarras;


                    listClase.Add(clase);
                    ReporteCaja report = new ReporteCaja();
                    report.DataSource = listClase;
                    // Disable margins warning. 
                    report.PrintingSystem.ShowMarginsWarning = false;
                    ReportPrintTool tool = new ReportPrintTool(report);
                    tool.ShowPreview();
                    //tool.ShowRibbonPreviewDialog(); // muestra el disenio 
                    //tool.PrintDialog(); //muestra a que impresora se va a mandar
                    tool.Print(); //imprime de golpe
                }
                List<ConsultaInventarioIDResult> consulta2 = f.ConsultaInventarioID(cajaid2);
                if (consulta2.Count > 0)
                {
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();

                    List<EtiquetaCajaModificada> listclase2 = new List<EtiquetaCajaModificada>();
                    EtiquetaCajaModificada clase2 = new EtiquetaCajaModificada();

                    clase2.poInCompleto = consulta2[0].poInCompleto;
                    clase2.po = consulta2[0].po;
                    clase2.poItem = consulta2[0].poItem;
                    clase2.ProductCode = consulta2[0].ProductCode;
                    clase2.Size = consulta2[0].Size;
                    clase2.size_derecho = consulta2[0].size_derecho;
                    clase2.size_izquierdo = consulta2[0].size_izquierdo;
                    clase2.TipoCarton = consulta2[0].TipoCarton;
                    clase2.upc = consulta2[0].upc;
                    clase2.Fecha = consulta2[0].create_dtm;
                    clase2.CartonLeft = consulta2[0].CartonLeft;
                    clase2.CartonRight = consulta2[0].CartonRight;
                    clase2.Cantidad = consulta2[0].Cantidad;
                    clase2.Carton = consulta2[0].Carton;
                    clase2.usuario = consulta2[0].usuario;
                    clase2.id_Inventario = consulta2[0].id;
                    clase2.id_cliente = Convert.ToInt32(consulta2[0].id_cliente);
                    clase2.id_factura = Convert.ToInt32(consulta2[0].id_factura);
                    clase2.id_terminado = Convert.ToInt32(consulta2[0].id_terminado);
                    clase2.cliente = consulta2[0].cliente == string.Empty ? "NA" : consulta2[0].cliente;
                    clase2.factura = consulta2[0].factura == string.Empty ? "NA" : consulta2[0].factura;
                    clase2.terminado = consulta2[0].terminado == string.Empty ? "NA" : consulta2[0].terminado;
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode("?id=" + clase2.id_Inventario +
                                                                      "&po=" + clase2.po +
                                                                      "&cl=" + clase2.cliente +
                                                                      "&fa=" + clase2.factura +
                                                                      "&te=" + clase2.terminado +
                                                                      "&u=" + clase2.usuario +
                                                                      "&pc=" + clase2.ProductCode +
                                                                      "&c=" + clase2.Cantidad +
                                                                      "&sz=" + clase2.Size +
                                                                      "&fe=" + clase2.Fecha, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
                    Codigo.IncludeLabel = true;
                    Image codigoBarras2 = Codigo.Encode(BarcodeLib.TYPE.CODE39
                                                       , consulta2[0].id.ToString()
                                                       , Color.Black
                                                       , Color.White, 200, 100);

                    clase2.qr = qrCode.GetGraphic(20);
                    clase2.codigoBarras = codigoBarras2;


                    listclase2.Add(clase2);
                    ReporteCaja report2 = new ReporteCaja();
                    report2.DataSource = listclase2;
                    // Disable margins warning. 
                    report2.PrintingSystem.ShowMarginsWarning = false;
                    ReportPrintTool tool2 = new ReportPrintTool(report2);
                    tool2.ShowPreview();
                    //tool.ShowRibbonPreviewDialog(); // muestra el disenio 
                    //tool.PrintDialog(); //muestra a que impresora se va a mandar
                    tool2.Print(); //imprime de golpe
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtCajaIDDividir_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ConsultaDivision();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPiezas_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPiezas.Text != string.Empty)
                {
                    int piezas = Convert.ToInt32(txtPiezas.Text);
                    if (piezas < CantidadDividir && piezas > -1)
                    {
                        txtRestante.Text = (CantidadDividir - piezas).ToString();
                    }
                    else if (piezas < 0)
                    {
                        txtPiezas.Text = string.Empty;
                        MessageBox.Show("Favor de ingresar una caja correcta");
                    }
                    else
                    {
                        MessageBox.Show("Favor de ingresar un numero de piezas menor de " + CantidadDividir);
                        txtRestante.Text = CantidadDividir.ToString();
                        txtPiezas.Text = string.Empty;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPiezas_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == (int)Keys.Enter)
                {
                    if (txtRestante.Text != "0" && txtRestante.Text != "")
                    {
                        DialogResult dialogResult = MessageBox.Show("Seguro que deseas Dividir esta caja " + txtCajaIDDividir.Text, "Cerrar", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            if (txtPiezas.Text != string.Empty && txtRestante.Text != string.Empty && txtCajaIDDividir.Text != string.Empty)
                            {
                                int idCaja = Convert.ToInt32(txtCajaIDDividir.Text);
                                int piezas = Convert.ToInt32(txtPiezas.Text);
                                int restante = Convert.ToInt32(txtRestante.Text);
                                ubicacion_Dividir_CajaIDResult r = f.DividirCaja(idCaja, usu[0].id, piezas, restante);
                                if (r.Caja1 != null && r.Caja2 != null)
                                {
                                    ImprimirEtiquetasDivididas(r.Caja1, r.Caja2);
                                    txtCajaIDDividir.Text = "";
                                    txtPiezas.Text = "";
                                    txtRestante.Text = "";
                                }
                                else
                                {
                                    MessageBox.Show("Esta Caja no se a dado entrada en Almacen");
                                }
                            }
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                        }
                    }
                    else
                    {
                        MessageBox.Show("Esta Caja no se a dado entrada en Almacen");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void xtpDividirCaja_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGuardarA_Click(object sender, EventArgs e)
        {

        }

        private void dgvAlta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtNuevaUbicacion_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbPOSolamente_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!iniciando)
                {
                    ConsultaCajasPO();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtUbicacionID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
