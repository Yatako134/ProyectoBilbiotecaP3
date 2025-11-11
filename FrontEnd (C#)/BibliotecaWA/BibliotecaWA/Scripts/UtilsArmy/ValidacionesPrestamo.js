let modalAlerta;

// Función general para mostrar cualquier mensaje de alerta
function mostrarAlerta(mensaje, tipo = "error") {
    // Cambiar color del encabezado según tipo
    const header = document.querySelector("#alertModal .modal-header");
    const titulo = document.getElementById("alertModalLabel");
    const cuerpo = document.getElementById("mensajeAlerta");

    cuerpo.innerHTML = mensaje;

    if (tipo === "error") {
        header.className = "modal-header bg-danger text-white";
        titulo.innerText = "⚠️ Alerta";
    } else if (tipo === "warning") {
        header.className = "modal-header bg-warning text-dark";
        titulo.innerText = "⚠️ Advertencia";
    } else if (tipo === "success") {
        header.className = "modal-header bg-success text-white";
        titulo.innerText = "✅ Éxito";
    }

    modalAlerta = new bootstrap.Modal(document.getElementById('alertModal'));
    modalAlerta.show();
}

// Alias para las funciones específicas que invocas desde el servidor
function alertaUsuario() {
    mostrarAlerta("Debe seleccionar un usuario antes de registrar el préstamo.", "warning");
}

function alertaLimite(nombre, limite) {
    mostrarAlerta(`El usuario <strong>${nombre}</strong> ya alcanzó su límite de préstamos (${limite}).`, "error");
}

function alertaEjemplar() {
    mostrarAlerta("No hay ejemplares disponibles para este material.", "error");
}