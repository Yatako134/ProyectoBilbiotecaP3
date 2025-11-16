
package biblioteca.gestionMaterial.mysql;
import biblioteca.config.DBManager;
import biblioteca.gestionMaterial.dao.TesisDAO;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.EstadoMaterial;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Tesis;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.MaterialBibliografico;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Tesis;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.TipoMaterial;

public class Tesislmpl implements TesisDAO{
    
    private ResultSet rs;
    
    @Override
    public int insertar(Tesis objeto) {
        Map<Integer,Object> parametrosSalida = new HashMap<>();
        Map<Integer,Object> parametrosEntrada = new HashMap<>();
        parametrosSalida.put(1, Types.INTEGER);
        parametrosEntrada.put(2, objeto.getTitulo());
        parametrosEntrada.put(3, objeto.getAnho_publicacion());
        parametrosEntrada.put(4, objeto.getNumero_paginas());
        
        parametrosEntrada.put(5, objeto.getClasificacion_tematica());
        
        parametrosEntrada.put(6, objeto.getIdioma());
        parametrosEntrada.put(7, objeto.getEspecialidad());
        parametrosEntrada.put(8, objeto.getAsesor());
        parametrosEntrada.put(9, objeto.getGrado());
        parametrosEntrada.put(10, objeto.getInstitucionPublicacion());
        DBManager.getInstance().ejecutarProcedimiento("INSERTAR_TESIS", parametrosEntrada, parametrosSalida);
        objeto.setIdMaterial((int) parametrosSalida.get(1));
        System.out.println("Se ha realizado el registro del tesis");
        return objeto.getIdMaterial();  
    }

    @Override
    public int modificar(Tesis objeto) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, objeto.getIdMaterial());
        parametrosEntrada.put(2, objeto.getTitulo());
        parametrosEntrada.put(3, objeto.getAnho_publicacion());
        parametrosEntrada.put(4, objeto.getNumero_paginas());

        parametrosEntrada.put(5, objeto.getClasificacion_tematica());
 
        parametrosEntrada.put(6, objeto.getIdioma());  
        parametrosEntrada.put(7, objeto.getEspecialidad());
        parametrosEntrada.put(8, objeto.getAsesor());
        parametrosEntrada.put(9, objeto.getGrado());
        parametrosEntrada.put(10, objeto.getInstitucionPublicacion());
        int resultado = DBManager.getInstance().ejecutarProcedimiento("MODIFICAR_TESIS", parametrosEntrada, null);
        System.out.println("Se ha realizado la modificacion del tesis");
        return resultado;
    }

    @Override
    public int eliminar(int idObjeto) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idObjeto);
        int resultado = DBManager.getInstance().ejecutarProcedimiento("ELIMINAR_TESIS", parametrosEntrada, null);
        System.out.println("Se ha realizado la eliminacion del tesis");
        return resultado;
    }

    @Override
    public Tesis obtenerPorId(int idObjeto) {
        Tesis tesis = null;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idObjeto);
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("OBTENER_TESIS_X_ID", parametrosEntrada);
        System.out.println("Lectura de tesis...");
        try{
            if(rs.next()){
                tesis = new Tesis();
                tesis.setIdMaterial(rs.getInt("id_tesis"));
                tesis.setTitulo(rs.getString("titulo"));
                tesis.setAnho_publicacion(rs.getInt("anho_publicacion"));
                tesis.setNumero_paginas(rs.getInt("numero_paginas"));
                tesis.setEstado(EstadoMaterial.valueOf(rs.getString("estado")));
                tesis.setClasificacion_tematica(rs.getString("clasificacion_tematica"));
                tesis.setActivo(rs.getBoolean("activo"));
                tesis.setIdioma(rs.getString("idioma"));
                tesis.setEspecialidad(rs.getString("especialidad"));
                tesis.setAsesor(rs.getString("asesor"));
                tesis.setGrado(rs.getString("grado"));
                tesis.setInstitucionPublicacion(rs.getString("institucion_publicacion"));
                tesis.setTipo(TipoMaterial.TESIS);
            }
        }catch(SQLException ex){
            System.out.println("ERROR: " + ex.getMessage());
        }finally{
            DBManager.getInstance().cerrarConexion();
        }
        tesis.setTipo(TipoMaterial.TESIS);
        return tesis;
    }

    @Override
    public ArrayList<Tesis> listarTodos() {
        ArrayList<Tesis> tesis = null;
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("LISTAR_TESIS_TODOS", null);
        System.out.println("Lectura de tesis...");
        try{
            while(rs.next()){
                if(tesis == null) tesis = new ArrayList<>();
                Tesis e = new Tesis();
                e.setIdMaterial(rs.getInt("id_tesis"));
                e.setTitulo(rs.getString("titulo"));
                e.setAnho_publicacion(rs.getInt("anho_publicacion"));
                e.setNumero_paginas(rs.getInt("numero_paginas"));
                e.setEstado(EstadoMaterial.valueOf(rs.getString("estado")));
                e.setClasificacion_tematica(rs.getString("clasificacion_tematica"));
                e.setActivo(rs.getBoolean("activo"));
                e.setIdioma(rs.getString("idioma"));
                e.setEspecialidad(rs.getString("especialidad"));
                e.setAsesor(rs.getString("asesor"));
                e.setGrado(rs.getString("grado"));
                e.setInstitucionPublicacion(rs.getString("institucion_publicacion"));
                tesis.add(e);
            }
        }catch(SQLException ex){
            System.out.println(ex.getMessage());
        }finally{
            DBManager.getInstance().cerrarConexion();
        }
        return tesis;
    }
    
}
