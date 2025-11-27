using BibliotecaWA.BibliotecaServices;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Web.UI;

namespace BibliotecaWA
{
    public partial class CodigoVerificacion : Page
    {
        CorreoWSClient correoBO;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CorreoValidado"] == null || !(bool)Session["CorreoValidado"])
            {
                Response.Redirect("RestablecerContrasena.aspx");
                return;
            }

            if (!IsPostBack)
            {
                string codigo_validacion = GenerarOTP();
                Session["CodigoDeValidacionReest"] = codigo_validacion;

                correoBO = new CorreoWSClient();
                string correo = (string)Session["CorreoReestablecimiento"];
                string asunto = "REESTABLECIMIENTO DE CONTRASEÑA - SISTEMA DE BIBLIOTECAS UTILSARMY";
                string HTML = $@"
<html>
  <body style='font-family: Arial, sans-serif; color:#333;'>
    <h2 style='color:#004080;'>Código de Verificación</h2>
    <p>Estimado usuario,</p>
    <p>Para reestablecer su contraseña, ingrese el siguiente código de verificación:</p>
    <h1 style='color:#d35400;'>{codigo_validacion}</h1>
    <p style='font-size:14px;'>Este código es válido solo por un tiempo limitado.</p>
    <img src='cid:logo' style='width:180px; height:auto; margin-top:20px;'>
    <p style='margin-top:25px; font-size:13px; color:#666;'>Sistema de Bibliotecas UtilsArmy</p>
  </body>
</html>";
                correoBO.enviar_correo(correo, asunto, HTML);
            }
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            //string codigoIngresado = string.Join("",
            //    txtCodigo1.Text, txtCodigo2.Text, txtCodigo3.Text,
            //    txtCodigo4.Text, txtCodigo5.Text, txtCodigo6.Text
            //);

            //string codigo_real = (string)Session["CodigoDeValidacionReest"];

            //if (codigoIngresado == codigo_real)
            //{
            //    Session["CodigoValidado"] = true;
            //    Response.Redirect("NuevaContrasena.aspx");
            //}
            //else
            //{
            //    lblError.Text = "El código ingresado no es válido. Intenta nuevamente.";
            //    lblError.Visible = true;
            //}
            string codigoIngresado = string.Join("",
                txtCodigo1.Text, txtCodigo2.Text, txtCodigo3.Text,
                txtCodigo4.Text, txtCodigo5.Text, txtCodigo6.Text
            );

            string codigo_real = (string)Session["CodigoDeValidacionReest"];

            if (codigoIngresado == codigo_real)
            {
                Session["CodigoValidado"] = true;
                Response.Redirect("NuevaContrasena.aspx");
            }
            else
            {
                // Mostrar error usando JavaScript
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowError", "showError();", true);

                // Limpiar los campos para que el usuario ingrese de nuevo
                ClearInputs();
                txtCodigo1.Focus();
            }
        }
        private void ClearInputs()
        {
            txtCodigo1.Text = "";
            txtCodigo2.Text = "";
            txtCodigo3.Text = "";
            txtCodigo4.Text = "";
            txtCodigo5.Text = "";
            txtCodigo6.Text = "";
        }

        
        public string GenerarOTP()
        {
            byte[] bytes = new byte[4];
            RandomNumberGenerator.Create().GetBytes(bytes);
            int value = BitConverter.ToInt32(bytes, 0) & 0x7FFFFFFF;
            int otp = value % 900000 + 100000;
            return otp.ToString();
        }
    }
}
