using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class Rol_N
    {
        Rol_D rolD = new Rol_D();
        public string buscarRutaManual(int IdTipo)
        {
            return rolD.buscarRutaManual(IdTipo);
        }
        public List<Rol_E> listarRoles()
        {
            return rolD.listarRoles();
        }
    }
}
