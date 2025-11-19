package biblioteca.servlets;

import biblioteca.config.DBManager;
import java.io.IOException;
import java.io.PrintWriter;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import java.awt.Image;
import java.net.URL;
import java.sql.Connection;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import javax.swing.ImageIcon;
import net.sf.jasperreports.engine.JRException;
import net.sf.jasperreports.engine.JasperExportManager;
import net.sf.jasperreports.engine.JasperFillManager;
import net.sf.jasperreports.engine.JasperPrint;
import net.sf.jasperreports.engine.JasperReport;
import net.sf.jasperreports.engine.util.JRLoader;

public class ReporteReq24 extends HttpServlet {

    protected void processRequest(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        try{
            Connection con = DBManager.getInstance()
                    .getConnection();
            JasperReport jr
            = (JasperReport)
                JRLoader.loadObject(getClass().
                    getResourceAsStream
                    ("/pe/edu/pucp/utilsarmy/reports/"
                            + "ReporteRF24.jasper"));
            
            //Logo de la web
            URL rutaImagen
                = getClass().getResource
            ("/pe/edu/pucp/utilsarmy/images/logo2.png");
            
            Image imagen =
            (new ImageIcon(rutaImagen)).getImage();
            
            HashMap hm = new HashMap();
            hm.put("nombre", "Luchex");
            hm.put("logo", imagen);
            //Fecha de inicio y fin
            String fechaInicioStr = request.getParameter("fechaInicio"); // yyyy-MM-dd
            String fechaFinStr = request.getParameter("fechaFin");

            SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
            Date fechaInicio = null;
            Date fechaFin = null;

            try {
                fechaInicio = sdf.parse(fechaInicioStr);
            } catch (ParseException e) {
                throw new ServletException("Error al parsear fechaInicio: " + fechaInicioStr, e);
            }

            try {
                fechaFin = sdf.parse(fechaFinStr);
            } catch (ParseException e) {
                throw new ServletException("Error al parsear fechaFin: " + fechaFinStr, e);
            }

            hm.put("FECHA_INICIO", new java.sql.Date(fechaInicio.getTime()));
            hm.put("FECHA_FIN", new java.sql.Date(fechaFin.getTime()));
            System.out.println("RUTA .jasper = " + 
                getClass().getResource("/pe/edu/pucp/utilsarmy/reports/ReporteRF24.jasper"));

            
            JasperPrint jp
            = JasperFillManager.fillReport(jr,hm,
                    con);
            JasperExportManager.exportReportToPdfStream(jp,
                    response.getOutputStream());
        }catch(IOException | JRException ex){
            System.out.println("ERROR GENERANDO EL REPORTE:" +
                    ex.getMessage());
        }finally{
            DBManager.getInstance().cerrarConexion();
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
        return "Short description";
    }

}
