using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class Categoria_D
    {
        DBHelper db = new DBHelper();
        public List<Categoria_E> listarCategorias()
        {
            List<Categoria_E> lista = new List<Categoria_E>();
            string query = "select * from categoria";
            try
            {
                SqlDataReader dr = db.ExecuteReaderNoSp(query, new List<string>());
                while (dr.Read())
                {
                    Categoria_E o = new Categoria_E();
                    if (!dr.IsDBNull(0)) { o.docentry = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { o.tipo = dr.GetString(1); }
                    lista.Add(o);
                }
                dr.Close();
            }
            catch { }
            return lista;
        }
    }

}
