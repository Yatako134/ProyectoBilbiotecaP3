package pe.edu.pucp.utilsarmy.gestion_de_material.model;

public class Biblioteca {
    private int idBiblioteca;
    private String nombre;
    private String ubicacion;
    private boolean activo;

    public Biblioteca( String nombre, String ubicacion, boolean activo) {
        this.nombre = nombre;
        this.ubicacion = ubicacion;
        this.activo = activo;
    }

    public Biblioteca() {
    }

    public int getIdBiblioteca() {
        return idBiblioteca;
    }

    public void setIdBiblioteca(int idBiblioteca) {
        this.idBiblioteca = idBiblioteca;
    }

    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public String getUbicacion() {
        return ubicacion;
    }

    public void setUbicacion(String ubicacion) {
        this.ubicacion = ubicacion;
    }

    public boolean isActivo() {
        return activo;
    }

    public void setActivo(boolean activo) {
        this.activo = activo;
    }
    
    
}
