# Sistema de Gestión Veterinaria - VetCare Enterprise

Ultima actualizacion: 16 de septiembre de 2026

Bienvenidos al repositorio de apoyo y entrega para el **Sistema de Control Clinico Veterinario** del curso de Programacion 2.

El objetivo principal de este repositorio es modelar e implementar un **Sistema de Control Clinico Veterinario** aplicando los principios de la Programacion Orientada a Objetos (POO) en Java utilizando el entorno **BlueJ**, asi como su solucion interactiva equivalente en C# (.NET) desarrollada para Visual Studio Code.

## Alcance Del Repositorio

Este repositorio contiene la documentacion metodologica y el codigo fuente en Java y C# correspondiente a la arquitectura del sistema: casos de uso, diagrama de flujo de procesos, clases del modelo POO, reglas de negocio clinicas (RN-01 a RN-05) y la interfaz para la simulacion del sistema con emision de facturas en Quetzales (Q).

El material privado de evaluacion docente, archivos compilados `.class`, carpetas de salida `bin/` / `obj/` y configuraciones locales no forman parte de este repositorio publico.

## Objetivos Del Repositorio

- Aplicar Herencia y Polimorfismo mediante una clase base abstracta (`Atencion`) y sus subclases (`ConsultaGeneral` y `Cirugia`).
- Implementar Encapsulamiento y control de estado en los objetos de negocio (`Propietario`, `Mascota`, `Hospitalizacion` y `Factura`).
- Validar restricciones de integridad y reglas de negocio clinicas (IDs unicos RN-01, esquema vacunal obligatorio RN-03 y estadia hospitalaria RN-04).
- Administrar colecciones dinamicas en memoria (`HashMap` en Java / `Dictionary` en C#) para la gestion de expedientes de clientes y pacientes.
- Calcular tarifas polimorficas (RN-02) y metricas globales/KPIs de ingresos y atenciones clinicas (RN-05).
- Facilitar la ejecucion de pruebas mediante un metodo `main` automatizado en BlueJ y una consola interactiva validada con seleccion de especie en C#.

## Estructura

El repositorio se encuentra organizado de la siguiente manera:

```text
SistemaVeterinario/
├── BlueJ/       -> Proyecto Java para BlueJ (Propietario, Mascota, Atencion, ConsultaGeneral, Cirugia, Hospitalizacion, Factura, SistemaVeterinario, Main)
├── Draw.io/     -> Diagramas UML de Casos de Uso y Diagramas de Flujo (.drawio / .xml)
├── VS_Code/     -> Proyecto interactivo en C# (.NET) para Visual Studio Code (Program.cs)
├── .gitignore   -> Archivo de exclusion de archivos compilados y temporales (.class, .ctxt, bin/, obj/)
└── README.md    -> Documentacion principal del repositorio