use BD_ComprasYa
go
/*PROCEDIMIENTOS DE PRODUCTO*/
create PROCEDURE [dbo].[MANT_PRO]
(
	@TipoMantenimiento nvarchar(5),
	@docentry INT OUT,
	@nombre varchar(160)=null,
	@descripcion varchar(350)=null,
	@marca varchar(100)=null,
	@categoria int=null,
	@subcategoria int=null,
	@precio decimal(10,2)=null,
	@stock int=null,
	@garantia char(2)=null,
	@color varchar(20)=null,
	@fotos varchar(250)=null,
	@fotos2 varchar(250)=null,
	@fotos3 varchar(250)=null,
	@idRegistro int=null,
	@opRegistro varchar(100)='',
	@tiempoRegistro datetime=null

)
AS
IF(@TipoMantenimiento='A') --ADD
BEGIN
	BEGIN TRY
	set @docentry = (select docentry from gene where Tabla='producto')+1;		
		BEGIN TRANSACTION TR01
			insert into producto values(@docentry,@nombre,@descripcion,@marca,@categoria,
			@subcategoria,@precio,@stock,@garantia,@color,@fotos,@fotos2,@fotos3,@idRegistro,@opRegistro,getdate());
			update gene set docentry=@docentry where Tabla='producto';

		COMMIT TRANSACTION TR01
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION TR01
		PRINT ERROR_MESSAGE()
		RAISERROR ('SU TRANSACCION PARA USUARIO NO SE REALIZO',16,1)
	END CATCH
END
IF(@TipoMantenimiento='U') --update
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION TR02
			update producto set nombre=@nombre,descripcion=@descripcion,marca=@marca,
			precio=@precio,stock=@stock,garantia=@garantia,color=@color where docentry=@docentry;
		COMMIT TRANSACTION TR02
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION TR02
		PRINT ERROR_MESSAGE()
		RAISERROR ('SU TRANSACCION PARA USUARIO NO SE REALIZO',16,1)
	END CATCH
END