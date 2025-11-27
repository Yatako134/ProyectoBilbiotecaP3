DROP TABLE IF EXISTS Contribuyente_Material;
DROP TABLE IF EXISTS Contribuyente;
DROP TABLE IF EXISTS Sancion;
DROP TABLE IF EXISTS Prestamo;
DROP TABLE IF EXISTS Usuario;
DROP TABLE IF EXISTS Rol;
DROP TABLE IF EXISTS Ejemplar;
DROP TABLE IF EXISTS Biblioteca;
DROP TABLE IF EXISTS Contribuyente;
DROP TABLE IF EXISTS Tesis;
DROP TABLE IF EXISTS Articulo;
DROP TABLE IF EXISTS Libro;
DROP TABLE IF EXISTS MaterialBibliografico;
DROP TABLE IF EXISTS Idioma;

CREATE TABLE `Rol` (
  `id_rol` int NOT NULL AUTO_INCREMENT,
  `tipo` varchar(30) NOT NULL,
  `cantidad_de_dias_por_prestamo` int NOT NULL,
  `activo` tinyint(1) DEFAULT '1',
  `limite_prestamos` int NOT NULL,
  PRIMARY KEY (`id_rol`),
  UNIQUE KEY `tipo` (`tipo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `Usuario` (
  `id_usuario` int NOT NULL AUTO_INCREMENT,
  `codigo_universitario` int NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `primer_apellido` varchar(50) NOT NULL,
  `segundo_apellido` varchar(50) NOT NULL,
  `DOI` int NOT NULL,
  `contrasena` varchar(40) NOT NULL,
  `correo` varchar(100) NOT NULL,
  `numero_de_telefono` varchar(12) NOT NULL,
  `activo` tinyint(1) DEFAULT '1',
  `id_rol` int NOT NULL,
  PRIMARY KEY (`id_usuario`),
  UNIQUE KEY `codigo_universitario` (`codigo_universitario`),
  UNIQUE KEY `correo` (`correo`),
  KEY `id_rol` (`id_rol`),
  CONSTRAINT `Usuario_ibfk_1` FOREIGN KEY (`id_rol`) REFERENCES `Rol` (`id_rol`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `MaterialBibliografico` (
  `id_material` int NOT NULL AUTO_INCREMENT,
  `titulo` varchar(150) NOT NULL,
  `anho_publicacion` int NOT NULL,
  `numero_paginas` int NOT NULL,
  `estado` enum('DISPONIBLE','NO_DISPONIBLE') NOT NULL,
  `clasificacion_tematica` varchar(100) NOT NULL,
  `activo` tinyint(1) DEFAULT '1',
  `idioma` varchar(40) NOT NULL,
  `tipo` enum('LIBRO','TESIS','ARTICULO') NOT NULL,
  `editoriales` varchar(300) DEFAULT NULL,
  PRIMARY KEY (`id_material`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;



CREATE TABLE `Articulo` (
  `id_articulo` int NOT NULL,
  `ISSN` varchar(30) NOT NULL,
  `revista` varchar(100) NOT NULL,
  `volumen` int NOT NULL,
  `numero` int NOT NULL,
  PRIMARY KEY (`id_articulo`),
  UNIQUE KEY `ISSN` (`ISSN`),
  CONSTRAINT `Articulo_ibfk_1` FOREIGN KEY (`id_articulo`) REFERENCES `MaterialBibliografico` (`id_material`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `Libro` (
  `id_libro` int NOT NULL,
  `ISBN` varchar(30) NOT NULL,
  `edicion` varchar(20) NOT NULL,
  PRIMARY KEY (`id_libro`),
  UNIQUE KEY `ISBN` (`ISBN`),
  CONSTRAINT `Libro_ibfk_1` FOREIGN KEY (`id_libro`) REFERENCES `MaterialBibliografico` (`id_material`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `Tesis` (
  `id_tesis` int NOT NULL,
  `especialidad` varchar(100) NOT NULL,
  `asesor` varchar(60) NOT NULL,
  `grado` varchar(50) NOT NULL,
  `institucion_publicacion` varchar(150) NOT NULL,
  PRIMARY KEY (`id_tesis`),
  CONSTRAINT `Tesis_ibfk_1` FOREIGN KEY (`id_tesis`) REFERENCES `MaterialBibliografico` (`id_material`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `Contribuyente` (
  `id_contribuyente` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(60) NOT NULL,
  `primer_apellido` varchar(60) NOT NULL,
  `segundo_apellido` varchar(60) NOT NULL,
  `seudonimo` varchar(60) DEFAULT NULL,
  `tipo_contribuyente` enum('AUTOR','EDITOR','TRADUCTOR') NOT NULL,
  PRIMARY KEY (`id_contribuyente`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `Contribuyente_Material` (
  `id_material` int NOT NULL,
  `id_contribuyente` int NOT NULL,
  PRIMARY KEY (`id_material`,`id_contribuyente`),
  KEY `id_contribuyente` (`id_contribuyente`),
  CONSTRAINT `Contribuyente_Material_ibfk_1` FOREIGN KEY (`id_material`) REFERENCES `MaterialBibliografico` (`id_material`),
  CONSTRAINT `Contribuyente_Material_ibfk_2` FOREIGN KEY (`id_contribuyente`) REFERENCES `Contribuyente` (`id_contribuyente`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `Biblioteca` (
  `id_biblioteca` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(80) DEFAULT NULL,
  `ubicacion` varchar(60) DEFAULT NULL,
  `activo` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id_biblioteca`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `Ejemplar` (
  `id_ejemplar` int NOT NULL AUTO_INCREMENT,
  `id_material` int NOT NULL,
  `estado` enum('DISPONIBLE','PRESTADO','EN_REPARACION','PERDIDO') NOT NULL,
  `ubicacion` varchar(50) NOT NULL,
  `activo` tinyint(1) DEFAULT '1',
  `id_biblioteca` int NOT NULL,
  PRIMARY KEY (`id_ejemplar`),
  KEY `id_material` (`id_material`),
  KEY `id_biblioteca` (`id_biblioteca`),
  CONSTRAINT `Ejemplar_ibfk_1` FOREIGN KEY (`id_material`) REFERENCES `MaterialBibliografico` (`id_material`),
  CONSTRAINT `Ejemplar_ibfk_2` FOREIGN KEY (`id_biblioteca`) REFERENCES `Biblioteca` (`id_biblioteca`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `Prestamo` (
  `id_prestamo` int NOT NULL AUTO_INCREMENT,
  `fecha_de_prestamo` datetime NOT NULL,
  `fecha_vencimiento` datetime NOT NULL,
  `fecha_devolucion` datetime DEFAULT NULL,
  `estado` enum('VIGENTE','FINALIZADO','RETRASADO') DEFAULT NULL,
  `id_ejemplar` int NOT NULL,
  `id_usuario` int NOT NULL,
  `recordatorio` tinyint DEFAULT '0',
  PRIMARY KEY (`id_prestamo`),
  KEY `id_usuario` (`id_usuario`),
  KEY `id_ejemplar` (`id_ejemplar`),
  CONSTRAINT `Prestamo_ibfk_1` FOREIGN KEY (`id_usuario`) REFERENCES `Usuario` (`id_usuario`),
  CONSTRAINT `Prestamo_ibfk_2` FOREIGN KEY (`id_ejemplar`) REFERENCES `Ejemplar` (`id_ejemplar`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `Sancion` (
  `id_sancion` int NOT NULL AUTO_INCREMENT,
  `tipo_sancion` enum('ENTREGA_TARDIA','DANHO') NOT NULL,
  `duracion_dias` int NOT NULL,
  `fecha_inicio` datetime NOT NULL,
  `fecha_fin` datetime NOT NULL,
  `justificacion` varchar(255) NOT NULL,
  `estado` enum('VIGENTE','FINALIZADA','PAUSADO') DEFAULT NULL,
  `id_prestamo` int DEFAULT NULL,
  PRIMARY KEY (`id_sancion`),
  UNIQUE KEY `id_prestamo` (`id_prestamo`),
  CONSTRAINT `Sancion_ibfk_1` FOREIGN KEY (`id_prestamo`) REFERENCES `Prestamo` (`id_prestamo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
