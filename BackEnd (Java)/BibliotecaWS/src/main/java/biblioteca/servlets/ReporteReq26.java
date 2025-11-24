//package biblioteca.servlets;
//
//import biblioteca.config.DBManager;
//import java.io.IOException;
//import java.io.PrintWriter;
//import jakarta.servlet.ServletException;
//import jakarta.servlet.http.HttpServlet;
//import jakarta.servlet.http.HttpServletRequest;
//import jakarta.servlet.http.HttpServletResponse;
//import java.awt.Image;
//import java.io.InputStream;
//import java.net.URL;
//import java.sql.Connection;
//import java.util.HashMap;
//import javax.swing.ImageIcon;
//import net.sf.jasperreports.engine.JREmptyDataSource;
//import net.sf.jasperreports.engine.JRException;
//import net.sf.jasperreports.engine.JasperExportManager;
//import net.sf.jasperreports.engine.JasperFillManager;
//import net.sf.jasperreports.engine.JasperPrint;
//import net.sf.jasperreports.engine.JasperReport;
//import net.sf.jasperreports.engine.util.JRLoader;
//
//public class ReporteReq26 extends HttpServlet {
//
//    protected void processRequest(HttpServletRequest request, HttpServletResponse response)
//            throws ServletException, IOException {
//        try{
//            Connection con = DBManager.getInstance()
//                    .getConnection();
//            JasperReport jr
//            = (JasperReport)
//                JRLoader.loadObject(getClass().
//                    getResourceAsStream
//        ("/pe/edu/pucp/utilsarmy/reports/"
//                + "ReporteReq26.jasper"));
//            
//            
//            URL rutaImagen1
//            = getClass().getResource
//        ("/pe/edu/pucp/utilsarmy/images/banner.png");
//            Image imagen1 =
//            (new ImageIcon(rutaImagen1)).getImage();
////            
//            URL rutaImagen2
//            = getClass().getResource
//        ("/pe/edu/pucp/utilsarmy/images/Isologo.png");
//            Image imagen2 =
//            (new ImageIcon(rutaImagen2)).getImage();
//
//            String nombre = request.getParameter("nombre");
//            HashMap hm = new HashMap();
//            hm.put("Nombre Usuario", nombre);
//            hm.put("Banner", imagen1);
//            hm.put("Logo", imagen2);
//            JasperPrint jp
//            = JasperFillManager.fillReport(jr,hm,
//                    con);
//            JasperExportManager.exportReportToPdfStream(jp,
//                    response.getOutputStream());
//        }catch(IOException | JRException ex){
//            System.out.println("ERROR GENERANDO EL REPORTE:" +
//                    ex.getMessage());
//        }finally{
//            DBManager.getInstance().cerrarConexion();
//        }
//
//
//    }
//
//    @Override
//    protected void doGet(HttpServletRequest request, HttpServletResponse response)
//            throws ServletException, IOException {
//        processRequest(request, response);
//    }
//
//    @Override
//    protected void doPost(HttpServletRequest request, HttpServletResponse response)
//            throws ServletException, IOException {
//        processRequest(request, response);
//    }
//
//    @Override
//    public String getServletInfo() {
//        return "Short description";
//    }
//
//}
package biblioteca.servlets;

import biblioteca.config.DBManager;
import java.io.IOException;
import java.sql.Connection;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import java.awt.Image;
import java.net.URL;
import javax.swing.ImageIcon;
import net.sf.jasperreports.engine.JRException;
import net.sf.jasperreports.engine.JasperExportManager;
import net.sf.jasperreports.engine.JasperFillManager;
import net.sf.jasperreports.engine.JasperPrint;
import net.sf.jasperreports.engine.JasperReport;
import net.sf.jasperreports.engine.util.JRLoader;

public class ReporteReq26 extends HttpServlet {

    protected void processRequest(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        Connection con = null;
        try {
            con = DBManager.getInstance().getConnection();
            
            // Cargar reporte
            JasperReport jr = (JasperReport) JRLoader.loadObject(
                getClass().getResourceAsStream("/pe/edu/pucp/utilsarmy/reports/ReporteReq26.jasper")
            );
            
            // Cargar imágenes
            URL rutaImagen1 = getClass().getResource("/pe/edu/pucp/utilsarmy/images/banner.png");
            URL rutaImagen2 = getClass().getResource("/pe/edu/pucp/utilsarmy/images/Isologo.png");
            
            // Obtener parámetros del request
            String nombre = request.getParameter("nombre");
            String fechaInicioStr = request.getParameter("fechaInicio"); // yyyy-MM-dd
            String fechaFinStr = request.getParameter("fechaFin");
            
            // Parsear fechas
            SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
            Date fechaInicio = null;
            Date fechaFin = null;
            
            if (fechaInicioStr != null && !fechaInicioStr.isEmpty()) {
                try {
                    fechaInicio = sdf.parse(fechaInicioStr);
                } catch (ParseException e) {
                    throw new ServletException("Error al parsear fechaInicio: " + fechaInicioStr, e);
                }
            }
            
            if (fechaFinStr != null && !fechaFinStr.isEmpty()) {
                try {
                    fechaFin = sdf.parse(fechaFinStr);
                } catch (ParseException e) {
                    throw new ServletException("Error al parsear fechaFin: " + fechaFinStr, e);
                }
            }
            
            // Crear parámetros para el reporte
            HashMap<String, Object> hm = new HashMap<>();
            hm.put("Nombre Usuario", nombre);
            
            // Agregar fechas como java.sql.Date
            if (fechaInicio != null) {
                hm.put("Fecha_Inicio", new java.sql.Date(fechaInicio.getTime()));
            }
            if (fechaFin != null) {
                hm.put("Fecha_Fin", new java.sql.Date(fechaFin.getTime()));
            }
            
            // Agregar imágenes
            if (rutaImagen1 != null) {
                Image imagen1 = (new ImageIcon(rutaImagen1)).getImage();
                hm.put("Banner", imagen1);
            }
            
            if (rutaImagen2 != null) {
                Image imagen2 = (new ImageIcon(rutaImagen2)).getImage();
                hm.put("Logo", imagen2);
            }
            
            // Generar reporte
            JasperPrint jp = JasperFillManager.fillReport(jr, hm, con);
            
            // Configurar respuesta
            response.setContentType("application/pdf");
            response.setHeader("Content-Disposition", "inline; filename=reporte_prestamos.pdf");
            
            // Exportar
            JasperExportManager.exportReportToPdfStream(jp, response.getOutputStream());
            
        } catch (ServletException | IOException | JRException ex) {
            System.out.println("ERROR GENERANDO EL REPORTE: " + ex.getMessage());
            response.getWriter().println("ERROR: " + ex.getMessage());
        } finally {
            if (con != null) {
                DBManager.getInstance().cerrarConexion();
            }
        }
    }

    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        processRequest(request, response);
    }

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        processRequest(request, response);
    }

    @Override
    public String getServletInfo() {
        return "Reporte de Préstamos con Fechas";
    }
}