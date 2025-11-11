let modalPrestamoExitoso;

function mostrarModalPrestamoExitoso(codigo) {
    document.getElementById("codigoPrestamo").innerHTML = `✅ Código de Préstamo: <strong>${codigo}</strong>`;
    modalPrestamoExitoso = new bootstrap.Modal(document.getElementById('modalPrestamoExitoso'));
    modalPrestamoExitoso.show();
}

document.getElementById("btnIrPrestamo").addEventListener("click", function () {
    // Aquí podrías redirigir a la página del préstamo
    window.location.href = "Prestamo.aspx";
});
