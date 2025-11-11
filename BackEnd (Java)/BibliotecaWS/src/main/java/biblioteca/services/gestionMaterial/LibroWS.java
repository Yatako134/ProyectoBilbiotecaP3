
package biblioteca.services.gestionMaterial;


import biblioteca.gestionMaterial.boImpl.LibroBOImpl;
import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Libro;
@WebService(serviceName = "LibroWS")
public class LibroWS {

    private LibroBOImpl librobo;

    @WebMethod(operationName = "insertarLibro")
    public int insertarLibro(@WebParam(name = "libro") Libro libro) {
        int resultado = 0;
        try {
            librobo = new LibroBOImpl();
            resultado = librobo.insertar(libro);
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return resultado;
    }

    @WebMethod(operationName = "modificarLibro")
    public int modificarLibro(@WebParam(name = "libro") Libro libro) throws Exception {
        librobo = new LibroBOImpl();
        return librobo.modificar(libro);
    }

    @WebMethod(operationName = "listarLibrosTodos")
    public ArrayList<Libro> listarLibrosTodos() throws Exception {
        ArrayList<Libro> libros = null;
        try {
            librobo = new LibroBOImpl();
            libros = librobo.listarTodos();
            if (libros == null) {
                libros = new ArrayList<>();
                return libros;
            }
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return libros;
    }

    @WebMethod(operationName = "obtenerLibroPorId")
    public Libro obtenerLibroPorId(@WebParam(name = "idLibro") int idLibro) throws Exception {
        Libro libro = null;
        try {
            librobo = new LibroBOImpl();
            libro = librobo.obtenerPorId(idLibro);
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return libro;
    }

    @WebMethod(operationName = "eliminarLibro")
    public int eliminarLibro(@WebParam(name = "idLibro") int idLibro) throws Exception {
        int resultado = 0;
        try {
            librobo = new LibroBOImpl();
            resultado = librobo.eliminar(idLibro);
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return resultado;
    }
}
