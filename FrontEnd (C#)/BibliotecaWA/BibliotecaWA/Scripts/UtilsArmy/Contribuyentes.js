
let modalContribuyentes;

function mostrarModalContribuyentes() {
    modalContribuyentes = new bootstrap.Modal(document.getElementById('modalContribuyentes'));
    modalContribuyentes.show();
}

function cerrarModalContribuyentes() {
    if (modalContribuyentes) modalContribuyentes.hide();
}
