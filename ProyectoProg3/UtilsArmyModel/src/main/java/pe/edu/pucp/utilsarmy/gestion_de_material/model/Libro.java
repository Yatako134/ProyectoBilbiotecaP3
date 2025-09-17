package pe.edu.pucp.utilsarmy.gestion_de_material.model;

public class Libro extends MaterialBibliografico {
    private String ISBN;
    private String edicion;
    
    public Libro(){
        super();
    }

    public Libro(String ISBN, String edicion, int idMaterial, int numero_copias, String titulo, int anho_publicacion, int numero_paginas, EstadoMaterial estado, String clasificacion_tematica, boolean activo, String idioma, Editorial editorial) {
<<<<<<< HEAD
        super(idMaterial,titulo, anho_publicacion, numero_paginas, estado, clasificacion_tematica, activo, idioma, editorial);
=======
        super(idMaterial, numero_copias, titulo, anho_publicacion, numero_paginas, estado, clasificacion_tematica, activo, idioma, editorial);
>>>>>>> e7cbe2292fd3facda28e5c7f4ace312400b5509f
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
