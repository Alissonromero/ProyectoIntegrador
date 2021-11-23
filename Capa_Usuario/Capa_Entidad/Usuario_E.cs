using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Usuario_E
    {
        public int docentry { get; set; }
        public string dni { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fecreg { get; set; }
        public DateTime fecnac { get; set; }
        public string direccion { get; set; }
        public string pais { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public int rol { get; set; }
        public string contraseña { get; set; }
        //
        public string opCreacion { get; set; }
        // campos que no son de la tabla
        public string newcontraseña { get; set; }
        public string password { get; set; }
        public string rol2 { get; set; }
        //metodos
        public string fechaVacia(DateTime fecha)
        {
            if (fecha == new DateTime()) { return ""; }
            else { return fecha.ToString("yyyy-MM-dd"); }
        }
    }
}
