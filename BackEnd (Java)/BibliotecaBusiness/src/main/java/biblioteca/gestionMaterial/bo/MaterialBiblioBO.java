/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Interface.java to edit this template
 */
package biblioteca.gestionMaterial.bo;

import biblioteca.ibo.IBO;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.MaterialBibliografico;

/**
 *
 * @author renat
 */
public interface MaterialBiblioBO extends IBO<MaterialBibliografico>{
    ArrayList<MaterialBibliografico> listar_busqueda(String _parametro);
}
