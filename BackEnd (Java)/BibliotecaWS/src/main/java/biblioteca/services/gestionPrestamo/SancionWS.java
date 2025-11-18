
package biblioteca.services.gestionPrestamo;

import biblioteca.gestionPrestamo.boImpl.SancionBOImpl;
import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Prestamo;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Sancion;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Tipo_sancion;

@WebService(serviceName = "SancionWS", targetNamespace = "pe.edu.pucp.utilsarmy.services")
public class SancionWS {
    
    private SancionBOImpl sancionboimpl;
    
    public SancionWS(){
        sancionboimpl=new SancionBOImpl();
    }

    @WebMethod(operationName = "insertarSancion")
    public int insertar(@WebParam(name = "tipo")String tipo
            ,@WebParam(name = "duracion") int duracion
            ,@WebParam(name = "justificacion")String justificacion
            ,@WebParam(name = "id_prestamo")int id_prestamo) throws Exception {
        Sancion sancion = new Sancion();
        Prestamo prest = new Prestamo();
        prest.setIdPrestamo(id_prestamo);
        
        // se da los valores
        sancion.setTipo_sancion(Tipo_sancion.valueOf(tipo));
        sancion.setDuracion_dias(duracion);
        sancion.setJustificacion(justificacion);
        sancion.setPrestamo(prest);
        return sancionboimpl.insertar(sancion);
    }
    
    @WebMethod(operationName = "listarSanciones")
    public ArrayList<Sancion> listarSanciones() throws Exception {
        return sancionboimpl.listarTodos();
    }

    @WebMethod(operationName = "listarSancionesPorUsuario")
    public ArrayList<Sancion> listarSancionesPorUsuario(@WebParam(name = "idUsuario") int idUsuario) throws Exception {
        return sancionboimpl.listarPorUsuario(idUsuario);
    }
    @WebMethod(operationName = "BusquedaSanciones")
    public ArrayList<Sancion> buscarSanciones(@WebParam(name = "codigo_universitario") int _codigo_universitario) throws Exception {
        return sancionboimpl.listar_busqueda_usuario(_codigo_universitario);
    }
    @WebMethod(operationName = "obtener_por_id")
    public Sancion obtener_sancion_por_id(@WebParam(name = "idSancion") int id_sancion) throws Exception {
        return sancionboimpl.obtenerPorId(id_sancion);
    }
    @WebMethod(operationName = "finalizar_sancion")
    public int finalizar_sancion(@WebParam(name = "idSancion") int id_sancion) throws Exception {
        return sancionboimpl.eliminar(id_sancion);
    }
    @WebMethod(operationName = "modificar_sancion")
    public int modificar_sancion(@WebParam(name = "idSancion") Sancion sancion,
           @WebParam(name = "tipo") String tipo ) throws Exception {
        if(tipo == "ENTREGA_TARDIA"){
            sancion.setTipo_sancion(Tipo_sancion.ENTREGA_TARDIA);
        }
        else{
            sancion.setTipo_sancion(Tipo_sancion.DANHO);
        }
        return sancionboimpl.modificar(sancion);
    }
    
    @WebMethod(operationName = "listarSancionesPorUsuarioPorPanel")
    public ArrayList<Sancion> listarSancionesPorUsuarioPorPanel(@WebParam(name = "idUsuario") int idUsuario,
            @WebParam(name = "filtro") String filtro) throws Exception {
        // Si viene null desde el cliente, lo convertimos a vacío
        if (filtro == null) filtro = "";
        return sancionboimpl.listarPorUsuario_X_ID(idUsuario,filtro);
    }
    
}

