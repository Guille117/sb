-- creando base de datos
create database BibliotecaDB
use BibliotecaDB

-- creando tabla Libros

create table Libros(
ID_Libro int primary key identity,
Titulo nvarchar(150) not null,
Autor nvarchar(150) not null,
Año_Publicacion int not null,
Genero nvarchar(100),
Disponibilidad bit not null
);

create table Miembros(
ID_Miembro int primary key identity,
Nombre nvarchar(70) not null,
Apellido nvarchar(70) not null,
Fecha_Registro Date not null,
Tipo_Membresia varchar(10) not null
);

