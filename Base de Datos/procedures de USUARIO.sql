use BD_ComprasYa
go
create PROCEDURE [dbo].[MANT_USU]
(
	@TipoMantenimiento nvarchar(5),
	@docentry INT OUT,
	@dni CHAR(8) ,
	@nombre varchar(250)=null,
	@apellido varchar(250)=NULL,
	@fecreg datetime=null,
	@fecnac date=null,
	@direccion varchar(250)=NULL,
	@pais varchar(250)=NULL,
	@telefono varchar(12)=NULL,
	@correo varchar(250)=NULL,
	@rol int=null,
	@opCreacion varchar(250)=NULL,
	@contraseña varchar(32)=NULL
)
AS
IF(@TipoMantenimiento='A') --ADD
BEGIN
	BEGIN TRY
	set @docentry = (select docentry from gene where Tabla='usuario')+1;
		
		BEGIN TRANSACTION TR01
			insert into usuario values(@docentry,@dni,@nombre,@apellido,getdate(),
			@fecnac,@direccion,@pais,@telefono,@correo,@rol,@opCreacion,@contraseña);
			update  gene set docentry=@docentry where Tabla='usuario';
		COMMIT TRANSACTION TR01
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION TR01
		PRINT ERROR_MESSAGE()
		RAISERROR ('SU TRANSACCION PARA USUARIO NO SE REALIZO',16,1)
	END CATCH
END
IF(@TipoMantenimiento='U') --update editar
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION TR02	
			update usuario set dni=@dni,nombre=@nombre,apellido=@apellido,
			fecnac=@fecnac,direccion=@direccion,pais=@pais,telefono=@telefono,correo=@correo,rol=@rol
			where docentry=@docentry and dni=@dni
			
		COMMIT TRANSACTION TR02
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION TR02
		PRINT ERROR_MESSAGE()
		RAISERROR ('SU TRANSACCION TMP NO SE REALIZO',16,1)
	END CATCH
END
