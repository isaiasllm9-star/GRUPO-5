# Diagrama Relacional de la Base de Datos

Este documento detalla la estructura de la base de datos SQLite utilizada por la aplicación **Password Manager**.

## Diagrama Entidad-Relación (Mermaid)

```mermaid
erDiagram
    CATEGORY ||--o{ CREDENTIAL : "contiene"
    
    CATEGORY {
        int Id PK
        string Name "Nombre de la categoría"
    }

    CREDENTIAL {
        int Id PK
        string SiteName "Nombre del sitio"
        string Username "Nombre de usuario"
        string EncryptedPassword "Contraseña cifrada"
        string Notes "Notas adicionales"
        int CategoryId FK "Referencia a Category"
        datetime CreatedAt "Fecha de creación"
    }
```

## Detalles de las Tablas

### Tabla: Categories
Almacena las agrupaciones de credenciales (ej. Redes Sociales, Trabajo, Bancos).
- **Id**: Identificador único autoincremental.
- **Name**: Etiqueta descriptiva de la categoría.

### Tabla: Credentials
Almacena la información sensible de las cuentas.
- **Id**: Identificador único autoincremental.
- **SiteName**: Nombre de la aplicación o sitio web.
- **Username**: Usuario o correo electrónico.
- **EncryptedPassword**: Contraseña almacenada de forma segura (cifrada).
- **Notes**: Descripción o información extra opcional.
- **CategoryId**: Clave foránea que vincula la credencial a una categoría.
- **CreatedAt**: Registro temporal del momento en que se creó la entrada.
