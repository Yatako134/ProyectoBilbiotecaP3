using BibliotecaWA.BibliotecaServices;
using System;
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
            if (!IsPostBack)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            hfCredentialError.Value = "";

            usuario us = new usuario();
            us.correo = txtUsername.Text;
            us.contrasena = txtPassword.Text;

            bousuario = new UsuarioWSClient();
            int resultado = bousuario.verificarCuenta(us);

            if (resultado != 0)
            {
                usuario usu = bousuario.obtenerUsuarioPorId(resultado);
                int rol = usu.rol_usuario.id_rol;
                string rolString = rol.ToString();

                // Crear cookie de autenticación con el rol en userData
                FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(
                    1,
                    us.correo,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    true,
                    rolString); // Aquí van los roles
                string cookiestr = FormsAuthentication.Encrypt(tkt);
                HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                ck.Expires = tkt.Expiration;
                Response.Cookies.Add(ck);

                Session["UserId"] = resultado;
                Session["UserName"] = $"{usu.nombre} {usu.primer_apellido}";
                Session["UserRole"] = rol;

                // Redirigir según el rol
                if (rol == 3)
                {
                    Response.Redirect("BusquedaMaterialas.aspx");
                }
                else
                {
                    Response.Redirect("BusquedaMaterialesEstudiante.aspx");
                }
            }
            else
            {
                hfCredentialError.Value = "true";
                txtUsername.Text = "";
                txtPassword.Text = "";
            }
        }
    }
}