------------------------------------------------------------------------
PROJECT TITLE: Sistema de Gestion Veterinaria
PURPOSE OF PROJECT: Administracion de expedientes medicos, calculo de
tarifas polimorficas, control de vacunacion obligatoria y seguimiento
de KPIs del centro veterinario.
VERSION or DATE: 1.0
HOW TO START THIS PROJECT: 
1. Presionar el boton "Compile" en la interfaz de BlueJ.
2. Hacer clic derecho sobre la clase "Main".
3. Seleccionar la opcion "void main(String[] args)".
4. Hacer clic en "OK" en la ventana emergente para abrir la terminal.
AUTHORS: [Pablo Guzman]

------------------------------------------------------------------------

ESTRUCTURA Y ARQUITECTURA DEL PROYECTO
========================================================================
1. Propietario.java       : Datos del cliente/tutor (ID, Nombre).
2. Mascota.java           : Datos del paciente (ID, Nombre, Especie, Vacunas).
3. Atencion.java          : Clase abstracta base para cobro polimorfico (RN-02).
   - ConsultaGeneral.java : Atencion basica mas receta medica.
   - Cirugia.java         : Intervencion quirurgica mas pruebas de laboratorio.
4. Hospitalizacion.java   : Calculo de tarifa por dias internado (RN-04).
5. Factura.java           : Consolidacion de cobro y alta medica (CU-08).
6. SistemaVeterinario.java: Controlador principal del sistema y KPIs (RN-05).
7. Main.java             : Clase de ejecucion y punto de entrada.


REGLAS DE NEGOCIO APLICADAS
========================================================================
- RN-01: Busqueda por ID de expediente. Si no existe, genera nuevo registro.
- RN-02: Aplicacion de tarifa polimorfica segun tipo de servicio.
- RN-03: Esquema de vacunacion obligatoria (+Q 25.00 en caso de estar desactualizado).
- RN-04: Acumulacion de dias de hospitalizacion (Q 45.00 por dia).
- RN-05: Actualizacion de metricas globales del sistema al emitir alta.
------------------------------------------------------------------------