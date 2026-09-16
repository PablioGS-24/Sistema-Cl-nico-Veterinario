
/**
 * Write a description of class Atencion here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public abstract class Atencion {
    protected String descripcion;

    public String getDescripcion() { return descripcion; }
    public abstract double calcularTarifa();
}