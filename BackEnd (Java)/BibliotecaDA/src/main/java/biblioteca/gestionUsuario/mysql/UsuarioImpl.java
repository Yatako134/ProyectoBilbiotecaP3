
package biblioteca.gestionUsuario.mysql;

import biblioteca.config.DBManager;
import biblioteca.gestionUsuario.dao.UsuarioDAO;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import pe.edu.pucp.utilsarmy.usuarios.model.Rol;
import pe.edu.pucp.utilsarmy.usuarios.model.Usuario;


public class UsuarioImpl implements UsuarioDAO{
    
    private ResultSet rs;
    
    @Override
    public int insertar(Usuario objeto) {
        Map<Integer,Object> parametrosSalida = new HashMap<>();
        Map<Integer,Object> parametrosEntrada = new HashMap<>();
        parametrosSalida.put(1, Types.INTEGER);
        parametrosEntrada.put(2, objeto.getCodigo());
        parametrosEntrada.put(3, objeto.getNombre());
        parametrosEntrada.put(4, objeto.getPrimer_apellido());
        parametrosEntrada.put(5, objeto.getSegundo_apellido());
        parametrosEntrada.put(6, objeto.getDOI());
        parametrosEntrada.put(7, objeto.getCorreo());
        parametrosEntrada.put(8, objeto.getContrasena());
        parametrosEntrada.put(9, objeto.getCorreo());
        parametrosEntrada.put(10, objeto.getTelefono());
        parametrosEntrada.put(11, objeto.getRol_usuario().getId_rol());
        DBManager.getInstance().ejecutarProcedimiento("INSERTAR_USUARIO", parametrosEntrada, parametrosSalida);
        objeto.setId_usuario((int) parametrosSalida.get(1));
        System.out.println("Se ha realizado el registro del usuario");
        return objeto.getId_usuario();    
    }

    @Override
    public int modificar(Usuario objeto) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, objeto.getId_usuario());
        parametrosEntrada.put(2, objeto.getNombre());
        parametrosEntrada.put(3, objeto.getPrimer_apellido());
        parametrosEntrada.put(4, objeto.getSegundo_apellido());
        parametrosEntrada.put(5, objeto.getDOI());
        parametrosEntrada.put(6, objeto.getCodigo());
        parametrosEntrada.put(7, objeto.getContrasena());
        parametrosEntrada.put(8, objeto.getCorreo());
        parametrosEntrada.put(9, objeto.getTelefono());
        parametrosEntrada.put(10, objeto.getRol_usuario().getId_rol());
        //parametrosEntrada.put(11, objeto.isActiva());
        int resultado = DBManager.getInstance().ejecutarProcedimiento("MODIFICAR_USUARIO", parametrosEntrada, null);
        System.out.println("Se ha realizado la modificacion del usuario");
        return resultado;    
    }

    @Override
    public int eliminar(int idObjeto) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idObjeto);
        int resultado = DBManager.getInstance().ejecutarProcedimiento("ELIMINAR_USUARIO", parametrosEntrada, null);
        System.out.println("Se ha realizado la eliminacion del usuario");
        return resultado;
    }

    @Override
    public Usuario obtenerPorId(int idObjeto) {
        Usuario usuario = null;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idObjeto);
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("OBTENER_USUARIO_X_ID", parametrosEntrada);
        System.out.println("Lectura de usuarios...");
        try{
            if(rs.next()){
                usuario = new Usuario();
                usuario.setId_usuario(rs.getInt("id_usuario"));
                usuario.setCodigo(rs.getInt("codigo_universitario"));
                usuario.setNombre(rs.getString("nombre"));
                usuario.setPrimer_apellido(rs.getString("primer_apellido"));
                usuario.setSegundo_apellido(rs.getString("segundo_apellido"));
                usuario.setDOI(rs.getString("DOI"));
                usuario.setContrasena(rs.getString("contrasena"));
                usuario.setCorreo(rs.getString("correo"));
                usuario.setTelefono(rs.getString("numero_de_telefono"));
                usuario.setActiva(rs.getBoolean("activo"));
                Rol r = new Rol();
                r.setId_rol(rs.getInt("id_rol"));
                usuario.setRol_usuario(r);
            }
        }catch(SQLException ex){
            System.out.println("ERROR: " + ex.getMessage());
        }finally{
            DBManager.getInstance().cerrarConexion();
        }
        return usuario;
    }

    @Override
    public ArrayList<Usuario> listarTodos() {
        ArrayList<Usuario> users = null;
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("LISTAR_USUARIOS_TODOS", null);
        System.out.println("Lectura de todas los usuarios...");
        try{
            while(rs.next()){
                if(users == null) users = new ArrayList<>();
                Usuario usuario = new Usuario();
                usuario.setId_usuario(rs.getInt("id_usuario"));
                usuario.setCodigo(rs.getInt("codigo_universitario"));
                usuario.setNombre(rs.getString("nombre"));
                usuario.setPrimer_apellido(rs.getString("primer_apellido"));
                usuario.setSegundo_apellido(rs.getString("segundo_apellido"));
                usuario.setDOI(rs.getString("DOI"));
                usuario.setContrasena(rs.getString("contrasena"));
                usuario.setCorreo(rs.getString("correo"));
                usuario.setTelefono(rs.getString("numero_de_telefono"));
                usuario.setActiva(rs.getBoolean("activo"));
                Rol r = new Rol();
                r.setId_rol(rs.getInt("id_rol"));
                usuario.setRol_usuario(r);
                users.add(usuario);
            }
        }catch(SQLException ex){
            System.out.println("ERROR: " + ex.getMessage());
        }finally{
            DBManager.getInstance().cerrarConexion();
        }
        return users;
    }
    
    
}