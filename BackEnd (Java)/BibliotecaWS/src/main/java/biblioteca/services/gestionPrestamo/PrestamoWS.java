
package biblioteca.services.gestionPrestamo;

import biblioteca.gestionPrestamo.boImpl.PrestamoBOImpl;
import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Prestamo;

@WebService(serviceName = "PrestamoWS", targetNamespace = "pe.edu.pucp.utilsarmy.services")
public class PrestamoWS {
    
    private PrestamoBOImpl prestamoboimpl;
    
    public PrestamoWS(){
        prestamoboimpl=new PrestamoBOImpl();
    }
    private PrestamoBOImpl prestamobo;
    @WebMethod(operationName = "insertarPrestamo")
    public int insertarPrestamo(@WebParam(name = "name") Prestamo prestamo) {
        int resultado=0;
        try
        {
            prestamobo =new PrestamoBOImpl();
            resultado = prestamobo.insertar(prestamo);
        }catch(Exception ex){
            System.out.println(ex.getMessage());
        }
        return resultado;
    }
    
    @WebMethod(operationName = "modificarPrestamo")
    public int modificarPrestamo(@WebParam(name = "name") Prestamo prestamo) throws Exception {
        prestamobo =new PrestamoBOImpl();
        return prestamobo.modificar(prestamo);
    }
    
    @WebMethod(operationName = "listarPrestamos")
    public ArrayList<Prestamo> listarPrestamos() throws Exception {
        return prestamoboimpl.listarTodos();
    }
    
    @WebMethod(operationName = "listarPrestamosPorUsuario")
    public ArrayList<Prestamo> listarPrestamosPorUsuario(@WebParam(name = "idUsuario") int idUsuario) throws Exception {
        return prestamoboimpl.listarPorUsuario(idUsuario);
    }
    @WebMethod(operationName = "obtenerPrestamoPorId")
    public Prestamo obtenerPrestamoPorId(@WebParam(name="name") int idPrestamo) throws Exception {
        Prestamo empleado=null;
        try{
            prestamobo =new PrestamoBOImpl();
            empleado= prestamobo.obtenerPorId(idPrestamo);
            
        }catch(Exception ex){
            System.out.println(ex.getMessage());
        }
        return empleado;
    }
    
    @WebMethod(operationName = "eliminarPrestamo")
    public int eliminarPrestamo(@WebParam(name="name") int idEmpleado) throws Exception {
        int resultado=0;
        try
        {
            prestamobo =new PrestamoBOImpl();
            resultado=prestamobo.eliminar(idEmpleado);
            
        }catch(Exception ex){
            System.out.println(ex.getMessage());
        }
        return resultado;
    }
}
