using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Capa_Datos
{
    public class DBHelper
    {
        Utilitarios_DAO uti = new Utilitarios_DAO();
        //correr una consulta con o sin parametros
        public SqlDataReader ExecuteReaderNoSp(string query, List<string> npara = null, params object[] Parametros)
        {
            SqlConnection cnx = new SqlConnection(uti.cadSql);
            cnx.Open();
            SqlCommand cmd = new SqlCommand(query, cnx);
            cmd.CommandType = CommandType.Text;
            if (npara != null)
            {
                foreach (string p in npara)
                {
                    SqlParameter par = new SqlParameter(p, null);
                    cmd.Parameters.Add(par);
                }
                if (Parametros.Length > 0)
                    LlenarParametrosNoSp(cmd, Parametros);
            }
            SqlDataReader lector = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return lector;
        }
        //correr un procedimiento almacenado con o sin parametros
        public SqlDataReader ExecuteReader(string NombreSP, params object[] Parametros)
        {
            SqlConnection cnx = new SqlConnection(uti.cadSql);
            cnx.Open();
            SqlCommand cmd = new SqlCommand(NombreSP, cnx);
            cmd.CommandType = CommandType.StoredProcedure;
            if (Parametros.Length > 0)
                LlenarParametros(cmd, Parametros);
            SqlDataReader lector = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return lector;
        }
        public void ExecuteNonQuery(string NombreSP, params object[] Parametros)
        {
            SqlConnection cnx = new SqlConnection(uti.cadSql);
            cnx.Open();
            //
            SqlCommand cmd = new SqlCommand(NombreSP, cnx);
            cmd.CommandType = CommandType.StoredProcedure;

            if (Parametros.Length > 0)
                LlenarParametros(cmd, Parametros);

            cmd.ExecuteNonQuery();

            cnx.Close();
        }
        public void ExecuteNonQueryTrx(string NombreSP, params object[] Parametros)
        {
            SqlConnection cnx = new SqlConnection(uti.cadSql);
            cnx.Open();
            // crear la transaccion
            SqlTransaction trx = cnx.BeginTransaction();
            try
            {
                // especificamos que la transaccion sera llevada a cabo por SqlCommand
                SqlCommand cmd = new SqlCommand(NombreSP, cnx, trx);
                cmd.CommandType = CommandType.StoredProcedure;

                if (Parametros.Length > 0)
                    LlenarParametros(cmd, Parametros);

                cmd.ExecuteNonQuery();
                // si no hay ningun ERROR, entonces CONFIRMAMOS las OPERACIONES
                trx.Commit();
                //
                cnx.Close();
            }
            catch (Exception ex)
            {
                trx.Rollback(); // cancela las operaciones
                throw new Exception(ex.Message);
            }
        }
        public void ExecuteNonQueryTrxNoSp(string query, List<string> npara = null, params object[] Parametros)
        {
            SqlConnection cnx = new SqlConnection(uti.cadSql);
            cnx.Open();
            SqlTransaction trx = cnx.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand(query, cnx, trx);
                cmd.CommandType = CommandType.Text;

                if (npara != null)
                {
                    foreach (string p in npara)
                    {
                        SqlParameter par = new SqlParameter(p, null);
                        cmd.Parameters.Add(par);
                    }
                    if (Parametros.Length > 0)
                        LlenarParametrosNoSp(cmd, Parametros);
                }
                cmd.ExecuteNonQuery();
                trx.Commit();
                //
                cnx.Close();
            }
            catch (Exception ex)
            {
                trx.Rollback(); // cancela las operaciones
                throw new Exception(ex.Message);
            }
        }
        public object ExecuteScalar(string NombreSP, params object[] Parametros)
        {
            SqlConnection cnx = new SqlConnection(uti.cadSql);
            cnx.Open();
            SqlCommand cmd = new SqlCommand(NombreSP, cnx);
            cmd.CommandType = CommandType.StoredProcedure;

            if (Parametros.Length > 0)
                LlenarParametros(cmd, Parametros);

            object rpta = cmd.ExecuteScalar();

            cnx.Close();

            return rpta;
        }
        public object ExecuteScalarNoSp(string NombreSP)
        {
            SqlConnection cnx = new SqlConnection(uti.cadSql);
            object rpta;
            try
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand(NombreSP, cnx);
                cmd.CommandType = CommandType.Text;
                rpta = cmd.ExecuteScalar();
                cnx.Close();
            }
            catch (Exception e) { cnx.Close(); throw new Exception(e.Message); }
            return rpta;
        }
        private void LlenarParametros(SqlCommand comando, params object[] parametros)
        {
            int indice = 0;
            int totalParam = parametros.Length;
            SqlCommandBuilder.DeriveParameters(comando);
            foreach (SqlParameter item in comando.Parameters)
            {
                if (item.ParameterName != "@RETURN_VALUE")
                {
                    item.Value = parametros[indice];
                    indice++;
                }
                if (totalParam == (indice)) { return; }
            }
        }
        private void LlenarParametrosNoSp(SqlCommand comando, params object[] parametros)
        {
            int indice = 0;
            //SqlCommandBuilder.DeriveParameters(comando);
            foreach (SqlParameter item in comando.Parameters)
            {
                if (item.ParameterName != "@RETURN_VALUE")
                {
                    item.Value = parametros[indice];
                    indice++;
                }
            }
        }
    }
}