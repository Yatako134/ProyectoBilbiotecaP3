
package biblioteca.gestionPrestamo.boImpl;

import biblioteca.gestionPrestamo.bo.SancionBO;
import biblioteca.gestionPrestamo.dao.SancionDAO;
import biblioteca.gestionPrestamo.mysql.SancionImpl;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Sancion;

public class SancionBOImpl implements SancionBO{
private final SancionDAO daoSancion;

    public SancionBOImpl() {
        daoSancion = new SancionImpl();
    }

    @Override
    public int insertar(Sancion objeto) throws Exception {
        return daoSancion.insertar(objeto);
    }

    @Override
    public int modificar(Sancion objeto) throws Exception {
        return daoSancion.modificar(objeto);
    }

    @Override
    public int eliminar(int idObjeto) throws Exception {
        return daoSancion.eliminar(idObjeto);
    }

    @Override
    public Sancion obtenerPorId(int idObjeto) throws Exception {
        return daoSancion.obtenerPorId(idObjeto);
    }

    @Override
    public ArrayList<Sancion> listarTodos() throws Exception {
        return daoSancion.listarTodos();
    }
    @Override
    public void validar(Sancion objeto) throws Exception {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public ArrayList<Sancion> listarPorUsuario(int idUsuario) {
        return daoSancion.listarPorUsuario(idUsuario);
    }
    
}
