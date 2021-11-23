use master
go
create database BD_ComprasYa
go

USE BD_ComprasYa
GO

create table rol(
docentry int,
rol nvarchar(50),
rutaManual nvarchar(300),
primary key (docentry))
go


create table operacion(
Id int,
Nombre nvarchar(100),
primary key (Id))
go


create table rol_operacion (
IdRol int,
IdOperacion int,
primary key (IdRol,IdOperacion),
foreign key (IdRol) references rol(docentry),
foreign key (IdOperacion) references operacion(Id))
go

create table usuario
(
docentry  int,
dni char(8) not null unique,
nombre varchar(250) NOT NULL,
apellido varchar(250)NOT NULL,
fecreg datetime not null ,
fecnac date not null,
direccion varchar(250) not null,
pais varchar(250) not null,
telefono varchar(12) not null,
correo varchar(250) NOT NULL,
rol int,
opCreacion varchar(250) not null,
contraseña varchar(32) not null,
constraint pk_u primary key (docentry),
constraint fk_u foreign key (rol) references rol(docentry))
go


create table menu (
Id int,
Descripcion nvarchar(50),
NombreOperacion nvarchar(100),
primary key(Id));


create table acceso_menu(
IdRol int,
IdMenu int,
Linea int,
primary key (IdRol,IdMenu),
foreign key (IdRol) references rol(docentry),
foreign key (IdMenu) references menu(Id))
go

create table categoria
(
docentry int,
tipo varchar(160),
constraint pk_c primary key (docentry))
go

create table subcategoria
(
docentry int,
categoria int,
tipo varchar(160),
constraint pk_sc primary key (docentry,categoria),
constraint fk_sc1 foreign key (categoria) references categoria(docentry))
go

create table producto
(
docentry int,
nombre varchar(160),
descripcion varchar(350),
marca varchar(100),
categoria int,
subcategoria int,
precio decimal(10,2),
stock int,
garantia char(2),
color varchar(20),
fotos nvarchar(250),
fotos2 nvarchar(250),
fotos3 nvarchar(250),
idRegistro int,
opRegistro nvarchar(100),
tiempoRegistro datetime,
constraint pk_p2 primary key (docentry),
constraint fk_p12 foreign key (subcategoria,categoria) references subcategoria(docentry,categoria),
constraint fk_p13 foreign key (idRegistro) references usuario(docentry))
go

create table pedido(
docentry int,
idproducto int,
nomproducto varchar(100),
idvendedor int ,
vendedor varchar(100),
fecreg date,
idcliente int,
nomcliente varchar(500),
lugarDestino varchar(250),
precio decimal(10,2),
cantidad int,
descuento decimal(10,2),
total decimal(10,2),
estado varchar(20),
constraint pk_pd primary key (docentry),
constraint fk_prv foreign key (idvendedor) references usuario(docentry),
constraint fk_pr foreign key (idproducto) references producto(docentry),
constraint fk_pd foreign key (idcliente) references usuario(docentry))
go

create table comprobante(
docentry int ,
fecreg datetime,
idpedido int,
cliente int,
tipo varchar(30),
modpago varchar(30),
cuotas int,
constraint pk_cp primary key (docentry),
constraint fk_pc1 foreign key (idpedido) references pedido(docentry),
constraint fk_pc2 foreign key (cliente) references usuario(docentry))
go


create table gene(
Tabla varchar(30) ,
docentry int,
docnum int,
modo nvarchar(10),
constraint pk_ge primary key (Tabla))
go