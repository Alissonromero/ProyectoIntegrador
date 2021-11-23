using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class Rol_Operacion_D { 
    DBHelper db = new DBHelper();
    public int verificarAccesoOperacion(int IdTipo, int idOperacion, string nombreOperacion)
    {
        registrarOperacion(idOperacion, nombreOperacion);
        int result = -1;
        string query = "SELECT COUNT(*) FROM Rol_Operacion WHERE IdRol=" + IdTipo + " AND IdOperacion=" + idOperacion;
        try
        {
            SqlDataReader dr = db.ExecuteReaderNoSp(query);
            dr.Read();
            result = dr.GetInt32(0);
            dr.Close();
        }
        catch { }
        return result;
    }
    private void registrarOperacion(int idOperacion, string operacion)
    {
        int existOpe = 1;
        string query = "";
        query = "select count(*) from operacion where Id=" + idOperacion;
        try
        {
            existOpe = int.Parse(db.ExecuteScalarNoSp(query).ToString());
        }
        catch { existOpe = -1; }
        if (existOpe == 0 && idOperacion > 0)
        {
            query = "insert into operacion values(@idOperation,@operacion)";
            try
            {
                db.ExecuteNonQueryTrxNoSp(query, new List<string> { "@idOperation", "@operacion" }, idOperacion, operacion);
            }
            catch { }
        }
    }
}
}

