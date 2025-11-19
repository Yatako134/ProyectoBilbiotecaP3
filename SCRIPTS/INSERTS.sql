-- DATOS DE ROLES


INSERT INTO Rol (tipo, cantidad_de_dias_por_prestamo, activo,limite_prestamos)
VALUES ('Estudiante', 7, TRUE,7);

INSERT INTO Rol (tipo, cantidad_de_dias_por_prestamo, activo,limite_prestamos)
VALUES ('Docente', 15, TRUE,15);

INSERT INTO Rol (tipo, cantidad_de_dias_por_prestamo, activo,limite_prestamos)
VALUES ('Bibliotecario', 0, TRUE,0);

-- DATOS DE USUARIOS

INSERT INTO Usuario 
(codigo_universitario, nombre, primer_apellido, segundo_apellido, DOI, contrasena, correo, numero_de_telefono, id_rol)
VALUES
(20230001, 'Luis', 'Gomez', 'Ramos', 12345678, MD5('123'), 'prueba@pucp.edu.pe', '987654321', 3);

INSERT INTO Usuario 
(codigo_universitario, nombre, primer_apellido, segundo_apellido, DOI, contrasena, correo, numero_de_telefono, id_rol)
VALUES
(20230002, 'Maria', 'Torres', 'Vargas', 87654321, MD5('clave2024'), 'maria.torres@pucp.edu.pe', '987111222', 1);

INSERT INTO Usuario 
(codigo_universitario, nombre, primer_apellido, segundo_apellido, DOI, contrasena, correo, numero_de_telefono, id_rol)
VALUES
(20230003, 'Jorge', 'Soto', 'Mendez', 11223344, MD5('password'), 'jorge.soto@pucp.edu.pe', '945777888', 1);

INSERT INTO Usuario 
(codigo_universitario, nombre, primer_apellido, segundo_apellido, DOI, contrasena, correo, numero_de_telefono, id_rol)
VALUES
(20230004, 'Carmen', 'Ruiz', 'Flores', 55667788, MD5('profe2024'), 'carmen.ruiz@pucp.edu.pe', '912333444', 2);

INSERT INTO Usuario 
(codigo_universitario, nombre, primer_apellido, segundo_apellido, DOI, contrasena, correo, numero_de_telefono, id_rol)
VALUES
(20230005, 'Eduardo', 'Perez', 'Linares', 99887766, MD5('teach123'), 'eduardo.perez@pucp.edu.pe', '987000111', 2);


-- DATOS DE BIBLIOTECA 

INSERT INTO Biblioteca (nombre, ubicacion, activo) 
VALUES 
('Biblioteca Central', 'Av. Principal 123, Lima', TRUE),
('Biblioteca de la Facultad de Derecho', 'Calle Universitaria 456, Lima', TRUE),
('Biblioteca Regional Arequipa', 'Jr. Independencia 789, Arequipa', FALSE),
('Biblioteca Virtual', 'Online, Lima', TRUE);

-- DATOS DE CONTRIBUYENTE