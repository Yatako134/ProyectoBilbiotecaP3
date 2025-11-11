
package biblioteca.gestionPrestamo.bo;

import biblioteca.ibo.IBO;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Sancion;

public interface SancionBO extends IBO<Sancion>{
    ArrayList<Sancion> listarPorUsuario(int idUsuario);
}
