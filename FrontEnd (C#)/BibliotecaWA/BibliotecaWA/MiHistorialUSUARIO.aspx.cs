using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BibliotecaWA
{
    public partial class MiHistorialUSUARIO : System.Web.UI.Page
    {
        private PrestamoBOImpl boprestamo;
        private SancionBOImpl bosancion;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPrestamos(); // Por defecto carga préstamos
                btnPrestamos.CssClass = "btn btn-sm btn-secondary me-1";
                btnSanciones.CssClass = "btn btn-sm btn-secondary";
            }
        }

        protected void btnPrestamos_Click(object sender, EventArgs e)
        {
            pnlPrestamos.Visible = true;
            pnlSanciones.Visible = false;
            btnPrestamos.CssClass = "btn btn-sm btn-secondary me-1";
            btnSanciones.CssClass = "btn btn-sm btn-secondary";
            CargarPrestamos();
        }

        protected void btnSanciones_Click(object sender, EventArgs e)
        {
            pnlPrestamos.Visible = false;
            pnlSanciones.Visible = true;
            btnPrestamos.CssClass = "btn btn-sm btn-secondary me-1";
            btnSanciones.CssClass = "btn btn-sm btn-secondary";
            CargarSanciones();
        }

        private void CargarPrestamos()
        {
            boprestamo = new PrestamoBOImpl();
            var lista = boprestamo.listarTodos();
            gvPrestamos.DataSource = lista;
            gvPrestamos.DataBind();

            // Información de la página
            int totalPaginas = (int)Math.Ceiling((double)lista.Count / gvPrestamos.PageSize);
            lblPaginaInfoPrestamos.Text = $"Página {gvPrestamos.PageIndex + 1} de {totalPaginas}";
        }

        private void CargarSanciones()
        {
            bosancion = new SancionBOImpl();
            var lista = bosancion.listarTodos();
            gvSanciones.DataSource = lista;
            gvSanciones.DataBind();

            int totalPaginas = (int)Math.Ceiling((double)lista.Count / gvSanciones.PageSize);
            lblPaginaInfoSanciones.Text = $"Página {gvSanciones.PageIndex + 1} de {totalPaginas}";
        }

        /// <summary>
        /// Genera el HTML para mostrar el estado como una "badge" con estilo.
        /// Usa: "vigente", "atrasado", "finalizado" (case-insensitive).
        /// </summary>
        /// <param name="estadoObj">valor del estado (puede venir de la BD)</param>
        /// <returns>string HTML con span y clases bootstrap</returns>
        protected string GetEstadoHtml(object estadoObj)
        {
            if (estadoObj == null) return string.Empty;

            string estado = estadoObj.ToString().Trim().ToLower();

            // Valores por defecto de estilo
            string clases = "fw-bold text-uppercase small rounded px-2 py-1";
            string texto = estado.ToUpper();

            switch (estado)
            {
                case "vigente":
                    clases = $"{clases} border border-success text-success bg-light"; // fondo claro
                    texto = "VIGENTE";
                    break;

                case "atrasado":
                    clases = $"{clases} border border-danger text-danger bg-light"; // fondo claro
                    texto = "ATRASADO";
                    break;

                case "finalizado":
                case "finalizada":
                case "finalizadoa":
                    clases = $"{clases} border border-dark text-dark bg-light"; // fondo claro
                    texto = "FINALIZADO";
                    break;

                default:
                    clases = $"{clases} border border-secondary text-secondary bg-light";
                    texto = estado.ToUpper();
                    break;
            }

            // Retornamos HTML inline (el GridView lo renderiza como HTML)
            return $"<span class=\"{HttpUtility.HtmlAttributeEncode(clases)}\">{HttpUtility.HtmlEncode(texto)}</span>";
        }
        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblEstado = (Label)e.Row.FindControl("lblEstado");
                if (lblEstado != null)
                {
                    string estado = lblEstado.Text.Trim().ToLower();

                    lblEstado.CssClass = "estado-label ";

                    if (estado == "vigente")
                        lblEstado.CssClass += "estado-vigente";
                    else if (estado == "atrasado")
                        lblEstado.CssClass += "estado-atrasado";
                    else if (estado == "finalizado")
                        lblEstado.CssClass += "estado-finalizado";
                }
            }
        }
        // Prestamos
        protected void gvPrestamos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPrestamos.PageIndex = e.NewPageIndex;
            CargarPrestamos(); // Recargar datos con el nuevo índice
        }

        protected void ddlPageSizePrestamos_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvPrestamos.PageSize = int.Parse(ddlPageSizePrestamos.SelectedValue);
            gvPrestamos.PageIndex = 0; // Reiniciamos a la primera página
            CargarPrestamos();
        }

        // Sanciones
        protected void gvSanciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSanciones.PageIndex = e.NewPageIndex;
            CargarSanciones();
        }

        protected void ddlPageSizeSanciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvSanciones.PageSize = int.Parse(ddlPageSizeSanciones.SelectedValue);
            gvSanciones.PageIndex = 0;
            CargarSanciones();
        }
    }
}