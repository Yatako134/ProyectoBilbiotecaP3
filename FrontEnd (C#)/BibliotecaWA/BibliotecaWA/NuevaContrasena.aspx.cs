using BibliotecaWA.BibliotecaServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BibliotecaWA
{
    public partial class NuevaContrasena : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si el usuario está logueado O si el código fue validado
            bool usuarioLogueado = (Session["UserId"] != null && Session["UserRole"] != null);
            bool codigoValidado = (Session["CodigoValidado"] != null && (bool)Session["CodigoValidado"]);

            // Si no está logueado Y no tiene código validado, redirigir
            if (!usuarioLogueado && !codigoValidado)
            {
                Response.Redirect("InicioSesion.aspx");
            }

            // Configurar el botón Cancelar según el contexto
            if (usuarioLogueado && !codigoValidado)
            {
                // Usuario logueado - Cancelar va a la página según su rol
                string redirectUrl = GetRedirectUrlByRole();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "UpdateCancelButton",
                    $"document.querySelector('.button-error-secondary').href = '{redirectUrl}';", true);
            }
            // Si es restablecimiento (con código), mantiene el href original a InicioSesion.aspx
        }

        private string GetRedirectUrlByRole()
        {
            if (Session["UserRole"] != null)
            {
                int userRole = (int)Session["UserRole"];

                // Rol 3 = Bibliotecario
                if (userRole == 3)
                {
                    return "BusquedaMaterialas.aspx";
                }
                // Otros roles (Estudiante, Docente, etc.)
                else
                {
                    return "BusquedaMaterialesEstudiante.aspx";
                }
            }

            // Por defecto, redirigir a inicio de sesión
            return "InicioSesion.aspx";
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            // Verificar si el usuario está autenticado y tiene un UserId en la sesión
            if (Session["UserId"] != null)
            {
                int userId = (int)Session["UserId"]; // Recuperar el UserId desde la sesión
                string nuevaContrasena = txtPassword.Text;  // Obtener la nueva contraseña

                // Llamar al servicio web para cambiar la contraseña
                var servicio = new UsuarioWSClient();
                int resultado = servicio.modificarContrasena(userId, nuevaContrasena);

                if (resultado == 1) // Si la contraseña se modificó con éxito
                {
                    // Limpiar las variables de sesión del flujo de restablecimiento
                    Session["CorreoValidado"] = null;
                    Session["CodigoValidado"] = null;
                    Session["UserId"] = null;

                    // Redirigir a la página de inicio de sesión
                    Response.Redirect("InicioSesion.aspx");
                }
                else
                {
                    // Mostrar un mensaje de error si hubo un problema
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorScript",
                        "document.getElementById('passwordErrorContainer').innerHTML = \"<div class='error-message-content'><i class='fa-solid fa-circle-exclamation'></i><span class='error-text'>Hubo un problema al restablecer la contraseña.</span></div>\"; " +
                        "document.getElementById('passwordErrorContainer').classList.add('show');", true);
                }
            }
            else
            {
                // Redirigir a la página de inicio de sesión si no hay un usuario autenticado
                Response.Redirect("InicioSesion.aspx");
            }
        }
    }
}