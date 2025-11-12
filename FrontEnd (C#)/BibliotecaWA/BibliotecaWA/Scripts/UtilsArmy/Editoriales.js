
let modalEditoriales;

function mostrarModalEditoriales() {
    modalEditoriales = new bootstrap.Modal(document.getElementById('modalEditoriales'));
    modalEditoriales.show();
}

function cerrarModalEditoriales() {
    if (modalEditoriales) modalEditoriales.hide();
}
