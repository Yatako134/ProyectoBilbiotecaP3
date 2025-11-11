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
        private MaterialBOImpl materialBO;
        private RolBOImpl borol;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                materialBO = new MaterialBOImpl();
                borol = new RolBOImpl();

                Session["roles"] = borol.listarTodos();
                Session["materiales"] = materialBO.listarTodos();

                CargarMateriales();
                ActualizarPaginacion();
            }
        }
        private void CargarMateriales()
        {
            BindingList<MaterialBibliografico> materiales = (BindingList<MaterialBibliografico>)Session["materiales"];

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
                MaterialBibliografico material = (MaterialBibliografico)e.Row.DataItem;
                //BindingList<Rol> roles = (BindingList<Rol>)Session["roles"];

                e.Row.Cells[0].Text = material.IdMaterial.ToString();
                e.Row.Cells[1].Text = material.Titulo.ToString();
                e.Row.Cells[2].Text = material.Tipo.ToString();
                e.Row.Cells[3].Text = material.Estado.ToString();
                string estado = material.Estado.ToString();

                e.Row.Cells[2].Text = "<span class='pill pill-info'>" + material.Tipo + "</span>";
                // Aplicar clase CSS según el estado
                if (estado == "DISPONIBLE")
                {
                    e.Row.Cells[3].Text = "<span class='pill pill-success'>" + material.Estado + "</span>";
                }
                else
                {
                    e.Row.Cells[3].Text = "<span class='pill pill-danger'>" + material.Estado + "</span>";
                }
            }
        }



        protected void btnRegistrar_click(object sender, EventArgs e)
        {
            Response.Redirect("RegistroMaterial.aspx");
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
            int total = ((BindingList<MaterialBibliografico>)Session["materiales"]).Count;
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
            int totalRecords = ((BindingList<MaterialBibliografico>)Session["materiales"]).Count;
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
                    Response.Redirect($"RegistroMaterial.aspx?id={idMaterial}");
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
            materialBO = new MaterialBOImpl();
            //materialBO.(idmaterial);

            BindingList<MaterialBibliografico> materiales = materialBO.listarTodos();
            Session["materiales"] = materiales;
            CargarMateriales();
        }
    }
}