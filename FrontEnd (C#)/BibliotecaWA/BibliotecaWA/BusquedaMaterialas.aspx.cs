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
    public partial class BusquedaMaterialas : System.Web.UI.Page
    {
        private MaterialWSClient materialBO;
        private BindingList<materialBibliografico> listaMateriales;
        private BibliotecaWSClient bibliotecaBO;
        private BindingList<biblioteca> bibliotecas;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                materialBO = new MaterialWSClient();
                listaMateriales = new BindingList<materialBibliografico>(materialBO.ListarTodos());
                Session["materiales"] = listaMateriales;
                CargarMateriales();
                CargarBibliotecas();
            }
        }

        private void CargarBibliotecas()
        {
            // Supongamos que obtienes una lista desde tu capa de negocio o BD
            if (bibliotecaBO == null) bibliotecaBO = new BibliotecaWSClient();

            bibliotecas = new BindingList<biblioteca>(bibliotecaBO.ListarTodas()); // Debe devolver lista con propiedades IdBiblioteca y Nombre

            ddlBiblioteca.DataSource = bibliotecas;
            ddlBiblioteca.DataTextField = "Nombre";
            ddlBiblioteca.DataValueField = "Nombre";
            ddlBiblioteca.DataBind();

            ddlBiblioteca.Items.Insert(0, new ListItem("-- Seleccione --", ""));

        }

        private void CargarMateriales(string filtro = "")
        {
            var materiales = Session["materiales"] as BindingList<materialBibliografico>;

            if (materiales != null && materiales.Count > 0)
            {
                gvResultados.DataSource = materiales;
                gvResultados.DataBind();
                ActualizarContador();

                int total = materiales.Count;
                int inicio = gvResultados.PageIndex * gvResultados.PageSize + 1;
                int fin = Math.Min((gvResultados.PageIndex + 1) * gvResultados.PageSize, total);
                lblPaginaInfo.Text = $"{inicio}-{fin} de {total}";
            }
            else
            {
                gvResultados.DataSource = null;
                gvResultados.DataBind();
                lblMensaje.Text = "No hay materiales bibliográficos para mostrar.";
                lblMensaje.Visible = true;
            }
        }
        private void ActualizarContador()
        {
            int total = ((BindingList<materialBibliografico>)Session["materiales"]).Count;
            int mostrados = gvResultados.Rows.Count;
            LabelBusqueda.Text = $"Mostrando {mostrados} de {total} usuarios";
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (materialBO == null) materialBO = new MaterialWSClient();
            var resultados = materialBO.Busqueda(txtBusqueda.Text)?.ToList();

            if (resultados == null || resultados.Count == 0)
            {
                gvResultados.DataSource = null;
                gvResultados.DataBind();
                lblMensaje.Text = "No se encontraron resultados.";
                lblMensaje.Visible = true;
            }
            else
            {
                listaMateriales = new BindingList<materialBibliografico>(resultados);
                Session["materiales"] = listaMateriales;

                gvResultados.PageIndex = 0;
                gvResultados.DataSource = listaMateriales;
                gvResultados.DataBind();
                lblMensaje.Visible = false;
            }
        }

        protected void btnBuscarAvanzado_Click(object sender, EventArgs e)
        {
            if (materialBO == null) materialBO = new MaterialWSClient();
            string titulo = string.IsNullOrWhiteSpace(txtTituloAvanzado.Text) ? null : txtTituloAvanzado.Text.Trim(); 
            string nombreContribuyente = string.IsNullOrWhiteSpace(txtNombreContribuyente.Text) ? null : txtNombreContribuyente.Text.Trim(); 
            string tema = string.IsNullOrWhiteSpace(txtTema.Text) ? null : txtTema.Text.Trim(); 

            int anioDesde = int.TryParse(txtAnioDesde.Text.Trim(), out int desde) ? desde : 0; 
            int anioHasta = int.TryParse(txtAnioHasta.Text.Trim(), out int hasta) ? hasta : 9999;
            string contribuyente = ddlContribuyente.SelectedIndex == 0 ? null : ddlContribuyente.SelectedValue; 
            string tipoMaterial = ddlTipoMaterial.SelectedIndex == 0 ? null : ddlTipoMaterial.SelectedValue; 
            string biblioteca = ddlBiblioteca.SelectedIndex == 0 ? null : ddlBiblioteca.SelectedValue; 
            string disponibilidad = ddlDisponibilidad.SelectedIndex == 0 ? null : ddlDisponibilidad.SelectedValue;

            var resultados = materialBO.BusquedaAvanzada(
                titulo,
                contribuyente,
                nombreContribuyente,
                tema,
                anioDesde,
                anioHasta,
                tipoMaterial,
                biblioteca,
                disponibilidad
            )?.ToList();

            if (resultados == null || resultados.Count == 0)
            {
                gvResultados.DataSource = null;
                gvResultados.DataBind();
                lblMensaje.Text = "No se encontraron resultados.";
                lblMensaje.Visible = true;
            }
            else
            {
                listaMateriales = new BindingList<materialBibliografico>(resultados);
                Session["materiales"] = listaMateriales;

                gvResultados.PageIndex = 0;
                gvResultados.DataSource = listaMateriales;
                gvResultados.DataBind();
                lblMensaje.Visible = false;
            }
        }

        protected void btnSolicitar_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            string id = btn.CommandArgument;
            // Aquí procesas la solicitud
            Response.Redirect("DetalleMaterial.aspx?id=" + id);
        }

        protected void gvResultados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvResultados.PageIndex = e.NewPageIndex;
            CargarMateriales();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvResultados.PageSize = int.Parse(ddlPageSize.SelectedValue);
            gvResultados.PageIndex = 0;
            CargarMateriales();
        }

        protected string GetTipoImagen(object tipo)
        {
            switch (tipo.ToString().ToLower())
            {
                case "libro": return "~/Images/book.png";
                case "tesis": return "~/Images/tesis.png";
                case "articulo": return "~/Images/article.png";
                default: return "~/Images/default.png";
            }
        }
    }
}