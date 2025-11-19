<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioSesion.aspx.cs" Inherits="BibliotecaWA.InicioSesion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Fonts/css/all.css" rel="stylesheet" />
    <link href="Content/site.css" rel="stylesheet" />
    <link href="Fonts/css/custom.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/bootstrap.bundle.js"></script>
    <script src="Scripts/jquery-3.7.1.js"></script>
    <script src="Scripts/activarMenu.js"></script>

    <title>Inicio de sesion</title>

    <script type="text/javascript">
        // Variables para rastrear si los campos han sido interactuados
        var emailInteracted = false;
        var passwordInteracted = false;

        // Validar el correo
        function validateEmail() {
            var email = document.getElementById('<%= txtUsername.ClientID %>').value;
        var emailError = document.getElementById('<%= lblEmailError.ClientID %>');
            var emailRegex = /^[a-zA-Z0-9._%+-]+@pucp\.edu\.pe$/; // Solo correos @ejemplo.com

            // Solo mostrar errores si el usuario ya interactuó con el campo
            if (!emailInteracted) {
                emailError.style.display = "none";
                return false;
            }

            if (email === "") {
                emailError.innerHTML = "Debe ingresar un correo electrónico.";
                emailError.style.display = "block";
                return false;
            } else if (!emailRegex.test(email)) {
                emailError.innerHTML = "El correo debe ser de tipo @pucp.edu.pe";
                emailError.style.display = "block";
                return false;
            } else {
                emailError.style.display = "none";
                return true;
            }
        }

        // Validar la contraseña
        function validatePassword() {
            var password = document.getElementById('<%= txtPassword.ClientID %>').value;
        var passwordError = document.getElementById('<%= lblPasswordError.ClientID %>');

            // Solo mostrar errores si el usuario ya interactuó con el campo
            if (!passwordInteracted) {
                passwordError.style.display = "none";
                return false;
            }

            if (password === "") {
                passwordError.innerHTML = "Debe ingresar su contraseña.";
                passwordError.style.display = "block";
                return false;
            } else {
                passwordError.style.display = "none";
                return true;
            }
        }

        // Habilitar el botón de login
        function enableLoginButton() {
            var emailValid = validateEmail();
            var passwordValid = validatePassword();
            var loginButton = document.getElementById('<%= btnLogin.ClientID %>');

            if (emailValid && passwordValid) {
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
            enableLoginButton();
        }

        // Marcar que el usuario interactuó con el campo de contraseña
        function markPasswordInteracted() {
            passwordInteracted = true;
            enableLoginButton();
        }

        // Validar en tiempo real mientras se digita
        function setupRealTimeValidation() {
            var emailField = document.getElementById('<%= txtUsername.ClientID %>');
    var passwordField = document.getElementById('<%= txtPassword.ClientID %>');

            emailField.addEventListener('input', enableLoginButton);
            passwordField.addEventListener('input', enableLoginButton);
        }

        // Ejecutar cuando el documento esté listo
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
                    <asp:Label ID="lblEmailError" runat="server" CssClass="text-danger"
                        Style="display: none; font-size: 0.875rem;"></asp:Label>
                </div>

                <!-- Campo de contraseña -->
                <div class="form-group mt-3">
                    <label for="txtPassword">Contraseña</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"
                            onblur="markPasswordInteracted()" oninput="enableLoginButton()"></asp:TextBox>
                    </div>
                    <asp:Label ID="lblPasswordError" runat="server" CssClass="text-danger"
                        Style="display: none; font-size: 0.875rem;"></asp:Label>
                </div>

                <div class="form-group mt-2">
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger text-center d-block" EnableViewState="false" Visible="false"></asp:Label>
                </div>

                <div class="form-group mt-3 text-start">
                    <a href="#" class="d-block">¿Olvidaste tu contraseña?</a>
                </div>

                <!-- Botón de inicio de sesión -->
                <div class="form-group mt-3 d-flex justify-content-center">
                    <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión" CssClass="btn btn-primary" OnClick="btnLogin_Click" Enabled="false" />
                </div>
            </form>
        </div>
    </div>
</body>
</html>
