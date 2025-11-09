
package pe.edu.pucp.utilsarmy.gestion_de_prestamos.model;

import java.util.Date;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Ejemplar;
import pe.edu.pucp.utilsarmy.usuarios.model.Usuario;

public class Prestamo {

    private int idPrestamo;
    private Date fecha_de_prestamo;
    private Date fecha_vencimiento;
    private Date fecha_devolucion;
    private EstadoPrestamo estado;
    private Ejemplar ejemplar;
    private Usuario usuario;
    public Prestamo() {
    }

    public Prestamo(Date fecha_de_prestamo, Date fecha_vencimiento,
            EstadoPrestamo estado, Ejemplar ejemplar,Usuario usuario) {
        this.fecha_de_prestamo = fecha_de_prestamo;
        this.fecha_vencimiento = fecha_vencimiento;
        this.estado = estado;
        this.ejemplar = ejemplar;
        this.usuario = usuario;
    }
    
    

    /**
     * @return the idPrestamo
     */
    public int getIdPrestamo() {
        return idPrestamo;
    }

    /**
     * @param idPrestamo the idPrestamo to set
     */
    public void setIdPrestamo(int idPrestamo) {
        this.idPrestamo = idPrestamo;
    }

    /**
     * @return the fecha_de_prestamo
     */
    public Date getFecha_de_prestamo() {
        return fecha_de_prestamo;
    }

    /**
     * @param fecha_de_prestamo the fecha_de_prestamo to set
     */
    public void setFecha_de_prestamo(Date fecha_de_prestamo) {
        this.fecha_de_prestamo = fecha_de_prestamo;
    }
    public Date getFecha_vencimiento() {
        return fecha_vencimiento;
    }

    public void setFecha_vencimiento(Date fecha_vencimiento) {
        this.fecha_vencimiento = fecha_vencimiento;
    }

    public Date getFecha_devolucion() {
        return fecha_devolucion;
    }

    /**
     * @param fecha_devolucion the fecha_devolucion to set
     */
    public void setFecha_devolucion(Date fecha_devolucion) {
        this.fecha_devolucion = fecha_devolucion;
    }

    /**
     * @return the estado
     */
    public EstadoPrestamo getEstado() {
        return estado;
    }

    /**
     * @param estado the estado to set
     */
    public void setEstado(EstadoPrestamo estado) {
        this.estado = estado;
    }

    /**
     * @return the ejemplar
     */
    public Ejemplar getEjemplar() {
        return ejemplar;
    }

    /**
     * @param ejemplar the ejemplar to set
     */
    public void setEjemplar(Ejemplar ejemplar) {
        this.ejemplar = ejemplar;
    }

    public Usuario getUsuario() {
        return usuario;
    }

    public void setUsuario(Usuario usuario) {
        this.usuario = usuario;
    }

}
