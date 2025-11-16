
package biblioteca.gestionPrestamo.mysql;

import biblioteca.config.DBManager;
import biblioteca.gestionPrestamo.dao.SancionDAO;
import java.sql.Date;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.EstadoSancion;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Prestamo;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Sancion;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Tipo_sancion;
import pe.edu.pucp.utilsarmy.usuarios.model.Usuario;

public class SancionImpl implements SancionDAO{
    private ResultSet rs;


    @Override
    public int insertar(Sancion objeto) {
        Map<Integer,Object> parametrosSalida = new HashMap<>();   
        Map<Integer,Object> parametrosEntrada = new HashMap<>();
        parametrosSalida.put(1, Types.INTEGER);
        parametrosEntrada.put(2, objeto.getTipo_sancion().name());
        parametrosEntrada.put(3, objeto.getDuracion_dias());
        parametrosEntrada.put(4, objeto.getFecha_fin());
        parametrosEntrada.put(5, objeto.getJustificacion());
        parametrosEntrada.put(6, objeto.getEstado().name());
        parametrosEntrada.put(7, objeto.getPrestamo().getIdPrestamo());
        DBManager.getInstance().ejecutarProcedimiento("INSERTAR_SANCION", parametrosEntrada, parametrosSalida);
        objeto.setId_sancion((int) parametrosSalida.get(1));
        System.out.println("Se ha realizado el registro de la sancion");
        return objeto.getId_sancion();
    }

    @Override
    public int modificar(Sancion objeto) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, objeto.getId_sancion());
        parametrosEntrada.put(2, objeto.getTipo_sancion().name());
        parametrosEntrada.put(3, objeto.getDuracion_dias());
        parametrosEntrada.put(4, objeto.getJustificacion());
        int resultado = DBManager.getInstance().ejecutarProcedimiento("MODIFICAR_SANCION", parametrosEntrada, null);
        System.out.println("Se ha realizado la modificacion de la sancion");
        return resultado;
    }

    @Override
    public int eliminar(int idObjeto) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idObjeto);
        int resultado = DBManager.getInstance().ejecutarProcedimiento("FINALIZAR_SANCION", parametrosEntrada, null);
        System.out.println("Se ha realizado la eliminacion de la sancion");
        return resultado;
    }

    @Override
    public Sancion obtenerPorId(int idObjeto) {
        Sancion sancion = null;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idObjeto);
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("OBTENER_SANCION_X_ID", parametrosEntrada);
        System.out.println("Lectura de sancion...");
        try{
            if(rs.next()){
                sancion = new Sancion();
                sancion.setId_sancion(rs.getInt("id_sancion"));
            // Mapeamos el tipo de sanción (enum)
            String tipo = rs.getString("tipo_sancion");
            sancion.setTipo_sancion(Tipo_sancion.valueOf(tipo.toUpperCase())); 
            // ⚠️ asegúrate de que en DB los valores coincidan con tu enum, 
            // si no, haz un switch o normaliza.

            sancion.setDuracion_dias(rs.getInt("duracion_dias"));

            sancion.setFecha_inicio(rs.getDate("fecha_inicio"));
            sancion.setFecha_fin(rs.getDate("fecha_fin"));

            sancion.setJustificacion(rs.getString("justificacion"));

            // Mapeamos el estado (enum)
            String estadoStr = rs.getString("estado");
            sancion.setEstado(EstadoSancion.valueOf(estadoStr.toUpperCase()));

            // Relación con Prestamo
            Prestamo p = new Prestamo();
            p.setIdPrestamo(rs.getInt("id_prestamo"));
            sancion.setPrestamo(p);
            }
            
        }catch(SQLException ex){
            System.out.println("ERROR: " + ex.getMessage());
        }finally{
                DBManager.getInstance().cerrarConexion();
        }
        return sancion;
    }

    @Override
    public ArrayList<Sancion> listarTodos() {
    ArrayList<Sancion> sanciones = null;
    rs = DBManager.getInstance().ejecutarProcedimientoLectura("LISTAR_SANCIONES_TODAS", null);
    System.out.println("Lectura de sanciones...");
    try {
        while (rs.next()) {
            if (sanciones == null) sanciones = new ArrayList<>();
            Sancion s = new Sancion();

            s.setId_sancion(rs.getInt("id_sancion"));

            // Enum Tipo_sancion
            String tipo = rs.getString("tipo_sancion");
            s.setTipo_sancion(Tipo_sancion.valueOf(tipo.toUpperCase()));

            s.setDuracion_dias(rs.getInt("duracion_dias"));
            s.setFecha_inicio(rs.getDate("fecha_inicio"));
            s.setFecha_fin(rs.getDate("fecha_fin"));
            s.setJustificacion(rs.getString("justificacion"));

            // Enum EstadoSancion
            String estadoStr = rs.getString("estado");
            s.setEstado(EstadoSancion.valueOf(estadoStr.toUpperCase()));

//            s.setActivo(rs.getBoolean("activo"));
            
            Usuario u = new Usuario();
            u.setCodigo(rs.getInt("codigo_universitario"));
            // Relación con Prestamo
            Prestamo p = new Prestamo();
            p.setIdPrestamo(rs.getInt("id_prestamo"));
            p.setUsuario(u);
            s.setPrestamo(p);
            
            
            sanciones.add(s);
        }
    } catch (SQLException ex) {
        System.out.println("ERROR: " + ex.getMessage());
    } finally {
        DBManager.getInstance().cerrarConexion();
    }
    return sanciones;
    }
    
    @Override
    public ArrayList<Sancion> listarPorUsuario(int idUsuario) {
        ArrayList<Sancion> sanciones = null;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idUsuario);

        rs = DBManager.getInstance().ejecutarProcedimientoLectura("LISTAR_SANCIONES_X_USUARIO", parametrosEntrada);
        System.out.println("Lectura de sanciones por usuario...");
        try {
            while (rs.next()) {
                if (sanciones == null) sanciones = new ArrayList<>();
                Sancion s = new Sancion();

                s.setId_sancion(rs.getInt("id_sancion"));

                // Enum Tipo_sancion
                String tipo = rs.getString("tipo_sancion");
                s.setTipo_sancion(Tipo_sancion.valueOf(tipo.toUpperCase()));

                s.setDuracion_dias(rs.getInt("duracion_dias"));
                s.setFecha_inicio(rs.getDate("fecha_inicio"));
                s.setFecha_fin(rs.getDate("fecha_fin"));
                s.setJustificacion(rs.getString("justificacion"));

                // Enum EstadoSancion
                String estadoStr = rs.getString("estado");
                s.setEstado(EstadoSancion.valueOf(estadoStr.toUpperCase()));

                // Relación con Prestamo
                Prestamo p = new Prestamo();
                p.setIdPrestamo(rs.getInt("id_prestamo"));
                s.setPrestamo(p);

                sanciones.add(s);
            }
        } catch (SQLException ex) {
            System.out.println("ERROR: " + ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }
        System.out.println("📋 Total sanciones leídas para usuario " + idUsuario + ": " + (sanciones == null ? 0 : sanciones.size()));
        return sanciones;
    }

    @Override
    public ArrayList<Sancion> listar_busqueda_usuario(int codigo_universitario) {
        ArrayList<Sancion> sanciones = null;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, codigo_universitario);
    rs = DBManager.getInstance().ejecutarProcedimientoLectura("LISTAR_SANCIONES_BUSQUEDA_CODIGO_UNIVERSITARIO", parametrosEntrada);
    System.out.println("Lectura de sanciones...");
    try {
        while (rs.next()) {
            if (sanciones == null) sanciones = new ArrayList<>();
            Sancion s = new Sancion();

            s.setId_sancion(rs.getInt("id_sancion"));

            // Enum Tipo_sancion
            String tipo = rs.getString("tipo_sancion");
            s.setTipo_sancion(Tipo_sancion.valueOf(tipo.toUpperCase()));

            s.setDuracion_dias(rs.getInt("duracion_dias"));
            s.setFecha_inicio(rs.getDate("fecha_inicio"));
            s.setFecha_fin(rs.getDate("fecha_fin"));
            s.setJustificacion(rs.getString("justificacion"));

            // Enum EstadoSancion
            String estadoStr = rs.getString("estado");
            s.setEstado(EstadoSancion.valueOf(estadoStr.toUpperCase()));

//            s.setActivo(rs.getBoolean("activo"));
            
            Usuario u = new Usuario();
            u.setCodigo(rs.getInt("codigo_universitario"));
            // Relación con Prestamo
            Prestamo p = new Prestamo();
            p.setIdPrestamo(rs.getInt("id_prestamo"));
            p.setUsuario(u);
            s.setPrestamo(p);
            
            
            sanciones.add(s);
        }
    } catch (SQLException ex) {
        System.out.println("ERROR: " + ex.getMessage());
    } finally {
        DBManager.getInstance().cerrarConexion();
    }
    return sanciones;
    }
}
