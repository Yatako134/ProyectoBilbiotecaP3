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
        private int idunica;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idMaterial = Request.QueryString["id"];
                
                if (!string.IsNullOrEmpty(idMaterial))
                {
                    CargarMaterialParaEdicion(int.Parse(idMaterial));
                    ddlTipoMaterial.Enabled = false;
                    ddlTipoMaterial.CssClass += " bg-light";
                }
            }
        }

        private void CargarMaterialParaEdicion(int idMaterial)
        {
            try
            {
                MaterialWSClient materialBO = new MaterialWSClient();
                materialBibliografico material = materialBO.obtenerPorId(idMaterial);

                if (material != null)
                {
                    // Llenar campos básicos
                    txtCodigo.Text = material.idMaterial.ToString();
                    txtTitulo.Text = material.titulo;
                    txtAnho.Text = material.anho_publicacion.ToString();
                    txtPaginas.Text = material.numero_paginas.ToString();
                    TextTema.Text = material.clasificacion_tematica;
                    TextIdioma.Text = material.idioma;
                    TextEditorial.Text = material.editoriales;
                    // Tipo de material - con más debug
                    string tipoDesdeBD = material.tipo.ToString();
                    System.Diagnostics.Debug.WriteLine($"🎯 Tipo desde BD normalizado: '{tipoDesdeBD}'");

                    // Tipo de material
                    if (material.tipo.ToString() == "LIBRO")
                    {
                        ddlTipoMaterial.SelectedValue = "Libro";
                    }
                    else if (material.tipo.ToString() == "TESIS")
                    {
                        ddlTipoMaterial.SelectedValue = "Tesis";
                    }
                    else if (material.tipo.ToString() == "ARTICULO")
                    {
                        ddlTipoMaterial.SelectedValue = "Articulo";
                    }

                    // Campos específicos según tipo
                    CargarCamposEspecificos(material.tipo.ToString(), idMaterial);

                    // Cargar ejemplares y contribuyentes
                    CargarEjemplaresMaterial(idMaterial);
                    CargarContribuyentesMaterial(idMaterial);

                    // Guardar ID
                    hfIdMaterial.Value = idMaterial.ToString();
                }
                else
                {
                    MostrarError("No se encontró el material con ID: " + idMaterial);
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar material: " + ex.Message);
            }
        }

        private void CargarCamposEspecificos(string tipoMaterial, int idmat)
        {
            if (tipoMaterial.ToLower() == "libro")
            {
                CargarDatosLibro(idmat);
            }
            else if (tipoMaterial.ToLower() == "tesis")
            {
                CargarDatosTesis(idmat);
            }
            else if (tipoMaterial.ToLower() == "articulo")
            {
                CargarDatosArticulo(idmat);
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "MostrarCampos", "mostrarCampos();", true);
        }

        private void CargarDatosLibro(int idmat)
        {
            try
            {
                LibroWSClient libroBO = new LibroWSClient();
                libro libro = libroBO.obtenerLibroPorId(idmat);
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
        }

        private void CargarDatosTesis(int idmat)
        {
            try
            {
                TesisWSClient tesisBO = new TesisWSClient();
                tesis tesis = tesisBO.obtenerTesisPorId(idmat);
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
        }

        private void CargarDatosArticulo(int idmat)
        {
            try
            {
                ArticuloWSClient articuloBO = new ArticuloWSClient();
                articulo articulo = articuloBO.obtenerArticuloPorId(idmat);
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
        }

        private void CargarEjemplaresMaterial(int idMaterial)
        {
            try
            {
                EjemplarWSClient ejemplarBO = new EjemplarWSClient();
                BindingList<ejemplar> ejemplares = new BindingList<ejemplar>(ejemplarBO.listar_por_material(idMaterial));

                if (ejemplares != null && ejemplares.Count > 0)
                {
                    var ejemplaresParaJson = ejemplares.Select(e => new
                    {
                        IdEjemplar = e.idEjemplar,
                        CodigoEjemplar = e.idEjemplar,
                        BibliotecaId = e.blibioteca.idBiblioteca,
                        Ubicacion = e.ubicacion,
                        Estado = e.estado.ToString() ?? "DISPONIBLE"
                    }).ToList();

                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string ejemplaresJson = serializer.Serialize(ejemplaresParaJson);

                    string script = $"cargarEjemplaresExistente({ejemplaresJson});";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CargarEjemplares", script, true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CargarEjemplares", "añadirEjemplar();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CargarEjemplares", "añadirEjemplar();", true);
            }
        }

        private void CargarContribuyentesMaterial(int idMaterial)
        {
            try
            {
                ConstribuyenteWSClient contribuyenteBO = new ConstribuyenteWSClient();
                BindingList<contribuyente> contribuyentes = new BindingList<contribuyente>(contribuyenteBO.listar_contribuyentes_por_material(idMaterial));

                if (contribuyentes != null && contribuyentes.Count > 0)
                {
                    var contribuyentesParaJson = contribuyentes.Select(c => new
                    {
                        IdContribuyente = c.idContribuyente,
                        Nombre = c.nombre,
                        Primer_apellido = c.primer_apellido,
                        Segundo_apellido = c.segundo_apellido,
                        Seudonimo = c.seudonimo,
                        Tipo_contribuyente = ConvertirTipo(c.tipo_contribuyente.ToString())
                    }).ToList();

                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string contribuyentesJson = serializer.Serialize(contribuyentesParaJson);

                    string script = $"cargarContribuyentesExistente({contribuyentesJson});";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CargarContribuyentes", script, true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CargarContribuyentes", "añadirContribuyente();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CargarContribuyentes", "añadirContribuyente();", true);
            }
        }

        // Método para convertir string a número (como en el sistema funcional)
        private int ConvertirTipo(string tipoStr)
        {
            if (string.IsNullOrEmpty(tipoStr)) return 0;

            switch (tipoStr.ToUpper())
            {
                case "AUTOR": return 0;
                case "TRADUCTOR": return 1;
                case "EDITOR": return 2;
                default: return 0;
            }
        }

        protected void GuardarMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarTodosLosErrores();

                if (!ValidarDatos())
                    return;

                bool esEdicion = !string.IsNullOrEmpty(hfIdMaterial.Value);
                int idMaterial;

                // === VALIDACIÓN DE UNICIDAD ISBN/ISSN ===
                string tipoMaterial = ddlTipoMaterial.SelectedValue;

                if (tipoMaterial == "Libro" && !esEdicion)
                {
                    /*
                    LibroWSClient libroBO = new LibroWSClient();
                    if (libroBO.existeISBN(txtISBN.Text))
                    {
                        MostrarErrorCampo(txtISBN, lblErrorISBN, "❌ Este ISBN ya está registrado en el sistema");
                        return;
                    }
                    */
                }
                else if (tipoMaterial == "Articulo" && !esEdicion)
                {
                    /*
                    ArticuloWSClient articuloBO = new ArticuloWSClient();
                    if (articuloBO.existeISSN(txtISSN.Text))
                    {
                        MostrarErrorCampo(txtISSN, lblErrorISSN, "❌ Este ISSN ya está registrado en el sistema");
                        return;
                    }
                    */
                }

                if (esEdicion)
                {
                    // MODO EDICIÓN
                    idMaterial = int.Parse(hfIdMaterial.Value);
                    ActualizarMaterialExistente(idMaterial);
                    ManejarContribuyentesEnEdicion(idMaterial);
                    ActualizarEjemplaresExistentes(idMaterial);
                    MostrarExito("Material actualizado exitosamente!");
                }
                else
                {
                    // MODO NUEVO
                    idMaterial = GuardarMaterialPorTipo();
                    GuardarContribuyentes(idMaterial);
                    GuardarEjemplares(idMaterial);
                    MostrarExito("Material registrado exitosamente!");
                }

                Response.Redirect("GestMaterial.aspx");
            }
            catch (Exception ex)
            {
                MostrarError($"Error al guardar el material: {ex.Message}");
            }
        }

        // ===== MÉTODOS DE VALIDACIÓN MEJORADOS =====
        private bool ValidarDatos()
        {
            bool esValido = true;

            // Validar título
            if (string.IsNullOrEmpty(txtTitulo.Text))
            {
                MostrarErrorCampo(txtTitulo, lblErrorTitulo, "El título es requerido");
                esValido = false;
            }
            else if (!EsTituloValido(txtTitulo.Text))
            {
                MostrarErrorCampo(txtTitulo, lblErrorTitulo, "El título contiene caracteres no permitidos");
                esValido = false;
            }

            // Validar tipo de material
            if (string.IsNullOrEmpty(ddlTipoMaterial.SelectedValue))
            {
                MostrarErrorCampo(ddlTipoMaterial, lblErrorTipoMaterial, "Debe seleccionar un tipo de material");
                esValido = false;
            }

            // Validar año de publicación
            if (string.IsNullOrEmpty(txtAnho.Text))
            {
                MostrarErrorCampo(txtAnho, lblErrorAnho, "El año de publicación es requerido");
                esValido = false;
            }
            else if (!int.TryParse(txtAnho.Text, out int anho) || anho < 1000 || anho > DateTime.Now.Year + 1)
            {
                MostrarErrorCampo(txtAnho, lblErrorAnho, $"El año debe estar entre 1000 y {DateTime.Now.Year + 1}");
                esValido = false;
            }

            // Validar número de páginas
            if (string.IsNullOrEmpty(txtPaginas.Text))
            {
                MostrarErrorCampo(txtPaginas, lblErrorPaginas, "El número de páginas es requerido");
                esValido = false;
            }
            else if (!int.TryParse(txtPaginas.Text, out int paginas) || paginas <= 0 || paginas > 10000)
            {
                MostrarErrorCampo(txtPaginas, lblErrorPaginas, "El número de páginas debe ser positivo y no exceder 10,000");
                esValido = false;
            }

            // Validar tema
            if (string.IsNullOrEmpty(TextTema.Text))
            {
                MostrarErrorCampo(TextTema, lblErrorTema, "El tema es requerido");
                esValido = false;
            }
            else if (!EsCampoGeneralValido(TextTema.Text))
            {
                MostrarErrorCampo(TextTema, lblErrorTema, "El tema contiene caracteres no permitidos");
                esValido = false;
            }

            // Validar idioma
            if (string.IsNullOrEmpty(TextIdioma.Text))
            {
                MostrarErrorCampo(TextIdioma, lblErrorIdioma, "El idioma es requerido");
                esValido = false;
            }
            else if (!EsIdiomaValido(TextIdioma.Text))
            {
                MostrarErrorCampo(TextIdioma, lblErrorIdioma, "El idioma solo puede contener letras y espacios");
                esValido = false;
            }

            // Validar campos específicos por tipo de material
            string tipoMaterial = ddlTipoMaterial.SelectedValue;

            if (tipoMaterial == "Libro")
            {
                // Validar ISBN
                if (string.IsNullOrEmpty(txtISBN.Text))
                {
                    MostrarErrorCampo(txtISBN, lblErrorISBN, "El ISBN es requerido para libros");
                    esValido = false;
                }
                else if (!EsISBNValido(txtISBN.Text))
                {
                    MostrarErrorCampo(txtISBN, lblErrorISBN, "El formato del ISBN no es válido");
                    esValido = false;
                }

                // Validar edición
                if (string.IsNullOrEmpty(txtEdicion.Text))
                {
                    MostrarErrorCampo(txtEdicion, lblErrorEdicion, "La edición es requerida para libros");
                    esValido = false;
                }
                else if (!EsCampoGeneralValido(txtEdicion.Text))
                {
                    MostrarErrorCampo(txtEdicion, lblErrorEdicion, "La edición contiene caracteres no permitidos");
                    esValido = false;
                }
            }
            else if (tipoMaterial == "Tesis")
            {
                // Validar campos de tesis
                if (string.IsNullOrEmpty(txtEspecialidad.Text))
                {
                    MostrarErrorCampo(txtEspecialidad, lblErrorEspecialidad, "La especialidad es requerida para tesis");
                    esValido = false;
                }
                else if (!EsCampoGeneralValido(txtEspecialidad.Text))
                {
                    MostrarErrorCampo(txtEspecialidad, lblErrorEspecialidad, "La especialidad contiene caracteres no permitidos");
                    esValido = false;
                }

                if (string.IsNullOrEmpty(txtAsesor.Text))
                {
                    MostrarErrorCampo(txtAsesor, lblErrorAsesor, "El asesor es requerido para tesis");
                    esValido = false;
                }
                else if (!EsCampoGeneralValido(txtAsesor.Text))
                {
                    MostrarErrorCampo(txtAsesor, lblErrorAsesor, "El asesor contiene caracteres no permitidos");
                    esValido = false;
                }

                if (string.IsNullOrEmpty(txtGrado.Text))
                {
                    MostrarErrorCampo(txtGrado, lblErrorGrado, "El grado es requerido para tesis");
                    esValido = false;
                }
                else if (!EsCampoGeneralValido(txtGrado.Text))
                {
                    MostrarErrorCampo(txtGrado, lblErrorGrado, "El grado contiene caracteres no permitidos");
                    esValido = false;
                }

                if (string.IsNullOrEmpty(txtInstitucion.Text))
                {
                    MostrarErrorCampo(txtInstitucion, lblErrorInstitucion, "La institución de publicación es requerida para tesis");
                    esValido = false;
                }
                else if (!EsCampoGeneralValido(txtInstitucion.Text))
                {
                    MostrarErrorCampo(txtInstitucion, lblErrorInstitucion, "La institución contiene caracteres no permitidos");
                    esValido = false;
                }
            }
            else if (tipoMaterial == "Articulo")
            {
                // Validar ISSN
                if (string.IsNullOrEmpty(txtISSN.Text))
                {
                    MostrarErrorCampo(txtISSN, lblErrorISSN, "El ISSN es requerido para artículos");
                    esValido = false;
                }
                else if (!EsISSNValido(txtISSN.Text))
                {
                    MostrarErrorCampo(txtISSN, lblErrorISSN, "El formato del ISSN no es válido");
                    esValido = false;
                }

                // Validar revista
                if (string.IsNullOrEmpty(txtRevista.Text))
                {
                    MostrarErrorCampo(txtRevista, lblErrorRevista, "La revista es requerida para artículos");
                    esValido = false;
                }
                else if (!EsCampoGeneralValido(txtRevista.Text))
                {
                    MostrarErrorCampo(txtRevista, lblErrorRevista, "La revista contiene caracteres no permitidos");
                    esValido = false;
                }

                // Validar volumen
                if (string.IsNullOrEmpty(txtVolumen.Text))
                {
                    MostrarErrorCampo(txtVolumen, lblErrorVolumen, "El volumen es requerido para artículos");
                    esValido = false;
                }
                else if (!int.TryParse(txtVolumen.Text, out int volumen) || volumen < 0)
                {
                    MostrarErrorCampo(txtVolumen, lblErrorVolumen, "El volumen debe ser un número positivo");
                    esValido = false;
                }

                // Validar número
                if (string.IsNullOrEmpty(txtNumero.Text))
                {
                    MostrarErrorCampo(txtNumero, lblErrorNumero, "El número es requerido para artículos");
                    esValido = false;
                }
                else if (!int.TryParse(txtNumero.Text, out int numero) || numero < 0)
                {
                    MostrarErrorCampo(txtNumero, lblErrorNumero, "El número debe ser un número positivo");
                    esValido = false;
                }
            }

            // Validar contribuyentes
            if (!ValidarContribuyentesFormulario())
                esValido = false;

            // Validar ejemplares
            if (!ValidarEjemplaresFormulario())
                esValido = false;

            return esValido;
        }

        private bool ValidarContribuyentesFormulario()
        {
            var nombres = Request.Form.GetValues("nombre[]");
            var primerApellidos = Request.Form.GetValues("primer_apellido[]");
            var segundoApellidos = Request.Form.GetValues("segundo_apellido[]");

            // === MODIFICACIÓN: Los contribuyentes son OPCIONALES ===
            // Si no hay contribuyentes, es válido
            if (nombres == null || nombres.Length == 0)
            {
                return true; // Cambiado de false a true
            }

            // Si hay contribuyentes, validar los que tengan datos
            for (int i = 0; i < nombres.Length; i++)
            {
                // Solo validar si el contribuyente tiene datos
                bool tieneDatos = !string.IsNullOrEmpty(nombres[i]) ||
                                 !string.IsNullOrEmpty(primerApellidos[i]) ||
                                 !string.IsNullOrEmpty(segundoApellidos[i]);

                if (tieneDatos)
                {
                    if (string.IsNullOrEmpty(nombres[i]))
                    {
                        MostrarError($"El nombre del contribuyente {i + 1} es requerido");
                        return false;
                    }

                    if (!EsNombreValido(nombres[i]))
                    {
                        MostrarError($"El nombre del contribuyente {i + 1} contiene caracteres no permitidos");
                        return false;
                    }

                    if (string.IsNullOrEmpty(primerApellidos[i]))
                    {
                        MostrarError($"El primer apellido del contribuyente {i + 1} es requerido");
                        return false;
                    }

                    if (!EsNombreValido(primerApellidos[i]))
                    {
                        MostrarError($"El primer apellido del contribuyente {i + 1} contiene caracteres no permitidos");
                        return false;
                    }

                    if (string.IsNullOrEmpty(segundoApellidos[i]))
                    {
                        MostrarError($"El segundo apellido del contribuyente {i + 1} es requerido");
                        return false;
                    }

                    if (!EsNombreValido(segundoApellidos[i]))
                    {
                        MostrarError($"El segundo apellido del contribuyente {i + 1} contiene caracteres no permitidos");
                        return false;
                    }

                    // Validar seudónimo (puede estar vacío)
                    var seudonimos = Request.Form.GetValues("seudonimo[]");
                    if (seudonimos != null && i < seudonimos.Length && !string.IsNullOrEmpty(seudonimos[i]))
                    {
                        if (!EsNombreValido(seudonimos[i]))
                        {
                            MostrarError($"El seudónimo del contribuyente {i + 1} contiene caracteres no permitidos");
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private void MostrarModal(string modalId = "modalAdvertencia")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "MostrarModal",
                $"new bootstrap.Modal(document.getElementById('{modalId}')).show();", true);
        }

        private bool ValidarEjemplaresFormulario()
        {
            var bibliotecas = Request.Form.GetValues("biblioteca[]");
            var ubicaciones = Request.Form.GetValues("ubicacion[]");

            if (bibliotecas == null || ubicaciones == null || bibliotecas.Length == 0)
            {
                MostrarModal();
                return false;
            }

            for (int i = 0; i < ubicaciones.Length; i++)
            {
                if (string.IsNullOrEmpty(ubicaciones[i]))
                {
                    MostrarError($"La ubicación del ejemplar {i + 1} es requerida");
                    return false;
                }

                if (!EsUbicacionValida(ubicaciones[i]))
                {
                    MostrarError($"La ubicación del ejemplar {i + 1} contiene caracteres no permitidos");
                    return false;
                }

                if (string.IsNullOrEmpty(bibliotecas[i]))
                {
                    MostrarError($"Debe seleccionar una biblioteca para el ejemplar {i + 1}");
                    return false;
                }
            }

            return true;
        }

        // ===== MÉTODOS AUXILIARES PARA MOSTRAR ERRORES =====
        private void MostrarErrorCampo(WebControl control, Label labelError, string mensaje)
        {
            labelError.Text = mensaje;
            labelError.Style["display"] = "block";

            if (control is TextBox)
                control.CssClass = "form-control is-invalid";
            else if (control is DropDownList)
                control.CssClass = "form-select is-invalid";
        }

        private void LimpiarTodosLosErrores()
        {
            // Limpiar errores de campos principales
            LimpiarErrorCampo(txtTitulo, lblErrorTitulo);
            LimpiarErrorCampo(ddlTipoMaterial, lblErrorTipoMaterial);
            LimpiarErrorCampo(txtAnho, lblErrorAnho);
            LimpiarErrorCampo(txtPaginas, lblErrorPaginas);
            LimpiarErrorCampo(TextTema, lblErrorTema);
            LimpiarErrorCampo(TextIdioma, lblErrorIdioma);

            // Limpiar errores de libro
            LimpiarErrorCampo(txtISBN, lblErrorISBN);
            LimpiarErrorCampo(txtEdicion, lblErrorEdicion);

            // Limpiar errores de tesis
            LimpiarErrorCampo(txtEspecialidad, lblErrorEspecialidad);
            LimpiarErrorCampo(txtAsesor, lblErrorAsesor);
            LimpiarErrorCampo(txtGrado, lblErrorGrado);
            LimpiarErrorCampo(txtInstitucion, lblErrorInstitucion);

            // Limpiar errores de artículo
            LimpiarErrorCampo(txtISSN, lblErrorISSN);
            LimpiarErrorCampo(txtRevista, lblErrorRevista);
            LimpiarErrorCampo(txtVolumen, lblErrorVolumen);
            LimpiarErrorCampo(txtNumero, lblErrorNumero);
        }

        private void LimpiarErrorCampo(WebControl control, Label labelError)
        {
            labelError.Text = "";
            labelError.Style["display"] = "none";

            if (control is TextBox)
                control.CssClass = "form-control";
            else if (control is DropDownList)
                control.CssClass = "form-select";
        }

        // ===== MÉTODOS DE VALIDACIÓN CON EXPRESIONES REGULARES =====
        private bool EsTituloValido(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return false;
            var regex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑüÜ\s\-\.,;:¿?¡!()""]+$");
            return regex.IsMatch(texto);
        }

        private bool EsNombreValido(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return false;
            var regex = new System.Text.RegularExpressions.Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$");
            return regex.IsMatch(texto);
        }

        private bool EsIdiomaValido(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return false;
            var regex = new System.Text.RegularExpressions.Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$");
            return regex.IsMatch(texto);
        }

        private bool EsCampoGeneralValido(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return false;
            var regex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑüÜ\s\-\.,;:¿?¡!()""]+$");
            return regex.IsMatch(texto);
        }

        private bool EsUbicacionValida(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return false;
            var regex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑüÜ\s\-\.,;:¿?¡!()""]+$");
            return regex.IsMatch(texto);
        }

        private bool EsISBNValido(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return false;
            var regex = new System.Text.RegularExpressions.Regex(@"^(?:\d[\-\s]?){9}[\dX]$|^(?:\d[\-\s]?){12}[\d]$");
            return regex.IsMatch(texto);
        }

        private bool EsISSNValido(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return false;
            var regex = new System.Text.RegularExpressions.Regex(@"^\d{4}\-\d{3}[\dX]$");
            return regex.IsMatch(texto);
        }

        // ===== MÉTODOS EXISTENTES (MANTENIENDO TU LÓGICA ORIGINAL) =====

        private void ManejarContribuyentesEnEdicion(int idMaterial)
        {
            try
            {
                ConstribuyenteWSClient contribuyenteBO = new ConstribuyenteWSClient();

                var idsContribuyentes = Request.Form.GetValues("id_contribuyente[]");
                var tipoContribuyenteList = Request.Form.GetValues("autor[]");
                var nombreList = Request.Form.GetValues("nombre[]");
                var primerApellidoList = Request.Form.GetValues("primer_apellido[]");
                var segundoApellidoList = Request.Form.GetValues("segundo_apellido[]");
                var seudonimoList = Request.Form.GetValues("seudonimo[]");

                List<int> idsContribuyentesEnFormulario = new List<int>();

                // === MODIFICACIÓN: Permitir formulario sin contribuyentes ===
                if (tipoContribuyenteList != null && nombreList != null && primerApellidoList != null)
                {
                    int minLength = Math.Min(tipoContribuyenteList.Length,
                                            Math.Min(nombreList.Length, primerApellidoList.Length));

                    for (int i = 0; i < minLength; i++)
                    {
                        // === MODIFICACIÓN: Solo procesar contribuyentes con datos completos ===
                        bool tieneDatosCompletos = !string.IsNullOrEmpty(tipoContribuyenteList[i]) &&
                                                  !string.IsNullOrEmpty(nombreList[i]) &&
                                                  !string.IsNullOrEmpty(primerApellidoList[i]);

                        if (tieneDatosCompletos)
                        {
                            int idContribuyente = 0;
                            if (idsContribuyentes != null && i < idsContribuyentes.Length)
                            {
                                int.TryParse(idsContribuyentes[i], out idContribuyente);
                            }

                            try
                            {
                                var tipoContribuyenteStr = tipoContribuyenteList[i];
                                tipoContribuyente tipoContribuyente;

                                if (Enum.IsDefined(typeof(tipoContribuyente), tipoContribuyenteStr))
                                {
                                    tipoContribuyente = (tipoContribuyente)Enum.Parse(typeof(tipoContribuyente), tipoContribuyenteStr, true);

                                    if (idContribuyente > 0)
                                    {
                                        

                                        idsContribuyentesEnFormulario.Add(idContribuyente);

                                        contribuyente contribuyenteExistente = contribuyenteBO.obtenerContribuyentePorId(idContribuyente);
                                        if (contribuyenteExistente != null)
                                        {
                                            contribuyenteExistente.nombre = nombreList[i];
                                            contribuyenteExistente.primer_apellido = primerApellidoList[i];
                                            contribuyenteExistente.segundo_apellido = i < segundoApellidoList?.Length ? segundoApellidoList[i] : "";
                                            contribuyenteExistente.seudonimo = i < seudonimoList?.Length ? seudonimoList[i] : null;
                                            contribuyenteExistente.tipo_contribuyente = tipoContribuyente;

                                            contribuyenteBO.modificarContribuyente(contribuyenteExistente);
                                            //System.Diagnostics.Debug.WriteLine($"Lo q actualice pe :v : {contribuyenteExistente.nombre} {contribuyenteExistente.primer_apellido}");
                                        }
                                    }
                                    else
                                    {
                                        var nuevoContribuyente = new contribuyente
                                        {
                                            nombre = nombreList[i],
                                            primer_apellido = primerApellidoList[i],
                                            segundo_apellido = i < segundoApellidoList?.Length ? segundoApellidoList[i] : "",
                                            seudonimo = i < seudonimoList?.Length ? seudonimoList[i] : null,
                                            tipo_contribuyente = tipoContribuyente
                                        };

                                        int nuevoId = contribuyenteBO.insertarContribuyente(nuevoContribuyente.nombre, nuevoContribuyente.primer_apellido
                                            , nuevoContribuyente.segundo_apellido, nuevoContribuyente.seudonimo, nuevoContribuyente.tipo_contribuyente.ToString());

                                        contribuyenteBO.asignarContribuyente(idMaterial, nuevoId);

                                        idsContribuyentesEnFormulario.Add(nuevoId);
                                        //System.Diagnostics.Debug.WriteLine($"contribuyente nuevo p :v : {nuevoContribuyente.nombre} -> ID: {nuevoId}");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($" Error procesando contribuyente {i}: {ex.Message}");
                            }
                        }
                    }
                }

                // === MODIFICACIÓN: Eliminar contribuyentes removidos (incluso si se removieron todos) ===
                EliminarContribuyentesRemovidos(idMaterial, idsContribuyentesEnFormulario);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al manejar contribuyentes en edición: {ex.Message}");
            }
        }

        // NUEVO MÉTODO PARA ELIMINAR CONTRIBUYENTES QUE FUERON REMOVIDOS DEL FORMULARIO
        private void EliminarContribuyentesRemovidos(int idMaterial, List<int> idsContribuyentesEnFormulario)
        {
            try
            {
                ConstribuyenteWSClient contribuyenteBO = new ConstribuyenteWSClient();

                var contribuyentesActuales = contribuyenteBO.listar_contribuyentes_por_material(idMaterial);

                foreach (var contribuyente in contribuyentesActuales)
                {
                    if (!idsContribuyentesEnFormulario.Contains(contribuyente.idContribuyente))
                    {
                        

                        if (contribuyenteBO.tiene_otras_relaciones(contribuyente.idContribuyente, idMaterial))
                        {
                            contribuyenteBO.eliminar_relacion_material_contribuyente(idMaterial, contribuyente.idContribuyente);
                            
                        }
                        else
                        {
                            contribuyenteBO.eliminarContribuyente(contribuyente.idContribuyente);
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar contribuyentes removidos: {ex.Message}");
            }
        }

        // MÉTODO SIMPLIFICADO PARA ACTUALIZAR EJEMPLARES
        private void ActualizarEjemplaresExistentes(int idMaterial)
        {
            try
            {
                EjemplarWSClient ejemplarBO = new EjemplarWSClient();

                var idsEjemplares = Request.Form.GetValues("id_ejemplar[]");
                var estadosList = Request.Form.GetValues("estado_ejemplar[]");
                var bibliotecasList = Request.Form.GetValues("biblioteca[]");
                var ubicacionesList = Request.Form.GetValues("ubicacion[]");

                System.Diagnostics.Debug.WriteLine($"=== ACTUALIZAR EJEMPLARES EXISTENTES  que funcione :(( (no he dormido 3 dias)===");

                List<int> idsEjemplaresEnFormulario = new List<int>();

                if (estadosList != null && bibliotecasList != null && ubicacionesList != null)
                {
                    int minLength = Math.Min(estadosList.Length, Math.Min(bibliotecasList.Length, ubicacionesList.Length));

                    for (int i = 0; i < minLength; i++)
                    {
                        int idEjemplar = 0;
                        if (idsEjemplares != null && i < idsEjemplares.Length)
                        {
                            int.TryParse(idsEjemplares[i], out idEjemplar);
                        }

                        if (!string.IsNullOrEmpty(estadosList[i]) &&
                            !string.IsNullOrEmpty(bibliotecasList[i]) &&
                            !string.IsNullOrEmpty(ubicacionesList[i]))
                        {
                            try
                            {
                                string estado = estadosList[i];
                                int idBiblioteca = int.Parse(bibliotecasList[i]);
                                string ubicacion = ubicacionesList[i];

                                if (idEjemplar > 0)
                                {
                                    //System.Diagnostics.Debug.WriteLine($"Actualizando el ejemplar ID: {idEjemplar}, Estado: {estado}");

                                    idsEjemplaresEnFormulario.Add(idEjemplar);

                                    ejemplar ejemplarExistente = ejemplarBO.obtenerEjemplarPorId(idEjemplar);
                                    if (ejemplarExistente != null)
                                    {
                                        if (ejemplarExistente.estado.ToString() != "PRESTADO")
                                        {
                                            if (Enum.TryParse<estadoEjemplar>(estado, out estadoEjemplar estadoEnum))
                                            {
                                                ejemplarExistente.estado = estadoEnum;
                                            }
                                        }

                                        ejemplarExistente.ubicacion = ubicacion;
                                        biblioteca bib = new biblioteca();
                                        bib.idBiblioteca = idBiblioteca;
                                        ejemplarExistente.blibioteca = bib;

                                        ejemplarBO.modificarEjemplar(ejemplarExistente);
                                        //System.Diagnostics.Debug.WriteLine($" EJEMPLAR actualizado o.o : ID {idEjemplar}");
                                    }
                                }
                                else
                                {
                                    System.Diagnostics.Debug.WriteLine($"➕ NUEVO ejemplar");

                                    var nuevoEjemplar = new ejemplar
                                    {
                                        ubicacion = ubicacion,
                                        blibioteca = new biblioteca { idBiblioteca = idBiblioteca },
                                        id_material = idMaterial,
                                        estado = estadoEjemplar.DISPONIBLE
                                    };

                                    int nuevoId = ejemplarBO.insertarEjemplar(nuevoEjemplar);

                                    idsEjemplaresEnFormulario.Add(nuevoId);

                                   // System.Diagnostics.Debug.WriteLine($" NUEVO EJEMPLAR xd : ID {nuevoId}");
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($" Error procesando ejemplar {i}: {ex.Message}");
                            }
                        }
                    }

                    if (idsEjemplaresEnFormulario.Count > 0)
                    {
                        EliminarEjemplaresRemovidos(idMaterial, idsEjemplaresEnFormulario);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar ejemplares: {ex.Message}");
            }
        }

        // MÉTODO CORREGIDO PARA ELIMINAR EJEMPLARES REMOVIDOS
        private void EliminarEjemplaresRemovidos(int idMaterial, List<int> idsEjemplaresEnFormulario)
        {
            try
            {
                EjemplarWSClient ejemplarBO = new EjemplarWSClient();

                var ejemplaresActuales = ejemplarBO.listar_por_material(idMaterial);

                foreach (var ejemplar in ejemplaresActuales)
                {
                    if (!idsEjemplaresEnFormulario.Contains(ejemplar.idEjemplar))
                    {
                        //System.Diagnostics.Debug.WriteLine($"VERIFICANDO ejemplar para eliminar: ID {ejemplar.idEjemplar}, Estado en la base datos xd: {ejemplar.estado}");

                        if (ejemplar.estado.ToString() == "DISPONIBLE")
                        {
                            ejemplarBO.eliminarEjemplar(ejemplar.idEjemplar);
                            //System.Diagnostics.Debug.WriteLine($" Ejemplar ELIMINADO: ID {ejemplar.idEjemplar}");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"Ejemplar NO eliminado (no estaba DISPONIBLE en BD) no seas loco p : ID {ejemplar.idEjemplar}, Estado en BD: {ejemplar.estado}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar ejemplares removidos: {ex.Message}");
            }
        }

        private void ActualizarMaterialExistente(int idMaterial)
        {
            string tipoMaterial = ddlTipoMaterial.SelectedValue;

            if (tipoMaterial == "Libro")
            {
                ActualizarLibro(idMaterial);
            }
            else if (tipoMaterial == "Tesis")
            {
                ActualizarTesis(idMaterial);
            }
            else if (tipoMaterial == "Articulo")
            {
                ActualizarArticulo(idMaterial);
            }
        }

        private void ActualizarLibro(int idMaterial)
        {
            try
            {
                LibroWSClient libroBO = new LibroWSClient();
                libro libro = libroBO.obtenerLibroPorId(idMaterial);

                if (libro != null)
                {
                    libro.titulo = txtTitulo.Text;
                    libro.anho_publicacion = int.Parse(txtAnho.Text);
                    libro.numero_paginas = int.Parse(txtPaginas.Text);
                    libro.clasificacion_tematica = TextTema.Text;
                    libro.idioma = TextIdioma.Text;
                    libro.ISBN = txtISBN.Text; 
                    libro.edicion = txtEdicion.Text;
                    libro.editoriales = TextEditorial.Text;
                    libroBO.modificarLibro(libro);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar libro: {ex.Message}");
            }
        }

        private void ActualizarTesis(int idMaterial)
        {
            try
            {
                TesisWSClient tesisBO = new TesisWSClient();
                tesis tesis = tesisBO.obtenerTesisPorId(idMaterial);

                if (tesis != null)
                {
                    tesis.titulo = txtTitulo.Text;
                    tesis.anho_publicacion = int.Parse(txtAnho.Text);
                    tesis.numero_paginas = int.Parse(txtPaginas.Text);
                    tesis.clasificacion_tematica = TextTema.Text;
                    tesis.idioma = TextIdioma.Text;
                    tesis.especialidad = txtEspecialidad.Text;
                    tesis.asesor = txtAsesor.Text;
                    tesis.grado = txtGrado.Text;
                    tesis.institucionPublicacion = txtInstitucion.Text;
                    tesis.editoriales = TextEditorial.Text;
                    tesisBO.modificarTesis(tesis);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar tesis: {ex.Message}");
            }
        }

        private void ActualizarArticulo(int idMaterial)
        {
            try
            {
                ArticuloWSClient articuloBO = new ArticuloWSClient();
                articulo articulo = articuloBO.obtenerArticuloPorId(idMaterial);

                if (articulo != null)
                {
                    articulo.titulo = txtTitulo.Text;
                    articulo.anho_publicacion = int.Parse(txtAnho.Text);
                    articulo.numero_paginas = int.Parse(txtPaginas.Text);
                    articulo.clasificacion_tematica = TextTema.Text;
                    articulo.idioma = TextIdioma.Text;
                    articulo.ISSN = txtISSN.Text; 
                    articulo.revista = txtRevista.Text;
                    articulo.volumen = int.Parse(txtVolumen.Text);
                    articulo.numero = int.Parse(txtNumero.Text);
                    articulo.editoriales = TextEditorial.Text;
                    articuloBO.modificarArticulo(articulo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar artículo: {ex.Message}");
            }
        }

        private int GuardarMaterialPorTipo()
        {
            string tipoMaterial = ddlTipoMaterial.SelectedValue;
            int idMaterial = 0;

            if (tipoMaterial == "Libro")
            {
                idMaterial = GuardarLibro();
            }
            else if (tipoMaterial == "Tesis")
            {
                idMaterial = GuardarTesis();
            }
            else if (tipoMaterial == "Articulo")
            {
                idMaterial = GuardarArticulo();
            }

            return idMaterial;
        }

        private int GuardarLibro()
        {
            try
            {
                libro libro = new libro();
                LibroWSClient libroDAO = new LibroWSClient();

                libro.titulo = txtTitulo.Text;
                libro.anho_publicacion = Convert.ToInt32(txtAnho.Text);
                libro.numero_paginas = Convert.ToInt32(txtPaginas.Text);
                libro.clasificacion_tematica = TextTema.Text;
                libro.idioma = TextIdioma.Text;
                libro.ISBN = txtISBN.Text; 
                libro.edicion = txtEdicion.Text;
                libro.editoriales = TextEditorial.Text;
                return libroDAO.insertarLibro(libro);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar libro: {ex.Message}");
            }
        }

        private int GuardarTesis()
        {
            try
            {
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
                tesis.editoriales = TextEditorial.Text;
                return tesisDAO.insertarTesis(tesis);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar tesis: {ex.Message}");
            }
        }

        private int GuardarArticulo()
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
                art.editoriales = TextEditorial.Text;
                return artiDAO.insertarArticulo(art);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar artículo: {ex.Message}");
            }
        }

        private void GuardarContribuyentes(int idMaterial)
        {
            List<contribuyente> contribuyentes = new List<contribuyente>();

            var tipoContribuyenteList = Request.Form.GetValues("autor[]");
            var nombreList = Request.Form.GetValues("nombre[]");
            var primerApellidoList = Request.Form.GetValues("primer_apellido[]");
            var segundoApellidoList = Request.Form.GetValues("segundo_apellido[]");
            var seudonimoList = Request.Form.GetValues("seudonimo[]");

            System.Diagnostics.Debug.WriteLine($"=== DEBUG CONTRIBUYENTES FORMULARIO (MODO NUEVO) ===");
            System.Diagnostics.Debug.WriteLine($"Arrays: autor[]={tipoContribuyenteList?.Length}, nombre[]={nombreList?.Length}, primer_apellido[]={primerApellidoList?.Length}");

            // === MODIFICACIÓN: Verificar si hay contribuyentes antes de procesar ===
            if (tipoContribuyenteList != null && nombreList != null && primerApellidoList != null)
            {
                int minLength = Math.Min(tipoContribuyenteList.Length,
                                        Math.Min(nombreList.Length, primerApellidoList.Length));

                for (int i = 0; i < minLength; i++)
                {
                    // === MODIFICACIÓN: Solo procesar contribuyentes con datos completos ===
                    bool tieneDatosCompletos = !string.IsNullOrEmpty(tipoContribuyenteList[i]) &&
                                              !string.IsNullOrEmpty(nombreList[i]) &&
                                              !string.IsNullOrEmpty(primerApellidoList[i]) &&
                                              !string.IsNullOrEmpty(segundoApellidoList?[i] ?? "");
                    if (tieneDatosCompletos)
                    {
                        try
                        {
                            var tipoContribuyenteStr = tipoContribuyenteList[i];

                            if (Enum.IsDefined(typeof(tipoContribuyente), tipoContribuyenteStr))
                            {
                                var tipoContribuyente = (tipoContribuyente)Enum.Parse(typeof(tipoContribuyente), tipoContribuyenteStr, true);

                                var contribuyente = new contribuyente
                                {
                                    nombre = nombreList[i],
                                    primer_apellido = primerApellidoList[i],
                                    segundo_apellido = i < segundoApellidoList?.Length ? segundoApellidoList[i] : "",
                                    seudonimo = i < seudonimoList?.Length ? seudonimoList[i] : null,
                                    tipo_contribuyente = tipoContribuyente
                                };

                                contribuyentes.Add(contribuyente);
                            }
                        }
                        catch (ArgumentException ex)
                        {
                            System.Diagnostics.Debug.WriteLine($" Error de conversión: {ex.Message}");
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($" FILTRADO: Posición {i} - Campos incompletos");
                    }
                }

                // === MODIFICACIÓN: Solo guardar si hay contribuyentes válidos ===
                if (contribuyentes.Count > 0)
                {
                    ConstribuyenteWSClient bocontribuyente = new ConstribuyenteWSClient();
                    foreach (var contribuyente in contribuyentes)
                    {
                        try
                        {
                            int idcon = bocontribuyente.insertarContribuyente(contribuyente.nombre, contribuyente.primer_apellido
                                                , contribuyente.segundo_apellido, contribuyente.seudonimo, contribuyente.tipo_contribuyente.ToString());
                            bocontribuyente.asignarContribuyente(idMaterial, idcon);
                            System.Diagnostics.Debug.WriteLine($" GUARDADO EN  LA BD: {contribuyente.nombre} -> ID: {idcon}");
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($" ERROR EN LA BD: {contribuyente.nombre} - {ex.Message}");
                        }
                    }

                    System.Diagnostics.Debug.WriteLine($" {contribuyentes.Count} contribuyentes guardados ===");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"0 contribuyentes guardados (opcional) ===");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($" No hay contribuyentes en el formulario ===");
            }
        }

        // MÉTODO PARA GUARDAR EJEMPLARES (funciona para nuevo y edición)
        private void GuardarEjemplares(int idMaterial)
        {
            string[] bibliotecas = Request.Form.GetValues("biblioteca[]");
            string[] ubicaciones = Request.Form.GetValues("ubicacion[]");

            System.Diagnostics.Debug.WriteLine($"=== GUARDAR EJEMPLARES NUEVOS ===");

            if (bibliotecas == null || ubicaciones == null || bibliotecas.Length == 0)
            {
                throw new Exception("Debe registrar al menos un ejemplar.");
            }

            int minLength = Math.Min(bibliotecas.Length, ubicaciones.Length);

            EjemplarWSClient boejemplar = new EjemplarWSClient();
            int ejemplaresGuardados = 0;

            for (int i = 0; i < minLength; i++)
            {
                try
                {
                    string idBiblioteca = bibliotecas[i];
                    string ubicacion = ubicaciones[i]?.Trim();

                    if (string.IsNullOrEmpty(idBiblioteca) || string.IsNullOrEmpty(ubicacion))
                    {
                        continue;
                    }

                    ejemplar ejemplar = new ejemplar();
                    ejemplar.ubicacion = ubicacion;
                    biblioteca bib = new biblioteca();
                    bib.idBiblioteca = Convert.ToInt32(idBiblioteca);
                    ejemplar.blibioteca = bib;
                    ejemplar.id_material = idMaterial;
                    ejemplar.estado = estadoEjemplar.DISPONIBLE;

                    int idEjemplar = boejemplar.insertarEjemplar(ejemplar);

                    if (idEjemplar > 0)
                    {
                        ejemplaresGuardados++;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"💥 ERROR ejemplar {i}: {ex.Message}");
                }
            }

            if (ejemplaresGuardados == 0)
            {
                throw new Exception("No se pudo guardar ningún ejemplar.");
            }
        }

        public string GetBibliotecasOptions()
        {
            try
            {
                BibliotecaWSClient bobiblioteca = new BibliotecaWSClient();
                var bibliotecas = bobiblioteca.ListarTodas();
                if (bibliotecas == null || bibliotecas.Any())
                    return "<option value=''>No hay bibliotecas</option>";

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

        private void MostrarError(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = "alert alert-danger";
        }

        private void MostrarExito(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = "alert alert-success";
        }
    }
}