package pe.edu.pucp.utilsarmy.gestion_de_material.model;

public class Libro extends MaterialBibliografico {
    private String ISBN;
    private String edicion;
    
    public Libro(){
        super();
    }

    public Libro(String ISBN, String edicion, int numero_copias, String titulo, int anho_publicacion, int numero_paginas, EstadoMaterial estado, String clasificacion_tematica, boolean activo, String idioma, String editoriales) {
        super(titulo, anho_publicacion, numero_paginas, estado, clasificacion_tematica, activo, idioma, editoriales);
        this.ISBN = ISBN;
        this.edicion = edicion;
    }

    public String getISBN() {
        return ISBN;
    }

    public void setISBN(String ISBN) {
        this.ISBN = ISBN;
    }

    public String getEdicion() {
        return edicion;
    }

    public void setEdicion(String edicion) {
        this.edicion = edicion;
    }
 
}
