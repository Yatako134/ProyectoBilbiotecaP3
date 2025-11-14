
package biblioteca.services.gestionUsuario;

import biblioteca.gestionUsuario.boImpl.UsuarioBOImpl;
import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.usuarios.model.Usuario;

@WebService(serviceName = "UsuarioWS", targetNamespace = "pe.edu.pucp.utilsarmy.services")
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
    
    @WebMethod(operationName = "obtenerUsuarioxCodigo")
    public Usuario obtenerUsuarioxCodigo(int codigo) throws Exception {
        return usuarioBO.obtenerUsuarioxCodigo(codigo);
    }
    
    @WebMethod(operationName = "obtener_prestamos_vigentesxUsuario")
    public int obtener_prestamos_vigentesxUsuario(int codigo) throws Exception {
        return usuarioBO.prestamos_vigentesxUsuario(codigo);
    }
    
    @WebMethod(operationName = "verificarCuenta")
    public int verificarCuenta(@WebParam(name = "cuenta") Usuario usuario) throws Exception {
        int resultado=0;
        try{
            usuarioBO=new UsuarioBOImpl();
            resultado=usuarioBO.verificar(usuario);
        }catch(Exception ex){
            System.out.println(ex.getMessage());
        }
        return resultado;
    }
}
