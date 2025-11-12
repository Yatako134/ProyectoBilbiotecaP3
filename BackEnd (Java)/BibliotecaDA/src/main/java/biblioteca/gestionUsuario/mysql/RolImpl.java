
package biblioteca.gestionUsuario.mysql;

import biblioteca.config.DBManager;
import biblioteca.gestionUsuario.dao.RolDAO;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import pe.edu.pucp.utilsarmy.usuarios.model.Rol;

public class RolImpl implements RolDAO{
    private ResultSet rs;
    
    @Override
    public int insertar(Rol objeto) {
        Map<Integer,Object> parametrosSalida = new HashMap<>();
        Map<Integer,Object> parametrosEntrada = new HashMap<>();
        parametrosSalida.put(1, Types.INTEGER);
        parametrosEntrada.put(2, objeto.getTipo());
        DBManager.getInstance().ejecutarProcedimiento("INSERTAR_ROL", parametrosEntrada, parametrosSalida);
        objeto.setId_rol((int) parametrosSalida.get(1));
        System.out.println("Se ha realizado el registro del rol");
        return objeto.getId_rol(); 
    }

    @Override
    public int modificar(Rol objeto) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, objeto.getId_rol());
        parametrosEntrada.put(2, objeto.getTipo());
        int resultado = DBManager.getInstance().ejecutarProcedimiento("MODIFICAR_ROL", parametrosEntrada, null);
        System.out.println("Se ha realizado la modificacion del area");
        return resultado;
    }

    @Override
    public int eliminar(int idObjeto) {
        throw new UnsupportedOperationException("Not supported yet.");
    }

    @Override
    public Rol obtenerPorId(int idObjeto) {
        Rol rol = null;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idObjeto);
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("OBTENER_ROL_X_ID", parametrosEntrada);
        System.out.println("Lectura de rol...");
        try{
            if(rs.next()){
                rol = new Rol();
                rol.setId_rol(rs.getInt("id_rol"));
                rol.setTipo(rs.getString("tipo"));
            }
        }catch(SQLException ex){
            System.out.println("ERROR: " + ex.getMessage());
        }finally{
            DBManager.getInstance().cerrarConexion();
        }
        return rol;
    }

    @Override
    public ArrayList<Rol> listarTodos() {
        ArrayList<Rol> roles = null;
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("LISTAR_ROLES_TODOS", null);
        System.out.println("Lectura de todas los roles...");
        try{
            while(rs.next()){
                if(roles == null) roles = new ArrayList<>();
                Rol rol = new Rol();
                rol.setId_rol(rs.getInt("id_rol"));
                rol.setTipo(rs.getString("tipo"));
                rol.setActivo(rs.getBoolean("activo"));
                rol.setCantidad_de_dias_por_prestamo(rs.getInt("cantidad_de_dias_por_prestamo"));
                roles.add(rol);
            }
        }catch(SQLException ex){
            System.out.println("ERROR: " + ex.getMessage());
        }finally{
            DBManager.getInstance().cerrarConexion();
        }
        return roles;
    }
    
}
