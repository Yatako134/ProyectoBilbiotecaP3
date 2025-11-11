
package biblioteca.services.gestionMaterial;
import biblioteca.gestionMaterial.bo.MaterialBiblioBO;
import biblioteca.gestionMaterial.boImpl.MaterialBiblioBOImpl;
import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Contribuyente;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Editorial;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Ejemplar;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.MaterialBibliografico;

@WebService(serviceName = "MaterialWS")
public class MaterialWS {

    MaterialBiblioBOImpl materialBO;

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
	
	@WebMethod(operationName = "obtenerPorId")
    public MaterialBibliografico obtenerPorId(@WebParam(name = "id") int id) {
        MaterialBibliografico material = null;
        try {
            material = bomaterial.obtenerPorId(id);
        } catch (Exception ex) {
            System.out.println("ERROR en obtenerPorId: " + ex.getMessage());
        }
        return material;
    }

    @WebMethod(operationName = "buscarEjemplares")
    public ArrayList<Ejemplar> buscarEjemplares(@WebParam(name = "idMaterial") int id) {
        ArrayList<Ejemplar> ejemplares = new ArrayList<>();
        try {
            ejemplares = bomaterial.buscarEjemplares(id);
            if (ejemplares == null) {
                ejemplares = new ArrayList<>();
            }
        } catch (Exception ex) {
            System.out.println("ERROR en buscarEjemplares: " + ex.getMessage());
        }
        return ejemplares;
    }

    @WebMethod(operationName = "buscarContribuyentes")
    public ArrayList<Contribuyente> buscarContribuyentes(@WebParam(name = "idMaterial") int id) {
        ArrayList<Contribuyente> contribuyentes = new ArrayList<>();
        try {
            contribuyentes = bomaterial.buscarContribuyente(id);
            if (contribuyentes == null) {
                contribuyentes = new ArrayList<>();
            }
        } catch (Exception ex) {
            System.out.println("ERROR en buscarContribuyentes: " + ex.getMessage());
        }
        return contribuyentes;
    }

    @WebMethod(operationName = "buscarEditoriales")
    public ArrayList<Editorial> buscarEditoriales(@WebParam(name = "idMaterial") int id) {
         ArrayList<Editorial> editoriales = new ArrayList<>();
        try {
            editoriales = bomaterial.buscarEditorial(id);
            if (editoriales == null) {
                editoriales = new ArrayList<>();
            }
        } catch (Exception ex) {
            System.out.println("ERROR en buscarEditoriales: " + ex.getMessage());
        }
        return editoriales;
    }

    @WebMethod(operationName = "obtenerEjemplaresDisponibles")
    public ArrayList<Ejemplar> obtenerEjemplaresDisponibles(
            @WebParam(name = "idMaterial") int idMaterial,
            @WebParam(name = "idBiblioteca") int idBiblioteca) {
         ArrayList<Ejemplar> ejemplares = new ArrayList<>();
        try {
            ejemplares = bomaterial.obtenerEjemplaresDisponibles(idMaterial, idBiblioteca);
            if (ejemplares == null) {
                ejemplares = new ArrayList<>();
            }
        } catch (Exception ex) {
            System.out.println("ERROR en obtenerEjemplaresDisponibles: " + ex.getMessage());
        }
        return ejemplares;
    }

}
