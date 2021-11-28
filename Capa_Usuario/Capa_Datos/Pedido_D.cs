using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class Pedido_D
    {
        Utilitarios_DAO uti = new Utilitarios_DAO(); DBHelper db = new DBHelper();
        public void registrarPedido(Pedido_E obj)
        {
            SqlConnection cn = new SqlConnection(uti.cadSql);
            try
            {
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction("transaccion1");
                try
                {
                    SqlCommand cmd = new SqlCommand("MANT_PED", cn);
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TipoMantenimiento", "A");
                    cmd.Parameters.AddWithValue("@docentry", obj.docentry).Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.AddWithValue("@idproducto", obj.idproducto);
                    cmd.Parameters.AddWithValue("@nomproducto", obj.nomproducto);
                    cmd.Parameters.AddWithValue("@idvendedor", obj.idvendedor);
                    cmd.Parameters.AddWithValue("@vendedor", obj.vendedor);
                    cmd.Parameters.AddWithValue("@idcliente", obj.idcliente);
                    cmd.Parameters.AddWithValue("@nomcliente", obj.nomcliente);
                    cmd.Parameters.AddWithValue("@lugarDestino", obj.lugarDestino);
                    cmd.Parameters.AddWithValue("@precio", obj.precio);
                    cmd.Parameters.AddWithValue("@cantidad ", obj.cantidad);
                    cmd.Parameters.AddWithValue("@descuento", obj.descuento);
                    cmd.Parameters.AddWithValue("@total", obj.total);
                    //generacion de comprobante
                    cmd.Parameters.AddWithValue("@docentry2", obj.docentry2).Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.AddWithValue("@tipo ", obj.tipo);
                    cmd.Parameters.AddWithValue("@modpago", obj.modpago);
                    cmd.Parameters.AddWithValue("@cuotas", obj.cuotas);

                    cmd.ExecuteNonQuery();

                    tran.Commit();
                }
                catch (Exception e) { tran.Rollback(); cn.Close(); throw new Exception("Error en creacion: " + e.Message); }
                cn.Close();
            }
            catch (Exception e2) { cn.Close(); throw new Exception("Error en creacion y conexion: " + e2.Message); }

        }
        public void atenderVenta(int id, string estado, int idcom)
        {
            SqlConnection cn = new SqlConnection(uti.cadSql);
            try
            {
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction("transaccion1");
                try
                {
                    SqlCommand cmd = new SqlCommand("MANT_PED", cn);
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TipoMantenimiento", "E");
                    cmd.Parameters.AddWithValue("@docentry", id).Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@docentry2", idcom).Direction = ParameterDirection.InputOutput;

                    cmd.ExecuteNonQuery();

                    tran.Commit();
                }
                catch (Exception e) { tran.Rollback(); cn.Close(); throw new Exception("Error en creacion: " + e.Message); }
                cn.Close();
            }
            catch (Exception e2) { cn.Close(); throw new Exception("Error en creacion y conexion: " + e2.Message); }

        }
        public void enCaminoVenta(int id, string estado, int idcom)
        {
            SqlConnection cn = new SqlConnection(uti.cadSql);
            try
            {
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction("transaccion1");
                try
                {
                    SqlCommand cmd = new SqlCommand("MANT_PED", cn);
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TipoMantenimiento", "EC");
                    cmd.Parameters.AddWithValue("@docentry", id).Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@docentry2", idcom).Direction = ParameterDirection.InputOutput;

                    cmd.ExecuteNonQuery();

                    tran.Commit();
                }
                catch (Exception e) { tran.Rollback(); cn.Close(); throw new Exception("Error en creacion: " + e.Message); }
                cn.Close();
            }
            catch (Exception e2) { cn.Close(); throw new Exception("Error en creacion y conexion: " + e2.Message); }

        }
        public List<Pedido_E> listarPedidos(Pedido_E filtro, int id)
        {
            List<Pedido_E> lista = new List<Pedido_E>();
            string fil = "";
            
            if (filtro != null)
            {
                if (filtro.docentry >0) { fil += " and p.docentry = '" + filtro.docentry + "'"; }
                if (filtro.vendedor != null) { fil += " and p.vendedor like '%" + filtro.vendedor + "%'"; }
                if (filtro.fechaVacia(filtro.fecreg) != "") { fil += " and convert(date,p.fecreg) = '" + filtro.fechaVacia(filtro.fecreg) + "'"; }
                if (filtro.lugarDestino != null) { fil += " and p.lugarDestino like '%" + filtro.lugarDestino + "%'"; }
                if (filtro.precio >0 ) { fil += " and p.precio= " + filtro.precio; }
                if (filtro.cantidad > 0) { fil += " and p.cantidad = " + filtro.cantidad ; }
                if (filtro.total > 0) { fil += " and p.total= " + filtro.total; }
                if (filtro.estado != null) { fil += " and p.estado ='" + filtro.estado + "'"; }
                if (filtro.tipo != null) { fil += " and c.tipo = '" + filtro.tipo + "'"; }
                if (filtro.modpago != null) { fil += " and c.modpago ='" + filtro.modpago + "'"; }
            }
            string query = "select p.docentry, p.vendedor, p.fecreg, p.lugarDestino, p.precio, p.cantidad, p.total, " +
                "p.estado, c.tipo, c.modpago from pedido p inner join comprobante c on c.idpedido=p.docentry where p.idcliente = "+id+" "+ fil + " order by p.fecreg desc";
            try
            {
                SqlDataReader dr = db.ExecuteReaderNoSp(query, new List<string>());
                while (dr.Read())
                {
                    Pedido_E o = new Pedido_E();
                    if (!dr.IsDBNull(0)) { o.docentry = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { o.vendedor = dr.GetString(1); }
                    if (!dr.IsDBNull(2)) { o.fecreg = dr.GetDateTime(2); }
                    if (!dr.IsDBNull(3)) { o.lugarDestino = dr.GetString(3); }
                    if (!dr.IsDBNull(4)) { o.precio = dr.GetDecimal(4); }
                    if (!dr.IsDBNull(5)) { o.cantidad = dr.GetInt32(5); }
                    if (!dr.IsDBNull(6)) { o.total = dr.GetDecimal(6); }
                    if (!dr.IsDBNull(7)) { o.estado = dr.GetString(7); }
                    if (!dr.IsDBNull(8)) { o.tipo = dr.GetString(8); }
                    if (!dr.IsDBNull(9)) { o.modpago = dr.GetString(9); }
                    lista.Add(o);
                }
                dr.Close();
            }
            catch { }
            return lista;
        }
        public List<Pedido_E> listarPedidosVen(Pedido_E filtro, int id)
        {
            List<Pedido_E> lista = new List<Pedido_E>();
            string fil = "";

            if (filtro != null)
            {
                if (filtro.docentry > 0) { fil += " and p.docentry = '" + filtro.docentry + "'"; }
                if (filtro.nomcliente != null) { fil += " and p.nomcliente like '%" + filtro.nomcliente + "%'"; }
                if (filtro.fechaVacia(filtro.fecreg) != "") { fil += " and convert(date,p.fecreg) = '" + filtro.fechaVacia(filtro.fecreg) + "'"; }
                if (filtro.lugarDestino != null) { fil += " and p.lugarDestino like '%" + filtro.lugarDestino + "%'"; }
                if (filtro.precio > 0) { fil += " and p.precio= " + filtro.precio; }
                if (filtro.cantidad > 0) { fil += " and p.cantidad = " + filtro.cantidad; }
                if (filtro.total > 0) { fil += " and p.total= " + filtro.total; }
                if (filtro.estado != null) { fil += " and p.estado ='" + filtro.estado + "'"; }
                if (filtro.tipo != null) { fil += " and c.tipo = '" + filtro.tipo + "'"; }
                if (filtro.modpago != null) { fil += " and c.modpago ='" + filtro.modpago + "'"; }
            }
            string query = "select p.docentry, p.nomcliente, p.fecreg, p.lugarDestino, p.precio, p.cantidad, p.total, " +
                "p.estado, c.docentry,c.tipo, c.modpago from pedido p inner join comprobante c on c.idpedido=p.docentry where p.idvendedor = " + id + " " + fil + " order by p.fecreg desc";
            try
            {
                SqlDataReader dr = db.ExecuteReaderNoSp(query, new List<string>());
                while (dr.Read())
                {
                    Pedido_E o = new Pedido_E();
                    if (!dr.IsDBNull(0)) { o.docentry = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { o.nomcliente = dr.GetString(1); }
                    if (!dr.IsDBNull(2)) { o.fecreg = dr.GetDateTime(2); }
                    if (!dr.IsDBNull(3)) { o.lugarDestino = dr.GetString(3); }
                    if (!dr.IsDBNull(4)) { o.precio = dr.GetDecimal(4); }
                    if (!dr.IsDBNull(5)) { o.cantidad = dr.GetInt32(5); }
                    if (!dr.IsDBNull(6)) { o.total = dr.GetDecimal(6); }
                    if (!dr.IsDBNull(7)) { o.estado = dr.GetString(7); }
                    if (!dr.IsDBNull(8)) { o.docentry2 = dr.GetInt32(8); }
                    if (!dr.IsDBNull(9)) { o.tipo = dr.GetString(9); }
                    if (!dr.IsDBNull(10)) { o.modpago = dr.GetString(10); }
                    lista.Add(o);
                }
                dr.Close();
            }
            catch { }
            return lista;
        }
    }
}
