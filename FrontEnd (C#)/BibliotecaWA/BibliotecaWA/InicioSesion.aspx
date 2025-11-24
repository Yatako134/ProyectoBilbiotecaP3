<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioSesion.aspx.cs" Inherits="BibliotecaWA.InicioSesion" %>

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

    <title>Inicio de sesión - Sistema de Bibliotecas</title>

    <script type="text/javascript">
        // Variables para rastrear si los campos han sido interactuados
        var emailInteracted = false;
        var passwordInteracted = false;
        var isCredentialError = false;

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
            var email = document.getElementById('<%= txtUsername.ClientID %>').value.trim();

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
            if (isCredentialError) {
                return false;
            }

            var email = document.getElementById('<%= txtUsername.ClientID %>').value.trim();
            var emailError = document.getElementById('emailErrorContainer');
            var emailField = document.getElementById('<%= txtUsername.ClientID %>');

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
                emailError.innerHTML = "<div class='error-message-content'><i class='fa-solid fa-circle-exclamation'></i><span class='error-text'>El formato del correo no es válido. Verifica que tenga un formato correcto.</span></div>";
                emailError.classList.add("show");
                emailField.classList.add("error");
                return false;
            }

            var parts = email.split('@');
            var localPart = parts[0];
            var domain = parts[1];

            if (!isValidLocalPart(localPart)) {
                emailError.innerHTML = "<div class='error-message-content'><i class='fa-solid fa-circle-exclamation'></i><span class='error-text'>El formato del correo no es válido. Verifica que tenga un formato correcto.</span></div>";
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

        // Validar la contraseña (para habilitar botón)
        function validatePasswordForButton() {
            var password = document.getElementById('<%= txtPassword.ClientID %>').value;
            return password !== "";
        }

        // Validar la contraseña (para mostrar errores)
        function validatePassword() {
            if (isCredentialError) {
                return false;
            }

            var password = document.getElementById('<%= txtPassword.ClientID %>').value;
            var passwordError = document.getElementById('passwordErrorContainer');
            var passwordField = document.getElementById('<%= txtPassword.ClientID %>');

            if (!passwordInteracted) {
                passwordError.classList.remove("show");
                passwordField.classList.remove("error");
                return false;
            }

            if (password === "") {
                passwordError.innerHTML = "<div class='error-message-content'><i class='fa-solid fa-circle-exclamation'></i><span class='error-text'>Debe ingresar su contraseña.</span></div>";
                passwordError.classList.add("show");
                passwordField.classList.add("error");
                return false;
            } else {
                passwordError.classList.remove("show");
                passwordField.classList.remove("error");
                passwordField.classList.add("filled");
                return true;
            }
        }

        // Habilitar el botón de login
        function enableLoginButton() {
            var emailValid = validateEmailForButton();
            var passwordValid = validatePasswordForButton();
            var loginButton = document.getElementById('<%= btnLogin.ClientID %>');

            if (emailValid && passwordValid && !isCredentialError) {
                loginButton.disabled = false;
                loginButton.classList.remove("disabled");
            } else {
                loginButton.disabled = true;
                loginButton.classList.add("disabled");
            }
        }

        // Marcar que el usuario interactuó con el campo de email
        function markEmailInteracted() {
            emailInteracted = true;
            validateEmail();
            enableLoginButton();
        }

        // Marcar que el usuario interactuó con el campo de contraseña
        function markPasswordInteracted() {
            passwordInteracted = true;
            validatePassword();
            enableLoginButton();
        }

        // Mostrar error de credenciales
        function showCredentialError() {
            var emailField = document.getElementById('<%= txtUsername.ClientID %>');
            var passwordField = document.getElementById('<%= txtPassword.ClientID %>');
            var passwordError = document.getElementById('passwordErrorContainer');
            var emailError = document.getElementById('emailErrorContainer');
            
            emailError.classList.remove("show");
            
            isCredentialError = true;
            passwordError.innerHTML = "<div class='error-message-content'><i class='fa-solid fa-circle-exclamation'></i><span class='error-text'>Credenciales incorrectas. Inténtalo nuevamente.</span></div>";
            passwordError.classList.add("show");
            
            emailField.classList.add("error");
            passwordField.classList.add("error");
            
            passwordInteracted = true;
            emailInteracted = true;
            
            enableLoginButton();
        }

        // Limpiar error de credenciales cuando el usuario empiece a escribir
        function clearCredentialError() {
            if (isCredentialError) {
                isCredentialError = false;
                var emailField = document.getElementById('<%= txtUsername.ClientID %>');
                var passwordField = document.getElementById('<%= txtPassword.ClientID %>');
                var passwordError = document.getElementById('passwordErrorContainer');
                
                emailField.classList.remove("error");
                passwordField.classList.remove("error");
                passwordError.classList.remove("show");
                
                enableLoginButton();
            }
        }

        // Validar en tiempo real mientras se digita
        function setupRealTimeValidation() {
            var emailField = document.getElementById('<%= txtUsername.ClientID %>');
            var passwordField = document.getElementById('<%= txtPassword.ClientID %>');

            emailField.addEventListener('input', function() {
                clearCredentialError();
                enableLoginButton();
                
                if (emailField.value !== "") {
                    emailInteracted = true;
                    validateEmail();
                }
            });
            
            passwordField.addEventListener('input', function() {
                clearCredentialError();
                enableLoginButton();
                
                if (passwordField.value !== "") {
                    passwordInteracted = true;
                    validatePassword();
                }
            });
            
            if (emailField.value !== "") {
                emailField.classList.add("filled");
                emailInteracted = true;
            }
            if (passwordField.value !== "") {
                passwordField.classList.add("filled");
                passwordInteracted = true;
            }
            
            var hfError = document.getElementById('<%= hfCredentialError.ClientID %>');
            if (hfError && hfError.value === "true") {
                showCredentialError();
                hfError.value = "";
            }

            enableLoginButton();
        }

        document.addEventListener('DOMContentLoaded', function () {
            setupRealTimeValidation();
        });
    </script>
</head>
<body>
    <div class="container-fluid d-flex p-0 m-0" style="height: 100vh; padding: 20px;">
        <!-- Contenedor de la imagen (lado izquierdo) -->
        <div class="col-md-6 d-none d-md-block img-container" style="padding: 20px;">
            <img src="Images\portada.png" alt="Imagen de portada" style="width: 100%; height: 100%; object-fit: cover; border-radius: 16px;">
        </div>

        <!-- Contenedor del formulario (lado derecho) -->
        <div class="col-md-6 d-flex justify-content-center align-items-center form-container" style="padding-left: 80px; padding-right: 80px;">
            <form id="formLogin" runat="server" class="w-100">

                <div class="text-center mb-4">
                    <h2 style="font-weight: 500;">¡Bienvenido al Sistema de Bibliotecas Universitario!</h2>
                </div>
                <div class="text-center mb-4">
                    <p>Ingresa tus credenciales para acceder a tu cuenta.</p>
                </div>

                <!-- Campo de correo electrónico -->
                <div class="form-group">
                    <label for="txtUsername">Correo institucional</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"
                        onblur="markEmailInteracted()" oninput="enableLoginButton()"></asp:TextBox>
                    <div id="emailErrorContainer" class="error-message"></div>
                </div>

                <!-- Campo de contraseña -->
                <div class="form-group mt-3">
                    <label for="txtPassword">Contraseña</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"
                            onblur="markPasswordInteracted()" oninput="enableLoginButton()"></asp:TextBox>
                    </div>
                    <div id="passwordErrorContainer" class="error-message"></div>
                </div>

                <!-- Campo oculto para comunicar error de credenciales desde el servidor -->
                <asp:HiddenField ID="hfCredentialError" runat="server" Value="" />

                <div class="form-group mt-3 text-start">
                    <button type="button" class="btn btn-link p-0" onclick="window.location.href='RestablecerContrasena.aspx'">¿Olvidaste tu contraseña?</button>
                </div>

                <!-- Botón de inicio de sesión con la nueva clase button-primary -->
                <div class="form-group mt-3 d-flex justify-content-center">
                    <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión" CssClass="button-primary" OnClick="btnLogin_Click" Enabled="false" />
                </div>
            </form>
        </div>
    </div>
</body>
</html>