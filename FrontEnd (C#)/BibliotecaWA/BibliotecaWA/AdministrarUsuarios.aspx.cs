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
    public partial class AdministrarUsuarios : System.Web.UI.Page
    {
        private UsuarioWSClient bousuario = new UsuarioWSClient();
        private RolWSClient borol = new RolWSClient();
        private usuario usuarioActual;
        private string modo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
                //  Recuperamos el modo desde QueryString o usamos "ver" por defecto

                modo = Request.QueryString["modo"]?.ToLower() ?? "ver";
                Session["modoUsuario"] = modo; // guardar en sesion

                int idUsuario = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                    idUsuario = int.Parse(Request.QueryString["id"]);

                CargarRoles();

                if (modo == "registrar")
                {
                    lblTitulo.Text = "Registro de nuevo usuario";
                    btnAccion.Text = "Agregar usuario";
                    LblGuia.Text = "Registro de usuario";
                    PrepararRegistro();

                    // Boton llama al modal
                    btnAccion.OnClientClick = "actualizarModal(); return false;";
                }
                else
                {

                    usuarioActual = bousuario.obtenerUsuarioPorId(idUsuario);
                    if (usuarioActual == null) return;

                    txtCodigo.Text = usuarioActual.codigo.ToString();
                    txtDNI.Text = usuarioActual.DOI.ToString();
                    txtNombre.Text = usuarioActual.nombre;
                    txtPrimerApellido.Text = usuarioActual.primer_apellido;
                    txtSegundoApellido.Text = usuarioActual.segundo_apellido;
                    txtCorreo.Text = usuarioActual.correo;
                    txtContrasena.Text = usuarioActual.contrasena;
                    txtTelefono.Text = usuarioActual.telefono;
                    ddlRol.SelectedValue = usuarioActual.rol_usuario.id_rol.ToString();

                    if (modo == "ver")
                    {
                        lblTitulo.Text = "Detalle de usuario";
                        LblGuia.Text = "Detalle de usuario";
                        btnAccion.Visible = false;
                        HabilitarSoloLectura();
                    }
                    else if (modo == "editar")
                    {
                        lblTitulo.Text = "Modificación del usuario";
                        LblGuia.Text = "Modificación de usuario";
                        btnAccion.Text = "Guardar Cambios";
                        HabilitarEdicion();

                        // Boton hace postback normal en edicion
                        btnAccion.OnClientClick = "";
                    }

                    Session["usuarioActual"] = usuarioActual;
                }
            }
            else
            {
                //  Recuperamos el modo y usuario en postbacks
                modo = Session["modoUsuario"]?.ToString() ?? "ver";
                usuarioActual = Session["usuarioActual"] as usuario;
            }
        }





        private void CargarRoles()
        {
            BindingList<rol> roles = new BindingList<rol>(borol.listarRoles());
            ddlRol.DataSource = roles;
            ddlRol.DataTextField = "Tipo";
            ddlRol.DataValueField = "Id_rol";
            ddlRol.DataBind();
        }

        private void PrepararRegistro()
        {
            txtCodigo.Text = "";
            txtDNI.Text = "";
            txtNombre.Text = "";
            txtPrimerApellido.Text = "";
            txtSegundoApellido.Text = "";
            txtCorreo.Text = "";
            txtContrasena.Text = "";
            txtTelefono.Text = "";
            ddlRol.SelectedIndex = 0;
        }

        private void HabilitarSoloLectura()
        {
            txtCodigo.Enabled = false; txtCodigo.CssClass += " readonly";
            txtDNI.Enabled = false; txtDNI.CssClass += " readonly";
            txtNombre.Enabled = false; txtNombre.CssClass += " readonly";
            txtPrimerApellido.Enabled = false; txtPrimerApellido.CssClass += " readonly";
            txtSegundoApellido.Enabled = false; txtSegundoApellido.CssClass += " readonly";
            txtCorreo.Enabled = false; txtCorreo.CssClass += " readonly";
            txtContrasena.Enabled = false; txtContrasena.CssClass += " readonly";
            txtTelefono.Enabled = false; txtTelefono.CssClass += " readonly";
            ddlRol.Enabled = false; ddlRol.CssClass += " readonly";
        }

        private void HabilitarEdicion()
        {
            txtCodigo.Enabled = true;
            txtDNI.Enabled = true;
            txtNombre.Enabled = true;
            txtPrimerApellido.Enabled = true;
            txtSegundoApellido.Enabled = true;
            txtCorreo.Enabled = true;
            txtContrasena.Enabled = true;
            txtTelefono.Enabled = true;
            ddlRol.Enabled = true;
        }

        protected void btnAccion_Click(object sender, EventArgs e)
        {
            //  Usamos el modo desde Session (ya persistido)
            modo = Session["modoUsuario"]?.ToString() ?? "ver";

            if (modo == "registrar")
            {
                usuario nuevo = new usuario
                {
                    codigo = int.Parse(txtCodigo.Text),
                    DOI = txtDNI.Text,
                    nombre = txtNombre.Text,
                    primer_apellido = txtPrimerApellido.Text,
                    segundo_apellido = txtSegundoApellido.Text,
                    correo = txtCorreo.Text,
                    contrasena = txtContrasena.Text,
                    telefono = txtTelefono.Text,
                    rol_usuario = new BindingList<rol>(borol.listarRoles()).ToList().Find(r => r.id_rol.ToString() == ddlRol.SelectedValue)
                };
                bousuario.insertarUsuario(nuevo);
            }
            else if (modo == "editar" && usuarioActual != null)
            {
                usuarioActual.codigo = int.Parse(txtCodigo.Text);
                usuarioActual.DOI = txtDNI.Text;
                usuarioActual.nombre = txtNombre.Text;
                usuarioActual.primer_apellido = txtPrimerApellido.Text;
                usuarioActual.segundo_apellido = txtSegundoApellido.Text;
                usuarioActual.correo = txtCorreo.Text;
                usuarioActual.contrasena = txtContrasena.Text;
                usuarioActual.telefono = txtTelefono.Text;
                usuarioActual.rol_usuario = new BindingList<rol>(borol.listarRoles()).ToList().Find(r => r.id_rol.ToString() == ddlRol.SelectedValue);

                bousuario.modificarUsuario(usuarioActual);
            }

            Response.Redirect("GestUsuarios.aspx");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestUsuarios.aspx");
        }
    }
}