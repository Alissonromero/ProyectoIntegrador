using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;

namespace Capa_Datos
{
    public class Menu_D
    {
        DBHelper db = new DBHelper();
        public List<Menu_E> listarOpcionesMenu(int IdTipo)
        {
            List<Menu_E> lista = new List<Menu_E>();
            string query = "SELECT t0.* FROM menu t0 inner join acceso_menu t1 on t1.IdMenu = t0.Id where t1.IdRol = @IdTipo";
            try
            {
                SqlDataReader dr = db.ExecuteReaderNoSp(query, new List<string>() { "@IdTipo" }, IdTipo);
                while (dr.Read())
                {
                    Menu_E o = new Menu_E();
                    if (!dr.IsDBNull(0)) { o.Id = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { o.Descripcion = dr.GetString(1); }
                    if (!dr.IsDBNull(2)) { o.NombreOperacion = dr.GetString(2); }
                    lista.Add(o);
                }
                dr.Close();
            }
            catch { }
            return lista;
        }
    }
}
