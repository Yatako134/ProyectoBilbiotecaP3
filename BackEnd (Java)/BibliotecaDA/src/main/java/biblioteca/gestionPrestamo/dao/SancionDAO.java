
package biblioteca.gestionPrestamo.dao;

import biblioteca.dao.IDAO;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Sancion;

public interface SancionDAO extends IDAO<Sancion>{
    ArrayList<Sancion> listarPorUsuario(int idUsuario);
    ArrayList<Sancion> listar_busqueda_usuario(int codigo_universitario);
}
