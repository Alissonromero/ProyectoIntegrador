using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class Pedido_N
    {
        Pedido_D pedidoD = new Pedido_D();
        public void registrarPedido(Pedido_E obj)
        {
            validarNuevoPedido(obj);
            pedidoD.registrarPedido(obj);
        }
        public void atenderVenta(int id, string estado, int idcom)
        {
            pedidoD.atenderVenta(id, estado, idcom);
        }
        public void enCaminoVenta(int id, string estado, int idcom)
        {
            pedidoD.enCaminoVenta(id, estado, idcom);
        }
        public List<Pedido_E> listarPedidos(Pedido_E filtro, int id)
        {
            return pedidoD.listarPedidos(filtro, id);
        }
        public List<Pedido_E> listarPedidosVen(Pedido_E filtro, int id)
        {
            return pedidoD.listarPedidosVen(filtro, id);
        }
        private bool cadenaVacia(string cad)
        {
            if (cad == null || cad.Replace(" ", "").Length == 0) { return true; }
            else return false;
        }

        //validaciones 
        public void validarNuevoPedido(Pedido_E obj)
        {
            if (cadenaVacia(obj.lugarDestino)) { throw new Exception("Ingrese lugar de entrega"); }
            if (obj.tipo==null) { throw new Exception("Seleccione tipo de comprobante"); }
            if (obj.modpago == null) { throw new Exception("Seleccione modo de pago"); }
            if (obj.modpago=="Credito") { 
                if(obj.cuotas <= 0){
                throw new Exception("Ingrese las cuotas para su pago a credito"); 
            }}

        }
    }
}
