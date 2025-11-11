<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Prestamo.aspx.cs" Inherits="BibliotecaWA.Prestamo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Prestamo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
     <script src="Scripts/UtilsArmy/SolicitarPrestamo.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <div class="container mt-3">

        <!-- Encabezado del libro -->
        <div class="card shadow-sm mb-4">
            <div class="card-header">
                <h2>
                    <asp:Label ID="lbltitlecard" runat="server" Text="Prestamo"></asp:Label>
                </h2>
            </div>
            <div class="card-body d-flex">
                <img src="images/book.png" class="img-thumbnail me-4" style="width: 120px; height: 140px;" />
                <div>
                    <h5 class="card-title mb-1">
                        <asp:Label ID="lblTitulo" runat="server"></asp:Label>
                    </h5>
                    <p class="mb-1 text-muted">
                        Autor(es):
                    <asp:Label ID="lblAutor" runat="server"></asp:Label>
                    </p>
                    <p class="mb-1 text-muted">
                        Año de publicación:
                    <asp:Label ID="lblAnio" runat="server"></asp:Label>
                    </p>
                    <div>
                        <span class="badge bg-primary" id="lblTipo" runat="server"></span>
                        <span class="badge bg-info text-white" id="lblTema" runat="server"></span>
                        <span class="badge bg-success text-white" id="lblEstado" runat="server"></span>
                    </div>
                    <hr />
                </div>
            </div>
        </div>


        <!-- Detalles del ejemplar -->
        <div class="card mb-4 shadow-sm">
            <div class="card-header bg-light fw-bold">
                <asp:Label ID="lblBiblioteca" runat="server"></asp:Label>
            </div>
            <div class="card-body">
                <div class="row mb-2">
                    <div class="col-md-4">
                        <strong>Código:</strong>
                        <span class="ms-2">
                            <asp:Label ID="lblCodigoEjemplar" runat="server"></asp:Label>
                        </span>
                    </div>
                    <br />
                    <div class="col-md-4">
                        <strong>Ubicación:</strong>
                        <span class="ms-2">
                            <asp:Label ID="lblUbicacionEjemplar" runat="server"></asp:Label>
                        </span>
                    </div>
                    <br />
                    <div class="col-md-4">
                        <strong>Estado:</strong>
                        <span class="ms-2">
                            <span id="lblEstadoEjemplar" runat="server" class="badge rounded-pill px-3 py-2"></span>
                        </span>
                    </div>
                </div>
            </div>
        </div>
        <!-- Detalles del préstamo -->
        <div class="card shadow-sm mb-4">
            <div class="card-header bg-light fw-bold">
                Detalles del préstamo
            </div>
            <div class="card-body">
                <div class="row mb-3">
                    <div class="col-md-4">
                        <asp:Label runat="server" Text="Código Universitario" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtCodigoUniv" runat="server" CssClass="form-control"
                            AutoPostBack="true" OnTextChanged="txtCodigoUniv_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-md-8">
                        <asp:Label runat="server" Text="Nombre" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" ReadOnly="true" Enabled="false"></asp:TextBox>
                    </div>
                </div>

                <!-- FILA 2: Tipo usuario + Límite días + Fecha préstamo -->
                <div class="row mb-3">
                    <div class="col-md-4">
                        <asp:Label runat="server" Text="Tipo de usuario" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtTipoUsuario" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Label runat="server" Text="Límite de días" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtLimiteDias" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    
                </div>

                <!-- FILA 3: Fecha vencimiento -->
                <div class="row mb-3">
                    <div class="col-md-4">
                        <asp:Label runat="server" Text="Fecha de préstamo" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtFechaPrestamo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Label runat="server" Text="Fecha de vencimiento" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtFechaVencimiento" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <!-- ALERTA -->
                    <div class="alert alert-danger small mb-3">
                        <i class="fa-solid fa-circle-exclamation"></i>
                        Si no se devuelve a tiempo, se aplicará la sanción correspondiente.
                    </div>
                </div>


                <div class="d-flex justify-content-end">
                    <asp:Button ID="btnSolicitarPrestamo" runat="server" Text="Solicitar préstamo"
                        CssClass="btn btn-primary" OnClick="btnSolicitarPrestamo_Click" />
                </div>
            </div>
        </div>
        <!-- MODAL DE CONFIRMACIÓN -->
        <div class="modal fade" id="confirmModal" tabindex="-1" aria-labelledby="confirmModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header bg-primary text-white">
                        <h5 class="modal-title" id="confirmModalLabel">Confirmar Préstamo
                        </h5>
                    </div>
                    <div class="modal-body">
                        <p id="mensajeModal">
                            <!-- Aquí se llenará dinámicamente el mensaje -->
                        </p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <asp:Button ID="btnConfirmarPrestamo" runat="server" Text="Registrar prestamo" CssClass="btn btn-primary"
                            OnClick="btnConfirmarPrestamo_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
