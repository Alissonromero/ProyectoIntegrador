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
    public  class Usuario_D
    {
        Utilitarios_DAO uti = new Utilitarios_DAO(); DBHelper db = new DBHelper();
        public List<Usuario_E> listarUsuarios()
        {
            List<Usuario_E> lista = new List<Usuario_E>();

            string query = "select top 15 t0.docentry, t0.dni, t0.nombre, t0.apellido, t0.fecnac, t0.direccion, t0.pais, t0.telefono, t0.correo, t1.rol from usuario t0 "+
                "inner join rol t1 on t1.docentry = t0.rol where 1=1  order by 2 desc";
            try
            {
                SqlDataReader dr = db.ExecuteReaderNoSp(query, new List<string>());
                while (dr.Read())
                {
                    Usuario_E o = new Usuario_E();
                    if (!dr.IsDBNull(0)) { o.docentry = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { o.dni = dr.GetString(1); }
                    if (!dr.IsDBNull(2)) { o.nombre = dr.GetString(2); }
                    if (!dr.IsDBNull(3)) { o.apellido = dr.GetString(3); }
                    if (!dr.IsDBNull(4)) { o.fecnac = dr.GetDateTime(4); }
                    if (!dr.IsDBNull(5)) { o.direccion = dr.GetString(5); }
                    if (!dr.IsDBNull(6)) { o.pais = dr.GetString(6); }
                    if (!dr.IsDBNull(7)) { o.telefono = dr.GetString(7); }
                    if (!dr.IsDBNull(8)) { o.correo = dr.GetString(8); }
                    if (!dr.IsDBNull(9)) { o.rol2 = dr.GetString(9); }
                     
                    lista.Add(o);
                }
                dr.Close();
            }
            catch { }
            return lista;
        }
        public Usuario_E buscarUsuario(int id)
        {
            Usuario_E o = null;
            string query = "select * from usuario where docentry=@id";
            try
            {
                SqlDataReader dr = db.ExecuteReaderNoSp(query, new List<string>() { "@id" }, id);
                dr.Read();
                o = new Usuario_E();
                if (!dr.IsDBNull(0)) { o.docentry = dr.GetInt32(0); }
                if (!dr.IsDBNull(1)) { o.dni = dr.GetString(1); }
                if (!dr.IsDBNull(2)) { o.nombre = dr.GetString(2); }
                if (!dr.IsDBNull(3)) { o.apellido = dr.GetString(3); }
                if (!dr.IsDBNull(4)) { o.fecreg = dr.GetDateTime(4); }
                if (!dr.IsDBNull(5)) { o.fecnac = dr.GetDateTime(5); }
                if (!dr.IsDBNull(6)) { o.direccion = dr.GetString(6); }
                if (!dr.IsDBNull(7)) { o.pais = dr.GetString(7); }
                if (!dr.IsDBNull(8)) { o.telefono = dr.GetString(8); }
                if (!dr.IsDBNull(9)) { o.correo = dr.GetString(9); }
                if (!dr.IsDBNull(10)) { o.rol = dr.GetInt32(10); }
                if (!dr.IsDBNull(11)) { o.opCreacion = dr.GetString(11); }
                if (!dr.IsDBNull(12)) { o.contraseña = dr.GetString(12); }

                dr.Close();
            }
            catch { return null; }
            return o;
        }
        public Usuario_E buscarUsuarioDni(string dni)
        {
            Usuario_E o = null;
            string query = "select * from usuario where dni=@dni";
            try
            {
                SqlDataReader dr = db.ExecuteReaderNoSp(query, new List<string>() { "@dni" }, dni);
                dr.Read();
                o = new Usuario_E();
                if (!dr.IsDBNull(0)) { o.docentry = dr.GetInt32(0); }
                if (!dr.IsDBNull(1)) { o.dni = dr.GetString(1); }
                if (!dr.IsDBNull(2)) { o.nombre = dr.GetString(2); }
                if (!dr.IsDBNull(3)) { o.apellido = dr.GetString(3); }
                if (!dr.IsDBNull(4)) { o.fecreg = dr.GetDateTime(4); }
                if (!dr.IsDBNull(5)) { o.fecnac = dr.GetDateTime(5); }
                if (!dr.IsDBNull(6)) { o.direccion = dr.GetString(6); }
                if (!dr.IsDBNull(7)) { o.pais = dr.GetString(7); }
                if (!dr.IsDBNull(8)) { o.telefono = dr.GetString(8); }
                if (!dr.IsDBNull(9)) { o.correo = dr.GetString(9); }
                if (!dr.IsDBNull(10)) { o.rol = dr.GetInt32(10); }
                if (!dr.IsDBNull(11)) { o.opCreacion = dr.GetString(11); }
                if (!dr.IsDBNull(12)) { o.contraseña = dr.GetString(12); }

                dr.Close();
            }
            catch { return null; }
            return o;
        }
        public Usuario_E buscarUsuarioLogueo(Usuario_E user)
        {
            Usuario_E obj = null;
            string query = "select docentry, dni, nombre, apellido,rol from usuario where dni = @CodUser and contraseña = @Contraseña";
            try
            {

                SqlDataReader dr = db.ExecuteReaderNoSp(query, new List<string>() { "@CodUser", "@Contraseña" }
                , user.dni, user.contraseña);
                dr.Read();
                obj = new Usuario_E();
                if (!dr.IsDBNull(0)) { obj.docentry = dr.GetInt32(0); }
                if (!dr.IsDBNull(1)) { obj.dni = dr.GetString(1); }
                if (!dr.IsDBNull(2)) { obj.nombre = dr.GetString(2); }
                if (!dr.IsDBNull(3)) { obj.apellido = dr.GetString(3); }
                if (!dr.IsDBNull(4)) { obj.rol = dr.GetInt32(4); }
                dr.Close();
                return obj;
            }
            catch (Exception)
            {
                throw new Exception("Usuario o contraseña no validos");

            }

        }
        public int registrarNuevoUsuario(Usuario_E obj)
        {
            int status = -1;
            SqlConnection cn = new SqlConnection(uti.cadSql);
            try
            {
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction("transaccion1");
                try
                {
                    SqlCommand cmd = new SqlCommand("MANT_USU", cn);
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TipoMantenimiento", "A");
                    cmd.Parameters.AddWithValue("@docentry", obj.docentry).Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.AddWithValue("@dni", obj.dni);
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("@apellido", obj.apellido);
                    cmd.Parameters.AddWithValue("@fecnac", obj.fecnac);
                    cmd.Parameters.AddWithValue("@direccion", obj.direccion);
                    cmd.Parameters.AddWithValue("@pais", obj.pais);
                    cmd.Parameters.AddWithValue("@telefono", obj.telefono);
                    cmd.Parameters.AddWithValue("@correo", obj.correo);
                    cmd.Parameters.AddWithValue("@rol", obj.rol);
                    cmd.Parameters.AddWithValue("@opCreacion", obj.opCreacion);
                    cmd.Parameters.AddWithValue("@contraseña", obj.contraseña);
                    

                    cmd.ExecuteNonQuery();
                    
                    tran.Commit();
                }
                catch (Exception e) { tran.Rollback(); cn.Close(); throw new Exception("Error en creacion: " + e.Message); }
                cn.Close();
            }
            catch (Exception e2) { cn.Close(); status = 0; throw new Exception("Error en creacion y conexion: " + e2.Message); }
            return status;
        }
        public int registrarNuevoUsuarioVisitante(Usuario_E obj)
        {
            int status = -1;
            SqlConnection cn = new SqlConnection(uti.cadSql);
            try
            {
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction("transaccion1");
                try
                {
                    SqlCommand cmd = new SqlCommand("MANT_USU", cn);
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TipoMantenimiento", "A");
                    cmd.Parameters.AddWithValue("@docentry", obj.docentry).Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.AddWithValue("@dni", obj.dni);
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("@apellido", obj.apellido);
                    cmd.Parameters.AddWithValue("@fecnac", obj.fecnac);
                    cmd.Parameters.AddWithValue("@direccion", obj.direccion);
                    cmd.Parameters.AddWithValue("@pais", obj.pais);
                    cmd.Parameters.AddWithValue("@telefono", obj.telefono);
                    cmd.Parameters.AddWithValue("@correo", obj.correo);
                    cmd.Parameters.AddWithValue("@rol", 3);
                    cmd.Parameters.AddWithValue("@opCreacion", obj.opCreacion);
                    cmd.Parameters.AddWithValue("@contraseña", obj.contraseña);


                    cmd.ExecuteNonQuery();

                    tran.Commit();
                }
                catch (Exception e) { tran.Rollback(); cn.Close(); throw new Exception("Error en creacion: " + e.Message); }
                cn.Close();
            }
            catch (Exception e2) { cn.Close(); status = 0; throw new Exception("Error en creacion y conexion: " + e2.Message); }
            return status;
        }
        public void editarUsuario(Usuario_E obj)
        {        
            SqlConnection cn = new SqlConnection(uti.cadSql);
            try
            {
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction("transaccion1");
                try
                {
                    SqlCommand cmd = new SqlCommand("MANT_USU", cn);
                    cmd.Transaction = tran;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TipoMantenimiento", "U");
                    cmd.Parameters.AddWithValue("@docentry", obj.docentry);
                    cmd.Parameters.AddWithValue("@dni", obj.dni);
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("@apellido", obj.apellido);
                    cmd.Parameters.AddWithValue("@fecnac", obj.fecnac);                    
                    cmd.Parameters.AddWithValue("@direccion", obj.direccion);
                    cmd.Parameters.AddWithValue("@pais", obj.pais);
                    cmd.Parameters.AddWithValue("@telefono", obj.telefono);
                    cmd.Parameters.AddWithValue("@correo", obj.correo); 
                    cmd.Parameters.AddWithValue("@rol", obj.rol);
                    cmd.ExecuteNonQuery();
                    tran.Commit();
                }
                catch (Exception e) { tran.Rollback(); cn.Close(); throw new Exception("Error en creacion: " + e.Message); }
                cn.Close();
            }
            catch (Exception e2) { cn.Close();throw new Exception("Error en creacion y conexion: " + e2.Message); }
        }
        public void eliminarUsuario(int id)
        {
            
            string query = "delete from comprobante where cliente=@id; " +
                "delete from pedido where idcliente=@id; " +
                "delete from usuario where docentry=@id ";

            SqlDataReader dr = db.ExecuteReaderNoSp(query, new List<string>() { "@id" }, id);
            dr.Close();
        }
        public string actualizacionContraseña(Usuario_E obj)
        {
            string msj = "";
            string query = "UPDATE usuario SET contraseña = @NuevaPassword where dni = @CodUser and contraseña = @Contraseña";
            try
            {
                db.ExecuteNonQueryTrxNoSp(query, new List<string>() { "@NuevaPassword", "@CodUser", "@Contraseña" }
                                                    , obj.newcontraseña, obj.dni, obj.contraseña);
            }
            catch { throw new Exception("Ocurrio un error al actualizar"); }
            return msj;
        }
          
    }

}

