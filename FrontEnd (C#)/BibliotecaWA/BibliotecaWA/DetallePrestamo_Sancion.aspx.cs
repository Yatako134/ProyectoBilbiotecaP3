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
            "DANHO"
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
                txtFechaDevo.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                string id = Request.QueryString["id"];
                string modo = Request.QueryString["modo"];

                int id_sancion;
                int id_prestamo;
                sancion sanc = null;
                if (modo.Equals("verSancion") || modo.Equals("editarSancion"))
                {
                    id_sancion = int.Parse(id);
                    sanc = sancionBo.obtener_por_id(id_sancion);
                    id_prestamo = sanc.prestamo.idPrestamo;
                }
                else { id_prestamo = int.Parse(id); }
                prestamo prestamo = prestBO.obtenerPrestamoPorId(id_prestamo);

                MostrarDetalle(modo, prestamo.fecha_vencimiento);
                Session["prestamo"] = prestamo;

                int id_ejemplar = prestamo.ejemplar.idEjemplar;
                ejemplar ejemp = ejemBO.obtenerEjemplarPorId(id_ejemplar);
                int id_material = ejemp.id_material;
                materialBibliografico material = mateBO.ObtenerSoloMaterial(id_material);

                if (modo == "ver")
                {
                    lblCabecera.Text = "Detalle de préstamo";
                    lblCabeceraInf.Text = "Detalle de préstamo";

                    switch (prestamo.estado)
                    {
                        case estadoPrestamo.FINALIZADO:
                            lblMensajePersonalizado.Text = "";
                            break;

                        case estadoPrestamo.VIGENTE:
                            lblMensajePersonalizado.Text =
                                "⚠️ Si no se devuelve a tiempo, se le aplicará la sanción correspondiente.";
                            break;

                        case estadoPrestamo.RETRASADO:
                            lblMensajePersonalizado.Text =
                                "⚠️ El usuario está retrasado en el préstamo actual";
                            break;
                    }
                }

                if (modo == "editar")
                {
                    lblCabeceraInf.Text = "Modificacion de préstamo";
                    lblCabecera.Text = "Modificacion de préstamo";
                }

                if (modo == "verSancion")
                {
                    lblCabeceraInf.Text = "Detalle de sanción";
                    lblCabecera.Text = "Detalle de sanción";

                    lblTipoSancion.Text =
                        sanc.tipo_sancion.ToString() == "ENTREGA_TARDIA"
                            ? "Entrega tardía"
                            : "Daño del material";

                    lblDuracion.Text = sanc.duracion_dias.ToString();
                    lblJustificacion.Text = sanc.justificacion;
                    lblFechaIni.Text = sanc.fecha_inicio.ToString("dd/MM/yyyy - hh:mm tt");
                    lblFechaFin.Text = sanc.fecha_fin.ToString("dd/MM/yyyy - hh:mm tt");
                }

                if (modo == "editarSancion")
                {
                    lblCabeceraInf.Text = "Modificación de sanción";
                    lblCabecera.Text = "Modificacion de sanción";
                    LabelInicioFecha.Text = sanc.fecha_inicio.ToString("dd/MM/yyyy - hh:mm tt");
                    LabelFinFecha.Text = sanc.fecha_fin.ToString("dd/MM/yyyy - hh:mm tt");
                }




                lblTitulo.Text = material.titulo;
                lblAutor.Text = material.autoresTexto;
                lblAnio.Text = material.anho_publicacion.ToString();
                lblTipo.InnerText = material.tipo.ToString();
                lblTema.InnerText = material.clasificacion_tematica.ToString();
                lblTema.Style["background-color"] = "#9b59b6";
                string tipo = material.tipo.ToString();
                imgTipo.ImageUrl = GetTipoImagen(tipo);

                biblioteca biblio = biblioBo.obtenerBibliotecaPorId(ejemp.blibioteca.idBiblioteca);

                lblBiblioteca.Text = biblio.nombre;
                lblLocacion.Text = ejemp.ubicacion.ToString();
                lblCodEjem.Text = ejemp.idEjemplar.ToString();

                usuario usuario = userBO.obtenerUsuarioxCodigo(prestamo.usuario.codigo);

                lblTipoUsuario.Text = usuario.rol_usuario.tipo.ToString();
                lblPrestamosVigentes.Text = userBO.obtener_prestamos_vigentesxUsuario(usuario.id_usuario).ToString();
                lblLimiteDias.Text = usuario.rol_usuario.cantidad_de_dias_por_prestamo.ToString();
                lblLimitePrestamos.Text = usuario.rol_usuario.limite_prestamo.ToString();
                lblFechaPrestamo.Text = prestamo.fecha_de_prestamo.ToString("dd/MM/yyyy - hh:mm tt", CultureInfo.InvariantCulture);
                lblFechaVencimiento.Text = prestamo.fecha_vencimiento.ToString("dd/MM/yyyy - hh:mm tt", CultureInfo.InvariantCulture);

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

        protected void MostrarDetalle(string modo, DateTime fecha_vencimiento)
        {
            if (modo == "editar")
            {
                pnlFilaEditar.Visible = true;
                pnlBotonGuardar.Visible = true;
                if (fecha_vencimiento > DateTime.Now)
                {

                    pnlSanciones.Visible = true;
                    pnlSancionAutomatica.Visible = false;
                }
                else
                {
                    pnlSancionAutomatica.Visible = true;
                    pnlSanciones.Visible = false;
                }

            }
            else if (modo == "ver")
            {
                pnlFilaEditar.Visible = false;
                pnlBotonGuardar.Visible = false;
            }
            else if (modo == "verSancion")
            {
                pnlSancionUnica.Visible = true;
                pnlFilaEditar.Visible = false;
                pnlBotonGuardar.Visible = false;
            }
            else if (modo == "editarSancion")
            {
                pnlSancionUnica.Visible = false;
                PanelEditarSancion.Visible = true;
                pnlBotonGuardar.Visible = true;

            }
        }


        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {

            string modo = Request.QueryString["modo"];
            if (modo == "editar")
            {
                prestamo pre = (prestamo)Session["prestamo"];
                // Lista para almacenar todas las sanciones
                var sanciones = new List<sancion>();
                sancion sanci = new sancion();
                string id = Request.QueryString["id"];
                sanci.prestamo = new prestamo();
                sanci.prestamo.idPrestamo = Convert.ToInt32(id);
                pre.estado = estadoPrestamo.FINALIZADO;
                // Iterar sobre los campos enviados
                sancionBo = new SancionWSClient();
                if (pre.fecha_vencimiento < DateTime.Now)
                {
                    pre.estado = estadoPrestamo.RETRASADO;
                    sancion sanciAuto = new sancion();
                    sanciAuto.prestamo = new prestamo();
                    sanciAuto.prestamo.idPrestamo = Convert.ToInt32(id);

                    sanciAuto.duracion_dias = Convert.ToInt32(hfDiasAuto.Value);
                    sanciAuto.justificacion = hfJustificacionAuto.Value;
                    sanciAuto.tipo_sancion = tipoSancion.ENTREGA_TARDIA; // o la que corresponda
                    sanciAuto.estado = estadoSancion.VIGENTE;

                    sancionBo.insertarSancion(tipoSancion.ENTREGA_TARDIA.ToString(), Convert.ToInt32(hfDiasAuto.Value),
                        hfJustificacionAuto.Value, Convert.ToInt32(id));
                }

                if (Request.Form.AllKeys.Any(k => k.StartsWith("txtTipoSancion_")
                                  && !string.IsNullOrWhiteSpace(Request.Form[k])))
                {
                    pre.estado = estadoPrestamo.FINALIZADO;
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

                            int valor = sancionBo.insertarSancion(tipo, Convert.ToInt32(duracion), justificacion, Convert.ToInt32(id));
                        }
                    }
                }

                prestBO.modificarPrestamo(pre);
                Response.Redirect("HistorialPrestamos.aspx");
            }
            else
            {
                string id = Request.QueryString["id"];
                int id_sancion = int.Parse(id);
                sancion sancionModificada = new sancion();
                sancionModificada.id_sancion = id_sancion;
                sancionModificada.justificacion = txtJustificacionUnica.Text;
                sancionModificada.duracion_dias = int.Parse(txtDuracionUnica.Text);
                string tipo = ddlTipoSancionUnica.SelectedValue;
                string tipoAMod;
                if (tipo == "Daño de material")
                {
                    tipoAMod = tipoSancion.DANHO.ToString();
                }
                else
                {
                    tipoAMod = tipoSancion.ENTREGA_TARDIA.ToString();
                }
                sancionBo = new SancionWSClient();
                sancionBo.modificar_sancion(sancionModificada, tipoAMod);
                Response.Redirect("HistorialPrestamos.aspx");
            }

        }

    }
}