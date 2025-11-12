
package biblioteca.gestionMaterial.boImpl;

import biblioteca.gestionMaterial.bo.ArticuloBO;
import biblioteca.gestionMaterial.dao.ArticuloDAO;
import biblioteca.gestionMaterial.mysql.Articulolmpl;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Articulo;

public class ArticuloBOImpl implements ArticuloBO{
    private final ArticuloDAO articulodao;

    public ArticuloBOImpl() {
        articulodao=new Articulolmpl();
    }
    
    @Override
    public int insertar(Articulo objeto) throws Exception {
        return articulodao.insertar(objeto);
    }

    @Override
    public int modificar(Articulo objeto) throws Exception {
        return articulodao.modificar(objeto);
    }

    @Override
    public int eliminar(int idObjeto) throws Exception {
        return articulodao.eliminar(idObjeto);
    }

    @Override
    public Articulo obtenerPorId(int idObjeto) throws Exception {
        return articulodao.obtenerPorId(idObjeto);
    }

    @Override
    public ArrayList<Articulo> listarTodos() throws Exception {
        return articulodao.listarTodos();
    }

    @Override
    public void validar(Articulo objeto) throws Exception {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }
    
}
