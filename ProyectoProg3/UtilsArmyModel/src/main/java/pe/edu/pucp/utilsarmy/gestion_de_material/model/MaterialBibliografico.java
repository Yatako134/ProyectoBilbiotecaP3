package pe.edu.pucp.utilsarmy.gestion_de_material.model;

import java.util.ArrayList;

public class MaterialBibliografico {
    private int idMaterial;
<<<<<<< HEAD
=======
    private int numero_copias;
>>>>>>> e7cbe2292fd3facda28e5c7f4ace312400b5509f
    private String titulo;
    private int anho_publicacion;
    private int numero_paginas;
    private EstadoMaterial estado;
    private String clasificacion_tematica;
    private boolean activo;
    private String idioma;
<<<<<<< HEAD
   
=======
    

>>>>>>> e7cbe2292fd3facda28e5c7f4ace312400b5509f
    private Editorial editorial;
    private ArrayList<Contribuyente> contribuyentes;
    private ArrayList<Ejemplar> ejemplares;

    public MaterialBibliografico(){
        this.editorial = new Editorial();
        this.contribuyentes = new ArrayList<>();
        this.ejemplares = new ArrayList<>();   
    }

<<<<<<< HEAD
    public MaterialBibliografico(int idMaterial, String titulo,
=======
    public MaterialBibliografico(int idMaterial, int numero_copias, String titulo,
>>>>>>> e7cbe2292fd3facda28e5c7f4ace312400b5509f
            int anho_publicacion, int numero_paginas, EstadoMaterial estado, 
            String clasificacion_tematica, boolean activo, String idioma, 
            Editorial editorial) {
        this.idMaterial = idMaterial;
<<<<<<< HEAD
=======
        this.numero_copias = numero_copias;
>>>>>>> e7cbe2292fd3facda28e5c7f4ace312400b5509f
        this.titulo = titulo;
        this.anho_publicacion = anho_publicacion;
        this.numero_paginas = numero_paginas;
        this.estado = estado;
        this.clasificacion_tematica = clasificacion_tematica;
        this.activo = activo;
        this.idioma = idioma;
        this.editorial = editorial;
        this.contribuyentes = new ArrayList<>();
        this.ejemplares = new ArrayList<>();
    }

    public int getIdMaterial() {
        return idMaterial;
    }

    public void setIdMaterial(int idMaterial) {
        this.idMaterial = idMaterial;
    }

<<<<<<< HEAD
 
=======
    public int getNumero_copias() {
        return numero_copias;
    }

    public void setNumero_copias(int numero_copias) {
        this.numero_copias = numero_copias;
    }
>>>>>>> e7cbe2292fd3facda28e5c7f4ace312400b5509f

    public String getTitulo() {
        return titulo;
    }

    public void setTitulo(String titulo) {
        this.titulo = titulo;
    }

    public int getAnho_publicacion() {
        return anho_publicacion;
    }

    public void setAnho_publicacion(int anho_publicacion) {
        this.anho_publicacion = anho_publicacion;
    }

    public int getNumero_paginas() {
        return numero_paginas;
    }

    public void setNumero_paginas(int numero_paginas) {
        this.numero_paginas = numero_paginas;
    }

    public EstadoMaterial getEstado() {
        return estado;
    }

    public void setEstado(EstadoMaterial estado) {
        this.estado = estado;
    }

    public String getClasificacion_tematica() {
        return clasificacion_tematica;
    }

    public void setClasificacion_tematica(String clasificacion_tematica) {
        this.clasificacion_tematica = clasificacion_tematica;
    }

    public boolean isActivo() {
        return activo;
    }

    public void setActivo(boolean activo) {
        this.activo = activo;
    }

    public String getIdioma() {
        return idioma;
    }

    public void setIdioma(String idioma) {
        this.idioma = idioma;
    }

    public Editorial getEditorial() {
        return editorial;
    }

    public void setEditorial(Editorial editorial) {
        this.editorial = editorial;
    }

    public ArrayList<Contribuyente> getContribuyentes() {
        return contribuyentes;
    }

    public void setContribuyentes(ArrayList<Contribuyente> contribuyentes) {
        this.contribuyentes = contribuyentes;
    }

    public ArrayList<Ejemplar> getEjemplares() {
        return ejemplares;
    }

    public void setEjemplares(ArrayList<Ejemplar> ejemplares) {
        this.ejemplares = ejemplares;
    }
   
    

    
    
    
    
}
