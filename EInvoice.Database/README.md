# EInvoice.Database

Scripts de creación y mantenimiento de la base de datos para el sistema de facturación electrónica.

## Estructura

- `/schema` → creación del esquema `einvoice`
- `/tables` → todas las tablas ordenadas por numeración
- `/seeds` → inserción inicial de catálogos

## Ejecución

Ejecutar en este orden:

1. schema/001_create_schema_einvoice.sql  
2. tables/*.sql  
3. seeds/*.sql

## Convención de numeración

- 001–009 → esquema
- 010–099 → tablas
- 100–199 → vistas
- 200–299 → stored procedures
- 300–399 → seeds
