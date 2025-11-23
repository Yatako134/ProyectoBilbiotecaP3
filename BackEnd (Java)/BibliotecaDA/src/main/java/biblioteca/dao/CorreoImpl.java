/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package biblioteca.dao;

import jakarta.mail.*;
import jakarta.mail.internet.*;
import jakarta.activation.*;
import jakarta.mail.util.ByteArrayDataSource;
import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author renat
 */
public class CorreoImpl {

    private String correoDeOrigen;
    private String correoDeDestino;
    private String asunto;
    private String mensajeDeTexto;
    private String contraseña16Digitos;
    private InputStream rutaLogo;

    public CorreoImpl() {
        correoDeOrigen = "utilsarmysistemabiblioteca@gmail.com";
        contraseña16Digitos = "adze tffa borr ndbg";
        rutaLogo= getClass().getResourceAsStream("/Isologo.png");
    }

    private boolean envioDeMensajes() throws IOException {
        try {
            Properties p = new Properties();
            p.put("mail.smtp.host", "smtp.gmail.com");
            p.setProperty("mail.smtp.starttls.enable", "true");
            p.put("mail.smtp.ssl.trust", "smtp.gmail.com");
            p.setProperty("mail.smtp.port", "587");
            p.setProperty("mail.smtp.user", correoDeOrigen);
            p.setProperty("mail.smtp.ssl.protocols", "TLSv1.2");
            p.setProperty("mail.smtp.auth", "true");

            Session s = Session.getInstance(p);
            MimeMessage mensaje = new MimeMessage(s);
            mensaje.setFrom(new InternetAddress(correoDeOrigen));
            mensaje.addRecipient(Message.RecipientType.TO, new InternetAddress(correoDeDestino));
            mensaje.setSubject(asunto);
            mensaje.setSentDate(new java.util.Date());

            // Parte 1: Cuerpo HTML con referencia al logo
            MimeBodyPart cuerpoHtml = new MimeBodyPart();
            cuerpoHtml.setContent(mensajeDeTexto, "text/html; charset=UTF-8");

            // Parte 2: Imagen del logo como recurso embebido
            MimeBodyPart imagenLogo = new MimeBodyPart();
            DataSource fds = new ByteArrayDataSource(rutaLogo, "image/png");
            imagenLogo.setDataHandler(new DataHandler(fds));
            imagenLogo.setHeader("Content-ID", "<logo>");
            imagenLogo.setDisposition(MimeBodyPart.INLINE);

            // Parte 3: Ensamblar todo
            Multipart contenidoCorreo = new MimeMultipart();
            contenidoCorreo.addBodyPart(cuerpoHtml);
            contenidoCorreo.addBodyPart(imagenLogo);
            mensaje.setContent(contenidoCorreo);

            Transport t = s.getTransport("smtp");
            t.connect(correoDeOrigen, contraseña16Digitos);
            t.sendMessage(mensaje, mensaje.getAllRecipients());
            t.close();

            Logger.getLogger(CorreoImpl.class.getName()).log(Level.INFO, "Correo enviado exitosamente");
            return true;

        } catch (MessagingException e) {
            Logger.getLogger(CorreoImpl.class.getName()).log(Level.SEVERE, "Error al enviar correo", e);
            return false;
        }
    }

// Método envioDeCorreos() actualizado para incluir rutaLogo
    public boolean envioDeCorreos( String destino, String asunto, String contenidoHTML) {
        boolean resultado = false;

        try {
            // Asignar valores a las variables de instancia
            this.correoDeDestino = destino;
            this.asunto = asunto;
            this.mensajeDeTexto = contenidoHTML;
            if (rutaLogo == null && this.rutaLogo == null) {
                throw new IOException("No se pudo cargar el logo desde los recursos.");
            }
            // Llamar al método privado que hace el envío
            resultado = envioDeMensajes();

            // Mostrar mensaje de éxito (como en enviarEmail)
            if (resultado) {
                Logger.getLogger(CorreoImpl.class.getName()).log(Level.INFO, "Correo enviado exitosamente.");
            }

        } catch (IOException e) {
            Logger.getLogger(CorreoImpl.class.getName()).log(Level.SEVERE, "Error al enviar correo", e);
            resultado = false;
        }

        return resultado;
    }

}
