using System;
using System.Collections.Generic;
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

        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            // Obtener el código ingresado por el usuario
            string codigoIngresado = string.Join("", txtCodigo1.Text, txtCodigo2.Text, txtCodigo3.Text, txtCodigo4.Text, txtCodigo5.Text, txtCodigo6.Text);

            // Verificar si el código ingresado es correcto
            if (codigoIngresado == "123456")
            {
                // Si el código es correcto, redirigir a la página correspondiente
                Response.Redirect("NuevaContrasena.aspx");
            }
            else
            {
                // Si el código es incorrecto, mostrar un mensaje de error
                lblError.Visible = true;
                lblError.Text = "El código ingresado no es válido. Intenta nuevamente.";
            }
        }
    }
}