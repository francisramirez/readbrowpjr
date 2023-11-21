El proyecto tiene desarrollado el backend basado en .NET 7, Entity Framework 7 y Azure SQL. Para ejecutar los endPoints se recomienda utilziar PostMan

Este contiente dos controllers el AuthController, para la creación del token de un determinado usuario a partir de su correo eléctroncio.

Parametro de entrada

{ "correo": "francisricardor@gmail.com" }

https://readcrowsapi.azurewebsites.net/api/Auth/CreateToken
Y el servicio de UsuarioController para la administración de los usuarios este contiene los siguientes endPoints:

https://readcrowsapi.azurewebsites.net/api/Usuario/GetUsuarios, traer la lista de los usuarios creados.

https://readcrowsapi.azurewebsites.net/api/Usuario/CreateUser, endPoint para crear los usuarios

Este recibe los siguientes parámetros:

{ "nombre": "string", "correo": "user@example.com", "edad": 0 }

https://readcrowsapi.azurewebsites.net/api/Usuario/UpdateUser, endPoint para actualizar la información de los usuarios.
Este recibe los siguientes parámetros:

{ "nombre": "string", "correo": "user@example.com", "edad": 0, "usuarioId": 0 }