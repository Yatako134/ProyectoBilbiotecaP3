package biblioteca.main;

import biblioteca.config.DBManager;
import biblioteca.config.Encriptamiento;
import biblioteca.dao.CorreoImpl;
import biblioteca.gestionMaterial.boImpl.ContribuyenteBOImpl;
import biblioteca.gestionMaterial.boImpl.MaterialBiblioBOImpl;
import biblioteca.gestionMaterial.mysql.BibliotecaImpl;
import biblioteca.gestionMaterial.mysql.EjemplarImpl;
import biblioteca.gestionMaterial.mysql.Librolmpl;
import biblioteca.gestionMaterial.mysql.Tesislmpl;
import biblioteca.gestionPrestamo.boImpl.SancionBOImpl;
import biblioteca.gestionPrestamo.dao.PrestamoDAO;
import biblioteca.gestionPrestamo.mysql.PrestamoImpl;
import biblioteca.gestionUsuario.boImpl.UsuarioBOImpl;
import biblioteca.gestionUsuario.dao.RolDAO;
import biblioteca.gestionUsuario.mysql.RolImpl;
import biblioteca.gestionUsuario.mysql.UsuarioImpl;
import java.io.InputStream;
import java.sql.Connection;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Biblioteca;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Contribuyente;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Ejemplar;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.EstadoEjemplar;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.EstadoMaterial;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Libro;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.MaterialBibliografico;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.Tesis;
import pe.edu.pucp.utilsarmy.gestion_de_material.model.TipoContribuyente;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.EstadoPrestamo;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Prestamo;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Sancion;
import pe.edu.pucp.utilsarmy.gestion_de_prestamos.model.Tipo_sancion;
import pe.edu.pucp.utilsarmy.usuarios.model.Rol;
import pe.edu.pucp.utilsarmy.usuarios.model.Usuario;

public class Principal {

    public static void main(String[] args) throws Exception {
        CorreoImpl correo = new CorreoImpl();
        String destino = "a20212519@pucp.edu.pe";               // <-- A donde quieres enviarlo
        String asunto = "Buenas noches <3";
        String html = """
<html>
  <body>
    <h1>Hola usuario</h1>
    <p>Este es un mensaje con imagen.</p>
  </body>
</html>
""";
        correo.envioDeCorreos(destino, asunto, html);
    }
}
