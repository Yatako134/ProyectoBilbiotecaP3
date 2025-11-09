
package biblioteca.gestionUsuario.boImpl;

import biblioteca.gestionUsuario.bo.RolBO;
import biblioteca.gestionUsuario.dao.RolDAO;
import biblioteca.gestionUsuario.mysql.RolImpl;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.usuarios.model.Rol;

public class RolBOImpl implements RolBO{
    private final RolDAO daoRol;

    public RolBOImpl() {
        daoRol = new RolImpl();
    }

    @Override
    public int insertar(Rol objeto) throws Exception {
        return daoRol.insertar(objeto);
    }

    @Override
    public int modificar(Rol objeto) throws Exception {
        return daoRol.modificar(objeto);
    }

    @Override
    public int eliminar(int idObjeto) throws Exception {
        return daoRol.eliminar(idObjeto);
    }

    @Override
    public Rol obtenerPorId(int idObjeto) throws Exception {
        return daoRol.obtenerPorId(idObjeto);
    }

    @Override
    public ArrayList<Rol> listarTodos() throws Exception {
        return daoRol.listarTodos();
    }
    @Override
    public void validar(Rol objeto) throws Exception {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }
    
}
