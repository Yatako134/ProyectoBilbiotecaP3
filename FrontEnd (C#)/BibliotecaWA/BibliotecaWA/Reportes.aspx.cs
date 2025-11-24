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
                    this, GetType(), "alerta",
                    "alert('Por favor, seleccione un tipo de reporte antes de descargar.');",
                    true
                );
                return;
            }

            // 3. Si hay más de uno → ERROR
            if (seleccionados > 1)
            {
                ScriptManager.RegisterStartupScript(
                    this, GetType(), "alerta",
                    "alert('Seleccione solo un reporte a la vez.');",
                    true
                );
                return;
            }

            // 4. Solo si hay EXACTAMENTE uno → hacemos redirect
            if (chkReporteEjemplares.Checked)
            {
                usuario user = new usuario();
                user = bousuario.obtenerUsuarioPorId(2);
                //string nombre = "Luchexx";
                string nombre = user.nombre;
                Response.Redirect($"http://localhost:8080/BibliotecaWS/ReporteReq25?nombre={nombre}");
                return;
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
                /*
                // 1️⃣ Validar que ambos campos tengan algo
                if (string.IsNullOrEmpty(fechaInicio) || string.IsNullOrEmpty(fechaFin))
                {
                    MessageBox.Show(
                        "Debes ingresar ambas fechas.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // 2️⃣ Intentar convertir a fecha
                DateTime fechaIni, fechaF;

                if (!DateTime.TryParse(fechaInicio, out fechaIni))
                {
                    MessageBox.Show(
                        "La fecha inicial no es válida.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                if (!DateTime.TryParse(fechaFin, out fechaF))
                {
                    MessageBox.Show(
                        "La fecha final no es válida.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                // 3️⃣ Validar rango
                if (fechaIni > fechaF)
                {
                    MessageBox.Show(
                        "La fecha inicial no puede ser mayor que la fecha final.",
                        "Rango inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }
                // ✅ 4️⃣ Formato CORRECTO para el servlet
                string fechaInicioFmt = fechaIni.ToString("yyyy-MM-dd");
                string fechaFinFmt = fechaF.ToString("yyyy-MM-dd");
                */
                string url = $"http://localhost:8080/BibliotecaWS/ReporteReq24?fechaInicio={fechaIni:yyyy-MM-dd}&fechaFin={fechaF:yyyy-MM-dd}";

                //string url = $"http://localhost:8080/BibliotecaWS/ReporteReq24?fechaInicio={fechaInicioFmt}&fechaFin={fechaFinFmt}";
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