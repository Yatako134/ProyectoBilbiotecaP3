
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
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Editorial;
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
            if(rs.next()){
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
    public ArrayList<Editorial> buscarEditorial(int id) {
        ArrayList<Editorial> editoriales = null;
        Map<Integer, Object> parametrosEntrada = new HashMap<>();
        parametrosEntrada.put(1, id);

        ResultSet rs = DBManager.getInstance().ejecutarProcedimientoLectura("ObtenerEditorialesPorId", parametrosEntrada);

        try {
            while (rs.next()) {
                if (editoriales == null) editoriales = new ArrayList<>();

                Editorial e = new Editorial();
                e.setNombre(rs.getString("nombre"));

                editoriales.add(e);
            }
        } catch (SQLException ex) {
            System.out.println("ERROR: " + ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }

        return editoriales;
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
                materiales.add(material);
            }
            for (MaterialBibliografico m : materiales) {
                ArrayList<Contribuyente> contribuyentes = contBO.listar_autores_por_material(m.getIdMaterial());
                m.setContribuyentes(contribuyentes);
                m.FormatoAutores();
                
                ArrayList<Ejemplar> ejemplares = ejemBO.listar_disponibles_por_material(m.getIdMaterial());
                m.setEjemplares(ejemplares);
                if(ejemplares!=null)m.setCantidadDisponible(ejemplares.size());
                else m.setCantidadDisponible(0);
                ArrayList<Biblioteca> bibliotecas = bibBO.listar_bibliotecas_por_material(m.getIdMaterial());
                m.setBibliotecas(bibliotecas);
                m.FormatoBibliotecas();
                
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
                materiales.add(material);
            }
            for (MaterialBibliografico m : materiales) {
                ArrayList<Contribuyente> contribuyentes = contBO.listar_autores_por_material(m.getIdMaterial());
                m.setContribuyentes(contribuyentes);
                m.FormatoAutores();
                
                ArrayList<Ejemplar> ejemplares = ejemBO.listar_disponibles_por_material(m.getIdMaterial());
                m.setEjemplares(ejemplares);
                if(ejemplares!=null)m.setCantidadDisponible(ejemplares.size());
                else m.setCantidadDisponible(0);
                ArrayList<Biblioteca> bibliotecas = bibBO.listar_bibliotecas_por_material(m.getIdMaterial());
                m.setBibliotecas(bibliotecas);
                m.FormatoBibliotecas();
                
            }
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        } finally {
            DBManager.getInstance().cerrarConexion();
        }
        return materiales;
    }
    
}
