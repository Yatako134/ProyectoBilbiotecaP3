
package biblioteca.gestionPrestamo.bo;

import biblioteca.ibo.IBO;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Prestamo;

public interface PrestamoBO extends IBO<Prestamo>{
    ArrayList<Prestamo> listarPorUsuario(int idUsuario);
    ArrayList<Prestamo> listar_busqueda_usuario(int codigo_universitario);
    ArrayList<Prestamo> listarPorUsuario_X_ID(int idUsuario,String filtro);
    ArrayList<Prestamo> UsuariosProximoAVencerse();
    int MarcarRecordatorioPrestamos();
}
