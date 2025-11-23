using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BibliotecaWA
{
    public partial class CodigoVerificacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si el correo fue validado
            if (Session["CorreoValidado"] == null || !(bool)Session["CorreoValidado"])
            {
                Response.Redirect("RestablecerContrasena.aspx");
            }
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            string codigoIngresado = string.Join("", txtCodigo1.Text, txtCodigo2.Text, txtCodigo3.Text, txtCodigo4.Text, txtCodigo5.Text, txtCodigo6.Text);

            if (codigoIngresado == "123456")
            {
                // Código correcto, marcar como validado y redirigir
                Session["CodigoValidado"] = true;
                Response.Redirect("NuevaContrasena.aspx");
            }
            else
            {
                // Código incorrecto, mostrar error (actualizacion: ahora en javascript)
                
            }
        }
    }
}