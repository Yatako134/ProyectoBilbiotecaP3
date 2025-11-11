
package biblioteca.services.gestionMaterial;

import biblioteca.gestionMaterial.boImpl.ContribuyenteBOImpl;
import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Contribuyente;
<<<<<<< HEAD
=======

>>>>>>> dbafd2528623e73d02b773112e815ec8c2a853d0
@WebService(serviceName = "ConstribuyenteWS")
public class ConstribuyenteWS {

    private ContribuyenteBOImpl contribuyentebo;

    @WebMethod(operationName = "insertarContribuyente")
    public int insertarContribuyente(@WebParam(name = "contribuyente") Contribuyente contribuyente) {
        int resultado = 0;
        try {
            contribuyentebo = new ContribuyenteBOImpl();
            resultado = contribuyentebo.insertar(contribuyente);
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return resultado;
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
    public int eliminarContribuyente(@WebParam(name = "idContribuyente") int idContribuyente) throws Exception {
        int resultado = 0;
        try {
            contribuyentebo = new ContribuyenteBOImpl();
            resultado = contribuyentebo.eliminar(idContribuyente);
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return resultado;
    }
}
