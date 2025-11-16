
package biblioteca.gestionMaterial.bo;

import biblioteca.ibo.IBO;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Ejemplar;

public interface EjemplarBO extends IBO<Ejemplar>{
    ArrayList<Ejemplar> listar_disponibles_por_material(int _id_material);
    ArrayList<Ejemplar> listar_por_material(int _id_material);
}
