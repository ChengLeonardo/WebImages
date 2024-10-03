# WebImages

## ET12 - Proyecto WebImages

### Requisitos previos

Antes de comenzar, asegúrate de tener instalados los siguientes programas:

1. **.NET Core**  
   Si no lo tienes instalado, descárgalo desde la página oficial:  
   [Descargar .NET](https://dotnet.microsoft.com/en-us/download)

2. **MySQL**  
   Si no tienes MySQL instalado, descárgalo desde la página oficial:  
   [Descargar MySQL](https://dev.mysql.com/downloads/installer/)

3. **Git**  
   Si no tienes Git instalado, descárgalo desde la página oficial:  
   [Descargar Git](https://git-scm.com/)

---

### Instalación del proyecto

Sigue estos pasos para configurar el proyecto localmente:

1. **Clonar el repositorio**  
   Abre la terminal en el directorio donde deseas clonar el proyecto y ejecuta:
   ```bash
   git clone https://github.com/ChengLeonardo/WebImages.git
   ```

2. **Cambiar a la rama `BackEnd`**  
   Dentro del repositorio clonado, cambia a la rama correspondiente con el siguiente comando:
   ```bash
   git switch BackEnd
   ```

3. **Configurar la base de datos**  
   Navega al directorio donde se encuentra el script de la base de datos y conéctate a MySQL:
   ```bash
   cd Backend
   cd BaseDatos
   mysql -u "tu-usuario-mysql" -p
   ```

   A continuación, se te pedirá que ingreses la contraseña de tu usuario MySQL:
   ```text
   Enter Password: ****
   ```

4. **Importar la base de datos**  
   Una vez conectado a MySQL, ejecuta el siguiente comando para importar la estructura de la base de datos:
   ```bash
   source install.sql
   ```
   Cuando el proceso termine, cierra la sesión de MySQL con:
   ```bash
   exit
   ```

5. **Configurar la cadena de conexión**  
   Vuelve al directorio `BackEnd` y edita el archivo `appsettings.json` para configurar la conexión a tu base de datos. Encuentra la sección `ConnectionStrings` y modifica la línea con tus credenciales de MySQL:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "server=localhost;database=5to_MiPaginaDeImagenes;user=tu-usuario;password=tu-contraseña;"
   }
   ```

6. **Ejecutar el proyecto**  
   Finalmente, desde el directorio principal del backend, ejecuta el siguiente comando para iniciar la aplicación:
   ```bash
   dotnet run
   ```

---

### Equipo de desarrollo

- **Cheng Leonardo**
- **Verduguez Miguel**
- **Espínola Luis**
- **Mendoza Davis**

---
