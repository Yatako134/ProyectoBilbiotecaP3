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
        private UsuarioBOImpl bousuario = new UsuarioBOImpl();
        private RolBOImpl borol = new RolBOImpl();
        private Usuario usuarioActual;
        private string modo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 🔹 Recuperamos el modo desde QueryString o usamos "ver" por defecto
                modo = Request.QueryString["modo"]?.ToLower() ?? "ver";
                Session["modoUsuario"] = modo; // guardar en sesión

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

                    // Botón llama al modal
                    btnAccion.OnClientClick = "actualizarModal(); return false;";
                }
                else
                {
                    usuarioActual = bousuario.obtenerPorId(idUsuario);
                    if (usuarioActual == null) return;

                    txtCodigo.Text = usuarioActual.Codigo_universitario.ToString();
                    txtDNI.Text = usuarioActual.DOI1.ToString();
                    txtNombre.Text = usuarioActual.Nombre;
                    txtPrimerApellido.Text = usuarioActual.Primer_apellido;
                    txtSegundoApellido.Text = usuarioActual.Segundo_apellido;
                    txtCorreo.Text = usuarioActual.Correo;
                    txtContrasena.Text = usuarioActual.Contrasena;
                    txtTelefono.Text = usuarioActual.Numero_de_telefono;
                    ddlRol.SelectedValue = usuarioActual.Rol_usuario.Id_rol.ToString();

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

                        // Botón hace postback normal en edición
                        btnAccion.OnClientClick = "";
                    }

                    Session["usuarioActual"] = usuarioActual;
                }
            }
            else
            {
                // 🔹 Recuperamos el modo y usuario en postbacks
                modo = Session["modoUsuario"]?.ToString() ?? "ver";
                usuarioActual = Session["usuarioActual"] as Usuario;
            }
        }





        private void CargarRoles()
        {
            BindingList<Rol> roles = borol.listarTodos();
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
                Usuario nuevo = new Usuario
                {
                    Codigo_universitario = int.Parse(txtCodigo.Text),
                    DOI1 = int.Parse(txtDNI.Text),
                    Nombre = txtNombre.Text,
                    Primer_apellido = txtPrimerApellido.Text,
                    Segundo_apellido = txtSegundoApellido.Text,
                    Correo = txtCorreo.Text,
                    Contrasena = txtContrasena.Text,
                    Numero_de_telefono = txtTelefono.Text,
                    Rol_usuario = borol.listarTodos().ToList().Find(r => r.Id_rol.ToString() == ddlRol.SelectedValue)
                };
                bousuario.insertar(nuevo);
            }
            else if (modo == "editar" && usuarioActual != null)
            {
                usuarioActual.Codigo_universitario = int.Parse(txtCodigo.Text);
                usuarioActual.DOI1 = int.Parse(txtDNI.Text);
                usuarioActual.Nombre = txtNombre.Text;
                usuarioActual.Primer_apellido = txtPrimerApellido.Text;
                usuarioActual.Segundo_apellido = txtSegundoApellido.Text;
                usuarioActual.Correo = txtCorreo.Text;
                usuarioActual.Contrasena = txtContrasena.Text;
                usuarioActual.Numero_de_telefono = txtTelefono.Text;
                usuarioActual.Rol_usuario = borol.listarTodos().ToList().Find(r => r.Id_rol.ToString() == ddlRol.SelectedValue);

                bousuario.modificar(usuarioActual);
            }

            Response.Redirect("GestUsuarios.aspx");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestUsuarios.aspx");
        }
    }
}