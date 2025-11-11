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
        try {

            materiales = materialBO.listarTodos();
            return materiales;
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return materiales;
    }

    @WebMethod(operationName = "Busqueda")
    public ArrayList<MaterialBibliografico> listar_por_busqueda(@WebParam(name = "_titulo_autor") String _parametro) {
        materialBO = new MaterialBiblioBOImpl();
        ArrayList<MaterialBibliografico> materiales = null;
        try {

            materiales = materialBO.listar_busqueda(_parametro);
            return materiales;
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return materiales;
    }

    @WebMethod(operationName = "BusquedaAvanzada")
    public ArrayList<MaterialBibliografico> listar_por_busqueda_avanzada
        (@WebParam(name = "Titulo") String _titulo,
            @WebParam(name = "Tipo_contribuyente") String _tipo_contribuyente,
            @WebParam(name ="Nombre_contribuyente")String _nombre_contribuyente, 
            @WebParam(name ="Tema")String _tema,
            @WebParam(name ="Fecha_desde")Integer _fecha_desde,
            @WebParam(name ="Fecha_hasta")Integer _fecha_hasta,
            @WebParam(name ="Tipo_de_material")String _tipo_material, 
            @WebParam(name ="Biblioteca")String _biblioteca, 
            @WebParam(name ="Disponibilidad")String _disponibilidad) {
        materialBO = new MaterialBiblioBOImpl();
        ArrayList<MaterialBibliografico> materiales = null;
        try {

            materiales = materialBO.listar_busqueda_avanzada(_titulo, _tipo_contribuyente, _nombre_contribuyente, _tema, _fecha_desde, _fecha_hasta, _tipo_material, _biblioteca, _disponibilidad);
            return materiales;
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return materiales;
    }
}
