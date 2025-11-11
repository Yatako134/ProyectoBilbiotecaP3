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
    public partial class BusquedaMaterial : System.Web.UI.Page
    {
        private MaterialBOImpl materialBO;
        private BindingList<MaterialBibliografico> listaMateriales;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMateriales();
                CargarBibliotecas();
            }
        }

        private void CargarBibliotecas()
        {
            // Supongamos que obtienes una lista desde tu capa de negocio o BD
            BibliotecaBOImpl bibliotecaBO = new BibliotecaBOImpl();
            var bibliotecas = bibliotecaBO.listarTodos(); // Debe devolver lista con propiedades IdBiblioteca y Nombre

            ddlBiblioteca.DataSource = bibliotecas;
            ddlBiblioteca.DataTextField = "Nombre";
            ddlBiblioteca.DataValueField = "IdBiblioteca";
            ddlBiblioteca.DataBind();

            ddlBiblioteca.Items.Insert(0, new ListItem("-- Seleccione --", ""));
        }
        protected void btnLimpiarAvanzado_Click(object sender, EventArgs e)
        {

        }
        private void CargarMateriales(string filtro = "")
        {
            materialBO = new MaterialBOImpl();
            listaMateriales = materialBO.listarTodos();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var filtrada = new BindingList<MaterialBibliografico>();
                foreach (var m in listaMateriales)
                {
                    if (m.Titulo.ToLower().Contains(filtro.ToLower()) ||
                        m.AutoresTexto.ToLower().Contains(filtro.ToLower()))
                        filtrada.Add(m);
                }
                listaMateriales = filtrada;
            }

            gvResultados.DataSource = listaMateriales;
            gvResultados.DataBind();

            int total = listaMateriales.Count;
            int inicio = gvResultados.PageIndex * gvResultados.PageSize + 1;
            int fin = Math.Min((gvResultados.PageIndex + 1) * gvResultados.PageSize, total);
            lblPaginaInfo.Text = $"{inicio}-{fin} de {total}";
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //CargarMateriales(txtBusqueda.Text.Trim());
        }

        protected void btnBuscarAvanzado_Click(object sender, EventArgs e)
        {
            // Aquí podrías abrir un modal o redirigir
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
            CargarMateriales(txtBusqueda.Text.Trim());
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvResultados.PageSize = int.Parse(ddlPageSize.SelectedValue);
            gvResultados.PageIndex = 0;
            CargarMateriales(txtBusqueda.Text.Trim());
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