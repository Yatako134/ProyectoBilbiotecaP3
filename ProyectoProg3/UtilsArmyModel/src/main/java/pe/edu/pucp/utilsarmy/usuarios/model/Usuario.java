
package pe.edu.pucp.utilsarmy.usuarios.model;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Prestamo;

public class Usuario {
    private int id_usuario;
    private String nombre;
    private String primer_apellido;
    private String segundo_apellido;
    private String DOI;
    private int codigo;
    private String correo;
    
    private String contrasena;
    //private String username;
    private String telefono;
    private int id_rol;
    //private Rol rol_usuario;
    private boolean activa;
    private ArrayList <Prestamo> prestamos;

    public Usuario( String nombre, String primer_apellido, String segundo_apellido, String DOI,
            int codigo,String correo, String contrasena, String telefono,int rol){
        this.nombre = nombre;
        this.primer_apellido = primer_apellido;
        this.segundo_apellido = segundo_apellido;
        this.DOI = DOI;
        this.codigo=codigo;
        this.correo = correo;
        this.contrasena = contrasena;
        //this.username = username;
        this.telefono = telefono;
        this.id_rol=rol;
        
        prestamos=new ArrayList<Prestamo>();
        //this.rol_usuario = rol_usuario;
    }
    public Usuario() {
        prestamos=new ArrayList<Prestamo>();
    }

    public int getId_usuario() {
        return id_usuario;
    }

    public void setId_usuario(int id_usuario) {
        this.id_usuario = id_usuario;
    }

    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public String getPrimer_apellido() {
        return primer_apellido;
    }

    public void setPrimer_apellido(String primer_apellido) {
        this.primer_apellido = primer_apellido;
    }

    public String getSegundo_apellido() {
        return segundo_apellido;
    }

    public void setSegundo_apellido(String segundo_apellido) {
        this.segundo_apellido = segundo_apellido;
    }

    public String getDOI() {
        return DOI;
    }

    public void setDOI(String DOI) {
        this.DOI = DOI;
    }

    

    public int getCodigo() {
        return codigo;
    }

    public void setCodigo(int codigo) {
        this.codigo = codigo;
    }

    public String getCorreo() {
        return correo;
    }

    public void setCorreo(String correo) {
        this.correo = correo;
    }


    public String getContrasena() {
        return contrasena;
    }

    public void setContrasena(String contrasena) {
        this.contrasena = contrasena;
    }

    public String getTelefono() {
        return telefono;
    }

    public void setTelefono(String telefono) {
        this.telefono = telefono;
    }

    public int getId_rol() {
        return id_rol;
    }

    public void setId_rol(int id_rol) {
        this.id_rol = id_rol;
    }

    public boolean isActiva() {
        return activa;
    }

    public void setActiva(boolean activa) {
        this.activa = activa;
    }
    
    @Override
    public String toString() {
        return "id_usuario=" + id_usuario +
                ", nombre='" + nombre + '\'' +
                ", primer_apellido='" + primer_apellido + '\'' +
                ", segundo_apellido='" + segundo_apellido + '\'' +
                ", DOI='" + DOI + '\'' +
                ", codigo=" + codigo +
                ", correo='" + correo + '\'' +
                ", telefono='" + telefono + '\'' +
                ", id_rol=" + id_rol +
                ", activa=" + activa +"\n";
    }

}
