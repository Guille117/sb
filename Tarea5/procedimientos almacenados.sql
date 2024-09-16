-- Creación de procedimientos almacenados

create proc guardarLibro
	@titulo nvarchar(100),
	@autor nvarchar(100),
	@añoPublicacion int
	as 
	begin
		declare @añoActual int = year(getdate());
		if @añoPublicacion < '1940' or @añoPublicacion > @añoActual
		begin
		raiserror('El año de publicación debe estar entre 1940 y el año actual',16,1);
		return;
	end;
	insert into Libros(Titulo, Autor, Año_Publicacion) values(@titulo, @autor, @añoPublicacion);
	end;

	exec guardarLibro '10 cosas', 'Mario Gonzales', 2012;


	select * from Libros;

	