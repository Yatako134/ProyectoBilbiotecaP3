
package biblioteca.gestionPrestamo.boImpl;

import biblioteca.gestionPrestamo.bo.PrestamoBO;
import biblioteca.gestionPrestamo.dao.PrestamoDAO;
import biblioteca.gestionPrestamo.mysql.PrestamoImpl;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Prestamo;

public class PrestamoBOImpl implements PrestamoBO{
    private final PrestamoDAO daoPrestamo;

    public PrestamoBOImpl() {
        daoPrestamo = new PrestamoImpl();
    }

    @Override
    public int insertar(Prestamo objeto) throws Exception {
        return daoPrestamo.insertar(objeto);
    }

    @Override
    public int modificar(Prestamo objeto) throws Exception {
        return daoPrestamo.modificar(objeto);
    }

    @Override
    public int eliminar(int idObjeto) throws Exception {
        return daoPrestamo.eliminar(idObjeto);
    }

    @Override
    public Prestamo obtenerPorId(int idObjeto) throws Exception {
        return daoPrestamo.obtenerPorId(idObjeto);
    }

    @Override
    public ArrayList<Prestamo> listarTodos() throws Exception {
        return daoPrestamo.listarTodos();
    }

    @Override
    public void validar(Prestamo objeto) throws Exception {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public ArrayList<Prestamo> listarPorUsuario(int idUsuario) {
        return daoPrestamo.listarPorUsuario(idUsuario);
    }
    
}
