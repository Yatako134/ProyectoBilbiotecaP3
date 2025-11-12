package biblioteca.services.gestionMaterial;

import biblioteca.gestionMaterial.boImpl.TesisBOImpl;
import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;

import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Tesis;

@WebService(serviceName = "TesisWS")
public class TesisWS {

    public TesisWS() {
        tesisbo = new TesisBOImpl();
    }

    private TesisBOImpl tesisbo;

    @WebMethod(operationName = "insertarTesis")
    public int insertarTesis(@WebParam(name = "tesis") Tesis tesis) {
        int resultado = 0;
        try {
            tesisbo = new TesisBOImpl();
            resultado = tesisbo.insertar(tesis);
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return resultado;
    }

    @WebMethod(operationName = "modificarTesis")
    public int modificarTesis(@WebParam(name = "tesis") Tesis tesis) throws Exception{
        tesisbo = new TesisBOImpl();
        return tesisbo.modificar(tesis);
    }

    @WebMethod(operationName = "listarTesisTodos")
    public ArrayList<Tesis> listarTesisTodos() throws Exception {
        ArrayList<Tesis> tesisLista = null;
        try {
            tesisbo = new TesisBOImpl();
            tesisLista = tesisbo.listarTodos();
            if (tesisLista == null) {
                tesisLista = new ArrayList<>();
                return tesisLista;
            }
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return tesisLista;
    }

    @WebMethod(operationName = "obtenerTesisPorId")
    public Tesis obtenerTesisPorId(@WebParam(name = "idTesis") int idTesis) throws Exception {
        Tesis tesis = null;
        try {
            tesisbo = new TesisBOImpl();
            tesis = tesisbo.obtenerPorId(idTesis);
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return tesis;
    }

    @WebMethod(operationName = "eliminarTesis")
    public int eliminarTesis(@WebParam(name = "idTesis") int idTesis) throws Exception {
        int resultado = 0;
        try {
            tesisbo = new TesisBOImpl();
            resultado = tesisbo.eliminar(idTesis);
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return resultado;
    }
}
