using BibliotecaWA.BibliotecaServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BibliotecaWA
{
    public partial class InicioSesion : System.Web.UI.Page
    {
        private UsuarioWSClient bousuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            ////Verificar si el usuario ya está autenticado
            //if (Request.IsAuthenticated)
            //{
            //    // Si el usuario ya está autenticado, redirigir a la página de búsqueda de materiales
            //    Response.Redirect("BusquedaMaterialas.aspx", true);
            //}
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Ocultar mensaje de error antes de la verificación
            lblMensaje.Visible = false;
            lblMensaje.Text = "";

            // Crear objeto para usuario
            usuario1 us = new usuario1();
            us.correo = txtUsername.Text;
            us.contrasena = txtPassword.Text;

            // Llamada al servicio web para verificar las credenciales
            bousuario = new UsuarioWSClient();
            int resultado = bousuario.verificarCuenta(us);

            // Verificar si las credenciales son correctas
            if (resultado != 0)
            {
                // Credenciales correctas: crear la cookie de autenticación
                FormsAuthenticationTicket tkt;
                string cookiestr;
                HttpCookie ck;
                tkt = new FormsAuthenticationTicket(1, us.correo, DateTime.Now,
                DateTime.Now.AddMinutes(30), true, "aqui van los roles");

                // Encriptar el ticket
                cookiestr = FormsAuthentication.Encrypt(tkt);

                // Crear la cookie de autenticación
                ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                ck.Expires = tkt.Expiration;
                ck.Path = FormsAuthentication.FormsCookiePath;

                // Añadir la cookie a la respuesta
                Response.Cookies.Add(ck);

                // Mostrar mensaje de éxito antes de redirigir
                lblMensaje.Visible = true;
                lblMensaje.Text = "Credenciales correctas";

                // Redirigir a la página solicitada o a la predeterminada
                string strRedirect = Request["ReturnUrl"];
                if (strRedirect == null)
                    strRedirect = "BusquedaMaterialas.aspx";  // Página predeterminada después de iniciar sesión

                // Redirige una vez
                Response.Redirect(strRedirect, true);
            }
            else
            {
                // Si las credenciales son incorrectas, mostrar mensaje de error
                lblMensaje.Visible = true;
                lblMensaje.Text = "Credenciales incorrectas. Inténtalo nuevamente.";
            }
        }
    }
}