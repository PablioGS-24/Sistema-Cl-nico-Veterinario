
/**
 * Write a description of class Factura here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class Factura {
    private Propietario propietario;
    private Mascota mascota;
    private Atencion atencion;
    private Hospitalizacion hospitalizacion;
    private double costoVacuna;

    public Factura(Propietario propietario, Mascota mascota, Atencion atencion, Hospitalizacion hospitalizacion, double costoVacuna) {
        this.propietario = propietario;
        this.mascota = mascota;
        this.atencion = atencion;
        this.hospitalizacion = hospitalizacion;
        this.costoVacuna = costoVacuna;
    }

    public double calcularTotal() {
        double total = atencion.calcularTarifa() + costoVacuna;
        if (hospitalizacion != null) {
            total += hospitalizacion.calcularCostoTotal();
        }
        return total;
    }

    public void imprimirFactura() {
        System.out.println("==========================================");
        System.out.println("         FACTURA DE ALTA MEDICA (CU-08)   ");
        System.out.println("==========================================");
        System.out.println("Propietario: " + propietario.getNombre() + " (ID: " + propietario.getId() + ")");
        System.out.println("Mascota: " + mascota.getNombre());
        System.out.println("Servicio: " + atencion.getDescripcion() + " -> Q" + atencion.calcularTarifa());
        if (hospitalizacion != null) {
            System.out.println("Hospitalizacion: Q" + hospitalizacion.calcularCostoTotal());
        }
        if (costoVacuna > 0) {
            System.out.println("Vacuna Obligatoria (RN-03): Q" + costoVacuna);
        }
        System.out.println("------------------------------------------");
        System.out.println("TOTAL CONSOLIDADO: Q" + calcularTotal());
        System.out.println("==========================================");
    }
}