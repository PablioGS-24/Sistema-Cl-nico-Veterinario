
/**
 * Write a description of class Mascota here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class Mascota {
    private String id;
    private String nombre;
    private String especie;
    private boolean vacunasAlDia;

    public Mascota(String id, String nombre, String especie, boolean vacunasAlDia) {
        this.id = id;
        this.nombre = nombre;
        this.especie = especie;
        this.vacunasAlDia = vacunasAlDia;
    }

    public String getId() { return id; }
    public String getNombre() { return nombre; }
    public boolean isVacunasAlDia() { return vacunasAlDia; }
    public void setVacunasAlDia(boolean vacunasAlDia) { this.vacunasAlDia = vacunasAlDia; }
}