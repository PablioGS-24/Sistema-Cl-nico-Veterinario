
/**
 * Write a description of class Cirugia here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class Cirugia extends Atencion {
    private double costoLaboratorio;

    public Cirugia(double costoLaboratorio) {
        this.descripcion = "Cirugia + Pruebas de Laboratorio";
        this.costoLaboratorio = costoLaboratorio;
    }

    @Override
    public double calcularTarifa() {
        double tarifaBase = 150.0;
        return tarifaBase + costoLaboratorio;
    }
}