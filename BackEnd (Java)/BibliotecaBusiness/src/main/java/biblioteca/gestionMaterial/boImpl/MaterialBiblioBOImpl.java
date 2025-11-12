/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package biblioteca.gestionMaterial.boImpl;

import biblioteca.gestionMaterial.bo.MaterialBiblioBO;
import biblioteca.gestionMaterial.dao.MaterialBiblioDAO;
import biblioteca.gestionMaterial.mysql.MaterialBiblioImpl;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Contribuyente;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Editorial;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Ejemplar;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.MaterialBibliografico;

/**
 *
 * @author renat
 */
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
    public ArrayList<Editorial> buscarEditorial(int id) {
        return matDao.buscarEditorial(id);
    }

    @Override
    public ArrayList<Ejemplar> obtenerEjemplaresDisponibles(int idMaterial, int idBiblioteca) {
        return matDao.obtenerEjemplaresDisponibles(idMaterial, idBiblioteca);
    }
    @Override
    public int insertar(MaterialBibliografico objeto) throws Exception {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public int modificar(MaterialBibliografico objeto) throws Exception {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public int eliminar(int idObjeto) throws Exception {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public MaterialBibliografico obtenerPorId(int idObjeto) throws Exception {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
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
                String _tipo_material, String _biblioteca, String _disponibilidad) {
            
            
        return matDao.listar_busqueda_avanzada(_titulo, _tipo_contribuyente, _nombre_contribuyente, _tema, _fecha_desde, _fecha_hasta, _tipo_material, _biblioteca, _disponibilidad);
    }
    
        
    @Override
    public int ContarEjemplares(int idMaterial) {
        return matDao.ContarEjemplares(idMaterial);
    }

    @Override
    public ArrayList<MaterialBibliografico> listartodosnormal() {
        return matDao.listartodosnormal();
    }
}
