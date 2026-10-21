# Sistema de Gestión Veterinaria - VetCare Enterprise (v2.0)

**Última actualización:** 21 de octubre de 2026  
**Curso:** Programación II  
**Tecnologías:** C# (.NET 8.0-windows) | Windows Forms | SQLite (`Microsoft.Data.Sqlite`) | Git & GitHub

---

## 📌 Descripción del Proyecto

**VetCare Enterprise** es un sistema de control clínico veterinario desarrollado bajo una **arquitectura por capas (Layered Architecture)**. Esta versión (v2) evoluciona el sistema desde prototipos iniciales a una **aplicación de escritorio profesional con interfaz gráfica (Windows Forms)** y **persistencia relacional completa en base de datos SQLite**.

El sistema administra el flujo completo de una clínica veterinaria: registro de clientes (propietarios), expediente de pacientes (mascotas), control de esquema de vacunación, hospitalizaciones y un módulo automatizado de atención y facturación.

---

## 🚀 Novedades y Requerimientos de la Versión 2.0

1. **Motor de Base de Datos SQLite:** Integración del controlador `Microsoft.Data.Sqlite` para la creación y gestión persistente de la base de datos `VetCare.db`.
2. **Esquema Relacional de Tablas:** Definición automatizada de tablas relacionales (`Propietarios`, `Mascotas`, `Atenciones`, `Hospitalizaciones` y `Facturas`) con restricciones de integridad y llaves foráneas (`FOREIGN KEY`).
3. **Capa DAO (Data Access Object):** Implementación de clases dedicadas para las operaciones **CRUD** (Crear, Leer, Actualizar, Eliminar) separando la lógica de datos de la interfaz visual.
4. **Interfaz Gráfica con Windows Forms:**
   - **`FormPrincipal`:** Menú central interactivo para la navegación entre módulos.
   - **`FormPropietarios`:** Gestión de clientes con validación de DPI/NIT único.
   - **`FormMascotas`:** Expediente de pacientes vinculados a su dueño y estado del esquema de vacunas.
   - **`FormFacturacion`:** Módulo de atención médica, estancia hospitalaria ($Q45.00/\text{día}$), recargos por vacunas pendientes ($Q75.00$) e historial de comprobantes emitidos.

---

## 📁 Estructura del Proyecto

El proyecto está organizado en una arquitectura limpia y modular para facilitar su mantenimiento y escalabilidad:

```text
Sistema-Clinico-Veterinario/
│
├── VS_Code/                             # Proyecto Principal en C# (.NET 8.0 WinForms)
│   ├── DAO/                             # Capa de Acceso a Datos (Lógica CRUD)
│   │   ├── PropietarioDAO.cs            # CRUD de Clientes / Propietarios
│   │   ├── MascotaDAO.cs                # CRUD de Pacientes / Mascotas
│   │   └── FacturacionDAO.cs            # Consultas de Atención y Emisión de Facturas
│   │
│   ├── Database/                        # Configuración y Conexión a Base de Datos
│   │   └── DatabaseHelper.cs            # Conexión SQLite y creación del esquema DDL
│   │
│   ├── Forms/                           # Capa de Interfaz Gráfica (Windows Forms)
│   │   ├── FormPrincipal.cs             # Menú principal de navegación
│   │   ├── FormPropietarios.cs          # Formulario de gestión de clientes
│   │   ├── FormMascotas.cs              # Formulario de registro de pacientes
│   │   └── FormFacturacion.cs           # Formulario de cobros y atención médica
│   │
│   ├── Program.cs / Veterinaria.cs      # Punto de entrada principal (Main)
│   ├── VS_Code.csproj                   # Configuración del proyecto, WinForms y Paquetes NuGet
│   └── .gitignore                       # Filtro para ignorar archivos temporales (bin, obj, .db)
│
├── BlueJ/                               # (Legacy) Prototipo inicial del modelo de clases en Java
├── Draw.io/                             # Diagramas de flujo y arquitectura del sistema
└── README.md                            # Documentación principal del repositorio
