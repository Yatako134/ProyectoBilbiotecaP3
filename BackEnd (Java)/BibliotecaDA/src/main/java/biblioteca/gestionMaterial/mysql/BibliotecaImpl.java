
package biblioteca.gestionMaterial.mysql;

import biblioteca.config.DBManager;
import biblioteca.gestionMaterial.dao.BibliotecaDAO;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Biblioteca;



public class BibliotecaImpl implements BibliotecaDAO{
    
    private ResultSet rs;
    
    @Override
    public int insertar(Biblioteca objeto) {
        Map<Integer,Object> parametrosSalida = new HashMap<>();
        Map<Integer,Object> parametrosEntrada = new HashMap<>();
        parametrosSalida.put(1, Types.INTEGER);
        parametrosEntrada.put(2, objeto.getNombre());
        parametrosEntrada.put(3, objeto.getUbicacion());
        parametrosEntrada.put(4, objeto.isActivo());
        DBManager.getInstance().ejecutarProcedimiento("INSERTAR_BIBLIOTECA", parametrosEntrada, parametrosSalida);
        objeto.setIdBiblioteca((int) parametrosSalida.get(1));
        System.out.println("Se ha realizado el registro de la biblioteca");
        return objeto.getIdBiblioteca(); 
    }
    

    @Override
    public int modificar(Biblioteca objeto) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, objeto.getIdBiblioteca());
        parametrosEntrada.put(2, objeto.getNombre());
        parametrosEntrada.put(3, objeto.getUbicacion());
        parametrosEntrada.put(4, objeto.isActivo());
        int resultado = DBManager.getInstance().ejecutarProcedimiento("MODIFICAR_BIBLIOTECA", parametrosEntrada, null);
        System.out.println("Se ha realizado la modificacion de la biblioteca");
        return resultado;
    }

    @Override
    public int eliminar(int idObjeto) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idObjeto);
        int resultado = DBManager.getInstance().ejecutarProcedimiento("ELIMINAR_BIBLIOTECA", parametrosEntrada, null);
        System.out.println("Se ha realizado la eliminacion de la biblioteca");
        return resultado;
    }

    @Override
    public Biblioteca obtenerPorId(int idObjeto) {
        Biblioteca biblioteca = null;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idObjeto);
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("OBTENER_BIBLIOTECA_X_ID", parametrosEntrada);
        System.out.println("Lectura de biblioteca...");
        try{
            if(rs.next()){
                biblioteca = new Biblioteca();
                biblioteca.setIdBiblioteca(rs.getInt("id_biblioteca"));
                biblioteca.setNombre(rs.getString("nombre"));
                biblioteca.setUbicacion(rs.getString("ubicacion"));
                biblioteca.setActivo(rs.getBoolean("activo"));
                
            }
        }catch(SQLException ex){
            System.out.println("ERROR: " + ex.getMessage());
        }finally{
            DBManager.getInstance().cerrarConexion();
        }
        return biblioteca;
    }
    
    @Override
    public ArrayList<Biblioteca> listarTodos() {
        ArrayList<Biblioteca> bibliotecas = null;
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("LISTAR_BIBLIOTECAS_TODAS", null);
        System.out.println("Lectura de todas las bibliotecas...");
        try{
            while(rs.next()){
                if(bibliotecas == null) bibliotecas = new ArrayList<>();
                Biblioteca bio = new Biblioteca();
                bio.setIdBiblioteca(rs.getInt("id_biblioteca"));
                bio.setNombre(rs.getString("nombre"));
                bio.setUbicacion(rs.getString("ubicacion"));
                bio.setActivo(rs.getBoolean("activo"));
                bibliotecas.add(bio);
            }
        }catch(SQLException ex){
            System.out.println("ERROR: " + ex.getMessage());
        }finally{
            DBManager.getInstance().cerrarConexion();
        }
        return bibliotecas;
    }
    
}
