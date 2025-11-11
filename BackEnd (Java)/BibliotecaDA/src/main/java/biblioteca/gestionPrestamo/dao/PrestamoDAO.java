
package biblioteca.gestionPrestamo.dao;

import biblioteca.dao.IDAO;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Prestamo;

public interface PrestamoDAO extends IDAO<Prestamo>{
    ArrayList<Prestamo> listarPorUsuario(int idUsuario);
}
