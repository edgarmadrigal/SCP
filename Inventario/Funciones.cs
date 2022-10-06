using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SCP
{
    public class Funciones
    {


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

        public int GuardarEntrada(ConsultaEntradaResult e)
        {
            int respuesta = 0;
            try
            {
                var consulta = new BDDataContext();
                consulta.GuardarEntrada(e.release,e.idTipoTela,e.idProveedor,e.idCliente,e.idEstilo,e.idEstatus,e.costo, e.ancho,
                                        e.po,e.camion,e.costoFlete,e.CostoImportacion,e.facturaAnterior,e.totalRollos,e.totalYardasRelease,e.fecha, e.idUsuario_Creacion);
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
        public int GuardarEntradaDetalle(ConsultaEntradaDetalleResult ed)
        {
            int respuesta = 0;
            try
            {
             var consulta = new BDDataContext();             
             consulta.GuardarEntradaDetalle(ed.idEntradaTela
            , ed.idUbicacion
            , ed.noRollo
            , ed.existencia
            , ed.logMetros
            , ed.logYardas
            , ed.entrada
            , ed.totalExistenciaYDS
            , ed.totalEntradaYDS
            , ed.stiffness
            , ed.onzas
            , ed.idUsuario_Creacion);
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

        public void ConsultaTipoTela(ComboBox combo)
        {
            try
            {
                BDDataContext consulta = new BDDataContext();
                List<ConsultaTipoTelaResult> po = consulta.ConsultaTipoTela().ToList();

                combo.DisplayMember = "nombre";
                combo.ValueMember = "id";
                combo.DataSource = po;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        public void ConsultaProveedor(ComboBox combo)
        {
            try
            {
                BDDataContext consulta = new BDDataContext();
                List<ConsultaProveedorResult> po = consulta.ConsultaProveedor().ToList();

                combo.DisplayMember = "nombre";
                combo.ValueMember = "id";
                combo.DataSource = po;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        public void ConsultaEstatus(ComboBox combo)
        {
            try
            {
                BDDataContext consulta = new BDDataContext();
                List<ConsultaEstatusResult> po = consulta.ConsultaEstatus().ToList();

                combo.DisplayMember = "nombre";
                combo.ValueMember = "id";
                combo.DataSource = po;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        public void ConsultaEstilo(ComboBox combo)
        {
            try
            {
                BDDataContext consulta = new BDDataContext();
                List<ConsultaEstiloResult> po = consulta.ConsultaEstilo().ToList();

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
                BDDataContext consulta = new BDDataContext();
                List<ConsultaClienteResult> po = consulta.ConsultaCliente().ToList();

                combo.DisplayMember = "nombre";
                combo.ValueMember = "id";
                combo.DataSource = po;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        public void ConsultaUbicacion(ComboBox combo)
        {
            try
            {
                BDDataContext consulta = new BDDataContext();
                List<ConsultaUbicacionResult> po = consulta.ConsultaUbicacion().ToList();

                combo.DisplayMember = "nombre";
                combo.ValueMember = "id";
                combo.DataSource = po;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

    }
}
