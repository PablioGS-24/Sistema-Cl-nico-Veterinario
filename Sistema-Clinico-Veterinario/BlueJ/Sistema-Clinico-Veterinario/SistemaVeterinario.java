
/**
 * Write a description of class SistemaVeterinario here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
import java.util.HashMap;
import java.util.Map;

public class SistemaVeterinario {
    private Map<String, Propietario> propietarios = new HashMap<>();
    private Map<String, Mascota> mascotas = new HashMap<>();
    
    private double ingresosTotales = 0.0;
    private int totalAtenciones = 0;

    public Propietario buscarPropietario(String id) {
        return propietarios.get(id);
    }

    public void registrarCliente(Propietario p, Mascota m) {
        propietarios.put(p.getId(), p);
        mascotas.put(m.getId(), m);
        System.out.println("[CU-01] Propietario " + p.getNombre() + " y mascota " + m.getNombre() + " registrados.");
    }

    public void registrarFacturaKPI(Factura factura) {
        this.ingresosTotales += factura.calcularTotal();
        this.totalAtenciones++;
    }

    public void mostrarKPIs() {
        System.out.println("\n--- METRICAS Y KPIS DEL SISTEMA (RN-05 / CU-10) ---");
        System.out.println("Total de atenciones procesadas: " + totalAtenciones);
        System.out.println("Ingresos totales acumulados: Q" + ingresosTotales);
    }
}