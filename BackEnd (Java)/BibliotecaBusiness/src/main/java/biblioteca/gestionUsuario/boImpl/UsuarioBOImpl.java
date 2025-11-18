
package biblioteca.gestionUsuario.boImpl;

import biblioteca.gestionUsuario.bo.UsuarioBO;
import biblioteca.gestionUsuario.dao.UsuarioDAO;
import biblioteca.gestionUsuario.mysql.UsuarioImpl;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Prestamo;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Sancion;
import pe.edu.pucp.utilsarmy.usuarios.model.Usuario;

public class UsuarioBOImpl implements UsuarioBO{
    private final UsuarioDAO daoUsuario;

    public UsuarioBOImpl() {
        daoUsuario = new UsuarioImpl();
    }

    @Override
    public int insertar(Usuario objeto) throws Exception {
        return daoUsuario.insertar(objeto);
    }

    @Override
    public int modificar(Usuario objeto) throws Exception {
        return daoUsuario.modificar(objeto);
    }

    @Override
    public int eliminar(int idObjeto) throws Exception {
        return daoUsuario.eliminar(idObjeto);
    }

    @Override
    public Usuario obtenerPorId(int idObjeto) throws Exception {
        return daoUsuario.obtenerPorId(idObjeto);
    }

    @Override
    public ArrayList<Usuario> listarTodos() throws Exception {
        return daoUsuario.listarTodos();
    }
    @Override
    public void validar(Usuario objeto) throws Exception {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }
    @Override
    public Usuario obtenerUsuarioxCodigo(int codigo) {
        return daoUsuario.obtenerUsuarioxCodigo(codigo);
    }

    @Override
    public int prestamos_vigentesxUsuario(int idUsuario) {
        
        return daoUsuario.prestamos_vigentesxUsuario(idUsuario);
    }

    @Override
    public int verificar(Usuario usuario) {
        return daoUsuario.verificar(usuario);
    }

    @Override
    public ArrayList<Usuario> listarPorPanelBusqueda(String filtro) {
        return daoUsuario.listarPorPanelBusqueda(filtro);
    }
    
    @Override
    public ArrayList<Usuario> listarTodosDelSistema(){
        return daoUsuario.listarTodosDelSistema();
    }

    @Override
    public Sancion obtener_sancion_usuario(int id_usuario) {
        return daoUsuario.obtener_sancion_usuario(id_usuario);
    }

    @Override
    public ArrayList<Prestamo> obtenerPrestamosRetrasados(int id_usuario) {
        return daoUsuario.obtenerPrestamosRetrasados(id_usuario);
    }
}
