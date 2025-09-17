
package pe.edu.pucp.utilsarmy.usuarios.model;

public class Rol {
    
    private int id_rol;
    private String tipo;

    public Rol() {
    }

    public Rol( String tipo) {
        this.tipo = tipo;
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
    @Override
    public String toString() {
        return String.format("Rol { id_rol=%d, tipo='%s' }", id_rol, tipo);
    }

}
