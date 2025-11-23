<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CodigoVerificacion.aspx.cs" Inherits="BibliotecaWA.CodigoVerificacion" %>

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
    <title>Código de Verificación</title>

      <script type="text/javascript">
          let timeLeft = 60;
          let timer;

          // Deshabilitar el botón de reenvío y actualizar el tiempo restante
          function startTimer() {
              document.getElementById('resendButton').disabled = true;
              document.getElementById('timer').innerText = `(${timeLeft})`;
              timer = setInterval(function () {
                  timeLeft--;
                  if (timeLeft <= 0) {
                      clearInterval(timer);
                      document.getElementById('resendButton').disabled = false;
                      document.getElementById('timer').innerText = ``;
                      timeLeft = 60; // Reset the timer
                  } else {
                      document.getElementById('timer').innerText = `(${timeLeft})`;
                  }
              }, 1000);
          }

          // Función para verificar si el código ingresado es correcto
          function validateCode() {
              var codeInputs = document.getElementsByClassName('code-input');
              var userCode = "";
              for (let i = 0; i < codeInputs.length; i++) {
                  userCode += codeInputs[i].value;
              }

              // El código de verificación siempre será "123456"
              var isValid = userCode === "123456";

              // Habilitar el botón solo si todos los campos están llenos y el código es válido
              document.getElementById('<%= btnContinue.ClientID %>').disabled = !(userCode.length === 6 && isValid);

              // Si el código es incorrecto, mostrar el error
              if (userCode.length === 6 && !isValid) {
                  document.getElementById('lblError').style.display = 'block';
                  document.getElementById('lblError').innerText = 'El código ingresado no es válido. Intenta nuevamente.';
              } else {
                  document.getElementById('lblError').style.display = 'none';
              }
          }

          // Ejecutar la validación del código al cambiar un campo
          function setupRealTimeValidation() {
              var codeInputs = document.getElementsByClassName('code-input');
              for (let i = 0; i < codeInputs.length; i++) {
                  codeInputs[i].addEventListener('input', validateCode);
              }
          }

          // Ejecutar cuando el documento esté listo
          document.addEventListener('DOMContentLoaded', function () {
              setupRealTimeValidation();
              startTimer();
          });

          // Función para reiniciar el contador de "Reenviar código"
          function resetTimer() {
              timeLeft = 60;
              startTimer();
          }
      </script>

    <style>
        /* Estilo para los campos de código, uno al lado del otro */
        .code-input {
            width: 40px;
            height: 60px;
            text-align: center;
            font-size: 24px;
            margin: 5px;
        }

        /* Contenedor de los campos de código en línea */
        .code-container {
            display: flex;
            justify-content: center;
            gap: 10px;
        }

        /* Estilo para los mensajes de error */
        #lblError {
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
                <h4 class="text-center mb-4">Código de Verificación</h4>
                <p class="text-center mb-4">Ingresa el código que ha sido enviado a tu correo. En caso de no encontrarlo, revisa tu bandeja de spam.</p>

                <form id="formVerificacion" runat="server">
                    <!-- Campo para el código de verificación -->
                    <div class="form-group code-container">
                        <asp:TextBox ID="txtCodigo1" runat="server" CssClass="form-control code-input" MaxLength="1" OnInput="validateCode()" />
                        <asp:TextBox ID="txtCodigo2" runat="server" CssClass="form-control code-input" MaxLength="1" OnInput="validateCode()" />
                        <asp:TextBox ID="txtCodigo3" runat="server" CssClass="form-control code-input" MaxLength="1" OnInput="validateCode()" />
                        <asp:TextBox ID="txtCodigo4" runat="server" CssClass="form-control code-input" MaxLength="1" OnInput="validateCode()" />
                        <asp:TextBox ID="txtCodigo5" runat="server" CssClass="form-control code-input" MaxLength="1" OnInput="validateCode()" />
                        <asp:TextBox ID="txtCodigo6" runat="server" CssClass="form-control code-input" MaxLength="1" OnInput="validateCode()" />
                    </div>

                    <!-- Mensaje de error -->
                    <div class="form-group">
                        <asp:Label ID="lblError" runat="server" CssClass="text-danger" Style="display: none; font-size: 0.875rem;"></asp:Label>
                    </div>

                    <!-- Reenviar código -->
                    <div class="form-group text-center">
                        <button id="resendButton" class="btn btn-link" type="button" onclick="resetTimer()" disabled>Reenviar código <span id="timer"></span></button>
                    </div>

                    <!-- Botón continuar -->
                    <div class="form-group mt-3 d-flex justify-content-center">
                        <asp:Button ID="btnContinue" runat="server" Text="Continuar" CssClass="btn btn-primary" OnClick="btnContinue_Click" Enabled="false" />
                    </div>
                </form>
            </div>
        </div>
    </div>

    <!-- Optional JavaScript -->
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

</body>
</html>
