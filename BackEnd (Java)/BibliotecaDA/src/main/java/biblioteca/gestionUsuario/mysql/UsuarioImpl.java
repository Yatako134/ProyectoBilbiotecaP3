
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
        parametrosEntrada.put(9, objeto.getTelefono());
        parametrosEntrada.put(10, objeto.getRol_usuario().getId_rol());
        DBManager.getInstance().ejecutarProcedimiento("INSERTAR_USUARIO", parametrosEntrada, parametrosSalida);
        objeto.setId_usuario((int) parametrosSalida.get(1));
        System.out.println("Se ha realizado el registro del usuario");
        return objeto.getId_usuario();    
    }

    @Override
    public int modificar(Usuario objeto) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, objeto.getId_usuario());
        parametrosEntrada.put(2, objeto.getCodigo());
        parametrosEntrada.put(3, objeto.getNombre());
        parametrosEntrada.put(4, objeto.getPrimer_apellido());
        parametrosEntrada.put(5, objeto.getSegundo_apellido());
        parametrosEntrada.put(6, objeto.getDOI());
        parametrosEntrada.put(7, objeto.getCorreo());
        parametrosEntrada.put(8, objeto.getContrasena());
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
    @Override
    public Usuario obtenerUsuarioxCodigo(int codigo) {
        Usuario usuario = null;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, codigo);

        rs = DBManager.getInstance().ejecutarProcedimientoLectura(
                "sp_obtener_usuario_por_codigo", parametrosEntrada);

        try {
            if (rs.next()) {
                usuario = new Usuario();
                if (rs.getObject("id_usuario") != null)
                    usuario.setId_usuario(rs.getInt("id_usuario"));
                if (rs.getObject("codigo_universitario") != null)
                    usuario.setCodigo(rs.getInt("codigo_universitario"));
                if (rs.getObject("nombre") != null)
                    usuario.setNombre(rs.getString("nombre"));
                if (rs.getObject("primer_apellido") != null)
                    usuario.setPrimer_apellido(rs.getString("primer_apellido"));
                if (rs.getObject("segundo_apellido") != null)
                    usuario.setSegundo_apellido(rs.getString("segundo_apellido"));
                if (rs.getObject("DOI") != null)
                    usuario.setDOI(rs.getString("DOI"));
                if (rs.getObject("correo") != null)
                    usuario.setCorreo(rs.getString("correo"));
                if (rs.getObject("numero_de_telefono") != null)
                    usuario.setTelefono(rs.getString("numero_de_telefono"));

                Rol rol = new Rol();
                if (rs.getObject("id_rol") != null)
                    rol.setId_rol(rs.getInt("id_rol"));
                if (rs.getObject("cantidad_de_dias_por_prestamo") != null)
                    rol.setCantidad_de_dias_por_prestamo(rs.getInt("cantidad_de_dias_por_prestamo"));
                if (rs.getObject("rol") != null)
                    rol.setTipo(rs.getString("rol"));
                if (rs.getObject("limite_prestamos") != null)
                    rol.setLimite_prestamo(rs.getInt("limite_prestamos"));

                usuario.setRol_usuario(rol);
            }
        } catch (SQLException ex) {
            System.out.println("ERROR en obtenerUsuarioPorCodigo: " + ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }

        return usuario;
    }

    @Override
    public int prestamos_vigentesxUsuario(int idUsuario) {
        int cantidad_prestamos_vigentes=0;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idUsuario);
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("SP_CONTAR_PRESTAMOS_VIGENTES_POR_USUARIO", parametrosEntrada);
//        System.out.println("Lectura de usuarios...");
        try{
            if(rs.next()){
                cantidad_prestamos_vigentes=rs.getInt("cantidad_prestamos_vigentes");
            }
        }catch(SQLException ex){
            System.out.println("ERROR: " + ex.getMessage());
        }finally{
            DBManager.getInstance().cerrarConexion();
        }
        return cantidad_prestamos_vigentes;
    }

    @Override
    public int verificar(Usuario usuario) {
        int resultado=0;
        Map<Integer,Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, usuario.getCorreo());
        parametrosEntrada.put(2, usuario.getContrasena());
        rs=DBManager.getInstance().ejecutarProcedimientoLectura("VERIFICAR_CUENTA", parametrosEntrada);
        try{
        if(rs.next()){
                resultado=rs.getInt("id_usuario");
            }
        }catch(SQLException ex){
            System.out.println(ex.getMessage());
        }finally{
            DBManager.getInstance().cerrarConexion();
        }
        System.out.println("Se esta verificando la cuenta del usuario...");
        return resultado;
    }
    
}