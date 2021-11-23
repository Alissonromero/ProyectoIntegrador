using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Capa_Entidad;
namespace Capa_Datos
{
    public class Producto_D
    {
        DBHelper db = new DBHelper(); Utilitarios_DAO uti = new Utilitarios_DAO();
        public List<Producto_E> listarProductos(Producto_E filtro)
        {
            List<Producto_E> lista = new List<Producto_E>();
            string fil = "";
            if (filtro != null)
            {
                if (filtro.nombre != null) { fil += " and p.nombre like '%" + filtro.nombre + "%'"; }
                if (filtro.marca != null) { fil += " and p.marca like '%" + filtro.marca + "%'"; }
                if (filtro.categoria2 != null) { fil += " and c.tipo = '" + filtro.categoria2 + "'"; }
                if (filtro.subcategoria2 != null) { fil += " and s.tipo = '" + filtro.subcategoria2 + "'"; }
                if (filtro.descripcion != null) { fil += " and p.descripcion like '%" + filtro.descripcion + "%'"; }
                if (filtro.precio2 != null) { fil += " and p.precio between " + filtro.precio2; }
                if (filtro.stock > 0) { fil += " and p.stock=" + filtro.stock; }
            }
            string query = "select p.docentry, p.nombre, p.descripcion,p.marca, c.tipo, s.tipo, p.precio, p.stock, p.fotos " +
                "from producto p  inner join categoria c on p.categoria = c.docentry inner join subcategoria s " +
                "on s.docentry = p.subcategoria and s.categoria = p.categoria  where p.docentry>0 " + fil + " order by docentry desc";
            try
            {
                SqlDataReader dr = db.ExecuteReaderNoSp(query, new List<string>());
                while (dr.Read())
                {
                    Producto_E o = new Producto_E();
                    if (!dr.IsDBNull(0)) { o.docentry = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { o.nombre = dr.GetString(1); }
                    if (!dr.IsDBNull(2)) { o.descripcion = dr.GetString(2); }
                    if (!dr.IsDBNull(3)) { o.marca = dr.GetString(3); }
                    if (!dr.IsDBNull(4)) { o.categoria2 = dr.GetString(4); }
                    if (!dr.IsDBNull(5)) { o.subcategoria2 = dr.GetString(5); }
                    if (!dr.IsDBNull(6)) { o.precio = dr.GetDecimal(6); }
                    if (!dr.IsDBNull(7)) { o.stock = dr.GetInt32(7); }
                    if (!dr.IsDBNull(8)) { o.fotos = dr.GetString(8); }
                    lista.Add(o);
                }
                dr.Close();
            }
            catch { }
            return lista;
        }
        public List<Producto_E> listarProductosUsuario(int id)
        {
            List<Producto_E> lista = new List<Producto_E>();
            
            string query = "select p.docentry, p.nombre, p.descripcion,p.marca, c.tipo, s.tipo, p.precio, p.stock, p.fotos " +
                "from producto p  inner join categoria c on p.categoria = c.docentry inner join subcategoria s " +
                "on s.docentry = p.subcategoria and s.categoria = p.categoria  where p.docentry>0  and p.idRegistro=" + id + " order by docentry desc";
            try
            {
                SqlDataReader dr = db.ExecuteReaderNoSp(query, new List<string>());
                while (dr.Read())
                {
                    Producto_E o = new Producto_E();
                    if (!dr.IsDBNull(0)) { o.docentry = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { o.nombre = dr.GetString(1); }
                    if (!dr.IsDBNull(2)) { o.descripcion = dr.GetString(2); }
                    if (!dr.IsDBNull(3)) { o.marca = dr.GetString(3); }
                    if (!dr.IsDBNull(4)) { o.categoria2 = dr.GetString(4); }
                    if (!dr.IsDBNull(5)) { o.subcategoria2 = dr.GetString(5); }
                    if (!dr.IsDBNull(6)) { o.precio = dr.GetDecimal(6); }
                    if (!dr.IsDBNull(7)) { o.stock = dr.GetInt32(7); }
                    if (!dr.IsDBNull(8)) { o.fotos = dr.GetString(8); }
                    lista.Add(o);
                }
                dr.Close();
            }
            catch { }
            return lista;
        }
        public void registrarProducto(Producto_E filtro, HttpPostedFileBase archivo, HttpPostedFileBase archivo2, HttpPostedFileBase archivo3)
        {
            SqlConnection cn = new SqlConnection(uti.cadSql);
            try
            {
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction("transaccion1");
                try
                {
                    string ruta_img = "~/images/" + Path.GetFileName(archivo.FileName);
                    string ruta_img_2 = "~/images/" + Path.GetFileName(archivo2.FileName);
                    string ruta_img_3 = "~/images/" + Path.GetFileName(archivo3.FileName);
                    SqlCommand cmd = new SqlCommand("MANT_PRO", cn);
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TipoMantenimiento", "A");
                    cmd.Parameters.AddWithValue("@docentry", filtro.docentry).Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.AddWithValue("@nombre", filtro.nombre);
                    cmd.Parameters.AddWithValue("@descripcion", filtro.descripcion);
                    cmd.Parameters.AddWithValue("@marca", filtro.marca);
                    cmd.Parameters.AddWithValue("@categoria", filtro.categoria);
                    cmd.Parameters.AddWithValue("@subcategoria", filtro.subcategoria);
                    cmd.Parameters.AddWithValue("@precio", filtro.precio);
                    cmd.Parameters.AddWithValue("@stock", filtro.stock);
                    cmd.Parameters.AddWithValue("@garantia", filtro.garantia);
                    cmd.Parameters.AddWithValue("@color", filtro.color);
                    cmd.Parameters.AddWithValue("@fotos", ruta_img);
                    cmd.Parameters.AddWithValue("@fotos2", ruta_img_2);
                    cmd.Parameters.AddWithValue("@fotos3", ruta_img_3);
                    cmd.Parameters.AddWithValue("@idRegistro", filtro.idRegistro);
                    cmd.Parameters.AddWithValue("@opRegistro", filtro.opRegistro);
                    cmd.ExecuteNonQuery();
                    
                    tran.Commit();
                }
                catch (Exception e) { tran.Rollback(); cn.Close(); throw new Exception("Error en creacion: " + e.Message); }
                cn.Close();
            }
            catch (Exception e2) { cn.Close();  throw new Exception("Error en creacion y conexion: " + e2.Message); }
            
        }
        public void editarProducto(Producto_E obj)
        {
            SqlConnection cn = new SqlConnection(uti.cadSql);
            try
            {
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction("transaccion1");
                try
                {
                    SqlCommand cmd = new SqlCommand("MANT_PRO", cn);
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TipoMantenimiento", "U");
                    cmd.Parameters.AddWithValue("@docentry", obj.docentry);
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("@marca", obj.marca);
                    cmd.Parameters.AddWithValue("@precio", obj.precio);
                    cmd.Parameters.AddWithValue("@stock", obj.stock);
                    cmd.Parameters.AddWithValue("@garantia", obj.garantia);
                    cmd.Parameters.AddWithValue("@color", obj.color);
                    cmd.ExecuteNonQuery();
                    tran.Commit();
                }
                catch (Exception e) { tran.Rollback(); cn.Close(); throw new Exception("Error en creacion: " + e.Message); }
                cn.Close();
            }
            catch (Exception e2) { cn.Close(); throw new Exception("Error en creacion y conexion: " + e2.Message); }
        }
        public Producto_E buscarProducto(int id)
        {
            Producto_E o = null;
            string query = "select p.docentry, p.nombre, p.descripcion,p.marca, c.tipo, "+
                            "s.tipo, p.precio, p.stock, p.garantia, p.color, p.fotos,p.fotos2,p.fotos3, p.idRegistro ,p.opRegistro,p.tiempoRegistro, u.direccion " +
                            "from producto p inner join categoria c on p.categoria = c.docentry inner join "+
                            "subcategoria s on s.docentry = p.subcategoria and s.categoria = p.categoria "+
                            "inner join usuario u on u.docentry = p.idRegistro where p.docentry=@DocEntry";
            try
            {
                SqlDataReader dr = db.ExecuteReaderNoSp(query, new List<string>() { "@DocEntry" }, id);
                dr.Read();
                o = new Producto_E();
                if (!dr.IsDBNull(0)) { o.docentry = dr.GetInt32(0); }
                if (!dr.IsDBNull(1)) { o.nombre = dr.GetString(1); }
                if (!dr.IsDBNull(2)) { o.descripcion = dr.GetString(2); }
                if (!dr.IsDBNull(3)) { o.marca = dr.GetString(3); }
                if (!dr.IsDBNull(4)) { o.categoria2 = dr.GetString(4); }
                if (!dr.IsDBNull(5)) { o.subcategoria2 = dr.GetString(5); }
                if (!dr.IsDBNull(6)) { o.precio = dr.GetDecimal(6); }
                if (!dr.IsDBNull(7)) { o.stock = dr.GetInt32(7); }
                if (!dr.IsDBNull(8)) { o.garantia = dr.GetString(8); }
                if (!dr.IsDBNull(9)) { o.color = dr.GetString(9); } 
                if (!dr.IsDBNull(10)) { o.fotos = dr.GetString(10); }
                if (!dr.IsDBNull(11)) { o.fotos2 = dr.GetString(11); }
                if (!dr.IsDBNull(12)) { o.fotos3 = dr.GetString(12); }
                if (!dr.IsDBNull(13)) { o.idRegistro = dr.GetInt32(13); }
                if (!dr.IsDBNull(14)) { o.opRegistro = dr.GetString(14); }
                if (!dr.IsDBNull(15)) { o.tiempoRegistro = dr.GetDateTime(15); }
                if (!dr.IsDBNull(16)) { o.direccionPublicador = dr.GetString(16); }
                dr.Close();

                
               
            }
            catch { }
            return o;
        }
        
    }
    }


        
                               

                                
                                    

                                    
                             
