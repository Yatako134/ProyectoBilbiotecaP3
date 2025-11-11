window.addEventListener("DOMContentLoaded", () => {
    const boton = document.getElementById("btnAccion");
    const campos = ["txtNombre", "txtPrimerApellido", "txtSegundoApellido", "txtCorreo", "txtContrasena", "txtTelefono", "txtCodigo", "txtDNI", "ddlRol"];

    function check() {
        const todosLlenos = campos.every(id => {
            const el = document.getElementById(id);
            return el && el.value.trim() !== "";
        });

        if (todosLlenos) {
            boton.classList.remove("btn-disabled");
            boton.classList.add("btn-enabled");
        } else {
            boton.classList.remove("btn-enabled");
            boton.classList.add("btn-disabled");
        }
    }

    campos.forEach(id => {
        const el = document.getElementById(id);
        if (el) {
            el.addEventListener("input", check);
            if (el.tagName === "SELECT") el.addEventListener("change", check);
        }
    });

    check(); // inicializa estado
});