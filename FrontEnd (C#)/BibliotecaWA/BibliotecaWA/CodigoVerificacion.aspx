<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CodigoVerificacion.aspx.cs" Inherits="BibliotecaWA.CodigoVerificacion" %>

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
    <title>Código de Verificación</title>

    <script type="text/javascript">
        let timeLeft = 60;
        let timer;

        function startTimer() {
            document.getElementById('resendButton').disabled = true;
            document.getElementById('timer').innerText = `(${timeLeft})`;
            timer = setInterval(function () {
                timeLeft--;
                if (timeLeft <= 0) {
                    clearInterval(timer);
                    document.getElementById('resendButton').disabled = false;
                    document.getElementById('timer').innerText = ``;
                    timeLeft = 60;
                } else {
                    document.getElementById('timer').innerText = `(${timeLeft})`;
                }
            }, 1000);
        }

        function handleInput(current, nextFieldId) {
            current.value = current.value.replace(/[^0-9]/g, '');

            if (current.value.length >= current.maxLength && nextFieldId) {
                var nextField = document.getElementById(nextFieldId);
                if (nextField) {
                    setTimeout(function () {
                        nextField.focus();
                        nextField.select();
                    }, 10);
                }
            }
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
                if (prevField) {
                    prevField.focus();
                    prevField.select();
                }
            }
            else if (key === 'ArrowRight' && nextFieldId) {
                e.preventDefault();
                var nextField = document.getElementById(nextFieldId);
                if (nextField) {
                    nextField.focus();
                    nextField.select();
                }
            }
            else if (!/^(Backspace|ArrowLeft|ArrowRight|Tab|Delete|Shift|Control|Alt|Meta)$/.test(key) && !/[0-9]/.test(key)) {
                e.preventDefault();
            }
        }

        function handlePaste(e) {
            e.preventDefault();
            var pastedData = e.clipboardData.getData('text');

            if (pastedData.length === 6 && /^\d+$/.test(pastedData)) {
                var codeInputs = document.getElementsByClassName('code-input');
                for (var i = 0; i < 6; i++) {
                    if (codeInputs[i]) {
                        codeInputs[i].value = pastedData[i];
                    }
                }
                validateCode();
                if (codeInputs[5]) {
                    codeInputs[5].focus();
                    codeInputs[5].select();
                }
            }
        }

        function validateCode() {
            var codeInputs = document.getElementsByClassName('code-input');
            var userCode = "";
            for (var i = 0; i < codeInputs.length; i++) {
                userCode += codeInputs[i].value;
            }

            var isValid = userCode === "123456";
            var continueButton = document.getElementById('<%= btnContinue.ClientID %>');
            var errorContainer = document.getElementById('codeErrorContainer');

            continueButton.disabled = !(userCode.length === 6 && isValid);

            if (userCode.length === 6 && !isValid) {
                errorContainer.innerHTML = "<div class='error-message-content'><i class='fa-solid fa-circle-exclamation'></i><span class='error-text'>El código ingresado no es válido. Intenta nuevamente.</span></div>";
                errorContainer.classList.add("show");
                for (var i = 0; i < codeInputs.length; i++) {
                    codeInputs[i].classList.add('error');
                }
            } else {
                errorContainer.classList.remove("show");
                for (var i = 0; i < codeInputs.length; i++) {
                    codeInputs[i].classList.remove('error');
                }
            }
        }

        function setupRealTimeValidation() {
            var codeInputs = document.getElementsByClassName('code-input');
            for (var i = 0; i < codeInputs.length; i++) {
                if (i === 0) {
                    codeInputs[i].addEventListener('paste', handlePaste);
                }
            }
        }

        document.addEventListener('DOMContentLoaded', function () {
            setupRealTimeValidation();
            startTimer();
            
            var firstInput = document.getElementById('<%= txtCodigo1.ClientID %>');
            if (firstInput) {
                firstInput.focus();
            }
        });

        function resetTimer() {
            timeLeft = 60;
            startTimer();
        }
    </script>
</head>
<body>
    <div class="container-fluid d-flex p-0 m-0" style="height: 100vh; justify-content: center; align-items: center; background-color: #f8f9fa;">
        <div class="card shadow-sm" style="max-width: 440px; width: 100%; border-radius: 16px; border: none;">
            <div class="card-body p-4">
                <h4 class="text-center mb-3" style="color: #071437; font-weight: 500;">Código de Verificación</h4>
                <p class="text-center mb-4" style="color: #4B5675;">Ingresa el código que ha sido enviado a tu correo. En caso de no encontrarlo, revisa tu bandeja de spam.</p>

                <form id="formVerificacion" runat="server">
                    <div class="form-group code-container">
                        <asp:TextBox ID="txtCodigo1" runat="server" CssClass="form-control code-input" MaxLength="1" 
                            onkeydown="handleKeyDown(this, '', '<%= txtCodigo2.ClientID %>', event)"
                            oninput="handleInput(this, '<%= txtCodigo2.ClientID %>')" />
                        <asp:TextBox ID="txtCodigo2" runat="server" CssClass="form-control code-input" MaxLength="1" 
                            onkeydown="handleKeyDown(this, '<%= txtCodigo1.ClientID %>', '<%= txtCodigo3.ClientID %>', event)"
                            oninput="handleInput(this, '<%= txtCodigo3.ClientID %>')" />
                        <asp:TextBox ID="txtCodigo3" runat="server" CssClass="form-control code-input" MaxLength="1" 
                            onkeydown="handleKeyDown(this, '<%= txtCodigo2.ClientID %>', '<%= txtCodigo4.ClientID %>', event)"
                            oninput="handleInput(this, '<%= txtCodigo4.ClientID %>')" />
                        <asp:TextBox ID="txtCodigo4" runat="server" CssClass="form-control code-input" MaxLength="1" 
                            onkeydown="handleKeyDown(this, '<%= txtCodigo3.ClientID %>', '<%= txtCodigo5.ClientID %>', event)"
                            oninput="handleInput(this, '<%= txtCodigo5.ClientID %>')" />
                        <asp:TextBox ID="txtCodigo5" runat="server" CssClass="form-control code-input" MaxLength="1" 
                            onkeydown="handleKeyDown(this, '<%= txtCodigo4.ClientID %>', '<%= txtCodigo6.ClientID %>', event)"
                            oninput="handleInput(this, '<%= txtCodigo6.ClientID %>')" />
                        <asp:TextBox ID="txtCodigo6" runat="server" CssClass="form-control code-input" MaxLength="1" 
                            onkeydown="handleKeyDown(this, '<%= txtCodigo5.ClientID %>', '', event)"
                            oninput="validateCode()" />
                    </div>

                    <div id="codeErrorContainer" class="error-message"></div>

                    <div class="form-group text-center mt-3">
                        <button id="resendButton" type="button" class="btn btn-link p-0" onclick="resetTimer()" disabled>
                            Reenviar código <span id="timer"></span>
                        </button>
                    </div>

                    <div class="form-group mt-4 d-flex justify-content-end" style="gap: 8px;">
                        <a href="InicioSesion.aspx" class="button-error-secondary">
                            Cancelar
                        </a>
                        
                        <asp:Button ID="btnContinue" runat="server" Text="Continuar" 
                            CssClass="button-primary" OnClick="btnContinue_Click" Enabled="false" />
                    </div>
                </form>
            </div>
        </div>
    </div>
</body>
</html>