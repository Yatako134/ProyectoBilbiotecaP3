<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NuevaContrasena.aspx.cs" Inherits="BibliotecaWA.NuevaContrasena" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Fonts/css/all.css" rel="stylesheet" />
    <link href="Content/site.css" rel="stylesheet" />
    <link href="Fonts/css/custom.css" rel="stylesheet" />
    <link href="Content/site-custom.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/bootstrap.bundle.js"></script>
    <script src="Scripts/jquery-3.7.1.js"></script>
    <title>Nueva contraseña</title>

    <script type="text/javascript">
        var passwordInteracted = false;
        var confirmPasswordInteracted = false;

        // Validar fortaleza de contraseña
        function validatePasswordStrength(password) {
            var regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$/;
            return regex.test(password);
        }

        // Validar contraseña
        function validatePassword() {
            var password = document.getElementById('<%= txtPassword.ClientID %>').value;
            var passwordError = document.getElementById('passwordErrorContainer');
            var passwordField = document.getElementById('<%= txtPassword.ClientID %>');

            if (!passwordInteracted) {
                passwordError.classList.remove("show");
                passwordField.classList.remove("error");
                return false;
            }

            if (password === "") {
                passwordError.innerHTML = "<div class='error-message-content'><i class='fa-solid fa-circle-exclamation'></i><span class='error-text'>La contraseña no puede estar vacía.</span></div>";
                passwordError.classList.add("show");
                passwordField.classList.add("error");
                return false;
            } else if (!validatePasswordStrength(password)) {
                passwordError.innerHTML = "<div class='error-message-content'><i class='fa-solid fa-circle-exclamation'></i><span class='error-text'>La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula y un número.</span></div>";
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

        // Validar confirmación de contraseña
        function validateConfirmPassword() {
            var password = document.getElementById('<%= txtPassword.ClientID %>').value;
            var confirmPassword = document.getElementById('<%= txtConfirmPassword.ClientID %>').value;
            var confirmPasswordError = document.getElementById('confirmPasswordErrorContainer');
            var confirmPasswordField = document.getElementById('<%= txtConfirmPassword.ClientID %>');

            if (!confirmPasswordInteracted) {
                confirmPasswordError.classList.remove("show");
                confirmPasswordField.classList.remove("error");
                return false;
            }

            if (confirmPassword === "") {
                confirmPasswordError.innerHTML = "<div class='error-message-content'><i class='fa-solid fa-circle-exclamation'></i><span class='error-text'>Debe confirmar la contraseña.</span></div>";
                confirmPasswordError.classList.add("show");
                confirmPasswordField.classList.add("error");
                return false;
            } else if (confirmPassword !== password) {
                confirmPasswordError.innerHTML = "<div class='error-message-content'><i class='fa-solid fa-circle-exclamation'></i><span class='error-text'>Las contraseñas no coinciden.</span></div>";
                confirmPasswordError.classList.add("show");
                confirmPasswordField.classList.add("error");
                return false;
            } else {
                confirmPasswordError.classList.remove("show");
                confirmPasswordField.classList.remove("error");
                confirmPasswordField.classList.add("filled");
                return true;
            }
        }

        // Habilitar el botón de restablecer
        function enableRestablecerButton() {
            var passwordValid = validatePassword();
            var confirmPasswordValid = validateConfirmPassword();
            var restablecerButton = document.getElementById('<%= btnRestablecer.ClientID %>');

            if (passwordValid && confirmPasswordValid) {
                restablecerButton.disabled = false;
                restablecerButton.classList.remove("disabled");
            } else {
                restablecerButton.disabled = true;
                restablecerButton.classList.add("disabled");
            }
        }

        // Marcar que el usuario interactuó con el campo de contraseña
        function markPasswordInteracted() {
            passwordInteracted = true;
            validatePassword();
            enableRestablecerButton();
        }

        // Marcar que el usuario interactuó con el campo de confirmación
        function markConfirmPasswordInteracted() {
            confirmPasswordInteracted = true;
            validateConfirmPassword();
            enableRestablecerButton();
        }

        // Validar en tiempo real mientras se digita
        function setupRealTimeValidation() {
            var passwordField = document.getElementById('<%= txtPassword.ClientID %>');
            var confirmPasswordField = document.getElementById('<%= txtConfirmPassword.ClientID %>');

            passwordField.addEventListener('input', function () {
                enableRestablecerButton();

                if (passwordField.value !== "") {
                    passwordInteracted = true;
                    validatePassword();
                }
            });

            confirmPasswordField.addEventListener('input', function () {
                enableRestablecerButton();

                if (confirmPasswordField.value !== "") {
                    confirmPasswordInteracted = true;
                    validateConfirmPassword();
                }
            });

            if (passwordField.value !== "") {
                passwordField.classList.add("filled");
                passwordInteracted = true;
            }
            if (confirmPasswordField.value !== "") {
                confirmPasswordField.classList.add("filled");
                confirmPasswordInteracted = true;
            }

            enableRestablecerButton();
        }

        document.addEventListener('DOMContentLoaded', function () {
            setupRealTimeValidation();
        });
    </script>
</head>
<body>
    <div class="container-fluid d-flex p-0 m-0" style="height: 100vh; justify-content: center; align-items: center; background-color: #f8f9fa;">
        <div class="card shadow-sm" style="max-width: 440px; width: 100%; border-radius: 16px; border: none;">
            <div class="card-body p-4">
                <h4 class="text-center mb-3" style="color: #071437; font-weight: 500;">Nueva contraseña</h4>
                <p class="text-center mb-4" style="color: #4B5675;">Ingrese su nueva contraseña y confírmela para acceder a tu cuenta.</p>

                <form id="formCambiar" runat="server">
                    <!-- Contraseña -->
                    <div class="form-group">
                        <label for="txtPassword">Contraseña</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"
                            onblur="markPasswordInteracted()" oninput="enableRestablecerButton()"></asp:TextBox>
                        <div id="passwordErrorContainer" class="error-message"></div>
                    </div>

                    <!-- Repetir Contraseña -->
                    <div class="form-group">
                        <label for="txtConfirmPassword">Repetir contraseña</label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"
                            onblur="markConfirmPasswordInteracted()" oninput="enableRestablecerButton()"></asp:TextBox>
                        <div id="confirmPasswordErrorContainer" class="error-message"></div>
                    </div>

                    <!-- Botones -->
                    <div class="form-group mt-4 d-flex justify-content-end" style="gap: 8px;">
                        <!-- Botón Cancelar -->
                        <a href="InicioSesion.aspx" class="button-error-secondary">
                            Cancelar
                        </a>
                        
                        <!-- Botón Restablecer -->
                        <asp:Button ID="btnRestablecer" runat="server" Text="Restablecer" 
                            CssClass="button-primary" OnClick="btnRestablecer_Click" Enabled="false" />
                    </div>
                </form>
            </div>
        </div>
    </div>
</body>
</html>