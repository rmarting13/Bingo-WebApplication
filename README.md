# Bingo-WebApplication
Esta aplicación web simula un juego de Bingo con 4 cartones generados al azar.
### Consideraciones:
>
> - El proyecto se desarrolló con el framework **ASP .Net Core Web App**, por lo que se recomienda abrir el proyecto con Microsoft Visual Studio. Si desea utilizar Visual Studio Code deberá instalar las dependencias correspondientes para su correcto funcionamiento.
> - El proyecto utiliza *Entity Framework Core*, por lo que se recomienda instalar *Microsoft SQL Server* y *SSMS* para la gestión y conexión a la base de datos.
> - Modificar el string de conexión en Data/Context/DataBaseContext.cs, en la línea 13 colocar en "Server='database-name'..." el nombre de su base de datos.
> - A continuación abra la **Nuget Package Manager Console (Consola de Administración de Paquetes Nuget)** en ***Herramientas>>Administrador de paquetes NuGet>>Consola del Administrador de paquetes***, y ejecutar los comandos '***add-migration***' y '***update-database***' de *Entity Framework Core* para efectuar las migraciones correspondientes al enfoque **Code-First**.
#### Link de descarga de MS-Visual Studio: https://visualstudio.microsoft.com/es/thank-you-downloading-visual-studio/?sku=Community&channel=Release&version=VS2022&source=VSLandingPage&passive=false&cid=2030
#### Link de descarga de MS-SQL-S Express: https://go.microsoft.com/fwlink/p/?linkid=2216019&clcid=0x409&culture=en-us&country=us

Para una prueba de *ejecución más rápida* se puede modificar el valor de la condición del método '***esGanador()***' en la clase ***Models/CartonViewModel.cs***, la condición es '***CantidadDeAciertos >= 15***', por lo que la ejecución del bingo se finaliza cuando algún cartón alcance la cifra de 15 aciertos. Modificando dicho valor se podría finalizar el juego con aciertos más pequeños, obteniendo resultados de manera más rápida para la prueba.
