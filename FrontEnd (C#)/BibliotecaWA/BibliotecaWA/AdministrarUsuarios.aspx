<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="AdministrarUsuarios.aspx.cs" Inherits="BibliotecaWA.AdministrarUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
    <link href="Fonts/css/AdminUsuarios.css" rel="stylesheet" />
    <style>
        .readonly { background-color: #e9ecef; }
        .header { display: flex; align-items: center; margin-bottom: 20px; }
        .header span { margin-left: 10px; font-weight: bold; font-size: 24px; }
        .form-group { margin-bottom: 15px; }
        .btn-right { float: right; margin-top: 20px; }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <div class="titulo-usuarios">
        <h6 class="ColorLetras">Gestión de Usuarios</h6>
        <h6><i class="small-icon fa-solid fa-angle-right"></i></h6>
        <asp:Label ID="LblGuia" runat="server" CssClass="h6 fw-bold" ClientIDMode="Static"></asp:Label>
    </div>
    <hr>
    <div class="container-fluid mt-4">

        <!-- Título con flecha de regreso -->
        <div class="d-flex align-items-center mb-4">
            <asp:LinkButton ID="btnRegresar" runat="server" CssClass="btn btn-link text-dark fs-4 me-3"
                OnClick="btnRegresar_Click" ToolTip="Regresar">
                <i class="fa-solid fa-arrow-left"></i>
            </asp:LinkButton>
            <asp:Label ID="lblTitulo" runat="server" CssClass="h3 m-0" ClientIDMode="Static"></asp:Label>
        </div>

        <div class="custom-form-container shadow-sm overflow-hidden mx-auto">
            <!-- Campos principales -->
            <div class="row g-3">
                <div class="col-md-4">
                    <label for="ddlRol" class="form-label-admiUsuarios">Tipo de usuario</label>
                    <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select form-input admiUsuarios-input" ClientIDMode="Static"></asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <label for="txtCodigo" class="form-label-admiUsuarios">Código</label>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control form-input admiUsuarios-input" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <label for="txtDNI" class="form-label-admiUsuarios">DNI</label>
                    <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control form-input admiUsuarios-input" ClientIDMode="Static"></asp:TextBox>
                </div>
            </div>

            <!-- Campos uno debajo del otro -->
            <div class="row g-0 mt-3">
                <div class="col-md-12">
                    <label for="txtNombre" class="form-label-admiUsuarios">Nombre(s)</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control form-input admiUsuarios-input" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="col-md-12">
                    <label for="txtPrimerApellido" class="form-label-admiUsuarios">Primer Apellido</label>
                    <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="form-control form-input admiUsuarios-input" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="col-md-12">
                    <label for="txtSegundoApellido" class="form-label-admiUsuarios">Segundo Apellido</label>
                    <asp:TextBox ID="txtSegundoApellido" runat="server" CssClass="form-control form-input admiUsuarios-input" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="col-md-12">
                    <label for="txtCorreo" class="form-label-admiUsuarios">Correo</label>
                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control form-input admiUsuarios-input" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="col-md-12">
                    <label for="txtContrasena" class="form-label-admiUsuarios">Contraseña</label>
                    <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control form-input admiUsuarios-input" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="col-md-12">
                    <label for="txtTelefono" class="form-label-admiUsuarios">Teléfono</label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control form-input admiUsuarios-input" ClientIDMode="Static"></asp:TextBox>
                </div>
            </div>

            <!-- HiddenField para ID -->
            <asp:HiddenField ID="hfIdUsuarioSeleccionado" runat="server" ClientIDMode="Static" />
        </div>

        <!-- Botón de acción -->
        <div class="d-flex justify-content-end mt-4">
            <asp:Button ID="btnAccion" runat="server" CssClass="btn btn-primary"
                ClientIDMode="Static"
                OnClick="btnAccion_Click" />
        </div>

        <!-- Modal centrado -->
        <div class="modal fade" id="confirmModal" tabindex="-1" aria-labelledby="confirmModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="confirmModalLabel">Confirmar registro de usuario</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                    </div>
                    <div class="modal-body text-center">
                        <asp:Label ID="lblModalMensaje" runat="server" ClientIDMode="Static"></asp:Label>
                    </div>
                    <div class="modal-footer justify-content-center">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <button type="button" class="btn btn-primary" onclick="document.getElementById('btnAccionServer').click();">Registrar usuario</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Botón hidden para disparar el evento en server -->
        <asp:Button ID="btnAccionServer" runat="server" OnClick="btnAccion_Click" Style="display:none" ClientIDMode="Static" />

        <script>
            function actualizarModal() {
                var nombre = document.getElementById('<%= txtNombre.ClientID %>').value;
                var primerApellido = document.getElementById('<%= txtPrimerApellido.ClientID %>').value;
                var segundoApellido = document.getElementById('<%= txtSegundoApellido.ClientID %>').value;
                var mensaje = "¿ Confirma que quieres registrar el usuario \"" + nombre + " " + primerApellido + " " + segundoApellido + "\" ? Esta acción guardará los datos en el sistema.";
                document.getElementById('<%= lblModalMensaje.ClientID %>').innerText = mensaje;

                var modal = new bootstrap.Modal(document.getElementById('confirmModal'));
                modal.show();
            }
        </script>

    </div>
</asp:Content>
