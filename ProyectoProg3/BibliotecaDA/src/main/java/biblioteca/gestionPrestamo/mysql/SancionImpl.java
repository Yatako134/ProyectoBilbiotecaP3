
package biblioteca.gestionPrestamo.mysql;

import biblioteca.config.DBManager;
import biblioteca.gestionPrestamo.dao.SancionDAO;
import java.sql.Date;
import java.sql.Types;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Sancion;

public class SancionImpl implements SancionDAO{

    @Override
    public int insertar(Sancion objeto) {
        Map<Integer,Object> parametrosSalida = new HashMap<>();   
        Map<Integer,Object> parametrosEntrada = new HashMap<>();
        parametrosSalida.put(1, Types.INTEGER);
        parametrosEntrada.put(2, objeto.getTipo_sancion());
        parametrosEntrada.put(3, objeto.getDuracion_dias());
        parametrosEntrada.put(4, objeto.getFecha_fin());
        parametrosEntrada.put(5, objeto.getJustificacion());
        parametrosEntrada.put(6, objeto.getEstado());
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
        parametrosEntrada.put(2, objeto.getTipo_sancion());
        parametrosEntrada.put(3, objeto.getDuracion_dias());
        parametrosEntrada.put(4, objeto.getFecha_fin());
        parametrosEntrada.put(5, objeto.getJustificacion());
        parametrosEntrada.put(6, objeto.getEstado());
        parametrosEntrada.put(7, objeto.getPrestamo().getIdPrestamo());
        int resultado = DBManager.getInstance().ejecutarProcedimiento("MODIFICAR_SANCION", parametrosEntrada, null);
        System.out.println("Se ha realizado la modificacion de la sancion");
        return resultado;
    }

    @Override
    public int eliminar(int idObjeto) {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public Sancion obtenerPorId(int idObjeto) {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public ArrayList<Sancion> listarTodos() {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }
    
}
