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


    }
}
