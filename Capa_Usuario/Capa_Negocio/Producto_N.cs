using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Capa_Negocio
{
    public class Producto_N
    {
        Producto_D productoD = new Producto_D();
        private bool cadenaVacia(string cad)
        {
            if (cad == null || cad.Replace(" ", "").Length == 0) { return true; }
            else return false;
        }
        public List<Producto_E> listarProductos(Producto_E filtro)
        {
            return productoD.listarProductos(filtro);
        }
        public List<Producto_E> listarProductosUsuario(int id)
        {
            return productoD.listarProductosUsuario(id);
        }
        public void registrarProducto(Producto_E filtro, HttpPostedFileBase archivo, HttpPostedFileBase archivo2, HttpPostedFileBase archivo3)
        {
            validarNuevoProducto(filtro);
            productoD.registrarProducto(filtro, archivo,archivo2,archivo3);
        }
        public void editarProducto(Producto_E filtro)
        {
            validarEditarProducto(filtro);
            productoD.editarProducto(filtro);
        }
        public Producto_E buscarProducto(int id)
        {
            return productoD.buscarProducto(id);
        }
        
        //validaciones
        public void validarNuevoProducto(Producto_E obj)
        {  
            if (cadenaVacia(obj.nombre)) { throw new Exception("Ingrese nombre"); }
            if (cadenaVacia(obj.descripcion)) { throw new Exception("Ingrese descripcion"); }
            if (cadenaVacia(obj.marca)) { throw new Exception("Ingrese marca"); }
            if (obj.precio <=0) { throw new Exception("Debe ingresar un precio valido"); }
            if (obj.stock <=0 ) { throw new Exception("Debe ingresar un stock valido"); }
            if (obj.categoria == -1) { throw new Exception("Seleccione categoria"); }
            if (obj.subcategoria == -1) { throw new Exception("Seleccione una subcategoria"); }
        }
        public void validarEditarProducto(Producto_E obj)
        {
            if (cadenaVacia(obj.nombre)) { throw new Exception("Ingrese nombre"); }
            if (cadenaVacia(obj.descripcion)) { throw new Exception("Ingrese descripcion"); }
            if (cadenaVacia(obj.marca)) { throw new Exception("Ingrese marca"); }
            if (obj.precio <= 0) { throw new Exception("Debe ingresar un precio valido"); }
            if (obj.stock <= 0) { throw new Exception("Debe ingresar un stock valido"); }
        }

    }
}
