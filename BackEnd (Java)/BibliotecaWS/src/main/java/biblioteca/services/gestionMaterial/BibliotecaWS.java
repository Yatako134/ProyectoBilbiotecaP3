
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
}
