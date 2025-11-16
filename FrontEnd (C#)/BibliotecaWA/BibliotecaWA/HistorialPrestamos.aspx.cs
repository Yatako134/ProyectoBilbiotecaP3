using BibliotecaWA.BibliotecaServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BibliotecaWA
{
    public partial class HistorialPrestamos : System.Web.UI.Page
    {
        private PrestamoWSClient boprestamo;
        private SancionWSClient bosancion;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bosancion = new SancionWSClient();
                boprestamo = new PrestamoWSClient();
                prestamo[] prest = boprestamo.listarPrestamos();
                sancion[] saci = bosancion.listarSanciones();
                if (prest != null)
                {
                    Session["prestamos"] = new BindingList<prestamo>(prest);
                }
                else
                {
                    LabelMensajePrestamo.Text = "No hay prestamos que mostrar.";
                    LabelMensajePrestamo.Visible = true;
                }
                if (saci != null)
                {
                    Session["sanciones"] = new BindingList<sancion>(saci);
                }
                else
                {
                    lblMensaje.Text = "No hay sanciones que mostrar.";
                    lblMensaje.Visible = true;
                }

                CargarPrestamos();
                CargarSanciones();

                // Botón seleccionado (al iniciar: Préstamos) -> azul suave
                btnPrestamos.CssClass = "btn btn-sm btn-primary me-1";

                // Botón no seleccionado mantiene estilo original (sin sombreado)
                btnSanciones.CssClass = "btn btn-sm btn-outline-secondary";
            }
        }

        protected void btnPrestamos_Click(object sender, EventArgs e)
        {
            pnlPrestamos.Visible = true;
            pnlSanciones.Visible = false;

            // Prestamos activo (azul suave)
            btnPrestamos.CssClass = "btn btn-sm btn-primary me-1";

            // Sanciones vuelve al estilo original (sin sombreado)
            btnSanciones.CssClass = "btn btn-sm btn-outline-secondary";


            CargarPrestamos();
        }

        protected void btnSanciones_Click(object sender, EventArgs e)
        {
            pnlPrestamos.Visible = false;
            pnlSanciones.Visible = true;

            // Prestamos vuelve al estilo original (sin sombreado)
            btnPrestamos.CssClass = "btn btn-sm btn-outline-secondary";

            // Sanciones activo (azul suave)
            btnSanciones.CssClass = "btn btn-sm btn-primary me-1";

            CargarSanciones();
        }

        private void CargarPrestamos()
        {
            BindingList<prestamo> prestamos = (BindingList<prestamo>)Session["prestamos"];
            gvPrestamos.DataSource = prestamos;
            gvPrestamos.DataBind();
            ActualizarContador();

        }

        protected void dgvPrestamo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                prestamo presta = (prestamo)e.Row.DataItem;
                e.Row.Cells[0].Text = presta.idPrestamo.ToString();
                e.Row.Cells[1].Text = presta.usuario.codigo.ToString();
                e.Row.Cells[2].Text = presta.fecha_de_prestamo.ToString("d 'de' MMM, yyyy");
                e.Row.Cells[3].Text = presta.fecha_vencimiento.ToString("d 'de' MMM, yyyy");
                e.Row.Cells[4].Text = (presta.fecha_devolucion == DateTime.MinValue) ? "-" : presta.fecha_devolucion.ToString("d 'de' MMM, yyyy");
            }
        }
        protected void dgvSanciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                sancion san = (sancion)e.Row.DataItem;
                e.Row.Cells[0].Text = san.id_sancion.ToString();
                e.Row.Cells[1].Text = san.prestamo.usuario.codigo.ToString();
                e.Row.Cells[2].Text = san.prestamo.idPrestamo.ToString();
                e.Row.Cells[3].Text = san.fecha_inicio.ToString("d 'de' MMM, yyyy");
                e.Row.Cells[4].Text = san.fecha_fin.ToString("d 'de' MMM, yyyy");
            }
        }
        private void ActualizarContador()
        {
            int total = 0;
            if (((BindingList<prestamo>)Session["prestamos"]) != null)
            {
                total = ((BindingList<prestamo>)Session["prestamos"]).Count;
            }

            int mostrados = gvPrestamos.Rows.Count;
            lblResultados.Text = $"Mostrando {mostrados} de {total} usuarios";
        }
        private void ActualizarContadorSancion()
        {
            int total = 0;
            if (((BindingList<sancion>)Session["Sanciones"]) != null)
            {
                total = ((BindingList<sancion>)Session["Sanciones"]).Count;
            }
            int mostrados = gvSanciones.Rows.Count;
            LabelSancion.Text = $"Mostrando {mostrados} de {total} usuarios";
        }
        private void CargarSanciones()
        {
            BindingList<sancion> sanciones = (BindingList<sancion>)Session["sanciones"];
            gvSanciones.DataSource = sanciones;
            gvSanciones.DataBind();
            ActualizarContadorSancion();
        }

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
                    clases = $"{clases} border border-danger text-danger bg-light";
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

        protected void ddlCantidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvPrestamos.PageSize = int.Parse(ddlCantidad.SelectedValue);
            CargarPrestamos();
        }

        // ======= BOTONES DE OPCIONES =======
        protected void btnVer_Click(object sender, EventArgs e)
        {
            int id = int.Parse(hfPrestamoSeleccionado.Value);
            Response.Redirect($"DetallePrestamo_Sancion.aspx?id={id}&modo=ver");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(hfPrestamoSeleccionado.Value);
            Response.Redirect($"DetallePrestamo_Sancion.aspx?id={id}&modo=editar");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(hfPrestamoSeleccionado.Value);
            PrestamoWSClient boprestamo = new PrestamoWSClient();
            boprestamo.eliminarPrestamo(id);

            BindingList<prestamo> prestamos = new BindingList<prestamo>(boprestamo.listarPrestamos());
            Session["prestamos"] = prestamos;
            CargarPrestamos();
        }

        protected void btnBuscarPrestamo_Click(object sender, EventArgs e)
        {
            boprestamo = new PrestamoWSClient();
            string codigo_a_buscar = txtBuscar.Text.Trim();

            prestamo[] prestamos_busqueda;
            if (codigo_a_buscar == "")
            {
                prestamos_busqueda = boprestamo.listarPrestamos();
            }
            else
            {
                prestamos_busqueda = boprestamo.buscarPrestamos(int.Parse(txtBuscar.Text.Trim()));
            }
            if (prestamos_busqueda != null)
            {
                Session["prestamos"] = new BindingList<prestamo>(prestamos_busqueda);
                LabelMensajePrestamo.Visible = false;
            }
            else
            {
                Session["prestamos"] = new BindingList<prestamo>();
                LabelMensajePrestamo.Text = "No se encontraron resultados para la búsqueda.";
                LabelMensajePrestamo.Visible = true;
            }
            CargarPrestamos();
        }


        // Sanciones
        protected void gvSanciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSanciones.PageIndex = e.NewPageIndex;
            CargarSanciones();
        }
        protected void ddlCantidadSancion_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvSanciones.PageSize = int.Parse(ddlCantidadSancion.SelectedValue);
            CargarSanciones();
        }

        protected void btnVerSancion_Click(object sender, EventArgs e)
        {

        }

        protected void btnEditarSancion_Click(object sender, EventArgs e)
        {

        }

        protected void btnEliminarSancion_Click(object sender, EventArgs e)
        {

        }
        protected void btnBuscarSancion_Click(object sender, EventArgs e)
        {
            bosancion = new SancionWSClient();
            string codigo_a_buscar = TextBoxSancion.Text.Trim();

            sancion[] sanciones_busqueda;
            if (codigo_a_buscar == "")
            {
                sanciones_busqueda = bosancion.listarSanciones();
            }
            else
            {
                sanciones_busqueda = bosancion.BusquedaSanciones(int.Parse(TextBoxSancion.Text.Trim()));
            }
            if (sanciones_busqueda != null)
            {
                Session["sanciones"] = new BindingList<sancion>(sanciones_busqueda);
                lblMensaje.Visible = false;
            }
            else
            {
                Session["sanciones"] = new BindingList<sancion>();
                lblMensaje.Text = "No se encontraron resultados para la búsqueda.";
                lblMensaje.Visible = true;
            }
            CargarSanciones();
        }
    }
}