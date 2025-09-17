
package biblioteca.main;
import biblioteca.config.DBManager;
import biblioteca.gestionUsuario.dao.RolDAO;
import biblioteca.gestionUsuario.mysql.RolImpl;
import biblioteca.gestionUsuario.mysql.UsuarioImpl;
import java.sql.Connection;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.usuarios.model.Rol;
import pe.edu.pucp.utilsarmy.usuarios.model.Usuario;
public class Principal {
    public static void main(String[] args){
        //Usuario
        Usuario user = new Usuario("Carlos","Ramos","Lopez","3490",20252324,
                "carlosr@gmail.com","gsdf1","990923678",1);
        UsuarioImpl usuarioDAO = new UsuarioImpl();
        int resultado = usuarioDAO.insertar(user);
        user = usuarioDAO.obtenerPorId(resultado);
        System.out.println(user);
        user.setNombre("Miguel Jose");
        usuarioDAO.modificar(user);
        
        //Rol
        Rol rol = new Rol("Alumno - Posgrado");
        RolDAO daorol = new RolImpl();
        resultado = daorol.insertar(rol);
        rol = daorol.obtenerPorId(resultado);
        System.out.println(rol);
        rol.setTipo("Alumno - Pregrado");
        daorol.modificar(rol);
        
    }
}
