
package biblioteca.gestionMaterial.boImpl;

import biblioteca.gestionMaterial.bo.TesisBO;
import biblioteca.gestionMaterial.dao.TesisDAO;
import biblioteca.gestionMaterial.mysql.Tesislmpl;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Tesis;

public class TesisBOImpl implements TesisBO{
private final TesisDAO daoTesis;

    public TesisBOImpl() {
        daoTesis = new Tesislmpl();
    }

    @Override
    public int insertar(Tesis objeto) throws Exception {
        return daoTesis.insertar(objeto);
    }

    @Override
    public int modificar(Tesis objeto) throws Exception {
        return daoTesis.modificar(objeto);
    }

    @Override
    public int eliminar(int idObjeto) throws Exception {
        return daoTesis.eliminar(idObjeto);
    }

    @Override
    public Tesis obtenerPorId(int idObjeto) throws Exception {
        return daoTesis.obtenerPorId(idObjeto);
    }

    @Override
    public ArrayList<Tesis> listarTodos() throws Exception {
        return daoTesis.listarTodos();
    }

    @Override
    public void validar(Tesis objeto) throws Exception {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }
    
}
