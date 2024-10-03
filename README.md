# WebImages
et12

Pasos para instalar el proyecto:

Instale .net por la pagina oficial si no lo posee:
https://dotnet.microsoft.com/en-us/download

Instale mysql por la pagina oficial si no lo posee:
https://dev.mysql.com/downloads/installer/

Instale git por la pagina oficial si no lo posee:
https://git-scm.com/

En la terminal donde desee clonar el repositorio ejecute el siguiente comando:
git clone https://github.com/ChengLeonardo/WebImages.git

En la terminal del directorio principal ejectute este comando 
git switch BackEnd

En su posicion de directorio, ejecute este comando:
cd Backend
cd BaseDatos
mysql -u "tu-nombre-usuario-de-la-base-de-datos" -p

Deberia aparecer la siguiente linea:
Enter Password:

Aqui ingrese su contraseña del usuario de la base de datos

Luego ejecte:
source install.sql

Al terminar el proceso, ejecute:
exit

Luego ingrese:
cd ..
cd ..
cd BackEnd 

Ahora modifique la siguiente linea en el archivo de appsettings.json, completando los datos correspondientes:
"server=localhost;database=5to_MiPaginaDeImagenes;user=tu-usuario;password=tu-contraseña;"

Y por ultimo realize este comando para poder ejecutar la pagina y ver sus funcionalidades
dotnet run

Participantes del equipo:
Cheng Leonardo
Verduguez Miguel
Espinola Luis
Mendoza Davis
