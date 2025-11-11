
package pe.edu.pucp.utilsarmy.usuarios.model;

public class Rol {

    private int id_rol;
    private String tipo;
    private int cantidad_de_dias_por_prestamo;
    private boolean activo;
    private int limite_prestamo;
    
    public Rol() {
    }

    public Rol(String tipo, int cantidad_de_dias_por_prestamo, boolean activo) {
        this.tipo = tipo;
        this.cantidad_de_dias_por_prestamo = cantidad_de_dias_por_prestamo;
        this.activo = activo;
    }

    
    /**
     * @return the id_rol
     */
    public int getId_rol() {
        return id_rol;
    }

    /**
     * @param id_rol the id_rol to set
     */
    public void setId_rol(int id_rol) {
        this.id_rol = id_rol;
    }

    public int getLimite_prestamo() {
        return limite_prestamo;
    }

    public void setLimite_prestamo(int limite_prestamo) {
        this.limite_prestamo = limite_prestamo;
    }

    /**
     * @return the tipo
     */
    public String getTipo() {
        return tipo;
    }

    /**
     * @param tipo the tipo to set
     */
    public void setTipo(String tipo) {
        this.tipo = tipo;
    }
    
    
    /**
     * @return the cantidad_de_dias_por_prestamo
     */
    public int getCantidad_de_dias_por_prestamo() {
        return cantidad_de_dias_por_prestamo;
    }

    /**
     * @param cantidad_de_dias_por_prestamo the cantidad_de_dias_por_prestamo to set
     */
    public void setCantidad_de_dias_por_prestamo(int cantidad_de_dias_por_prestamo) {
        this.cantidad_de_dias_por_prestamo = cantidad_de_dias_por_prestamo;
    }

    /**
     * @return the activo
     */
    public boolean isActivo() {
        return activo;
    }

    /**
     * @param activo the activo to set
     */
    public void setActivo(boolean activo) {
        this.activo = activo;
    }
    @Override
    public String toString() {
        return "Rol { " +
               "id_rol=" + id_rol +
               ", tipo='" + tipo + '\'' +
               ", cantidad_de_dias_por_prestamo=" + getCantidad_de_dias_por_prestamo() +
               ", activo=" + isActivo() +
               " }";
    }

}
