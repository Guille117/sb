-- Consultas avanzadas


-- Obtener un reporte de los libros prestados y su estado
-- verifica la fecha de devoluci�n y si la fecha ya venci� y no se devuelto el libro
-- agrega el estado 'vencido', si aun hay tiempo para la devoluci�n del libro agrega 'activo'
CREATE PROCEDURE sp_ReporteLibrosPrestados
	AS
	BEGIN

		SELECT L.ID_Libro, L.Titulo, 
		P.ID_Prestamo, cast(P.Fecha_Prestamo AS DATE) as Fecha_Prestamo, 
		cast(P.Fecha_Devolucion AS DATE) AS Fecha_Devolucion,
		CASE				-- usamos un caso
			WHEN P.Fecha_Devolucion < getdate()		-- cuando la fecha de devoluci�n es menor a la fecha actual
			THEN 'Pr�stamo vencido.'
			ELSE 'Pr�stamo activo'
	END AS Estado
	FROM Libros L
	join Prestamos P ON L.ID_Libro = P.ID_Libro			-- unir Libros con prestamos
	WHERE L.Disponibilidad = 0 and P.Estado = 1;
END


-- libros m�s solicitados en el �ltimo mes
CREATE PROCEDURE sp_librosMasSolicitadosUltimoMes
AS
BEGIN
    
    SELECT 
        L.ID_Libro,                
        L.Titulo,                  
        L.Autor,                   
        COUNT(P.ID_Prestamo) AS Numero_De_Prestamos -- Cuenta el n�mero de pr�stamos
    FROM Libros L
    JOIN Prestamos P
    ON L.ID_Libro = P.ID_Libro								 -- Une las tablas de Libros y Pr�stamos por ID_Libro
    WHERE P.Fecha_Prestamo >= DATEADD(MONTH, -1, GETDATE())   -- Filtra por pr�stamos en el �ltimo mes
    GROUP BY L.ID_Libro, L.Titulo, L.Autor					  -- Agrupa los resultados por libro
    ORDER BY Numero_De_Prestamos DESC;						  -- Ordena los resultados por el n�mero de pr�stamos, de mayor a menor
END


-- miembros con mas prestamos activos
CREATE PROCEDURE sp_miembrosConMasPrestamosActivos
AS
BEGIN
    SELECT 
        M.ID_Miembro,                
        M.Nombre,                    
        M.Tel�fono,                  
        COUNT(P.ID_Prestamo) AS Prestamos_Activos -- Cuenta el n�mero de pr�stamos activos
    FROM Miembros M
    JOIN Prestamos P
    ON M.ID_Miembro = P.ID_Miembro			  -- Une las tablas de Miembros y Pr�stamos por ID_Miembro
	JOIN Libros L ON L.ID_Libro = P.ID_Libro  -- Une la tabla de Libros para verificar disponibilidad
    WHERE L.Disponibilidad = 0				  -- Filtra por libros no disponibles (pr�stamos activos)
    GROUP BY M.ID_Miembro, M.Nombre, M.Direccion, M.Tel�fono -- Agrupa los resultados por miembro
    ORDER BY Prestamos_Activos DESC;		  -- Ordena los resultados por el n�mero de pr�stamos activos, de mayor a menor
END


 -- libros que no han sido prestados en el �ltimo a�o
CREATE PROCEDURE sp_librosNoPrestadosUltimoA�o
AS
BEGIN
   
    SELECT 
        L.ID_Libro,  
        L.Titulo,     
        L.Autor       
    FROM Libros L
    LEFT JOIN Prestamos P
    ON L.ID_Libro = P.ID_Libro								-- Unir las tablas por el ID del libro
    AND P.Fecha_Prestamo >= DATEADD(MONTH, -12, GETDATE())  -- Filtrar pr�stamos en el �ltimo a�o
    WHERE P.ID_Prestamo IS NULL								-- Solo obtener libros sin pr�stamos en el per�odo
    ORDER BY L.Titulo;										-- Ordenar resultados por t�tulo
END;


use tarea5;