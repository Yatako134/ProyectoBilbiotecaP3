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
    public partial class GestUsuarios : System.Web.UI.Page
    {
        private UsuarioWSClient bousuario;
        private RolWSClient borol;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bousuario = new UsuarioWSClient();
                borol = new RolWSClient();

                Session["roles"] = new BindingList<rol>(borol.listarRoles());
                Session["usuarios"] = new BindingList<usuario>(bousuario.listarUsuarios());

                CargarUsuarios();

            }
        }

        private void CargarUsuarios()
        {
            BindingList<usuario> usuarios = (BindingList<usuario>)Session["usuarios"];

            dgvUsuario.DataSource = usuarios;
            dgvUsuario.DataBind();
            ActualizarContador();
            GenerarPaginador();
        }

        protected void dgvUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                usuario user = (usuario)e.Row.DataItem;
                BindingList<rol> roles = (BindingList<rol>)Session["roles"];

                e.Row.Cells[0].Text = user.codigo.ToString();
                e.Row.Cells[1].Text = user.DOI.ToString();
                e.Row.Cells[2].Text = user.nombre + " " + user.primer_apellido + " " + user.segundo_apellido;
                e.Row.Cells[3].Text = user.correo;
                e.Row.Cells[4].Text = user.telefono;

                rol rolEncontrado = roles.FirstOrDefault(r => r.id_rol == user.rol_usuario.id_rol);
                string tipoRol = rolEncontrado != null ? rolEncontrado.tipo : "Sin rol";

                // 🔹 Mostrar el rol con contorno azul
                e.Row.Cells[5].Text = $"<span class='contorno-rol'>{tipoRol}</span>";

                // 🔹 Permitir que se renderice el HTML
                e.Row.Cells[5].Attributes["style"] = "white-space: nowrap;";

                // Configurar el botón de opciones
                LinkButton btnOpciones = (LinkButton)e.Row.FindControl("btnOpciones");
                if (btnOpciones != null)
                {
                    btnOpciones.Attributes["data-codigo"] = user.id_usuario.ToString();
                    btnOpciones.OnClientClick = $"abrirModalOpciones({user.id_usuario}); return false;";
                }
            }
        }

        protected void dgvUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Si no necesitas hacer nada especial aquí, deja el método vacío
            // o usa esto de ejemplo para detectar comandos:
            if (e.CommandName == "MostrarOpciones")
            {
                string argumento = e.CommandArgument?.ToString() ?? "";
                // Ejemplo: poner en hiddenfield y/o mostrar alert
                hfIdUsuarioSeleccionado.Value = argumento;
                // Opcional: abrir modal desde servidor (si usas ScriptManager)
                ScriptManager.RegisterStartupScript(this, GetType(), "openModal",
                    "abrirModalOpciones('" + argumento + "');", true);
            }
        }

        // Evento de paginación
        protected void dgvUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvUsuario.PageIndex = e.NewPageIndex;
            CargarUsuarios();
        }

        // Cambia la cantidad de filas visibles
        protected void ddlCantidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvUsuario.PageSize = int.Parse(ddlCantidad.SelectedValue);
            CargarUsuarios();
        }

        // Botón buscar (lo puedes implementar luego)
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
        }

        // Boton de registrar :V
        protected void btnRegistrar_click(object sender, EventArgs e)
        {
            Response.Redirect("AdministrarUsuarios.aspx?&modo=registrar");
        }

        // ======= BOTONES DE OPCIONES =======
        protected void btnVer_Click(object sender, EventArgs e)
        {
            int idUsuario = int.Parse(hfIdUsuarioSeleccionado.Value);
            Response.Redirect($"AdministrarUsuarios.aspx?id={idUsuario}&modo=ver");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            int idUsuario = int.Parse(hfIdUsuarioSeleccionado.Value);
            Response.Redirect($"AdministrarUsuarios.aspx?id={idUsuario}&modo=editar");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int idUsuario = int.Parse(hfIdUsuarioSeleccionado.Value);
            bousuario = new UsuarioWSClient();
            bousuario.eliminarUsuario(idUsuario);

            BindingList<usuario> usuarios = new BindingList<usuario>(bousuario.listarUsuarios());
            Session["usuarios"] = usuarios;
            CargarUsuarios();
        }

        // ======= CONTADOR Y PAGINADOR =======
        private void ActualizarContador()
        {
            int total = ((BindingList<usuario>)Session["usuarios"]).Count;
            int mostrados = dgvUsuario.Rows.Count;
            lblResultados.Text = $"Mostrando {mostrados} de {total} usuarios";
        }

        protected void rptPaginas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "CambiarPagina" && e.CommandArgument.ToString() != "...")
            {
                int nuevaPagina = int.Parse(e.CommandArgument.ToString()) - 1;
                dgvUsuario.PageIndex = nuevaPagina;
                CargarUsuarios();
            }
        }

        protected void lnkPrev_Click(object sender, EventArgs e)
        {
            if (dgvUsuario.PageIndex > 0)
            {
                dgvUsuario.PageIndex--;
                CargarUsuarios();
            }
        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (dgvUsuario.PageIndex < dgvUsuario.PageCount - 1)
            {
                dgvUsuario.PageIndex++;
                CargarUsuarios();
            }
        }

        private void GenerarPaginador()
        {
            int paginaActual = dgvUsuario.PageIndex + 1;
            int totalPaginas = dgvUsuario.PageCount;

            List<object> paginas = new List<object>();
            int totalPosiciones = 7;

            if (totalPaginas <= totalPosiciones)
            {
                for (int i = 1; i <= totalPaginas; i++)
                    paginas.Add(i);
            }
            else
            {
                paginas.Add(1);

                if (paginaActual <= 4)
                {
                    paginas.Add(2);
                    paginas.Add(3);
                    paginas.Add("...");
                }
                else if (paginaActual >= totalPaginas - 3)
                {
                    paginas.Add("...");
                    paginas.Add(totalPaginas - 2);
                    paginas.Add(totalPaginas - 1);
                }
                else
                {
                    paginas.Add("...");
                    paginas.Add(paginaActual - 1);
                    paginas.Add(paginaActual);
                    paginas.Add(paginaActual + 1);
                    paginas.Add("...");
                }

                paginas.Add(totalPaginas);
            }

            while (paginas.Count < totalPosiciones)
                paginas.Insert(paginas.Count - 1, "");

            rptPaginas.DataSource = paginas;
            rptPaginas.DataBind();

            lnkPrev.Enabled = paginaActual > 1;
            lnkNext.Enabled = paginaActual < totalPaginas;
        }

        protected string ObtenerCssPagina(object dataItem)
        {
            if (dataItem == null || dataItem.ToString() == "")
                return "btn btn-outline-secondary btn-sm mx-1 disabled";

            string str = dataItem.ToString();

            if (str == "...") return "btn btn-outline-secondary btn-sm mx-1 disabled";

            if (int.TryParse(str, out int paginaNum))
            {
                if (paginaNum == dgvUsuario.PageIndex + 1)
                    return "btn btn-outline-primary btn-sm mx-1 btn-pagina-activa";
                else
                    return "btn btn-outline-primary btn-sm mx-1";
            }

            return "btn btn-outline-secondary btn-sm mx-1 disabled";
        }
    }
}