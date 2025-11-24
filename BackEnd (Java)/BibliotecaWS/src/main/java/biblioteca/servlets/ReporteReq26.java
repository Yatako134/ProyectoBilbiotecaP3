package biblioteca.servlets;

import biblioteca.config.DBManager;
import java.io.IOException;
import java.io.PrintWriter;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import java.awt.Image;
import java.io.InputStream;
import java.net.URL;
import java.sql.Connection;
import java.util.HashMap;
import javax.swing.ImageIcon;
import net.sf.jasperreports.engine.JREmptyDataSource;
import net.sf.jasperreports.engine.JRException;
import net.sf.jasperreports.engine.JasperExportManager;
import net.sf.jasperreports.engine.JasperFillManager;
import net.sf.jasperreports.engine.JasperPrint;
import net.sf.jasperreports.engine.JasperReport;
import net.sf.jasperreports.engine.util.JRLoader;

public class ReporteReq26 extends HttpServlet {

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
                + "ReporteReq26.jasper"));
            
            
            URL rutaImagen1
            = getClass().getResource
        ("/pe/edu/pucp/utilsarmy/images/banner.png");
            Image imagen1 =
            (new ImageIcon(rutaImagen1)).getImage();
//            
            URL rutaImagen2
            = getClass().getResource
        ("/pe/edu/pucp/utilsarmy/images/Isologo.png");
            Image imagen2 =
            (new ImageIcon(rutaImagen2)).getImage();

            HashMap hm = new HashMap();
            hm.put("Nombre Usuario", "FREDDY");
            hm.put("Banner", imagen1);
            hm.put("Logo", imagen2);
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
