/*Las categorias que tienes que insertar son las que aparecen en el layout, como Tecnologia, etc*/
/*Las subcategorias que tienes que insertar igual son las que aparece ahi por cada una de las categorias
salen sus subcategorias los nombres tienen que ser tal cual para que no salga errores*/

USE BD_ComprasYa
GO
/*esto sirve para que se genere automaticamente los id en estas dos tablas*/
insert into [gene] values('producto',0,1,'POST')
insert into [gene] values('usuario',3,1,'POST')
insert into [gene] values('pedido',0,1,'POST')
insert into [gene] values('comprobante',0,1,'POST')

/*cuando crees tu usuario ponle docentry 0 luego lo borras cuando ya tengas acceso desde la pagina a crear otro usuario*/

/*insert into de rol*/
INSERT INTO rol VALUES (1, 'Administrador','F:\Desarrollos\Proyecto Integrador nuevo\IntegratingProject\Manuales\ManualAdministrador.pdf');
INSERT INTO rol VALUES (2, 'Encargado','F:\Desarrollos\Proyecto Integrador nuevo\IntegratingProject\Manuales\ManualEncargado.pdf');
INSERT INTO rol VALUES (3, 'Cliente','F:\Desarrollos\Proyecto Integrador nuevo\IntegratingProject\Manuales\ManualCliente.pdf');

/* insert into de usuario*/
INSERT INTO usuario VALUES
           (1,'74434643','Alisson', 'Romero Cabrera', getdate(), '2000-07-23','Av los Alamos 5623 Canto Rey', 
		   'Peru','956985695','Alisson2785@gmail.com','1','Alisson Romero Cabrera','MateoMR4')
GO
INSERT INTO usuario VALUES
           (2,'12345678','Fernando', 'Flores Carita', getdate(), '1999-02-20','Av los Ciruelos 603 Canto Grande', 
		   'Chile','957877745','FloresC5@hotmail.com','3','Alisson Romero Cabrera','123456')
GO

/*insert into menu*/
INSERT INTO menu VALUES(1,	'Registrar Usuario','RegistrarUsuario')
INSERT INTO menu VALUES(2,	'Mantenimiento de Usuario'	,'GestionUsuarios')
INSERT INTO menu VALUES(3,	'Actualiza Contraseña'	,'ActualizarContraseña')
INSERT INTO menu VALUES(4,	'Mantenimiento de Productos',	'GestionProductos')
INSERT INTO menu VALUES(5,	'Catalogo',	'ListadoProductos')
INSERT INTO menu VALUES(6,	'Mis publicaciones'	,'ListadoPublicaciones')
INSERT INTO menu VALUES(7,	'Mis ordenes'	,'ListadoCompras')
INSERT INTO menu VALUES(8,	'Atender mis pedidos'	,'AtenderPedidos')
INSERT INTO menu VALUES(9,	'x'	,'CerrarSesion')

/*insert into acceso menu */
INSERT INTO acceso_menu VALUES(1,	1,	1)
INSERT INTO acceso_menu VALUES(1,	2,	2)
INSERT INTO acceso_menu VALUES(1,	3,	3)
INSERT INTO acceso_menu VALUES(1,	4,	4)
INSERT INTO acceso_menu VALUES(1,	5,	5)
INSERT INTO acceso_menu VALUES(1,	6,	6)
INSERT INTO acceso_menu VALUES(1,	7,	7)
INSERT INTO acceso_menu VALUES(1,	8,	8)
INSERT INTO acceso_menu VALUES(1,	9,	9)
insert into acceso_menu values ( 3, 5, 1)
insert into acceso_menu values ( 3, 6, 2)
insert into acceso_menu values ( 3, 7, 3)
insert into acceso_menu values ( 3, 8, 4)
insert into acceso_menu values ( 3, 3, 5)
insert into acceso_menu values ( 3, 9, 6)


/*insert into  operacion*/
INSERT INTO operacion VALUES(1	,'SesionUsuario')
INSERT INTO operacion VALUES(2	,'RegistrarUsuario')
INSERT INTO operacion VALUES(3	,'GestionUsuarios')
INSERT INTO operacion VALUES(4	,'DetallesUsuario')
INSERT INTO operacion VALUES(5	,'EditarUsuario')
INSERT INTO operacion VALUES(6	,'EliminarUsuario')
INSERT INTO operacion VALUES(7	,'ActualizarContraseña')
INSERT INTO operacion VALUES(8	,'GestionProductos')
INSERT INTO operacion VALUES(9, 'ListadoProductos')
INSERT INTO operacion VALUES(10, 'PublicarProductos')
INSERT INTO operacion VALUES(11, 'DetalleProducto')
INSERT INTO operacion VALUES(12, 'EditarProducto')
INSERT INTO operacion VALUES(13, 'GestionPedido')
INSERT INTO operacion VALUES(14, 'ListadoCompras')
INSERT INTO operacion VALUES(15, 'ListadoPublicaciones')
INSERT INTO operacion VALUES(16, 'SeguimientoCompras')
INSERT INTO operacion VALUES(17, 'AtenderPedidos')

/*insert into rol operacion*/
INSERT INTO rol_operacion VALUES(1,	1)
INSERT INTO rol_operacion VALUES(1,	2)
INSERT INTO rol_operacion VALUES(1,	3)
INSERT INTO rol_operacion VALUES(1,	4)
INSERT INTO rol_operacion VALUES(1,	5)
INSERT INTO rol_operacion VALUES(1,	6)
INSERT INTO rol_operacion VALUES(1,	7)
INSERT INTO rol_operacion VALUES(1,	8)
INSERT INTO rol_operacion VALUES(1,	9)
INSERT INTO rol_operacion VALUES(1,	10)
INSERT INTO rol_operacion VALUES(1,	11)
INSERT INTO rol_operacion VALUES(1,	12)
INSERT INTO rol_operacion VALUES(1,	13)
INSERT INTO rol_operacion VALUES(1,	14)
INSERT INTO rol_operacion VALUES(1,	15)
INSERT INTO rol_operacion VALUES(1,	16)
INSERT INTO rol_operacion VALUES(1,	17)
--
INSERT INTO rol_operacion VALUES(3, 1)
INSERT INTO rol_operacion VALUES(3,	5)
INSERT INTO rol_operacion VALUES(3,	7)
INSERT INTO rol_operacion VALUES(3,	9)
INSERT INTO rol_operacion VALUES(3,	10)
INSERT INTO rol_operacion VALUES(3,	11)
INSERT INTO rol_operacion VALUES(3,	12)
INSERT INTO rol_operacion VALUES(3,	13)
INSERT INTO rol_operacion VALUES(3,	14)
INSERT INTO rol_operacion VALUES(3,	15)
INSERT INTO rol_operacion VALUES(3,	16)
INSERT INTO rol_operacion VALUES(3,	17)





/*insert into categoria*/
INSERT INTO categoria VALUES(1,	'Tecnología y Computadoras')
INSERT INTO categoria VALUES(2,	'Libros')
INSERT INTO categoria VALUES(3	,'Ropa y Accesorios')
INSERT INTO categoria VALUES(4,	'Hogar y Cocina')
INSERT INTO categoria VALUES(5	,'Juegos y Juguetes')
INSERT INTO categoria VALUES(6	,'Salud y Belleza')
INSERT INTO categoria VALUES(7	,'Alimentos y Bebidas')

/*insert into subcategoria*/

INSERT INTO subcategoria VALUES(1,	1,	'Laptops')
INSERT INTO subcategoria VALUES(2,	1,	'Tablets')
INSERT INTO subcategoria VALUES(3,	1,	'Computadoras de Escritorio')
INSERT INTO subcategoria VALUES(4,	1,	'Monitores')
INSERT INTO subcategoria VALUES(5,	1,	'Componentes')
INSERT INTO subcategoria VALUES(1,	2,	'Top Sellers')
INSERT INTO subcategoria VALUES(2,	2,	'Ciencia Ficcion')
INSERT INTO subcategoria VALUES(3,	2,	'Fantasia')
INSERT INTO subcategoria VALUES(4,	2,	'Miedo')
INSERT INTO subcategoria VALUES(1,	3,	'Ropa')
INSERT INTO subcategoria VALUES(2,	3,	'Zapatos')
INSERT INTO subcategoria VALUES(3,	3,	'Accesorios')
INSERT INTO subcategoria VALUES(4,	3,	'Relojes')
INSERT INTO subcategoria VALUES(1,	4,	'Cocina')
INSERT INTO subcategoria VALUES(2,	4,	'Electrodomesticos')
INSERT INTO subcategoria VALUES(3,	4,	'Limpieza')
INSERT INTO subcategoria VALUES(4,	4,	'Baño')
INSERT INTO subcategoria VALUES(5,	4,	'Decoracion')
INSERT INTO subcategoria VALUES(6,	4,	'Arte')
INSERT INTO subcategoria VALUES(7,	4,	'Jardin')
INSERT INTO subcategoria VALUES(1,	5,	'Juguetes')
INSERT INTO subcategoria VALUES(2,	5,	'Juegos de Mesa')
INSERT INTO subcategoria VALUES(3,	5,	'Aire Libre')
INSERT INTO subcategoria VALUES(4,	5,	'Muñecas')
INSERT INTO subcategoria VALUES(1,	6,	'Cuidado de la Piel')
INSERT INTO subcategoria VALUES(2,	6,	'Maquillaje')
INSERT INTO subcategoria VALUES(3,	6,	'Lociones')
INSERT INTO subcategoria VALUES(4,	6,	'Shampoo')
INSERT INTO subcategoria VALUES(1,	7,	'Alimentos')
INSERT INTO subcategoria VALUES(2,	7,	'Bebidas')









