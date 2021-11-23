using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class Usuario_N
    {
        Usuario_D usuarioD = new Usuario_D();
        private bool validarNuevaContraseña(string plainText)
        {
            bool resultado;
            Regex regex = new Regex(@"^(?=.*?[A-Za-zñÑ0-9])(?=.*?[#&$*@]).{6,12}$");
            Match match = regex.Match(plainText);
            resultado = match.Success;
            return resultado;
        }
        private bool cadenaVacia(string cad)
        {
            if (cad == null || cad.Replace(" ", "").Length == 0) { return true; }
            else return false;
        }
        public List<Usuario_E> listarUsuarios()
        {
            return usuarioD.listarUsuarios();
        }
        public Usuario_E buscarUsuario(int id)
        {
            return usuarioD.buscarUsuario(id);
        }
        public Usuario_E buscarUsuarioDni(string dni)
        {
            return usuarioD.buscarUsuarioDni(dni);
        }
        public void editarUsuario(Usuario_E obj)
        {
            validarEditarUsuario(obj);
            usuarioD.editarUsuario(obj);
        }
        public Usuario_E buscarUsuarioLogueo(Usuario_E user)
        {
            return usuarioD.buscarUsuarioLogueo(user);
        }
        public int registrarNuevoUsuario(Usuario_E obj)
        {
            validarNuevoUsuario(obj);
            return usuarioD.registrarNuevoUsuario(obj);
        }
        public int registrarNuevoUsuarioVisitante(Usuario_E obj)
        {
            validarNuevoUsuarioVisitante(obj);
            return usuarioD.registrarNuevoUsuarioVisitante(obj);
        }
        public string actualizacionContraseña(Usuario_E obj)
        {
            validarActualizarContraseña(obj);
            return usuarioD.actualizacionContraseña(obj);

        }
        public void eliminarUsuario(int id)
        {
            usuarioD.eliminarUsuario(id);
         }

            //validaciones 
        public void validarNuevoUsuario(Usuario_E obj)
        {
        if (obj.rol == -1) { throw new Exception("Debe seleccionar rol de usuario"); }
        if (cadenaVacia(obj.nombre)) { throw new Exception("Ingrese nombre"); }
        if (cadenaVacia(obj.apellido)) { throw new Exception("Ingrese apellido"); }
        if (cadenaVacia(obj.dni) || obj.dni.Length!=8 ) { throw new Exception("Ingrese dni valido"); } 
        if (buscarUsuarioDni(obj.dni) != null) { throw new Exception("Ya existe un usuario con ese DNI"); }
        if (cadenaVacia(obj.direccion)) { throw new Exception("Ingrese direccion"); }
        if (cadenaVacia(obj.telefono) || obj.telefono.Length > 12) { throw new Exception("Ingrese telefono valido"); }
        if (cadenaVacia(obj.correo)) { throw new Exception("Ingrese correo valido"); }
        if (cadenaVacia(obj.pais)) { throw new Exception("Ingrese pais"); }
        if (obj.fecnac == new DateTime()) { throw new Exception("Seleccione su fec de nacimiento"); }
        if (cadenaVacia(obj.contraseña)) { throw new Exception("Ingrese una contraseña"); }
        if (!obj.contraseña.Equals(obj.newcontraseña)) { throw new Exception("Contrasenas no coinciden"); }
       
        }
        public void validarEditarUsuario(Usuario_E obj)
        {
            if (obj.rol == -1) { throw new Exception("Debe seleccionar rol de usuario"); }
            if (cadenaVacia(obj.nombre)) { throw new Exception("Ingrese nombre"); }
            if (cadenaVacia(obj.apellido)) { throw new Exception("Ingrese apellido"); }
            if (cadenaVacia(obj.direccion)) { throw new Exception("Ingrese direccion"); }
            if (cadenaVacia(obj.telefono) || obj.telefono.Length > 12) { throw new Exception("Ingrese telefono valido"); }
            if (cadenaVacia(obj.correo)) { throw new Exception("Ingrese correo valido"); }
            if (cadenaVacia(obj.pais)) { throw new Exception("Ingrese pais"); }
            if (obj.fecnac == new DateTime()) { throw new Exception("Seleccione su fec de nacimiento"); }
        }
        public void validarNuevoUsuarioVisitante(Usuario_E obj)
        {
            if (cadenaVacia(obj.nombre)) { throw new Exception("Ingrese nombre"); }
            if (cadenaVacia(obj.apellido)) { throw new Exception("Ingrese apellido"); }
            if (cadenaVacia(obj.dni) || obj.dni.Length != 8) { throw new Exception("Ingrese dni valido"); }
            if (buscarUsuarioDni(obj.dni) != null) { throw new Exception("Ya existe un usuario con ese DNI"); }
            if (cadenaVacia(obj.direccion)) { throw new Exception("Ingrese direccion"); }
            if (cadenaVacia(obj.telefono) || obj.telefono.Length > 12) { throw new Exception("Ingrese telefono valido"); }
            if (cadenaVacia(obj.correo)) { throw new Exception("Ingrese correo valido"); }
            if (cadenaVacia(obj.pais)) { throw new Exception("Ingrese pais"); }
            if (obj.fecnac == new DateTime()) { throw new Exception("Seleccione su fec de nacimiento"); }
            if (cadenaVacia(obj.contraseña)) { throw new Exception("Ingrese una contraseña"); }
            if (!obj.contraseña.Equals(obj.newcontraseña)) { throw new Exception("Contrasenas no coinciden"); }
        }
        public void validarActualizarContraseña(Usuario_E obj)
        {
            if (cadenaVacia(obj.contraseña)) { throw new Exception("Ingrese su contraseña actual"); }
            if (cadenaVacia(obj.newcontraseña)) { throw new Exception("Ingrese su nueva contraseña"); }
            if (cadenaVacia(obj.password)) { throw new Exception("Repita su nueva contraseña"); }
            if (validarNuevaContraseña(obj.newcontraseña) == false) { throw new Exception("La contraseña ingresada no cumple con los requisitos"); }
            if (obj.newcontraseña.Equals(obj.password) == false) { throw new Exception("Contrasenas no coinciden"); }
        }
    }
}

