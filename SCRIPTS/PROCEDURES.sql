-- Bibliotecas:
DELIMITER $
CREATE PROCEDURE INSERTAR_BIBLIOTECA(
    OUT _id_biblioteca INT,
    IN _nombre VARCHAR(80),
    IN _ubicacion VARCHAR(60)
)
BEGIN
    INSERT INTO Biblioteca(nombre, ubicacion, activo)
    VALUES(_nombre, _ubicacion, 1);
    
    SET _id_biblioteca = LAST_INSERT_ID();
END$

-- MODIFICAR
DELIMITER $

CREATE PROCEDURE MODIFICAR_BIBLIOTECA(
    IN _id_biblioteca INT,
    IN _nombre VARCHAR(80),
    IN _ubicacion VARCHAR(60)
)
BEGIN
    UPDATE Biblioteca
    SET nombre = _nombre,
        ubicacion = _ubicacion
    WHERE id_biblioteca = _id_biblioteca;
END$
	
    
    
    
    -- ELIMINAR
DELIMITER $

CREATE PROCEDURE ELIMINAR_BIBLIOTECA(
    IN _id_biblioteca INT
)
BEGIN
    UPDATE Biblioteca
    SET activo = 0
    WHERE id_biblioteca = _id_biblioteca;
END$

-- BUSCAR POR ID
DELIMITER $

CREATE PROCEDURE OBTENER_BIBLIOTECA_X_ID(
    IN _id_biblioteca INT
)
BEGIN
    SELECT id_biblioteca, nombre, ubicacion, activo
    FROM Biblioteca
    WHERE id_biblioteca = _id_biblioteca;
END$

-- LISTAR
DELIMITER $

CREATE PROCEDURE LISTAR_BIBLIOTECAS_TODAS()
BEGIN
    SELECT id_biblioteca, nombre, ubicacion, activo
    FROM Biblioteca
    WHERE activo = 1;
END$

-- Libros:

DELIMITER $

CREATE PROCEDURE INSERTAR_LIBRO(
    OUT _id_libro INT,
    IN _titulo VARCHAR(150),
    IN _anho_publicacion INT,
    IN _numero_paginas INT,
    IN _clasificacion_tematica VARCHAR(100),
    IN _idioma VARCHAR(40),
    IN _ISBN CHAR(13),
    IN _edicion VARCHAR(20)
)
BEGIN
	INSERT INTO MaterialBibliografico(
        titulo,
        anho_publicacion,numero_paginas,estado,clasificacion_tematica,activo,idioma, tipo
    )
    VALUES(
        _titulo,_anho_publicacion,_numero_paginas,'NO_DISPONIBLE',_clasificacion_tematica,1,_idioma,'LIBRO'
    );
    SET _id_libro = @@last_insert_id;
    INSERT INTO Libro(id_libro, ISBN, edicion)
    VALUES(_id_libro, _ISBN, _edicion);
    
END$

-- MODIFICAR
DELIMITER $
-- idioma VARCHAR(40) NOT NULL,
CREATE PROCEDURE MODIFICAR_LIBRO(
    IN _id_libro INT,
    IN _titulo VARCHAR(150),
    IN _anho_publicacion INT,
    IN _numero_paginas INT,
    IN _clasificacion_tematica VARCHAR(100),
    IN _idioma VARCHAR(40),
    IN _ISBN CHAR(13),
    IN _edicion VARCHAR(20)
    
)
BEGIN
	UPDATE MaterialBibliografico SET titulo=_titulo,
        anho_publicacion=_anho_publicacion,numero_paginas=_numero_paginas,
        clasificacion_tematica=_clasificacion_tematica,
        idioma=_idioma WHERE id_material=_id_libro;
    UPDATE Libro SET ISBN = _ISBN,
        edicion = _edicion
    WHERE id_libro = _id_libro;
END$

-- BUSCAR POR ID
DELIMITER $
    -- ELIMINAR

CREATE PROCEDURE ELIMINAR_LIBRO(
    IN _id_libro INT
)
BEGIN
    UPDATE MaterialBibliografico
    SET activo = 0
    WHERE id_material = _id_libro;
END$

-- BUSCAR POR ID
DELIMITER $
CREATE PROCEDURE OBTENER_LIBRO_X_ID(
    IN _id_libro INT
)
BEGIN

	SELECT l.id_libro, m.titulo, m.anho_publicacion, m.numero_paginas, m.estado, m.clasificacion_tematica, m.activo, m.idioma,m.tipo, l.ISBN, l.edicion
    FROM MaterialBibliografico m INNER JOIN Libro l
    ON m.id_material=l.id_libro  WHERE l.id_libro=_id_libro;
END$

-- LISTAR
DELIMITER $

CREATE PROCEDURE LISTAR_LIBROS_TODOS()
BEGIN
    SELECT l.id_libro, m.titulo, m.anho_publicacion, m.numero_paginas, m.estado, m.clasificacion_tematica, m.activo, m.idioma,m.tipo, l.ISBN, l.edicion
    FROM MaterialBibliografico m INNER JOIN Libro l
    ON m.id_material=l.id_libro and m.activo=1;
END$

-- Articulo:

DELIMITER $

CREATE PROCEDURE INSERTAR_ARTICULO(
    OUT _id_articulo INT,
    IN _titulo VARCHAR(150),
    IN _anho_publicacion INT,
    IN _numero_paginas INT,
    IN _clasificacion_tematica VARCHAR(100),
    IN _idioma VARCHAR(40),
    IN _ISSN CHAR(9),
    IN _revista VARCHAR(100),
    IN _volumen INT,
    IN _numero INT
)
BEGIN
	INSERT INTO MaterialBibliografico(
        titulo,
        anho_publicacion,numero_paginas,estado,clasificacion_tematica,activo,idioma, tipo
    )
    VALUES(
        _titulo,_anho_publicacion,_numero_paginas,'NO_DISPONIBLE',_clasificacion_tematica,1,_idioma,'ARTICULO'
    );
    SET _id_articulo = @@last_insert_id;
    INSERT INTO Articulo(id_articulo, ISSN, revista, volumen, numero)
    VALUES(_id_articulo, _ISSN, _revista, _volumen, _numero);
    
END$

-- MODIFICAR
DELIMITER $

CREATE PROCEDURE MODIFICAR_ARTICULO(
    IN _id_articulo INT,
    IN _titulo VARCHAR(150),
    IN _anho_publicacion INT,
    IN _numero_paginas INT,
    IN _clasificacion_tematica VARCHAR(100),
    IN _idioma VARCHAR(40),
    IN _ISSN CHAR(9),
    IN _revista VARCHAR(100),
    IN _volumen INT,
    IN _numero INT
    
)
BEGIN
	UPDATE MaterialBibliografico SET titulo=_titulo,
        anho_publicacion=_anho_publicacion,numero_paginas=_numero_paginas,
        clasificacion_tematica=_clasificacion_tematica,
        idioma=_idioma WHERE id_material=_id_articulo;
    UPDATE Articulo SET ISSN = _ISSN, revista=_revista, volumen=_volumen, numero=_numero
    WHERE id_articulo = _id_articulo;
END$

-- BUSCAR POR ID
DELIMITER $
    -- ELIMINAR

CREATE PROCEDURE ELIMINAR_ARTICULO(
    IN _id_articulo INT
)
BEGIN
    UPDATE MaterialBibliografico
    SET activo = 0
    WHERE id_material = _id_articulo;
END$

-- BUSCAR POR ID
DELIMITER $
CREATE PROCEDURE OBTENER_ARTICULO_X_ID(
    IN _id_articulo INT
)
BEGIN

	SELECT a.id_articulo, m.titulo, m.anho_publicacion, m.numero_paginas, m.estado, m.clasificacion_tematica, m.activo, m.idioma,m.tipo,
    a.ISSN, a.numero, a.revista, a.volumen
    FROM MaterialBibliografico m INNER JOIN Articulo a
    ON m.id_material=a.id_articulo  WHERE a.id_articulo=_id_articulo;
END$

-- LISTAR
DELIMITER $

CREATE PROCEDURE LISTAR_ARTICULOS_TODOS()
BEGIN
    SELECT a.id_articulo, m.titulo, m.anho_publicacion, m.numero_paginas, m.estado, m.clasificacion_tematica, m.activo, m.idioma,m.tipo,
    a.ISSN, a.numero, a.revista, a.volumen
    FROM MaterialBibliografico m INNER JOIN Articulo a
    ON m.id_material=a.id_articulo and m.activo=1;
END$

-- TESIS
DELIMITER $

CREATE PROCEDURE INSERTAR_TESIS(
    OUT _id_tesis INT,
    IN _titulo VARCHAR(150),
    IN _anho_publicacion INT,
    IN _numero_paginas INT,
    IN _clasificacion_tematica VARCHAR(100),
    IN _idioma VARCHAR(40),
    IN _especialidad VARCHAR(100),
    IN _asesor VARCHAR(60),
    IN _grado VARCHAR(50),
    IN _institucion_publicacion VARCHAR(150)
)
BEGIN
	INSERT INTO MaterialBibliografico(
        titulo,
        anho_publicacion,numero_paginas,estado,clasificacion_tematica,activo,idioma, tipo
    )
    VALUES(
        _titulo,_anho_publicacion,_numero_paginas,'NO_DISPONIBLE',_clasificacion_tematica,1,_idioma,'TESIS'
    );
    SET _id_tesis = @@last_insert_id;
    INSERT INTO Tesis(id_tesis, especialidad, asesor, 
    grado, institucion_publicacion)
    VALUES(_id_tesis, _especialidad, _asesor, _grado,
    _institucion_publicacion);
    
END$

-- MODIFICAR
DELIMITER $

CREATE PROCEDURE MODIFICAR_TESIS(
    IN _id_tesis INT,
    IN _titulo VARCHAR(150),
    IN _anho_publicacion INT,
    IN _numero_paginas INT,
    IN _clasificacion_tematica VARCHAR(100),
    IN _idioma VARCHAR(40),
    IN _especialidad VARCHAR(100),
    IN _asesor VARCHAR(60),
    IN _grado VARCHAR(50),
    IN _institucion_publicacion VARCHAR(150)
    
)
BEGIN
	UPDATE MaterialBibliografico SET titulo=_titulo,
        anho_publicacion=_anho_publicacion,numero_paginas=_numero_paginas,
        clasificacion_tematica=_clasificacion_tematica,
        idioma=_idioma WHERE id_material=_id_tesis;
    UPDATE Tesis SET especialidad = _especialidad, asesor=_asesor, grado=_grado, institucion_publicacion=_institucion_publicacion
    WHERE id_tesis = _id_tesis;
END$

-- BUSCAR POR ID
DELIMITER $
    -- ELIMINAR

CREATE PROCEDURE ELIMINAR_TESIS(
    IN _id_tesis INT
)
BEGIN
    UPDATE MaterialBibliografico
    SET activo = 0
    WHERE id_tesis = _id_tesis;
END$

-- BUSCAR POR ID
DELIMITER $
CREATE PROCEDURE OBTENER_TESIS_X_ID(
    IN _id_tesis INT
)
BEGIN

	SELECT t.id_tesis, m.titulo, m.anho_publicacion, m.numero_paginas, m.estado, m.clasificacion_tematica, m.activo, m.idioma,m.tipo,
    t.especialidad, t.asesor, t.grado, t.institucion_publicacion
    FROM MaterialBibliografico m INNER JOIN Tesis t
    ON m.id_material=t.id_tesis  WHERE t.id_tesis=_id_tesis;
END$

-- LISTAR
DELIMITER $

CREATE PROCEDURE LISTAR_TESIS_TODOS()
BEGIN
    SELECT t.id_tesis, m.titulo, m.anho_publicacion, m.numero_paginas, m.estado, m.clasificacion_tematica, m.activo, m.idioma,m.tipo,
    t.especialidad, t.asesor, t.grado, t.institucion_publicacion
    FROM MaterialBibliografico m INNER JOIN Tesis t
    ON m.id_material=t.id_tesis and m.activo=1;
END$
-- ROL:


-- INSERTAR 
DELIMITER $
CREATE PROCEDURE INSERTAR_ROL(
    OUT _id_rol INT,
    IN _tipo VARCHAR(30),
    IN _cantidad_de_dias_por_prestamo INT
)
BEGIN
    INSERT INTO Rol(tipo, activo, cantidad_de_dias_por_prestamo)
    VALUES(_tipo, 1, _cantidad_de_dias_por_prestamo);
    SET _id_rol = @@last_insert_id;
END$

-- MODIFICAR
DELIMITER $
CREATE PROCEDURE MODIFICAR_ROL(
    IN _id_rol INT,
    IN _tipo VARCHAR(30),
    IN _cantidad_de_dias_por_prestamo INT
)
BEGIN
    UPDATE Rol 
    SET tipo = _tipo, cantidad_de_dias_por_prestamo = _cantidad_de_dias_por_prestamo
    WHERE id_rol = _id_rol;
END$

-- ELIMINAR 
DELIMITER $
CREATE PROCEDURE ELIMINAR_ROL(
	IN _id_rol INT
)
BEGIN
	UPDATE Rol 
    SET activo = 0
    WHERE id_rol = _id_rol;

END$
-- OBTENER POR ID
DELIMITER $
CREATE PROCEDURE OBTENER_ROL_X_ID(
    IN _id_rol INT
)
BEGIN
    SELECT id_rol, tipo, activo, cantidad_de_dias_por_prestamo
    FROM Rol 
    WHERE id_rol = _id_rol;
END$

-- LISTAR 
DELIMITER $
CREATE PROCEDURE LISTAR_ROLES_TODOS()
BEGIN
    SELECT id_rol, tipo, activo, cantidad_de_dias_por_prestamo
    FROM Rol
    WHERE activo = 1;
END$

-- Usuario:


-- INSERTAR
DELIMITER $
CREATE PROCEDURE INSERTAR_USUARIO(
    OUT _id_usuario INT,
    IN _codigo_universitario INT,
    IN _nombre VARCHAR(50),
    IN _primer_apellido VARCHAR(50),
    IN _segundo_apellido VARCHAR(50),
    IN _DOI VARCHAR(30),
    IN _correo VARCHAR(100),
    IN _contrasena VARCHAR(40),
    IN _numero_de_telefono VARCHAR(12),
    IN _id_rol INT
)
BEGIN
    INSERT INTO Usuario(
        codigo_universitario, nombre,primer_apellido,segundo_apellido,DOI,
        contrasena,correo,numero_de_telefono,activo,id_rol
    )
    VALUES(
        _codigo_universitario, _nombre,_primer_apellido, _segundo_apellido,_DOI,
        MD5(_contrasena),_correo,_numero_de_telefono,1, _id_rol);
    
    SET _id_usuario = @@last_insert_id;
END$
-- MODIFICAR

DELIMITER $
CREATE PROCEDURE MODIFICAR_USUARIO(
    IN _id_usuario INT,
    IN _codigo_universitario INT,
    IN _nombre VARCHAR(50),
    IN _primer_apellido VARCHAR(50),
    IN _segundo_apellido VARCHAR(50),
    IN _DOI VARCHAR(30),
    IN _correo VARCHAR(100),
    IN _contrasena VARCHAR(40),
    IN _numero_de_telefono VARCHAR(12),
    IN _id_rol INT
)
BEGIN
    UPDATE Usuario 
    SET 
		codigo_universitario = _codigo_universitario,
        nombre = _nombre,
        primer_apellido = _primer_apellido,
        segundo_apellido = _segundo_apellido,
        DOI = _DOI,
        correo = _correo,
        contrasena = MD5(_contrasena),
        numero_de_telefono = _numero_de_telefono,
        id_rol = _id_rol
    WHERE id_usuario = _id_usuario;
END$

-- ELIMINAR
DELIMITER $
CREATE PROCEDURE ELIMINAR_USUARIO(
    IN _id_usuario INT
)
BEGIN
    UPDATE Usuario 
    SET activo = 0 
    WHERE id_usuario = _id_usuario;
END$

-- BUSCAR POR ID
DELIMITER $
CREATE PROCEDURE OBTENER_USUARIO_X_ID(
    IN _id_usuario INT
)
BEGIN
    SELECT id_usuario,codigo_universitario, nombre, primer_apellido, segundo_apellido, DOI, contrasena, correo, numero_de_telefono, activo, id_rol 
    FROM Usuario 
    WHERE id_usuario = _id_usuario;
END$

-- LISTAR
DELIMITER $
CREATE PROCEDURE LISTAR_USUARIOS_TODOS()
BEGIN
    SELECT id_usuario,codigo_universitario, nombre, primer_apellido, segundo_apellido, DOI, contrasena, correo, numero_de_telefono, activo, id_rol
    FROM Usuario 
    WHERE activo = 1;
END$

-- Contribuyente 

-- INSERTAR
DELIMITER $
CREATE PROCEDURE INSERTAR_CONTRIBUYENTE(
    OUT _id_contribuyente INT,
    IN _nombre VARCHAR(60),
    IN _primer_apellido VARCHAR(60),
    IN _segundo_apellido VARCHAR(60),
    IN _seudonimo VARCHAR(60),
    IN _tipo_contribuyente ENUM('AUTOR', 'EDITOR', 'ILUSTRADOR')
)
BEGIN
    INSERT INTO Contribuyente(nombre,primer_apellido,segundo_apellido,seudonimo,tipo_contribuyente)
    VALUES(_nombre, _primer_apellido,_segundo_apellido,_seudonimo, _tipo_contribuyente);
    
    SET _id_contribuyente = @@last_insert_id; 
END$

-- MODIFICAR
DELIMITER $
CREATE PROCEDURE MODIFICAR_CONTRIBUYENTE(
    IN _id_contribuyente INT,
    IN _nombre VARCHAR(60),
    IN _primer_apellido VARCHAR(60),
    IN _segundo_apellido VARCHAR(60),
    IN _seudonimo VARCHAR(60),
    IN _tipo_contribuyente ENUM('AUTOR', 'EDITOR', 'ILUSTRADOR')
)
BEGIN
    UPDATE Contribuyente
    SET 
        nombre = _nombre,
        primer_apellido = _primer_apellido,
        segundo_apellido = _segundo_apellido,
        seudonimo = _seudonimo,
        tipo_contribuyente = _tipo_contribuyente
    WHERE id_contribuyente = _id_contribuyente;
END$

-- BUSCAR POR ID
DELIMITER $
CREATE PROCEDURE OBTENER_CONTRIBUYENTE_X_ID(
    IN _id_contribuyente INT
)
BEGIN
    SELECT id_contribuyente, nombre, primer_apellido, segundo_apellido, seudonimo, tipo_contribuyente
    FROM Contribuyente
    WHERE id_contribuyente = _id_contribuyente;
END$

-- LISTAR TODOS
DELIMITER $
CREATE PROCEDURE LISTAR_CONTRIBUYENTES_TODOS()
BEGIN
    SELECT id_contribuyente, nombre, primer_apellido, segundo_apellido, seudonimo, tipo_contribuyente
    FROM Contribuyente;
END$

DELIMITER $
CREATE PROCEDURE ASIGNAR_CONTRIBUYENTE(
	IN _id_material INT,
    IN _id_contribuyente INT
)
BEGIN
	INSERT INTO Contribuyente_Material(id_material, id_contribuyente)
	VALUES (_id_material, _id_contribuyente);
END$

-- Editorial:

DELIMITER $
CREATE PROCEDURE INSERTAR_EDITORIAL(
	OUT _id_editorial INT,
    IN _nombre VARCHAR(100)
)
BEGIN
	INSERT INTO Editorial(nombre)
    VALUES (_nombre);
	SET _id_editorial = @@last_insert_id; 
END$

DELIMITER $
CREATE PROCEDURE MODIFICAR_EDITORIAL(
	IN _id_editorial INT,
    IN _nombre VARCHAR(100)
)
BEGIN
	UPDATE Editorial
    SET
		nombre=_nombre
    WHERE id_editorial = _id_editoral;
END$

DELIMITER $
CREATE PROCEDURE OBTENER_EDITORIAL_X_ID(
	IN _id_editorial INT
)
BEGIN
	SELECT id_editorial, nombre
    FROM Editorial
    WHERE id_editorial = _id_editorial;
END$

DELIMITER $
CREATE PROCEDURE LISTAR_EDITORIALES_TODAS(
	IN _id_editorial INT
)
BEGIN
	SELECT id_editorial, nombre
    FROM Editorial;
END$

DELIMITER $
CREATE PROCEDURE ASIGNAR_EDITORIAL(
	IN _id_material INT,
    IN _id_editorial INT
)
BEGIN
	INSERT INTO Editorial_Material(id_material, id_editorial)
	VALUES (_id_material, _id_editorial);
END$

-- Ejemplar:

-- INSERTAR
DELIMITER $

CREATE PROCEDURE INSERTAR_EJEMPLAR(
    OUT _id_ejemplar INT,
    IN _id_material INT,
    IN _ubicacion VARCHAR(50),
    IN _id_biblioteca INT
)
BEGIN
    INSERT INTO Ejemplar(id_material, estado, ubicacion, activo, id_biblioteca)
    VALUES(_id_material, 'DISPONIBLE', _ubicacion, 1, _id_biblioteca);
    
    SET _id_ejemplar = @@last_insert_id;  

    UPDATE MaterialBibliografico 
    SET estado = 'DISPONIBLE'
    WHERE id_material = _id_material;

END$

-- MODIFICAR
DELIMITER $

CREATE PROCEDURE MODIFICAR_EJEMPLAR(
    IN _id_ejemplar INT,
    IN _estado ENUM('DISPONIBLE', 'PRESTADO', 'EN_REPARACION', 'PERDIDO'),
    IN _ubicacion VARCHAR(50),
    IN _id_biblioteca INT
)
BEGIN
    UPDATE Ejemplar
    SET estado = _estado,
        ubicacion = _ubicacion,
        id_biblioteca = _id_biblioteca
    WHERE id_ejemplar = _id_ejemplar;
END$

-- ELIMINAR
DELIMITER $

CREATE PROCEDURE ELIMINAR_EJEMPLAR(
    IN _id_ejemplar INT
)
BEGIN
    UPDATE Ejemplar
    SET activo = 0 
    WHERE id_ejemplar = _id_ejemplar;
END$

-- BUSCAR POR ID
DELIMITER $

CREATE PROCEDURE OBTENER_EJEMPLAR_X_ID(
    IN _id_ejemplar INT
)
BEGIN
    SELECT id_ejemplar, id_material, estado, ubicacion, activo, id_biblioteca
    FROM Ejemplar
    WHERE id_ejemplar = _id_ejemplar;
END$

-- LISTAR
DELIMITER $
CREATE PROCEDURE LISTAR_EJEMPLARES_TODOS()
BEGIN
    SELECT id_ejemplar, id_material, estado, ubicacion, activo, id_biblioteca
    FROM Ejemplar
    WHERE activo = 1;
END$

-- Prestamo:

DELIMITER $

-- INSERTAR_PRESTAMO
CREATE PROCEDURE INSERTAR_PRESTAMO(
    OUT _id_prestamo INT,
    IN _id_ejemplar INT,
    IN _id_usuario INT
)
BEGIN
	DECLARE _fecha_de_prestamo  datetime;
    DECLARE cant_dias INT;
    DECLARE _id_rol  INT;
    DECLARE _fecha_vencimiento DATETIME;
    
    SET _fecha_de_prestamo = NOW();
	
    SELECT id_rol
    INTO _id_rol
    FROM Usuario
    WHERE id_usuario = _id_usuario;
    
    SELECT cantidad_de_dias_por_prestamo
    INTO cant_dias
    FROM Rol
    WHERE id_rol = _id_rol;
    
    SET _fecha_vencimiento = date_add(_fecha_de_prestamo, INTERVAL cant_dias DAY);
    
    INSERT INTO Prestamo(fecha_de_prestamo, fecha_vencimiento, fecha_devolucion, estado, id_ejemplar, id_usuario)
    VALUES(_fecha_de_prestamo, _fecha_vencimiento, null,'VIGENTE' , _id_ejemplar, _id_usuario);
    SET _id_prestamo = @@last_insert_id;
    
    UPDATE Ejemplar
    SET estado = 'PRESTADO'
    WHERE id_ejemplar = _id_ejemplar;
END$


DELIMITER $
-- Finalizar prestamo
CREATE PROCEDURE FINALIZAR_PRESTAMO(
    IN _id_prestamo INT
)
BEGIN
	DECLARE _id_ejemplar INT;
    SELECT id_ejemplar
    INTO _id_ejemplar
    FROM Prestamo
    WHERE id_prestamo = _id_prestamo;
    
    UPDATE Prestamo 
    SET fecha_devolucion = NOW(),
        estado = 'FINALIZADO'
    WHERE id_prestamo = _id_prestamo;
    
    UPDATE Ejemplar
    SET estado = 'DISPONIBLE'
    WHERE id_ejemplar = _id_ejemplar;
    
END$

DELIMITER $
-- OBTENER_PRESTAMO_X_ID
CREATE PROCEDURE OBTENER_PRESTAMO_X_ID(
    IN _id_prestamo INT
)
BEGIN
    SELECT id_prestamo, fecha_de_prestamo, fecha_vencimiento, fecha_devolucion, estado, id_ejemplar, id_usuario
    FROM Prestamo 
    WHERE id_prestamo = _id_prestamo;
END$


DELIMITER $
-- LISTAR_PRESTAMOS_TODOS
CREATE PROCEDURE LISTAR_PRESTAMOS_TODOS()
BEGIN
    SELECT id_prestamo, fecha_de_prestamo, fecha_vencimiento, fecha_devolucion, estado, id_ejemplar, id_usuario
    FROM Prestamo;
END$

DELIMITER $$
CREATE DEFINER=`admin`@`%` PROCEDURE `LISTAR_PRESTAMOS_X_USUARIO`(
    IN p_id_usuario INT
)
BEGIN
    SELECT 
        id_prestamo,
        fecha_de_prestamo,
        fecha_vencimiento,
        fecha_devolucion,
        estado,
        id_ejemplar,
        id_usuario
    FROM Prestamo
    WHERE id_usuario = p_id_usuario;
END $$


DELIMITER $
CREATE PROCEDURE INSERTAR_SANCION(
    OUT _id_sancion INT,
    IN _tipo_sancion ENUM ('ENTREGA_TARDIA','DANHO'),
    IN _duracion_dias INT,
    IN _justificacion VARCHAR (255),
    IN _id_prestamo INT
)
BEGIN
	DECLARE _fecha_inicio  datetime;
    DECLARE _fecha_fin DATETIME;
    
    SET _fecha_inicio = NOW();
	SET _fecha_fin = date_add(_fecha_inicio, INTERVAL _duracion_dias DAY);
	INSERT INTO Sancion(tipo_sancion, duracion_dias, fecha_inicio, fecha_fin,
    justificacion, estado, activo, id_prestamo)
    VALUES (_tipo_sancion, _duracion_dias, _fecha_inicio, _fecha_fin, _justificacion,
    'VIGENTE', 1, _id_prestamo);
    SET _id_sancion = @@last_insert_id;
    
END$

DELIMITER $
CREATE PROCEDURE MODIFICAR_SANCION(
    IN _id_sancion INT,
    IN _tipo_sancion ENUM ('ENTREGA_TARDIA','DANHO'),
    IN _duracion_dias INT,
    IN _justificacion VARCHAR (255)
)
BEGIN
	DECLARE _fecha_inicio  datetime;
    DECLARE _fecha_fin  datetime;
    SELECT fecha_inicio
    INTO _fecha_inicio
    FROM Sancion
    WHERE id_sancion =_id_sancion;
    
	SET _fecha_fin = date_add(_fecha_inicio, INTERVAL _duracion_dias DAY);
    
    UPDATE Sancion
    SET 
    tipo_sancion = _tipo_sancion,
    duracion_dias = _duracion_dias,
    justificacion = _justificacion,
	fecha_fin = _fecha_fin
    WHERE id_sancion = _id_sancion;
END$
DELIMITER $
CREATE PROCEDURE FINALIZAR_SANCION(
    IN _id_sancion INT
)
BEGIN
    UPDATE Sancion
    SET 
    estado = 'FINALIZADA'
    WHERE id_sancion = _id_sancion;
END$


DELIMITER $
-- OBTENER_PRESTAMO_X_ID
CREATE PROCEDURE OBTENER_SANCION_X_ID(
    IN _id_sancion INT
)
BEGIN
    SELECT id_sancion, tipo_sancion, duracion_dias, fecha_inicio,
    fecha_fin, justificacion, estado, id_prestamo
    FROM Sancion 
    WHERE id_sancion = _id_sancion;
END$

DELIMITER $
-- OBTENER_PRESTAMO_X_ID
CREATE PROCEDURE LISTAR_SANCIONES_TODAS()
BEGIN
    SELECT id_sancion, tipo_sancion, duracion_dias, fecha_inicio,
    fecha_fin, justificacion, estado, id_prestamo
    FROM Sancion;
END$

DELIMITER $$

CREATE DEFINER=`admin`@`%` PROCEDURE `LISTAR_SANCIONES_X_USUARIO`(IN p_id_usuario INT)
BEGIN
    SELECT 
        s.id_sancion,
        s.tipo_sancion,
        s.duracion_dias,
        s.fecha_inicio,
        s.fecha_fin,
        s.justificacion,
        s.estado,
        s.id_prestamo
    FROM Sancion s
    INNER JOIN Prestamo p ON s.id_prestamo = p.id_prestamo
    WHERE p.id_usuario = p_id_usuario;
END $$



DELIMITER $
CREATE PROCEDURE LISTAR_MATERIALES_TODOS()
BEGIN
    SELECT 
        m.id_material,
        m.titulo,
        m.anho_publicacion,
        m.numero_paginas,
        m.estado,
        m.clasificacion_tematica,
        m.idioma,
        m.tipo,
        
        IFNULL(
            NULLIF(
                GROUP_CONCAT(
                    DISTINCT CONCAT(
                        IFNULL(c.nombre, ''), 
                        CASE 
                            WHEN c.nombre IS NOT NULL AND c.nombre <> '' THEN ' ' 
                            ELSE '' 
                        END,
                        IFNULL(c.primer_apellido, ''), ' ', IFNULL(c.segundo_apellido, '')
                    ) SEPARATOR ', '
                ), ''
            ), 'No hay autores registrados'
        ) AS autores,
        
        IFNULL(
            NULLIF(
                GROUP_CONCAT(
                    DISTINCT CASE 
                        WHEN e.estado = 'DISPONIBLE' THEN b.nombre 
                        ELSE NULL 
                    END SEPARATOR ', '
                ), ''
            ), 'No hay ejemplares disponibles para préstamo'
        ) AS bibliotecas,
        
        COUNT(DISTINCT CASE WHEN e.estado = 'DISPONIBLE' THEN e.id_ejemplar END) AS ejemplares_disponibles
        
    FROM MaterialBibliografico m
    LEFT JOIN Contribuyente_Material cm 
        ON m.id_material = cm.id_material
    LEFT JOIN Contribuyente c 
        ON cm.id_contribuyente = c.id_contribuyente 
        AND c.tipo_contribuyente = 'AUTOR'
    LEFT JOIN Ejemplar e 
        ON m.id_material = e.id_material 
        AND e.activo = 1
    LEFT JOIN Biblioteca b 
        ON e.id_biblioteca = b.id_biblioteca 
        AND b.activo = 1
    WHERE m.activo = 1
    GROUP BY 
        m.id_material, m.titulo, m.anho_publicacion, m.numero_paginas, 
        m.estado, m.clasificacion_tematica, m.idioma, m.tipo
    ORDER BY ejemplares_disponibles DESC, m.titulo ASC;  -- primero por disponibilidad, luego por título
END $
DELIMITER ;

DELIMITER $
CREATE PROCEDURE LISTAR_BIBLIOTECAS_DE_MATERIAL
(IN _id_material INT)
BEGIN
SELECT DISTINCT b.nombre
FROM Biblioteca b, Ejemplar e
WHERE e.id_material = _id_material and b.id_biblioteca = e.id_biblioteca and estado = 'DISPONIBLE';
END$


DELIMITER $
CREATE PROCEDURE LISTAR_EJEMPLARES_DISPONIBLES_POR_MATERIAL
(IN _id_material INT)
BEGIN
SELECT  id_ejemplar, id_material, estado, ubicacion, activo, id_biblioteca
FROM Ejemplar
WHERE id_material = _id_material and activo = 1 and estado = 'DISPONIBLE';
END$


-- MODIFICAR
DELIMITER $
-- idioma VARCHAR(40) NOT NULL,
CREATE PROCEDURE EJEMPLARES_BIBLIOTECA(
    IN _id_material INT
    
)
BEGIN
	select e.id_ejemplar,e.estado,e.ubicacion,b.nombre,b.id_biblioteca
	from Ejemplar e
	inner join Biblioteca b
	on e.id_biblioteca=b.id_biblioteca
	where e.id_material=_id_material;
END$

DELIMITER $$

CREATE PROCEDURE sp_obtener_usuario_por_codigo (
    IN p_codigo_universitario INT
)
BEGIN
    SELECT 
        u.id_usuario,
        u.codigo_universitario,
        u.nombre,
        u.primer_apellido, 
        u.segundo_apellido,
        u.DOI,
        u.correo,
        u.numero_de_telefono,
        r.id_rol,
        r.tipo AS rol,
        r.cantidad_de_dias_por_prestamo
    FROM Usuario u
    INNER JOIN Rol r ON u.id_rol = r.id_rol
    WHERE u.codigo_universitario = p_codigo_universitario
      AND u.activo = TRUE;
END $$

DELIMITER $$
CREATE  PROCEDURE ObtenerContribuyentesPorMaterial(
    IN p_id_material INT
)
BEGIN
    SELECT c.nombre,
           c.primer_apellido,
           c.segundo_apellido,
           c.tipo_contribuyente
    FROM Contribuyente_Material cm
    INNER JOIN Contribuyente c
        ON c.id_contribuyente = cm.id_contribuyente
    WHERE cm.id_material = p_id_material;
END $$

DELIMITER $$

CREATE PROCEDURE ObtenerEditorialesPorId(IN p_id_material INT)
BEGIN
    SELECT e.nombre
    FROM Editorial_Material em
    INNER JOIN Editorial e
        ON e.id_editorial = em.id_editorial
    WHERE em.id_material = p_id_material;
END $$

DELIMITER $$
CREATE PROCEDURE sp_obtener_ejemplares_disponibles(
    IN p_id_material INT,
    IN p_id_biblioteca INT
)
BEGIN
    SELECT *
    FROM Ejemplar
    WHERE id_material = p_id_material
      AND id_biblioteca = p_id_biblioteca
      AND estado = 'DISPONIBLE';
END $$

DELIMITER $$

CREATE PROCEDURE SP_OBTENER_TIPO_MATERIAL (
    IN _id_material INT
)
BEGIN
    SELECT tipo
    FROM MaterialBibliografico
    WHERE id_material = _id_material;
END$$

DELIMITER $


CREATE PROCEDURE LISTAR_MATERIALES_BUSQUEDA(IN _titulo_autor VARCHAR(150))
BEGIN
    SELECT 
        m.id_material,
        m.titulo,
        m.anho_publicacion,
        m.numero_paginas,
        m.estado,
        m.clasificacion_tematica,
        m.idioma,
        m.tipo,
        IFNULL(
            NULLIF(
                GROUP_CONCAT(
                    CONCAT(
                        IFNULL(c.nombre, ''), 
                        CASE WHEN c.nombre IS NOT NULL AND c.nombre <> '' THEN ' ' ELSE '' END,
                        IFNULL(c.primer_apellido, ''), ' ', IFNULL(c.segundo_apellido, '')
                    ) SEPARATOR ', '
                ), ''
            ), 'No hay autores registrados'
        ) AS autores,
        IFNULL(
            NULLIF(
                GROUP_CONCAT(DISTINCT CASE WHEN e.estado = 'DISPONIBLE' THEN b.nombre END SEPARATOR ', '), 
                ''
            ), 'No hay ejemplares disponibles en ninguna biblioteca'
        ) AS bibliotecas,
        COUNT(DISTINCT CASE WHEN e.estado = 'DISPONIBLE' THEN e.id_ejemplar END) AS ejemplares_disponibles
    FROM MaterialBibliografico m
    LEFT JOIN Contribuyente_Material cm ON m.id_material = cm.id_material
    LEFT JOIN Contribuyente c ON cm.id_contribuyente = c.id_contribuyente AND c.tipo_contribuyente = 'AUTOR'
    LEFT JOIN Ejemplar e ON m.id_material = e.id_material AND e.activo = 1
    LEFT JOIN Biblioteca b ON e.id_biblioteca = b.id_biblioteca AND b.activo = 1
    WHERE m.activo = 1
      AND (
          m.titulo LIKE CONCAT('%', _titulo_autor, '%')
          OR c.nombre LIKE CONCAT('%', _titulo_autor, '%')
          OR c.primer_apellido LIKE CONCAT('%', _titulo_autor, '%')
          OR c.segundo_apellido LIKE CONCAT('%', _titulo_autor, '%')
          OR c.seudonimo LIKE CONCAT('%', _titulo_autor, '%')
      )
    GROUP BY 
        m.id_material, m.titulo, m.anho_publicacion, m.numero_paginas, 
        m.estado, m.clasificacion_tematica, m.idioma, m.tipo
    ORDER BY (m.estado = 'DISPONIBLE') DESC, m.titulo ASC;
END $

DELIMITER $$

CREATE PROCEDURE LISTAR_MATERIALES_BUSQUEDA_AVANZADA(
    IN _titulo VARCHAR(150),
    IN _tipo_contribuyente VARCHAR(20),
    IN _nombre_contribuyente VARCHAR(60),
    IN _tema VARCHAR(100),
    IN _fecha_desde INT,
    IN _fecha_hasta INT,
    IN _tipo_material VARCHAR(20),
    IN _nombre_biblioteca VARCHAR(80),
    IN _disponibilidad VARCHAR(20)
)
BEGIN
    SELECT 
        m.id_material,
        m.titulo,
        m.anho_publicacion,
        m.numero_paginas,
        m.estado,
        m.clasificacion_tematica,
        m.idioma,
        m.tipo,
        IFNULL(
            NULLIF(
                GROUP_CONCAT(
                    CONCAT(
                        IFNULL(c.nombre, ''), 
                        CASE WHEN c.nombre IS NOT NULL AND c.nombre <> '' THEN ' ' ELSE '' END,
                        IFNULL(c.primer_apellido, ''), ' ', IFNULL(c.segundo_apellido, '')
                    ) SEPARATOR ', '
                ), ''
            ), 'No hay autores registrados'
        ) AS autores,
        IFNULL(
            NULLIF(
                GROUP_CONCAT(DISTINCT CASE WHEN e.estado = 'DISPONIBLE' THEN b.nombre END SEPARATOR ', '), 
                ''
            ), 'No hay ejemplares disponibles en ninguna biblioteca'
        ) AS bibliotecas,
        COUNT(DISTINCT CASE WHEN e.estado = 'DISPONIBLE' THEN e.id_ejemplar END) AS ejemplares_disponibles
    FROM MaterialBibliografico m
    LEFT JOIN Contribuyente_Material cm ON m.id_material = cm.id_material
    LEFT JOIN Contribuyente c ON cm.id_contribuyente = c.id_contribuyente
    LEFT JOIN Ejemplar e ON e.id_material = m.id_material AND e.activo = 1
    LEFT JOIN Biblioteca b ON e.id_biblioteca = b.id_biblioteca AND b.activo = 1
    WHERE 
        m.activo = 1
        AND (_titulo IS NULL OR m.titulo LIKE CONCAT('%', _titulo, '%'))
        AND (
            _nombre_contribuyente IS NULL
            OR (
                (_tipo_contribuyente IS NULL OR c.tipo_contribuyente = _tipo_contribuyente)
                AND (
                    c.nombre LIKE CONCAT('%', _nombre_contribuyente, '%')
                    OR c.primer_apellido LIKE CONCAT('%', _nombre_contribuyente, '%')
                    OR c.segundo_apellido LIKE CONCAT('%', _nombre_contribuyente, '%')
                    OR c.seudonimo LIKE CONCAT('%', _nombre_contribuyente, '%')
                )
            )
        )
        AND (_tema IS NULL OR m.clasificacion_tematica LIKE CONCAT('%', _tema, '%'))
        AND (_fecha_desde IS NULL OR m.anho_publicacion >= _fecha_desde)
        AND (_fecha_hasta IS NULL OR m.anho_publicacion <= _fecha_hasta)
        AND (_tipo_material IS NULL OR m.tipo = _tipo_material)
        AND (_nombre_biblioteca IS NULL OR b.nombre LIKE CONCAT('%', _nombre_biblioteca, '%'))
        AND (_disponibilidad IS NULL OR m.estado = _disponibilidad)
    GROUP BY 
        m.id_material, m.titulo, m.anho_publicacion, m.numero_paginas, 
        m.estado, m.clasificacion_tematica, m.idioma, m.tipo
    ORDER BY 
        (m.estado = 'DISPONIBLE') DESC,
        m.titulo ASC;
END $$

DELIMITER $$

CREATE PROCEDURE SP_CONTAR_PRESTAMOS_VIGENTES_POR_USUARIO (
    IN _id_usuario INT
)
BEGIN
    SELECT COUNT(*) AS cantidad_prestamos_vigentes
    FROM Prestamo p
    WHERE p.id_usuario = _id_usuario
      AND p.estado = 'VIGENTE';
END $$

DELIMITER ;

--VERIFICAR CUENTA
DELIMITER $
CREATE PROCEDURE VERIFICAR_CUENTA(
	IN _correo VARCHAR(100),
	IN _contrasena VARCHAR(40)
)
BEGIN
	SELECT * FROM Usuario WHERE correo=_correo AND contrasena=MD5(_contrasena);
END$


-- MODIFICAR
DELIMITER $
CREATE PROCEDURE MODIFICAR_PRESTAMO(
    IN _id_prestamo INT,
    IN _fecha_de_prestamo datetime,
    IN _fecha_vencimiento datetime,
    IN _fecha_devolucion datetime,
    IN _estado ENUM('VIGENTE', 'FINALIZADO', 'RETRASADO'),
    IN _id_ejemplar INT,
    IN _id_usuario INT
)
BEGIN
    UPDATE Prestamo
    SET 
        fecha_de_prestamo = _fecha_de_prestamo,
        fecha_vencimiento = _fecha_vencimiento,
        fecha_devolucion = _fecha_devolucion,
        estado = _estado,
        id_ejemplar = _id_ejemplar,
        id_usuario = _id_usuario
    WHERE id_prestamo = _id_prestamo;
END$

