package pe.edu.pucp.utilsarmy.gestion_de_material.model;

public class Articulo extends MaterialBibliografico {
    private String ISSN;
    private String revista;
    private int volumen;
    private int numero;
    
    public Articulo(){
        super();
    }

    public Articulo(String ISSN, String revista, int volumen, int numero, int idMaterial, int numero_copias, String titulo, int anho_publicacion, int numero_paginas, EstadoMaterial estado, String clasificacion_tematica, boolean activo, String idioma, Editorial editorial) {
<<<<<<< HEAD
        super(idMaterial, titulo, anho_publicacion, numero_paginas, estado, clasificacion_tematica, activo, idioma, editorial);
=======
        super(idMaterial, numero_copias, titulo, anho_publicacion, numero_paginas, estado, clasificacion_tematica, activo, idioma, editorial);
>>>>>>> e7cbe2292fd3facda28e5c7f4ace312400b5509f
        this.ISSN = ISSN;
        this.revista = revista;
        this.volumen = volumen;
        this.numero = numero;
    }

    

    public String getISSN() {
        return ISSN;
    }

    public void setISSN(String ISSN) {
        this.ISSN = ISSN;
    }

    public String getRevista() {
        return revista;
    }

    public void setRevista(String revista) {
        this.revista = revista;
    }

    public int getVolumen() {
        return volumen;
    }

    public void setVolumen(int volumen) {
        this.volumen = volumen;
    }

    public int getNumero() {
        return numero;
    }

    public void setNumero(int numero) {
        this.numero = numero;
    }

    
    


    
}
