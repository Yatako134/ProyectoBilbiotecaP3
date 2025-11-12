
package biblioteca.services.gestionMaterial;

import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;

@WebService(serviceName = "EjemplarWS")
public class EjemplarWS {

    private EjemplarBOImpl ejemplarbo;

    @WebMethod(operationName = "insertarEjemplar")
    public int insertarEjemplar(@WebParam(name = "ejemplar") Ejemplar ejemplar) {
        int resultado = 0;
        try {
            ejemplarbo = new EjemplarBOImpl();
            resultado = ejemplarbo.insertar(ejemplar);
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return resultado;
    }

    @WebMethod(operationName = "modificarEjemplar")
    public int modificarEjemplar(@WebParam(name = "ejemplar") Ejemplar ejemplar) throws Exception {
        ejemplarbo = new EjemplarBOImpl();
        return ejemplarbo.modificar(ejemplar);
    }

    @WebMethod(operationName = "listarEjemplaresTodos")
    public ArrayList<Ejemplar> listarEjemplaresTodos() throws Exception {
        ArrayList<Ejemplar> ejemplares = null;
        try {
            ejemplarbo = new EjemplarBOImpl();
            ejemplares = ejemplarbo.listarTodos();
            if (ejemplares == null) {
                ejemplares = new ArrayList<>();
                return ejemplares;
            }
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return ejemplares;
    }

    @WebMethod(operationName = "obtenerEjemplarPorId")
    public Ejemplar obtenerEjemplarPorId(@WebParam(name = "idEjemplar") int idEjemplar) throws Exception {
        Ejemplar ejemplar = null;
        try {
            ejemplarbo = new EjemplarBOImpl();
            ejemplar = ejemplarbo.obtenerPorId(idEjemplar);
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return ejemplar;
    }

    @WebMethod(operationName = "eliminarEjemplar")
    public int eliminarEjemplar(@WebParam(name = "idEjemplar") int idEjemplar) throws Exception {
        int resultado = 0;
        try {
            ejemplarbo = new EjemplarBOImpl();
            resultado = ejemplarbo.eliminar(idEjemplar);
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return resultado;
    }
}
