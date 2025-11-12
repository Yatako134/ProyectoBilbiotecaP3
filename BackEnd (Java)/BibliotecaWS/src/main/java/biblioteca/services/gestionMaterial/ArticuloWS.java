package biblioteca.services.gestionMaterial;

import biblioteca.gestionMaterial.boImpl.ArticuloBOImpl;
import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import java.util.ArrayList;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Articulo;
@WebService(serviceName = "ArticuloWS")
public class ArticuloWS {

    private ArticuloBOImpl articulobo;

    @WebMethod(operationName = "insertarArticulo")
    public int insertarArticulo(@WebParam(name = "articulo") Articulo articulo) {
        int resultado = 0;
        try {
            articulobo = new ArticuloBOImpl();
            resultado = articulobo.insertar(articulo);
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return resultado;
    }

    @WebMethod(operationName = "modificarArticulo")
    public int modificarArticulo(@WebParam(name = "articulo") Articulo articulo) throws Exception {
        articulobo = new ArticuloBOImpl();
        return articulobo.modificar(articulo);
    }

    @WebMethod(operationName = "listarArticulosTodos")
    public ArrayList<Articulo> listarArticulosTodos() throws Exception {
        ArrayList<Articulo> articulos = null;
        try {
            articulobo = new ArticuloBOImpl();
            articulos = articulobo.listarTodos();
            if (articulos == null) {
                articulos = new ArrayList<>();
                return articulos;
            }
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return articulos;
    }

    @WebMethod(operationName = "obtenerArticuloPorId")
    public Articulo obtenerArticuloPorId(@WebParam(name = "idArticulo") int idArticulo) throws Exception {
        Articulo articulo = null;
        try {
            articulobo = new ArticuloBOImpl();
            articulo = articulobo.obtenerPorId(idArticulo);
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return articulo;
    }

    @WebMethod(operationName = "eliminarArticulo")
    public int eliminarArticulo(@WebParam(name = "idArticulo") int idArticulo) throws Exception {
        int resultado = 0;
        try {
            articulobo = new ArticuloBOImpl();
            resultado = articulobo.eliminar(idArticulo);
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
        return resultado;
    }
}
