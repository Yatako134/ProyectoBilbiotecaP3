/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/WebServices/WebService.java to edit this template
 */
package biblioteca.services.gestionMaterial;

import biblioteca.gestionMaterial.boImpl.MaterialBiblioBOImpl;
import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.MaterialBibliografico;

/**
 *
 * @author renat
 */
@WebService(serviceName = "MaterialWS")
public class MaterialWS {
    MaterialBiblioBOImpl materialBO;
    /**
     * This is a sample web service operation
     */
     @WebMethod(operationName = "ListarTodos")
    public ArrayList<MaterialBibliografico> listarTodos() {
        materialBO = new MaterialBiblioBOImpl();
        ArrayList<MaterialBibliografico> materiales = null;
        try{
        
        materiales = materialBO.listarTodos();
        return materiales;
        }
        catch(Exception ex){
            System.out.println(ex.getMessage());
        }
        return materiales;
    }
    @WebMethod(operationName = "Busqueda")
    public ArrayList<MaterialBibliografico> listar_por_busqueda(@WebParam(name ="_titulo_autor")
    String _parametro) {
        materialBO = new MaterialBiblioBOImpl();
        ArrayList<MaterialBibliografico> materiales = null;
        try{
        
        materiales = materialBO.listar_busqueda(_parametro);
        return materiales;
        }
        catch(Exception ex){
            System.out.println(ex.getMessage());
        }
        return materiales;
    }
}
