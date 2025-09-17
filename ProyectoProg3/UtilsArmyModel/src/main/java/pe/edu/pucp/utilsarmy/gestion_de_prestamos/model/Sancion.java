
package pe.edu.pucp.utilsarmy.gestion_de_prestamos.model;

import java.util.Date;

public class Sancion {

    private int id_sancion;
    private Tipo_sancion tipo_sancion;
    private int duracion_dias;
    private Date fecha_inicio;
    private Date fecha_fin;
    private String justificacion;
    private EstadoSancion estado;
    private boolean activo;
    private Prestamo prestamo;

    public Sancion() {
    }

    public Sancion(Tipo_sancion tipo_sancion, int duracion_dias, Date fecha_inicio,
            Date fecha_fin, String justificacion, Prestamo p) {
        this.tipo_sancion = tipo_sancion;
        this.duracion_dias = duracion_dias;
        this.fecha_inicio = fecha_inicio;
        this.fecha_fin = fecha_fin;
        this.justificacion = justificacion;
        this.prestamo=p;
    }
    
    

    public int getId_sancion() {
        return id_sancion;
    }

    public void setId_sancion(int id_sancion) {
        this.id_sancion = id_sancion;
    }

    public Tipo_sancion getTipo_sancion() {
        return tipo_sancion;
    }

    public void setTipo_sancion(Tipo_sancion tipo_sancion) {
        this.tipo_sancion = tipo_sancion;
    }

    public int getDuracion_dias() {
        return duracion_dias;
    }

    /**
     * @param duracion_dias the duracion_dias to set
     */
    public void setDuracion_dias(int duracion_dias) {
        this.duracion_dias = duracion_dias;
    }

    /**
     * @return the fecha_inicio
     */
    public Date getFecha_inicio() {
        return fecha_inicio;
    }

    /**
     * @param fecha_inicio the fecha_inicio to set
     */
    public void setFecha_inicio(Date fecha_inicio) {
        this.fecha_inicio = fecha_inicio;
    }

    /**
     * @return the fecha_fin
     */
    public Date getFecha_fin() {
        return fecha_fin;
    }

    /**
     * @param fecha_fin the fecha_fin to set
     */
    public void setFecha_fin(Date fecha_fin) {
        this.fecha_fin = fecha_fin;
    }

    /**
     * @return the justificacion
     */
    public String getJustificacion() {
        return justificacion;
    }

    /**
     * @param justificacion the justificacion to set
     */
    public void setJustificacion(String justificacion) {
        this.justificacion = justificacion;
    }

    /**
     * @return the estado
     */
    public EstadoSancion getEstado() {
        return estado;
    }

    /**
     * @param estado the estado to set
     */
    public void setEstado(EstadoSancion estado) {
        this.estado = estado;
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

    public Prestamo getPrestamo() {
        return prestamo;
    }

    public void setPrestamo(Prestamo prestamo) {
        this.prestamo = prestamo;
    }

}
