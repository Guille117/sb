-- Creaci�n de procedimientos almacenados

create proc guardarLibro
	@titulo nvarchar(100),
	@autor nvarchar(100),
	@a�oPublicacion int
	as 
	begin
		declare @a�oActual int = year(getdate());
		if @a�oPublicacion < '1940' or @a�oPublicacion > @a�oActual
		begin
		raiserror('El a�o de publicaci�n debe estar entre 1940 y el a�o actual',16,1);
		return;
	end;
	insert into Libros(Titulo, Autor, A�o_Publicacion) values(@titulo, @autor, @a�oPublicacion);
	end;

	exec guardarLibro '10 cosas', 'Mario Gonzales', 2012;


	select * from Libros;

	