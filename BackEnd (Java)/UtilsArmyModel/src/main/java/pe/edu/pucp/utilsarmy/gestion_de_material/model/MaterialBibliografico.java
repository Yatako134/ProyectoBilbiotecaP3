package pe.edu.pucp.utilsarmy.gestion_de_material.model;

import java.util.ArrayList;
import java.util.stream.Collectors;

public class MaterialBibliografico {

    private int idMaterial;
    private String titulo;
    private int anho_publicacion;
    private int numero_paginas;
    private EstadoMaterial estado;
    private String clasificacion_tematica;
    private boolean activo;
    private String idioma;
    private String editoriales;
    private ArrayList<Contribuyente> contribuyentes;
    private ArrayList<Ejemplar> ejemplares;
    private ArrayList<Biblioteca> bibliotecas;
    private String autoresTexto;
    private String bibliotecasTexto;
    private TipoMaterial tipo;
    private int cantidadDisponible;
    public MaterialBibliografico() {
        this.contribuyentes = new ArrayList<>();
        this.ejemplares = new ArrayList<>();
        this.bibliotecas = new ArrayList<>();
    }

    public MaterialBibliografico(String titulo,
            int anho_publicacion, int numero_paginas, EstadoMaterial estado,
            String clasificacion_tematica, boolean activo, String idioma, String editoriales) {
       
        this.titulo = titulo;
        this.anho_publicacion = anho_publicacion;
        this.numero_paginas = numero_paginas;
        this.estado = estado;
        this.clasificacion_tematica = clasificacion_tematica;
        this.activo = activo;
        this.idioma = idioma;
        this.contribuyentes = new ArrayList<>();
        this.ejemplares = new ArrayList<>();
        this.editoriales = editoriales;
    }

    public int getIdMaterial() {
        return idMaterial;
    }

    public void setIdMaterial(int idMaterial) {
        this.idMaterial = idMaterial;
    }

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

    public String getAutoresTexto() {
        return autoresTexto;
    }

    public void setAutoresTexto(String autoresTexto) {
        this.autoresTexto = autoresTexto;
    }

    public void FormatoAutores() {
        if (contribuyentes == null || contribuyentes.isEmpty()) {
            autoresTexto = "No registrados";
        } else {
            autoresTexto = contribuyentes.stream()
                    .map(a -> a.getNombre() + " " + a.getPrimer_apellido())
                    .collect(Collectors.joining(", "));
        }

    }
    public void FormatoBibliotecas() {
        if (bibliotecas == null || bibliotecas.isEmpty()) {
            bibliotecasTexto = "No hay bibliotecas con este material";
        } else {
            bibliotecasTexto = bibliotecas.stream()
                    .map(a -> a.getNombre())
                    .collect(Collectors.joining(", "));
        }

    }

    public TipoMaterial getTipo() {
        return tipo;
    }

    public void setTipo(TipoMaterial tipo) {
        this.tipo = tipo;
    }

    public int getCantidadDisponible() {
        return cantidadDisponible;
    }

    public void setCantidadDisponible(int cantidadDisponible) {
        this.cantidadDisponible = cantidadDisponible;
    }

    public ArrayList<Biblioteca> getBibliotecas() {
        return bibliotecas;
    }

    public void setBibliotecas(ArrayList<Biblioteca> bibliotecas) {
        this.bibliotecas = bibliotecas;
    }

    public String getBibliotecasTexto() {
        return bibliotecasTexto;
    }

    public void setBibliotecasTexto(String bibliotecasTexto) {
        this.bibliotecasTexto = bibliotecasTexto;
    }

    public String getEditoriales() {
        return editoriales;
    }

    public void setEditoriales(String editoriales) {
        this.editoriales = editoriales;
    }

}
