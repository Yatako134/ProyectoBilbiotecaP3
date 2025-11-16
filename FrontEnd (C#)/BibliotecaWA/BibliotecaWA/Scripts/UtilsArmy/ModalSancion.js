function mostrarModalSancion(fecha, motivo) {
    document.getElementById("txtFecha").innerHTML = "Fecha de culminación: " + fecha;
    document.getElementById("txtMotivo").innerHTML = "Motivo de sanción: " + motivo;

    // Bootstrap 5 puro
    var modalSancion = new bootstrap.Modal(document.getElementById('modalSancion'));
    modalSancion.show();
}
