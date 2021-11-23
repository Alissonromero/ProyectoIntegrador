using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class Categoria_N
    {
        Categoria_D categoriaD = new Categoria_D();

        public List<Categoria_E> listarCategorias()
        {
            return categoriaD.listarCategorias();
        }
    }
}
