# RetoMegaConfigurador2025
Este es un reto hecho por MEGA para los ingenieros en desarollo 2025. Consta de un configurador de promociones y calculo de la deuda de un suscriptor para vendedores o usuarios que manejen contrataciones y ofrezcan productos o servicios que se les puedan aplicar estas promociones. 


## Configuración de entornos de server.
Crea los archivos _appsettings.Development.json_ y _appsettings.json_ en la carpeta _server_. Agrega el siguiente contenido para cada archivo.

  - **appsettings.Development.json**
    ```json
    {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "ConnectionStrings": {
        "db": "Server=.;Database=BASE_DE_DATOS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;"
      }
    }
    ```
  
  - **appsettings.json**
    ```json
    {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "AllowedHosts": "*",
      "ConnectionStrings": {
        "db": "Server=.;Database=BASE_DE_DATOS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;"
      }
    }
    ```

    Debes especificar el nombre de la base de datos en la cadena de conexión. Esta cadena solo funciona para la autenticación integrada de Windows. Si no puedes utilizar esta característica, deberás hacerlo con la autenticación SQL:

    ```json
    "db": "Server=.;Database=BASE_DE_DATOS;User Id=USUARIO;Password=CONTRASEÑA;TrustServerCertificate=True;"
    ```

    Debes poner un usuario que tenga privilegios sobre la base de datos.
