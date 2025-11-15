
package biblioteca.gestionMaterial.dao;

import biblioteca.dao.IDAO;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Contribuyente;

public interface ContribuyenteDAO extends IDAO<Contribuyente>{
    ArrayList<Contribuyente> listar_autores_por_material(int id_material);
    int asignar_contribuyente(int id_material, int id_contribuyente);
    int eliminar_relacion_material_contribuyente(int id_material, int id_contribuyente);
    boolean tiene_otras_relaciones(int id_contribuyente, int id_material_excluir);
    ArrayList<Contribuyente> listar_contribuyentes_por_material(int id_material);
}
