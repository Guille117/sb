-- Creaci�n de base de datos

create database tarea5;
use tarea5;

-- creaci�n de tabla libros, la disponibilidad tendr� un valor por default de disponible(true)
create table Libros(
ID_Libro int primary key identity, 
Titulo nvarchar(100) not null,
Autor nvarchar(100) not null,
A�o_Publicacion int not null,
--ISBN, 
Disponibilidad bit not null default(1)
);

-- creaci�n de tabla miembros, la fecha e registro tendr� como valor por defecto la fecha actual
-- y el n�mero de tel�fono tendra un valor �nico
create table Miembros(
ID_Miembro int primary key identity,
Nombre nvarchar(100) not null, 
Direccion nvarchar(100) not null,
Tel�fono int unique not null,
Fecha_Registro datetime not null default(getdate())
);

-- Creaci�n de tabla Prestamos, tendra dos llaves foraneas (libro y miembro) y la fecha del prestamo por 
-- default ser� la fecha actual.
create table Prestamos(
ID_Prestamo int primary key identity,
ID_Libro int references Libros(ID_Libro),
ID_Miembro int references Miembros(Id_Miembro),
Fecha_Prestamo datetime not null default(getdate()), 
Fecha_Devolucion datetime not null,
Estado bit not null default(1)
);

--  Creaci�n de �ndices para cada tabla
-- Libros
create index indexLibro on Libros(ID_Libro);

-- Miembros
create index indexMiembro on Miembros(ID_Miembro);

-- Prestamos
create index indexPrestamos on Prestamos(ID_Prestamo);


use tarea5;