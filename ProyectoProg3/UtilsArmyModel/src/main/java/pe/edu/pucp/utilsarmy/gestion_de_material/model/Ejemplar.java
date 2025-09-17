package pe.edu.pucp.utilsarmy.gestion_de_material.model;

public class Ejemplar {
    private int idEjemplar;
    private EstadoEjemplar estado;
    private String ubicacion;
    private Biblioteca biblioteca;
    private boolean activo;
    
    public Ejemplar() {
        this.estado = EstadoEjemplar.DISPONIBLE;//Por defecto disponible
    }

    public Ejemplar(int idEjemplar, EstadoEjemplar estado, String ubicacion,
           Biblioteca biblioteca, boolean activo) {
        this.idEjemplar = idEjemplar;
        this.estado = estado;
        this.ubicacion = ubicacion;
        this.biblioteca = biblioteca;
        this.activo = activo;
    }

    public int getIdEjemplar() {
        return idEjemplar;
    }

    public void setIdEjemplar(int idEjemplar) {
        this.idEjemplar = idEjemplar;
    }

    public EstadoEjemplar getEstado() {
        return estado;
    }

    public void setEstado(EstadoEjemplar estado) {
        this.estado = estado;
    }

    public String getUbicacion() {
        return ubicacion;
    }

    public void setUbicacion(String ubicacion) {
        this.ubicacion = ubicacion;
    }

    public Biblioteca getBiblioteca() {
        return biblioteca;
    }

    public void setBiblioteca(Biblioteca biblioteca) {
        this.biblioteca = biblioteca;
    }
    
    public boolean prestar() {
        if (estado == EstadoEjemplar.DISPONIBLE) {
            estado = EstadoEjemplar.PRESTADO;
            return true;
        }
        return false;
    }

    public boolean devolver() {
        if (estado == EstadoEjemplar.PRESTADO) {
            estado = EstadoEjemplar.DISPONIBLE;
            return true;
        }
        return false;
    }

    public void marcarEnReparacion() {
        estado = EstadoEjemplar.EN_REPARACION;
    }

    public void marcarNoDisponible() {
        estado = EstadoEjemplar.NO_DISPONIBLE;
    }    

    public boolean isActivo() {
        return activo;
    }

    public void setActivo(boolean activo) {
        this.activo = activo;
    }
    
    
}
