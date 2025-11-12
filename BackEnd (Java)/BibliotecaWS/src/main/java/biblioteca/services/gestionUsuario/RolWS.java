

package biblioteca.services.gestionUsuario;

import biblioteca.gestionUsuario.boImpl.RolBOImpl;
import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.usuarios.model.Rol;

@WebService(serviceName = "RolWS")
public class RolWS {
    private RolBOImpl rolboimpl;

    public RolWS() {
        rolboimpl=new RolBOImpl();
    }
    
    @WebMethod(operationName = "listarRoles")
    public ArrayList<Rol> listarRoles() throws Exception {
        
        return rolboimpl.listarTodos();
    }
    @WebMethod(operationName = "insertarRol")
    public int insertarRol(@WebParam(name = "rol") Rol rol) throws Exception {
        return rolboimpl.insertar(rol);
    }
    @WebMethod(operationName = "modificarRol")
    public int modificarRol(@WebParam(name = "rol") Rol rol) throws Exception {
        return rolboimpl.modificar(rol);
    }

    @WebMethod(operationName = "eliminarRol")
    public int eliminarRol(@WebParam(name = "idRol") int idRol) throws Exception {
        return rolboimpl.eliminar(idRol);
    }

    @WebMethod(operationName = "obtenerRolPorId")
    public Rol obtenerRolPorId(@WebParam(name = "idRol") int idRol) throws Exception {
        return rolboimpl.obtenerPorId(idRol);
    }
}
