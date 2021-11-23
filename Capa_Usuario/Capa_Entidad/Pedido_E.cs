using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Pedido_E
    {
        public int docentry { get; set; }
        public int idproducto { get; set; }
        public string nomproducto { get; set; }
        public int idvendedor { get; set; }
        public string vendedor { get; set; }
        public DateTime fecreg { get; set; }
        public int idcliente { get; set; }
        public string nomcliente { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public decimal descuento { get; set; }
        public decimal total { get; set; }
        public string estado { get; set; }
        public string lugarDestino { get; set; }
        /*campo calculable*/
        public decimal totalreal { get { return total-descuento; } }
        //comprobante
        public int docentry2 { get; set; }
        public string tipo { get; set; }
        public string modpago { get; set; }
        public int cuotas { get; set; }
        public DateTime fecreg2 { get; set; }
        //metodos
        public string fechaVacia(DateTime fecha)
        {
            if (fecha == new DateTime()) { return ""; }
            else { return fecha.ToString("yyyy-MM-dd"); }
        }
    }
}
