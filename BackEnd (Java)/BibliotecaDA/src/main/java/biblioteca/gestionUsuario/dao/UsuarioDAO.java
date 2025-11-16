
package biblioteca.gestionUsuario.dao;

import biblioteca.dao.IDAO;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.usuarios.model.Usuario;

public interface UsuarioDAO extends IDAO<Usuario>{
    Usuario obtenerUsuarioxCodigo(int codigo);
    int prestamos_vigentesxUsuario(int idUsuario);
    int verificar(Usuario usuario);
     ArrayList<Usuario> listarPorPanelBusqueda(String filtro);
}
