package pe.edu.pucp.utilsarmy.gestion_de_material.model;

public class Tesis extends MaterialBibliografico{
    private String institucionPublicacion;
    private String especialidad;
    private String asesor;
    private String grado; 
    
    public Tesis(){
        super();
    }

    public Tesis(String institucionPublicacion, String especialidad, String asesor, String grado, int idMaterial, int numero_copias, String titulo, int anho_publicacion, int numero_paginas, EstadoMaterial estado, String clasificacion_tematica, boolean activo, String idioma, Editorial editorial) {
<<<<<<< HEAD
        super(idMaterial, titulo, anho_publicacion, numero_paginas, estado, clasificacion_tematica, activo, idioma, editorial);
=======
        super(idMaterial, numero_copias, titulo, anho_publicacion, numero_paginas, estado, clasificacion_tematica, activo, idioma, editorial);
>>>>>>> e7cbe2292fd3facda28e5c7f4ace312400b5509f
        this.institucionPublicacion = institucionPublicacion;
        this.especialidad = especialidad;
        this.asesor = asesor;
        this.grado = grado;
    }

    public String getInstitucionPublicacion() {
        return institucionPublicacion;
    }

    public void setInstitucionPublicacion(String institucionPublicacion) {
        this.institucionPublicacion = institucionPublicacion;
    }

    public String getEspecialidad() {
        return especialidad;
    }

    public void setEspecialidad(String especialidad) {
        this.especialidad = especialidad;
    }

    public String getAsesor() {
        return asesor;
    }

    public void setAsesor(String asesor) {
        this.asesor = asesor;
    }

    public String getGrado() {
        return grado;
    }

    public void setGrado(String grado) {
        this.grado = grado;
    }
    
    
    
}
