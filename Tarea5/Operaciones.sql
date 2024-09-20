--  Sección de acciones


--------------- Acciones para libros -------------------

-- agregar libro
		-- Título  --  Autor  --  año publicación 
exec sp_guardarLibro 'Los campos de batalla de la guerra fría', 'John Lewis Gaddis', 2005;		

-- consultar libros disponibles
exec sp_librosDisponibles;


-- Actualizar información de un libro.
		-- id libro  --  Título  --  Autor  --  año publicación 
exec sp_actualizarLibro 6, 'Crónicas', null, 2000;


-- eliminar libro
		-- id libro
exec sp_eliminarLibro 6;


--------------- Acciones para Mimbros -------------------


-- Registrar un nuevo miembro.
		--  nombre  --  dirección  --  telefono
exec sp_registrarMiembro 'Mario Cumes', 'Avenida los arboles', 89456783


-- Actualizar datos de un miembro.
		--  idMiembro  --  nombre  --  dirección  --  telefono
exec sp_actualizarMiembro 1, 'Julia Ortega', null, null;


-- Eliminar un miembro.
		--  idMiembro 
exec sp_eliminarMiembro 7;



--------------- Acciones para Prestamos -------------------

-- Registrar un nuevo préstamo.
		--  idLibro  --  idMiembro  --   Fecha devolución
exec sp_registrarPrestamo 10, 4, '2024/9/21';


-- Actualizar información de un préstamo.
		--  idPrestamo  --  idLibro  --  idMiembro  --   Fecha devolución
exec sp_actualizarPrestamo 1, null, 5, null


-- Devolver un libro = finalizar un prestamo
		--  idPrestamo
exec sp_finalizarPrestamo 13;


--------------- Consultas avanzados -------------------

-- Obtener un reporte de los libros prestados y su estado
exec sp_ReporteLibrosPrestados;


-- libros más solicitados en el último mes
exec sp_librosMasSolicitadosUltimoMes;


-- miembros con mas prestamos activos
exec sp_miembrosConMasPrestamosActivos;


 -- libros que no han sido prestados en el último año
exec sp_librosNoPrestadosUltimoAño;




use tarea5




