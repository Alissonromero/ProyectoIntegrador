USE [BD_ComprasYa]
GO

create PROCEDURE [dbo].[MANT_PED]
(
	@TipoMantenimiento nvarchar(5),
	@docentry int out,
	@idproducto int =null,
	@nomproducto varchar(100)=null,
	@idvendedor int=null,
	@vendedor varchar(100)=null,
	@fecreg date=null,
	@idcliente int=null,
	@nomcliente varchar(500)=null,
	@lugarDestino varchar(250)=null,
	@precio decimal(10,2)=null,
	@cantidad int=null,
	@descuento decimal(10,2)=null,
	@total decimal(10,2)=null,
	@estado varchar(20)=null,
	@docentry2 int out,
	@fecreg2 datetime=null,--fecha de entrega
	@tipo varchar(30) =null,
	@modpago varchar(30)=null,
	@cuotas int =null
)
AS
IF(@TipoMantenimiento='A') --ADD
BEGIN
	BEGIN TRY
		set @docentry = (select docentry from gene where Tabla='pedido')+1;
		set @docentry2 = (select docentry from gene where Tabla='comprobante')+1;
		BEGIN TRANSACTION TR01
			insert into pedido values (@docentry,@idproducto,@nomproducto,@idvendedor,@vendedor,getdate(),@idcliente
										,@nomcliente,@lugarDestino,@precio,@cantidad,@descuento,@total,'Pendiente');
--comprobante
			insert into comprobante values (@docentry2,@fecreg2,@docentry,@idcliente,@tipo,@modpago,@cuotas);

			update gene set docentry=@docentry where Tabla='pedido';
			update gene set docentry=@docentry where Tabla='comprobante';
		COMMIT TRANSACTION TR01
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION TR01
		PRINT ERROR_MESSAGE()
		RAISERROR ('SU TRANSACCION TMP NO SE REALIZO',16,1)
	END CATCH
END
IF(@TipoMantenimiento='EC') --update por estado de En Proceso a Entregado y se registra la fecha de entrega en el comprobante
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION TR02
			update pedido set estado='En Camino' where docentry=@docentry;
			update comprobante set fecreg=getdate() where docentry=@docentry2;
		COMMIT TRANSACTION TR02
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION TR02
		PRINT ERROR_MESSAGE()
		RAISERROR ('SU TRANSACCION TMP NO SE REALIZO',16,1)
	END CATCH
END
IF(@TipoMantenimiento='E') --update por estado de En Proceso a Entregado y se registra la fecha de entrega en el comprobante
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION TR03
			update pedido set estado='Entregado' where docentry=@docentry;
			update comprobante set fecreg=getdate() where docentry=@docentry2;
		COMMIT TRANSACTION TR03
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION TR03
		PRINT ERROR_MESSAGE()
		RAISERROR ('SU TRANSACCION TMP NO SE REALIZO',16,1)
	END CATCH
END