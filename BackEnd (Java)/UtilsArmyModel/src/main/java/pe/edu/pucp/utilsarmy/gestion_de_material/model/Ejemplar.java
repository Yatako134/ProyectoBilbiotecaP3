package pe.edu.pucp.utilsarmy.gestion_de_material.model;
public class Ejemplar {
    private int idEjemplar;
    private int id_material;
    private EstadoEjemplar estado;
    private String ubicacion;
    private Biblioteca blibioteca;
    private boolean activo;
    
    public Ejemplar() {
        this.estado = EstadoEjemplar.DISPONIBLE;//Por defecto disponible
    }

    public Ejemplar(EstadoEjemplar estado, String ubicacion,Biblioteca biblioteca,
           int id_biblioteca, boolean activo, int id_material) {
        this.estado = estado;
        this.ubicacion = ubicacion;
        this.blibioteca=biblioteca;
        this.activo = activo;
        this.id_material=id_material;
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

    /**
     * @return the id_material
     */
    public int getId_material() {
        return id_material;
    }

    /**
     * @param id_material the id_material to set
     */
    public void setId_material(int id_material) {
        this.id_material = id_material;
    }

    public Biblioteca getBlibioteca() {
        return blibioteca;
    }

    public void setBlibioteca(Biblioteca blibioteca) {
        this.blibioteca = blibioteca;
    }

    
}
