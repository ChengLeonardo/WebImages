drop DATABASE if EXISTS MiPaginaDeImagenes;
-- Crear la base de datos
CREATE DATABASE IF NOT EXISTS MiPaginaDeImagenes;
USE MiPaginaDeImagenes;

-- Tabla RolUsuario
CREATE TABLE RolUsuario (
    IdRol INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    Descripcion VARCHAR(50) NOT NULL
);

-- Tabla Usuario
CREATE TABLE Usuario (
    IdUsuario INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    NombreUsuario VARCHAR(255) UNIQUE NOT NULL,
    Contrasena VARCHAR(255) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    FotoPerfil VARCHAR(255), -- URL de la imagen de perfil
    IdRol INT UNSIGNED,
    FOREIGN KEY (IdRol) REFERENCES RolUsuario(IdRol)
);

-- Tabla Post
CREATE TABLE Post (
    IdPost INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    UrlImagen VARCHAR(255) NOT NULL, -- URL de la imagen
    IdUsuario INT UNSIGNED,
    CantidadLikes INT UNSIGNED DEFAULT 0,
    FechaPublicacion DATE NOT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);

-- Tabla UsuarioLikes
CREATE TABLE UsuarioLikes (
    IdUsuario INT UNSIGNED,
    IdPost INT UNSIGNED,
    PRIMARY KEY (IdUsuario, IdPost),
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
    FOREIGN KEY (IdPost) REFERENCES Post(IdPost)
);

-- Tabla Seguidores
CREATE TABLE Seguidor (
    IdUsuario INT UNSIGNED,
    IdUsuarioSeguido INT UNSIGNED,
    FechaSeguimiento DATE NOT NULL,
    PRIMARY KEY (IdUsuario, IdUsuarioSeguido),
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
    FOREIGN KEY (IdUsuarioSeguido) REFERENCES Usuario(IdUsuario)
);
