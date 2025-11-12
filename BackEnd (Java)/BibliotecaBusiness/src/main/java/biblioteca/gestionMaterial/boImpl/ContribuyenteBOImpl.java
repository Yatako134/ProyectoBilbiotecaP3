
package biblioteca.gestionMaterial.boImpl;

import biblioteca.gestionMaterial.bo.ContribuyenteBO;
import biblioteca.gestionMaterial.dao.ContribuyenteDAO;
import biblioteca.gestionMaterial.mysql.ContribuyenteImpl;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Contribuyente;

public class ContribuyenteBOImpl implements ContribuyenteBO{
    ContribuyenteDAO contDao;

    public ContribuyenteBOImpl() {
        contDao = new ContribuyenteImpl();
    }
    
    @Override
    public int insertar(Contribuyente objeto) throws Exception {
        return contDao.insertar(objeto);
    }

    @Override
    public int modificar(Contribuyente objeto) throws Exception {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public int eliminar(int idObjeto) throws Exception {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public Contribuyente obtenerPorId(int idObjeto) throws Exception {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public ArrayList<Contribuyente> listarTodos() throws Exception {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public void validar(Contribuyente objeto) throws Exception {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public ArrayList<Contribuyente> listar_autores_por_material(int id_material) {
        return contDao.listar_autores_por_material(id_material);
    }

    @Override
    public int asignar_contribuyente(int id_material, int id_contribuyente) {
        return contDao.asignar_contribuyente(id_material, id_contribuyente);
    }
    
}
