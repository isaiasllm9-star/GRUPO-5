# PassGuard - Gestor de Contraseñas (Proyecto Final)

## Descripción del Proyecto
PassGuard es una aplicación de consola desarrollada en .NET C# para la gestión segura de credenciales y contraseñas. Implementa un sistema completo que permite resguardar sitios web, encriptar contraseñas, y organizarlas bajo categorías específicas. Este proyecto es el resultado final de la asignatura, demostrando la integración de:

*   **Programación Orientada a Objetos**
*   **Diseño de bases de datos relacionales**
*   **Uso avanzado de ORM (Entity Framework Core)**
*   **Arquitectura de Software por Capas**

El sistema aísla estrictamente la lógica de la interfaz y acceso a los datos utilizando Repositorios.

---

## Tecnologías Utilizadas

*   **Lenguaje:** C# (.NET 10/9/8)
*   **Base de Datos Relacional:** SQLite (`Microsoft.EntityFrameworkCore.Sqlite`)
*   **ORM:** Entity Framework Core (Enfoque Code-First)
*   **Interfaz de Usuario:** Spectre.Console (Renderizado de tablas, paneles interactivos y menús atractivos)

---

## Arquitectura por Capas

El proyecto está diseñado bajo una arquitectura limpia y separada:

*   **`/Database`**: Contiene la clase `AppDbContext` encargada de configurar la conexión al proveedor SQLite, y definir los DbSets de EF Core.
*   **`/Models`**: Contiene las entidades del dominio mapeadas a tablas (`Credential` y `Category`). Contiene configuraciones con DataAnnotations como `[Key]`, `[Required]` o `[ForeignKey]`.
*   **`/Repositories`**: Implementa las clases abstractas para realizar las funciones CRUD de acceso a datos (`CredentialRepository`, `CategoryRepository`). Todo el uso de LINQ se maneja aquí de manera unificada.
*   **`/Services`**: Mantiene lógica de negocio independiente de la persistencia de datos (por ejemplo, cifrado en `EncryptionService.cs`).
*   **`/Screens`**: Gestiona estrictamente las interacciones visuales haciendo uso exclusivo de Spectre.Console. Ninguna lógica de SQL o DbContext directo está en esta capa.

---

## Estructura de la Base de Datos

Cuenta con dos principales entidades que mantienen una relación *(Uno a Muchos / One-to-Many)*.
*   **`Categories`**: Trabajos, Bancos, Redes Sociales. (ID como PK).
*   **`Credentials`**: Sitios agregados por el usuario. Posee llave foránea asociada al CategoryId.

---

## Instrucciones para Ejecutar

Siga estos pasos para compilar e iniciar la aplicación:

1.  Abra su terminal preferida (Powershell/CMD/Bash).
2.  Navegue al directorio raíz del proyecto:
    ```bash
    cd "RUTA_AL_PROYECTO/PasswordManagerApp"
    ```
3.  Ejecute la aplicación utilizando el SDK de .NET:
    ```bash
    dotnet run
    ```
4.  *Nota:* La base de datos SQLite (`passwords.db`) será generada de forma completamente automática tras la primera corrida del sistema a través de las migraciones/código interno (función EnsureCreated).

---

Elaborado aplicando buenas prácticas y separación de responsabilidades para evaluación técnica.
