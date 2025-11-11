
package biblioteca.services.gestionUsuario;

import biblioteca.gestionUsuario.boImpl.UsuarioBOImpl;
import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.usuarios.model.Usuario;

@WebService(serviceName = "UsuarioWS")
public class UsuarioWS {

    private UsuarioBOImpl usuarioBO;

    public UsuarioWS() {
        usuarioBO = new UsuarioBOImpl();
    }

    @WebMethod(operationName = "insertarUsuario")
    public int insertarUsuario(@WebParam(name = "usuario") Usuario usuario) throws Exception {
        return usuarioBO.insertar(usuario);
    }

    @WebMethod(operationName = "modificarUsuario")
    public int modificarUsuario(@WebParam(name = "usuario") Usuario usuario) throws Exception {
        return usuarioBO.modificar(usuario);
    }

    @WebMethod(operationName = "eliminarUsuario")
    public int eliminarUsuario(@WebParam(name = "idUsuario") int idUsuario) throws Exception {
        return usuarioBO.eliminar(idUsuario);
    }

    @WebMethod(operationName = "obtenerUsuarioPorId")
    public Usuario obtenerUsuarioPorId(@WebParam(name = "idUsuario") int idUsuario) throws Exception {
        return usuarioBO.obtenerPorId(idUsuario);
    }

    @WebMethod(operationName = "listarUsuarios")
    public ArrayList<Usuario> listarUsuarios() throws Exception {
        return usuarioBO.listarTodos();
    }
}
