
package biblioteca.gestionMaterial.mysql;

import biblioteca.config.DBManager;
import biblioteca.gestionMaterial.dao.ArticuloDAO;
import biblioteca.gestionMaterial.dao.BibliotecaDAO;
import biblioteca.gestionMaterial.dao.ContribuyenteDAO;
import biblioteca.gestionMaterial.dao.EjemplarDAO;
import biblioteca.gestionMaterial.dao.LibroDAO;
import biblioteca.gestionMaterial.dao.MaterialBiblioDAO;
import biblioteca.gestionMaterial.dao.TesisDAO;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Biblioteca;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Contribuyente;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Ejemplar;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.EstadoEjemplar;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.EstadoMaterial;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Libro;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.MaterialBibliografico;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.TipoMaterial;

public class MaterialBiblioImpl implements MaterialBiblioDAO{
     private ResultSet rs;

    // DAOs
    private LibroDAO librobo;
    private ArticuloDAO articulobo;
    private TesisDAO tesisbo;
    private ContribuyenteDAO contBO;
    private EjemplarDAO ejemBO;
    private BibliotecaDAO bibBO;
    public MaterialBiblioImpl() {
        librobo = new Librolmpl();
        tesisbo = new Tesislmpl();
        articulobo = new Articulolmpl();
        contBO = new ContribuyenteImpl();
        ejemBO = new EjemplarImpl();
        bibBO = new BibliotecaImpl();
    }
    
    @Override
    public ArrayList<Ejemplar> buscarEjemplares(int id) {
         ArrayList<Ejemplar> ejemplares = null;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, id);
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("EJEMPLARES_BIBLIOTECA", parametrosEntrada);
        try{
            while(rs.next()){
                if(ejemplares == null) ejemplares = new ArrayList<>();
                Ejemplar e = new Ejemplar();
                e.setIdEjemplar(rs.getInt("id_ejemplar"));
                e.setEstado(EstadoEjemplar.valueOf(rs.getString("estado")));
                e.setUbicacion(rs.getString("ubicacion"));
                Biblioteca b =new Biblioteca();
                
                b.setNombre(rs.getString("nombre"));
                b.setIdBiblioteca(rs.getInt("id_biblioteca"));
                
                e.setBlibioteca(b);
                ejemplares.add(e);
            }
        }catch(SQLException ex){
            System.out.println("ERROR: " + ex.getMessage());
        }finally{
            DBManager.getInstance().cerrarConexion();
        }
        return ejemplares;
        
    }

    @Override
    public ArrayList<Contribuyente> buscarContribuyente(int id) {
        ArrayList<Contribuyente> contribuyentes = null;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, id);

        ResultSet rs = DBManager.getInstance().ejecutarProcedimientoLectura("ObtenerContribuyentesPorMaterial", parametrosEntrada);

        try {
            while (rs.next()) {
                if (contribuyentes == null) contribuyentes = new ArrayList<>();

                Contribuyente c = new Contribuyente();
                c.setNombre(rs.getString("nombre"));
                c.setPrimer_apellido(rs.getString("primer_apellido"));
                c.setSegundo_apellido(rs.getString("segundo_apellido"));

                contribuyentes.add(c);
            }
        } catch (SQLException ex) {
            System.out.println("ERROR: " + ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }

        return contribuyentes;

    }


    @Override
    public ArrayList<Ejemplar> obtenerEjemplaresDisponibles(int idMaterial, int idBiblioteca) {
        ArrayList<Ejemplar> ejemplares = null;

        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idMaterial);
        parametrosEntrada.put(2, idBiblioteca);

        ResultSet rs = DBManager.getInstance().ejecutarProcedimientoLectura("sp_obtener_ejemplares_disponibles", parametrosEntrada);

        try {
            while (rs.next()) {
                if (ejemplares == null) ejemplares = new ArrayList<>();

                Ejemplar e = new Ejemplar();
                e.setIdEjemplar(rs.getInt("id_ejemplar"));
                e.setEstado(rs.getString("estado") != null ? EstadoEjemplar.valueOf(rs.getString("estado")) : null);
                e.setUbicacion(rs.getString("ubicacion"));

                ejemplares.add(e);
            }
        } catch (SQLException ex) {
            System.out.println("ERROR: " + ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }

        return ejemplares;
    }

    @Override
    public int insertar(MaterialBibliografico objeto) {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public int modificar(MaterialBibliografico objeto) {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public int eliminar(int idObjeto) {
        throw new UnsupportedOperationException("Not supported yet."); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/GeneratedMethodBody
    }

    @Override
    public MaterialBibliografico obtenerPorId(int idObjeto){
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idObjeto);

        ResultSet rs = DBManager.getInstance().ejecutarProcedimientoLectura("SP_OBTENER_TIPO_MATERIAL", parametrosEntrada);

        String tipo = "";
        try {
            if (rs.next()) {
                tipo = rs.getString("tipo");
            }
        } catch (SQLException ex) {
            System.out.println("ERROR: " + ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }

        switch (tipo) {
            case "LIBRO":
                return librobo.obtenerPorId(idObjeto);
            case "TESIS":
                return tesisbo.obtenerPorId(idObjeto);
            case "ARTICULO":
                return articulobo.obtenerPorId(idObjeto);
            default:
                return null;
        }
    }
    
    @Override
    public ArrayList<MaterialBibliografico> listarTodos() {
        ArrayList<MaterialBibliografico> materiales = null;
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("LISTAR_MATERIALES_TODOS", null);
        System.out.println("Lectura de materiales...");
        try {
            while (rs.next()) {
                if (materiales == null) {
                    materiales = new ArrayList<>();
                }
                MaterialBibliografico material = new MaterialBibliografico();
                material.setIdMaterial(rs.getInt("id_material"));
                material.setTitulo(rs.getString("titulo"));
                material.setAnho_publicacion(rs.getInt("anho_publicacion"));
                material.setNumero_paginas(rs.getInt("numero_paginas"));
                material.setEstado(EstadoMaterial.valueOf(rs.getString("estado")));
                material.setClasificacion_tematica(rs.getString("clasificacion_tematica"));
                material.setIdioma(rs.getString("idioma"));
                material.setTipo(TipoMaterial.valueOf(rs.getString("tipo")));
                material.setAutoresTexto(rs.getString("autores"));
                material.setBibliotecasTexto(rs.getString("bibliotecas"));
                material.setCantidadDisponible(rs.getInt("ejemplares_disponibles"));
                if(rs.getString("editoriales")!=null)material.setEditoriales(rs.getString("editoriales"));
                materiales.add(material);
            }

        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }
        return materiales;
    }

    @Override
    public ArrayList<MaterialBibliografico> listar_busqueda(String _parametro) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, _parametro);
        ArrayList<MaterialBibliografico> materiales = null;
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("LISTAR_MATERIALES_BUSQUEDA", parametrosEntrada);
        System.out.println("Lectura de materiales...");
        try {
            while (rs.next()) {
                if (materiales == null) {
                    materiales = new ArrayList<>();
                }
                MaterialBibliografico material = new MaterialBibliografico();
                material.setIdMaterial(rs.getInt("id_material"));
                material.setTitulo(rs.getString("titulo"));
                material.setAnho_publicacion(rs.getInt("anho_publicacion"));
                material.setNumero_paginas(rs.getInt("numero_paginas"));
                material.setEstado(EstadoMaterial.valueOf(rs.getString("estado")));
                material.setClasificacion_tematica(rs.getString("clasificacion_tematica"));
                material.setIdioma(rs.getString("idioma"));
                material.setTipo(TipoMaterial.valueOf(rs.getString("tipo")));
                material.setAutoresTexto(rs.getString("autores"));
                material.setBibliotecasTexto(rs.getString("bibliotecas"));
                material.setCantidadDisponible(rs.getInt("ejemplares_disponibles"));
                material.setEditoriales(rs.getString("editoriales"));
                materiales.add(material);
            }

        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }
        return materiales;
    }

    @Override
    public ArrayList<MaterialBibliografico> listar_busqueda_avanzada(String _titulo, 
            String _tipo_contribuyente, String _nombre_contribuyente, 
            String _tema, Integer _fecha_desde, Integer _fecha_hasta, 
            String _tipo_material, String _biblioteca, String _disponibilidad,
            String _editoriales) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, _titulo);
        parametrosEntrada.put(2, _tipo_contribuyente);
        parametrosEntrada.put(3, _nombre_contribuyente);
        parametrosEntrada.put(4, _tema);
        parametrosEntrada.put(5, _fecha_desde);
        parametrosEntrada.put(6, _fecha_hasta);
        parametrosEntrada.put(7, _tipo_material);
        parametrosEntrada.put(8, _biblioteca);
        parametrosEntrada.put(9, _disponibilidad);
        parametrosEntrada.put(10, _editoriales);
        ArrayList<MaterialBibliografico> materiales = null;
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("LISTAR_MATERIALES_BUSQUEDA_AVANZADA", parametrosEntrada);
        System.out.println("Lectura de materiales...");
        try {
            while (rs.next()) {
                if (materiales == null) {
                    materiales = new ArrayList<>();
                }
                MaterialBibliografico material = new MaterialBibliografico();
                material.setIdMaterial(rs.getInt("id_material"));
                material.setTitulo(rs.getString("titulo"));
                material.setAnho_publicacion(rs.getInt("anho_publicacion"));
                material.setNumero_paginas(rs.getInt("numero_paginas"));
                material.setEstado(EstadoMaterial.valueOf(rs.getString("estado")));
                material.setClasificacion_tematica(rs.getString("clasificacion_tematica"));
                material.setIdioma(rs.getString("idioma"));
                material.setTipo(TipoMaterial.valueOf(rs.getString("tipo")));
                material.setAutoresTexto(rs.getString("autores"));
                material.setBibliotecasTexto(rs.getString("bibliotecas"));
                material.setCantidadDisponible(rs.getInt("ejemplares_disponibles"));
                material.setAutoresTexto(rs.getString("autores"));
                material.setBibliotecasTexto(rs.getString("bibliotecas"));
                material.setCantidadDisponible(rs.getInt("ejemplares_disponibles"));
                material.setEditoriales(rs.getString("editoriales"));
                materiales.add(material);
            }
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }
        return materiales;
    }
    
    @Override
    public int ContarEjemplares(int idMaterial) {
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idMaterial);
        int cant = 0;
        ResultSet rs = DBManager.getInstance().ejecutarProcedimientoLectura("CONTAR_EJEMPLARES_ASIGNADOS_POR_MATERIAL", parametrosEntrada);

        try {
            while (rs.next()) {
                cant = rs.getInt("cantidad_ejemplares");
            }
        } catch (SQLException ex) {
            System.out.println("ERROR: " + ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }

        return cant;
    }

    @Override
    public ArrayList<MaterialBibliografico> listartodosnormal() {
        ArrayList<MaterialBibliografico> materiales = null;
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("LISTAR_MATERIALES_TODOS", null);
        System.out.println("Lectura de materiales...");
        try {
            while (rs.next()) {
                if (materiales == null) {
                    materiales = new ArrayList<>();
                }
                MaterialBibliografico material = new MaterialBibliografico();
                material.setIdMaterial(rs.getInt("id_material"));
                material.setTitulo(rs.getString("titulo"));
                material.setAnho_publicacion(rs.getInt("anho_publicacion"));
                material.setNumero_paginas(rs.getInt("numero_paginas"));
                material.setEstado(EstadoMaterial.valueOf(rs.getString("estado")));
                material.setClasificacion_tematica(rs.getString("clasificacion_tematica"));
                material.setIdioma(rs.getString("idioma"));
                material.setTipo(TipoMaterial.valueOf(rs.getString("tipo")));
                materiales.add(material);
            }
            
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }
        return materiales;
    }

    @Override
    public MaterialBibliografico obtener_por_id_solo_material(int idMaterial) {
        MaterialBibliografico material = null;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, idMaterial);
        rs = DBManager.getInstance().ejecutarProcedimientoLectura("OBTENER_MATERIAL_X_ID", parametrosEntrada);
        System.out.println("Lectura de material...");
        try{
            if(rs.next()){
                material = new MaterialBibliografico();
                material.setIdMaterial(rs.getInt("id_material"));
                material.setTitulo(rs.getString("titulo"));
                material.setAnho_publicacion(rs.getInt("anho_publicacion"));
                material.setNumero_paginas(rs.getInt("numero_paginas"));
                material.setEstado(EstadoMaterial.valueOf(rs.getString("estado")));
                material.setClasificacion_tematica(rs.getString("clasificacion_tematica"));
                material.setIdioma(rs.getString("idioma"));
                material.setTipo(TipoMaterial.valueOf(rs.getString("tipo")));
                material.setAutoresTexto(rs.getString("autores"));
                material.setBibliotecasTexto(rs.getString("bibliotecas"));
                material.setCantidadDisponible(rs.getInt("ejemplares_disponibles"));
                material.setEditoriales(rs.getString("editoriales"));
            }
        }catch(SQLException ex){
            System.out.println("ERROR: " + ex.getMessage());
        }finally{
            DBManager.getInstance().cerrarConexion();
        }
        return material;
    }
    
}
