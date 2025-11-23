<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RestablecerContrasena.aspx.cs" Inherits="BibliotecaWA.RestablecerContrasena" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Fonts/css/all.css" rel="stylesheet" />
    <link href="Content/site.css" rel="stylesheet" />
    <link href="Fonts/css/custom.css" rel="stylesheet" />
    <link href="Content/site-custom.css" rel="stylesheet" />
    
    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/bootstrap.bundle.js"></script>
    <script src="Scripts/jquery-3.7.1.js"></script>
    <script src="Scripts/activarMenu.js"></script>
    
    <title>Restablecer contraseña</title>

    <script type="text/javascript">
        // Validar formato básico del correo
        function isValidEmailFormat(email) {
            var basicEmailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            return basicEmailRegex.test(email);
        }

        // Validar caracteres permitidos en la parte local (antes del @)
        function isValidLocalPart(localPart) {
            var localPartRegex = /^[a-zA-Z0-9][a-zA-Z0-9._%+-]*[a-zA-Z0-9]$/;
            var invalidChars = /[!#$&*()=\[\]{}|:;<>\/]/;

            return localPartRegex.test(localPart) && !invalidChars.test(localPart) &&
                !localPart.startsWith('.') && !localPart.endsWith('.') &&
                !localPart.startsWith('-') && !localPart.endsWith('-');
        }

        // Validar dominio específico
        function isValidDomain(domain) {
            return domain === "pucp.edu.pe";
        }

        // Validar el correo completo (para habilitar botón)
        function validateEmailForButton() {
            var email = document.getElementById('<%= txtEmail.ClientID %>').value.trim();

            if (email === "") return false;
            if (!isValidEmailFormat(email)) return false;

            var parts = email.split('@');
            var localPart = parts[0];
            var domain = parts[1];

            if (!isValidLocalPart(localPart)) return false;
            if (!isValidDomain(domain)) return false;

            return true;
        }

        // Validar el correo completo (para mostrar errores)
        function validateEmail() {
            var email = document.getElementById('<%= txtEmail.ClientID %>').value.trim();
            var emailError = document.getElementById('emailErrorContainer');
            var emailField = document.getElementById('<%= txtEmail.ClientID %>');

            if (!emailInteracted) {
                emailError.classList.remove("show");
                emailField.classList.remove("error");
                return false;
            }

            if (email === "") {
                emailError.innerHTML = "<div class='error-message-content'><i class='fa-solid fa-circle-exclamation'></i><span class='error-text'>Debe ingresar su correo electrónico.</span></div>";
                emailError.classList.add("show");
                emailField.classList.add("error");
                return false;
            }

            if (!isValidEmailFormat(email)) {
                emailError.innerHTML = "<div class='error-message-content'><i class='fa-solid fa-circle-exclamation'></i><span class='error-text'>El formato del correo no es válido. Verifica que tenga un formato correcto (ejemplo: usuario@dominio.com)</span></div>";
                emailError.classList.add("show");
                emailField.classList.add("error");
                return false;
            }

            var parts = email.split('@');
            var localPart = parts[0];
            var domain = parts[1];

            if (!isValidLocalPart(localPart)) {
                emailError.innerHTML = "<div class='error-message-content'><i class='fa-solid fa-circle-exclamation'></i><span class='error-text'>El formato del correo no es válido. No se permiten espacios ni caracteres especiales como !, #, $, %, &, *, etc.</span></div>";
                emailError.classList.add("show");
                emailField.classList.add("error");
                return false;
            }

            if (!isValidDomain(domain)) {
                emailError.innerHTML = "<div class='error-message-content'><i class='fa-solid fa-circle-exclamation'></i><span class='error-text'>Debe ser un correo institucional @pucp.edu.pe</span></div>";
                emailError.classList.add("show");
                emailField.classList.add("error");
                return false;
            }

            emailError.classList.remove("show");
            emailField.classList.remove("error");
            emailField.classList.add("filled");
            return true;
        }

        // Habilitar el botón de continuar
        function enableContinueButton() {
            var emailValid = validateEmailForButton();
            var continueButton = document.getElementById('<%= btnContinue.ClientID %>');

            if (emailValid) {
                continueButton.disabled = false;
                continueButton.classList.remove("disabled");
            } else {
                continueButton.disabled = true;
                continueButton.classList.add("disabled");
            }
        }

        // Marcar que el usuario interactuó con el campo de email
        function markEmailInteracted() {
            emailInteracted = true;
            validateEmail();
            enableContinueButton();
        }

        // Variables para rastrear si los campos han sido interactuados
        var emailInteracted = false;

        // Validar en tiempo real mientras se digita
        function setupRealTimeValidation() {
            var emailField = document.getElementById('<%= txtEmail.ClientID %>');

            emailField.addEventListener('input', function () {
                enableContinueButton();

                if (emailField.value !== "") {
                    emailInteracted = true;
                    validateEmail();
                }
            });

            if (emailField.value !== "") {
                emailField.classList.add("filled");
                emailInteracted = true;
            }

            enableContinueButton();
        }

        // Ejecutar cuando el documento esté listo
        document.addEventListener('DOMContentLoaded', function () {
            setupRealTimeValidation();
        });
    </script>

</head>
<body>
    <div class="container-fluid d-flex p-0 m-0" style="height: 100vh; justify-content: center; align-items: center; background-color: #f8f9fa;">
        <!-- Contenedor del formulario con bordes redondeados -->
        <div class="card shadow-sm" style="max-width: 440px; width: 100%; border-radius: 16px; border: none;">
            <div class="card-body p-4">
                <h4 class="text-center mb-3" style="color: #071437; font-weight: 500;">Restablecer Contraseña</h4>
                <p class="text-center mb-4" style="color: #4B5675;">Ingresa tu correo institucional registrado para continuar con el proceso.</p>

                <form id="formRestablecer" runat="server">
                    <!-- Campo de correo electrónico -->
                    <div class="form-group">
                        <label for="txtEmail" style="color: #071437; margin-bottom: 4px;">Correo institucional</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" 
                            onblur="markEmailInteracted()" oninput="enableContinueButton()"></asp:TextBox>
                        <div id="emailErrorContainer" class="error-message"></div>
                    </div>

                    <!-- Botones alineados a la derecha con espacio de 8px -->
                    <div class="form-group mt-4 d-flex justify-content-end" style="gap: 8px;">
                        <!-- Botón Cancelar (secundario) -->
                        <a href="InicioSesion.aspx" class="btn btn-cancel-secondary d-flex align-items-center">
                            <i class="fa-solid fa-arrow-left me-2"></i>Cancelar
                        </a>
                        
                        <!-- Botón Continuar -->
                        <asp:Button ID="btnContinue" runat="server" Text="Continuar" 
                            CssClass="btn btn-primary" OnClick="btnContinue_Click" Enabled="false" />
                    </div>
                </form>
            </div>
        </div>
    </div>
</body>
</html>