
/**
 * Write a description of class Propietario here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class Propietario {
    private String id;
    private String nombre;

    public Propietario(String id, String nombre) {
        this.id = id;
        this.nombre = nombre;
    }

    public String getId() { return id; }
    public String getNombre() { return nombre; }
}