// Función para obtener las opciones de bibliotecas desde el HiddenField
function getBibliotecasOptions() {
    const hiddenField = document.getElementById('<%= hfBibliotecasOptions.ClientID %>');
    return hiddenField ? hiddenField.value : '<option value="">Error al cargar bibliotecas</option>';
}





let contribuyenteCount = 1;
let ejemplarCount = 0;

// ===== ACTUALIZAR LA FUNCIÓN inicializarFormulario =====
function inicializarFormulario() {
    const urlParams = new URLSearchParams(window.location.search);
    const tieneId = urlParams.has('id');

    if (!tieneId) {
        añadirEjemplar();
        añadirContribuyente();
    }

    // Configurar event listeners para limpiar errores
    configurarEventListeners();

    // Configurar validaciones iniciales - ESPERAR a que la página cargue completamente
    setTimeout(() => {
        const ddlTipoMaterial = document.getElementById("<%= ddlTipoMaterial.ClientID %>");
        if (ddlTipoMaterial && ddlTipoMaterial.value) {
            configurarValidacionesDinamicas(ddlTipoMaterial.value);
        }
        verificarErroresYDeshabilitarBoton();
    }, 500);
}
document.addEventListener('DOMContentLoaded', inicializarFormulario);

// ===== MEJORAR LA FUNCIÓN CONFIGURAR VALIDACIONES DINÁMICAS =====
function configurarValidacionesDinamicas(tipoMaterial) {
    console.log("🔧 Configurando validaciones para:", tipoMaterial);

    // Limpiar validaciones previas de campos dinámicos
    const camposDinamicos = [
        document.getElementById("<%= txtISBN.ClientID %>"),
        document.getElementById("<%= txtEdicion.ClientID %>"),
        document.getElementById("<%= txtEspecialidad.ClientID %>"),
        document.getElementById("<%= txtAsesor.ClientID %>"),
        document.getElementById("<%= txtGrado.ClientID %>"),
        document.getElementById("<%= txtInstitucion.ClientID %>"),
        document.getElementById("<%= txtISSN.ClientID %>"),
        document.getElementById("<%= txtRevista.ClientID %>"),
        document.getElementById("<%= txtVolumen.ClientID %>"),
        document.getElementById("<%= txtNumero.ClientID %>")
    ];

    camposDinamicos.forEach(campo => {
        if (campo) {
            campo.classList.remove('is-invalid');
            campo.classList.remove('is-valid');
            const errorLabel = campo.parentNode.querySelector('.invalid-feedback');
            if (errorLabel) {
                errorLabel.remove();
            }
        }
    });

    // Configurar validaciones según el tipo de material
    if (tipoMaterial === 'Libro') {
        const isbnField = document.getElementById("<%= txtISBN.ClientID %>");
        const edicionField = document.getElementById("<%= txtEdicion.ClientID %>");

        if (isbnField) {
            console.log("📚 Configurando validación para ISBN");
            configurarValidacion(isbnField, validarISBN);
        }
        if (edicionField) {
            console.log("📚 Configurando validación para Edición");
            configurarValidacion(edicionField, validarCampoGeneral, 'Edición');
        }
    }
    else if (tipoMaterial === 'Tesis') {
        const especialidadField = document.getElementById("<%= txtEspecialidad.ClientID %>");
        const asesorField = document.getElementById("<%= txtAsesor.ClientID %>");
        const gradoField = document.getElementById("<%= txtGrado.ClientID %>");
        const institucionField = document.getElementById("<%= txtInstitucion.ClientID %>");

        if (especialidadField) {
            console.log("🎓 Configurando validación para Especialidad");
            configurarValidacion(especialidadField, validarCampoGeneral, 'Especialidad');
        }
        if (asesorField) {
            console.log("🎓 Configurando validación para Asesor");
            configurarValidacion(asesorField, validarCampoGeneral, 'Asesor');
        }
        if (gradoField) {
            console.log("🎓 Configurando validación para Grado");
            configurarValidacion(gradoField, validarCampoGeneral, 'Grado');
        }
        if (institucionField) {
            console.log("🎓 Configurando validación para Institución");
            configurarValidacion(institucionField, validarCampoGeneral, 'Institución de Publicación');
        }
    }
    else if (tipoMaterial === 'Articulo') {
        const issnField = document.getElementById("<%= txtISSN.ClientID %>");
        const revistaField = document.getElementById("<%= txtRevista.ClientID %>");
        const volumenField = document.getElementById("<%= txtVolumen.ClientID %>");
        const numeroField = document.getElementById("<%= txtNumero.ClientID %>");

        if (issnField) {
            console.log("📄 Configurando validación para ISSN");
            configurarValidacion(issnField, validarISSN);
        }
        if (revistaField) {
            console.log("📄 Configurando validación para Revista");
            configurarValidacion(revistaField, validarCampoGeneral, 'Revista');
        }
        if (volumenField) {
            console.log("📄 Configurando validación para Volumen");
            configurarValidacion(volumenField, validarNumeroPositivo, 'Volumen');
        }
        if (numeroField) {
            console.log("📄 Configurando validación para Número");
            configurarValidacion(numeroField, validarNumeroPositivo, 'Número');
        }
    }

    console.log("✅ Validaciones dinámicas configuradas para:", tipoMaterial);
}





// ===== FUNCIÓN PARA SOLO NÚMEROS =====
function soloNumeros(event) {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        event.preventDefault();
        return false;
    }
    return true;
}

// ===== VALIDACIONES ESPECÍFICAS POR CAMPO =====
function validarTitulo(campo) {
    const valor = campo.value.trim();

    if (valor === '') {
        return { esValido: false, mensaje: 'El título es requerido' };
    }

    // Letras, números, espacios, acentos y puntuación básica
    const regex = /^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑüÜ\s\-\.,;:¿?¡!()"]+$/;

    if (!regex.test(valor)) {
        return {
            esValido: false,
            mensaje: 'El título solo puede contener letras, números, espacios y signos de puntuación básicos (.,;:¿?¡!-)"'
        };
    }

    if (valor.length < 2) {
        return { esValido: false, mensaje: 'El título debe tener al menos 2 caracteres' };
    }

    if (valor.length > 500) {
        return { esValido: false, mensaje: 'El título no puede exceder 500 caracteres' };
    }

    return { esValido: true, mensaje: '' };
}

function validarNombreApellido(campo, nombreCampo) {
    const valor = campo.value.trim();

    if (valor === '') {
        return { esValido: false, mensaje: `El campo ${nombreCampo} es requerido` };
    }

    // Solo letras, espacios y acentos
    const regex = /^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$/;

    if (!regex.test(valor)) {
        return {
            esValido: false,
            mensaje: `El campo ${nombreCampo} solo puede contener letras y espacios`
        };
    }

    if (valor.length < 2) {
        return { esValido: false, mensaje: `El campo ${nombreCampo} debe tener al menos 2 caracteres` };
    }

    if (valor.length > 50) {
        return { esValido: false, mensaje: `El campo ${nombreCampo} no puede exceder 50 caracteres` };
    }

    return { esValido: true, mensaje: '' };
}

function validarSeudonimo(campo) {
    const valor = campo.value.trim();

    // Puede estar vacío
    if (valor === '') {
        return { esValido: true, mensaje: '' };
    }

    // Solo letras, espacios y acentos
    const regex = /^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$/;

    if (!regex.test(valor)) {
        return {
            esValido: false,
            mensaje: 'El seudónimo solo puede contener letras y espacios'
        };
    }

    if (valor.length > 50) {
        return { esValido: false, mensaje: 'El seudónimo no puede exceder 50 caracteres' };
    }

    return { esValido: true, mensaje: '' };
}

function validarAnhoPublicacion(campo) {
    const valor = campo.value.trim();

    if (valor === '') {
        return { esValido: false, mensaje: 'El año de publicación es requerido' };
    }

    // Solo números
    const regex = /^\d+$/;
    if (!regex.test(valor)) {
        return { esValido: false, mensaje: 'El año de publicación debe contener solo números' };
    }

    const anho = parseInt(valor);
    const anhoActual = new Date().getFullYear();

    if (anho < 868 || anho > anhoActual + 1) {
        return {
            esValido: false,
            mensaje: `El año de publicación debe estar entre 868  y ${anhoActual + 1}`
        };
    }

    return { esValido: true, mensaje: '' };
}

function validarNumeroPaginas(campo) {
    const valor = campo.value.trim();

    if (valor === '') {
        return { esValido: false, mensaje: 'El número de páginas es requerido' };
    }

    // Solo números
    const regex = /^\d+$/;
    if (!regex.test(valor)) {
        return { esValido: false, mensaje: 'El número de páginas debe contener solo números' };
    }

    const paginas = parseInt(valor);

    if (paginas <= 0) {
        return { esValido: false, mensaje: 'El número de páginas debe ser mayor a 0' };
    }

    if (paginas > 10000) {
        return { esValido: false, mensaje: 'El número de páginas no puede exceder 10,000' };
    }

    return { esValido: true, mensaje: '' };
}

function validarIdioma(campo) {
    const valor = campo.value.trim();

    if (valor === '') {
        return { esValido: false, mensaje: 'El idioma es requerido' };
    }

    // Solo letras y espacios
    const regex = /^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$/;

    if (!regex.test(valor)) {
        return {
            esValido: false,
            mensaje: 'El idioma solo puede contener letras y espacios'
        };
    }

    if (valor.length < 2) {
        return { esValido: false, mensaje: 'El idioma debe tener al menos 2 caracteres' };
    }

    if (valor.length > 50) {
        return { esValido: false, mensaje: 'El idioma no puede exceder 50 caracteres' };
    }

    return { esValido: true, mensaje: '' };
}

function validarUbicacion(campo) {
    const valor = campo.value.trim();

    if (valor === '') {
        return { esValido: false, mensaje: 'La ubicación es requerida' };
    }

    // Letras, números, espacios y puntuación básica
    const regex = /^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑüÜ\s\-\.,;:¿?¡!()"]+$/;

    if (!regex.test(valor)) {
        return {
            esValido: false,
            mensaje: 'La ubicación solo puede contener letras, números, espacios y signos de puntuación básicos'
        };
    }

    if (valor.length < 2) {
        return { esValido: false, mensaje: 'La ubicación debe tener al menos 2 caracteres' };
    }

    if (valor.length > 100) {
        return { esValido: false, mensaje: 'La ubicación no puede exceder 100 caracteres' };
    }

    return { esValido: true, mensaje: '' };
}

function validarCampoGeneral(campo, nombreCampo) {
    const valor = campo.value.trim();

    if (valor === '') {
        return { esValido: false, mensaje: `El campo ${nombreCampo} es requerido` };
    }

    // Letras, números, espacios y puntuación básica
    const regex = /^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑüÜ\s\-\.,;:¿?¡!()"]+$/;

    if (!regex.test(valor)) {
        return {
            esValido: false,
            mensaje: `El campo ${nombreCampo} solo puede contener letras, números, espacios y signos de puntuación básicos`
        };
    }

    if (valor.length < 2) {
        return { esValido: false, mensaje: `El campo ${nombreCampo} debe tener al menos 2 caracteres` };
    }

    if (valor.length > 100) {
        return { esValido: false, mensaje: `El campo ${nombreCampo} no puede exceder 100 caracteres` };
    }

    return { esValido: true, mensaje: '' };
}

function validarISBN(campo) {
    const valor = campo.value.trim();

    if (valor === '') {
        return { esValido: false, mensaje: 'El ISBN es requerido para libros' };
    }

    // ISBN-10 o ISBN-13 (permite guiones y espacios)
    const regex = /^(?:\d[\-\s]?){9}[\dX]$|^(?:\d[\-\s]?){12}[\d]$/;

    if (!regex.test(valor)) {
        return {
            esValido: false,
            mensaje: 'El formato del ISBN no es válido. Use ISBN-10 (ej: 0-306-40615-2) o ISBN-13 (ej: 978-0-306-40615-7)'
        };
    }

    return { esValido: true, mensaje: '' };
}

function validarISSN(campo) {
    const valor = campo.value.trim();

    if (valor === '') {
        return { esValido: false, mensaje: 'El ISSN es requerido para artículos' };
    }

    // Formato ISSN: XXXX-XXXX (8 caracteres con guión)
    const regex = /^\d{4}\-\d{3}[\dX]$/;

    if (!regex.test(valor)) {
        return {
            esValido: false,
            mensaje: 'El formato del ISSN no es válido. Use el formato: XXXX-XXXX (ej: 1234-5678)'
        };
    }

    return { esValido: true, mensaje: '' };
}

function validarNumeroPositivo(campo, nombreCampo) {
    const valor = campo.value.trim();

    if (valor === '') {
        return { esValido: false, mensaje: `El campo ${nombreCampo} es requerido` };
    }

    // Solo números
    const regex = /^\d+$/;
    if (!regex.test(valor)) {
        return { esValido: false, mensaje: `El campo ${nombreCampo} debe contener solo números` };
    }

    const numero = parseInt(valor);

    if (numero < 0) {
        return { esValido: false, mensaje: `El campo ${nombreCampo} no puede ser negativo` };
    }

    if (numero > 1000000) {
        return { esValido: false, mensaje: `El campo ${nombreCampo} no puede exceder 1,000,000` };
    }

    return { esValido: true, mensaje: '' };
}

// ===== FUNCIÓN PARA APLICAR ESTILOS DE ERROR =====
function mostrarErrorCampo(campo, mensaje) {
    // Remover estilos previos
    campo.classList.remove('is-invalid');
    campo.classList.remove('is-valid');

    // Remover mensaje anterior
    const feedbackExistente = campo.parentNode.querySelector('.invalid-feedback');
    if (feedbackExistente) {
        feedbackExistente.remove();
    }

    if (mensaje) {
        campo.classList.add('is-invalid');

        const feedback = document.createElement('div');
        feedback.className = 'invalid-feedback';
        feedback.textContent = mensaje;
        campo.parentNode.appendChild(feedback);
    } else {
        campo.classList.add('is-valid');
    }

    verificarErroresYDeshabilitarBoton();
}

// ===== FUNCIÓN PARA VERIFICAR ERRORES Y DESHABILITAR BOTÓN =====
function verificarErroresYDeshabilitarBoton() {
    const errores = document.querySelectorAll('.is-invalid');
    const boton = document.getElementById("<%= btnGuardarMaterial.ClientID %>");

    if (boton) {
        if (errores.length > 0) {
            // Hay errores - deshabilitar botón
            boton.disabled = true;
            boton.classList.remove('btn-primary');
            boton.classList.add('btn-secondary');
        } else {
            // No hay errores - habilitar botón
            boton.disabled = false;
            boton.classList.remove('btn-secondary');
            boton.classList.add('btn-primary');
        }
    }
}

// ===== MANTENER configurarEventListeners PERO MEJORAR EL DROPDOWN =====
function configurarEventListeners() {
    // Configurar event listeners para todos los campos
    const configurarValidacion = (elemento, funcionValidacion, nombreCampo = '') => {
        if (elemento) {
            elemento.addEventListener('blur', function () {
                let validacion;
                if (nombreCampo) {
                    validacion = funcionValidacion(this, nombreCampo);
                } else {
                    validacion = funcionValidacion(this);
                }
                mostrarErrorCampo(this, validacion.mensaje);
            });

            elemento.addEventListener('input', function () {
                this.classList.remove('is-invalid');
                const errorLabel = this.parentNode.querySelector('.invalid-feedback');
                if (errorLabel) {
                    errorLabel.remove();
                }
                verificarErroresYDeshabilitarBoton();
            });
        }
    };

    // Configurar validaciones para campos estáticos
    configurarValidacion(document.getElementById("<%= txtTitulo.ClientID %>"), validarTitulo);
    configurarValidacion(document.getElementById("<%= TextTema.ClientID %>"), validarCampoGeneral, 'Tema');
    configurarValidacion(document.getElementById("<%= txtAnho.ClientID %>"), validarAnhoPublicacion);
    configurarValidacion(document.getElementById("<%= txtPaginas.ClientID %>"), validarNumeroPaginas);
    configurarValidacion(document.getElementById("<%= TextIdioma.ClientID %>"), validarIdioma);

    // Para el dropdown de tipo de material - MEJORADO
    const ddlTipoMaterial = document.getElementById("<%= ddlTipoMaterial.ClientID %>");
    if (ddlTipoMaterial) {
        ddlTipoMaterial.addEventListener('change', function () {
            this.classList.remove('is-invalid');
            // Configurar validaciones después de cambiar el tipo
            setTimeout(() => {
                configurarValidacionesDinamicas(this.value);
            }, 100);
            verificarErroresYDeshabilitarBoton();
        });
    }

    // Resto del código para campos dinámicos (contribuyentes y ubicaciones)...
    document.addEventListener('blur', function (e) {
        if (e.target.name === 'nombre[]') {
            const validacion = validarNombreApellido(e.target, 'Nombre');
            mostrarErrorCampo(e.target, validacion.mensaje);
        } else if (e.target.name === 'primer_apellido[]') {
            const validacion = validarNombreApellido(e.target, 'Primer apellido');
            mostrarErrorCampo(e.target, validacion.mensaje);
        } else if (e.target.name === 'segundo_apellido[]') {
            const validacion = validarNombreApellido(e.target, 'Segundo apellido');
            mostrarErrorCampo(e.target, validacion.mensaje);
        } else if (e.target.name === 'seudonimo[]') {
            const validacion = validarSeudonimo(e.target);
            mostrarErrorCampo(e.target, validacion.mensaje);
        } else if (e.target.name === 'ubicacion[]') {
            const validacion = validarUbicacion(e.target);
            mostrarErrorCampo(e.target, validacion.mensaje);
        } else if (e.target.name === 'biblioteca[]') {
            // Validar biblioteca seleccionada
            if (e.target.value) {
                e.target.classList.remove('is-invalid');
                const errorLabel = e.target.parentNode.querySelector('.invalid-feedback');
                if (errorLabel) {
                    errorLabel.remove();
                }
            } else {
                e.target.classList.add('is-invalid');
                const feedback = document.createElement('div');
                feedback.className = 'invalid-feedback';
                feedback.textContent = 'Debe seleccionar una biblioteca';
                e.target.parentNode.appendChild(feedback);
            }
            verificarErroresYDeshabilitarBoton();
        }
    }, true);

    // También escuchar cambios en inputs para campos dinámicos
    document.addEventListener('input', function (e) {
        if (e.target.name === 'nombre[]' || e.target.name === 'primer_apellido[]' ||
            e.target.name === 'segundo_apellido[]' || e.target.name === 'seudonimo[]' ||
            e.target.name === 'ubicacion[]') {
            e.target.classList.remove('is-invalid');
            const errorLabel = e.target.parentNode.querySelector('.invalid-feedback');
            if (errorLabel) {
                errorLabel.remove();
            }
            verificarErroresYDeshabilitarBoton();
        }
    }, true);
}

// ===== CONTRIBUYENTES =====
function añadirContribuyente() {
    contribuyenteCount++;
    const newContribuyente = `
                <div class="row mb-3" id="contribuyente-${contribuyenteCount}">
                    <input type="hidden" name="id_contribuyente[]" value="">
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
                </div>`;
    document.getElementById("contribuyentes-container").insertAdjacentHTML('beforeend', newContribuyente);
    verificarErroresYDeshabilitarBoton();
}

function eliminarContribuyente(id) {
    const contribuyente = document.getElementById(`contribuyente-${id}`);
    if (contribuyente) {
        contribuyente.remove();
        reindexarContribuyentes();
        verificarErroresYDeshabilitarBoton();
    }
}

function reindexarContribuyentes() {
    const container = document.getElementById("contribuyentes-container");
    const filas = container.querySelectorAll('.row.mb-3');

    filas.forEach((fila, index) => {
        const nuevoId = index + 1;
        fila.id = `contribuyente-${nuevoId}`;

        const btnEliminar = fila.querySelector('button.btn-danger');
        if (btnEliminar) {
            btnEliminar.setAttribute('onclick', `eliminarContribuyente(${nuevoId})`);
        }
    });

    contribuyenteCount = filas.length;
}

// ===== EJEMPLARES =====
function añadirEjemplar() {
    ejemplarCount++;
    const newEjemplar = `
        <div class="row mb-3" id="ejemplar-${ejemplarCount}">
            <input type="hidden" name="id_ejemplar[]" value="">
            <input type="hidden" name="estado_ejemplar[]" value="DISPONIBLE">
            <div class="col-md-2">
                <label class="form-label">Código Ejemplar</label>
                <input type="text" class="form-control" disabled value="Nuevo">
            </div>
            <div class="col-md-2">
                <label class="form-label">Estado</label>
                <input type="text" class="form-control" value="DISPONIBLE" readonly>
            </div>
            <div class="col-md-3">
                <label class="form-label">Biblioteca</label>
                <select class="form-select" name="biblioteca[]" required>
                    <option value="">Seleccione biblioteca</option>
                    ${getBibliotecasOptions()}
                </select>
            </div>
            <div class="col-md-3">
                <label class="form-label">Ubicación</label>
                <input type="text" class="form-control" placeholder="Ingrese ubicación" name="ubicacion[]" required>
            </div>
            <div class="col-md-2 d-flex align-items-end">
                <button type="button" class="btn btn-danger" onclick="eliminarEjemplar(${ejemplarCount})">
                    <i class="fas fa-trash-alt"></i>
                </button>
            </div>
        </div>`;
    document.getElementById("ejemplares-container").insertAdjacentHTML('beforeend', newEjemplar);
    verificarErroresYDeshabilitarBoton();
}

function añadirEjemplarExistente(ejemplar, index) {
    const ejemplarId = `ejemplar-${Date.now()}-${index}`;
    const selectId = `ddlBiblioteca-${Date.now()}-${index}`;
    const selectEstadoId = `ddlEstado-${Date.now()}-${index}`;

    const estadoActual = ejemplar.Estado || 'DISPONIBLE';
    const esPrestado = estadoActual === 'PRESTADO';
    const puedeEliminar = estadoActual === 'DISPONIBLE';
    const puedeEditarEstado = !esPrestado;

    const newEjemplar = `
        <div class="row mb-3" id="${ejemplarId}">
            <input type="hidden" name="id_ejemplar[]" value="${ejemplar.IdEjemplar || ''}">
            <div class="col-md-2">
                <label class="form-label">Código Ejemplar</label>
                <input type="text" class="form-control" disabled value="${ejemplar.CodigoEjemplar || ''}">
            </div>
            <div class="col-md-2">
                <label class="form-label">Estado</label>
                ${esPrestado ?
            `<input type="text" class="form-control" value="PRESTADO" readonly>
                     <input type="hidden" name="estado_ejemplar[]" value="PRESTADO">` :
            `<select class="form-select" name="estado_ejemplar[]" id="${selectEstadoId}" required 
                            onchange="actualizarBotonEliminar(this, '${ejemplarId}')">
                        <option value="DISPONIBLE" ${estadoActual === 'DISPONIBLE' ? 'selected' : ''}>DISPONIBLE</option>
                        <option value="EN_REPARACION" ${estadoActual === 'EN_REPARACION' ? 'selected' : ''}>EN_REPARACION</option>
                        <option value="PERDIDO" ${estadoActual === 'PERDIDO' ? 'selected' : ''}>PERDIDO</option>
                    </select>`
        }
            </div>
            <div class="col-md-3">
                <label class="form-label">Biblioteca</label>
                <select class="form-select" name="biblioteca[]" id="${selectId}" required 
                    ${esPrestado ? '' : ''}>
                    <option value="">Seleccione biblioteca</option>
                    ${getBibliotecasOptions()}
                </select>
            </div>
            <div class="col-md-3">
                <label class="form-label">Ubicación</label>
                <input type="text" class="form-control" name="ubicacion[]"
                    value="${ejemplar.Ubicacion || ''}" required
                    ${esPrestado ? '' : ''}>
            </div>
            <div class="col-md-2 d-flex align-items-end">
                <button type="button" class="btn btn-danger ${!puedeEliminar ? 'disabled' : ''}" 
                    onclick="${puedeEliminar ? `eliminarEjemplarExistente('${ejemplarId}')` : ''}"
                    ${!puedeEliminar ? 'disabled' : ''}
                    style="${!puedeEliminar ? 'opacity: 0.5; cursor: not-allowed;' : ''}">
                    <i class="fas fa-trash-alt"></i>
                </button>
            </div>
        </div>`;

    document.getElementById("ejemplares-container").insertAdjacentHTML('beforeend', newEjemplar);

    // Seleccionar valores en los dropdowns
    setTimeout(() => {
        const selectElement = document.getElementById(selectId);
        if (selectElement && ejemplar.BibliotecaId) {
            selectElement.value = ejemplar.BibliotecaId.toString();
        }

        if (!esPrestado) {
            const selectEstado = document.getElementById(selectEstadoId);
            if (selectEstado && estadoActual) {
                selectEstado.value = estadoActual;
            }
        }
        verificarErroresYDeshabilitarBoton();
    }, 0);
}

function obtenerClaseEstado(estado) {
    const estadoUpper = estado?.toUpperCase() || 'DISPONIBLE';
    switch (estadoUpper) {
        case 'DISPONIBLE': return 'bg-success';
        case 'PRESTADO': return 'bg-warning text-dark';
        case 'EN_REPARACION': return 'bg-info';
        case 'NO_DISPONIBLE': return 'bg-danger';
        default: return 'bg-secondary';
    }
}

// Función para actualizar el estado del botón eliminar (solo en edición)
function actualizarBotonEliminar(selectElement, ejemplarId) {
    const estado = selectElement.value;
    const puedeEliminar = estado === 'DISPONIBLE';

    const ejemplarDiv = document.getElementById(ejemplarId);

    if (ejemplarDiv) {
        const botonEliminar = ejemplarDiv.querySelector('button.btn-danger');
        if (botonEliminar) {
            if (puedeEliminar) {
                // Habilitar botón
                botonEliminar.classList.remove('disabled');
                botonEliminar.disabled = false;
                botonEliminar.style.opacity = '1';
                botonEliminar.style.cursor = 'pointer';
                botonEliminar.setAttribute('onclick', `eliminarEjemplarExistente('${ejemplarId}')`);
            } else {
                // Deshabilitar botón
                botonEliminar.classList.add('disabled');
                botonEliminar.disabled = true;
                botonEliminar.style.opacity = '0.5';
                botonEliminar.style.cursor = 'not-allowed';
                botonEliminar.removeAttribute('onclick');
            }
        }
    }
}

function eliminarEjemplarExistente(id) {
    const ejemplar = document.getElementById(id);
    if (ejemplar) {
        const selectEstado = ejemplar.querySelector('select[name="estado_ejemplar[]"]');
        const estado = selectEstado ? selectEstado.value : 'PRESTADO'; // Si no hay select, es PRESTADO

        if (estado === 'DISPONIBLE') {
            ejemplar.remove();
            verificarErroresYDeshabilitarBoton();
        } else {
            mostrarErrorEstado('Solo se pueden eliminar ejemplares con estado DISPONIBLE');
        }
    }
}

function mostrarErrorEstado(mensaje) {
    const modalHtml = `
        <div class="modal fade" id="modalErrorEstado" tabindex="-1">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header bg-warning text-dark">
                        <h5 class="modal-title">Acción no permitida</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body text-center">
                        <div class="mb-3">
                            <i class="fas fa-exclamation-triangle text-warning" style="font-size: 3rem;"></i>
                        </div>
                        <h5 class="mb-3">${mensaje}</h5>
                        <p class="text-muted">Solo se pueden eliminar ejemplares con estado DISPONIBLE.</p>
                    </div>
                    <div class="modal-footer justify-content-center">
                        <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Entendido</button>
                    </div>
                </div>
            </div>
        </div>`;

    const modalExistente = document.getElementById('modalErrorEstado');
    if (modalExistente) modalExistente.remove();

    document.body.insertAdjacentHTML('beforeend', modalHtml);
    const modal = new bootstrap.Modal(document.getElementById('modalErrorEstado'));
    modal.show();

    document.getElementById('modalErrorEstado').addEventListener('hidden.bs.modal', function () {
        this.remove();
    });
}

function eliminarEjemplar(id) {
    const ejemplar = document.getElementById(`ejemplar-${id}`);
    if (ejemplar) {
        // En modo registro, todos los ejemplares se pueden eliminar
        ejemplar.remove();
        reindexarEjemplares();
        verificarErroresYDeshabilitarBoton();
    }
}

function reindexarEjemplares() {
    const container = document.getElementById("ejemplares-container");
    const filas = container.querySelectorAll('.row.mb-3');

    filas.forEach((fila, index) => {
        const nuevoId = index + 1;
        fila.id = `ejemplar-${nuevoId}`;

        const btnEliminar = fila.querySelector('button.btn-danger');
        if (btnEliminar) {
            btnEliminar.setAttribute('onclick', `eliminarEjemplar(${nuevoId})`);
        }
    });

    ejemplarCount = filas.length;
}

// ===== ACTUALIZAR mostrarCampos =====
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

    // CONFIGURAR VALIDACIONES PARA LOS CAMPOS QUE SE MUESTRAN
    configurarValidacionesDinamicas(tipoMaterial);
    verificarErroresYDeshabilitarBoton();
}

// ===== CARGA DE DATOS =====
function cargarEjemplaresExistente(ejemplares) {
    const container = document.getElementById("ejemplares-container");
    if (container) {
        container.innerHTML = '';
        ejemplarCount = 0;

        if (ejemplares && Array.isArray(ejemplares) && ejemplares.length > 0) {
            ejemplares.forEach(function (ejemplar, index) {
                añadirEjemplarExistente(ejemplar, index);
            });
        } else {
            añadirEjemplar();
        }
    }
}

function cargarContribuyentesExistente(contribuyentes) {
    const container = document.getElementById("contribuyentes-container");
    if (container) {
        container.innerHTML = '';
        contribuyenteCount = 0;

        if (contribuyentes && Array.isArray(contribuyentes) && contribuyentes.length > 0) {
            contribuyentes.forEach(function (contribuyente, index) {
                añadirContribuyenteExistente(contribuyente);
            });
        } else {
            añadirContribuyente();
        }
    }
}

function añadirContribuyenteExistente(contribuyente) {
    contribuyenteCount++;
    let tipoValor = "AUTOR";
    if (contribuyente.Tipo_contribuyente == 1) tipoValor = "TRADUCTOR";
    if (contribuyente.Tipo_contribuyente == 2) tipoValor = "EDITOR";

    const newContribuyente = `
                <div class="row mb-3" id="contribuyente-${contribuyenteCount}">
                    <!-- AGREGADO: Hidden field con ID existente -->
                    <input type="hidden" name="id_contribuyente[]" value="${contribuyente.IdContribuyente || ''}">
                    <div class="col-md-2">
                        <select class="form-select" name="autor[]" required>
                            <option value="AUTOR" ${tipoValor === 'AUTOR' ? 'selected' : ''}>AUTOR</option>
                            <option value="TRADUCTOR" ${tipoValor === 'TRADUCTOR' ? 'selected' : ''}>TRADUCTOR</option>
                            <option value="EDITOR" ${tipoValor === 'EDITOR' ? 'selected' : ''}>EDITOR</option>
                        </select>
                    </div>
                    <div class="col-md-2">
                        <input type="text" class="form-control" name="nombre[]" value="${contribuyente.Nombre || ''}" required>
                    </div>
                    <div class="col-md-2">
                        <input type="text" class="form-control" name="primer_apellido[]" value="${contribuyente.Primer_apellido || ''}" required>
                    </div>
                    <div class="col-md-2">
                        <input type="text" class="form-control" name="segundo_apellido[]" value="${contribuyente.Segundo_apellido || ''}" required>
                    </div>
                    <div class="col-md-3">
                        <input type="text" class="form-control" name="seudonimo[]" value="${contribuyente.Seudonimo || ''}">
                    </div>
                    <div class="col-md-1">
                        <button type="button" class="btn btn-danger" onclick="eliminarContribuyente(${contribuyenteCount})">
                            <i class="fas fa-trash-alt"></i>
                        </button>
                    </div>
                </div>`;
    document.getElementById("contribuyentes-container").insertAdjacentHTML('beforeend', newContribuyente);
}

// Reemplazar la función validarFormulario existente
function validarFormulario() {
    return validarFormularioCompleto();
}

// Función de validación completa para el envío del formulario
function validarFormularioCompleto() {
    let esValido = true;

    // Validar título
    const titulo = document.getElementById("<%= txtTitulo.ClientID %>");
    const validacionTitulo = validarTitulo(titulo);
    if (!validacionTitulo.esValido) {
        mostrarErrorCampo(titulo, validacionTitulo.mensaje);
        esValido = false;
    }

    // Validar contribuyentes
    const nombres = document.getElementsByName("nombre[]");
    const primerApellidos = document.getElementsByName("primer_apellido[]");
    const segundoApellidos = document.getElementsByName("segundo_apellido[]");
    const seudonimos = document.getElementsByName("seudonimo[]");

    for (let i = 0; i < nombres.length; i++) {
        // Validar nombre
        const validacionNombre = validarNombreApellido(nombres[i], `Nombre del contribuyente ${i + 1}`);
        if (!validacionNombre.esValido) {
            mostrarErrorCampo(nombres[i], validacionNombre.mensaje);
            esValido = false;
        }

        // Validar primer apellido
        const validacionPrimerApellido = validarNombreApellido(primerApellidos[i], `Primer apellido del contribuyente ${i + 1}`);
        if (!validacionPrimerApellido.esValido) {
            mostrarErrorCampo(primerApellidos[i], validacionPrimerApellido.mensaje);
            esValido = false;
        }

        // Validar segundo apellido
        const validacionSegundoApellido = validarNombreApellido(segundoApellidos[i], `Segundo apellido del contribuyente ${i + 1}`);
        if (!validacionSegundoApellido.esValido) {
            mostrarErrorCampo(segundoApellidos[i], validacionSegundoApellido.mensaje);
            esValido = false;
        }

        // Validar seudónimo
        const validacionSeudonimo = validarSeudonimo(seudonimos[i]);
        if (!validacionSeudonimo.esValido) {
            mostrarErrorCampo(seudonimos[i], validacionSeudonimo.mensaje);
            esValido = false;
        }
    }

    // Validar tema
    const tema = document.getElementById("<%= TextTema.ClientID %>");
    const validacionTema = validarCampoGeneral(tema, 'Tema');
    if (!validacionTema.esValido) {
        mostrarErrorCampo(tema, validacionTema.mensaje);
        esValido = false;
    }

    // Validar año de publicación
    const anho = document.getElementById("<%= txtAnho.ClientID %>");
    const validacionAnho = validarAnhoPublicacion(anho);
    if (!validacionAnho.esValido) {
        mostrarErrorCampo(anho, validacionAnho.mensaje);
        esValido = false;
    }

    // Validar número de páginas
    const paginas = document.getElementById("<%= txtPaginas.ClientID %>");
    const validacionPaginas = validarNumeroPaginas(paginas);
    if (!validacionPaginas.esValido) {
        mostrarErrorCampo(paginas, validacionPaginas.mensaje);
        esValido = false;
    }

    // Validar idioma
    const idioma = document.getElementById("<%= TextIdioma.ClientID %>");
    const validacionIdioma = validarIdioma(idioma);
    if (!validacionIdioma.esValido) {
        mostrarErrorCampo(idioma, validacionIdioma.mensaje);
        esValido = false;
    }

    // Validar ubicaciones de ejemplares
    const ubicaciones = document.getElementsByName("ubicacion[]");
    const bibliotecas = document.getElementsByName("biblioteca[]");
    for (let i = 0; i < ubicaciones.length; i++) {
        const validacionUbicacion = validarUbicacion(ubicaciones[i]);
        if (!validacionUbicacion.esValido) {
            mostrarErrorCampo(ubicaciones[i], validacionUbicacion.mensaje);
            esValido = false;
        }

        // Validar biblioteca seleccionada
        if (!bibliotecas[i] || !bibliotecas[i].value) {
            bibliotecas[i].classList.add('is-invalid');
            const feedback = document.createElement('div');
            feedback.className = 'invalid-feedback';
            feedback.textContent = 'Debe seleccionar una biblioteca';
            bibliotecas[i].parentNode.appendChild(feedback);
            esValido = false;
        }
    }

    // Validar campos específicos por tipo de material
    const tipoMaterial = document.getElementById("<%= ddlTipoMaterial.ClientID %>").value;

    if (tipoMaterial === 'Libro') {
        // Validar ISBN
        const isbn = document.getElementById("<%= txtISBN.ClientID %>");
        if (isbn) {
            const validacionISBN = validarISBN(isbn);
            if (!validacionISBN.esValido) {
                mostrarErrorCampo(isbn, validacionISBN.mensaje);
                esValido = false;
            }
        }

        // Validar Edición
        const edicion = document.getElementById("<%= txtEdicion.ClientID %>");
        if (edicion) {
            const validacionEdicion = validarCampoGeneral(edicion, 'Edición');
            if (!validacionEdicion.esValido) {
                mostrarErrorCampo(edicion, validacionEdicion.mensaje);
                esValido = false;
            }
        }
    }
    else if (tipoMaterial === 'Tesis') {
        // Validar campos de tesis
        const camposTesis = [
            { id: "<%= txtEspecialidad.ClientID %>", nombre: 'Especialidad' },
            { id: "<%= txtAsesor.ClientID %>", nombre: 'Asesor' },
            { id: "<%= txtGrado.ClientID %>", nombre: 'Grado' },
            { id: "<%= txtInstitucion.ClientID %>", nombre: 'Institución de Publicación' }
        ];

        camposTesis.forEach(campo => {
            const elemento = document.getElementById(campo.id);
            if (elemento) {
                const validacion = validarCampoGeneral(elemento, campo.nombre);
                if (!validacion.esValido) {
                    mostrarErrorCampo(elemento, validacion.mensaje);
                    esValido = false;
                }
            }
        });
    }
    else if (tipoMaterial === 'Articulo') {
        // Validar ISSN
        const issn = document.getElementById("<%= txtISSN.ClientID %>");
        if (issn) {
            const validacionISSN = validarISSN(issn);
            if (!validacionISSN.esValido) {
                mostrarErrorCampo(issn, validacionISSN.mensaje);
                esValido = false;
            }
        }

        // Validar Revista
        const revista = document.getElementById("<%= txtRevista.ClientID %>");
        if (revista) {
            const validacionRevista = validarCampoGeneral(revista, 'Revista');
            if (!validacionRevista.esValido) {
                mostrarErrorCampo(revista, validacionRevista.mensaje);
                esValido = false;
            }
        }

        // Validar Volumen y Número
        const volumen = document.getElementById("<%= txtVolumen.ClientID %>");
        if (volumen) {
            const validacionVolumen = validarNumeroPositivo(volumen, 'Volumen');
            if (!validacionVolumen.esValido) {
                mostrarErrorCampo(volumen, validacionVolumen.mensaje);
                esValido = false;
            }
        }

        const numero = document.getElementById("<%= txtNumero.ClientID %>");
        if (numero) {
            const validacionNumero = validarNumeroPositivo(numero, 'Número');
            if (!validacionNumero.esValido) {
                mostrarErrorCampo(numero, validacionNumero.mensaje);
                esValido = false;
            }
        }
    }

    // Validar que haya al menos un ejemplar
    const ejemplares = document.querySelectorAll('#ejemplares-container .row.mb-3');
    if (ejemplares.length === 0) {
        const modal = new bootstrap.Modal(document.getElementById('modalAdvertencia'));
        modal.show();
        esValido = false;
    }

    return esValido;
}

// ===== FUNCIÓN FALTANTE PARA CONFIGURAR VALIDACIONES =====
function configurarValidacion(elemento, funcionValidacion, nombreCampo = '') {
    if (elemento) {
        elemento.addEventListener('blur', function () {
            let validacion;
            if (nombreCampo) {
                validacion = funcionValidacion(this, nombreCampo);
            } else {
                validacion = funcionValidacion(this);
            }
            mostrarErrorCampo(this, validacion.mensaje);
        });

        elemento.addEventListener('input', function () {
            this.classList.remove('is-invalid');
            const errorLabel = this.parentNode.querySelector('.invalid-feedback');
            if (errorLabel) {
                errorLabel.remove();
            }
            verificarErroresYDeshabilitarBoton();
        });
    }
}