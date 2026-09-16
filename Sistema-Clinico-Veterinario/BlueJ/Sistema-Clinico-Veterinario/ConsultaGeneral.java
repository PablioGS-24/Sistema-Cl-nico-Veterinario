
/**
 * Write a description of class ConsultaGeneral here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class ConsultaGeneral extends Atencion {
    private double costoReceta;

    public ConsultaGeneral(double costoReceta) {
        this.descripcion = "Consulta General + Receta Farmacia";
        this.costoReceta = costoReceta;
    }

    @Override
    public double calcularTarifa() {
        double tarifaBase = 30.0;
        return tarifaBase + costoReceta;
    }
}