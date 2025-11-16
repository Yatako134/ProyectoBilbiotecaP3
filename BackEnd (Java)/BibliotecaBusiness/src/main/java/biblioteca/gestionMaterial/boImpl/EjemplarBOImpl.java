
package biblioteca.gestionMaterial.boImpl;

import biblioteca.gestionMaterial.bo.EjemplarBO;
import biblioteca.gestionMaterial.dao.EjemplarDAO;
import biblioteca.gestionMaterial.mysql.EjemplarImpl;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Ejemplar;

public class EjemplarBOImpl implements EjemplarBO{
private final EjemplarDAO daoEjemplar;

    public EjemplarBOImpl() {
        daoEjemplar = new EjemplarImpl();
    }

    @Override
    public int insertar(Ejemplar objeto) throws Exception {
        return daoEjemplar.insertar(objeto);
    }

    @Override
    public int modificar(Ejemplar objeto) throws Exception {
        return daoEjemplar.modificar(objeto);
    }

    @Override
    public int eliminar(int idObjeto) throws Exception {
        return daoEjemplar.eliminar(idObjeto);
    }

    @Override
    public Ejemplar obtenerPorId(int idObjeto) throws Exception {
        return daoEjemplar.obtenerPorId(idObjeto);
    }

    @Override
    public ArrayList<Ejemplar> listarTodos() throws Exception {
        return daoEjemplar.listarTodos();
    }

    @Override
    public void validar(Ejemplar objeto) throws Exception {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public ArrayList<Ejemplar> listar_disponibles_por_material(int _id_material) {
        return daoEjemplar.listar_disponibles_por_material(_id_material);
    }

    @Override
    public ArrayList<Ejemplar> listar_por_material(int _id_material) {
        return daoEjemplar.listar_por_material(_id_material);
    }
    
}
