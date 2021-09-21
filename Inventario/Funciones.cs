using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace SCP
{
    public class Funciones
    {

        public void ConsultaUbicacion(ComboBox combo)
        {
            try
            {
                var consulta = new BDDataContext();

                var po = consulta.ubicacion_Entrada_ConsultaUbicacion().ToList();

                combo.DisplayMember = "nombre";
                combo.ValueMember = "id";
                combo.DataSource = po;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        public void ConsultaCliente(ComboBox combo)
        {
            try
            {

                var consulta = new BDDataContext();

                var po = consulta.ConsultaCliente().ToList();

                combo.DisplayMember = "descripcion";
                combo.ValueMember = "numero";
                combo.DataSource = po;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        public void ConsultaTerminado(ComboBox combo)
        {
            try
            {

                var consulta = new BDDataContext();

                var po = consulta.ConsultaTerminado().ToList();

                combo.DisplayMember = "descripcion";
                combo.ValueMember = "numero";
                combo.DataSource = po;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        public void ConsultaFactura(ComboBox combo)
        {
            try
            {

                var consulta = new BDDataContext();

                var po = consulta.ConsultaFactura().ToList();

                combo.DisplayMember = "descripcion";
                combo.ValueMember = "numero";
                combo.DataSource = po;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        public void ConsultaPO(ComboBox combo)
        {
            try
            {

                var consulta = new BDDataContext();

                var po = consulta.ConsultaPO().ToList();

                combo.DisplayMember = "po_numero";
                combo.ValueMember = "po_numero";
                combo.DataSource = po;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        public void ConsultaPOItem(ComboBox combo, string po)
        {
            try
            {

                var consulta = new BDDataContext();

                var poItem = consulta.ConsultaPOItem(po);

                combo.DataSource = poItem;
                combo.DisplayMember = "po_item";
                combo.ValueMember = "po_item";

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        public void ConsultaProductCode(ComboBox combo, string po, string poItem)
        {
            try
            {

                var consulta = new BDDataContext();

                var productCode = consulta.ConsultaProductCode(po, poItem);

                combo.DataSource = productCode;
                combo.DisplayMember = "prod_cd";
                combo.ValueMember = "prod_cd";

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        public void ConsultaSizes(ComboBox combo, string po, string poItem, string prodCd)
        {
            try
            {

                var consulta = new BDDataContext();

                var size = consulta.ConsultaSizes(po, poItem, prodCd);

                combo.DataSource = size;
                combo.DisplayMember = "size";
                combo.ValueMember = "size";

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        public List<ConsultaProductosResult> ConsultaProductos(string po, string poItem, string prodCd, string size)
        {
            List<ConsultaProductosResult> Productonull = null;
            try
            {
                var consulta = new BDDataContext();

                List<ConsultaProductosResult> Producto = consulta.ConsultaProductos(Convert.ToInt64(po), poItem, prodCd, size).ToList();

                return Producto;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return Productonull;
            }
        }
        public List<ConsultaUsuarioResult> ConsultaUsuario(string usuario, string password)
        {
            List<ConsultaUsuarioResult> objusuario = null;
            try
            {
                var consulta = new BDDataContext();

                var objusu = consulta.ConsultaUsuario(usuario, password).ToList();

                return objusu;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return objusuario;
            }

        }
        public List<ConsultaEtiquetaResult> ConsultaEtiqueta(int? id)
        {
            List<ConsultaEtiquetaResult> objusuario = null;
            try
            {
                var consulta = new BDDataContext();

                var objusu = consulta.ConsultaEtiqueta(id).ToList();

                return objusu;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return objusuario;
            }

        }

        public int GuardaInventario(EtiquetaCajaModificada et, int iduser)
        {
            int respuesta = 0;
            try
            {
                var consulta = new BDDataContext();

                GuardarInventarioResult insertInventario = consulta.GuardarInventario(et.id,
                                                                                      et.po,
                                                                                      et.poItem,
                                                                                      et.Cantidad,
                                                                                      et.size_izquierdo,
                                                                                      et.size_derecho,
                                                                                      et.upc, et.Carton,
                                                                                      et.ProductCode,
                                                                                      et.TipoCarton,
                                                                                      iduser,
                                                                                      et.id_cliente,
                                                                                      et.id_factura,
                                                                                      et.id_terminado).FirstOrDefault();
                consulta.SubmitChanges();
                respuesta = Convert.ToInt32(insertInventario.Column1);
            }
            catch (Exception ex)
            {
                respuesta = 0;
                MessageBox.Show(ex.Message);
            }
            return respuesta;
        }

        public List<ConsultaInventarioResult> ConsultaInventario(DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<ConsultaInventarioResult> objInv = null;
            try
            {
                var consulta = new BDDataContext();

                var objin = consulta.ConsultaInventario(fechaInicio, fechaFin).ToList();

                return objin;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return objInv;
            }

        }
        public List<ConsultaInventarioIDResult> ConsultaInventarioID(int id)
        {
            List<ConsultaInventarioIDResult> objInv = null;
            try
            {
                var consulta = new BDDataContext();

                var objin = consulta.ConsultaInventarioID(id).ToList();

                return objin;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return objInv;
            }

        }

        public bool BajaPO(string po, int id_Cliente, int id_Facturacion, int id_Terminado, int iduser)
        {
            bool respuesta = false;
            try
            {
                var consulta = new BDDataContext();

                var respuesta1 = consulta.BajaPO(po, id_Cliente, id_Facturacion, id_Terminado, iduser).FirstOrDefault();
                consulta.SubmitChanges();
                if (respuesta1.Column1 == 0)
                {
                    respuesta = false;

                }
                else
                {
                    respuesta = true;
                }
            }

            catch (Exception ex)
            {
                respuesta = false;
                MessageBox.Show(ex.Message);

            }
            return respuesta;


        }

        public bool BajaCaja(string po, int iduser)
        {
            bool respuesta = false;
            try
            {
                var consulta = new BDDataContext();

                var respuesta1 = consulta.BajaCaja2(po, iduser).FirstOrDefault();
                consulta.SubmitChanges();
                if (respuesta1.Column1 == 0)
                {
                    respuesta = false;

                }
                else
                {
                    respuesta = true;
                }
            }

            catch (Exception ex)
            {
                respuesta = false;
                MessageBox.Show(ex.Message);

            }
            return respuesta;
        }

        public void ConsultaTallas(ComboBox combo)
        {
            try
            {

                var consulta = new BDDataContext();

                var poItem = consulta.ConsultaTallas();

                combo.DataSource = poItem;
                combo.DisplayMember = "size";
                combo.ValueMember = "id";

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        public void ConsultaTipoCaja(ComboBox combo)
        {
            try
            {
                var consulta = new BDDataContext();

                var poItem = consulta.ConsultaTipoCaja();

                combo.DataSource = poItem;
                combo.DisplayMember = "descripcion";
                combo.ValueMember = "id";

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        public List<ubicacion_Entrada_ConsultaUbicacionIDResult> ConsultaUbicacionID(int idUbicacion)
        {
            List<ubicacion_Entrada_ConsultaUbicacionIDResult> objInv = null;
            try
            {
                var consulta = new BDDataContext();
                var objin = consulta.ubicacion_Entrada_ConsultaUbicacionID(idUbicacion).ToList();
                return objin;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return objInv;
            }
        }

        public List<ubicacion_Entrada_ConsultaUbicacionDetalleIDResult> ConsultaUbicacionDetalleID(int idUbicacion)
        {
            List<ubicacion_Entrada_ConsultaUbicacionDetalleIDResult> objInv = null;
            try
            {
                var consulta = new BDDataContext();
                var objin = consulta.ubicacion_Entrada_ConsultaUbicacionDetalleID(idUbicacion).ToList();
                return objin;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return objInv;
            }
        }

        public int GuardaUbicacion(int id, int id_caja, string po, int id_Cliente, int id_Facturacion, int id_Terminado, int iduser)
        {
            int respuesta = 0;
            try
            {
                var consulta = new BDDataContext();
                ubicacion_Entrada_GuardarUbicacionResult insertInventario = consulta.ubicacion_Entrada_GuardarUbicacion(id, id_caja, po, id_Cliente, id_Facturacion, id_Terminado, iduser).FirstOrDefault();
                consulta.SubmitChanges();
                respuesta = insertInventario.Column1;
            }
            catch (Exception ex)
            {
                respuesta = 0;
                MessageBox.Show(ex.Message);
            }
            return respuesta;
        }


        public DataTable ConsultaTablaGeneral(String NombreSP, List<clsParametro> lst)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter miadapter = new SqlDataAdapter();
            SqlConnection MiConexion = new SqlConnection(@"Data Source =AIN-MSSRV\TASQLEXPRESS;Initial Catalog=Inventario;uid=sa;pwd=TANet001");
            DataTable dt = new DataTable();
            SqlDataAdapter da;
            try
            {
                da = new SqlDataAdapter(NombreSP, MiConexion);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (lst != null)
                {
                    for (int i = 0; i < lst.Count; i++)
                    {
                        da.SelectCommand.Parameters.AddWithValue(lst[i].Nombre, lst[i].Valor);
                    }
                }
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public void ConsultaTallas(CheckedListBox clbT)
        {
            try
            {
                var consulta = new BDDataContext();
                List<ConsultaTallasResult> Listtallas = consulta.ConsultaTallas().ToList();

                foreach (ConsultaTallasResult talla in Listtallas)
                {
                    clbT.Items.Add(talla.size);
                    //clbT.fill
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public List<ubicacion_Salida_ConsultaPOTallasCantidadResult> ConsultaPOTallasCantidad(string po, string talla)
        {
            List<ubicacion_Salida_ConsultaPOTallasCantidadResult> objInv = null;
            try
            {
                var consulta = new BDDataContext();

                var objin = consulta.ubicacion_Salida_ConsultaPOTallasCantidad(po, talla).ToList();

                return objin;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return objInv;
            }
        }

        public ubicacion_Entrada_ComprobarCajaResult ComprobarCaja(int idCaja)
        {
            ubicacion_Entrada_ComprobarCajaResult objInv = null;
            try
            {
                var consulta = new BDDataContext();

                var objin = consulta.ubicacion_Entrada_ComprobarCaja(idCaja).FirstOrDefault();

                return objin;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return objInv;
            }
        }

        public int GuardarSalida(int id_caja, int iduser)
        {
            int respuesta = 0;
            try
            {
                var consulta = new BDDataContext();
                consulta.ubicacion_Salida_Guardar(id_caja, iduser);
                consulta.SubmitChanges();
                respuesta = 1;
            }
            catch (Exception ex)
            {
                respuesta = 0;
                MessageBox.Show(ex.Message);
            }
            return respuesta;
        }


        public int Terminar(int iduser)
        {
            int respuesta = 0;
            try
            {
                var consulta = new BDDataContext();
                consulta.ubicacion_Salida_Terminar(iduser);
                consulta.SubmitChanges();
                respuesta = 1;
            }
            catch (Exception ex)
            {
                respuesta = 0;
                MessageBox.Show(ex.Message);
            }
            return respuesta;
        }


        public int MoverUbicacion(int iduser, int idUbicacion, int idCaja)
        {
            int respuesta = 0;
            try
            {
                var consulta = new BDDataContext();
                ubicacion_MoverUbicacion_GuardarResult respuesta1 = consulta.ubicacion_MoverUbicacion_Guardar(iduser, idUbicacion, idCaja).FirstOrDefault();
                consulta.SubmitChanges();
                respuesta = respuesta1.Column1;
            }
            catch (Exception ex)
            {
                respuesta = 0;
                MessageBox.Show(ex.Message);
            }
            return respuesta;
        }



        public int ComprobarCajaPO(string po, string cliente, string factura, string terminado, int idCaja, bool? pOSolamente)
        {
            int respuesta = 0;
            try
            {
                var consulta = new BDDataContext();
                ubicacion_Entrada_ComprobarCajaPOResult respuesta1 = consulta.ubicacion_Entrada_ComprobarCajaPO(idCaja, po, cliente, factura, terminado, pOSolamente).FirstOrDefault();
                consulta.SubmitChanges();
                respuesta = respuesta1.Column1;
            }
            catch (Exception ex)
            {
                respuesta = 0;
                MessageBox.Show(ex.Message);
            }
            return respuesta;
        }

        public List<ubicacion_ReporteAlmacen_ConsultaResult> ConsultaAlmacen(DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<ubicacion_ReporteAlmacen_ConsultaResult> objInv = null;
            try
            {
                var consulta = new BDDataContext();

                var objin = consulta.ubicacion_ReporteAlmacen_Consulta(fechaInicio, fechaFin).ToList();

                return objin;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return objInv;
            }

        }

        public List<ubicacion_ReporteEmbarques_ConsultaResult> ConsultaEmbarques(DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<ubicacion_ReporteEmbarques_ConsultaResult> objInv = null;
            try
            {
                var consulta = new BDDataContext();

                var objin = consulta.ubicacion_ReporteEmbarques_Consulta(fechaInicio, fechaFin).ToList();

                return objin;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return objInv;
            }
        }

        public ubicacion_Dividir_CajaIDResult DividirCaja(int idCaja, int idUser, int cantidad, int restante)
        {
            ubicacion_Dividir_CajaIDResult respuesta = new ubicacion_Dividir_CajaIDResult();
            try
            {
                var consulta = new BDDataContext();
                respuesta = consulta.ubicacion_Dividir_CajaID(idCaja, idUser, cantidad, restante).FirstOrDefault();
                consulta.SubmitChanges();
            }
            catch (Exception ex)
            {
                respuesta = null;
                MessageBox.Show(ex.Message);
            }
            return respuesta;
        }

        public ubicacion_Dividir_ConsultaCajaIDResult ConsultaCajaID(int idCaja)
        {
            ubicacion_Dividir_ConsultaCajaIDResult respuesta = new ubicacion_Dividir_ConsultaCajaIDResult();
            try
            {
                var consulta = new BDDataContext();
                respuesta = consulta.ubicacion_Dividir_ConsultaCajaID(idCaja).FirstOrDefault();
                consulta.SubmitChanges();
            }
            catch (Exception ex)
            {
                respuesta = null;
                MessageBox.Show(ex.Message);
            }
            return respuesta;
        }


    }
}
