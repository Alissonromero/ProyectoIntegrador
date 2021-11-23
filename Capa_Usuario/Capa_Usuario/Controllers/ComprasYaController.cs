using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Capa_Entidad;
using Capa_Negocio;

namespace Capa_Usuario.Controllers
{

    public class ComprasYaController : Controller
    {
        public ActionResult Index(string msj = "")
        {
            ViewBag.Mensaje = msj;
            return View();
        }
        [ActionName("Index")]
        [HttpPost]
        public ActionResult Logueo(Usuario_E user)
        {
            
            try
            {
                Usuario_N ousrN = new Usuario_N();
                Session["Usuario"] = ousrN.buscarUsuarioLogueo(user);

                return RedirectToAction("SesionUsuario");
            }
            catch (Exception e) { return RedirectToAction("Index", new { msj = e.Message }); }
        }  
        public ActionResult CerrarSesion()
        {
            Session.Remove("Usuario");
            return RedirectToAction("Index");
        }
        public ActionResult ErrorOperacion()
        {
            return View();
        }
        public ActionResult SesionUsuario(string msj, int idOperacion = 1)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                ViewBag.mensaje = msj;
                return View();
            }
            else if (verificacionAccesos(idOperacion) == "E_Login") { return RedirectToAction("Index"); }
            else { return RedirectToAction("ErrorOperacion"); }
        }
        public FileResult Manual(int idOperacion = 1)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                Rol_N orolN = new Rol_N();
                Usuario_E user = (Usuario_E)Session["Usuario"];
                string ruta = orolN.buscarRutaManual(user.rol);
                return File(ruta, "application/pdf", "Manual.pdf");
            }
            else if (verificacionAccesos(idOperacion) == "E_Login") { return null; }
            else { return null; }
        }
        public ActionResult RegistrarUsuario(int idOperacion = 2)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                Rol_N rolN = new Rol_N();
                ViewBag.Roles = rolN.listarRoles();
                return View();
            }
            else if (verificacionAccesos(idOperacion) == "E_Login") { return RedirectToAction("RegistrarUsuario2"); }
            else { return RedirectToAction("ErrorOperacion"); }
        }
        [HttpPost]
        public ActionResult RegistrarUsuario(Usuario_E obj, int idOperacion = 2)
        {

            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                Usuario_N usuN = new Usuario_N();
                Rol_N rolN = new Rol_N();
                try
                {
                    Usuario_E user = (Usuario_E)Session["Usuario"];
                    obj.opCreacion = user.nombre;
                    usuN.registrarNuevoUsuario(obj);
                    return RedirectToAction("GestionUsuarios");
                }
                catch (Exception e)
                { ViewBag.Mensaje = e.Message; ViewBag.Roles = rolN.listarRoles(); return View(obj); }
            }
            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }

        }
        public ActionResult RegistrarUsuario2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegistrarUsuario2(Usuario_E obj)
        { Usuario_N usuN = new Usuario_N();
            try
            {
                obj.opCreacion =obj.nombre +" "+ obj.apellido;  
                usuN.registrarNuevoUsuarioVisitante(obj);
                return RedirectToAction("GestionUsuarios");
            }
            catch (Exception e)
            { ViewBag.Mensaje = e.Message; return View(obj); }
        }
        public ActionResult GestionUsuarios(int idOperacion = 3)
        {

            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                try
                {
                    Usuario_N usuN = new Usuario_N();
                    return View(usuN.listarUsuarios());
                }
                catch (Exception e)
                { ViewBag.Mensaje = e.Message; return View(); }
            }
            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }
        }
        public ActionResult DetallesUsuario(int id, int idOperacion = 4)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                try
                {
                    Usuario_N usuN = new Usuario_N();
                    return View(usuN.buscarUsuario(id));
                }
                catch (Exception e)
                {
                    ViewBag.Mensaje = e.Message; return View(id);
                }
            }
            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }

        }
        public ActionResult EditarUsuario(int id, int idOperacion = 5)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                Usuario_N usuN = new Usuario_N();
                Rol_N rolN = new Rol_N();
                ViewBag.Roles = rolN.listarRoles();
                return View(usuN.buscarUsuario(id));
            }
            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }
        }
        [HttpPost]
        public ActionResult EditarUsuario(Usuario_E obj, int idOperacion = 5)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                try
                {
                    Usuario_N usuN = new Usuario_N();
                    usuN.editarUsuario(obj);
                    return RedirectToAction("GestionUsuarios");
                }
                catch (Exception e)
                {
                    ViewBag.Mensaje = e.Message;
                    return View(obj);
                }
            }
            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }
        }
        public ActionResult EliminarUsuario(int id, int idOperacion = 6)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                try
                {
                    Usuario_N usuN = new Usuario_N();
                    usuN.eliminarUsuario(id);
                    return RedirectToAction("GestionUsuarios");
                }
                catch (Exception e)
                {
                    ViewBag.Mensaje = e.Message;
                    return View(id);
                }
            }
            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }
        }
        public ActionResult ActualizarContraseña(int idOperacion = 7)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                return View();
            }
            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }
        }
        [HttpPost]
        public ActionResult ActualizarContraseña(Usuario_E obj, int idOperacion = 7)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                Usuario_N ousrN = new Usuario_N();
                try
                {
                    Usuario_E user = (Usuario_E)Session["Usuario"];
                    obj.dni = user.dni;
                    ousrN.actualizacionContraseña(obj);
                    return RedirectToAction("Index", new { msj = "Contraseña actualizada, ingrese nuevamente" });
                }
                catch (Exception e)
                { ViewBag.Mensaje = e.Message; return View(obj); }
            }
            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }
        }
        public ActionResult GestionProductos(Producto_E filtro, int idOperacion = 8)
        {

            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                Producto_N producN = new Producto_N();
                SubCategoria_N subcatN = new SubCategoria_N();
                try
                {
                    ViewBag.SubCategorias = subcatN.listarSubCategorias();
                    ViewBag.Producto = filtro;
                    ViewBag.listaProductos = producN.listarProductos(filtro);
                    return View(producN.listarProductos(filtro));
                }
                catch (Exception e)
                { ViewBag.Mensaje = e.Message; return View(); }
            }
            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }
        }
        public ActionResult ListadoProductos(Producto_E filtro, string x, string y, int idOperacion = 9)
        {

            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                Producto_N producN = new Producto_N();
                SubCategoria_N subcatN = new SubCategoria_N();
                try
                {
                    ViewBag.SubCategorias = subcatN.listarSubCategorias();
                    //ViewBag.Producto = filtro;
                    ViewBag.listaProductos = producN.listarProductos(new Producto_E() { categoria2 = x, subcategoria2 = y });
                    return View(producN.listarProductos(filtro));

                }
                catch (Exception e)
                { ViewBag.Mensaje = e.Message; ViewBag.SubCategorias = subcatN.listarSubCategorias(); return View(); }
            }
            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }
        }
        public ActionResult PublicarProductos(Producto_E filtro, int idOperacion = 10)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                SubCategoria_N subcatN = new SubCategoria_N(); Categoria_N catN = new Categoria_N();
                
                try
                {
                    if (filtro == null) filtro = new Producto_E();
                    ViewBag.Categorias = catN.listarCategorias();
                    ViewBag.SubCategorias = subcatN.listarSubCategorias();
                    return View();
                }
                catch (Exception e)
                { ViewBag.Mensaje = e.Message; return View(); }
            }
            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }

        }
        [HttpPost]
        public ActionResult PublicarProductos(Producto_E filtro, HttpPostedFileBase archivo, HttpPostedFileBase archivo2, HttpPostedFileBase archivo3, int idOperacion = 10)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            { 
                SubCategoria_N subcatN = new SubCategoria_N(); Categoria_N catN = new Categoria_N();
                    Producto_N productoN = new Producto_N();

                try
                {
                    Usuario_E user = (Usuario_E)Session["Usuario"];
                    if (archivo != null && archivo.ContentLength > 0)
                    {
                        string ruta_img = "~/images/" + Path.GetFileName(archivo.FileName);
                        if (!(archivo.ContentType == "image/jpeg" ||
                            archivo.ContentType == "image/png" ||
                            archivo.ContentType == "image/jpg" ||
                            archivo.ContentType == "image/jfif"))
                        {
                            throw new Exception("Debe elegir una imagen valida .png,.jpg,.jpeg o .jfif");
                        }
                        archivo.SaveAs(Server.MapPath(ruta_img));
                    }
                    if (archivo2 != null && archivo2.ContentLength > 0)
                    {
                        string ruta_img_2 = "~/images/" + Path.GetFileName(archivo2.FileName);
                        if (!(archivo2.ContentType == "image/jpeg" ||
                            archivo2.ContentType == "image/png" ||
                            archivo2.ContentType == "image/jpg" ||
                            archivo2.ContentType == "image/jfif"))
                        {
                            throw new Exception("Debe elegir una imagen valida .png,.jpg,.jpeg o .jfif");
                        }
                        archivo2.SaveAs(Server.MapPath(ruta_img_2));
                    }
                    if (archivo3 != null && archivo3.ContentLength > 0)
                    {
                        string ruta_img_3 = "~/images/" + Path.GetFileName(archivo3.FileName);
                        if (!(archivo3.ContentType == "image/jpeg" ||
                            archivo3.ContentType == "image/png" ||
                            archivo3.ContentType == "image/jpg" ||
                            archivo3.ContentType == "image/jfif"))
                        {
                            throw new Exception("Debe elegir una imagen valida .png,.jpg,.jpeg o .jfif");
                        }
                        archivo3.SaveAs(Server.MapPath(ruta_img_3));
                    }
                    else
                    {
                        throw new Exception("Seleccione una foto");
                    }

                    ViewBag.Categorias = catN.listarCategorias();
                    ViewBag.SubCategorias = subcatN.listarSubCategorias();
                    filtro.idRegistro = user.docentry;
                    filtro.opRegistro = user.nombre + ' '+user.apellido;
                    productoN.registrarProducto(filtro, archivo, archivo2, archivo3);
                    return RedirectToAction("ListadoProductos");

                }
                catch (Exception e)
                {
                    ViewBag.Mensaje = e.Message;
                    ViewBag.Categorias = catN.listarCategorias();
                    ViewBag.SubCategorias = subcatN.listarSubCategorias();
                    return View(filtro);
                }
            }
            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }
        }
        public ActionResult DetalleProducto(int id, int idOperacion = 11)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                Producto_N productoN = new Producto_N();
                Producto_E p = productoN.buscarProducto(id);
                if (p == null)
                    return RedirectToAction("ListadoProductos");
                else
                    return View(p);
            }
            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }
        }
        [HttpPost]
        public ActionResult DetalleProducto(int id, int cantidad, int idOperacion = 11)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
            Producto_N productoN = new Producto_N();
            Producto_E p = productoN.buscarProducto(id);
            Pedido_E ped = new Pedido_E();
            Usuario_E user = (Usuario_E)Session["Usuario"];
            ped.idcliente = user.docentry;
            ped.nomcliente = user.nombre + ' ' + user.apellido;
            ped.idproducto = p.docentry;
            ped.nomproducto = p.nombre;
            ped.idvendedor = p.idRegistro;
            ped.vendedor = p.opRegistro;
            ped.precio = p.precio;
            ped.cantidad = cantidad;
            ped.total = cantidad * ped.precio;
            if (ped.total > 2000) { ped.descuento = 30; } else { ped.descuento = 0; }
            return RedirectToAction("GestionPedido", ped);
            }
            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }
        }
        public ActionResult EditarProducto(int id, int idOperacion = 12)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                Producto_N productoN = new Producto_N(); SubCategoria_N subcatN = new SubCategoria_N(); Categoria_N catN = new Categoria_N();
                ViewBag.Categorias = catN.listarCategorias();
                ViewBag.SubCategorias = subcatN.listarSubCategorias();
                Producto_E p = productoN.buscarProducto(id);
                if (p == null)
                    return RedirectToAction("ListadoPublicaciones");
                else
                    return View(p);
            }
            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }
        }
        [HttpPost]
        public ActionResult EditarProducto(Producto_E obj,int idOperacion = 12)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                Producto_N productoN = new Producto_N();
                productoN.editarProducto(obj);
                return RedirectToAction("ListadoPublicaciones");
            }
            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }
        }
        public ActionResult GestionPedido(Pedido_E obj, int idOperacion = 13)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                return View(obj);
            }
            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }
        }
        [HttpPost]
        public ActionResult GestionPedido(Pedido_E obj, decimal totalreal,int idOperacion = 13)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                Pedido_N pedidoN = new Pedido_N();
                try
                {
                    Usuario_E user = (Usuario_E)Session["Usuario"];
                    obj.idcliente = user.docentry;obj.total = totalreal;
                    pedidoN.registrarPedido(obj);
                    return RedirectToAction("ListadoCompras");
                }
                catch (Exception e)
                {
                    ViewBag.Mensaje = e.Message;
                    return View();
                }
            }
            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }
        }
        public ActionResult ListadoCompras(Pedido_E filtro, int idOperacion = 14)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                Usuario_E user = (Usuario_E)Session["Usuario"];
                Pedido_N pedidoN = new Pedido_N();
                ViewBag.Pedido = filtro;
                return View(pedidoN.listarPedidos(filtro, user.docentry));
            }

            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }
        }
        public ActionResult ListadoPublicaciones( int idOperacion = 15)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                Usuario_E user = (Usuario_E)Session["Usuario"];
                Producto_N productoN = new Producto_N();
                SubCategoria_N subcatN = new SubCategoria_N();
                try
                {
                ViewBag.SubCategorias = subcatN.listarSubCategorias();
                    ViewBag.listaProductos = productoN.listarProductosUsuario(user.docentry);
                    return View(productoN.listarProductosUsuario(user.docentry));
                }
               
                catch (Exception e)
                { ViewBag.Mensaje = e.Message; ViewBag.SubCategorias = subcatN.listarSubCategorias(); 
                    ViewBag.listaProductos = productoN.listarProductosUsuario(user.docentry); return View(); }
             }

            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }
        }
        //falta seguimiento
        public ActionResult SeguimientoCompras(Pedido_E filtro, int id,int idOperacion = 16)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                Usuario_E user = (Usuario_E)Session["Usuario"];
            Pedido_N pedidoN = new Pedido_N();
            ViewBag.Pedido = filtro;
            filtro.docentry = id;
            return View(pedidoN.listarPedidos(filtro, user.docentry));
            }

            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }

        }
        public ActionResult AtenderPedidos(Pedido_E filtro,  int idOperacion = 17)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                Usuario_E user = (Usuario_E)Session["Usuario"];
                Pedido_N pedidoN = new Pedido_N();
                ViewBag.Pedido = filtro;
                return View(pedidoN.listarPedidosVen(filtro, user.docentry));
            }

            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }

        }
        public ActionResult AtenderVenta( int id, string estado, int idcom, int idOperacion = 17)
        {
            if (verificacionAccesos(idOperacion) == "C_Access")
            {
                
                Pedido_N pedidoN = new Pedido_N();
                pedidoN.atenderVenta(id, estado, idcom);
                return RedirectToAction("AtenderPedidos");
            }

            else if (verificacionAccesos(idOperacion) == "E_Login")
            { return RedirectToAction("Index"); }
            else
            { return RedirectToAction("ErrorOperacion"); }

        }
        //llamado a las validaciones tipo ajax
        public ActionResult validarNuevoUsuario(Usuario_E obj)
        {

            Usuario_N ousrN = new Usuario_N();
            string status = "true";
            try
            {
                ousrN.validarNuevoUsuario(obj);
                return Content(status);
            }
            catch (Exception e) { return Content(e.Message); }
        }
        public ActionResult validarNuevoUsuarioVisitante(Usuario_E obj)
        {

            Usuario_N ousrN = new Usuario_N();
            string status = "true";
            try
            {
                ousrN.validarNuevoUsuarioVisitante(obj);
                return Content(status);
            }
            catch (Exception e) { return Content(e.Message); }
        }
        public ActionResult validarNuevaContraseña(Usuario_E obj)
        {
            
                Usuario_N ousrN = new Usuario_N();
                string status = "true";
                try
                {
                    ousrN.validarActualizarContraseña(obj);
                    return Content(status);
                }
                catch (Exception e) { return Content(e.Message); }
            }
        public ActionResult validarEditarUsuario(Usuario_E obj)
        {

            Usuario_N ousrN = new Usuario_N();
            string status = "true";
            try
            {
                ousrN.validarEditarUsuario(obj);
                return Content(status);
            }
            catch (Exception e) { return Content(e.Message); }
        }
        public ActionResult validarNuevoProducto(Producto_E obj)
        {

            Producto_N productoN = new Producto_N();
            string status = "true";
            try
            {
                productoN.validarNuevoProducto(obj);
                return Content(status);
            }
            catch (Exception e) { return Content(e.Message); }
        }
        public ActionResult validarEditarProducto(Producto_E obj)
        {

            Producto_N productoN = new Producto_N();
            string status = "true";
            try
            {
                productoN.validarEditarProducto(obj);
                return Content(status);
            }
            catch (Exception e) { return Content(e.Message); }
        }
        public ActionResult validarNuevoPedido(Pedido_E obj)
        {

            Pedido_N pedidoN = new Pedido_N();
            string status = "true";
            try
            {
                pedidoN.validarNuevoPedido(obj);
                return Content(status);
            }
            catch (Exception e) { return Content(e.Message); }
        }

        //verificacion de accesos
        private string verificacionAccesos(int ope)
        {
            string nombreOperacion = this.ControllerContext.RouteData.Values["action"].ToString();
            Usuario_E user = (Usuario_E)Session["Usuario"];
            if (user == null)
            { return "E_Login"; }
            else
            {
                Rol_Operacion_N oroN = new Rol_Operacion_N();
                if ((oroN.verificarAccesoOperacion(user.rol, ope, nombreOperacion) == 1) || (user.rol == 1))
                {
                    return "C_Access";
                }
                else
                { return "E_Access"; }
            }
        }
    }
}
    

