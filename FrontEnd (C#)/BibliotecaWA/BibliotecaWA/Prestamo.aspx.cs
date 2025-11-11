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
    public partial class Prestamo : System.Web.UI.Page
    {
        private IUsuarioBO usuarioBO;
        private Usuario usuario;
        //private Biblioteca biblioteca;
        //private Libro libro;
        //private ILibroBO libroBO;
        private IBibliotecaBO ibbo;
        private Biblioteca biblioteca;
        private MaterialBibliografico materialBibliografico;
        private IMaterialBiblioBO materialBiblioBO;
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarioBO = new UsuarioBOImpl();
            materialBiblioBO = new MaterialBiblioBOImpl();
            if (!IsPostBack)
            {
                txtFechaPrestamo.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm");
                materialBibliografico = new MaterialBibliografico();
                biblioteca = (Biblioteca)Session["biblioteca"];
                materialBibliografico = (MaterialBibliografico)Session["material"];
                lblTitulo.Text = materialBibliografico.Titulo;
                //lblAutor.Text = libro.;
                lblAnio.Text = materialBibliografico.Anho_publicacion.ToString();
                lblTema.InnerText = materialBibliografico.Clasificacion_tematica;
                lblEstado.InnerText = materialBibliografico.Estado.ToString();
                lblTipo.InnerText = materialBibliografico.Tipo.ToString();
                BindingList<Contribuyente> c = new BindingList<Contribuyente>();
                c = (BindingList<Contribuyente>)Session["contribuyentes"];

                lblAutor.Text = c[0].Nombre + " " + c[0].Primer_apellido + " " + c[0].Segundo_apellido;
                lblBiblioteca.Text = biblioteca.Nombre;
                cargarDatosBiblioteca();
            }

        }

        private void cargarDatosBiblioteca()
        {
            MaterialBibliografico m = (MaterialBibliografico)Session["material"];
            Biblioteca b = (Biblioteca)Session["biblioteca"];
            BindingList<Ejemplar> e = new BindingList<Ejemplar>();

            e = materialBiblioBO.ObtenerEjemplaresDisponibles(materialBibliografico.IdMaterial, b.IdBiblioteca);

            lblCodigoEjemplar.Text = e[0].IdEjemplar.ToString();
            lblUbicacionEjemplar.Text = e[0].Ubicacion.ToString();
            if (e[0].Estado.ToString().Equals("DISPONIBLE"))
            {
                lblEstadoEjemplar.InnerHtml = "Disponible";
                lblEstadoEjemplar.Attributes["class"] = "badge rounded-pill bg-success px-3 py-2";
            }
            else
            {
                lblEstadoEjemplar.InnerHtml = "Prestado";
                lblEstadoEjemplar.Attributes["class"] = "badge rounded-pill bg-danger px-3 py-2";
            }
            // lblEstadoEjemplar.InnerText = e[0].Estado.ToString();   


        }
        public string GetEstadoCss(string estado)
        {
            switch (estado)
            {
                case "DISPONIBLE": return "badge bg-success";
                case "PRESTADO": return "badge bg-warning text-dark";
                case "EN_REPARACION": return "badge bg-danger";
                default: return "badge bg-secondary";
            }
        }

        protected void btnSolicitarPrestamo_Click(object sender, EventArgs e)
        {
            MaterialBibliografico m = (MaterialBibliografico)Session["material"];
            string tituloMaterial = m.Titulo;
            Usuario usuario = (Usuario)Session["usuario"];
            string nombreUsuario = usuario.Nombre.ToString() + " " + usuario.Primer_apellido.ToString() + " " + usuario.Segundo_apellido.ToString();

            string script = $"mostrarModalConfirmacion('{nombreUsuario}', '{tituloMaterial}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModalConfirmacion", script, true);

        }
        protected void btnConfirmarPrestamo_Click(object sender, EventArgs e)
        {
            // Aquí irá el código para registrar el préstamo en la base de datos*/
        }

        protected void txtCodigoUniv_TextChanged(object sender, EventArgs e)
        {
            string codigo = txtCodigoUniv.Text.Trim();

            if (string.IsNullOrEmpty(codigo))
                return;

            usuario = usuarioBO.ObtenerUsuarioxCodigo(Int32.Parse(codigo));
            if (usuario == null) return;
            Session["usuario"] = usuario;

            string NombreCompleto = usuario.Nombre + " " + usuario.Segundo_apellido + " " + usuario.Primer_apellido;
            txtNombre.Text = NombreCompleto;
            txtTipoUsuario.Text = usuario.Rol_usuario.Tipo;
            txtLimiteDias.Text = usuario.Rol_usuario.Cantidad_de_dias_por_prestamo.ToString();
            int dias = usuario.Rol_usuario.Cantidad_de_dias_por_prestamo;
            DateTime fechaPrestamo = DateTime.Now;
            DateTime fechaVencimiento = fechaPrestamo.AddDays(dias);
            txtFechaVencimiento.Text = fechaVencimiento.ToString("yyyy-MM-dd hh:mm");
        }
    }
}