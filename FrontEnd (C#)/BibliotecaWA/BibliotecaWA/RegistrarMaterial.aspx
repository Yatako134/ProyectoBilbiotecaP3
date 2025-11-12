<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="RegistrarMaterial.aspx.cs" Inherits="BibliotecaWA.RegistrarMaterial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
    
    <link href="Fonts/css/Estilos2.css" rel="stylesheet" />
    <script>
        let contribuyenteCount = 1;
        let ejemplarCount = 0; // <- AGREGAR ESTO
        // Función para añadir un contribuyente dinámicamente
        function añadirContribuyente() {
            contribuyenteCount++;
            const newContribuyente = `
        <div class="row mb-3" id="contribuyente-${contribuyenteCount}">
            <div class="col-md-2">
                <select class="form-select" name="autor[]" required>
                    <option value="AUTOR">AUTOR</option>
                    <option value="TRADUCTOR">TRADUCTOR</option>
                    <option value="EDITOR">EDITOR</option>
                </select>
            </div>
            <div class="col-md-2">
                <input type="text" class="form-control" placeholder="Nombre" name="nombre[]" required>
            </div>
            <div class="col-md-2">
                <input type="text" class="form-control" placeholder="Primer Apellido" name="primer_apellido[]" required>
            </div>
            <div class="col-md-2">
                <input type="text" class="form-control" placeholder="Segundo Apellido" name="segundo_apellido[]" required>
            </div>
            <div class="col-md-3">
                <input type="text" class="form-control" placeholder="Seudónimo" name="seudonimo[]">
            </div>
            <div class="col-md-1">
                <button type="button" class="btn btn-danger" onclick="eliminarContribuyente(${contribuyenteCount})">
                    <i class="fas fa-trash-alt"></i>
                </button>
            </div>
        </div>
    `;
            document.getElementById("contribuyentes-container").insertAdjacentHTML('beforeend', newContribuyente);
        }

        // Función para eliminar un contribuyente dinámicamente
        function eliminarContribuyente(id) {
            const contribuyente = document.getElementById(`contribuyente-${id}`);
            if (contribuyente) {
                contribuyente.remove();
            }
        }



        // Función para mostrar los campos según el tipo de material seleccionado
        function mostrarCampos() {
            var tipoMaterial = document.getElementById("<%= ddlTipoMaterial.ClientID %>").value;

            // Ocultar todos los campos adicionales por defecto
            document.getElementById("isbnField").style.display = 'none';
            document.getElementById("edicionField").style.display = 'none';
            document.getElementById("especialidadField").style.display = 'none';
            document.getElementById("asesorField").style.display = 'none';
            document.getElementById("gradoField").style.display = 'none';
            document.getElementById("institucionField").style.display = 'none';
            document.getElementById("issnField").style.display = 'none';
            document.getElementById("revistaField").style.display = 'none';
            document.getElementById("volumenField").style.display = 'none';
            document.getElementById("numeroField").style.display = 'none';

            // Mostrar los campos correspondientes al tipo de material seleccionado
            if (tipoMaterial === 'Libro') {
                document.getElementById("isbnField").style.display = 'block';
                document.getElementById("edicionField").style.display = 'block';
            } else if (tipoMaterial === 'Tesis') {
                document.getElementById("especialidadField").style.display = 'block';
                document.getElementById("asesorField").style.display = 'block';
                document.getElementById("gradoField").style.display = 'block';
                document.getElementById("institucionField").style.display = 'block';
            } else if (tipoMaterial === 'Articulo') {
                document.getElementById("issnField").style.display = 'block';
                document.getElementById("revistaField").style.display = 'block';
                document.getElementById("volumenField").style.display = 'block';
                document.getElementById("numeroField").style.display = 'block';
            }
        }

        function validarCampos() {
            var tipoMaterial = document.getElementById("<%= ddlTipoMaterial.ClientID %>").value;
            var isValid = true;

            // Validar campos comunes
            if (!document.getElementById("<%= txtTitulo.ClientID %>").value) {
                alert("El título es requerido");
                isValid = false;
            }

            // Validar campos específicos según el tipo
            if (tipoMaterial === 'Libro') {
                if (!document.getElementById("<%= txtISBN.ClientID %>").value) {
                    alert("El ISBN es requerido para libros");
                    isValid = false;
                }
            } else if (tipoMaterial === 'Tesis') {
                if (!document.getElementById("<%= txtEspecialidad.ClientID %>").value) {
                    alert("La especialidad es requerida para tesis");
                    isValid = false;
                }
            } else if (tipoMaterial === 'Articulo') {
                if (!document.getElementById("<%= txtISSN.ClientID %>").value) {
                    alert("El ISSN es requerido para artículos");
                    isValid = false;
                }
            }

            return isValid;
        }
        ///todo del ejemplar xd
        // Versión modificada de añadirEjemplar para edición - CORREGIDA
        function añadirEjemplarExistente(ejemplar) {
            ejemplarCount++;
            const newEjemplar = `
        <div class="row mb-3" id="ejemplar-${ejemplarCount}">
            <div class="col-md-4">
                <label class="form-label">Código Ejemplar</label>
                <input type="text" class="form-control" disabled value="${ejemplar.CodigoEjemplar || ''}">
            </div>
            <div class="col-md-4">
                <label class="form-label">Biblioteca</label>
                <select class="form-select" name="biblioteca[]" required>
                    <option value="">Seleccione biblioteca</option>
                    <%= GetBibliotecasOptions() %>
                </select>
            </div>
            <div class="col-md-3">
                <label class="form-label">Ubicación</label>
                <input type="text" class="form-control" placeholder="Ingrese ubicación" name="ubicacion[]"
                    value="${ejemplar.Ubicacion || ''}" required>
            </div>
            <div class="col-md-1 d-flex align-items-end">
                <button type="button" class="btn btn-danger" onclick="eliminarEjemplar(${ejemplarCount})">
                    <i class="fas fa-trash-alt"></i>
                </button>
            </div>
        </div>
    `;
            document.getElementById("ejemplares-container").insertAdjacentHTML('beforeend', newEjemplar);
        }
 

        // Función para eliminar un ejemplar dinámicamente
        function eliminarEjemplar(id) {
            const ejemplar = document.getElementById(`ejemplar-${id}`);
            if (ejemplar) {
                ejemplar.remove();
            }
        }
        // Función ORIGINAL para añadir ejemplar (nuevo)
        function añadirEjemplar() {
            ejemplarCount++;
            const newEjemplar = `
        <div class="row mb-3" id="ejemplar-${ejemplarCount}">
            <div class="col-md-4">
                <label class="form-label">Código Ejemplar</label>
                <input type="text" class="form-control" disabled value=" ">
            </div>
            <div class="col-md-4">
                <label class="form-label">Biblioteca</label>
                <select class="form-select" name="biblioteca[]" id="ddlBiblioteca-${ejemplarCount}" required>
                    <option value="">Seleccione biblioteca</option>
                     <%= GetBibliotecasOptions() %>  
                </select>
            </div>
            <div class="col-md-3">
                <label class="form-label">Ubicación</label>
                <input type="text" class="form-control" placeholder="Ingrese ubicación" name="ubicacion[]" required>
            </div>
            <div class="col-md-1 d-flex align-items-end">
                <button type="button" class="btn btn-danger" onclick="eliminarEjemplar(${ejemplarCount})">
                    <i class="fas fa-trash-alt"></i>
                </button>
            </div>
        </div>
    `;
            document.getElementById("ejemplares-container").insertAdjacentHTML('beforeend', newEjemplar);
        }




        // Función para inicializar el formulario según el modo
        function inicializarFormulario() {
            console.log("=== INICIALIZANDO FORMULARIO ===");

            // Verificar si estamos en modo nuevo (sin ID en URL)
            const urlParams = new URLSearchParams(window.location.search);
            const tieneId = urlParams.has('id');

            console.log("Modo edición?:", tieneId);

            if (!tieneId) {
                // MODO NUEVO - inicializar con un ejemplar y contribuyente vacío
                console.log("MODO NUEVO - Inicializando elementos vacíos");
                añadirEjemplar();
                añadirContribuyente();
            } else {
                // MODO EDICIÓN - los datos se cargarán via ScriptManager
                console.log("MODO EDICIÓN - Esperando datos del servidor");
            }
        }

        // Ejecutar cuando el DOM esté listo
        document.addEventListener('DOMContentLoaded', inicializarFormulario);


        function validarFormulario() {
            // Validar que haya al menos un ejemplar
            const ejemplares = document.querySelectorAll('#ejemplares-container .row.mb-3');
            if (ejemplares.length === 0) {
                // Mostrar modal centrado en lugar de alert
                const modal = new bootstrap.Modal(document.getElementById('modalAdvertencia'));
                modal.show();
                return false;
            }

            // Validar que todos los ejemplares tengan biblioteca y ubicación
            const bibliotecas = document.querySelectorAll('select[name="biblioteca[]"]');
            const ubicaciones = document.querySelectorAll('input[name="ubicacion[]"]');

            for (let i = 0; i < bibliotecas.length; i++) {
                if (!bibliotecas[i].value) {
                    mostrarErrorEjemplar(`El ejemplar ${i + 1} debe tener una biblioteca seleccionada.`);
                    bibliotecas[i].focus();
                    return false;
                }

            }

            // Validar campos comunes del material
            if (!validarCampos()) {
                return false;
            }

            return true;
        }

        // Función para mostrar errores específicos de ejemplares en un modal
        function mostrarErrorEjemplar(mensaje) {
            // Crear modal dinámico para errores
            const modalHtml = `
        <div class="modal fade" id="modalErrorEjemplar" tabindex="-1">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header bg-danger text-white">
                        <h5 class="modal-title">
                            <i class="fas fa-exclamation-triangle me-2"></i>
                            Error en Ejemplar
                        </h5>
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body text-center">
                        <div class="mb-3">
                            <i class="fas fa-times-circle text-danger" style="font-size: 3rem;"></i>
                        </div>
                        <h5 class="mb-3">${mensaje}</h5>
                    </div>
                    <div class="modal-footer justify-content-center">
                        <button type="button" class="btn btn-primary" data-bs-dismiss="modal">
                            <i class="fas fa-check me-2"></i>Entendido
                        </button>
                    </div>
                </div>
            </div>
        </div>
    `;

            // Remover modal anterior si existe
            const modalExistente = document.getElementById('modalErrorEjemplar');
            if (modalExistente) {
                modalExistente.remove();
            }

            // Añadir nuevo modal al body
            document.body.insertAdjacentHTML('beforeend', modalHtml);

            // Mostrar modal
            const modal = new bootstrap.Modal(document.getElementById('modalErrorEjemplar'));
            modal.show();

            // Remover modal del DOM después de cerrarse
            document.getElementById('modalErrorEjemplar').addEventListener('hidden.bs.modal', function () {
                this.remove();
            });
        }

        // Función para cargar ejemplares existentes en edición
        function cargarEjemplaresExistente(ejemplares) {
            console.log("=== CARGANDO EJEMPLARES EXISTENTES ===");
            console.log("Datos recibidos:", ejemplares);

            const container = document.getElementById("ejemplares-container");
            if (container) {
                container.innerHTML = ''; // Limpiar los ejemplares existentes
                ejemplarCount = 0;

                // Recorrer los ejemplares y agregarlos
                if (ejemplares && Array.isArray(ejemplares) && ejemplares.length > 0) {
                    ejemplares.forEach(function (ejemplar, index) {
                        console.log(`Ejemplar ${index}:`, ejemplar);
                        añadirEjemplarExistente(ejemplar); // Usar esta función para agregar el ejemplar

                        // Obtener el DropDownList correspondiente al ejemplar actual
                        const ejemplarDropdown = document.getElementById(`ddlBiblioteca-${index + 1}`); // index + 1 porque ejemplarCount empieza desde 1

                        // Asignar el valor del ejemplar.BibliotecaId al DropDownList de la biblioteca
                        if (ejemplarDropdown) {
                            ejemplarDropdown.value = ejemplar.BibliotecaId; // Asignar el valor de BibliotecaId al DropDownList

                            // Verifica si el valor se ha asignado correctamente
                            console.log(`Ejemplar ${index + 1} - Biblioteca seleccionada: ${ejemplarDropdown.value}`);
                        }
                    });
                } else {
                    console.log("No hay ejemplares existentes, agregando uno vacío");
                    añadirEjemplar(); // Añadir uno vacío si no hay ejemplares
                }
            } else {
                console.error("No se encontró el contenedor de ejemplares");
            }
        }

        // Función para cargar contribuyentes existentes en edición  
        function cargarContribuyentesExistente(contribuyentes) {
            console.log("=== CARGANDO CONTRIBUYENTES EXISTENTES ===");
            console.log("Datos recibidos:", contribuyentes);

            const container = document.getElementById("contribuyentes-container");
            if (container) {
                container.innerHTML = ''; // Limpiar los contribuyentes existentes
                contribuyenteCount = 0;

                // Recorrer los contribuyentes y agregarlos
                if (contribuyentes && Array.isArray(contribuyentes) && contribuyentes.length > 0) {
                    contribuyentes.forEach(function (contribuyente, index) {
                        console.log(`Contribuyente ${index}:`, contribuyente);
                        añadirContribuyenteExistente(contribuyente); // Usar esta función para agregar el contribuyente

                        // Obtener el DropDownList correspondiente al contribuyente actual
                        const contribuyenteDropdown = document.getElementById(`ddlTipoContribuyente-${index + 1}`); // index + 1 porque contribuyenteCount empieza desde 1

                        // Asignar el valor al DropDownList basado en el valor de Tipo_contribuyente
                        if (contribuyenteDropdown) {
                            if (contribuyente.Tipo_contribuyente == 0) {
                                contribuyenteDropdown.value = "AUTOR";
                            }
                            if (contribuyente.Tipo_contribuyente == 1) {
                                contribuyenteDropdown.value = "TRADUCTOR";
                            }
                            if (contribuyente.Tipo_contribuyente == 2) {
                                contribuyenteDropdown.value = "EDITOR";
                            }
                        }
                    });
                } else {
                    console.log("No hay contribuyentes existentes, agregando uno vacío");
                    añadirContribuyente(); // Añadir uno vacío si no hay contribuyentes
                }
            } else {
                console.error("No se encontró el contenedor de contribuyentes");
            }
        }


        // En añadirContribuyenteExistente - VERIFICA LOS NOMBRES DE PROPIEDADES:
        function añadirContribuyenteExistente(contribuyente) {
            contribuyenteCount++;
            const newContribuyente = `
        <div class="row mb-3" id="contribuyente-${contribuyenteCount}">
            <div class="col-md-2">
                <select class="form-select" name="autor[]" id="ddlTipoContribuyente-${contribuyenteCount}" required>
                    <option value="AUTOR" ${contribuyente.Tipo_contribuyente === 'AUTOR' ? 'selected' : ''}>AUTOR</option>
                    <option value="TRADUCTOR" ${contribuyente.Tipo_contribuyente === 'TRADUCTOR' ? 'selected' : ''}>TRADUCTOR</option>
                    <option value="EDITOR" ${contribuyente.Tipo_contribuyente === 'EDITOR' ? 'selected' : ''}>EDITOR</option>
                </select>
            </div>
            <div class="col-md-2">
                <input type="text" class="form-control" placeholder="Nombre" name="nombre[]" value="${contribuyente.Nombre || ''}" required>
            </div>
            <div class="col-md-2">
                <input type="text" class="form-control" placeholder="Primer Apellido" name="primer_apellido[]" value="${contribuyente.Primer_apellido || ''}" required>
            </div>
            <div class="col-md-2">
                <input type="text" class="form-control" placeholder="Segundo Apellido" name="segundo_apellido[]" value="${contribuyente.Segundo_apellido || ''}" required>
            </div>
            <div class="col-md-3">
                <input type="text" class="form-control" placeholder="Seudónimo" name="seudonimo[]" value="${contribuyente.Seudonimo || ''}">
            </div>
            <div class="col-md-1">
                <button type="button" class="btn btn-danger" onclick="eliminarContribuyente(${contribuyenteCount})">
                    <i class="fas fa-trash-alt"></i>
                </button>
            </div>
        </div>
    `;
            document.getElementById("contribuyentes-container").insertAdjacentHTML('beforeend', newContribuyente);
        }

    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <!-- Modal para advertencia de ejemplares -->
    <div class="modal fade" id="modalAdvertencia" tabindex="-1" aria-labelledby="modalAdvertenciaLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-danger text-dark">
                    <h5 class="modal-title" id="modalAdvertenciaLabel">
                        <i class="fas fa-exclamation-triangle me-2"></i>
                        Advertencia
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body text-center">
                    <div class="mb-3">
                        <i class="fas fa-exclamation-circle text-danger" style="font-size: 3rem;"></i>
                    </div>
                    <h5 class="mb-3">Debe registrar al menos un ejemplar</h5>
                    <p class="text-muted">Para guardar el material bibliográfico, es necesario que registre al menos un ejemplar en el sistema.</p>
                </div>
                <div class="modal-footer justify-content-center">
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">
                        <i class="fas fa-check me-2"></i>Entendido
                    </button>
                </div>
            </div>
        </div>
    </div>


    <!-- AGREGAR ESTE HIDDEN FIELD -->
    <asp:HiddenField ID="hfIdMaterial" runat="server" />

    <div class="d-flex align-items-center mb-4">
        <i class="fas fa-th" style="margin-right: 10px; font-size: 24px;"></i>
        <div style="border-right: 2px solid #ccc; height: 24px; margin-right: 10px;"></div>
        <p1>Gestión de materiales</p1>
    </div>
    <hr />

    <div class="tabla-header d-flex justify-content-between align-items-center p-3">

        

        <h1><strong>Registro de nuevo material bibliográfico</strong></h1>
    </div>

    <div class="card" style="width: 100rem;">
        <div class="card-body">
            <!-- Fila de Código y Título -->
            <div class="row">
                <div class="col-md-2 mb-3">
                    <label for="codigo" class="form-label">Código</label>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-10 mb-3">
                    <label for="titulo" class="form-label">Título</label>
                    <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <!-- Mensaje de advertencia -->
            <div class="alert alert-danger" role="alert">
                <strong><span class="icon">⚠️</span></strong> El orden en el que se agreguen los contribuyentes será el orden que se registrará en el sistema.
            </div>

            <p1>Contribuyentes</p1>

            <!-- Contenedor para los contribuyentes -->
            <div id="contribuyentes-container">
                <!-- Los contribuyentes serán agregados aquí dinámicamente -->
            </div>

            <!-- Botón para añadir un contribuyente -->
            <div class="row mt-3">
                <div class="col-md-12">
                    <button type="button" class="btn pill-info w-100 py-2" onclick="añadirContribuyente()">
                        <i class="fa-solid fa-circle-plus me-2"></i>Añadir contribuyente
                    </button>
                </div>
            </div>
            
            <!-- Fila de TEMA -->
            <div class="row">
                <div class="col-md-12 mb-3">
                    <label for="tema" class="form-label">Tema</label>
                    <asp:TextBox ID="TextTema" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

            </div>


            <!-- Tipo de material -->
            <div class="row">
                <div class="col-md-4 mb-3">
                    <label for="tipoMaterial" class="form-label">Tipo de material</label>
                    <asp:DropDownList ID="ddlTipoMaterial" runat="server" CssClass="form-select" onchange="mostrarCampos()">
                        <asp:ListItem Value="">Seleccione un tipo de material</asp:ListItem>
                        <asp:ListItem Value="Libro">Libro</asp:ListItem>
                        <asp:ListItem Value="Tesis">Tesis</asp:ListItem>
                        <asp:ListItem Value="Articulo">Artículo</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4 mb-3">
                    <label for="anho" class="form-label">Año de Publicación</label>
                    <asp:TextBox ID="txtAnho" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-4 mb-3">
                    <label for="paginas" class="form-label">Nro. de páginas</label>
                    <asp:TextBox ID="txtPaginas" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <!-- Fila de IDIOMA -->
            <div class="row">
                <div class="col-md-12 mb-3">
                    <label for="idioma" class="form-label">Idioma</label>
                    <asp:TextBox ID="TextIdioma" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>
                
            </div>




            <!-- Campos para "Libro" -->
            <div class="row">
                <div class="col-md-6 mb-3" id="isbnField" style="display:none;">
                    <label for="isbn" class="form-label">ISBN</label>
                    <asp:TextBox ID="txtISBN" runat="server" CssClass="form-control" placeholder="Ingrese ISBN"></asp:TextBox>
                </div>
                <div class="col-md-6 mb-3" id="edicionField" style="display:none;">
                    <label for="edicion" class="form-label">Edición</label>
                    <asp:TextBox ID="txtEdicion" runat="server" CssClass="form-control" placeholder="Ingrese edición"></asp:TextBox>
                </div>
            </div>

            <!-- Campos para "Tesis" -->
            <div class="row">
                <div class="col-md-3 mb-3" id="especialidadField" style="display:none;">
                    <label for="especialidad" class="form-label">Especialidad</label>
                    <asp:TextBox ID="txtEspecialidad" runat="server" CssClass="form-control" placeholder="Ingrese especialidad"></asp:TextBox>
                </div>
                <div class="col-md-3 mb-3" id="asesorField" style="display:none;">
                    <label for="asesor" class="form-label">Asesor</label>
                    <asp:TextBox ID="txtAsesor" runat="server" CssClass="form-control" placeholder="Ingrese asesor"></asp:TextBox>
                </div>
                <div class="col-md-3 mb-3" id="gradoField" style="display:none;">
                    <label for="grado" class="form-label">Grado</label>
                    <asp:TextBox ID="txtGrado" runat="server" CssClass="form-control" placeholder="Ingrese grado"></asp:TextBox>
                </div>
                <div class="col-md-3 mb-3" id="institucionField" style="display:none;">
                    <label for="institucion" class="form-label">Institución de Publicación</label>
                    <asp:TextBox ID="txtInstitucion" runat="server" CssClass="form-control" placeholder="Ingrese institución"></asp:TextBox>
                </div>
            </div>

            <!-- Campos para "Articulo" -->
            <div class="row">
                <div class="col-md-3 mb-3" id="issnField" style="display:none;">
                    <label for="issn" class="form-label">ISSN</label>
                    <asp:TextBox ID="txtISSN" runat="server" CssClass="form-control" placeholder="Ingrese ISSN"></asp:TextBox>
                </div>
                <div class="col-md-3 mb-3" id="revistaField" style="display:none;">
                    <label for="revista" class="form-label">Revista</label>
                    <asp:TextBox ID="txtRevista" runat="server" CssClass="form-control" placeholder="Ingrese nombre de la revista"></asp:TextBox>
                </div>
                <div class="col-md-3 mb-3" id="volumenField" style="display:none;">
                    <label for="volumen" class="form-label">Volumen</label>
                    <asp:TextBox ID="txtVolumen" runat="server" CssClass="form-control" placeholder="Ingrese volumen"></asp:TextBox>
                </div>
                <div class="col-md-3 mb-3" id="numeroField" style="display:none;">
                    <label for="numero" class="form-label">Número</label>
                    <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" placeholder="Ingrese número"></asp:TextBox>
                </div>
            </div>

             <p1>Registro de ejemplares</p1>

                <!-- Contenedor para los ejemplares -->
                <div id="ejemplares-container">
                    <!-- Los ejemplares serán agregados aquí dinámicamente -->
                </div>

            <!-- Botón para añadir un ejemplar -->
            <div class="row mt-3">
                <div class="col-md-12">
                    <button type="button" class="btn pill-info w-100 py-2" onclick="añadirEjemplar()">
                        <i class="fa-solid fa-circle-plus me-2"></i>Añadir ejemplar
                    </button>
                </div>
            </div>



            </div>

        </div>

    <div class="row mt-3 mb-4">
        <div class="col-md-12 text-end">
            <asp:Button
                ID="btnGuardarMaterial"
                runat="server"
                Text="Agregar material →"
                OnClick="GuardarMaterial_Click"
                OnClientClick="return validarFormulario();"
                CssClass="btn btn-primary" />
        </div>
    </div>

    
    <asp:Label ID="lblMensaje" runat="server" CssClass="mt-3" style="display: block;"></asp:Label>

    
</asp:Content>

