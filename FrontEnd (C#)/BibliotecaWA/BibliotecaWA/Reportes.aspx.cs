using BibliotecaWA.BibliotecaServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Forms;
namespace BibliotecaWA
{
    public partial class Reportes : System.Web.UI.Page
    {
        UsuarioWSClient bousuario = new UsuarioWSClient();
        EjemplarWSClient boejemplar = new EjemplarWSClient();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrar_click(object sender, EventArgs e)
        {
            // 1. Contar cuántos checkboxes están seleccionados
            int seleccionados = 0;

            if (chkReporteEjemplares.Checked) seleccionados++;
            if (chkReporteUsuarios.Checked) seleccionados++;
            if (chkReporteLibros.Checked) seleccionados++;

            // 2. Si NO hay ninguno seleccionado
            if (seleccionados == 0)
            {
                ScriptManager.RegisterStartupScript(
                    this, GetType(), "modal",
                    "mostrarModal('Advertencia', 'Por favor, seleccione un tipo de reporte antes de descargar.');",
                    true
                );
                return;
            }

            // 3. Si hay más de uno → ERROR
            if (seleccionados > 1)
            {
                ScriptManager.RegisterStartupScript(
                    this, GetType(), "modal",
                    "mostrarModal('Advertencia', 'Seleccione solo un reporte a la vez.');",
                    true
                );
                return;
            }

            // 4. Solo si hay EXACTAMENTE uno → hacemos redirect
            if (chkReporteEjemplares.Checked)
            {
                //BindingList<ejemplar> ejemplares = boejemplar.listarEjemplaresTodos();
                ejemplar[] ejemp = boejemplar.listarEjemplaresTodos();

                // Validar que la lista no sea null
                if (ejemp == null)
                {
                    ScriptManager.RegisterStartupScript(
                        this, GetType(), "modal",
                        "mostrarModal('Advertencia', 'No hay ejemplares en reparacion.');",
                        true
                    );
                    return;
                }

                int cantEnReparacion = ejemp.Count(ejem => ejem.estado == estadoEjemplar.EN_REPARACION);

                if (cantEnReparacion > 0)
                {
                    usuario user = new usuario();
                    user = bousuario.obtenerUsuarioPorId(21);
                    //string nombre = "Luchexx";
                    string nombre = user.nombre;
                    Response.Redirect($"http://localhost:8080/BibliotecaWS/ReporteReq25?nombre={nombre}");
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(
                        this, GetType(), "modal",
                        "mostrarModal('Advertencia', 'No hay ejemplares en reparacion.');",
                        true
                    );
                    return;
                }
                //usuario user = new usuario();
                //user = bousuario.obtenerUsuarioPorId(21);
                ////string nombre = "Luchexx";
                //string nombre = user.nombre;
                //Response.Redirect($"http://localhost:8080/BibliotecaWS/ReporteReq25?nombre={nombre}");
                //return;
            }

            if (chkReporteUsuarios.Checked)
            {
                string fechaInicio = txtFechaInicio1.Text;
                string fechaFin = txtFechaFin1.Text;
                if (string.IsNullOrEmpty(fechaInicio) || string.IsNullOrEmpty(fechaFin))
                {
                    ShowModal("Debes ingresar ambas fechas.");
                    return;
                }

                if (!DateTime.TryParse(fechaInicio, out DateTime fechaIni))
                {
                    ShowModal("La fecha inicial no es válida.");
                    return;
                }

                if (!DateTime.TryParse(fechaFin, out DateTime fechaF))
                {
                    ShowModal("La fecha final no es válida.");
                    return;
                }

                if (fechaIni > fechaF)
                {
                    ShowModal("La fecha inicial no puede ser mayor que la fecha final.");
                    return;
                }

                // Validación de fecha muy adelantada (opcional)
                if ((fechaF - DateTime.Now).TotalDays > 365)
                {
                    ShowModal("La fecha final es demasiado adelantada.");
                    return;
                }
                SancionWSClient sancionbo = new SancionWSClient();
                int cant = sancionbo.contarSancionesPorFechas(fechaIni, fechaF);
                if (cant <= 0)
                {
                    ShowModal("No hay sanciones para mostrar en el rango de fecha seleccionado.");
                    return;
                }
                string nombre = (String)Session["UserName"];
                string url = $"http://localhost:8080/BibliotecaWS/ReporteReq24?fechaInicio={fechaIni:yyyy-MM-dd}&fechaFin={fechaF:yyyy-MM-dd}&nombre={nombre}";


                Response.Redirect(url);
                return;
            }

            if (chkReporteLibros.Checked)
            {
                Response.Redirect("http://localhost:8080/BibliotecaWS/ReporteLibros");
                return;
            }

        }
        private void ShowModal(string mensaje)
        {
            string script = $@"
            var modal = new bootstrap.Modal(document.getElementById('modalAlert'));
            document.getElementById('modalBody').innerText = '{mensaje}';
            modal.show();";
            ScriptManager.RegisterStartupScript(this, GetType(), "showModal", script, true);
        }

    }
}