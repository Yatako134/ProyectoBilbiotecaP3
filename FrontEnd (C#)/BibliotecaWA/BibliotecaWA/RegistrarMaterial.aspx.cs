using BibliotecaWA.BibliotecaServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Exception = System.Exception;

namespace BibliotecaWA
{
    public partial class RegistrarMaterial : System.Web.UI.Page
    {
        //private UsuarioBOImpl bousuario;
        private MaterialWSClient bomaterial;
        private ConstribuyenteWSClient bocontribuyente;
        private BibliotecaWSClient bobiblioteca;
        private EjemplarWSClient boejemplar;
        private contribuyente contribuyente;
        private biblioteca biblioteca;
        private libro libro;
        private articulo articulo;
        private tesis tesis;

        int idunica;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idMaterial = Request.QueryString["id"];

                System.Diagnostics.Debug.WriteLine($"ID Material desde QueryString: {idMaterial}");
                contribuyente = new contribuyente();
                if (!string.IsNullOrEmpty(idMaterial))
                {
                    // DEBUG TEMPORAL - ELIMINAR DESPUÉS
                    System.Diagnostics.Debug.WriteLine("=== DEBUG FINAL ===");
                    System.Diagnostics.Debug.WriteLine($"¿Se ejecutó CargarEjemplaresMaterial?: Sí");
                    System.Diagnostics.Debug.WriteLine($"¿Se ejecutó CargarContribuyentesMaterial?: Sí");
                    // MODO EDICIÓN
                    System.Diagnostics.Debug.WriteLine("MODO EDICIÓN - Cargando material ID: " + idMaterial);
                    CargarMaterialParaEdicion(int.Parse(idMaterial));

                }
                else
                {
                    // MODO NUEVO
                    System.Diagnostics.Debug.WriteLine("MODO NUEVO - Inicializando formulario vacío");
                    //ScriptManager.RegisterStartupScript(this, GetType(), "InicializarEjemplar", "añadirEjemplar();", true);
                    //ScriptManager.RegisterStartupScript(this, GetType(), "InicializarContribuyente", "añadirContribuyente();", true);
                }
            }
        }

        private void CargarMaterialParaEdicion(int idMaterial)
        {
            /*
            try
            {
                MaterialWSClient materialBO = new MaterialWSClient();
                materialBibliografico material = materialBO.obtenerPorId(idMaterial);

                if (material != null)
                {

                    // Llenar campos 
                    txtCodigo.Text = material.idMaterial.ToString();
                    txtTitulo.Text = material.titulo;
                    txtAnho.Text = material.anho_publicacion.ToString();
                    txtPaginas.Text = material.numero_paginas.ToString();
                    TextTema.Text = material.clasificacion_tematica;
                    TextIdioma.Text = material.idioma;

                    // Tipo de material - IMPORTANTE: verifica el valor exacto

                    if(material.tipo.ToString() == "LIBRO")
                    {
                        ddlTipoMaterial.SelectedValue = "Libro";
                    }
                    if (material.tipo.ToString() == "TESIS")
                    {
                        ddlTipoMaterial.SelectedValue = "Tesis";
                    }
                    if (material.tipo.ToString() == "ARTICULO")
                    {
                        ddlTipoMaterial.SelectedValue = "Articulo";
                    }

                    System.Diagnostics.Debug.WriteLine($"Tipo seleccionado en dropdown: {ddlTipoMaterial.SelectedValue}");

                    // Campos específicos según tipo
                    CargarCamposEspecificos(material.tipo.ToString(),idMaterial);

                    // Cargar ejemplares y contribuyentes
                    CargarEjemplaresMaterial(idMaterial);
                    CargarContribuyentesMaterial(idMaterial);

                    // Guardar ID
                    hfIdMaterial.Value = idMaterial.ToString();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Material NO encontrado");
                    MostrarError("No se encontró el material con ID: " + idMaterial);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en CargarMaterialParaEdicion: {ex.Message}");
                MostrarError("Error al cargar material: " + ex.Message);
            }
            */
        }

        private void CargarCamposEspecificos(string tipoMaterial, int idmat)
        {
            System.Diagnostics.Debug.WriteLine($"Cargando campos específicos para: {tipoMaterial}");

            // Lógica para cargar campos específicos según el tipo
            switch (tipoMaterial.ToLower())
            {
                case "libro":
                    // Cargar datos de libro si existen
                    CargarDatosLibro(idmat);
                    break;
                case "tesis":
                    // Cargar datos de tesis si existen
                    CargarDatosTesis(idmat);
                    break;
                case "articulo":
                    // Cargar datos de artículo si existen
                    CargarDatosArticulo(idmat);
                    break;
            }

            // Forzar mostrar campos
            ScriptManager.RegisterStartupScript(this, GetType(), "MostrarCampos", "mostrarCampos();", true);
        }
        private void CargarDatosLibro(int idmat)
        {
            /*
            try
            {
                // Cargar datos específicos de libro
                LibroWSClient libroBO = new LibroWSClient();
                libro libro = new libro();
                libro = libroBO.obtenerPorId(idmat);
                if (libro != null)
                {
                    txtISBN.Text = libro.ISBN;
                    txtEdicion.Text = libro.edicion;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cargando datos de libro: {ex.Message}");
            }
            */

        }
        private void CargarDatosTesis(int idmat)
        {
            /* 
             try
             {
                 // Cargar datos específicos de tesis
                  TesisWSClient tesisBO = new TesisWSClient();
                 tesis tesis = new tesis();
                 tesis = tesisBO.obtenerPorId(idmat);
                  if (tesis != null)
                  {
                      txtEspecialidad.Text = tesis.especialidad;
                      txtAsesor.Text = tesis.asesor;
                      txtGrado.Text = tesis.grado;
                      txtInstitucion.Text = tesis.institucionPublicacion;
                  }
             }
             catch (Exception ex)
             {
                 System.Diagnostics.Debug.WriteLine($"Error cargando datos de tesis: {ex.Message}");
             }
             */
        }
        private void CargarDatosArticulo(int idmat)
        {
            /*
            try
            {
                // Cargar datos específicos de artículo
                 ArticuloWSClient articuloBO = new ArticuloWSClient();
                 articulo articulo = articuloBO.obtenerPorId(idmat);
                 if (articulo != null)
                 {
                     txtISSN.Text = articulo.ISSN;
                     txtRevista.Text = articulo.revista;
                     txtVolumen.Text = articulo.volumen.ToString();
                     txtNumero.Text = articulo.numero.ToString();
                 }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cargando datos de artículo: {ex.Message}");
            }
            */
        }
        private void CargarEjemplaresMaterial(int idMaterial)
        {
            /*
            try
            {
                // Si material.Ejemplares está vacío, cargar directamente desde el BO
                EjemplarWSClient ejemplarBO = new EjemplarWSClient();
                //Session["usuarios"] = new BindingList<usuario>(bousuario.listarUsuarios());
                BindingList<ejemplar> ejemplares = new BindingList<ejemplar>(ejemplarBO.listar_disponibles_por_material(idMaterial));

                System.Diagnostics.Debug.WriteLine($"=== CARGANDO EJEMPLARES DIRECTAMENTE ===");
                System.Diagnostics.Debug.WriteLine($"Ejemplares encontrados: {ejemplares?.Count ?? 0}");

                if (ejemplares != null && ejemplares.Count > 0)
                {
                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string ejemplaresJson = serializer.Serialize(ejemplares);

                    System.Diagnostics.Debug.WriteLine($"JSON Ejemplares: {ejemplaresJson}");

                    string script = $"console.log('Cargando {ejemplares.Count} ejemplares desde BO directo'); cargarEjemplaresExistente({ejemplaresJson});";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CargarEjemplares", script, true);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No hay ejemplares en el BO directo");
                    string script = "console.log('No hay ejemplares - inicializando vacío'); añadirEjemplar();";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CargarEjemplares", script, true);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en CargarEjemplaresMaterial: {ex.Message}");
                string script = $"console.error('Error cargando ejemplares: {ex.Message}'); añadirEjemplar();";
                ScriptManager.RegisterStartupScript(this, GetType(), "CargarEjemplares", script, true);
            }
            */
        }


        private void CargarContribuyentesMaterial(int idMaterial)
        {
            /*
            try
            {
                // Si material.Contribuyentes está vacío, cargar directamente desde el BO
                //Session["usuarios"] = new BindingList<usuario>(bousuario.listarUsuarios());
                ConstribuyenteWSClient contribuyenteBO = new ConstribuyenteWSClient();
                BindingList<contribuyente> contribuyentes = new BindingList<contribuyente>(contribuyenteBO.listar_contribuyentes_por_material(idMaterial));

                System.Diagnostics.Debug.WriteLine($"=== CARGANDO CONTRIBUYENTES DIRECTAMENTE ===");
                System.Diagnostics.Debug.WriteLine($"Contribuyentes encontrados: {contribuyentes?.Count ?? 0}");

                if (contribuyentes != null && contribuyentes.Count > 0)
                {
                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string contribuyentesJson = serializer.Serialize(contribuyentes);

                    System.Diagnostics.Debug.WriteLine($"JSON Contribuyentes: {contribuyentesJson}");

                    string script = $"console.log('Cargando {contribuyentes.Count} contribuyentes desde BO directo'); cargarContribuyentesExistente({contribuyentesJson});";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CargarContribuyentes", script, true);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No hay contribuyentes en el BO directo");
                    string script = "console.log('No hay contribuyentes - inicializando vacío'); añadirContribuyente();";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CargarContribuyentes", script, true);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en CargarContribuyentesMaterial: {ex.Message}");
                string script = $"console.error('Error cargando contribuyentes: {ex.Message}'); añadirContribuyente();";
                ScriptManager.RegisterStartupScript(this, GetType(), "CargarContribuyentes", script, true);
            }
            */
        }

        protected void GuardarMaterial_Click(object sender, EventArgs e)
        {

            try
            {
                // Validar datos antes de proceder
                if (!ValidarDatos())
                    return;

                // Validar que haya ejemplares antes de guardar el material
                if (!ValidarEjemplaresAntesDeGuardar())
                    return;

                // Inicializar BOs
                bomaterial = new MaterialWSClient();
                bocontribuyente = new ConstribuyenteWSClient();
                boejemplar = new EjemplarWSClient();
                bobiblioteca = new BibliotecaWSClient();

                // Guardar en orden: Material -> Contribuyentes -> Ejemplares
                GuardarMaterialPorTipo();

                if (idunica > 0)
                {
                    GuardarContribuyentes();
                    GuardarEjemplares();

                    lblMensaje.Text = "Material bibliográfico guardado exitosamente!";
                    lblMensaje.CssClass = "alert alert-success";
                    Response.Redirect("GestMaterial.aspx");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error al guardar el material: {ex.Message}";
                lblMensaje.CssClass = "alert alert-danger";
            }

        }

        private bool ValidarDatos()
        {
            // Validar campos comunes
            if (string.IsNullOrEmpty(txtTitulo.Text))
            {
                MostrarError("El título es requerido");
                return false;
            }

            if (!int.TryParse(txtAnho.Text, out int anho) || anho < 1000 || anho > DateTime.Now.Year + 1)
            {
                MostrarError("El año de publicación debe ser un año válido");
                return false;
            }

            if (!int.TryParse(txtPaginas.Text, out int paginas) || paginas <= 0)
            {
                MostrarError("El número de páginas debe ser un valor positivo");
                return false;
            }

            // Validar tipo de material seleccionado
            if (string.IsNullOrEmpty(ddlTipoMaterial.SelectedValue))
            {
                MostrarError("Debe seleccionar un tipo de material");
                return false;
            }

            // Validar campos específicos según el tipo
            string tipoMaterial = ddlTipoMaterial.SelectedValue;
            switch (tipoMaterial)
            {
                case "Libro":
                    if (string.IsNullOrEmpty(txtISBN.Text))
                    {
                        MostrarError("El ISBN es requerido para libros");
                        return false;
                    }
                    break;
                case "Tesis":
                    if (string.IsNullOrEmpty(txtEspecialidad.Text))
                    {
                        MostrarError("La especialidad es requerida para tesis");
                        return false;
                    }
                    break;
                case "Articulo":
                    if (string.IsNullOrEmpty(txtISSN.Text))
                    {
                        MostrarError("El ISSN es requerido para artículos");
                        return false;
                    }
                    break;
            }

            return true;
        }

        private bool ValidarEjemplaresAntesDeGuardar()
        {
            // Validar que existan ejemplares en el formulario
            string[] bibliotecas = Request.Form.GetValues("biblioteca[]");
            string[] ubicaciones = Request.Form.GetValues("ubicacion[]");

            if (bibliotecas == null || ubicaciones == null || bibliotecas.Length == 0)
            {
                lblMensaje.Text = "Error: Debe registrar al menos un ejemplar antes de guardar el material.";
                lblMensaje.CssClass = "alert alert-danger";
                return false;
            }

            // Validar que todos los ejemplares tengan datos completos
            List<string> ejemplaresIncompletos = new List<string>();

            for (int i = 0; i < bibliotecas.Length; i++)
            {
                if (string.IsNullOrEmpty(bibliotecas[i]) || string.IsNullOrEmpty(ubicaciones[i]?.Trim()))
                {
                    ejemplaresIncompletos.Add($"Ejemplar {i + 1}");
                }
            }

            if (ejemplaresIncompletos.Count > 0)
            {
                lblMensaje.Text = $"Error: Los siguientes ejemplares están incompletos: {string.Join(", ", ejemplaresIncompletos)}. Complete todos los campos antes de guardar.";
                lblMensaje.CssClass = "alert alert-danger";
                return false;
            }

            return true;
        }

        private void MostrarError(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = "alert alert-danger";
        }

        private void GuardarContribuyentes()
        {
            try
            {
                // Obtener los valores (MANTENIENDO tu código original que funcionaba)
                var tipoContribuyenteList = Request.Form.GetValues("autor[]");
                var nombreList = Request.Form.GetValues("nombre[]");
                var primerApellidoList = Request.Form.GetValues("primer_apellido[]");
                var segundoApellidoList = Request.Form.GetValues("segundo_apellido[]");
                var seudonimoList = Request.Form.GetValues("seudonimo[]");

                if (tipoContribuyenteList != null && nombreList != null && primerApellidoList != null)
                {
                    for (int i = 0; i < tipoContribuyenteList.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(tipoContribuyenteList[i]) &&
                            !string.IsNullOrEmpty(nombreList[i]) &&
                            !string.IsNullOrEmpty(primerApellidoList[i]))
                        {
                            try
                            {
                                var tipoContribuyenteStr = tipoContribuyenteList[i];

                                // USAR EL MÉTODO TEMPORAL con parámetros individuales
                                bocontribuyente = new ConstribuyenteWSClient();
                                int idContribuyente = bocontribuyente.insertarContribuyente(
                                    nombreList[i],
                                    primerApellidoList[i],
                                    segundoApellidoList?[i] ?? "",
                                    seudonimoList?[i] ?? "",
                                    tipoContribuyenteStr
                                );

                                if (idContribuyente > 0 && idunica > 0)
                                {
                                    bocontribuyente.asignarContribuyente(idunica, idContribuyente);
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Error contribuyente {i}: {ex.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error general: {ex.Message}");
            }
        }



        private void GuardarMaterialPorTipo()
        {
            string tipoMaterial = ddlTipoMaterial.SelectedValue;

            try
            {
                switch (tipoMaterial)
                {
                    case "Libro":
                        GuardarLibro();
                        break;
                    case "Tesis":
                        GuardarTesis();
                        break;
                    case "Articulo":
                        GuardarArticulo();
                        break;
                    default:
                        lblMensaje.Text = "Por favor, seleccione un tipo de material válido.";
                        break;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error al guardar el material: {ex.Message}";
            }
        }

        private void GuardarArticulo()
        {
            try
            {
                articulo art = new articulo();
                ArticuloWSClient artiDAO = new ArticuloWSClient();

                art.titulo = txtTitulo.Text;
                art.anho_publicacion = Convert.ToInt32(txtAnho.Text);
                art.numero_paginas = Convert.ToInt32(txtPaginas.Text);
                art.clasificacion_tematica = TextTema.Text;
                art.idioma = TextIdioma.Text;
                art.ISSN = txtISSN.Text;
                art.revista = txtRevista.Text;
                art.volumen = Convert.ToInt32(txtVolumen.Text);
                art.numero = Convert.ToInt32(txtNumero.Text);

                idunica = artiDAO.insertarArticulo(art);
                lblMensaje.Text = "Artículo guardado exitosamente!";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error al guardar artículo: {ex.Message}";
            }
        }

        private void GuardarLibro()
        {
            try
            {
                // Asumiendo que tienes una clase Libro similar a Articulo
                libro libro = new libro();
                LibroWSClient libroDAO = new LibroWSClient();

                libro.titulo = txtTitulo.Text;
                libro.anho_publicacion = Convert.ToInt32(txtAnho.Text);
                libro.numero_paginas = Convert.ToInt32(txtPaginas.Text);
                libro.clasificacion_tematica = TextTema.Text;
                libro.idioma = TextIdioma.Text;
                libro.ISBN = txtISBN.Text;
                libro.edicion = txtEdicion.Text;

                idunica = libroDAO.insertarLibro(libro);

                lblMensaje.Text = "Libro guardado exitosamente!";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error al guardar libro: {ex.Message}";
            }
        }

        private void GuardarTesis()
        {
            try
            {
                // Asumiendo que tienes una clase Tesis similar a Articulo
                tesis tesis = new tesis();
                TesisWSClient tesisDAO = new TesisWSClient();

                tesis.titulo = txtTitulo.Text;
                tesis.anho_publicacion = Convert.ToInt32(txtAnho.Text);
                tesis.numero_paginas = Convert.ToInt32(txtPaginas.Text);
                tesis.clasificacion_tematica = TextTema.Text;
                tesis.idioma = TextIdioma.Text;
                tesis.especialidad = txtEspecialidad.Text;
                tesis.asesor = txtAsesor.Text;
                tesis.grado = txtGrado.Text;
                tesis.institucionPublicacion = txtInstitucion.Text;

                idunica = tesisDAO.insertarTesis(tesis);
                lblMensaje.Text = "Tesis guardada exitosamente!";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error al guardar tesis: {ex.Message}";
            }
        }

        // Método público para que el JavaScript pueda acceder a las opciones de bibliotecas
        public string GetBibliotecasOptions()
        {
            try
            {
                bobiblioteca = new BibliotecaWSClient();
                var bibliotecas = bobiblioteca.ListarTodas();

                string options = "";
                foreach (var biblioteca in bibliotecas)
                {
                    options += $"<option value='{biblioteca.idBiblioteca}'>{biblioteca.nombre}</option>";
                }

                return options;
            }
            catch (Exception ex)
            {
                return "<option value=''>Error al cargar bibliotecas</option>";
            }
        }

        private void GuardarEjemplares()
        {
            // Obtener arrays de datos de los ejemplares
            string[] bibliotecas = Request.Form.GetValues("biblioteca[]");
            string[] ubicaciones = Request.Form.GetValues("ubicacion[]");

            if (bibliotecas == null || ubicaciones == null || bibliotecas.Length == 0)
            {
                throw new Exception("Debe registrar al menos un ejemplar.");
            }

            int ejemplaresGuardados = 0;
            List<string> errores = new List<string>();

            // Guardar cada ejemplar
            for (int i = 0; i < bibliotecas.Length; i++)
            {
                try
                {
                    string idBiblioteca = bibliotecas[i];
                    string ubicacion = ubicaciones[i];

                    if (string.IsNullOrEmpty(idBiblioteca) || string.IsNullOrEmpty(ubicacion))
                    {
                        errores.Add($"Ejemplar {i + 1}: Faltan datos requeridos");
                        continue;
                    }
                    ejemplar ejemplar = new ejemplar();
                    ejemplar.estado = estadoEjemplar.DISPONIBLE;
                    ejemplar.ubicacion = ubicacion;
                    biblioteca bib = new biblioteca();
                    bib.idBiblioteca = Convert.ToInt32(idBiblioteca);
                    ejemplar.blibioteca = bib;
                    // Crear y guardar el ejemplar
                    /*
                    ejemplar ejemplar = new ejemplar
                    {
                        id_material = 0,
                        Idbiblioteca = Convert.ToInt32(idBiblioteca),
                        ubicacion = ubicacion,
                        estado = estadoEjemplar.DISPONIBLE // Estado por defecto
                    };
                    */
                    ejemplar.id_material = idunica;
                    int idEjemplar = boejemplar.insertarEjemplar(ejemplar);
                    if (idEjemplar > 0)
                    {
                        ejemplaresGuardados++;
                    }
                    else
                    {
                        errores.Add($"Ejemplar {i + 1}: No se pudo guardar en la base de datos");
                    }
                }
                catch (Exception ex)
                {
                    errores.Add($"Ejemplar {i + 1}: {ex.Message}");
                }
            }

            if (ejemplaresGuardados == 0)
            {
                throw new Exception("No se pudo guardar ningún ejemplar. Errores: " + string.Join("; ", errores));
            }
            else if (errores.Count > 0)
            {
                // Si se guardaron algunos pero hubo errores, mostrar advertencia
                lblMensaje.Text += $" Se guardaron {ejemplaresGuardados} ejemplares, pero hubo errores: {string.Join("; ", errores)}";
            }
        }



    }
}