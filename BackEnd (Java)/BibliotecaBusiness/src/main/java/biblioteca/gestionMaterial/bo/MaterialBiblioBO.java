
package biblioteca.gestionMaterial.bo;

import biblioteca.ibo.IBO;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Contribuyente;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Editorial;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Ejemplar;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.MaterialBibliografico;

public interface MaterialBiblioBO extends IBO<MaterialBibliografico>{
    ArrayList<MaterialBibliografico> listar_busqueda(String _parametro);
    ArrayList<Ejemplar> buscarEjemplares(int id);

    // Busca contribuyentes por ID
    ArrayList<Contribuyente> buscarContribuyente(int id);

    // Busca editoriales por ID
    ArrayList<Editorial> buscarEditorial(int id);

    // Obtiene los ejemplares disponibles de un material en una biblioteca
    ArrayList<Ejemplar> obtenerEjemplaresDisponibles(int idMaterial, int idBiblioteca);
}
