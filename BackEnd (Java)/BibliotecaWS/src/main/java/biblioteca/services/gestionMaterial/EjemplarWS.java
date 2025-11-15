package biblioteca.services.gestionMaterial;

import biblioteca.gestionMaterial.boImpl.EjemplarBOImpl;
import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;

import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Ejemplar;

@WebService(serviceName = "EjemplarWS", targetNamespace = "pe.edu.pucp.utilsarmy.services")
public class EjemplarWS {

    private EjemplarBOImpl ejemplarbo;

    public EjemplarWS() {
        ejemplarbo = new EjemplarBOImpl();
    }

    @WebMethod(operationName = "insertarEjemplar")
    public int insertarEjemplar(@WebParam(name = "ejemplar") Ejemplar ejemplar
    ) {
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
        
    @WebMethod(operationName = "listar_disponibles_por_material")
    public ArrayList<Ejemplar> listar_disponibles_por_material(@WebParam(name = "_id_material")int _id_material)throws Exception {
        return ejemplarbo.listar_disponibles_por_material(_id_material);
    }   
    
}
