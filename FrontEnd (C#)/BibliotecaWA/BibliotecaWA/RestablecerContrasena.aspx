<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RestablecerContrasena.aspx.cs" Inherits="BibliotecaWA.RestablecerContrasena" %>

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
    <title>Restablecer contraseña</title>

    <script type="text/javascript">
        // Validar el correo institucional
        function validateEmail() {
            var email = document.getElementById('<%= txtEmail.ClientID %>').value;
            var emailError = document.getElementById('<%= lblEmailError.ClientID %>');
            // Regex para cualquier correo válido
            var emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

            if (email === "") {
                emailError.innerHTML = "Debe ingresar su correo institucional.";
                emailError.style.display = "block";
                return false;
            } else if (!emailRegex.test(email)) {
                emailError.innerHTML = "El correo debe ser válido.";
                emailError.style.display = "block";
                return false;
            } else {
                emailError.style.display = "none";
                return true;
            }
        }

        // Habilitar el botón de continuar
        function enableContinueButton() {
            var emailValid = validateEmail();
            var continueButton = document.getElementById('<%= btnContinue.ClientID %>');

            if (emailValid) {
                continueButton.disabled = false;
                continueButton.classList.remove("disabled");
            } else {
                continueButton.disabled = true;
                continueButton.classList.add("disabled");
            }
        }

        // Validar en tiempo real mientras se digita y al quitar el foco
        function setupRealTimeValidation() {
            var emailField = document.getElementById('<%= txtEmail.ClientID %>');
            emailField.addEventListener('input', enableContinueButton);
            emailField.addEventListener('blur', validateEmail); // Ejecutar validación al quitar el foco
        }

        // Ejecutar cuando el documento esté listo
        document.addEventListener('DOMContentLoaded', function () {
            setupRealTimeValidation();
        });
    </script>

</head>
<body>
     <div class="container-fluid d-flex p-0 m-0" style="height: 100vh; justify-content: center; align-items: center;">
        <!-- Contenedor del formulario -->
        <div class="card" style="max-width: 400px; width: 100%; padding: 20px;">
            <div class="card-body">
                <h4 class="text-center mb-4">Restablecer Contraseña</h4>
                <p class="text-center mb-4">Ingresa tu correo institucional registrado para continuar con el proceso.</p>

                <form id="formRestablecer" runat="server">
                    <!-- Campo de correo electrónico -->
                    <div class="form-group">
                        <label for="txtEmail">Correo institucional</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" oninput="enableContinueButton()" onblur="validateEmail()"></asp:TextBox>
                        <asp:Label ID="lblEmailError" runat="server" CssClass="text-danger" Style="display: none; font-size: 0.875rem;"></asp:Label>
                    </div>

                    <!-- Botón de continuar -->
                    <div class="form-group mt-3 d-flex justify-content-center">
                        <asp:Button ID="btnContinue" runat="server" Text="Continuar" CssClass="btn btn-primary" OnClick="btnContinue_Click" Enabled="false" />
                    </div>
                </form>
            </div>
        </div>
    </div>
</body>
</html>
