
package biblioteca.services.gestionPrestamo;

import biblioteca.gestionPrestamo.boImpl.SancionBOImpl;
import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Sancion;

@WebService(serviceName = "SancionWS")
public class SancionWS {
    
    private SancionBOImpl sancionboimpl;
    
    public SancionWS(){
        sancionboimpl=new SancionBOImpl();
    }
    
    @WebMethod(operationName = "listarSanciones")
    public ArrayList<Sancion> listarSanciones() throws Exception {
        return sancionboimpl.listarTodos();
    }

    @WebMethod(operationName = "listarSancionesPorUsuario")
    public ArrayList<Sancion> listarSancionesPorUsuario(@WebParam(name = "idUsuario") int idUsuario) throws Exception {
        return sancionboimpl.listarPorUsuario(idUsuario);
    }    
}
