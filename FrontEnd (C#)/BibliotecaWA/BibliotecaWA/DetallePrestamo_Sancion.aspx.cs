using BibliotecaWA.BibliotecaServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BibliotecaWA
{
    public partial class DetallePrestamo_Sancion : System.Web.UI.Page
    {
        private MaterialWSClient mateBO;
        private PrestamoWSClient prestBO;
        private EjemplarWSClient ejemBO;
        protected void Page_Load(object sender, EventArgs e)
        {
            ejemBO = new EjemplarWSClient();
            mateBO = new MaterialWSClient();
            prestBO = new PrestamoWSClient();
            string id = Request.QueryString["id"];
            string modo = Request.QueryString["modo"];

            lblCabecera.Text = "Detalle de préstamo";
            int id_prestamo = int.Parse(id);
            prestamo prestamo = prestBO.obtenerPrestamoPorId(id_prestamo);
            int id_ejemplar = prestamo.ejemplar.idEjemplar;
            ejemplar ejemp = ejemBO.obtenerEjemplarPorId(id_ejemplar);
            int id_material = ejemp.id_material;
            materialBibliografico material = mateBO.ObtenerSoloMaterial(id_material);

            lblTitulo.Text = material.titulo;
            lblAutor.Text = material.autoresTexto;
            lblAnio.Text = material.anho_publicacion.ToString();
            lblTipo.InnerText = material.tipo.ToString();
            lblTema.InnerText = material.clasificacion_tematica.ToString();
            lblTema.Style["background-color"] = "#9b59b6";
            string tipo = material.tipo.ToString();
            imgTipo.ImageUrl = GetTipoImagen(tipo);

        }
        protected string GetTipoImagen(object tipo)
        {
            switch (tipo.ToString().ToLower())
            {
                case "libro": return "~/Images/book.png";
                case "tesis": return "~/Images/tesis.png";
                case "articulo": return "~/Images/article.png";
                default: return "~/Images/default.png";
            }
        }

    }
}