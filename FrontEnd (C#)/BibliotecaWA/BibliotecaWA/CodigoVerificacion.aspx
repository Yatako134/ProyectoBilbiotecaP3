<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CodigoVerificacion.aspx.cs" Inherits="BibliotecaWA.CodigoVerificacion" %>

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
    
    <title>Código de Verificación</title>

    <style type="text/css">
        /* Estilos específicos para esta página */
        .error-message {
            display: none;
            color: #EF4444;
            font-size: 0.875rem;
            margin-top: 8px;
            align-items: center;
            justify-content: center;
            gap: 8px;
        }
        
        .error-message.show {
            display: flex !important;
        }
        
        .code-input.error {
            border-color: #EF4444 !important;
            background-color: #FEF2F2 !important;
            animation: shake 0.5s ease-in-out;
        }
        
        @keyframes shake {
            0%, 100% { transform: translateX(0); }
            25% { transform: translateX(-5px); }
            75% { transform: translateX(5px); }
        }
    </style>

    <script type="text/javascript">
        function handleInput(current, nextFieldId) {
            current.value = current.value.replace(/[^0-9]/g, '');

            // Animación de escritura
            if (current.value.length > 0) {
                current.style.transform = 'scale(1.05)';
                setTimeout(() => {
                    current.style.transform = 'scale(1)';
                }, 150);
            }

            if (current.value.length >= current.maxLength && nextFieldId) {
                var nextField = document.getElementById(nextFieldId);
                if (nextField) {
                    nextField.focus();
                    nextField.select();
                }
            }

            // Ocultar error cuando el usuario empiece a escribir
            hideError();
            validateCode();
        }

        function handleKeyDown(current, previousFieldId, nextFieldId, e) {
            var key = e.key;

            if (key === 'Backspace' && current.value.length === 0 && previousFieldId) {
                e.preventDefault();
                var prevField = document.getElementById(previousFieldId);
                if (prevField) {
                    prevField.focus();
                    prevField.select();
                }
            }
            else if (key === 'ArrowLeft' && previousFieldId) {
                e.preventDefault();
                var prevField = document.getElementById(previousFieldId);
                if (prevField) prevField.focus();
            }
            else if (key === 'ArrowRight' && nextFieldId) {
                e.preventDefault();
                var nextField = document.getElementById(nextFieldId);
                if (nextField) nextField.focus();
            }
            else if (!/^(Backspace|ArrowLeft|ArrowRight|Tab|Delete|Shift|Control|Alt|Meta)$/.test(key) && !/[0-9]/.test(key)) {
                e.preventDefault();
            }
        }

        function validateCode() {
            var codeInputs = document.getElementsByClassName('code-input');
            var userCode = "";
            for (var i = 0; i < codeInputs.length; i++) {
                userCode += codeInputs[i].value;
            }

            var continueButton = document.getElementById('<%= btnContinue.ClientID %>');

            // Habilita solo si hay 6 dígitos
            if (userCode.length === 6) {
                continueButton.disabled = false;
                continueButton.classList.remove("disabled");
            } else {
                continueButton.disabled = true;
                continueButton.classList.add("disabled");
            }
        }

        function showError() {
            var errorContainer = document.getElementById('codeErrorContainer');
            var codeInputs = document.getElementsByClassName('code-input');
            
            if (errorContainer) {
                errorContainer.classList.add("show");
            }
            
            // Agregar clase error a todos los inputs
            for (var i = 0; i < codeInputs.length; i++) {
                codeInputs[i].classList.add('error');
            }
        }

        function hideError() {
            var errorContainer = document.getElementById('codeErrorContainer');
            var codeInputs = document.getElementsByClassName('code-input');
            
            if (errorContainer) {
                errorContainer.classList.remove("show");
            }
            
            // Remover clase error de todos los inputs
            for (var i = 0; i < codeInputs.length; i++) {
                codeInputs[i].classList.remove('error');
            }
        }

        function handlePaste(e) {
            e.preventDefault();
            var pastedData = e.clipboardData.getData('text').trim();
            
            // Limpiar y validar datos pegados
            pastedData = pastedData.replace(/[^0-9]/g, '');
            
            if (pastedData.length === 6) {
                var codeInputs = document.getElementsByClassName('code-input');
                
                // Ocultar error si existe
                hideError();
                
                // Animación de pegado
                for (var i = 0; i < codeInputs.length; i++) {
                    codeInputs[i].style.transform = 'scale(1.1)';
                    codeInputs[i].value = pastedData[i];
                    setTimeout(() => {
                        codeInputs[i].style.transform = 'scale(1)';
                    }, 100 + (i * 50));
                }
                
                // Enfocar el último campo
                if (codeInputs[5]) {
                    codeInputs[5].focus();
                }
                
                validateCode();
            }
        }

        function selectText(field) {
            field.select();
        }

        // Configuración inicial
        document.addEventListener('DOMContentLoaded', function () {
            var firstInput = document.getElementById('<%= txtCodigo1.ClientID %>');
            if (firstInput) {
                firstInput.focus();
                firstInput.select();
            }

            // Agregar evento paste a todos los campos
            var codeInputs = document.getElementsByClassName('code-input');
            for (var i = 0; i < codeInputs.length; i++) {
                codeInputs[i].addEventListener('paste', handlePaste);
                codeInputs[i].addEventListener('click', function () {
                    this.select();
                });
            }

            // Validar código inicial
            validateCode();
        });
    </script>
</head>
<body>
    <div class="container-fluid d-flex p-0 m-0" style="height: 100vh; justify-content: center; align-items: center; background-color: #f8f9fa;">
        <div class="card shadow-sm" style="max-width: 440px; width: 100%; border-radius: 16px; border: none;">
            <div class="card-body p-4">
                <h4 class="text-center mb-3" style="color: #071437; font-weight: 500;">Código de Verificación</h4>
                <p class="text-center mb-4" style="color: #4B5675;">Ingresa el código de 6 dígitos enviado a tu correo electrónico.</p>

                <form id="formVerificacion" runat="server">
                    <!-- Contenedor de código de verificación -->
                    <div class="code-container">
                        <asp:TextBox ID="txtCodigo1" runat="server" CssClass="form-control code-input" MaxLength="1"
                            onkeydown="handleKeyDown(this, '', '<%= txtCodigo2.ClientID %>', event)"
                            oninput="handleInput(this, '<%= txtCodigo2.ClientID %>')"
                            onclick="selectText(this)" />
                        <asp:TextBox ID="txtCodigo2" runat="server" CssClass="form-control code-input" MaxLength="1"
                            onkeydown="handleKeyDown(this, '<%= txtCodigo1.ClientID %>', '<%= txtCodigo3.ClientID %>', event)"
                            oninput="handleInput(this, '<%= txtCodigo3.ClientID %>')"
                            onclick="selectText(this)" />
                        <asp:TextBox ID="txtCodigo3" runat="server" CssClass="form-control code-input" MaxLength="1"
                            onkeydown="handleKeyDown(this, '<%= txtCodigo2.ClientID %>', '<%= txtCodigo4.ClientID %>', event)"
                            oninput="handleInput(this, '<%= txtCodigo4.ClientID %>')"
                            onclick="selectText(this)" />
                        <asp:TextBox ID="txtCodigo4" runat="server" CssClass="form-control code-input" MaxLength="1"
                            onkeydown="handleKeyDown(this, '<%= txtCodigo3.ClientID %>', '<%= txtCodigo5.ClientID %>', event)"
                            oninput="handleInput(this, '<%= txtCodigo5.ClientID %>')"
                            onclick="selectText(this)" />
                        <asp:TextBox ID="txtCodigo5" runat="server" CssClass="form-control code-input" MaxLength="1"
                            onkeydown="handleKeyDown(this, '<%= txtCodigo4.ClientID %>', '<%= txtCodigo6.ClientID %>', event)"
                            oninput="handleInput(this, '<%= txtCodigo6.ClientID %>')"
                            onclick="selectText(this)" />
                        <asp:TextBox ID="txtCodigo6" runat="server" CssClass="form-control code-input" MaxLength="1"
                            onkeydown="handleKeyDown(this, '<%= txtCodigo5.ClientID %>', '', event)"
                            oninput="handleInput(this, '')"
                            onclick="selectText(this)" />
                    </div>

                    <!-- Mensaje de error - CORREGIDO -->
                    <div id="codeErrorContainer" class="error-message">
                        <i class="fa-solid fa-circle-exclamation"></i>
                        <span class="error-text">El código ingresado no es válido. Intenta nuevamente.</span>
                    </div>

                    <!-- Botones -->
                    <div class="form-group mt-4 d-flex justify-content-end" style="gap: 8px;">
                        <!-- Botón Cancelar -->
                        <a href="InicioSesion.aspx" class="button-error-secondary">
                            Cancelar
                        </a>
                        
                        <!-- Botón Continuar -->
                        <asp:Button ID="btnContinue" runat="server" Text="Continuar" 
                            CssClass="button-primary" OnClick="btnContinue_Click" Enabled="false" />
                    </div>
                </form>
            </div>
        </div>
    </div>
</body>
</html>