
package biblioteca.gestionPrestamo.bo;

import biblioteca.ibo.IBO;
import java.util.ArrayList;
import java.util.Date;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Sancion;

public interface SancionBO extends IBO<Sancion>{
    ArrayList<Sancion> listarPorUsuario(int idUsuario);
    ArrayList<Sancion> listar_busqueda_usuario(int codigo_universitario);
    ArrayList<Sancion> listarPorUsuario_X_ID(int idUsuario,String filtro);
    int contarSancionesPorFechas(Date fechaInicio, Date fechaFin);

}
