using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;

namespace Capa_Negocio
{

    public class Rol_Operacion_N
    {
        Rol_Operacion_D oroD = new Rol_Operacion_D();
        public int verificarAccesoOperacion(int IdTipo, int idOperacion, string nombreOperacion)
        {
            return oroD.verificarAccesoOperacion(IdTipo, idOperacion, nombreOperacion);
        }
    }
    
   
}
