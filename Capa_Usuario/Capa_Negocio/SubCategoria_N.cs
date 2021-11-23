using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class SubCategoria_N
    {
        SubCategoria_D subcategoriaD = new SubCategoria_D();

        public List<SubCategoria_E> listarSubCategorias()
        {
            return subcategoriaD.listarSubCategorias();
        }
    }
}
