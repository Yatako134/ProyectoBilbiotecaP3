// ===== modalRetraso.js =====

let modalRetrasoInstance = null;

function mostrarModalRetraso(cantidad, prestamos) {

    document.getElementById("lblCantidad").innerText = cantidad;

    let cont = document.getElementById("listaPrestamos");
    cont.innerHTML = "";

    prestamos.forEach(p => {
        cont.innerHTML += `
            <li>
                <b>ID:</b> ${p.idPrestamo} -
                <b>Vence:</b> ${new Date(p.fecha_vencimiento).toLocaleDateString()}
            </li>`;
    });

    const modal = new bootstrap.Modal(document.getElementById("modalRetraso"));
    modal.show();
    modalRetrasoInstance = modal;
}

function cerrarModalRetraso() {
    if (modalRetrasoInstance) {
        modalRetrasoInstance.hide();
    }
}