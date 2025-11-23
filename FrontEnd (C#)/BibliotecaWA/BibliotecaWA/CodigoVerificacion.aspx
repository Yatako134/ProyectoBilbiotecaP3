<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CodigoVerificacion.aspx.cs" Inherits="BibliotecaWA.CodigoVerificacion" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Código de Verificación</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Fonts/css/all.css" rel="stylesheet" />
    <link href="Content/site.css" rel="stylesheet" />

    <style>
        .code-container {
            display: flex;
            justify-content: space-between;
            gap: 8px;
        }
        .code-input {
            width: 50px;
            height: 50px;
            text-align: center;
            font-size: 24px;
        }
        .error {
            border-color: #e74c3c !important;
        }
        .error-message {
            display: none;
            margin-top: 5px;
            color: #e74c3c;
        }
        .error-message.show {
            display: block;
        }
    </style>

    <script type="text/javascript">
        function handleInput(current, nextFieldId) {
            current.value = current.value.replace(/[^0-9]/g, '');
            if (current.value.length >= current.maxLength && nextFieldId) {
                var nextField = document.getElementById(nextFieldId);
                if (nextField) nextField.focus();
            }
            validateCode();
        }

        function handleKeyDown(current, previousFieldId, nextFieldId, e) {
            var key = e.key;
            if (key === 'Backspace' && current.value.length === 0 && previousFieldId) {
                e.preventDefault();
                var prevField = document.getElementById(previousFieldId);
                if (prevField) prevField.focus();
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
            var errorContainer = document.getElementById('codeErrorContainer');

            // Habilita solo si hay 6 dígitos
            continueButton.disabled = userCode.length !== 6;

            if (userCode.length === 6) {
                errorContainer.classList.remove("show");
                for (var i = 0; i < codeInputs.length; i++) {
                    codeInputs[i].classList.remove('error');
                }
            }
        }

        function handlePaste(e) {
            e.preventDefault();
            var pastedData = e.clipboardData.getData('text');
            if (pastedData.length === 6 && /^\d+$/.test(pastedData)) {
                var codeInputs = document.getElementsByClassName('code-input');
                for (var i = 0; i < 6; i++) {
                    codeInputs[i].value = pastedData[i];
                }
                validateCode();
            }
        }

        document.addEventListener('DOMContentLoaded', function () {
            var firstInput = document.getElementById('<%= txtCodigo1.ClientID %>');
            if (firstInput) firstInput.focus();
            var codeInputs = document.getElementsByClassName('code-input');
            if (codeInputs.length > 0) codeInputs[0].addEventListener('paste', handlePaste);
        });
    </script>
</head>
<body>
    <div class="container d-flex justify-content-center align-items-center" style="height:100vh;">
        <div class="card p-4 shadow" style="max-width: 400px; width: 100%;">
            <h4 class="text-center mb-3" style="color: #004080;">Código de Verificación</h4>
            <p class="text-center mb-4">Ingresa el código enviado a tu correo.</p>

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
                        oninput="handleInput(this, '')" />
                </div>

                <div id="codeErrorContainer" class="error-message">
    <asp:Label ID="lblError" runat="server" ForeColor="#e74c3c" Visible="false"></asp:Label>
</div>

                <div class="form-group mt-4 text-end">
                    <asp:Button ID="btnContinue" runat="server" Text="Continuar" CssClass="btn btn-primary" 
                        OnClick="btnContinue_Click" Enabled="false" />
                </div>

            </form>

        </div>
    </div>
</body>
</html>