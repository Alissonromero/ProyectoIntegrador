using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Producto_E
    {
        public int docentry { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string marca { get; set; }
        public int categoria { get; set; }
        public int subcategoria { get; set; }
        public decimal precio { get; set; }
        public int stock { get; set; }
        public string garantia { get; set; }
        public string color { get; set; }
        public string fotos { get; set; }
        public string fotos2 { get; set; }
        public string fotos3 { get; set; }
        public int idRegistro { get; set; }
        public string opRegistro { get; set; }
        public DateTime tiempoRegistro { get; set; }
        //campos que no son de la tabla 
        public string categoria2 { get; set; }
        public string subcategoria2 { get; set; }
        public string precio2  { get; set; }
        public string direccionPublicador { get; set; }
        
    }
}
