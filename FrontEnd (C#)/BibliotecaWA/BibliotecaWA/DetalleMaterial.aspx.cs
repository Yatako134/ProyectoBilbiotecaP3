using BibliotecaWA.BibliotecaServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BibliotecaWA
{
    public partial class DetalleMaterial : System.Web.UI.Page
    {
        //    private ILibroBO librobo;
        //    private BindingList<Libro> libros;
        //    private Libro libro;
        //private IBibliotecaBO ibbo;
        //private Biblioteca biblioteca;
        //private ILibroBO librobo;
        //private BindingList<Libro> libros;
        //private Libro libro;
        private IBibliotecaBO ibbo;
        private Biblioteca biblioteca;
        private MaterialBibliografico materialBibliografico;
        private IMaterialBiblioBO materialBiblioBO;

        protected void Page_Load(object sender, EventArgs e)
        {
            //librobo = new LibroBOImpl();
            ibbo = new BibliotecaBOImpl();
            biblioteca = new Biblioteca();
            materialBiblioBO = new MaterialBiblioBOImpl();
            materialBibliografico = new MaterialBibliografico();
            if (!IsPostBack)
            {
                string idMaterial = Request.QueryString["id"];
                // Cargar lista de bibliotecas en el dropdown
                // Cargar lista de bibliotecas en el dropdown
                ddlbibliotecas.DataSource = ibbo.listarTodos();
                ddlbibliotecas.DataTextField = "Nombre";
                ddlbibliotecas.DataValueField = "IdBiblioteca";
                ddlbibliotecas.DataBind();
                // 🔹 Agregar un item por defecto al inicio
                ddlbibliotecas.Items.Insert(0, new ListItem("Seleccione una opción", ""));

                // Opcional: seleccionar por defecto ese valor
                ddlbibliotecas.SelectedIndex = 0;


                // Cargar el libro (solo una vez)
                materialBibliografico = materialBiblioBO.obtenerPorId(2);
                //libros = new BindingList<Libro>(librobo.listarTodos());
                //materialBibliografico = libros.Single(x => x.IdMaterial == 1);
                Session["material"] = materialBibliografico;


                txtTitulo.Text = materialBibliografico.Titulo;
                txtTema.Text = materialBibliografico.Clasificacion_tematica;
                TextAnhioPubli.Text = materialBibliografico.Anho_publicacion.ToString();
                txtIdioma.Text = materialBibliografico.Idioma;
                TextNroPaginas.Text = materialBibliografico.Numero_paginas.ToString();
                materialBibliografico.Ejemplares = materialBiblioBO.buscarEjemplares(materialBibliografico.IdMaterial);
                if (materialBibliografico.Ejemplares != null) TextNroEjemplares.Text = materialBibliografico.Ejemplares.Count.ToString();
                else TextNroEjemplares.Text = "0";
                txtTipoMaterial.Text = materialBibliografico.Tipo.ToString();
                TextNroPaginas.Text = materialBibliografico.Numero_paginas.ToString();

                if (materialBibliografico is Libro libro)
                {
                    TextISBN.Text = libro.ISBNP;
                    txtEdicion.Text = libro.Edicion;
                    // Ocultar campos de tesis
                    TextInstitucionPublicacion.Visible = false;
                    lblInstitucionPublicacion.Visible = false;
                    TextEspecialidad.Visible = false;
                    lblEspecialidad.Visible = false;
                    TextAsesor.Visible = false;
                    lblAsesor.Visible = false;
                    TextGrado.Visible = false;
                    lblGrado.Visible = false;

                    // Ocultar campos de artículo
                    TextISSN.Visible = false;
                    lblISSN.Visible = false;
                    TextRevista.Visible = false;
                    lblRevista.Visible = false;
                    TextVolumen.Visible = false;
                    lblVolumen.Visible = false;
                    TextNumero.Visible = false;
                    lblNumero.Visible = false;
                }
                else if (materialBibliografico is Tesis tesis)
                {
                    TextInstitucionPublicacion.Text = tesis.InstitucionPublicacion.ToString();
                    TextEspecialidad.Text = tesis.Especialidad.ToString();
                    TextAsesor.Text = tesis.Asesor;
                    TextGrado.Text = tesis.Grado;

                    // Ocultar campos de libro
                    TextISBN.Visible = false;
                    lblISBN.Visible = false;
                    txtEdicion.Visible = false;
                    lblEdicion.Visible = false;

                    // Ocultar campos de artículo
                    TextISSN.Visible = false;
                    lblISSN.Visible = false;
                    TextRevista.Visible = false;
                    lblRevista.Visible = false;
                    TextVolumen.Visible = false;
                    lblVolumen.Visible = false;
                    TextNumero.Visible = false;
                    lblNumero.Visible = false;
                }
                else if (materialBibliografico is Articulo articulo)
                {
                    TextISSN.Text = articulo.ISSNP;
                    TextRevista.Text = articulo.Revista;
                    TextVolumen.Text = articulo.Volumen.ToString();
                    TextNumero.Text = articulo.Numero.ToString();


                    // Ocultar campos de libro
                    TextISBN.Visible = false;
                    lblISBN.Visible = false;
                    txtEdicion.Visible = false;
                    lblEdicion.Visible = false;

                    // Ocultar campos de tesis
                    TextInstitucionPublicacion.Visible = false;
                    lblInstitucionPublicacion.Visible = false;
                    TextEspecialidad.Visible = false;
                    lblEspecialidad.Visible = false;
                    TextAsesor.Visible = false;
                    lblAsesor.Visible = false;
                    TextGrado.Visible = false;
                    lblGrado.Visible = false;
                }
                //// Mostrar sus datos
                //txtTitulo.Text = libro.Titulo;
                //txtTema.Text = libro.Clasificacion_tematica;
                //TextAnhioPubli.Text = libro.Anho_publicacion.ToString();
                //txtIdioma.Text = libro.Idioma;
                //TextNroPaginas.Text = libro.Numero_paginas.ToString();
                //TextISBN.Text = libro.ISBNP;
                //libro.Ejemplares = librobo.buscarEjemplares(libro.IdMaterial);
                //TextNroEjemplares.Text = libro.Ejemplares.Count.ToString();
                //txtTipoMaterial.Text = libro.Tipo.ToString();
                //txtEdicion.Text = libro.Edicion;
                // Cargar ejemplares iniciales
                ActualizarEstado(materialBibliografico.Estado.ToString());
                CargarEjemplares(materialBibliografico.IdMaterial);
                CargarContribuyentes();
                CargarEditoriales();
                Deshabilitar();
            }


        }

        private void CargarEditoriales()
        {
            BindingList<Editorial> es = new BindingList<Editorial>();
            es = materialBiblioBO.buscarEditorial(materialBibliografico.IdMaterial);
            if (es != null && es.Count > 0)
            {
                // Mostrar solo el primer contribuyente en el label
                lblEditorial1.InnerText = $"{es[0].Nombre}";

                // Llenar el modal con todos los contribuyentes
                rptEditoriales.DataSource = es;
                rptEditoriales.DataBind();
            }
            else
            {
                lblEditorial1.InnerText = "Sin editoriales registrados";
            }
        }

        private void CargarContribuyentes()
        {
            BindingList<Contribuyente> cs = new BindingList<Contribuyente>();
            cs = materialBiblioBO.buscarContribuyente(materialBibliografico.IdMaterial);
            Session["contribuyentes"] = cs;
            if (cs != null && cs.Count > 0)
            {
                // Mostrar solo el primer contribuyente en el label
                lblContribuyente.InnerText = $"{cs[0].Nombre + " " + cs[0].Primer_apellido + " " + cs[0].Segundo_apellido} ({cs[0].Tipo_contribuyente.ToString().ToLower()})";

                // Llenar el modal con todos los contribuyentes
                rptContribuyentes.DataSource = cs;
                rptContribuyentes.DataBind();
            }
            else
            {
                lblContribuyente.InnerText = "Sin contribuyentes registrados";
            }

        }
        private void ActualizarEstado(string estado)
        {
            lblEstado.InnerText = estado;
            lblEstado.Attributes["class"] = "badge rounded-pill px-3 py-2";

            switch (estado)
            {
                case "DISPONIBLE":
                    lblEstado.Attributes["class"] += " bg-success";
                    break;
                case "NO_DISPONIBLE":
                    lblEstado.Attributes["class"] += " bg-danger";
                    break;
                default:
                    lblEstado.Attributes["class"] += " bg-secondary";
                    break;
            }
        }
        public void Deshabilitar()
        {
            txtTitulo.Enabled = false;
            txtIdioma.Enabled = false;
            TextAnhioPubli.Enabled = false;
            TextNroPaginas.Enabled = false;
            txtTema.Enabled = false;
            TextISBN.Enabled = false;
            TextNroEjemplares.Enabled = false;
            txtTipoMaterial.Enabled = false;
            txtEdicion.Enabled = false;
        }
        public void CargarEjemplares(int id)
        {
            // 1️⃣ Obtener todas las bibliotecas existentes

            BindingList<Biblioteca> listaBibliotecas = ibbo.listarTodos();
            // 2️⃣ Obtener todos los ejemplares del libro, cada ejemplar ya incluye su biblioteca
            var ejemplares = materialBiblioBO.buscarEjemplares(id);

            // 3️⃣ Recorrer cada ejemplar y agruparlos por biblioteca
            foreach (var ejemplar in ejemplares)
            {
                int idBiblio = ejemplar.Biblioteca.IdBiblioteca;

                // Buscar si la biblioteca ya está en la lista
                var bibliotecaExistente = listaBibliotecas
                    .FirstOrDefault(b => b.IdBiblioteca == idBiblio);

                // Si no existe, crearla y agregarla a la lista
                if (bibliotecaExistente == null)
                {
                    bibliotecaExistente = new Biblioteca
                    {
                        IdBiblioteca = idBiblio,
                        Nombre = ejemplar.Biblioteca.Nombre
                    };
                    bibliotecaExistente.Ejemplares = new BindingList<Ejemplar>();
                    listaBibliotecas.Add(bibliotecaExistente);
                }

                // Agregar el ejemplar a la biblioteca correspondiente
                bibliotecaExistente.Ejemplares.Add(new Ejemplar
                {
                    IdEjemplar = ejemplar.IdEjemplar,
                    Ubicacion = ejemplar.Ubicacion,
                    Estado = ejemplar.Estado
                });
            }

            // 4️⃣ Contar ejemplares disponibles y prestados por biblioteca
            //foreach (var biblioteca in listaBibliotecas)
            //{
            //    biblioteca.Disponibles = biblioteca.Ejemplares
            //        .Count(e => e.Estado.Equals("DISPONIBLE"));

            //    biblioteca.Prestados = biblioteca.Ejemplares
            //        .Count(e => e.Estado.Equals("PRESTADO"));
            //}

            // 5️⃣ Vincular la lista al Repeater para mostrarla en la interfaz
            rptBibliotecas.DataSource = listaBibliotecas;
            rptBibliotecas.DataBind();

        }
        protected void btnPrestar_Click(object sender, EventArgs e)
        {
            biblioteca = ibbo.obtenerPorId(int.Parse(ddlbibliotecas.SelectedValue));
            Session["biblioteca"] = biblioteca;
            Response.Redirect("Prestamo.aspx");
        }

        public string GetEstadoCss(string estado)
        {
            switch (estado)
            {
                case "DISPONIBLE": return "badge bg-success";
                case "PRESTADO": return "badge bg-warning text-dark";
                case "EN_REPARACION": return "badge bg-danger";
                default: return "badge bg-secondary";
            }
        }

        protected void ddlbibliotecas_SelectedIndexChanged(object sender, EventArgs e)
        {
            materialBibliografico = (MaterialBibliografico)Session["material"];
            int idMaterial = materialBibliografico.IdMaterial;
            int idBibliotecaSeleccionada;

            // Si no hay selección, mostramos todas
            if (string.IsNullOrEmpty(ddlbibliotecas.SelectedValue))
            {
                CargarEjemplares(idMaterial);
                return;
            }

            idBibliotecaSeleccionada = int.Parse(ddlbibliotecas.SelectedValue);
            biblioteca = ibbo.obtenerPorId(idBibliotecaSeleccionada);
            // Filtrar ejemplares del libro por biblioteca seleccionada
            var ejemplares = materialBiblioBO.buscarEjemplares(idMaterial)
                                    .Where(x => x.Biblioteca.IdBiblioteca == idBibliotecaSeleccionada)
                                    .ToList();

            var bibliotecaSeleccionada = new Biblioteca
            {
                IdBiblioteca = idBibliotecaSeleccionada,
                Nombre = ibbo.listarTodos().First(b => b.IdBiblioteca == idBibliotecaSeleccionada).Nombre,
                Ejemplares = new BindingList<Ejemplar>(ejemplares)
            };

            rptBibliotecas.DataSource = new List<Biblioteca> { bibliotecaSeleccionada };
            rptBibliotecas.DataBind();
        }

    }
}