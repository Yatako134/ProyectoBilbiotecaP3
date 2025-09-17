
package biblioteca.gestionUsuario.mysql;

import biblioteca.config.DBManager;
import biblioteca.gestionUsuario.dao.UsuarioDAO;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import pe.edu.pucp.utilsarmy.usuarios.model.Usuario;


public class UsuarioImpl implements UsuarioDAO{
    
    private ResultSet rs;
    
    @Override
    public int insertar(Usuario objeto) {
        Map<Integer,Object> parametrosSalida = new HashMap<>();
        Map<Integer,Object> parametrosEntrada = new HashMap<>();
        parametrosSalida.put(1, Types.INTEGER);
        parametrosEntrada.put(2, objeto.getNombre());
        parametrosEntrada.put(3, objeto.getPrimer_apellido());
        parametrosEntrada.put(4, objeto.getSegundo_apellido());
        parametrosEntrada.put(5, objeto.getCodigo());
        parametrosEntrada.put(6, objeto.getDOI());
        parametrosEntrada.put(7, objeto.getCorreo());
        parametrosEntrada.put(8, objeto.getContrasena());
        parametrosEntrada.put(9, objeto.getTelefono());
        parametrosEntrada.put(10, objeto.getId_rol());
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
        parametrosEntrada.put(5, objeto.getCodigo());
        parametrosEntrada.put(6, objeto.getDOI());
        parametrosEntrada.put(7, objeto.getCorreo());
        parametrosEntrada.put(8, objeto.getContrasena());
        parametrosEntrada.put(9, objeto.getTelefono());
        parametrosEntrada.put(10, objeto.getId_rol());
        parametrosEntrada.put(11, objeto.isActiva());
        int resultado = DBManager.getInstance().ejecutarProcedimiento("MODIFICAR_USUARIO", parametrosEntrada, null);
        System.out.println("Se ha realizado la modificacion del usuario");
        return resultado;    }

    @Override
    public int eliminar(int idObjeto) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idObjeto);
        int resultado = DBManager.getInstance().ejecutarProcedimiento("ELIMINAR_USUARIO", parametrosEntrada, null);
        System.out.println("Se ha realizado la eliminacion del area");
        return resultado;
    }

    @Override
    public Usuario obtenerPorId(int idObjeto) {
        Usuario usuario = null;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idObjeto);
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("OBTENER_USUARIO_X_ID", parametrosEntrada);
        System.out.println("Lectura de area...");
        try{
            if(rs.next()){
                usuario = new Usuario();
                usuario.setId_usuario(rs.getInt("id_usuario"));
                usuario.setNombre(rs.getString("nombre"));
                usuario.setPrimer_apellido(rs.getString("primer_apellido"));
                usuario.setSegundo_apellido(rs.getString("segundo_apellido"));
                usuario.setDOI(rs.getString("DOI"));
                usuario.setCodigo(rs.getInt("codigo"));
                usuario.setContrasena(rs.getString("contrasena"));
                usuario.setCorreo(rs.getString("correo"));
                usuario.setTelefono(rs.getString("telefono"));
                usuario.setActiva(rs.getBoolean("activo"));
                usuario.setId_rol(rs.getInt("id_rol"));
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
        ArrayList<Usuario> listaUsuarios = new ArrayList<>();
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("LISTAR_USUARIOS", null); // Suponiendo que tu SP se llama LISTAR_USUARIOS
        System.out.println("Leyendo todos los usuarios...");
        try {
            while (rs.next()) {
                Usuario usuario = new Usuario();
                usuario.setId_usuario(rs.getInt("id_usuario"));
                usuario.setNombre(rs.getString("nombre"));
                usuario.setPrimer_apellido(rs.getString("primer_apellido"));
                usuario.setSegundo_apellido(rs.getString("segundo_apellido"));
                usuario.setDOI(rs.getString("DOI"));
                usuario.setCodigo(rs.getInt("codigo"));
                usuario.setContrasena(rs.getString("contrasena"));
                usuario.setCorreo(rs.getString("correo"));
                usuario.setTelefono(rs.getString("telefono"));
                usuario.setActiva(rs.getBoolean("activo"));
                usuario.setId_rol(rs.getInt("id_rol"));
                // Si quieres, también podrías inicializar la lista de préstamos aquí: usuario.setPrestamos(new ArrayList<>());

                listaUsuarios.add(usuario);
            }
        } catch (SQLException ex) {
            System.out.println("ERROR: " + ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }
        return listaUsuarios;
    }
    
}
