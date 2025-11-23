/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package biblioteca.correo;

import biblioteca.dao.CorreoImpl;

/**
 *
 * @author renat
 */
public class CorreoBOImpl {
    private CorreoImpl correoDAO;
    public CorreoBOImpl(){
        correoDAO = new CorreoImpl();
    }
    public boolean envioDeCorreos( String destino, String asunto, String contenidoHTML){
        return correoDAO.envioDeCorreos(destino, asunto, contenidoHTML);
    }
}
