
package biblioteca.gestionMaterial.dao;
import biblioteca.dao.IDAO;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Ejemplar;

public interface EjemplarDAO extends IDAO<Ejemplar>{
    ArrayList<Ejemplar> listar_disponibles_por_material(int _id_material);
    
    ArrayList<Ejemplar> listar_por_material(int _id_material);
}
