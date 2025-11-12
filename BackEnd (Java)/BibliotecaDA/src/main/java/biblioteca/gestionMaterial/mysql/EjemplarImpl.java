
package biblioteca.gestionMaterial.mysql;

import biblioteca.config.DBManager;
import biblioteca.gestionMaterial.dao.EjemplarDAO;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Biblioteca;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Ejemplar;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.EstadoEjemplar;

public class EjemplarImpl implements EjemplarDAO {
private ResultSet rs;
    @Override
    public int insertar(Ejemplar objeto) {
        Map<Integer, Object> parametrosSalida = new HashMap<>();
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosSalida.put(1, Types.INTEGER);
        parametrosEntrada.put(2, objeto.getId_material());
        parametrosEntrada.put(3, objeto.getUbicacion());
        parametrosEntrada.put(4, objeto.getBlibioteca().getIdBiblioteca());
        DBManager.getInstance().ejecutarProcedimiento("INSERTAR_Ejemplar", parametrosEntrada, parametrosSalida);
        objeto.setIdEjemplar((int) parametrosSalida.get(1));
        System.out.println("Se ha realizado el registro del ejemplar");
        return objeto.getIdEjemplar();
    }

    @Override
    public int modificar(Ejemplar objeto) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, objeto.getIdEjemplar());
        parametrosEntrada.put(2, objeto.getEstado().name());
        parametrosEntrada.put(3, objeto.getUbicacion());
        parametrosEntrada.put(4, objeto.isActivo());
        parametrosEntrada.put(5, objeto.getBlibioteca().getIdBiblioteca());
        int resultado = DBManager.getInstance().ejecutarProcedimiento("MODIFICAR_EJEMPLAR", parametrosEntrada, null);
        System.out.println("Se ha realizado la modificacion del ejemplar");
        return resultado;
    }

    @Override
    public int eliminar(int idObjeto) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idObjeto);
        int resultado = DBManager.getInstance().ejecutarProcedimiento("ELIMINAR_EJEMPLAR", parametrosEntrada, null);
        System.out.println("Se ha realizado la eliminacion del ejemplar");
        return resultado;
    }

    @Override
    public Ejemplar obtenerPorId(int idObjeto) {
        Ejemplar ejemplar = null;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idObjeto);

        ResultSet rs = DBManager.getInstance().ejecutarProcedimientoLectura("OBTENER_EJEMPLAR_X_ID", parametrosEntrada);
        System.out.println("Lectura de ejemplar...");
        try {
            if (rs.next()) {
                ejemplar = new Ejemplar();
                ejemplar.setIdEjemplar(rs.getInt("id_ejemplar"));
                ejemplar.setId_material(rs.getInt("id_material"));
                ejemplar.setUbicacion(rs.getString("ubicacion"));
                ejemplar.setActivo(rs.getBoolean("activo"));
                Biblioteca b=new Biblioteca();
                b.setIdBiblioteca(rs.getInt("id_biblioteca"));
                ejemplar.setBlibioteca(b);
                
                String estadoStr = rs.getString("estado");
                ejemplar.setEstado(EstadoEjemplar.valueOf(estadoStr.toUpperCase()));
            }
        } catch (SQLException ex) {
            System.out.println("ERROR: " + ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }
        return ejemplar;
    }

    @Override
    public ArrayList<Ejemplar> listarTodos() {
        ArrayList<Ejemplar> ejemplares = null;
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("LISTAR_EJEMPLARES_TODOS", null);
        System.out.println("Lectura de ejemplares...");
        try {
            while (rs.next()) {
                if (ejemplares == null) ejemplares = new ArrayList<>();
                Ejemplar e = new Ejemplar();

                e = new Ejemplar();
                e.setIdEjemplar(rs.getInt("id_ejemplar"));
                e.setId_material(rs.getInt("id_material"));
                e.setUbicacion(rs.getString("ubicacion"));
                e.setActivo(rs.getBoolean("activo"));
                Biblioteca b=new Biblioteca();
                b.setIdBiblioteca(rs.getInt("id_biblioteca"));
                e.setBlibioteca(b);
                
                String estadoStr = rs.getString("estado");
                e.setEstado(EstadoEjemplar.valueOf(estadoStr.toUpperCase()));
                ejemplares.add(e);
            }
        } catch (SQLException ex) {
            System.out.println("ERROR: " + ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }
        return ejemplares;
    }

    @Override
    public ArrayList<Ejemplar> listar_disponibles_por_material(int _id_material) {
        ArrayList<Ejemplar> ejemplares = null;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, _id_material);
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("LISTAR_EJEMPLARES_DISPONIBLES_POR_MATERIAL", parametrosEntrada);
        System.out.println("Lectura de ejemplares...");
        try {
            while (rs.next()) {
                if (ejemplares == null) ejemplares = new ArrayList<>();
                Ejemplar e = new Ejemplar();

                e = new Ejemplar();
                e.setIdEjemplar(rs.getInt("id_ejemplar"));
                e.setId_material(rs.getInt("id_material"));
                e.setUbicacion(rs.getString("ubicacion"));
                Biblioteca b=new Biblioteca();
                b.setIdBiblioteca(rs.getInt("id_biblioteca"));
                e.setBlibioteca(b);
                
                String estadoStr = rs.getString("estado");
                e.setEstado(EstadoEjemplar.valueOf(estadoStr.toUpperCase()));
                ejemplares.add(e);
            }
        } catch (SQLException ex) {
            System.out.println("ERROR: " + ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }
        return ejemplares;
    }
    

}
