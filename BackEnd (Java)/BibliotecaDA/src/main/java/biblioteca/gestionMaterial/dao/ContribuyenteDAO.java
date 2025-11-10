
package biblioteca.gestionMaterial.dao;

import biblioteca.dao.IDAO;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Contribuyente;

public interface ContribuyenteDAO extends IDAO<Contribuyente>{
    ArrayList<Contribuyente> listar_autores_por_material(int id_material);
    int asignar_contribuyente(int id_material, int id_contribuyente);
}
