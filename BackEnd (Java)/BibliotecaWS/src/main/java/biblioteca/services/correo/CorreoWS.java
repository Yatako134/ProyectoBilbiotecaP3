/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/WebServices/WebService.java to edit this template
 */
package biblioteca.services.correo;

import biblioteca.correo.CorreoBOImpl;
import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;

/**
 *
 * @author renat
 */
@WebService(serviceName = "CorreoWS")
public class CorreoWS {

    /**
     * This is a sample web service operation
     */
    private CorreoBOImpl correoBO;
    public CorreoWS(){
        correoBO = new CorreoBOImpl();
    }
    @WebMethod(operationName = "enviar_correo")
    public boolean envio_correo(@WebParam(name = "destino") String destino,
            @WebParam(name="asunto") String asunto, 
            @WebParam(name="contenidoHTML") String contenidoHTML) {
        return correoBO.envioDeCorreos(destino, asunto, contenidoHTML);
    }
}
