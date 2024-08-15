USE MiPaginaDeImagenes;

CALL InsertRolUsuario("Usuario Comun", @RolComun);
CALL InsertRolUsuario("Usuario Administrador", @RolAdmin);

CALL InsertUsuario("Administrador", "Administrador", "Administrador", "Administrador", "Administrador@gmail.com", NULL, @RolAdmin, @UsuarioAdmin);
