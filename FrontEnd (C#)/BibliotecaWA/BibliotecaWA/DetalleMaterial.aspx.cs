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
        private BibliotecaWSClient ibbo;
        private biblioteca biblioteca;
        private materialBibliografico materialBibliografico;
        private MaterialWSClient materialBiblioBO;
        private LibroWSClient librobo;
        private TesisWSClient tesisbo;
        private ArticuloWSClient articulobo;

        protected void Page_Load(object sender, EventArgs e)
        {
            //librobo = new LibroBOImpl();
            ibbo = new BibliotecaWSClient();
            biblioteca = new biblioteca();
            materialBiblioBO = new MaterialWSClient();
            materialBibliografico = new materialBibliografico();
            if (!IsPostBack)
            {
                string idMaterial = Request.QueryString["id"];
                // Cargar lista de bibliotecas en el dropdown
                // Cargar lista de bibliotecas en el dropdown
                ddlbibliotecas.DataSource = ibbo.ListarTodas();
                ddlbibliotecas.DataTextField = "Nombre";
                ddlbibliotecas.DataValueField = "IdBiblioteca";
                ddlbibliotecas.DataBind();
                // 🔹 Agregar un item por defecto al inicio
                ddlbibliotecas.Items.Insert(0, new ListItem("Seleccione una opción", ""));

                // Opcional: seleccionar por defecto ese valor
                ddlbibliotecas.SelectedIndex = 0;


                // Cargar el libro (solo una vez)
                materialBibliografico = materialBiblioBO.obtenerPorId(Convert.ToInt32(idMaterial));
                //libros = new BindingList<Libro>(librobo.listarTodos());
                //materialBibliografico = libros.Single(x => x.IdMaterial == 1);
                Session["material"] = materialBibliografico;


                txtTitulo.Text = materialBibliografico.titulo;
                txtTema.Text = materialBibliografico.clasificacion_tematica;
                TextAnhioPubli.Text = materialBibliografico.anho_publicacion.ToString();
                txtIdioma.Text = materialBibliografico.idioma;
                TextNroPaginas.Text = materialBibliografico.numero_paginas.ToString();
                materialBibliografico.ejemplares = materialBiblioBO.buscarEjemplares(materialBibliografico.idMaterial);
                if (materialBibliografico.ejemplares != null) TextNroEjemplares.Text = materialBibliografico.ejemplares.Length.ToString();
                else TextNroEjemplares.Text = "No tiene ejemplares registrados";
                txtTipoMaterial.Text = materialBibliografico.tipo.ToString();
                TextNroPaginas.Text = materialBibliografico.numero_paginas.ToString();
                txtEditoriales.Text=materialBibliografico.editoriales.ToString();
             


                switch (materialBibliografico.tipo.ToString().ToUpper())
                {
                    case "LIBRO":
                        librobo = new LibroWSClient();
                        libro lib = librobo.obtenerLibroPorId(materialBibliografico.idMaterial);
                        if (lib != null)
                        {
                            TextISBN.Text = lib.ISBN;
                            txtEdicion.Text = lib.edicion;
                            TextISBN.Enabled = false;
                            txtEdicion.Enabled = false;
                        }

                        // Ocultar campos de tesis
                        Institucion.Visible = false;
                        Especialidad.Visible = false;
                        Asesor.Visible = false;
                        Grado.Visible = false;

                        // Ocultar campos de artículo
                        ISSN.Visible = false;
                        Revista.Visible = false;
                        Volumen.Visible = false;
                        Número.Visible = false;
                        break;

                    case "TESIS":
                        tesisbo = new TesisWSClient();
                        tesis tes = tesisbo.obtenerTesisPorId(materialBibliografico.idMaterial);
                        if (tes != null)
                        {
                            TextInstitucionPublicacion.Text = tes.institucionPublicacion;
                            TextEspecialidad.Text = tes.especialidad;
                            TextAsesor.Text = tes.asesor;
                            TextGrado.Text = tes.grado;
                            TextInstitucionPublicacion.Enabled = false;
                            TextEspecialidad.Enabled = false;
                            TextAsesor.Enabled = false;
                            TextGrado.Enabled = false;
                        }

                        // Ocultar campos de libro
                        ISBN.Visible = false;
                        Edicion.Visible = false;    

                        // Ocultar campos de artículo
                        ISSN.Visible = false;
                        Revista.Visible = false;
                        Volumen.Visible = false;
                        Número.Visible = false;
                        break;

                    case "ARTICULO":
                        articulobo = new ArticuloWSClient();
                        articulo art = articulobo.obtenerArticuloPorId(materialBibliografico.idMaterial);
                        if (art != null)
                        {
                            TextISSN.Text = art.ISSN;
                            TextRevista.Text = art.revista;
                            TextVolumen.Text = art.volumen.ToString();
                            TextNumero.Text = art.numero.ToString();
                            TextISSN.Enabled = false;
                            TextRevista.Enabled = false;
                            TextVolumen.Enabled = false;    
                            TextNumero.Enabled = false;
                        }

                        // Ocultar campos de libro
                        ISBN.Visible = false;
                        Edicion.Visible = false;

                        // Ocultar campos de tesis
                        Institucion.Visible = false;
                        Especialidad.Visible = false;
                        Asesor.Visible = false;
                        Grado.Visible = false;
                        break;

                    default:
                        // Si no se reconoce el tipo, ocultar todo lo específico
                        TextISBN.Visible = false;
                        lblISBN.Visible = false;
                        txtEdicion.Visible = false;
                        lblEdicion.Visible = false;
                        TextInstitucionPublicacion.Visible = false;
                        lblInstitucionPublicacion.Visible = false;
                        TextEspecialidad.Visible = false;
                        lblEspecialidad.Visible = false;
                        TextAsesor.Visible = false;
                        lblAsesor.Visible = false;
                        TextGrado.Visible = false;
                        lblGrado.Visible = false;
                        TextISSN.Visible = false;
                        lblISSN.Visible = false;
                        TextRevista.Visible = false;
                        lblRevista.Visible = false;
                        TextVolumen.Visible = false;
                        lblVolumen.Visible = false;
                        TextNumero.Visible = false;
                        lblNumero.Visible = false;
                        break;
                }
                ActualizarEstado(materialBibliografico.estado.ToString());
                CargarEjemplares(materialBibliografico.idMaterial);
                CargarContribuyentes();
                CargarEditoriales();
                Deshabilitar();
            }


        }

        private void CargarEditoriales()
        {
            
        }

        private void CargarContribuyentes()
        {
            BindingList<contribuyente> cs;
            cs = new BindingList<contribuyente>(materialBiblioBO.buscarContribuyentes(materialBibliografico.idMaterial));
            Session["contribuyentes"] = cs;
            if (cs != null && cs.Count > 0)
            {
                // Mostrar solo el primer contribuyente en el label
                lblContribuyente.InnerText = $"{cs[0].nombre + " " + cs[0].primer_apellido + " " + cs[0].segundo_apellido} ({cs[0].tipo_contribuyente.ToString().ToLower()})";

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

            BindingList<biblioteca> listaBibliotecas = new BindingList<biblioteca>(ibbo.ListarTodas());
            // 2️⃣ Obtener todos los ejemplares del libro, cada ejemplar ya incluye su biblioteca
            var ejemplares = materialBiblioBO.buscarEjemplares(id);

            List<biblioteca> listaBibliotecasFinal = new List<biblioteca>();
            if (ejemplares == null || ejemplares.Length == 0)
            {
                panelSinEjemplares.Visible = true;
                rptBibliotecas.Visible = false;
                SelectorBiblioteca.Visible = false;
                btnPrestar.Enabled = false;
                return;
            }
            // 3️⃣ Recorrer cada ejemplar y agruparlos por biblioteca
            if (ejemplares != null)
            {
                foreach (var ejemplar in ejemplares)
                {
                    int idBiblio = ejemplar.blibioteca.idBiblioteca;
                    var bibliotecaExistente = listaBibliotecasFinal
                        .FirstOrDefault(b => b.idBiblioteca == idBiblio);

                    // Si no existe, crearla y agregarla a la lista
                    if (bibliotecaExistente == null)
                    {
                        bibliotecaExistente = new biblioteca
                        {
                            idBiblioteca = idBiblio,
                            nombre = ejemplar.blibioteca.nombre

                        };
                        bibliotecaExistente.ejemplares = new ejemplar[] { };
                        listaBibliotecasFinal.Add(bibliotecaExistente);
                    }

                    var listaTemp = bibliotecaExistente.ejemplares.ToList();
                    listaTemp.Add(new BibliotecaWA.BibliotecaServices.ejemplar
                    {
                        idEjemplar = ejemplar.idEjemplar,
                        ubicacion = ejemplar.ubicacion,
                        estado = ejemplar.estado // si es enum SOAP
                    });

                    // Convertimos de nuevo a array para asignar a la propiedad SOAP
                    bibliotecaExistente.ejemplares = listaTemp.ToArray();

                    rptBibliotecas.DataSource = listaBibliotecasFinal.ToArray();
                    rptBibliotecas.DataBind();
                }
            }

            // 5️⃣ Vincular la lista al Repeater para mostrarla en la interfaz
            rptBibliotecas.DataSource = listaBibliotecasFinal;
            rptBibliotecas.DataBind();

        }
        protected void btnPrestar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlbibliotecas.SelectedValue))
            {
                // Ejecutar el modal desde el servidor
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "MostrarModal", "mostrarModalSeleccionarBiblioteca();", true);
                return;
            }
            biblioteca = ibbo.obtenerBibliotecaPorId(int.Parse(ddlbibliotecas.SelectedValue));
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
            materialBibliografico = (materialBibliografico)Session["material"];


            int idMaterial = materialBibliografico.idMaterial;
            int idBibliotecaSeleccionada;

            // Si no hay selección, mostramos todas
            if (string.IsNullOrEmpty(ddlbibliotecas.SelectedValue))
            {
                CargarEjemplares(idMaterial);
                return;
            }

            idBibliotecaSeleccionada = int.Parse(ddlbibliotecas.SelectedValue);
            biblioteca = ibbo.obtenerBibliotecaPorId(idBibliotecaSeleccionada);
            // Filtrar ejemplares del libro por biblioteca seleccionada
            var ejemplares = materialBiblioBO.buscarEjemplares(idMaterial)
                                    .Where(x => x.blibioteca.idBiblioteca == idBibliotecaSeleccionada)
                                    .ToList();

            // 📌 Si NO HAY ejemplares → mostrar mensaje
            if (ejemplares == null || ejemplares.Count == 0)
            {
                rptBibliotecas.Visible = false;
                panelSinEjemplares.Visible = true;
                panelSinEjemplares.Controls.Clear(); // limpia mensajes previos
                btnPrestar.Visible = false;
                //btnPrestar.Enabled = false;
                panelSinEjemplares.Controls.Add(new Literal
                {
                    Text = $"No hay ejemplares disponibles en la biblioteca <b>{biblioteca.nombre}</b>."
                });

                return;
            }
            //btnPrestar.Enabled = true;
            btnPrestar.Visible = true;
            panelSinEjemplares.Visible = false;
            rptBibliotecas.Visible = true;

            var bibliotecaSeleccionada = new biblioteca
            {
                idBiblioteca = idBibliotecaSeleccionada,
                nombre = ibbo.ListarTodas().First(b => b.idBiblioteca == idBibliotecaSeleccionada).nombre,
                ejemplares = ejemplares.ToArray()
            };

            rptBibliotecas.DataSource = new List<biblioteca> { bibliotecaSeleccionada };
            rptBibliotecas.DataBind();
        }

    }
}