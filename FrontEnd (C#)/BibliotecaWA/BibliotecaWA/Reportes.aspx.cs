using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BibliotecaWA
{
    public partial class Reportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrar_click(object sender, EventArgs e)
        {
            if (chkReporteEjemplares != null && chkReporteEjemplares.Checked)
            {
                Response.Redirect("http://localhost:8080/BibliotecaWS/ReporteReq25");
                return;
            }

            if (chkReporteUsuarios != null && chkReporteUsuarios.Checked)
            {
                Response.Redirect("http://localhost:8080/BibliotecaWS/ReporteReq24");
                return;
            }

            if (chkReporteLibros != null && chkReporteLibros.Checked)
            {
                //Response.Redirect("ReporteLibros.aspx");
                return;
            }

            // Agrega aquí más checkboxes si tienes más
            // if (chkOtro.Checked) { Response.Redirect("OtroReporte.aspx"); return; }

            // Si no se marcó nada
            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "alerta",
                "alert('Por favor seleccione un tipo de reporte antes de descargar.');",
                true
            );

        }
    }
}