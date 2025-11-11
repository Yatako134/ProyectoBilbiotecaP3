function abrirMenuOpciones(idUsuario, elemento) {
    // Referencias a los elementos
    var menu = document.getElementById("menuOpciones");
    var hiddenField = document.getElementById("hfIdUsuarioSeleccionado");

    // 🔍 Verificación (mira la consola F12)
    console.log("🟩 MENÚ:", menu);
    console.log("🟩 HiddenField:", hiddenField);

    if (!menu || !hiddenField) {
        console.error("❌ No se encontró el menú o el campo oculto. Revisa los IDs o el ClientIDMode.");
        return;
    }

    // Guardar el ID del usuario
    hiddenField.value = idUsuario;

    // Obtener la posición del botón que se clicó
    var rect = elemento.getBoundingClientRect();

    // Posicionar el menú al lado derecho
    menu.style.position = "absolute";
    menu.style.top = (rect.top + window.scrollY) + "px";
    menu.style.left = (rect.right + window.scrollX + 10) + "px";
    menu.style.display = "block";
    menu.style.zIndex = "9999"; // para que no se oculte detrás del GridView

    // Cerrar el menú si se hace clic fuera
    document.addEventListener("click", function cerrarMenu(e) {
        if (!menu.contains(e.target) && e.target !== elemento) {
            menu.style.display = "none";
            document.removeEventListener("click", cerrarMenu);
        }
    });
}