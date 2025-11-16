
package biblioteca.gestionMaterial.mysql;

import biblioteca.config.DBManager;
import biblioteca.gestionMaterial.dao.LibroDAO;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import javax.naming.spi.DirStateFactory;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.EstadoMaterial;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Libro;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Libro;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.TipoMaterial;

public class Librolmpl implements LibroDAO{
    
    private ResultSet rs;
    
    @Override
    public int insertar(Libro objeto) {
        Map<Integer,Object> parametrosSalida = new HashMap<>();
        Map<Integer,Object> parametrosEntrada = new HashMap<>();
        parametrosSalida.put(1, Types.INTEGER);
        parametrosEntrada.put(2, objeto.getTitulo());
        parametrosEntrada.put(3, objeto.getAnho_publicacion());
        parametrosEntrada.put(4, objeto.getNumero_paginas());
        parametrosEntrada.put(5, objeto.getClasificacion_tematica());
        
        parametrosEntrada.put(6, objeto.getIdioma());
        parametrosEntrada.put(7, objeto.getISBN());
        parametrosEntrada.put(8, objeto.getEdicion());
        
        DBManager.getInstance().ejecutarProcedimiento("INSERTAR_LIBRO", parametrosEntrada, parametrosSalida);
        objeto.setIdMaterial((int) parametrosSalida.get(1));
        System.out.println("Se ha realizado el registro del libro");
        return objeto.getIdMaterial();  
    }

    @Override
    public int modificar(Libro objeto) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, objeto.getIdMaterial());
        parametrosEntrada.put(2, objeto.getTitulo());
        parametrosEntrada.put(3, objeto.getAnho_publicacion());
        parametrosEntrada.put(4, objeto.getNumero_paginas());
        parametrosEntrada.put(5, objeto.getClasificacion_tematica());
        parametrosEntrada.put(6, objeto.getIdioma());
        parametrosEntrada.put(7, objeto.getISBN());
        parametrosEntrada.put(8, objeto.getEdicion());

        int resultado = DBManager.getInstance().ejecutarProcedimiento("MODIFICAR_LIBRO", parametrosEntrada, null);
        System.out.println("Se ha realizado la modificacion del libro");
        return resultado;
    }

    @Override
    public int eliminar(int idObjeto) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idObjeto);
        int resultado = DBManager.getInstance().ejecutarProcedimiento("ELIMINAR_LIBRO", parametrosEntrada, null);
        System.out.println("Se ha realizado la eliminacion del libro");
        return resultado;
    }

    @Override
    public Libro obtenerPorId(int idObjeto) {
        Libro libro = null;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idObjeto);
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("OBTENER_LIBRO_X_ID", parametrosEntrada);
        System.out.println("Lectura de libro...");
        try{
            if(rs.next()){
                libro = new Libro();
                libro.setIdMaterial(rs.getInt("id_libro"));
                libro.setTitulo(rs.getString("titulo"));
                libro.setAnho_publicacion(rs.getInt("anho_publicacion"));
                libro.setNumero_paginas(rs.getInt("numero_paginas"));
                libro.setClasificacion_tematica(rs.getString("clasificacion_tematica"));
                libro.setActivo(rs.getBoolean("activo"));
                libro.setIdioma(rs.getString("idioma"));
                libro.setISBN(rs.getString("ISBN"));
                libro.setEdicion(rs.getString("edicion"));
                libro.setEstado(EstadoMaterial.valueOf(rs.getString("estado")));
                
            }
        }catch(SQLException ex){
            System.out.println("ERROR: " + ex.getMessage());
        }finally{
            DBManager.getInstance().cerrarConexion();
        }
        libro.setTipo(TipoMaterial.LIBRO);
        return libro;
    }

    @Override
    public ArrayList<Libro> listarTodos() {
        ArrayList<Libro> libros = null;
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("LISTAR_LIBROS_TODOS", null);
        System.out.println("Lectura de libros...");
        try{
            while(rs.next()){
                if(libros == null) libros = new ArrayList<>();
                Libro e = new Libro();
                e.setIdMaterial(rs.getInt("id_libro"));
                e.setTitulo(rs.getString("titulo"));
                e.setAnho_publicacion(rs.getInt("anho_publicacion"));
                e.setNumero_paginas(rs.getInt("numero_paginas"));
                e.setClasificacion_tematica(rs.getString("clasificacion_tematica"));
                e.setActivo(rs.getBoolean("activo"));
                e.setIdioma(rs.getString("idioma"));
                e.setISBN(rs.getString("ISBN"));
                e.setEdicion(rs.getString("edicion"));
                e.setEstado(EstadoMaterial.valueOf(rs.getString("estado")));
                libros.add(e);
            }
        }catch(SQLException ex){
            System.out.println(ex.getMessage());
        }finally{
            DBManager.getInstance().cerrarConexion();
        }
        return libros;
    }
    
}
