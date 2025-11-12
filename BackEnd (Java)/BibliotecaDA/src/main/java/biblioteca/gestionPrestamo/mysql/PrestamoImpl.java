
package biblioteca.gestionPrestamo.mysql;

import biblioteca.config.DBManager;
import biblioteca.gestionPrestamo.dao.PrestamoDAO;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Ejemplar;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.EstadoPrestamo;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Prestamo;
import pe.edu.pucp.utilsarmy.usuarios.model.Usuario;

public class PrestamoImpl implements PrestamoDAO{
    private ResultSet rs;
    @Override
    public int insertar(Prestamo objeto) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        Map<Integer, Object> parametrosSalida = new HashMap<>();
        
        parametrosSalida.put(1, Types.INTEGER); //
        parametrosEntrada.put(2, objeto.getEjemplar().getIdEjemplar());
        parametrosEntrada.put(3, objeto.getUsuario().getId_usuario());
        DBManager.getInstance().ejecutarProcedimiento("INSERTAR_PRESTAMO", parametrosEntrada, parametrosSalida);
        objeto.setIdPrestamo((int) parametrosSalida.get(1));
        System.out.println("Se ha realizado el registro del préstamo");
        return objeto.getIdPrestamo();
    }

    @Override
    public int modificar(Prestamo objeto) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        
        parametrosEntrada.put(1, objeto.getIdPrestamo());
        parametrosEntrada.put(2, objeto.getFecha_de_prestamo());
        parametrosEntrada.put(3, objeto.getFecha_vencimiento());
        parametrosEntrada.put(4, objeto.getEstado().name());
        parametrosEntrada.put(5, objeto.getEjemplar().getIdEjemplar());
        parametrosEntrada.put(6, objeto.getUsuario().getId_usuario());

        int resultado = DBManager.getInstance().ejecutarProcedimiento("MODIFICAR_PRESTAMO", parametrosEntrada, null);
        System.out.println("Se ha realizado la modificación del préstamo");
        return resultado;
    }

    @Override
    public int eliminar(int idObjeto) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idObjeto);

        int resultado = DBManager.getInstance().ejecutarProcedimiento("ELIMINAR_PRESTAMO", parametrosEntrada, null);
        System.out.println("Se ha eliminado el préstamo");
        return resultado;
    }

    @Override
    public Prestamo obtenerPorId(int idObjeto) {
        Prestamo prestamo = null;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idObjeto);

        rs = DBManager.getInstance().ejecutarProcedimientoLectura("OBTENER_PRESTAMO_X_ID", parametrosEntrada);
        System.out.println("Lectura de préstamo...");
        try {
            if (rs.next()) {
                prestamo = new Prestamo();
                prestamo.setIdPrestamo(rs.getInt("id_prestamo"));
                prestamo.setFecha_de_prestamo(rs.getTimestamp("fecha_de_prestamo"));
                prestamo.setFecha_vencimiento(rs.getTimestamp("fecha_vencimiento"));

                String estadoStr = rs.getString("estado");
                prestamo.setEstado(EstadoPrestamo.valueOf(estadoStr.toUpperCase()));

                // Relaciones
                Ejemplar ejemplar = new Ejemplar();
                ejemplar.setIdEjemplar(rs.getInt("id_ejemplar"));
                prestamo.setEjemplar(ejemplar);

                Usuario usuario = new Usuario();
                usuario.setId_usuario(rs.getInt("id_usuario"));
                prestamo.setUsuario(usuario);
            }
        } catch (SQLException ex) {
            System.out.println("ERROR: " + ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }
        return prestamo;
    }

    @Override
    public ArrayList<Prestamo> listarTodos() {
        ArrayList<Prestamo> prestamos = null;
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("LISTAR_PRESTAMOS_TODOS", null);
        System.out.println("Lectura de préstamos...");
        try {
            while (rs.next()) {
                if (prestamos == null) prestamos = new ArrayList<>();
                Prestamo p = new Prestamo();

                p.setIdPrestamo(rs.getInt("id_prestamo"));
                p.setFecha_de_prestamo(rs.getTimestamp("fecha_de_prestamo"));
                p.setFecha_vencimiento(rs.getTimestamp("fecha_vencimiento"));

                String estadoStr = rs.getString("estado");
                p.setEstado(EstadoPrestamo.valueOf(estadoStr.toUpperCase()));

                Ejemplar ejemplar = new Ejemplar();
                ejemplar.setIdEjemplar(rs.getInt("id_ejemplar"));
                p.setEjemplar(ejemplar);

                Usuario usuario = new Usuario();
                usuario.setId_usuario(rs.getInt("id_usuario"));
                p.setUsuario(usuario);

                prestamos.add(p);
            }
        } catch (SQLException ex) {
            System.out.println("ERROR: " + ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }
        return prestamos;
    }
    
    @Override
    public ArrayList<Prestamo> listarPorUsuario(int idUsuario) {
        ArrayList<Prestamo> prestamos = null;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idUsuario); // El parámetro que espera tu procedimiento almacenado

        rs = DBManager.getInstance().ejecutarProcedimientoLectura("LISTAR_PRESTAMOS_X_USUARIO", parametrosEntrada);
        System.out.println("Lectura de préstamos por usuario...");
        try {
            while (rs.next()) {
                if (prestamos == null) prestamos = new ArrayList<>();
                Prestamo p = new Prestamo();

                p.setIdPrestamo(rs.getInt("id_prestamo"));
                p.setFecha_de_prestamo(rs.getTimestamp("fecha_de_prestamo"));
                p.setFecha_vencimiento(rs.getTimestamp("fecha_vencimiento"));

                String estadoStr = rs.getString("estado");
                p.setEstado(EstadoPrestamo.valueOf(estadoStr.toUpperCase()));

                Ejemplar ejemplar = new Ejemplar();
                ejemplar.setIdEjemplar(rs.getInt("id_ejemplar"));
                p.setEjemplar(ejemplar);

                Usuario usuario = new Usuario();
                usuario.setId_usuario(rs.getInt("id_usuario"));
                p.setUsuario(usuario);

                prestamos.add(p);
            }
        } catch (SQLException ex) {
            System.out.println("ERROR: " + ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }

        return prestamos;
    }    
    
}
