
package biblioteca.gestionUsuario.dao;

import biblioteca.dao.IDAO;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Prestamo;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Sancion;
import pe.edu.pucp.utilsarmy.usuarios.model.Usuario;

public interface UsuarioDAO extends IDAO<Usuario>{
    Usuario obtenerUsuarioxCodigo(int codigo);
    int prestamos_vigentesxUsuario(int idUsuario);
    int verificar(Usuario usuario);
     ArrayList<Usuario> listarPorPanelBusqueda(String filtro);
     ArrayList<Usuario> listarTodosDelSistema();
     Sancion obtener_sancion_usuario(int id_usuario);
     ArrayList<Prestamo> obtenerPrestamosRetrasados(int id_usuario);
     int verificarCorreoExistente(String correo);
     int modificarContrasena(int idUsuario, String nuevaContrasena);
}
