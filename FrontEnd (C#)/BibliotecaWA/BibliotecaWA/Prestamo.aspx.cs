using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BibliotecaWA.BibliotecaServices;
using SoftProgBusiness.GestMaterial.BO;
using SoftProgBusiness.GestMaterial.BOI;
using SoftProgBusiness.GestUsuarios.BO;
using SoftProgBusiness.GestUsuarios.BOI;
using SoftProgModel.GestMaterial;
using SoftProgModel.GestUsuarios;

namespace BibliotecaWA
{
    public partial class Prestamo : System.Web.UI.Page
    {
		 private UsuarioWSClient usuarioBO;
        private ProyectoP3.BibliotecaServices.usuario1 usuario;
        //private Biblioteca biblioteca;
        //private Libro libro;
        //private ILibroBO libroBO;
        private BibliotecaWSClient ibbo;
        private biblioteca biblioteca;
        private materialBibliografico materialBiblio;
        private MaterialBibliograficoWSClient materialBiblioBO;
        private PrestamoWSClient prestamobo;
        protected void Page_Load(object sender, EventArgs e)
        {
			usuarioBO = new UsuarioWSClient();
            materialBiblioBO = new MaterialBibliograficoWSClient();
            if (!IsPostBack)
            {
                txtFechaPrestamo.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm ");
                materialBiblio = new materialBibliografico();
                biblioteca = (biblioteca)Session["biblioteca"];
                materialBiblio = (materialBibliografico)Session["material"];
                lblTitulo.Text = materialBiblio.titulo;
                //lblAutor.Text = libro.;
                lblAnio.Text = materialBiblio.anho_publicacion.ToString();
                lblTema.InnerText = materialBiblio.clasificacion_tematica;
                lblEstado.InnerText= materialBiblio.estado.ToString();   
                lblTipo.InnerText= materialBiblio.tipo.ToString();    
                BindingList<contribuyente> c = new BindingList<contribuyente>();
                c = (BindingList<contribuyente>)Session["contribuyentes"];

                lblAutor.Text = c[0].nombre + " " + c[0].primer_apellido + " " + c[0].segundo_apellido;
                lblBiblioteca.Text = biblioteca.nombre;
                cargarDatosBiblioteca();
            }
        }
		private void cargarDatosBiblioteca()
        {
            materialBibliografico m = (materialBibliografico)Session["material"];
            biblioteca b = (biblioteca)Session["biblioteca"];
            //ejemplar[]/* e =*/ new BindingList<Ejemplar>();

            ejemplar[] e = materialBiblioBO.obtenerEjemplaresDisponibles(materialBiblio.idMaterial, b.idBiblioteca);

            lblCodigoEjemplar.Text = e[0].idEjemplar.ToString();
            lblUbicacionEjemplar.Text = e[0].ubicacion.ToString();
            if (e[0].estado.ToString().Equals("DISPONIBLE"))
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
            materialBibliografico m = (materialBibliografico)Session["material"];
            string tituloMaterial = m.titulo;
            string script;

            usuario1 usuario = (usuario1)Session["usuario"];
            // Validar que haya usuario seleccionado
            if (Session["usuario"] == null)
            {
                script = "mostrarAlerta('Debe seleccionar un usuario antes de registrar el préstamo.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertaUsuario", script, true);
                return;
            }
            // Recuperar usuario y validar límites
            usuario = (usuario1)Session["usuario"];
            int prestamosVigentes = usuarioBO.obtener_prestamos_vigentesxUsuario(usuario.id_usuario);
            int limitePrestamos = usuario.rol_usuario.limite_prestamo;

            if (prestamosVigentes >= limitePrestamos)
            {
                script = $"mostrarAlerta('El usuario {usuario.nombre} ya alcanzó su límite de préstamos ({limitePrestamos}).');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertaLimite", script, true);
                return;
            }
            string nombreUsuario = usuario.nombre.ToString()+" "+usuario.primer_apellido.ToString()+" "+usuario.segundo_apellido.ToString();

            // Validar que haya ejemplar disponible
            m = (materialBibliografico)Session["material"];
            biblioteca b = (biblioteca)Session["biblioteca"];
            ejemplar[] ejemplares = materialBiblioBO.obtenerEjemplaresDisponibles(m.idMaterial, b.idBiblioteca);
            if (ejemplares == null || ejemplares.Length == 0)
            {
                script = "mostrarAlerta('No hay ejemplares disponibles para este material.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertaEjemplar", script, true);
                return;
            }
            script = $"mostrarModalConfirmacion('{nombreUsuario}', '{tituloMaterial}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModalConfirmacion", script, true);

        }
        protected void btnConfirmarPrestamo_Click(object sender, EventArgs e)
        {
            // Crear objeto préstamo
            prestamo p = new prestamo();

            materialBibliografico m = (materialBibliografico)Session["material"];
            m = (materialBibliografico)Session["material"];
            biblioteca b = (biblioteca)Session["biblioteca"];

            usuario1 usuarioSesion = (usuario1)Session["usuario"];

            ProyectoP3.BibliotecaServices.usuario usuarioPrestamo = new ProyectoP3.BibliotecaServices.usuario();
            usuarioPrestamo.id_usuario = usuarioSesion.id_usuario;
            p.usuario = usuarioPrestamo;


            ejemplar[] ej = materialBiblioBO.obtenerEjemplaresDisponibles(m.idMaterial, b.idBiblioteca);
            ejemplar1 ejemplar = new ejemplar1();
            ejemplar.idEjemplar = ej[0].idEjemplar;
            p.ejemplar = ejemplar;


            prestamobo = new PrestamoWSClient();
            int codigoPrestamo = prestamobo.insertarPrestamo(p);

            //Modal para la confirmación del prestamo
            string script = $"mostrarModalPrestamoExitoso('{codigoPrestamo:D5}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModalExito", script, true);
        }


        protected void btnContinuar_Click(object sender, EventArgs e)
        {

            Response.Redirect("BusquedaMaterialaspx.aspx");
        }

        protected void txtCodigoUniv_TextChanged(object sender, EventArgs e)
        {
            string codigo = txtCodigoUniv.Text.Trim();

            if (string.IsNullOrEmpty(codigo))
                return;

            usuario = usuarioBO.obtenerUsuarioxCodigo(Int32.Parse(codigo));

            if (usuario == null) return;

            Session["usuario"] = usuario;

            string NombreCompleto = usuario.nombre + " " + usuario.segundo_apellido + " " + usuario.primer_apellido;
            txtNombre.Text = NombreCompleto;
            txtTipoUsuario.Text = usuario.rol_usuario.tipo;
            txtLimiteDias.Text = usuario.rol_usuario.cantidad_de_dias_por_prestamo.ToString();
            txtLimitePrestamo.Text = usuario.rol_usuario.limite_prestamo.ToString();
            txtPrestamosVigentes.Text=usuarioBO.obtener_prestamos_vigentesxUsuario(usuario.id_usuario).ToString();
            int dias = usuario.rol_usuario.cantidad_de_dias_por_prestamo;
            DateTime fechaPrestamo = DateTime.Now;
            DateTime fechaVencimiento = fechaPrestamo.AddDays(dias);
            txtFechaVencimiento.Text = fechaVencimiento.ToString("yyyy-MM-dd HH:mm");
        }
    }
}