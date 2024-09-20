-- Creación de base de datos

create database tarea5;
use tarea5;

-- creación de tabla libros, la disponibilidad tendrá un valor por default de disponible(true)
create table Libros(
ID_Libro int primary key identity, 
Titulo nvarchar(100) not null,
Autor nvarchar(100) not null,
Año_Publicacion int not null,
--ISBN, 
Disponibilidad bit not null default(1)
);

-- creación de tabla miembros, la fecha e registro tendrá como valor por defecto la fecha actual
-- y el número de teléfono tendra un valor único
create table Miembros(
ID_Miembro int primary key identity,
Nombre nvarchar(100) not null, 
Direccion nvarchar(100) not null,
Teléfono int unique not null,
Fecha_Registro datetime not null default(getdate())
);

-- Creación de tabla Prestamos, tendra dos llaves foraneas (libro y miembro) y la fecha del prestamo por 
-- default será la fecha actual.
create table Prestamos(
ID_Prestamo int primary key identity,
ID_Libro int references Libros(ID_Libro),
ID_Miembro int references Miembros(Id_Miembro),
Fecha_Prestamo datetime not null default(getdate()), 
Fecha_Devolucion datetime not null,
Estado bit not null default(1)
);

--  Creación de índices para cada tabla
-- Libros
create index indexLibro on Libros(ID_Libro);

-- Miembros
create index indexMiembro on Miembros(ID_Miembro);

-- Prestamos
create index indexPrestamos on Prestamos(ID_Prestamo);


use tarea5;