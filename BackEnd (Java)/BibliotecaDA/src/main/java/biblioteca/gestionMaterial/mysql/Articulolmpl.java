package biblioteca.gestionMaterial.mysql;

import biblioteca.config.DBManager;
import biblioteca.gestionMaterial.dao.ArticuloDAO;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.EstadoMaterial;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Articulo;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.TipoMaterial;

public class Articulolmpl implements ArticuloDAO {

    private ResultSet rs;

    @Override
    public int insertar(Articulo objeto) {
        Map<Integer, Object> parametrosSalida = new HashMap<>();
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosSalida.put(1, Types.INTEGER);
        parametrosEntrada.put(2, objeto.getTitulo());
        parametrosEntrada.put(3, objeto.getAnho_publicacion());
        parametrosEntrada.put(4, objeto.getNumero_paginas());
        parametrosEntrada.put(5, objeto.getClasificacion_tematica());
        parametrosEntrada.put(6, objeto.getIdioma());
        parametrosEntrada.put(7, objeto.getISSN());
        parametrosEntrada.put(8, objeto.getRevista());
        parametrosEntrada.put(9, objeto.getVolumen());
        parametrosEntrada.put(10, objeto.getNumero());
        parametrosEntrada.put(11, objeto.getEditoriales());
        DBManager.getInstance().ejecutarProcedimiento("INSERTAR_ARTICULO", parametrosEntrada, parametrosSalida);
        objeto.setIdMaterial((int) parametrosSalida.get(1));
        System.out.println("Se ha realizado el registro del articulo");
        return objeto.getIdMaterial();
    }

    @Override
    public int modificar(Articulo objeto) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, objeto.getIdMaterial());
        parametrosEntrada.put(2, objeto.getTitulo());
        parametrosEntrada.put(3, objeto.getAnho_publicacion());
        parametrosEntrada.put(4, objeto.getNumero_paginas());
        parametrosEntrada.put(5, objeto.getClasificacion_tematica());
        parametrosEntrada.put(6, objeto.getIdioma());
        parametrosEntrada.put(7, objeto.getISSN());
        parametrosEntrada.put(8, objeto.getRevista());
        parametrosEntrada.put(9, objeto.getVolumen());
        parametrosEntrada.put(10, objeto.getNumero());
        parametrosEntrada.put(11, objeto.getEditoriales());

        int resultado = DBManager.getInstance().ejecutarProcedimiento("MODIFICAR_ARTICULO", parametrosEntrada, null);
        System.out.println("Se ha realizado la modificacion del articulo");
        return resultado;
    }

    @Override
    public int eliminar(int idObjeto) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idObjeto);
        int resultado = DBManager.getInstance().ejecutarProcedimiento("ELIMINAR_ARTICULO", parametrosEntrada, null);
        System.out.println("Se ha realizado la eliminacion del articulo");
        return resultado;
    }

    @Override
    public Articulo obtenerPorId(int idObjeto) {
        Articulo articulo = null;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idObjeto);
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("OBTENER_ARTICULO_X_ID", parametrosEntrada);
        System.out.println("Lectura de articulo...");
        try {
            if (rs.next()) {
                articulo = new Articulo();
                articulo.setIdMaterial(rs.getInt("id_articulo"));
                articulo.setTitulo(rs.getString("titulo"));
                articulo.setAnho_publicacion(rs.getInt("anho_publicacion"));
                articulo.setNumero_paginas(rs.getInt("numero_paginas"));
                articulo.setEstado(EstadoMaterial.valueOf(rs.getString("estado")));
                articulo.setClasificacion_tematica(rs.getString("clasificacion_tematica"));
                articulo.setActivo(rs.getBoolean("activo"));
                articulo.setIdioma(rs.getString("idioma"));
                articulo.setISSN(rs.getString("ISSN"));
                articulo.setRevista(rs.getString("revista"));
                articulo.setVolumen(rs.getInt("volumen"));
                articulo.setNumero(rs.getInt("numero"));
                articulo.setTipo(TipoMaterial.ARTICULO);
                articulo.setEditoriales(rs.getString("editoriales"));
            }
        } catch (SQLException ex) {
            System.out.println("ERROR: " + ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }
        articulo.setTipo(TipoMaterial.ARTICULO);
        return articulo;
    }

    @Override
    public ArrayList<Articulo> listarTodos() {
        ArrayList<Articulo> articulos = null;
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("LISTAR_ARTICULO_TODOS", null);
        System.out.println("Lectura de articulos...");
        try {
            while (rs.next()) {
                if (articulos == null) {
                    articulos = new ArrayList<>();
                }
                Articulo e = new Articulo();
                e.setIdMaterial(rs.getInt("id_articulo"));
                e.setTitulo(rs.getString("titulo"));
                e.setAnho_publicacion(rs.getInt("anho_publicacion"));
                e.setNumero_paginas(rs.getInt("numero_paginas"));
                e.setEstado(EstadoMaterial.valueOf(rs.getString("estado")));
                e.setClasificacion_tematica(rs.getString("clasificacion_tematica"));
                e.setActivo(rs.getBoolean("activo"));
                e.setIdioma(rs.getString("idioma"));
                e.setISSN(rs.getString("ISSN"));
                e.setRevista(rs.getString("revista"));
                e.setVolumen(rs.getInt("volumen"));
                e.setNumero(rs.getInt("numero"));
                e.setEditoriales(rs.getString("editoriales"));

                articulos.add(e);
            }
        } catch (SQLException ex) {
            System.out.println(ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }
        return articulos;
    }

}
