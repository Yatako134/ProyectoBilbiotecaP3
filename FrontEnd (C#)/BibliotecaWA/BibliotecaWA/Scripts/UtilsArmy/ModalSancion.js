function mostrarModalSancion(fecha, motivo) {
    document.getElementById("txtFecha").innerHTML = "Fecha de culminación: " + fecha;
    document.getElementById("txtMotivo").innerHTML = "Motivo de sanción: " + motivo;

    $("#modalSancion").modal("show");
}
