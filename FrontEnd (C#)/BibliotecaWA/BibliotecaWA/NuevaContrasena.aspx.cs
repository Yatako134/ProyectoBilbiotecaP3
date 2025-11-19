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
                    // Redirigir a la página de éxito
                    Response.Redirect("InicioSesion.aspx");
                }
                else
                {
                    // Mostrar un mensaje de error si hubo un problema
                    lblPasswordError.Text = "Hubo un problema al restablecer la contraseña.";
                    lblPasswordError.Visible = true;
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