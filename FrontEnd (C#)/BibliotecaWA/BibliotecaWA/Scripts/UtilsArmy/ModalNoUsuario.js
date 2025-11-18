let modalUsuario = null;

function mostrarModalUsuario(mensaje) {
    document.getElementById("mensajeUsuario").innerText = mensaje;

    modalUsuario = new bootstrap.Modal(document.getElementById("modalUsuario"));
    modalUsuario.show();
}

function cerrarModalUsuario() {
    if (modalUsuario) {
        modalUsuario.hide();
    }
}
