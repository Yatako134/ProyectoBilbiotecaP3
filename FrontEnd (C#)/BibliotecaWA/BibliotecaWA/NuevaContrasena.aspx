<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NuevaContrasena.aspx.cs" Inherits="BibliotecaWA.NuevaContrasena" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Fonts/css/all.css" rel="stylesheet" />
    <link href="Content/site.css" rel="stylesheet" />
    <link href="Fonts/css/custom.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/bootstrap.bundle.js"></script>
    <script src="Scripts/jquery-3.7.1.js"></script>
    <script src="Scripts/activarMenu.js"></script>

    <title>Nueva contraseña</title>
    <script type="text/javascript">
    // Función para validar las contraseñas
    function validatePassword() {
        var password = document.getElementById('<%= txtPassword.ClientID %>').value;
        var confirmPassword = document.getElementById('<%= txtConfirmPassword.ClientID %>').value;
        var passwordError = document.getElementById('<%= lblPasswordError.ClientID %>');
        var confirmPasswordError = document.getElementById('<%= lblConfirmPasswordError.ClientID %>');
            var passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$/; // Al menos 8 caracteres, una mayúscula, una minúscula y un número

            // Validar la contraseña
            if (password === "") {
                passwordError.innerHTML = "La contraseña no puede estar vacía.";
                passwordError.style.display = "block";
                return false;
            } else if (!passwordRegex.test(password)) {
                passwordError.innerHTML = "La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula y un número.";
                passwordError.style.display = "block";
                return false;
            } else {
                passwordError.style.display = "none";
            }

            // Validar que las contraseñas coincidan
            if (confirmPassword !== password) {
                confirmPasswordError.innerHTML = "Las contraseñas no coinciden.";
                confirmPasswordError.style.display = "block";
                return false;
            } else {
                confirmPasswordError.style.display = "none";
            }

            return true;
        }

        // Validar en tiempo real mientras se digita y al quitar el foco
        function setupRealTimeValidation() {
            var passwordField = document.getElementById('<%= txtPassword.ClientID %>');
        var confirmPasswordField = document.getElementById('<%= txtConfirmPassword.ClientID %>');

            passwordField.addEventListener('input', enableChangeButton);
            confirmPasswordField.addEventListener('input', enableChangeButton);
            passwordField.addEventListener('blur', validatePassword);
            confirmPasswordField.addEventListener('blur', validatePassword);
        }

        // Habilitar el botón de cambiar contraseña
        function enableChangeButton() {
            var passwordValid = validatePassword();
            var changeButton = document.getElementById('<%= btnRestablecer.ClientID %>');

            if (passwordValid) {
                changeButton.disabled = false;
                changeButton.classList.remove("disabled");
            } else {
                changeButton.disabled = true;
                changeButton.classList.add("disabled");
            }
        }

        // Ejecutar cuando el documento esté listo
        document.addEventListener('DOMContentLoaded', function () {
            setupRealTimeValidation();
        });
    </script>

    <style>
        /* Estilo para los campos de entrada */
        .form-control {
            font-size: 16px;
        }

        /* Estilo para los mensajes de error */
        #lblPasswordError, #lblConfirmPasswordError {
            color: red;
            font-size: 0.875rem;
            display: none;
        }
    </style>
</head>
<body>
     <div class="container-fluid d-flex p-0 m-0" style="height: 100vh; justify-content: center; align-items: center;">
    <!-- Contenedor del formulario -->
    <div class="card" style="max-width: 400px; width: 100%; padding: 20px;">
        <div class="card-body">
            <h4 class="text-center mb-4">Nueva contraseña</h4>
            <p class="text-center mb-4">Ingrese su nueva contraseña y confírmela para acceder a su cuenta.</p>

            <form id="formCambiar" runat="server">
                <!-- Contraseña -->
                <div class="form-group">
                    <label for="txtPassword">Contraseña</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                    <asp:Label ID="lblPasswordError" runat="server" CssClass="text-danger" Style="display: none;"></asp:Label>
                </div>

                <!-- Repetir Contraseña -->
                <div class="form-group">
                    <label for="txtConfirmPassword">Repetir contraseña</label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" />
                    <asp:Label ID="lblConfirmPasswordError" runat="server" CssClass="text-danger" Style="display: none;"></asp:Label>
                </div>

                <!-- Botón Restablecer -->
                <div class="form-group mt-3 d-flex justify-content-center">
                    <asp:Button ID="btnRestablecer" runat="server" Text="Restablecer" CssClass="btn btn-primary" OnClick="btnRestablecer_Click" Enabled="false" />
                </div>
            </form>
        </div>
    </div>
</div>
</body>
</html>
