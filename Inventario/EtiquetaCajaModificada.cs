using System.Drawing;

namespace SCP
{
    public class EtiquetaCajaModificada
    {

        public EtiquetaCajaModificada()
        {
        }
        public int id { get; set; }

        public int id_cliente { get; set; }

        public int id_factura { get; set; }

        public int id_terminado { get; set; }

        public string cliente { get; set; }

        public string nivel { get; set; }

        public string factura { get; set; }

        public string terminado { get; set; }

        public int id_Inventario { get; set; }

        public Bitmap qr { get; set; }

        public Image codigoBarras { get; set; }

        public System.Nullable<decimal> po { get; set; }
        public System.Nullable<decimal> poInCompleto { get; set; }

        public string poItem { get; set; }

        public System.Nullable<decimal> Cantidad { get; set; }

        public string size_izquierdo { get; set; }

        public string size_derecho { get; set; }

        public string Size { get; set; }

        public string upc { get; set; }

        public System.Nullable<long> Carton { get; set; }

        public string CartonLeft { get; set; }

        public string CartonRight { get; set; }

        public string ProductCode { get; set; }

        public string TipoCarton { get; set; }

        public string usuario { get; set; }

        public System.Nullable<System.DateTime> Fecha { get; set; }
    }
}
