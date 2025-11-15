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
                BindingList<ejemplar> ejemplares = new BindingList<ejemplar>(ejemplarBO.listar_disponibles_por_material(idMaterial));

                if (ejemplares != null && ejemplares.Count > 0)
                {
                    var ejemplaresParaJson = ejemplares.Select(e => new
                    {
                        CodigoEjemplar = e.idEjemplar,
                        BibliotecaId = e.blibioteca.idBiblioteca,
                        Ubicacion = e.ubicacion,
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
                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string contribuyentesJson = serializer.Serialize(contribuyentes);

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

        protected void GuardarMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarDatos())
                    return;

                bool esEdicion = !string.IsNullOrEmpty(hfIdMaterial.Value);
                int idMaterial;

                if (esEdicion)
                {
                    // MODO EDICIÓN - ESTRATEGIA SIMPLE
                    idMaterial = int.Parse(hfIdMaterial.Value);

                    // 1. Actualizar material principal
                    ActualizarMaterialExistente(idMaterial);

                    // 2. Contribuyentes (complejo - mantener IDs)
                    ManejarContribuyentesEnEdicion(idMaterial);

                    // 3. Ejemplares (simple - eliminar todos + crear nuevos)
                    EliminarEjemplaresAntiguos(idMaterial);
                    GuardarEjemplares(idMaterial);

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

        // MÉTODO ACTUALIZADO PARA MANEJAR CONTRIBUYENTES EN EDICIÓN
        private void ManejarContribuyentesEnEdicion(int idMaterial)
        {
            try
            {
                ConstribuyenteWSClient contribuyenteBO = new ConstribuyenteWSClient();

                // Obtener arrays del formulario
                var idsContribuyentes = Request.Form.GetValues("id_contribuyente[]");
                var tipoContribuyenteList = Request.Form.GetValues("autor[]");
                var nombreList = Request.Form.GetValues("nombre[]");
                var primerApellidoList = Request.Form.GetValues("primer_apellido[]");
                var segundoApellidoList = Request.Form.GetValues("segundo_apellido[]");
                var seudonimoList = Request.Form.GetValues("seudonimo[]");

                System.Diagnostics.Debug.WriteLine($"=== MODO EDICIÓN CONTRIBUYENTES ===");
                System.Diagnostics.Debug.WriteLine($"IDs recibidos: {idsContribuyentes?.Length}");

                // Lista para guardar los IDs de contribuyentes que SÍ están en el formulario
                List<int> idsContribuyentesEnFormulario = new List<int>();

                if (tipoContribuyenteList != null && nombreList != null && primerApellidoList != null)
                {
                    int minLength = Math.Min(tipoContribuyenteList.Length,
                                            Math.Min(nombreList.Length, primerApellidoList.Length));

                    for (int i = 0; i < minLength; i++)
                    {
                        // Obtener ID del contribuyente (0 si es nuevo)
                        int idContribuyente = 0;
                        if (idsContribuyentes != null && i < idsContribuyentes.Length)
                        {
                            int.TryParse(idsContribuyentes[i], out idContribuyente);
                        }

                        // Validar campos requeridos
                        if (!string.IsNullOrEmpty(tipoContribuyenteList[i]) &&
                            !string.IsNullOrEmpty(nombreList[i]) &&
                            !string.IsNullOrEmpty(primerApellidoList[i]))
                        {
                            try
                            {
                                var tipoContribuyenteStr = tipoContribuyenteList[i];
                                tipoContribuyente tipoContribuyente;

                                if (Enum.IsDefined(typeof(tipoContribuyente), tipoContribuyenteStr))
                                {
                                    tipoContribuyente = (tipoContribuyente)Enum.Parse(typeof(tipoContribuyente), tipoContribuyenteStr, true);

                                    if (idContribuyente > 0)
                                    {
                                        // UPDATE - Contribuyente existente
                                        System.Diagnostics.Debug.WriteLine($"🔄 ACTUALIZANDO contribuyente ID: {idContribuyente}");

                                        // Agregar a la lista de contribuyentes que SÍ están en el formulario
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
                                            System.Diagnostics.Debug.WriteLine($"✅ ACTUALIZADO: {contribuyenteExistente.nombre} {contribuyenteExistente.primer_apellido}");
                                        }
                                    }
                                    else
                                    {
                                        // INSERT - Nuevo contribuyente
                                        System.Diagnostics.Debug.WriteLine($"➕ NUEVO contribuyente");

                                        var nuevoContribuyente = new contribuyente
                                        {
                                            nombre = nombreList[i],
                                            primer_apellido = primerApellidoList[i],
                                            segundo_apellido = i < segundoApellidoList?.Length ? segundoApellidoList[i] : "",
                                            seudonimo = i < seudonimoList?.Length ? seudonimoList[i] : null,
                                            tipo_contribuyente = tipoContribuyente
                                        };

                                        int nuevoId = contribuyenteBO.insertarContribuyente(nuevoContribuyente.nombre,nuevoContribuyente.primer_apellido
                                            ,nuevoContribuyente.segundo_apellido,nuevoContribuyente.seudonimo,nuevoContribuyente.tipo_contribuyente.ToString());

                                        contribuyenteBO.asignarContribuyente(idMaterial, nuevoId);

                                        // Agregar el nuevo ID a la lista
                                        idsContribuyentesEnFormulario.Add(nuevoId);

                                        System.Diagnostics.Debug.WriteLine($"✅ NUEVO GUARDADO: {nuevoContribuyente.nombre} -> ID: {nuevoId}");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"❌ Error procesando contribuyente {i}: {ex.Message}");
                            }
                        }
                    }

                    // PASO FINAL: Eliminar contribuyentes que fueron removidos del formulario
                    if (idsContribuyentesEnFormulario.Count > 0)
                    {
                        EliminarContribuyentesRemovidos(idMaterial, idsContribuyentesEnFormulario);
                    }
                }
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

                // Obtener todos los contribuyentes actuales del material
                var contribuyentesActuales = contribuyenteBO.listar_contribuyentes_por_material(idMaterial);

                foreach (var contribuyente in contribuyentesActuales)
                {
                    // Si el contribuyente actual NO está en la lista del formulario, significa que fue eliminado
                    if (!idsContribuyentesEnFormulario.Contains(contribuyente.idContribuyente))
                    {
                        System.Diagnostics.Debug.WriteLine($"🗑️ ELIMINANDO contribuyente removido: {contribuyente.nombre} (ID: {contribuyente.idContribuyente})");

                        // Verificar si el contribuyente está siendo usado por otros materiales
                        if (contribuyenteBO.tiene_otras_relaciones(contribuyente.idContribuyente, idMaterial))
                        {
                            // Solo eliminar la relación (contribuyente pertenece a otros materiales)
                            contribuyenteBO.eliminar_relacion_material_contribuyente(idMaterial, contribuyente.idContribuyente);
                            System.Diagnostics.Debug.WriteLine($"➖ Solo se eliminó la relación del contribuyente ID: {contribuyente.idContribuyente}");
                        }
                        else
                        {
                            // Eliminar contribuyente completo (solo pertenece a este material)
                            contribuyenteBO.eliminarContribuyente(contribuyente.idContribuyente);
                            System.Diagnostics.Debug.WriteLine($"✅ Contribuyente eliminado completamente: {contribuyente.nombre}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar contribuyentes removidos: {ex.Message}");
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
                    // Actualizar propiedades
                    libro.titulo = txtTitulo.Text;
                    libro.anho_publicacion = int.Parse(txtAnho.Text);
                    libro.numero_paginas = int.Parse(txtPaginas.Text);
                    libro.clasificacion_tematica = TextTema.Text;
                    libro.idioma = TextIdioma.Text;
                    libro.ISBN = txtISBN.Text;
                    libro.edicion = txtEdicion.Text;

                    // Ejecutar actualización
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
                    // Actualizar propiedades
                    tesis.titulo = txtTitulo.Text;
                    tesis.anho_publicacion = int.Parse(txtAnho.Text);
                    tesis.numero_paginas = int.Parse(txtPaginas.Text);
                    tesis.clasificacion_tematica = TextTema.Text;
                    tesis.idioma = TextIdioma.Text;
                    tesis.especialidad = txtEspecialidad.Text;
                    tesis.asesor = txtAsesor.Text;
                    tesis.grado = txtGrado.Text;
                    tesis.institucionPublicacion = txtInstitucion.Text;

                    // Ejecutar actualización
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
                    // Actualizar propiedades
                    articulo.titulo = txtTitulo.Text;
                    articulo.anho_publicacion = int.Parse(txtAnho.Text);
                    articulo.numero_paginas = int.Parse(txtPaginas.Text);
                    articulo.clasificacion_tematica = TextTema.Text;
                    articulo.idioma = TextIdioma.Text;
                    articulo.ISSN = txtISSN.Text;
                    articulo.revista = txtRevista.Text;
                    articulo.volumen = int.Parse(txtVolumen.Text);
                    articulo.numero = int.Parse(txtNumero.Text);

                    // Ejecutar actualización
                    articuloBO.modificarArticulo(articulo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar artículo: {ex.Message}");
            }
        }

        // MÉTODO PARA ELIMINAR EJEMPLARES ANTIGUOS
        private void EliminarEjemplaresAntiguos(int idMaterial)
        {
            try
            {
                EjemplarWSClient ejemplarBO = new EjemplarWSClient();
                var ejemplaresAntiguos = new BindingList<ejemplar>(ejemplarBO.listar_disponibles_por_material(idMaterial));

                //xd
                if (ejemplaresAntiguos == null) return;

                foreach (var ejemplar in ejemplaresAntiguos)
                {
                    ejemplarBO.eliminarEjemplar(ejemplar.idEjemplar);
                }

                System.Diagnostics.Debug.WriteLine($"🗑️ Se eliminaron {ejemplaresAntiguos.Count} ejemplares antiguos");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar ejemplares antiguos: {ex.Message}");
            }
        }

        private bool ValidarDatos()
        {
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

            if (string.IsNullOrEmpty(ddlTipoMaterial.SelectedValue))
            {
                MostrarError("Debe seleccionar un tipo de material");
                return false;
            }

            string tipoMaterial = ddlTipoMaterial.SelectedValue;
            if (tipoMaterial == "Libro")
            {
                if (string.IsNullOrEmpty(txtISBN.Text))
                {
                    MostrarError("El ISBN es requerido para libros");
                    return false;
                }
            }
            else if (tipoMaterial == "Tesis")
            {
                if (string.IsNullOrEmpty(txtEspecialidad.Text))
                {
                    MostrarError("La especialidad es requerida para tesis");
                    return false;
                }
            }
            else if (tipoMaterial == "Articulo")
            {
                if (string.IsNullOrEmpty(txtISSN.Text))
                {
                    MostrarError("El ISSN es requerido para artículos");
                    return false;
                }
            }

            return true;
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

                return artiDAO.insertarArticulo(art);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar artículo: {ex.Message}");
            }
        }

        // MÉTODO ORIGINAL PARA NUEVOS CONTRIBUYENTES (solo se usa en modo NUEVO)
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

            if (tipoContribuyenteList != null && nombreList != null && primerApellidoList != null)
            {
                int minLength = Math.Min(tipoContribuyenteList.Length,
                                        Math.Min(nombreList.Length, primerApellidoList.Length));

                System.Diagnostics.Debug.WriteLine($"Usando longitud mínima: {minLength}");

                for (int i = 0; i < minLength; i++)
                {
                    System.Diagnostics.Debug.WriteLine($"Contribuyente {i}: autor='{tipoContribuyenteList[i]}', nombre='{nombreList[i]}'");

                    if (!string.IsNullOrEmpty(tipoContribuyenteList[i]) &&
                        !string.IsNullOrEmpty(nombreList[i]) &&
                        !string.IsNullOrEmpty(primerApellidoList[i]))
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
                                System.Diagnostics.Debug.WriteLine($"✅ AGREGADO: {contribuyente.nombre} {contribuyente.primer_apellido}");
                            }
                        }
                        catch (ArgumentException ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"❌ Error de conversión: {ex.Message}");
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"❌ FILTRADO: Posición {i} - Campos vacíos");
                    }
                }

                ConstribuyenteWSClient bocontribuyente = new ConstribuyenteWSClient();
                foreach (var contribuyente in contribuyentes)
                {
                    try
                    {
                        int idcon = bocontribuyente.insertarContribuyente(contribuyente.nombre, contribuyente.primer_apellido
                                            , contribuyente.segundo_apellido, contribuyente.seudonimo, contribuyente.tipo_contribuyente.ToString());
                        bocontribuyente.asignarContribuyente(idMaterial, idcon);
                        System.Diagnostics.Debug.WriteLine($"💾 GUARDADO EN BD: {contribuyente.nombre} -> ID: {idcon}");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"💥 ERROR BD: {contribuyente.nombre} - {ex.Message}");
                    }
                }

                System.Diagnostics.Debug.WriteLine($"=== RESUMEN: {contribuyentes.Count} contribuyentes guardados ===");
            }
        }

        // MÉTODO PARA GUARDAR EJEMPLARES (funciona para nuevo y edición)
        private void GuardarEjemplares(int idMaterial)
        {
            string[] bibliotecas = Request.Form.GetValues("biblioteca[]");
            string[] ubicaciones = Request.Form.GetValues("ubicacion[]");

            System.Diagnostics.Debug.WriteLine($"=== GUARDAR EJEMPLARES ===");
            System.Diagnostics.Debug.WriteLine($"biblioteca[]: {bibliotecas?.Length}, ubicacion[]: {ubicaciones?.Length}");

            if (bibliotecas == null || ubicaciones == null || bibliotecas.Length == 0)
            {
                throw new Exception("Debe registrar al menos un ejemplar.");
            }

            int minLength = Math.Min(bibliotecas.Length, ubicaciones.Length);

            System.Diagnostics.Debug.WriteLine($"Usando longitud mínima: {minLength}");

            EjemplarWSClient boejemplar = new EjemplarWSClient();
            int ejemplaresGuardados = 0;

            for (int i = 0; i < minLength; i++)
            {
                try
                {
                    string idBiblioteca = bibliotecas[i];
                    string ubicacion = ubicaciones[i]?.Trim();

                    // DEBUG: Ver qué datos llegan
                    System.Diagnostics.Debug.WriteLine($"Ejemplar {i}: Biblioteca='{idBiblioteca}', Ubicación='{ubicacion}'");

                    if (string.IsNullOrEmpty(idBiblioteca) || string.IsNullOrEmpty(ubicacion))
                    {
                        System.Diagnostics.Debug.WriteLine($"❌ Ejemplar {i} ignorado: datos incompletos");
                        continue;
                    }

                    ejemplar ejemplar = new ejemplar
                    {
                        id_material = idMaterial,
                        ubicacion = ubicacion,
                        estado = estadoEjemplar.DISPONIBLE
                    };
                    ejemplar.idEjemplar = Convert.ToInt32(idBiblioteca);

                    int idEjemplar = boejemplar.insertarEjemplar(ejemplar);
                    if (idEjemplar > 0)
                    {
                        ejemplaresGuardados++;
                        System.Diagnostics.Debug.WriteLine($"✅ EJEMPLAR GUARDADO: ID={idEjemplar}, Biblioteca={idBiblioteca}");
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

            System.Diagnostics.Debug.WriteLine($"🎯 TOTAL EJEMPLARES GUARDADOS: {ejemplaresGuardados}");
        }

        public string GetBibliotecasOptions()
        {
            try
            {
                BibliotecaWSClient bobiblioteca = new BibliotecaWSClient();
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