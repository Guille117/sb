--  Secci�n de acciones


--------------- Acciones para libros -------------------

-- agregar libro
		-- T�tulo  --  Autor  --  a�o publicaci�n 
exec sp_guardarLibro 'Los campos de batalla de la guerra fr�a', 'John Lewis Gaddis', 2005;		

-- consultar libros disponibles
exec sp_librosDisponibles;


-- Actualizar informaci�n de un libro.
		-- id libro  --  T�tulo  --  Autor  --  a�o publicaci�n 
exec sp_actualizarLibro 6, 'Cr�nicas', null, 2000;


-- eliminar libro
		-- id libro
exec sp_eliminarLibro 6;


--------------- Acciones para Mimbros -------------------


-- Registrar un nuevo miembro.
		--  nombre  --  direcci�n  --  telefono
exec sp_registrarMiembro 'Mario Cumes', 'Avenida los arboles', 89456783


-- Actualizar datos de un miembro.
		--  idMiembro  --  nombre  --  direcci�n  --  telefono
exec sp_actualizarMiembro 1, 'Julia Ortega', null, null;


-- Eliminar un miembro.
		--  idMiembro 
exec sp_eliminarMiembro 7;



--------------- Acciones para Prestamos -------------------

-- Registrar un nuevo pr�stamo.
		--  idLibro  --  idMiembro  --   Fecha devoluci�n
exec sp_registrarPrestamo 10, 4, '2024/9/21';


-- Actualizar informaci�n de un pr�stamo.
		--  idPrestamo  --  idLibro  --  idMiembro  --   Fecha devoluci�n
exec sp_actualizarPrestamo 1, null, 5, null


-- Devolver un libro = finalizar un prestamo
		--  idPrestamo
exec sp_finalizarPrestamo 13;


--------------- Consultas avanzados -------------------

-- Obtener un reporte de los libros prestados y su estado
exec sp_ReporteLibrosPrestados;


-- libros m�s solicitados en el �ltimo mes
exec sp_librosMasSolicitadosUltimoMes;


-- miembros con mas prestamos activos
exec sp_miembrosConMasPrestamosActivos;


 -- libros que no han sido prestados en el �ltimo a�o
exec sp_librosNoPrestadosUltimoA�o;




use tarea5




