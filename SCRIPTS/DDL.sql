DROP TABLE IF EXISTS Contribuyente_Material;
DROP TABLE IF EXISTS Contribuyente;
DROP TABLE IF EXISTS Sancion;
DROP TABLE IF EXISTS Prestamo;
DROP TABLE IF EXISTS Usuario;
DROP TABLE IF EXISTS Rol;
DROP TABLE IF EXISTS Editorial_Material;
DROP TABLE IF EXISTS Editorial;
DROP TABLE IF EXISTS Ejemplar;
DROP TABLE IF EXISTS Biblioteca;
DROP TABLE IF EXISTS Contribuyente;
DROP TABLE IF EXISTS Tesis;
DROP TABLE IF EXISTS Articulo;
DROP TABLE IF EXISTS Libro;
DROP TABLE IF EXISTS MaterialBibliografico;
DROP TABLE IF EXISTS Idioma;

CREATE TABLE Rol(
	id_rol INT AUTO_INCREMENT PRIMARY KEY,
	tipo VARCHAR(30) NOT NULL UNIQUE,
    cantidad_de_dias_por_prestamo INT NOT NULL,
    activo BOOLEAN DEFAULT TRUE,
	limite_prestamos INT NOT NULL
)ENGINE=InnoDB;

CREATE TABLE Usuario (
    id_usuario INT AUTO_INCREMENT PRIMARY KEY,
    codigo_universitario INT UNIQUE NOT NULL,
    nombre VARCHAR(50) NOT NULL,
    primer_apellido VARCHAR(50) NOT NULL,
    segundo_apellido VARCHAR(50) NOT NULL,
    DOI INT NOT NULL,
    contrasena VARCHAR(40) NOT NULL,
    correo VARCHAR(100) UNIQUE NOT NULL,
    numero_de_telefono VARCHAR(12) NOT NULL,
    activo BOOLEAN DEFAULT TRUE,
    id_rol INT NOT NULL,
    FOREIGN KEY(id_rol) REFERENCES Rol(id_rol)
)ENGINE=InnoDB;


CREATE TABLE MaterialBibliografico (
    id_material INT AUTO_INCREMENT PRIMARY KEY,
    titulo VARCHAR(150) NOT NULL,
    anho_publicacion INT NOT NULL,
    numero_paginas INT NOT NULL,
    estado ENUM('DISPONIBLE','NO_DISPONIBLE') NOT NULL,
    clasificacion_tematica VARCHAR(100) NOT NULL,
    activo BOOLEAN DEFAULT TRUE,
    idioma VARCHAR(40) NOT NULL,
    tipo ENUM('LIBRO','TESIS','ARTICULO') NOT NULL,
    editoriales VARCHAR(300)
) ENGINE=InnoDB;

CREATE TABLE Libro (
    id_libro INT PRIMARY KEY,
    FOREIGN KEY(id_libro) REFERENCES MaterialBibliografico(id_material),
    ISBN VARCHAR(30) UNIQUE NOT NULL,  
    edicion VARCHAR(20) NOT NULL
) ENGINE=InnoDB;

CREATE TABLE Articulo (
    id_articulo INT,
    PRIMARY KEY(id_articulo),
    FOREIGN KEY(id_articulo) REFERENCES MaterialBibliografico(id_material),
    ISSN VARCHAR(30) UNIQUE NOT NULL,
    revista VARCHAR(100) NOT NULL,
    volumen INT NOT NULL,
    numero INT NOT NULL
)ENGINE=InnoDB;

CREATE TABLE Tesis (
    id_tesis INT,
    PRIMARY KEY(id_tesis),
    FOREIGN KEY(id_tesis) REFERENCES MaterialBibliografico(id_material),
    especialidad VARCHAR(100) NOT NULL,
    asesor VARCHAR(60) NOT NULL,
    grado VARCHAR(50) NOT NULL,
    institucion_publicacion VARCHAR(150) NOT NULL
)ENGINE=InnoDB;



CREATE TABLE Contribuyente(
	id_contribuyente INT AUTO_INCREMENT PRIMARY KEY,
	nombre VARCHAR(60) NOT NULL,
    primer_apellido VARCHAR(60) NOT NULL,
    segundo_apellido VARCHAR(60) NOT NULL,
    seudonimo VARCHAR(60),
    tipo_contribuyente ENUM('AUTOR','EDITOR','TRADUCTOR') NOT NULL
)ENGINE=InnoDB;

CREATE TABLE Contribuyente_Material(
	id_material INT,
    id_contribuyente INT,
	PRIMARY KEY (id_material, id_contribuyente),
    FOREIGN KEY (id_material) REFERENCES MaterialBibliografico(id_material),
    FOREIGN KEY (id_contribuyente) REFERENCES Contribuyente(id_contribuyente)

)ENGINE=InnoDB;

CREATE TABLE Editorial(
	id_editorial INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL  
)ENGINE = InnoDB;

CREATE TABLE Editorial_Material (
    id_editorial INT,
    id_material INT,
    PRIMARY KEY (id_editorial, id_material),
    FOREIGN KEY (id_editorial) REFERENCES Editorial(id_editorial),
    FOREIGN KEY (id_material) REFERENCES MaterialBibliografico(id_material)
) ENGINE = InnoDB;


CREATE TABLE Biblioteca(
	id_biblioteca INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(80),
    ubicacion VARCHAR(60),
    activo BOOLEAN DEFAULT TRUE
)ENGINE= InnoDB;




CREATE TABLE Ejemplar (
    id_ejemplar INT AUTO_INCREMENT PRIMARY KEY,
    id_material INT NOT NULL,
    estado ENUM('DISPONIBLE','PRESTADO','EN_REPARACION','PERDIDO') NOT NULL,
    ubicacion VARCHAR(50) NOT NULL,
    activo BOOLEAN DEFAULT TRUE,
    id_biblioteca INT NOT NULL,
    FOREIGN KEY (id_material) REFERENCES MaterialBibliografico(id_material),
    FOREIGN KEY(id_biblioteca) REFERENCES Biblioteca(id_biblioteca)
)ENGINE=InnoDB;





CREATE TABLE Prestamo (
  id_prestamo INT AUTO_INCREMENT PRIMARY KEY,
  fecha_de_prestamo DATETIME NOT NULL,
  fecha_vencimiento DATETIME NOT NULL,
  fecha_devolucion DATETIME NULL,
  estado ENUM('VIGENTE','FINALIZADO','RETRASADO'),
  id_ejemplar INT NOT NULL,
  id_usuario INT NOT NULL,
  FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario),
  FOREIGN KEY (id_ejemplar) REFERENCES Ejemplar(id_ejemplar)
) ENGINE=InnoDB;

CREATE TABLE Sancion (
  id_sancion INT AUTO_INCREMENT PRIMARY KEY,               
  tipo_sancion ENUM('ENTREGA_TARDIA','DANHO') NOT NULL,
  duracion_dias INT NOT NULL,
  fecha_inicio DATETIME NOT NULL,
  fecha_fin DATETIME NOT NULL,
  justificacion VARCHAR(255) NOT NULL,
  estado ENUM('VIGENTE','FINALIZADA','PAUSADO'),
  id_prestamo INT UNIQUE,                        
  FOREIGN KEY (id_prestamo) REFERENCES Prestamo(id_prestamo)
) ENGINE=InnoDB;
