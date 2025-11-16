using BibliotecaWA.BibliotecaServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BibliotecaWA
{
    public partial class DetallePrestamo_Sancion : System.Web.UI.Page
    {
        private MaterialWSClient mateBO;
        private PrestamoWSClient prestBO;
        private EjemplarWSClient ejemBO;
        private UsuarioWSClient userBO;
        private BibliotecaWSClient biblioBo;
        private SancionWSClient sancionBo;

        public List<string> TiposDeSancion
        {
            get
            {
                return new List<string>
        {
            "DANHO",
            "ENTREGA_TARDIA",
        };
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            ejemBO = new EjemplarWSClient();
            mateBO = new MaterialWSClient();
            prestBO = new PrestamoWSClient();
            userBO = new UsuarioWSClient();
            biblioBo = new BibliotecaWSClient();
            sancionBo = new SancionWSClient();

            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                string modo = Request.QueryString["modo"];
                MostrarDetalle(modo);
                if (modo.Equals("ver"))
                {
                    lblCabecera.Text = "Detalle de préstamo";
                    lblCabeceraInf.Text = "Detalle de préstamo";
                    lblMensajePersonalizado.Text = "⚠️ Si no se devuelve a tiempo, se le aplicará la sanción correspondiente.";
                }
                else
                {
                    lblCabeceraInf.Text = "Modificacion de préstamo";
                    lblCabecera.Text = "Modificacion de préstamo";

                }
                int id_prestamo = int.Parse(id);
                prestamo prestamo = prestBO.obtenerPrestamoPorId(id_prestamo);
                int id_ejemplar = prestamo.ejemplar.idEjemplar;
                ejemplar ejemp = ejemBO.obtenerEjemplarPorId(id_ejemplar);
                int id_material = ejemp.id_material;
                materialBibliografico material = mateBO.ObtenerSoloMaterial(id_material);

                lblTitulo.Text = material.titulo;
                lblAutor.Text = material.autoresTexto;
                lblAnio.Text = material.anho_publicacion.ToString();
                lblTipo.InnerText = material.tipo.ToString();
                lblTema.InnerText = material.clasificacion_tematica.ToString();
                lblTema.Style["background-color"] = "#9b59b6";
                string tipo = material.tipo.ToString();
                imgTipo.ImageUrl = GetTipoImagen(tipo);

                biblioteca biblio = biblioBo.obtenerBibliotecaPorId(ejemp.blibioteca.idBiblioteca);

                lblNombreBiblioteca.Text = biblio.nombre;
                lblLocacion.Text = biblio.ubicacion;

                usuario usuario = userBO.obtenerUsuarioxCodigo(prestamo.usuario.codigo);

                lblTipoUsuario.Text = usuario.rol_usuario.tipo.ToString();
                lblPrestamosVigentes.Text = userBO.obtener_prestamos_vigentesxUsuario(usuario.id_usuario).ToString();
                lblLimiteDias.Text = usuario.rol_usuario.cantidad_de_dias_por_prestamo.ToString();
                lblLimitePrestamos.Text = usuario.rol_usuario.limite_prestamo.ToString();
                lblFechaPrestamo.Text = prestamo.fecha_de_prestamo.ToString("dd/MM/yyyy - hh:mm tt");
                lblFechaVencimiento.Text = prestamo.fecha_vencimiento.ToString("dd/MM/yyyy - hh:mm tt");

                txtCodigo.Text = usuario.codigo.ToString();
                txtNombre.Text = usuario.nombre + " " + usuario.primer_apellido + " " + usuario.segundo_apellido;
            }

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

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("HistorialPrestamos.aspx"); 
        }

        protected void MostrarDetalle(string modo)
        {
            if (modo == "editar")
            {
                pnlFilaEditar.Visible = true;
                pnlBotonGuardar.Visible = true;
                DateTime fechaHoy = DateTime.Now.Date;
                DateTime fechaMaxima = fechaHoy.AddYears(1);

                txtFechaDevo.Attributes["min"] = fechaHoy.ToString("yyyy-MM-dd");
                txtFechaDevo.Attributes["max"] = fechaMaxima.ToString("yyyy-MM-dd");

                // Opcional: precargar fecha actual
                txtFechaDevo.Text = fechaHoy.ToString("yyyy-MM-dd");
            }
            else if (modo == "ver")
            {
                pnlFilaEditar.Visible = false;
                pnlBotonGuardar.Visible = false;
            }
        }

        protected void cvFecha_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime fechaHoy = DateTime.Now.Date;
            DateTime fechaMaxima = fechaHoy.AddYears(1);
            DateTime fechaIngresada;

            // Validación estricta: si no es una fecha válida o está fuera del rango, se marca inválida
            if (!DateTime.TryParse(txtFechaDevo.Text, out fechaIngresada) ||
                fechaIngresada < fechaHoy ||
                fechaIngresada > fechaMaxima)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            // Lista para almacenar todas las sanciones
            var sanciones = new List<sancion>();

            // Iterar sobre los campos enviados
            foreach (string key in Request.Form.AllKeys)
            {
                if (key.StartsWith("txtTipoSancion_"))
                {
                    string suffix = key.Substring("txtTipoSancion_".Length);
                    string tipo = Request.Form[key];
                    string duracionKey = "txtDuracion_" + suffix;
                    string justificacionKey = "txtJustificacion_" + suffix;

                    string duracion = Request.Form[duracionKey];
                    string justificacion = Request.Form[justificacionKey];
                    sancion sanci = new sancion();

                    // Asignar tipo de sanción desde string al enum
                    sanci.tipo_sancion = (tipoSancion)Enum.Parse(typeof(tipoSancion), tipo);

                    // Convertir duración a entero
                    sanci.duracion_dias = Convert.ToInt32(duracion);

                    // Convertir fechas desde los Labels a DateTime usando el formato exacto
                    string fechaInicioStr = lblFechaPrestamo.Text.Split('-')[0].Trim();
                    sanci.fecha_fin = DateTime.Parse(txtFechaDevo.Text);
                    sanci.fecha_inicio = DateTime.Parse(fechaInicioStr);


                    // Asignar justificación y estado
                    sanci.justificacion = justificacion;
                    sanci.estado = estadoSancion.VIGENTE;

                    string id = Request.QueryString["id"];
                    sanci.prestamo.idPrestamo = Convert.ToInt32(id);
                    sanciones.Add(sanci);
                    // falta implementar en la bd

                }
            }

            Response.Redirect("HistorialPrestamos.aspx");
        }

    }
}