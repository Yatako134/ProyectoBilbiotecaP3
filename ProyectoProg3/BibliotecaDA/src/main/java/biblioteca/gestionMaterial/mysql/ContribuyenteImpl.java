
package biblioteca.gestionMaterial.mysql;

<<<<<<< HEAD
import biblioteca.gestionMaterial.dao.ContribuyenteDAO;
import biblioteca.config.DBManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Contribuyente;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.TipoContribuyente;

public class ContribuyenteImpl implements ContribuyenteDAO{
    
    private ResultSet rs;
    
    @Override
    public int insertar(Contribuyente contribuyente) {
        Map<Integer,Object> parametrosSalida = new HashMap<>();   
        Map<Integer,Object> parametrosEntrada = new HashMap<>();
        parametrosSalida.put(1, Types.INTEGER);
        parametrosEntrada.put(2, contribuyente.getNombre());
        parametrosEntrada.put(3, contribuyente.getPrimer_apellido());
        parametrosEntrada.put(4, contribuyente.getSegundo_apellido());
        parametrosEntrada.put(5, contribuyente.getSeudonimo());
        parametrosEntrada.put(6, contribuyente.getTipo_contribuyente());
        parametrosEntrada.put(7, contribuyente.getId_material());
        DBManager.getInstance().ejecutarProcedimiento("INSERTAR_CONTRIBUYENTE", parametrosEntrada, parametrosSalida);
        contribuyente.setIdContribuyente((int) parametrosSalida.get(1));
        System.out.println("Se ha realizado el registro del contribuyente");
        return contribuyente.getIdContribuyente();
    }
    
    @Override
    public int modificar(Contribuyente contribuyente) {
        Map<Integer,Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1,contribuyente.getIdContribuyente());
        parametrosEntrada.put(2, contribuyente.getNombre());
        parametrosEntrada.put(3, contribuyente.getPrimer_apellido());
        parametrosEntrada.put(4, contribuyente.getSegundo_apellido());
        parametrosEntrada.put(5, contribuyente.getSeudonimo());
        parametrosEntrada.put(6, contribuyente.getTipo_contribuyente());
        parametrosEntrada.put(7, contribuyente.getId_material());
        int resultado = DBManager.getInstance().ejecutarProcedimiento("MODIFICAR_CONTRIBUYENTE", parametrosEntrada, null);
        System.out.println("Se ha realizado la modificacion del contribuyente");
        return resultado;
    }
    
    @Override
    public int eliminar(int idContribuyente) {
        Map<Integer,Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idContribuyente);
        int resultado = DBManager.getInstance().ejecutarProcedimiento("ELIMINAR_CONTRIBUYENTE", parametrosEntrada, null);
        System.out.println("Se ha realizado la eliminacion del contribuyente");
        return resultado;
    }
    
    @Override
    public Contribuyente obtenerPorId(int idContribuyente) {
        Contribuyente contribuyente = null;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idContribuyente);
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("OBTENER_CONTRIBUYENTE_X_ID", parametrosEntrada);
        System.out.println("Lectura de contribuyente...");
        try{
            while(rs.next()){
                if(contribuyente == null) contribuyente = new Contribuyente();
                contribuyente.setIdContribuyente(rs.getInt("id_contribuyente"));
                contribuyente.setNombre(rs.getString("nombre"));
                contribuyente.setPrimer_apellido(rs.getString("primer_apellido"));
                contribuyente.setSegundo_apellido(rs.getString("segundo_apellido"));
                contribuyente.setSeudonimo(rs.getString("seudonimo"));
                contribuyente.setTipo_contribuyente(TipoContribuyente.valueOf(rs.getString("tipo_contribuyente")));
                contribuyente.setId_material(rs.getInt("id_material"));
            }
        }catch(SQLException ex){
            System.out.println(ex.getMessage());
        }finally{
            DBManager.getInstance().cerrarConexion();
        }
        return contribuyente;
    }
    
    
    @Override
    public ArrayList<Contribuyente> listarTodos() {
        ArrayList<Contribuyente> contribuyentes = null;
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("LISTAR_CONTRIBUYENTES_TODOS", null);
        System.out.println("Lectura de contribuyentes...");
        try{
            while(rs.next()){
                if(contribuyentes == null) contribuyentes = new ArrayList<>();
                Contribuyente e = new Contribuyente();
                e.setIdContribuyente(rs.getInt("id_contribuyente"));
                e.setNombre(rs.getString("nombre"));
                e.setPrimer_apellido(rs.getString("primer_apellido"));
                e.setSegundo_apellido(rs.getString("segundo_apellido"));
                e.setSeudonimo(rs.getString("seudonimo"));
                e.setTipo_contribuyente(TipoContribuyente.valueOf(rs.getString("tipo_contribuyente")));
                e.setId_material(rs.getInt("id_material"));
                contribuyentes.add(e);
            }
        }catch(SQLException ex){
            System.out.println(ex.getMessage());
        }finally{
            DBManager.getInstance().cerrarConexion();
        }
        return contribuyentes;
    }
=======
public class ContribuyenteImpl {
>>>>>>> e7cbe2292fd3facda28e5c7f4ace312400b5509f
    
}
