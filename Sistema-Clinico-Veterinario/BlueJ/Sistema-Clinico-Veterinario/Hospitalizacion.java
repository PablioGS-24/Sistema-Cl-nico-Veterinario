
/**
 * Write a description of class Hospitalizacion here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class Hospitalizacion {
    private int diasInternado;
    private double tarifaPorDia;

    public Hospitalizacion(int diasInternado) {
        this.diasInternado = diasInternado;
        this.tarifaPorDia = 40.0;
    }

    public double calcularCostoTotal() {
        return diasInternado * tarifaPorDia;
    }
}