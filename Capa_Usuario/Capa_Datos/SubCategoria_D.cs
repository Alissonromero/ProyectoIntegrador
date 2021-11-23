using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class SubCategoria_D
    {
        DBHelper db = new DBHelper();
        public List<SubCategoria_E> listarSubCategorias()
        {
            List<SubCategoria_E> lista = new List<SubCategoria_E>();
            string query = "select * from subcategoria";
            try
            {
                SqlDataReader dr = db.ExecuteReaderNoSp(query, new List<string>());
                while (dr.Read())
                {
                    SubCategoria_E o = new SubCategoria_E();
                    if (!dr.IsDBNull(0)) { o.docentry = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { o.categoria = dr.GetInt32(1); }
                    if (!dr.IsDBNull(2)) { o.tipo = dr.GetString(2); }
                    lista.Add(o);
                }
                dr.Close();
            }
            catch { }
            return lista;
        }
    }
}
