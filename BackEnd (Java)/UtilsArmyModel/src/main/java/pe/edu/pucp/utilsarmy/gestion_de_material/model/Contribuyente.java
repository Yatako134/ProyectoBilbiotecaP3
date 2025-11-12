package pe.edu.pucp.utilsarmy.gestion_de_material.model;

public class Contribuyente {
    private int idContribuyente;
    private String nombre;
    private String primer_apellido;
    private String segundo_apellido;
    private String seudonimo;
    private TipoContribuyente tipo_contribuyente;
//    private int id_material;
    
    public Contribuyente(){}

    public Contribuyente(String nombre, String primer_apellido,
            String segundo_apellido, String seudonimo, TipoContribuyente tipo_contribuyente,int id_material) {
        this.nombre = nombre;
        this.primer_apellido = primer_apellido;
        this.segundo_apellido = segundo_apellido;
        this.seudonimo = seudonimo;
        this.tipo_contribuyente = tipo_contribuyente;
//        this.id_material = id_material;
    }
    
    public int getIdContribuyente() {
        return idContribuyente;
    }

    public void setIdContribuyente(int idContribuyente) {
        this.idContribuyente = idContribuyente;
    }

    public String getNombre() {
        return nombre;
    }

//    public int getId_material() {
//        return id_material;
//    }
//
//    public void setId_material(int id_material) {
//        this.id_material = id_material;
//    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public String getPrimer_apellido() {
        return primer_apellido;
    }

    public void setPrimer_apellido(String primer_apellido) {
        this.primer_apellido = primer_apellido;
    }

    public String getSegundo_apellido() {
        return segundo_apellido;
    }

    public void setSegundo_apellido(String segundo_apellido) {
        this.segundo_apellido = segundo_apellido;
    }

    public String getSeudonimo() {
        return seudonimo;
    }

    public void setSeudonimo(String seudonimo) {
        this.seudonimo = seudonimo;
    }

    public TipoContribuyente getTipo_contribuyente() {
        return tipo_contribuyente;
    }

    public void setTipo_contribuyente(TipoContribuyente tipo_contribuyente) {
        this.tipo_contribuyente = tipo_contribuyente;
    }
    
}
