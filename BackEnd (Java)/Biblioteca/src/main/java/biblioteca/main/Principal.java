
package biblioteca.main;
import biblioteca.config.DBManager;
import biblioteca.config.Encriptamiento;
import biblioteca.gestionMaterial.mysql.BibliotecaImpl;
import biblioteca.gestionMaterial.mysql.EjemplarImpl;
import biblioteca.gestionMaterial.mysql.Librolmpl;
import biblioteca.gestionMaterial.mysql.Tesislmpl;
import biblioteca.gestionPrestamo.dao.PrestamoDAO;
import biblioteca.gestionPrestamo.mysql.PrestamoImpl;
import biblioteca.gestionUsuario.boImpl.UsuarioBOImpl;
import biblioteca.gestionUsuario.dao.RolDAO;
import biblioteca.gestionUsuario.mysql.RolImpl;
import biblioteca.gestionUsuario.mysql.UsuarioImpl;
import java.sql.Connection;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Biblioteca;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Editorial;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Ejemplar;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.EstadoEjemplar;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.EstadoMaterial;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Libro;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Tesis;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.EstadoPrestamo;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Prestamo;
import pe.edu.pucp.utilsarmy.usuarios.model.Rol;
import pe.edu.pucp.utilsarmy.usuarios.model.Usuario;

public class Principal {
    public static void main(String[] args) throws Exception{
//        int i=1;
//        // Libro
//        
//        Libro libro = new Libro("9783161484100", "3ra edición", 1, 5, "Programación en Java",
//                2023, 850, EstadoMaterial.DISPONIBLE, "Ingeniería de Software",
//                true, "Español", new Editorial(1, "Pearson"));
//        Librolmpl libroDAO = new Librolmpl();
//        int resultadoUser = libroDAO.insertar(libro);
//        libro = libroDAO.obtenerPorId(resultadoUser);
//        
//        libro.setTitulo("LIBRO-MODIFICADO");
//        libroDAO.modificar(libro);
//        
//        ArrayList<Libro> libros;
//        libros = libroDAO.listarTodos();
//        System.out.println("Listado de libros:");
//        System.out.println("=====================");
//        for(Libro l : libros){
//            System.out.println(i + ") " + l.getTitulo());
//        } 
//        
//        // Biblioteca
//        i=0;
//        Biblioteca biblioteca = new Biblioteca(1, "Biblioteca Central", "Av. Universitaria 123 - Lima", true);
//        BibliotecaImpl bibliotecaDAO = new BibliotecaImpl();
//        int resultadoBiblio = bibliotecaDAO.insertar(biblioteca);
//        biblioteca = bibliotecaDAO.obtenerPorId(resultadoBiblio);
//        
//        biblioteca.setNombre("CIA");
//        bibliotecaDAO.modificar(biblioteca);
//        
//        ArrayList<Biblioteca> bibliotecas;
//        bibliotecas = bibliotecaDAO.listarTodos();
//        System.out.println("Listado de bibliotecas:");
//        System.out.println("=====================");
//        for(Biblioteca l : bibliotecas){
//            System.out.println(i + ") " + l.getNombre());
//        } 
//        
////         Ejemplar
//        i=0;
//        Ejemplar ejemplar = new Ejemplar(1, EstadoEjemplar.DISPONIBLE, "Estante A1", 1, true, 1);
//        EjemplarImpl ejemplarDAO = new EjemplarImpl();
//        int resultadoEjemplar = ejemplarDAO.insertar(ejemplar);
//        ejemplar = ejemplarDAO.obtenerPorId(resultadoEjemplar);
//        
//        ejemplar.setEstado(EstadoEjemplar.EN_REPARACION);
//        ejemplarDAO.modificar(ejemplar);
//        
//        ArrayList<Ejemplar> ejemplares;
//        ejemplares = ejemplarDAO.listarTodos();
//        System.out.println("Listado de ejemplares:");
//        System.out.println("=====================");
//        for(Ejemplar l : ejemplares){
//            System.out.println(i + ") " + l.getUbicacion());
//        }
//        
//        int resultado;
////        
////        //Rol
//        Rol rol = new Rol("Alumno - Posgrado");
//        RolDAO daorol = new RolImpl();
//        resultado = daorol.insertar(rol);
//        
//        rol = new Rol("Profesor");
//        daorol = new RolImpl();
//        resultado = daorol.insertar(rol);
//        
//        rol = daorol.obtenerPorId(1);
//        System.out.println(rol.getTipo());
//        rol.setTipo("Bibliotecario");
//        daorol.modificar(rol);
//        if(resultado!=0)
//            System.out.println("El rol se ha modificado con exito");
//        
//        
//        
////        //USUARIO
//        Usuario user = new Usuario("Carlos","Ramos","Lopez","3490",20252324,
//                "carlosr@gmail.com","gsdf1","999993698",2);
//        Usuario user2 = new Usuario("Pepe","Ramiro","Lopez","1234",20244324,
//                "ctyuu@gmail.com","gsdf1","990999678",1);
//        Usuario user3 = new Usuario("Luis","Ramon","Gomez","5678",20789324,
//                "cpouyu@gmail.com","gsdf1","945623678",2);
//        UsuarioImpl usuarioDAO = new UsuarioImpl();
//        resultado = usuarioDAO.insertar(user);
//        resultado = usuarioDAO.insertar(user2);
//        resultado = usuarioDAO.insertar(user3);
//        
//        Usuario userModificado = new Usuario();
//        userModificado = usuarioDAO.obtenerPorId(2);
//        userModificado.setNombre("Pepian");
//        //modificar
//        usuarioDAO.modificar(userModificado);
//        //eliminar
//        usuarioDAO.eliminar(1);
//        ArrayList<Usuario> listaUsuarios = new ArrayList<>();
//        listaUsuarios = usuarioDAO.listarTodos();
//        for(Usuario u :listaUsuarios){
//            System.out.println(u.getNombre());
//        }
        
        //PRESTAMO
        
        //Crear fechas
        
//        Calendar cal = Calendar.getInstance();
//        Date fechaPrestamo = cal.getTime(); // hoy
//        cal.add(Calendar.DAY_OF_MONTH, 14); // agregar 14 días
//        Date fechaVencimiento = cal.getTime();
//        
//        Usuario user2 = new Usuario("Pepe","Ramiro","Lopez","1234",20244324,
//                "ctyuu@gmail.com","gsdf1","990999678",1);
//        Ejemplar ejemplar = new Ejemplar(1, EstadoEjemplar.DISPONIBLE, "Estante A1", 1, true, 1);
//        
//        Prestamo prestamo1 = new Prestamo(fechaPrestamo, fechaVencimiento,EstadoPrestamo.VIGENTE, ejemplar,user2);
//        
//        PrestamoDAO prestDAO = new PrestamoImpl();
//        int resultado = prestDAO.insertar(prestamo1);
//        
//        prestamo1 = prestDAO.obtenerPorId(resultado);
//        prestamo1.setEstado(EstadoPrestamo.RETRASADO);
//        prestDAO.modificar(prestamo1);
//        
//        ArrayList<Prestamo> listaPrestamos;
//        listaPrestamos = prestDAO.listarTodos();
//        for(Prestamo pr : listaPrestamos){
//            System.out.println(pr.getFecha_de_prestamo());
//        }
//        
        String contra = Encriptamiento.encriptar("Prog3_BD_20223397", "jGwZUx4lOJZj8wXav7Sknw==");
        UsuarioBOImpl bouser = new UsuarioBOImpl();
        ArrayList<Usuario> usuarios = bouser.listarTodos();
        DBManager.getInstance().getConnection();
    }
}
