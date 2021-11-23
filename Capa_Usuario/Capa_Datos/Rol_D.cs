using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Datos
{
    public class Rol_D
    {
        DBHelper db = new DBHelper();

        public string buscarRutaManual(int IdTipo)
        {
            Rol_E o = new Rol_E();
            string query = "select rutaManual from rol where docentry=" + IdTipo;
            try
            {
                SqlDataReader dr = db.ExecuteReaderNoSp(query);
                dr.Read();
                if (!dr.IsDBNull(0)) { o.rutaManual = dr.GetString(0); }
                dr.Close();
            }
            catch
            { }
            return o.rutaManual;

        }
        public List<Rol_E> listarRoles()
        {
            List<Rol_E> lista = new List<Rol_E>();
            string query = "select * from rol";
            try
            {
                SqlDataReader dr = db.ExecuteReaderNoSp(query, new List<string>() );
                while (dr.Read())
                {
                    Rol_E o = new Rol_E();
                    if (!dr.IsDBNull(0)) { o.docentry = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { o.rol = dr.GetString(1); }
                    
                    lista.Add(o);
                }
                dr.Close();
            }
            catch { }
            return lista;
        }


    }
}