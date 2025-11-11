
package biblioteca.services.gestionPrestamo;

import biblioteca.gestionPrestamo.boImpl.PrestamoBOImpl;
import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Prestamo;

@WebService(serviceName = "PrestamoWS")
public class PrestamoWS {
    
    private PrestamoBOImpl prestamoboimpl;
    
    public PrestamoWS(){
        prestamoboimpl=new PrestamoBOImpl();
    }

    @WebMethod(operationName = "listarPrestamos")
    public ArrayList<Prestamo> listarPrestamos() throws Exception {
        return prestamoboimpl.listarTodos();
    }
    
    @WebMethod(operationName = "listarPrestamosPorUsuario")
    public ArrayList<Prestamo> listarPrestamosPorUsuario(@WebParam(name = "idUsuario") int idUsuario) throws Exception {
        return prestamoboimpl.listarPorUsuario(idUsuario);
    }
    
}
