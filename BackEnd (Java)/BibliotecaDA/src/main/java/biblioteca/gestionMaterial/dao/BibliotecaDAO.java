package biblioteca.gestionMaterial.dao;

import biblioteca.dao.IDAO;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Biblioteca;

public interface BibliotecaDAO extends IDAO<Biblioteca>{
    ArrayList<Biblioteca> listar_bibliotecas_por_material(int _id_material);
}

