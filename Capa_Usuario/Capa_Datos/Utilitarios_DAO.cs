using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Configuration;

namespace Capa_Datos
{
    public class Utilitarios_DAO
    {
        public string schemaSql;
        public string cadSql;

        public Utilitarios_DAO()
        {
            this.schemaSql = "BD_ComprasYa";
            //this.cadSql = @"Server=.;database=" + this.schemaSql + ";integrated security=true; Min Pool Size=0;Max Pool Size=10024;Pooling=true"; 
            this.cadSql = @"Server=W10P-ALISSON;database=" + this.schemaSql + ";integrated security=true; Min Pool Size=0;Max Pool Size=10024;Pooling=true";
        }
    }
}

