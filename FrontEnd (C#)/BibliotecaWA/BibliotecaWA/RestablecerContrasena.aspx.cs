using BibliotecaWA.BibliotecaServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BibliotecaWA
{
    public partial class RestablecerContrasena : System.Web.UI.Page
    {
        private UsuarioWSClient bousuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Limpiar mensajes de error al cargar la página
            hfEmailError.Value = "";
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            // Limpiar mensajes de error previos
            hfEmailError.Value = "";

            // Obtener correo ingresado
            string correo = txtEmail.Text;

            // Llamada al servicio web para verificar si el correo existe
            bousuario = new UsuarioWSClient();

            // Verificar si el correo existe y obtener el id_usuario
            int idUsuario = bousuario.verificarCorreoExistente(correo);

            // Verificar si el correo existe en el sistema
            if (idUsuario > 0)
            {
                // El correo está registrado, podemos proceder con el restablecimiento de la contraseña
                Session["UserId"] = idUsuario;
                Session["CorreoValidado"] = true; // Marcar que el correo se validó
                Session["CorreoReestablecimiento"] = correo;
                Response.Redirect("CodigoVerificacion.aspx");
            }
            else
            {
                // Si el correo no está registrado, mostrar un mensaje de error
                hfEmailError.Value = "true";
                // Limpiar el campo de email
                txtEmail.Text = "";
                // Aseguramos que no haya sesión de correo validado
                Session["CorreoValidado"] = false;
            }
        }
    }
}