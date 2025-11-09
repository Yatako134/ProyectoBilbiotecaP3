
package biblioteca.gestionMaterial.boImpl;

import biblioteca.gestionMaterial.bo.BibliotecaBO;
import biblioteca.gestionMaterial.dao.BibliotecaDAO;
import biblioteca.gestionMaterial.mysql.BibliotecaImpl;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Biblioteca;

public class BibliotecaBOImpl implements BibliotecaBO{
    private final BibliotecaDAO daobiblioteca;

    public BibliotecaBOImpl() {
        daobiblioteca=new BibliotecaImpl();
    }

    @Override
    public int insertar(Biblioteca objeto) throws Exception {
        return daobiblioteca.insertar(objeto);
    }

    @Override
    public int modificar(Biblioteca objeto) throws Exception {
        return daobiblioteca.modificar(objeto);
    }

    @Override
    public int eliminar(int idObjeto) throws Exception {
        return daobiblioteca.eliminar(idObjeto);
    }

    @Override
    public Biblioteca obtenerPorId(int idObjeto) throws Exception {
        return daobiblioteca.obtenerPorId(idObjeto);
    }

    @Override
    public ArrayList<Biblioteca> listarTodos() throws Exception {
        return daobiblioteca.listarTodos();
    }

    @Override
    public void validar(Biblioteca objeto) throws Exception {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }
    
}
