//using BibliotecaWA.BibliotecaServices;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace BibliotecaWA
//{
//    public partial class RestablecerContrasena : System.Web.UI.Page
//    {
//        private UsuarioWSClient bousuario;
//        protected void Page_Load(object sender, EventArgs e)
//        {

//        }

//        protected void btnContinue_Click(object sender, EventArgs e)
//        {
//            // Obtener correo ingresado
//            string correo = txtEmail.Text;

//            // Llamada al servicio web para verificar si el correo existe
//            bousuario = new UsuarioWSClient();

//            // Verificar si el correo existe y obtener el id_usuario
//            int idUsuario = bousuario.verificarCorreoExistente(correo);

//            // Verificar si el correo existe en el sistema
//            if (idUsuario > 0)
//            {
//                // El correo está registrado, podemos proceder con el restablecimiento de la contraseña
//                Session["UserId"] = idUsuario;
//                Response.Redirect("CodigoVerificacion.aspx");
//            }
//            else
//            {
//                // Si el correo no está registrado, mostrar un mensaje de error
//                lblEmailError.Visible = true;
//                lblEmailError.Text = "El correo ingresado no es válido.";
//            }
//        }
//    }
//}
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
            var emailError = Page.FindControl("emailErrorContainer") as System.Web.UI.HtmlControls.HtmlGenericControl;
            if (emailError != null)
            {
                emailError.Visible = false;
            }
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            // Limpiar mensajes de error previos
            var emailError = Page.FindControl("emailErrorContainer") as System.Web.UI.HtmlControls.HtmlGenericControl;
            if (emailError != null)
            {
                emailError.Visible = false;
            }

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
                Response.Redirect("CodigoVerificacion.aspx");
            }
            else
            {
                // Si el correo no está registrado, mostrar un mensaje de error
                if (emailError != null)
                {
                    emailError.InnerHtml = "<div class='error-message-content'><i class='fa-solid fa-circle-exclamation'></i><span class='error-text'>El correo electrónico no está asociado a ninguna cuenta.</span></div>";
                    emailError.Attributes["class"] = "error-message show";
                }
            }
        }
    }
}