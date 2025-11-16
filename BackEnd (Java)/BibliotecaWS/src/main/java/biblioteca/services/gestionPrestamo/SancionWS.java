
package biblioteca.services.gestionPrestamo;

import biblioteca.gestionPrestamo.boImpl.SancionBOImpl;
import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Sancion;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Tipo_sancion;

@WebService(serviceName = "SancionWS", targetNamespace = "pe.edu.pucp.utilsarmy.services")
public class SancionWS {
    
    private SancionBOImpl sancionboimpl;
    
    public SancionWS(){
        sancionboimpl=new SancionBOImpl();
    }

    @WebMethod(operationName = "insertarSancion")
    public int insertar(Sancion objeto) throws Exception {
        return sancionboimpl.insertar(objeto);
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
}

