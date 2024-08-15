DELIMITER //

-- Stored Procedure para insertar en la tabla Usuario
CREATE PROCEDURE InsertUsuario(
    IN p_Nombre VARCHAR(50),
    IN p_Apellido VARCHAR(50),
    IN p_NombreUsuario VARCHAR(255),
    IN p_Contrasena VARCHAR(255),
    IN p_Email VARCHAR(100),
    IN p_FotoPerfil VARCHAR(255),
    IN p_IdRol INT UNSIGNED,
    OUT p_IdUsuario INT UNSIGNED
)
BEGIN
    INSERT INTO Usuario (Nombre, Apellido, NombreUsuario, Contrasena, Email, FotoPerfil, IdRol)
    VALUES (p_Nombre, p_Apellido, p_NombreUsuario, p_Contrasena, p_Email, p_FotoPerfil, p_IdRol);
    SET p_IdUsuario = LAST_INSERT_ID();
END //

-- Stored Procedure para insertar en la tabla Post
CREATE PROCEDURE InsertPost(
    IN p_UrlImagen VARCHAR(255),
    IN p_IdUsuario INT UNSIGNED,
    IN p_CantidadLikes INT UNSIGNED,
    IN p_FechaPublicacion DATE,
    OUT p_IdPost INT UNSIGNED
)
BEGIN
    INSERT INTO Post (UrlImagen, IdUsuario, CantidadLikes, FechaPublicacion)
    VALUES (p_UrlImagen, p_IdUsuario, p_CantidadLikes, p_FechaPublicacion);
    SET p_IdPost = LAST_INSERT_ID();
END //

-- Stored Procedure para insertar en la tabla UsuarioLikes
CREATE PROCEDURE InsertUsuarioLikes(
    IN p_IdUsuario INT UNSIGNED,
    IN p_IdPost INT UNSIGNED,
    IN p_FechaLike DATE
)
BEGIN
    INSERT INTO UsuarioLikes (IdUsuario, IdPost, FechaLike)
    VALUES (p_IdUsuario, p_IdPost, p_FechaLike);
END //

-- Stored Procedure para insertar en la tabla Seguidores
CREATE PROCEDURE InsertSeguidores(
    IN p_IdUsuario INT UNSIGNED,
    IN p_IdUsuarioSeguido INT UNSIGNED,
    IN p_FechaSeguimiento DATE
)
BEGIN
    INSERT INTO Seguidores (IdUsuario, IdUsuarioSeguido, FechaSeguimiento)
    VALUES (p_IdUsuario, p_IdUsuarioSeguido, p_FechaSeguimiento);
END //

-- Stored Procedure para insertar en la tabla RolUsuario
CREATE PROCEDURE InsertRolUsuario(
    IN p_Descripcion VARCHAR(50),
    OUT p_IdRol INT UNSIGNED
)
BEGIN
    INSERT INTO RolUsuario (Descripcion)
    VALUES (p_Descripcion);
    SET p_IdRol = LAST_INSERT_ID();
END //

DELIMITER ;
