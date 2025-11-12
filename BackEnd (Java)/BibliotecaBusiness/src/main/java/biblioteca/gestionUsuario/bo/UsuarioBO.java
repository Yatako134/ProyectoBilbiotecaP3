
package biblioteca.gestionUsuario.bo;

import biblioteca.ibo.IBO;
import pe.edu.pucp.utilsarmy.usuarios.model.Usuario;

public interface UsuarioBO extends IBO<Usuario>{
    Usuario obtenerUsuarioxCodigo(int codigo);
    int prestamos_vigentesxUsuario(int idUsuario);
    int verificar(Usuario usuario);
}
