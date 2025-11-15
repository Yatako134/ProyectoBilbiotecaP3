
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
        return contDao.modificar(objeto);
    }

    @Override
    public int eliminar(int idObjeto) throws Exception {
        return contDao.eliminar(idObjeto);
    }

    @Override
    public Contribuyente obtenerPorId(int idObjeto) throws Exception {
        return contDao.obtenerPorId(idObjeto);
    }

    @Override
    public ArrayList<Contribuyente> listarTodos() throws Exception {
        return contDao.listarTodos();
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

    @Override
    public int eliminar_relacion_material_contribuyente(int id_material, int id_contribuyente) {
        return contDao.eliminar_relacion_material_contribuyente(id_material,id_contribuyente);
    }

    @Override
    public boolean tiene_otras_relaciones(int id_contribuyente, int id_material_excluir) {
        return contDao.tiene_otras_relaciones(id_contribuyente,id_material_excluir);
    }

    @Override
    public ArrayList<Contribuyente> listar_contribuyentes_por_material(int id_material) {
        return contDao.listar_contribuyentes_por_material(id_material);
    }
    
}
