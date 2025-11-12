
package biblioteca.ibo;
import java.util.ArrayList;

public interface IBO <T>{
    int insertar(T objeto) throws Exception;
    int modificar(T objeto) throws Exception;
    int eliminar(int idObjeto) throws Exception;
    T obtenerPorId(int idObjeto) throws Exception;
    ArrayList<T> listarTodos() throws Exception;
    void validar(T objeto) throws Exception;
}