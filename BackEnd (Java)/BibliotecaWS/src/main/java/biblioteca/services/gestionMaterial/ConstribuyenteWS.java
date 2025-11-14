
package biblioteca.services.gestionMaterial;

import biblioteca.gestionMaterial.boImpl.ContribuyenteBOImpl;
import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Contribuyente;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.TipoContribuyente;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Contribuyente;


@WebService(serviceName = "ConstribuyenteWS", targetNamespace = "pe.edu.pucp.utilsarmy.services")
public class ConstribuyenteWS {

    private ContribuyenteBOImpl contribuyentebo;

    public ConstribuyenteWS() {
        contribuyentebo = new ContribuyenteBOImpl();
    }


    @WebMethod(operationName = "modificarContribuyente")
    public int modificarContribuyente(@WebParam(name = "contribuyente") Contribuyente contribuyente) throws Exception {
        contribuyentebo = new ContribuyenteBOImpl();
        return contribuyentebo.modificar(contribuyente);
    }

    @WebMethod(operationName = "listarContribuyentesTodos")
    public ArrayList<Contribuyente> listarContribuyentesTodos() throws Exception {
        ArrayList<Contribuyente> contribuyentes = null;
        try {
            contribuyentebo = new ContribuyenteBOImpl();
            contribuyentes = contribuyentebo.listarTodos();
            if (contribuyentes == null) {
                contribuyentes = new ArrayList<>();
                return contribuyentes;
            }
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return contribuyentes;
    }

    @WebMethod(operationName = "obtenerContribuyentePorId")
    public Contribuyente obtenerContribuyentePorId(@WebParam(name = "idContribuyente") int idContribuyente) throws Exception {
        Contribuyente contribuyente = null;
        try {
            contribuyentebo = new ContribuyenteBOImpl();
            contribuyente = contribuyentebo.obtenerPorId(idContribuyente);
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return contribuyente;
    }

    @WebMethod(operationName = "eliminarContribuyente")
    public int eliminarContribuyente(@WebParam(name = "idContribuyente") int idContribuyente) {
        int resultado = 0;
        try {
            contribuyentebo = new ContribuyenteBOImpl();
            resultado = contribuyentebo.eliminar(idContribuyente);
            return resultado;
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return resultado;
    }
    /*
    @WebMethod(operationName = "insertarContribuyente")
    public int insertarContribuyente(@WebParam(name = "contribuyente") Contribuyente contribuyente) throws Exception {
        return contribuyenteBO.insertar(contribuyente);
    }
    */
    
    @WebMethod(operationName = "insertarContribuyente")
public int insertarContribuyente(
    @WebParam(name = "nombre") String nombre,
    @WebParam(name = "primerApellido") String primerApellido, 
    @WebParam(name = "segundoApellido") String segundoApellido,
    @WebParam(name = "seudonimo") String seudonimo,
    @WebParam(name = "tipoContribuyente") String tipoContribuyenteStr) throws Exception {
    // Crear objeto Contribuyente
    Contribuyente contribuyente = new Contribuyente();
    contribuyente.setNombre(nombre);
    contribuyente.setPrimer_apellido(primerApellido);
    contribuyente.setSegundo_apellido(segundoApellido);
    contribuyente.setSeudonimo(seudonimo);
    contribuyente.setTipo_contribuyente(TipoContribuyente.valueOf(tipoContribuyenteStr));
    
    return contribuyentebo.insertar(contribuyente);
}
    
    
    @WebMethod(operationName = "asignarContribuyente")
    public int asignarContribuyente(@WebParam(name = "idcontribuyente") int idcontribuyente,
            @WebParam(name = "idmaterial") int idmaterial) throws Exception {
        return contribuyentebo.asignar_contribuyente(idcontribuyente,idmaterial);
    }
    
}
