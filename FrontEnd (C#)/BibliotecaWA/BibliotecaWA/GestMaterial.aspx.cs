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
    public partial class GestMaterial : System.Web.UI.Page
    {
        //private BibliotecaWSClient bobiblioteca;
        private MaterialWSClient materialBO;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                materialBO = new MaterialWSClient();
                //Session["usuarios"] = new BindingList<usuario>(bousuario.listarUsuarios());
                Session["materiales"] = new BindingList<materialBibliografico>(materialBO.ListarTodos());

                CargarMateriales();
                ActualizarPaginacion();
            }
        }
        private void CargarMateriales()
        {
            BindingList<materialBibliografico> materiales = (BindingList<materialBibliografico>)Session["materiales"];
            //recorremos esos materiales para contar los ejemplares y si es 0 ponemos NODISPONIBLE
            dgvUsuario.DataSource = materiales;
            dgvUsuario.DataBind();
            ActualizarContador();
            //GenerarPaginador();
            ActualizarPaginacion();
        }
        protected void dgvUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                materialBibliografico material = (materialBibliografico)e.Row.DataItem;
                materialBO = new MaterialWSClient();

                e.Row.Cells[0].Text = material.idMaterial.ToString();
                e.Row.Cells[1].Text = material.titulo.ToString();
                e.Row.Cells[2].Text = material.tipo.ToString();
                int cant = materialBO.ContarMateriales(material.idMaterial);
                //int cant = 0;
                if (cant > 0)
                {
                    material.estado = estadoMaterial.DISPONIBLE;
                }
                else
                {
                    material.estado = estadoMaterial.NO_DISPONIBLE;
                }

                e.Row.Cells[3].Text = material.estado.ToString();
                string estado = material.estado.ToString();

                e.Row.Cells[4].Text = cant.ToString();

                e.Row.Cells[2].Text = "<span class='pill pill-info'>" + material.tipo + "</span>";
                // Aplicar clase CSS según el estado
                if (estado == "DISPONIBLE")
                {
                    e.Row.Cells[3].Text = "<span class='pill pill-success'>" + material.estado + "</span>";
                }
                else
                {
                    e.Row.Cells[3].Text = "<span class='pill pill-danger'>" + material.estado + "</span>";
                }
            }
        }



        protected void btnRegistrar_click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarMaterial.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

        }
        protected void btnBuscarAvanzada_Click(object sender, EventArgs e)
        {

        }
        protected void dgvUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvUsuario.PageIndex = e.NewPageIndex;
            CargarMateriales();
        }
        private void ActualizarContador()
        {
            int total = ((BindingList<materialBibliografico>)Session["materiales"]).Count;
            int mostrados = dgvUsuario.Rows.Count;
            lblResultados.Text = $"Mostrando {mostrados} de {total} resultados";
        }

        // ======= PAGINACIÓN PERSONALIZADA =======
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvUsuario.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            dgvUsuario.PageIndex = 0; // Volver a la primera página
            CargarMateriales();
            ActualizarPaginacion();
        }

        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            dgvUsuario.PageIndex = 0;
            CargarMateriales();
            ActualizarPaginacion();
        }

        protected void lnkPrev_Click(object sender, EventArgs e)
        {
            if (dgvUsuario.PageIndex > 0)
            {
                dgvUsuario.PageIndex--;
                CargarMateriales();
                ActualizarPaginacion();
            }
        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (dgvUsuario.PageIndex < dgvUsuario.PageCount - 1)
            {
                dgvUsuario.PageIndex++;
                CargarMateriales();
                ActualizarPaginacion();
            }
        }

        protected void lnkLast_Click(object sender, EventArgs e)
        {
            dgvUsuario.PageIndex = dgvUsuario.PageCount - 1;
            CargarMateriales();
            ActualizarPaginacion();
        }

        protected void lnkPage_Click(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument) - 1;
            dgvUsuario.PageIndex = pageIndex;
            CargarMateriales();
            ActualizarPaginacion();
        }

        private void ActualizarPaginacion()
        {
            int totalRecords = ((BindingList<materialBibliografico>)Session["materiales"]).Count;
            int pageSize = dgvUsuario.PageSize;
            int currentPage = dgvUsuario.PageIndex + 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            // Actualizar información de paginación
            int startRecord = (currentPage - 1) * pageSize + 1;
            int endRecord = Math.Min(currentPage * pageSize, totalRecords);
            lblPaginationInfo.Text = $"{startRecord}-{endRecord} de {totalRecords}";

            // Actualizar controles de navegación
            lnkFirst.Enabled = (currentPage > 1);
            lnkPrev.Enabled = (currentPage > 1);
            lnkNext.Enabled = (currentPage < totalPages);
            lnkLast.Enabled = (currentPage < totalPages);

            // Generar números de página
            GenerarNumerosPagina(totalPages, currentPage);
        }

        private void GenerarNumerosPagina(int totalPages, int currentPage)
        {
            rptPager.DataSource = null;
            rptPager.DataBind();

            var pages = new List<object>();
            int startPage = Math.Max(1, currentPage - 2);
            int endPage = Math.Min(totalPages, currentPage + 2);

            // Agregar páginas al repeater
            for (int i = startPage; i <= endPage; i++)
            {
                pages.Add(new
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Class = i == currentPage ? "page-link active" : "page-link"
                });
            }

            rptPager.DataSource = pages;
            rptPager.DataBind();
        }

        protected void Opciones_Command(object sender, CommandEventArgs e)
        {
            int idMaterial = int.Parse(e.CommandArgument.ToString());

            switch (e.CommandName)
            {
                case "VerDetalle":
                    // Redirigir a página de ver detalle
                    Response.Redirect($"DetalleMaterial.aspx?id={idMaterial}");
                    break;
                case "Editar":
                    // Redirigir a página de registro/edición
                    Response.Redirect($"RegistrarMaterial.aspx?id={idMaterial}");
                    break;
                case "Eliminar":
                    // Eliminar material
                    hfIdUsuarioSeleccionado.Value = idMaterial.ToString();
                    btnEliminar_Click(null, null);
                    break;
            }
        }



        // ======= BOTONES DE OPCIONES =======
        protected void btnVer_Click(object sender, EventArgs e)
        {
            int idmaterial = int.Parse(hfIdUsuarioSeleccionado.Value);
            Response.Redirect($"AdmistrarUsuarios.aspx?id={idmaterial}&modo=ver");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            int idmaterial = int.Parse(hfIdUsuarioSeleccionado.Value);
            Response.Redirect($"AdmistrarUsuarios.aspx?id={idmaterial}&modo=editar");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int idmaterial = int.Parse(hfIdUsuarioSeleccionado.Value);
            materialBO = new MaterialWSClient();
            //materialBO.(idmaterial);

            BindingList<materialBibliografico> materiales = new BindingList<materialBibliografico>(materialBO.ListarMaterialesNormal());
            Session["materiales"] = materiales;
            CargarMateriales();
        }

    }
}