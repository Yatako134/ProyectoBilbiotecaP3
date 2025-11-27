
package biblioteca.gestionMaterial.boImpl;

import biblioteca.gestionMaterial.bo.MaterialBiblioBO;
import biblioteca.gestionMaterial.dao.MaterialBiblioDAO;
import biblioteca.gestionMaterial.mysql.MaterialBiblioImpl;
import java.util.ArrayList;
import java.util.Date;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Contribuyente;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Ejemplar;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.MaterialBibliografico;

public class MaterialBiblioBOImpl implements MaterialBiblioBO{
    MaterialBiblioDAO matDao;
    public MaterialBiblioBOImpl(){
        matDao = new MaterialBiblioImpl();
    }
	
	@Override
    public ArrayList<Ejemplar> buscarEjemplares(int id) {
//        validar(objeto);
        return matDao.buscarEjemplares(id);
    }

    @Override
    public ArrayList<Contribuyente> buscarContribuyente(int id) {
        
        return matDao.buscarContribuyente(id);
    }


    @Override
    public ArrayList<Ejemplar> obtenerEjemplaresDisponibles(int idMaterial, int idBiblioteca) {
        return matDao.obtenerEjemplaresDisponibles(idMaterial, idBiblioteca);
    }
    @Override
    public int insertar(MaterialBibliografico objeto) throws Exception {
        return matDao.insertar(objeto);
    }

    @Override
    public int modificar(MaterialBibliografico objeto) throws Exception {
       return matDao.modificar(objeto);
    }

    @Override
    public int eliminar(int idObjeto) throws Exception {
        return matDao.eliminar(idObjeto);
    }

    @Override
    public MaterialBibliografico obtenerPorId(int idObjeto) throws Exception {
        return matDao.obtenerPorId(idObjeto);
    }

    @Override
    public ArrayList<MaterialBibliografico> listarTodos() throws Exception {
        return matDao.listarTodos();
    }

    @Override
    public void validar(MaterialBibliografico objeto) throws Exception {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public ArrayList<MaterialBibliografico> listar_busqueda(String _parametro) {
        return matDao.listar_busqueda(_parametro);
    }

    @Override
    public ArrayList<MaterialBibliografico> listar_busqueda_avanzada
        (String _titulo, String _tipo_contribuyente, 
                String _nombre_contribuyente, String _tema, 
                Integer _fecha_desde, Integer _fecha_hasta, 
                String _tipo_material, String _biblioteca, String _disponibilidad,
                String _editoriales) {
            
            
        return matDao.listar_busqueda_avanzada(_titulo, _tipo_contribuyente, _nombre_contribuyente, _tema, _fecha_desde, _fecha_hasta, _tipo_material, _biblioteca, _disponibilidad,
                _editoriales);
    }
    
        
    @Override
    public int ContarEjemplares(int idMaterial) {
        return matDao.ContarEjemplares(idMaterial);
    }

    @Override
    public ArrayList<MaterialBibliografico> listartodosnormal() {
        return matDao.listartodosnormal();
    }

    @Override
    public MaterialBibliografico obtener_por_id_solo_material(int idMaterial) {
        return matDao.obtener_por_id_solo_material(idMaterial);
    }

    @Override
    public int ContarMaterialesPrestadosRango(Date fechaInicio, Date fechaFin) {
        return matDao.ContarMaterialesPrestadosRango(fechaInicio, fechaFin);
    }
}
