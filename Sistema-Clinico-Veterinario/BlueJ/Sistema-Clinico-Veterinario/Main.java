
/**
 * Write a description of class Main here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class Main {
    public static void main(String[] args) {
        SistemaVeterinario sistema = new SistemaVeterinario();

        // 1. Simulación de búsqueda y registro (CU-01 / RN-01)
        String idExpediente = "PROP-101";
        Propietario prop = sistema.buscarPropietario(idExpediente);
        
        if (prop == null) {
            System.out.println("Expediente " + idExpediente + " no encontrado. Registrando cliente...");
            prop = new Propietario(idExpediente, "Carlos Gomez");
            Mascota masc = new Mascota("MASC-01", "Rocky", "Perro", false);
            sistema.registrarCliente(prop, masc);
        }

        Mascota mascotaCarlos = new Mascota("MASC-01", "Rocky", "Perro", false);

        // 2. Selección de atención polimórfica (RN-02): Cirugía + Laboratorio
        Atencion atencion = new Cirugia(75.0);

        // 3. Hospitalización por 3 días (RN-04 / CU-07)
        Hospitalizacion hosp = new Hospitalizacion(3);

        // 4. Validación de esquema de vacunación (RN-03 / CU-09)
        double costoVacuna = 0.0;
        if (!mascotaCarlos.isVacunasAlDia()) {
            System.out.println("[CU-09] Vacunas desactualizadas. Aplicando dosis obligatoria...");
            costoVacuna = 25.0;
            mascotaCarlos.setVacunasAlDia(true);
        }

        // 5. Facturación Consolidada (CU-08)
        Factura factura = new Factura(prop, mascotaCarlos, atencion, hosp, costoVacuna);
        factura.imprimirFactura();

        // 6. Actualización de métricas (RN-05 / CU-10)
        sistema.registrarFacturaKPI(factura);
        sistema.mostrarKPIs();
    }
}