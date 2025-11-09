
package biblioteca.gestionMaterial.boImpl;

import biblioteca.gestionMaterial.bo.LibroBO;
import biblioteca.gestionMaterial.dao.LibroDAO;
import biblioteca.gestionMaterial.mysql.Librolmpl;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Libro;

public class LibroBOImpl implements LibroBO{
private final LibroDAO daoLibro;

    public LibroBOImpl() {
        daoLibro = new Librolmpl();
    }

    @Override
    public int insertar(Libro objeto) throws Exception {
        return daoLibro.insertar(objeto);
    }

    @Override
    public int modificar(Libro objeto) throws Exception {
        return daoLibro.modificar(objeto);
    }

    @Override
    public int eliminar(int idObjeto) throws Exception {
        return daoLibro.eliminar(idObjeto);
    }

    @Override
    public Libro obtenerPorId(int idObjeto) throws Exception {
        return daoLibro.obtenerPorId(idObjeto);
    }

    @Override
    public ArrayList<Libro> listarTodos() throws Exception {
        return daoLibro.listarTodos();
    }
    @Override
    public void validar(Libro objeto) throws Exception {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }
    
}
