let modalConfirm;

function mostrarModalConfirmacion(nombreUsuario, tituloMaterial) {
    const mensaje = `¿Confirma que desea registrar el préstamo del libro "<strong>${tituloMaterial}</strong>" al usuario "<strong>${nombreUsuario}</strong>"? 
    <br><br>Esta acción guardará los cambios en el sistema.`;

    document.getElementById("mensajeModal").innerHTML = mensaje;

    modalConfirm = new bootstrap.Modal(document.getElementById('confirmModal'));
    modalConfirm.show();
}

function cerrarModalConfirmacion() {
    if (modalConfirm) modalConfirm.hide();
}
