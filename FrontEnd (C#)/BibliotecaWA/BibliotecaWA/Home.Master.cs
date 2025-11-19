using BibliotecaWA.BibliotecaServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BibliotecaWA
{
    public partial class Home : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatosUsuario();
            }
        }

        private void CargarDatosUsuario()
        {
            // Verificar si el usuario está autenticado
            if (Session["UserId"] != null)
            {
                try
                {
                    int userId = (int)Session["UserId"];

                    // Si ya tenemos el nombre en sesión, usarlo
                    if (Session["UserName"] != null)
                    {
                        spnNombreUsuario.Text = Session["UserName"].ToString();
                    }
                    else
                    {
                        // Si no, obtenerlo del servicio web
                        UsuarioWSClient servicio = new UsuarioWSClient();
                        var usuario = servicio.obtenerUsuarioPorId(userId);

                        if (usuario != null)
                        {
                            string nombreCompleto = $"{usuario.nombre} {usuario.primer_apellido}";
                            spnNombreUsuario.Text = nombreCompleto;
                            Session["UserName"] = nombreCompleto;
                        }
                        else
                        {
                            spnNombreUsuario.Text = "Usuario";
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    spnNombreUsuario.Text = "Usuario";
                }
            }
            else
            {
                // Si no hay usuario autenticado, redirigir al login
                Response.Redirect("InicioSesion.aspx");
            }
        }

        protected void lnkCambiarContrasena_Click(object sender, EventArgs e)
        {
            Response.Redirect("NuevaContrasena.aspx");
        }

        protected void lnkCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("InicioSesion.aspx");
        }
    }

}