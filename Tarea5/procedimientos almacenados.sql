-- Creación de procedimientos almacenados


-- Guardar libro
create proc sp_guardarLibro
	@titulo nvarchar(100),			-- declarar variables
	@autor nvarchar(100),
	@añoPublicacion int
as 
begin
	declare @añoActual int = year(getdate());			-- obtenemos el año actual 
	if @añoPublicacion < '1440' or @añoPublicacion > @añoActual			-- validamos
		begin
			raiserror('El año de publicación debe estar entre 1440 y el año actual',16,1);  -- retornamos error si es necesario 
			return;
		end;
	insert into Libros(Titulo, Autor, Año_Publicacion) values(@titulo, @autor, @añoPublicacion);   -- guardamos
end;


	-- actualizar libro
CREATE PROCEDURE sp_actualizarLibro
    @ID_Libro INT,
    @Titulo NVARCHAR(100) = NULL,   -- variables auxiliares
    @Autor NVARCHAR(100) = NULL,
    @AñoPublicacion INT = NULL
AS
BEGIN
	-- validamos la existencia del libro
	EXEC sp_validarId 'Libros', 'ID_Libro', @ID_Libro;

	-- validamos el año de publicación
	DECLARE @añoActual INT = year(getdate());							-- obtenemos el año actual 
	IF @AñoPublicacion < '1440' or @AñoPublicacion > @añoActual			-- validamos
		BEGIN
			RAISERROR('El año de publicación debe estar entre 1440 y el año actual',16,1);  -- retornamos mansaje de error si es necesario 
			Return;
		END

	UPDATE Libros
		SET 
			Titulo = COALESCE(@Titulo, Titulo),			-- mantener el valor actual si es nulo.
			Autor = COALESCE(@Autor, Autor),
			Año_Publicacion = COALESCE(@AñoPublicacion, Año_Publicacion)
		WHERE ID_Libro = @ID_Libro;
END;


-- Eliminar un libro.
CREATE PROCEDURE sp_eliminarLibro
    @ID_Libro INT
AS
BEGIN
	EXEC sp_validarId 'Libros', 'ID_Libro', @ID_Libro;			-- validamos existencia
	IF EXISTS (SELECT * FROM Libros WHERE ID_Libro = @ID_Libro AND Disponibilidad = 0)		-- validamos si es posible eliminar
		BEGIN 
			RAISERROR('El libro no se puede eliminar ahora porque que está en préstamo', 16, 1);
			RETURN 
		END
    DELETE FROM Libros
    WHERE ID_Libro = @ID_Libro;
END;


-- Mostrar libros disponibles
CREATE PROCEDURE sp_librosDisponibles
AS
BEGIN
	SELECT * FROM Libros
	WHERE Disponibilidad = 1;		-- condición
END;


-- Registrar un nuevo miembro
CREATE PROCEDURE sp_registrarMiembro
    @Nombre NVARCHAR(100),			
    @Direccion NVARCHAR(100),
    @Telefono INT
AS
BEGIN
    BEGIN TRY					-- intentar insertar
        INSERT INTO Miembros (Nombre, Direccion, Teléfono)
        VALUES (@Nombre, @Direccion, @Telefono);
    END TRY
    BEGIN CATCH				-- atrapar error en caso exista
        PRINT 'Error: El número de teléfono ya está registrado.';
    END CATCH;
END;


-- Actualizar datos de un miembro.
CREATE PROCEDURE sp_actualizarMiembro
    @ID_Miembro INT,
    @Nombre NVARCHAR(100) = NULL,			-- variables auxiliares
    @Direccion NVARCHAR(100) = NULL,
    @Telefono INT = NULL
AS
BEGIN
	EXEC sp_validarId 'Miembros', 'ID_Miembro', @ID_Miembro;	-- validar existencia
	BEGIN TRY				-- intentar actualizar
		UPDATE Miembros
		SET 
			Nombre = COALESCE(@Nombre, Nombre),
			Direccion = COALESCE(@Direccion, Direccion),
			Teléfono = COALESCE(@Telefono, Teléfono)
		WHERE ID_Miembro = @ID_Miembro;
	END TRY
	BEGIN CATCH			-- atrapar error y mostrar mensaje de error
		PRINT ERROR_MESSAGE();
	END CATCH
END;


-- Eliminar un miembro.
CREATE PROCEDURE sp_eliminarMiembro
    @ID_Miembro INT
AS
BEGIN
		EXEC sp_validarId 'Miembros', 'ID_Miembro', @ID_Miembro;  -- validar existencia
		IF EXISTS (SELECT ID_Prestamo FROM Prestamos Where ID_Miembro = @ID_Miembro)
		BEGIN
			RAISERROR('No es posible eliminar a este miembro porque aun tiene préstamos activos', 16,1)
			RETURN 
		END
		DELETE FROM Miembros
    WHERE ID_Miembro = @ID_Miembro;
END;


--Registrar un nuevo préstamo.
CREATE PROCEDURE sp_registrarPrestamo
    @ID_Libro INT,
    @ID_Miembro INT,
    @Fecha_Devolucion DATETIME
AS
BEGIN
	-- Verificar disponibilidad del libro
	IF EXISTS (SELECT 1 FROM Libros WHERE ID_Libro = @ID_Libro AND Disponibilidad = 1)
	BEGIN
		-- validamos la existencia de miembro
		EXEC sp_validarId 'Miembros','ID_Miembro',@ID_Miembro;

		-- validamos que la fecha de devolución sea posterior o igual a la fecha actual
		DECLARE @fechaActual DATETIME = getdate();
		IF @fechaActual >= @Fecha_Devolucion
			BEGIN
				RAISERROR('Error: Fecha de devolución incorrecta',16,1);
				RETURN 
			END
		-- Registrar el préstamo
		INSERT INTO Prestamos (ID_Libro, ID_Miembro, Fecha_Devolucion)
		VALUES (@ID_Libro, @ID_Miembro, @Fecha_Devolucion);
        
		-- Actualizar la disponibilidad del libro a no disponible (0)
		UPDATE Libros
		SET Disponibilidad = 0
		WHERE ID_Libro = @ID_Libro;
	END
		ELSE
		BEGIN
		RAISERROR('Error: El libro no está disponible',16,1);
		RETURN
    END
END;

-- actualizar Prestamo
CREATE PROCEDURE sp_actualizarPrestamo
    @ID_Prestamo INT,
    @ID_Libro INT = NULL,				
    @ID_Miembro INT = NULL,
    @Fecha_Devolucion DATETIME = NULL
AS
BEGIN
    -- Validamos el ID del préstamo
    EXEC sp_validarId 'Prestamos', 'ID_Prestamo', @ID_Prestamo;

    DECLARE @LibroActual INT;

    -- Obtener el ID del libro actual
    SELECT @LibroActual = ID_Libro FROM Prestamos WHERE ID_Prestamo = @ID_Prestamo;

    -- Validar nuevo libro si se proporciona
    IF @ID_Libro IS NOT NULL
    BEGIN
        -- Verificar que el nuevo libro esté disponible
        IF NOT EXISTS (SELECT 1 FROM Libros WHERE ID_Libro = @ID_Libro AND Disponibilidad = 1)
        BEGIN
            RAISERROR('Error: El nuevo libro no está disponible', 16, 1);
            RETURN;
        END
    END

    -- Validar que la fecha de devolución sea posterior o igual a la fecha actual si se proporciona
    IF @Fecha_Devolucion IS NOT NULL
    BEGIN
        DECLARE @fechaActual DATETIME = GETDATE();
        IF @fechaActual >= @Fecha_Devolucion
        BEGIN
            RAISERROR('Error: Fecha de devolución incorrecta', 16, 1);
            RETURN;
        END
    END

    -- Actualizar el préstamo
    UPDATE Prestamos
    SET 
        ID_Libro = COALESCE(@ID_Libro, ID_Libro),
        ID_Miembro = COALESCE(@ID_Miembro, ID_Miembro),
        Fecha_Devolucion = COALESCE(@Fecha_Devolucion, Fecha_Devolucion)
    WHERE ID_Prestamo = @ID_Prestamo;

    -- Si se cambió el libro, actualizar la disponibilidad del libro anterior
    IF @ID_Libro IS NOT NULL AND @ID_Libro <> @LibroActual
    BEGIN
        UPDATE Libros
        SET Disponibilidad = 1
        WHERE ID_Libro = @LibroActual;
        
        -- Actualizar la disponibilidad del nuevo libro a no disponible
        UPDATE Libros
        SET Disponibilidad = 0
        WHERE ID_Libro = @ID_Libro;
    END
END;



-- Finalizar prestamo

CREATE PROCEDURE sp_finalizarPrestamo
    @ID_Prestamo INT
AS
BEGIN
	
	EXEC sp_validarId 'Prestamos', 'ID_Prestamo', @ID_Prestamo;     -- validamos existencia del préstamo

	-- actualizar la fecha de devolución
	UPDATE Prestamos
	SET Fecha_Devolucion = GETDATE()
	WHERE ID_Prestamo = @ID_Prestamo;

	-- Actualizar la disponibilidad del libro a disponible (1)
	UPDATE Libros
	SET Disponibilidad = 1
	WHERE ID_Libro = (select ID_Libro from Prestamos where ID_Prestamo = @ID_Prestamo);

	-- Actualizar el estado del préstamo para la tabla préstamos
	UPDATE 
	Prestamos SET Estado = 0
	WHERE ID_Prestamo = @ID_Prestamo
END;


-- proceso almacenado para validar la existencia por medio ID para cualquier tabla
CREATE PROCEDURE sp_validarId
    @Tabla NVARCHAR(100),     -- nombre de la tabla
    @Columna NVARCHAR(100),   -- nombre de la columna
    @ID INT                   -- valor a buscar	
AS 
BEGIN
    DECLARE @Sql NVARCHAR(MAX);

    SET @Sql = 'IF NOT EXISTS(SELECT * FROM ' + QUOTENAME(@Tabla) +   
    ' WHERE ' + QUOTENAME(@Columna) + ' = @ID) ' +
    'BEGIN THROW 50000, ''No existe registro del ID ingresado.'', 1; END';

    EXEC sp_executesql @sql, N'@ID INT', @ID;  -- Ejecutamos la consulta
END;


use tarea5;