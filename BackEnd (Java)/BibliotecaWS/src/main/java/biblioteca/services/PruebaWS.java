
package biblioteca.services;

import jakarta.jws.WebService;
import jakarta.jws.WebMethod;
import jakarta.jws.WebParam;
import java.time.LocalDate;

@WebService(serviceName = "PruebaWS")
public class PruebaWS {

   
    @WebMethod(operationName = "saludo")
    public String saludo(@WebParam(name = "nombre") String nombre) {
        return "¡Hola, " + nombre + "! Bienvenido al servicio SOAP de prueba.";
    }

    @WebMethod(operationName = "sumar")
    public int sumar(@WebParam(name = "a") int a, @WebParam(name = "b") int b) {
        return a + b;
    }

    @WebMethod(operationName = "restar")
    public int restar(@WebParam(name = "a") int a, @WebParam(name = "b") int b) {
        return a - b;
    }

    @WebMethod(operationName = "multiplicar")
    public int multiplicar(@WebParam(name = "a") int a, @WebParam(name = "b") int b) {
        return a * b;
    }

    @WebMethod(operationName = "dividir")
    public double dividir(@WebParam(name = "a") double a, @WebParam(name = "b") double b) {
        if (b == 0) return Double.NaN; // evita división por cero
        return a / b;
    }

    @WebMethod(operationName = "esPar")
    public boolean esPar(@WebParam(name = "numero") int numero) {
        return numero % 2 == 0;
    }

    @WebMethod(operationName = "concatenar")
    public String concatenar(@WebParam(name = "texto1") String texto1, 
                             @WebParam(name = "texto2") String texto2) {
        return texto1 + " " + texto2;
    }

    @WebMethod(operationName = "obtenerFechaActual")
    public String obtenerFechaActual() {
        return "La fecha actual es: " + LocalDate.now();
    }

    @WebMethod(operationName = "generarMensajePersonalizado")
    public String generarMensajePersonalizado(
            @WebParam(name = "nombre") String nombre,
            @WebParam(name = "edad") int edad) {
        if (edad < 18)
            return "Hola " + nombre + ", eres menor de edad.";
        else
            return "Hola " + nombre + ", eres mayor de edad.";
    }
}
