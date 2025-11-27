<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="AdministrarUsuarios.aspx.cs" Inherits="BibliotecaWA.AdministrarUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
    <link href="Fonts/css/AdminUsuarios.css" rel="stylesheet" />
    <style>
        .readonly { background-color: #e9ecef; }
        .header { display: flex; align-items: center; margin-bottom: 20px; }
        .header span { margin-left: 10px; font-weight: bold; font-size: 24px; }
        .form-group { margin-bottom: 15px; }
        .btn-right { float: right; margin-top: 20px; }
    </style>

    <style>
        .inactivo {
            pointer-events: none; /* desactiva clics */
            opacity: 0.5; /* se ve deshabilitado */
            cursor: not-allowed;
        }
</style>

    <script>
        function soloLetras(e) {
            var key = e.key;
            var input = e.target;

            // Permite solo letras, espacios, tildes, guiones y apóstrofes
            if (!/^[A-Za-zÁÉÍÓÚáéíóúÑñ \-']$/.test(key)) {
                e.preventDefault();
                return;
            }

        }
    </script>

    <script>
        function soloEnteros(e) {
            var key = e.key;

            // Solo permitir números del 0 al 9
            if (!/^[0-9]$/.test(key)) {
                e.preventDefault();
            }
        }
    </script>

    <script>
        function actualizarEstadoBoton() {

            // Forzar validación del ValidationGroup antes de revisar
            if (typeof (Page_ClientValidate) === "function") {
                Page_ClientValidate('vgUsuario');
            }

            var esValido = true;

            // Recorremos los validators del grupo
            if (typeof (Page_Validators) !== "undefined") {
                for (var i = 0; i < Page_Validators.length; i++) {

                    // Solo validar los de este ValidationGroup
                    if (Page_Validators[i].validationGroup === "vgUsuario") {

                        if (!Page_Validators[i].isvalid) {
                            esValido = false;
                            break;
                        }
                    }
                }
            }

            // Cambiar el estado del botón
            document.getElementById('btnAccion').classList.toggle("inactivo", !esValido);
        }

        // Ejecutar en cada cambio del formulario
        document.addEventListener("input", actualizarEstadoBoton);
        document.addEventListener("change", actualizarEstadoBoton);

        // Ejecutar al cargar la página (para dejar el botón deshabilitado)
        window.onload = actualizarEstadoBoton;
    </script>

    <style>
        /* Si dentro metes <ul>, esto las centra también */
        #cuerpoError ul {
            text-align: center;
            list-style-position: inside;
        }
    </style>
    <script>

        // LIMPIAR ERROR
        function limpiarError(campo) {
            campo.classList.remove('is-invalid');
            campo.classList.remove('is-valid');

            const errorLabel = campo.parentNode.querySelector('.invalid-feedback');
            if (errorLabel) errorLabel.remove();
        }

        // VALIDAR NOMBRE / APELLIDOS
        function validarCampoNombreApellido(campo, nombreCampo) {
            const valor = campo.value.trim();

            if (valor === '') {
                return { mensaje: `El campo ${nombreCampo} es requerido` };
            }

            if (valor.length < 2) {
                return { mensaje: `El campo ${nombreCampo} debe tener al menos 2 caracteres` };
            }


            return { mensaje: "" }; // SIN ERRORES
        }

        function validarContrasena(campo) {
            const valor = campo.value.trim();

            // 1) Longitud mínima
            if (valor.length < 8) {
                return { mensaje: "La contraseña debe tener al menos 8 caracteres" };
            }

            // 2) Letra minúscula
            if (!/[a-z]/.test(valor)) {
                return { mensaje: "La contraseña debe contener al menos una letra minúscula" };
            }

            // 3) Letra mayúscula
            if (!/[A-Z]/.test(valor)) {
                return { mensaje: "La contraseña debe contener al menos una letra mayúscula" };
            }

            // 4) Al menos 2 números
            const numeros = valor.match(/\d/g);
            if (!numeros || numeros.length < 2) {
                return { mensaje: "La contraseña debe contener al menos 2 números" };
            }

            // 5) Al menos 1 caracter especial
            const regexEspecial = /[!@#$%\*\_\-\+=]/;
            if (!regexEspecial.test(valor)) {
                return { mensaje: "La contraseña debe contener al menos un carácter especial (!, @, #, $, %, *, _, -, +, =)" };
            }

            return { mensaje: "" }; // SIN ERRORES
        }

        function validarCorreoPUCP(campo) {
            const valor = campo.value.trim();

            if (valor === "") {
                return { mensaje: "El correo es requerido" };
            }

            // Expresión regular para: cualquier texto válido antes de @, luego exactamente @pucp.edu.pe
            const regexPUCP = /^[a-zA-Z0-9._%+-]+@pucp\.edu\.pe$/;

            if (!regexPUCP.test(valor)) {
                return { mensaje: "El correo debe tener el formato usuario@pucp.edu.pe" };
            }

            return { mensaje: "" }; // SIN ERRORES
        }

        function validarDNI(campo) {
            const valor = campo.value.trim();

            if (valor === "") {
                return { mensaje: "El DNI es requerido" };
            }

            // Debe tener exactamente 8 dígitos
            if (valor.length !== 8) {
                return { mensaje: "El DNI debe tener exactamente 8 dígitos" };
            }

            return { mensaje: "" }; // SIN ERRORES
        }

        function validarCodigo(campo) {
            const valor = campo.value.trim();

            if (valor === "") {
                return { mensaje: "El Codigo es requerido" };
            }

            // Debe tener exactamente 8 dígitos
            if (valor.length !== 8) {
                return { mensaje: "El Codigo debe tener exactamente 8 dígitos" };
            }

            return { mensaje: "" }; // SIN ERRORES
        }

        function validarTelefono(campo, nombreCampo) {
            const valor = campo.value.trim();

            // Validar requerido
            if (valor === '') {
                return { mensaje: `El campo ${nombreCampo} es requerido` };
            }

            // Mínimo 9
            if (valor.length < 9) {
                return { mensaje: `El campo ${nombreCampo} debe tener al menos 9 dígitos` };
            }

            return { mensaje: "" }; // SIN ERRORES
        }



        // MOSTRAR ERROR
        function mostrarErrorCampo(campo, mensaje) {
            limpiarError(campo);

            if (mensaje) {
                campo.classList.add('is-invalid');

                const feedback = document.createElement('div');
                feedback.className = 'invalid-feedback';
                feedback.textContent = mensaje;
                campo.parentNode.appendChild(feedback);
            } else {
                campo.classList.add('is-valid');
            }

            verificarErroresYDeshabilitarBoton();
        }

        // BOTÓN
        function verificarErroresYDeshabilitarBoton() {
            const errores = document.querySelectorAll('.is-invalid');
            const boton = document.getElementById("<%= btnAccion.ClientID %>");

            if (!boton) return;

            if (errores.length > 0) {
                boton.disabled = true;
                boton.classList.remove('btn-primary');
                boton.classList.add('btn-secondary');
            } else {
                boton.disabled = false;
                boton.classList.remove('btn-secondary');
                boton.classList.add('btn-primary');
            }
        }

        // CONFIGURAR VALIDACIÓN
        function configurarValidacion(elemento, funcionValidacion, nombreCampo) {
            if (!elemento) return;

            elemento.addEventListener('blur', function () {
                const resultado = funcionValidacion(this, nombreCampo);
                mostrarErrorCampo(this, resultado.mensaje);
            });

            elemento.addEventListener('input', function () {
                limpiarError(this);
                verificarErroresYDeshabilitarBoton();
            });
        }

        // ASIGNAR EVENTOS
        function configurarEventListeners() {
            configurarValidacion(document.getElementById("<%= txtNombre.ClientID %>"), validarCampoNombreApellido, "Nombre");
            configurarValidacion(document.getElementById("<%= txtPrimerApellido.ClientID %>"), validarCampoNombreApellido, "Primer apellido");
            configurarValidacion(document.getElementById("<%= txtSegundoApellido.ClientID %>"), validarCampoNombreApellido, "Segundo apellido");
            configurarValidacion(document.getElementById("<%= txtContrasena.ClientID %>"), validarContrasena);
            configurarValidacion(document.getElementById("<%= txtCorreo.ClientID %>"), validarCorreoPUCP, "Correo");
            configurarValidacion(document.getElementById("<%= txtDNI.ClientID %>"), validarDNI, "DNI");
            configurarValidacion(document.getElementById("<%= txtCodigo.ClientID %>"), validarCodigo, "Codigo");
            configurarValidacion(document.getElementById("<%= txtTelefono.ClientID %>"), validarTelefono, "Telefono");

        }

        document.addEventListener("DOMContentLoaded", function () {

            // 🔹 Desactivar botón al empezar
            const boton = document.getElementById("<%= btnAccion.ClientID %>");
            if (boton) {
                boton.disabled = true;
                boton.classList.remove('btn-primary');
                boton.classList.add('btn-secondary');
            }

            configurarEventListeners();
        });

    </script>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <div class="d-flex align-items-center mb-4">
    <i class="fas fa-th" style="margin-right: 10px; font-size: 24px;"></i>
    <div style="border-right: 2px solid #ccc; height: 24px; margin-right: 10px;"></div>
    <div class="me-3"> <p1>Gestión de usuarios</p1> </div>
    <i class="fa-solid fa-greater-than me-3 fa-xs"></i>
    <asp:Label ID="LblGuia" runat="server" CssClass="fw-bold" ClientIDMode="Static"></asp:Label>
</div>
    <hr>
    <div class="container-fluid mt-4">

        <!-- Título con flecha de regreso -->

        <div class="d-flex align-items-center mb-4">
            <asp:LinkButton ID="btnRegresar" runat="server" CssClass="btn btn-link text-dark fs-4 me-3"
                OnClick="btnRegresar_Click" ToolTip="Regresar">
                <i class="fa-solid fa-arrow-left"></i>
            </asp:LinkButton>
            <asp:Label ID="lblTitulo" runat="server" CssClass="h3 m-0" ClientIDMode="Static"></asp:Label>
        </div>

        <div class="custom-form-container shadow-sm overflow-hidden mx-auto">
            <!-- Campos principales -->
            <div class="row g-3">
                <div class="col-md-4">
                    <label for="ddlRol" class="form-label-admiUsuarios">Tipo de usuario</label>
                    <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select form-input admiUsuarios-input" ClientIDMode="Static"></asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <label for="txtCodigo" class="form-label-admiUsuarios">Código</label>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control form-input admiUsuarios-input" ClientIDMode="Static" MaxLength="8" onkeypress="soloEnteros(event)"></asp:TextBox>

                </div>
                <div class="col-md-4">
                    <label for="txtDNI" class="form-label-admiUsuarios">DNI</label>
                    <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control form-input admiUsuarios-input" ClientIDMode="Static" MaxLength="8" onkeypress="soloEnteros(event)"></asp:TextBox>
                </div>
            </div>

            <!-- Campos uno debajo del otro -->
            <div class="row g-0 mt-3">
                <div class="col-md-12">
                    <label for="txtNombre" class="form-label-admiUsuarios">Nombre(s)</label>
                    <asp:TextBox ID="txtNombre" runat="server"
                        CssClass="form-control form-input admiUsuarios-input"
                        ClientIDMode="Static" onkeypress="soloLetras(event)"></asp:TextBox>
                </div>
                <div class="col-md-12">
                    <label for="txtPrimerApellido" class="form-label-admiUsuarios">Primer Apellido</label>
                    <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="form-control form-input admiUsuarios-input" MaxLength="50" ClientIDMode="Static" onkeypress="soloLetras(event)"></asp:TextBox>

                </div>
                <div class="col-md-12">
                    <label for="txtSegundoApellido" class="form-label-admiUsuarios">Segundo Apellido</label>
                    <asp:TextBox ID="txtSegundoApellido" runat="server" CssClass="form-control form-input admiUsuarios-input" MaxLength="50" ClientIDMode="Static" onkeypress="soloLetras(event)"></asp:TextBox>
                </div>
                <div class="col-md-12">
                    <label for="txtCorreo" class="form-label-admiUsuarios">Correo</label>
                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control form-input admiUsuarios-input" MaxLength="100" ClientIDMode="Static"></asp:TextBox>

                </div>

                <div class="col-md-12">
                    <label for="txtContrasena" class="form-label-admiUsuarios">Contraseña</label>
                    <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control form-input admiUsuarios-input"  MaxLength="40" ClientIDMode="Static"></asp:TextBox>


                </div>
                <div class="col-md-12">
                    <label for="txtTelefono" class="form-label-admiUsuarios">Teléfono</label>
                    <asp:TextBox ID="txtTelefono" runat="server"  MaxLength="12" CssClass="form-control form-input admiUsuarios-input" ClientIDMode="Static" onkeypress="soloEnteros(event)"></asp:TextBox>
                </div>
            </div>

            <!-- HiddenField para ID -->
            <asp:HiddenField ID="hfIdUsuarioSeleccionado" runat="server" ClientIDMode="Static" />
        </div>

        <!-- Botón de acción -->
        <div class="d-flex justify-content-end mt-4">
            <asp:Button ID="btnAccion" runat="server" CssClass="btn btn-primary inactivo"
                ClientIDMode="Static"
                ValidationGroup="vgUsuario"
                CausesValidation="true"
                 OnClick="btnAccion_Click" />
        </div>

        <!-- Modal centrado -->
        <div class="modal fade" id="confirmModal" tabindex="-1" aria-labelledby="confirmModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="confirmModalLabel">Confirmar registro de usuario</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                    </div>
                    <div class="modal-body text-center">
                        <asp:Label ID="lblModalMensaje" runat="server" ClientIDMode="Static"></asp:Label>
                    </div>
                    <div class="modal-footer justify-content-center">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <button type="button" class="btn btn-primary" onclick="document.getElementById('btnAccionServer').click();">Registrar usuario</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- MODAL DE ERROR -->
        <div class="modal fade" id="modalError" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">

                    <div class="modal-header bg-danger text-white">
                        <h5 class="modal-title text-center w-100" id="tituloError"></h5>
                    </div>

                    <div class="modal-body text-center" id="cuerpoError">
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    </div>

                </div>
            </div>
        </div>

        <!-- Botón hidden para disparar el evento en server -->
        <asp:Button ID="btnAccionServer" ValidationGroup="vgUsuario" CausesValidation="true" runat="server" OnClick="btnAccion_Click" Style="display:none" ClientIDMode="Static" />  
            
        <script>
            // Activa el botón cuando lo necesites
            function activarBoton() {
                let btn = document.getElementById("btnAccion");

                // quitar estado inactivo
                btn.classList.remove("inactivo");

                // permitir modal automáticamente
                btn.setAttribute("data-bs-toggle", "modal");
                btn.setAttribute("data-bs-target", "#confirmModal");

            }
        </script>
  
        <script>
            function actualizarModal() {

                let nombre = document.getElementById("txtNombre").value.trim();
                let apellido1 = document.getElementById("txtPrimerApellido").value.trim();
                let apellido2 = document.getElementById("txtSegundoApellido").value.trim();

                let nombreCompleto = nombre + " " + apellido1 + " " + apellido2;

                // Llenar el mensaje dentro del Label ASP.NET
                document.getElementById("lblModalMensaje").innerHTML =
                    "¿Confirma que desea registrar el usuario '<strong>" + nombreCompleto + "</strong>'? Esta acción guadará los datos en el sistema.";

                // Activar atributos del modal
                let btn = document.getElementById("btnAccion");
                btn.setAttribute("data-bs-toggle", "modal");
                btn.setAttribute("data-bs-target", "#confirmModal");

                return false; // prevenir postback automático
            }
        </script>

        <script>
            function mostrarError(titulo, mensaje) {
                document.getElementById("tituloError").innerText = titulo;
                document.getElementById("cuerpoError").innerHTML = mensaje;
                var modal = new bootstrap.Modal(document.getElementById("modalError"));
                modal.show();
            }
        </script>
    </div>
</asp:Content>
