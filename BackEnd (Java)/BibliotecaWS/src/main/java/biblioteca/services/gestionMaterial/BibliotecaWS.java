
package biblioteca.services.gestionMaterial;

import biblioteca.gestionMaterial.bo.BibliotecaBO;
import biblioteca.gestionMaterial.boImpl.BibliotecaBOImpl;
import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Biblioteca;
@WebService(serviceName = "BibliotecaWS")
public class BibliotecaWS {
    BibliotecaBO bibBO;
	
	
	@WebMethod(operationName = "insertarBiblioteca")
    public int insertarMaterial(@WebParam(name = "material") Biblioteca biblioteca) {
        int resultado = 0;
        try {
            bibBO = new BibliotecaBOImpl();
            resultado = bibBO.insertar(biblioteca);
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return resultado;
    }

    @WebMethod(operationName = "modificarBiblioteca")
    public int modificarMaterial(@WebParam(name = "material") Biblioteca biblioteca) throws Exception {
        bibBO = new BibliotecaBOImpl();
        return bibBO.modificar(biblioteca);
    }
    @WebMethod(operationName = "ListarTodas")
    public ArrayList<Biblioteca> listar_bibliotecas_todas() {
        ArrayList<Biblioteca> bibliotecas = null;
        bibBO = new BibliotecaBOImpl();
        try{
            bibliotecas = bibBO.listarTodos();
        }
        catch(Exception ex){
            System.out.println(ex.getMessage());
        }
        return bibliotecas;
    }
	
	@WebMethod(operationName = "obtenerBibliotecaPorId")
    public Biblioteca obtenerMaterialPorId(@WebParam(name = "idMaterial") int idMaterial) throws Exception {
        Biblioteca material = null;
        try {
            bibBO = new BibliotecaBOImpl();
            material = bibBO.obtenerPorId(idMaterial);
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return material;
    }

    @WebMethod(operationName = "eliminarBiblioteca")
    public int eliminarMaterial(@WebParam(name = "idMaterial") int idMaterial) throws Exception {
        int resultado = 0;
        try {
            bibBO = new BibliotecaBOImpl();
            resultado = bibBO.eliminar(idMaterial);
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return resultado;
    }
}
