
package biblioteca.gestionUsuario.dao;

import biblioteca.dao.IDAO;
import pe.edu.pucp.utilsarmy.usuarios.model.Usuario;

public interface UsuarioDAO extends IDAO<Usuario>{
<<<<<<< HEAD
     Usuario obtenerUsuarioxCodigo(int codigo);
=======
    Usuario obtenerUsuarioxCodigo(int codigo);
>>>>>>> dbafd2528623e73d02b773112e815ec8c2a853d0
    int prestamos_vigentesxUsuario(int idUsuario);
}
