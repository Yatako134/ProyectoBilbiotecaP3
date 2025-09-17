
package biblioteca.main;
import biblioteca.config.DBManager;
import biblioteca.gestionPrestamo.dao.PrestamoDAO;
import biblioteca.gestionPrestamo.mysql.PrestamoImpl;
import biblioteca.gestionUsuario.dao.RolDAO;
import biblioteca.gestionUsuario.mysql.RolImpl;
import biblioteca.gestionUsuario.mysql.UsuarioImpl;
import java.sql.Connection;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Biblioteca;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Ejemplar;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.EstadoEjemplar;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.EstadoPrestamo;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Prestamo;
import pe.edu.pucp.utilsarmy.usuarios.model.Rol;
import pe.edu.pucp.utilsarmy.usuarios.model.Usuario;
public class Principal {
    public static void main(String[] args){
        //Rol
        /*Rol rol = new Rol("Alumno - Posgrado");
        RolDAO daorol = new RolImpl();
        int resultado = daorol.insertar(rol);
        rol = daorol.obtenerPorId(resultado);
        System.out.println(rol);
        rol.setTipo("Alumno - Pregrado");
        daorol.modificar(rol);
        if(resultado!=0)
            System.out.println("El rol se ha modificado con exito");
        ArrayList<Rol> roles = daorol.listarTodos();
        for(Rol r : roles){
            System.out.println(r);
        }
        
        //Usuario
        Usuario user = new Usuario("Carlos","Ramos","Lopez","3490",20252324,
                "carlosr@gmail.com","gsdf1","990923678",2);
        UsuarioImpl usuarioDAO = new UsuarioImpl();
        resultado = usuarioDAO.insertar(user);
        user = usuarioDAO.obtenerPorId(resultado);
        System.out.println(user);
        user.setNombre("Miguel Jose");
        usuarioDAO.modificar(user);
        if(resultado!=0)
            System.out.println("El usuario se ha modificado con exito");
        resultado = usuarioDAO.eliminar(user.getId_rol());
        if(resultado!=0)
            System.out.println("El usuario se ha eliminado con exito");
         ArrayList<Usuario> usuarios = usuarioDAO.listarTodos();
        for(Usuario u : usuarios){
            System.out.println(u);
        }*/
        
        
        
        //Prestamo
        Biblioteca biblio = new Biblioteca(1, "Biblioteca Central", "Av. Universitaria 1234, Lima", true);
        Ejemplar ejemplar1 = new Ejemplar(1, EstadoEjemplar.DISPONIBLE, "Estante 5A", biblio, true);
        Usuario usuario1 = new Usuario(1, "Juan", "Pérez", "García", "U001",
                20202425, "juan@mail.com","1234", "987654321",1);

        // Crear fechas
        Calendar cal = Calendar.getInstance();
        Date fechaPrestamo = cal.getTime(); // hoy
        cal.add(Calendar.DAY_OF_MONTH, 14); // agregar 14 días
        Date fechaVencimiento = cal.getTime();

        // Crear préstamo
        Prestamo prestamo1 = new Prestamo(fechaPrestamo, fechaVencimiento, 
                EstadoPrestamo.VIGENTE, ejemplar1, usuario1);
        
        PrestamoDAO prestDAO = new PrestamoImpl();
        int resultado = prestDAO.insertar(prestamo1);
        
        
    }
}
