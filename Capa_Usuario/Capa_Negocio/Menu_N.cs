using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class Menu_N
    {
        Menu_D oemeD = new Menu_D();
        public List<Menu_E> listarOpcionesMenu(int IdTipo)
        {
            return oemeD.listarOpcionesMenu(IdTipo);
        }
    }
}
