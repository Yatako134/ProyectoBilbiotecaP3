<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Prestamo.aspx.cs" Inherits="BibliotecaWA.Prestamo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Prestamo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
     <script src="Scripts/UtilsArmy/SolicitarPrestamo.js"></script>
    <script src="Scripts/UtilsArmy/ValidacionesPrestamo.js"></script>
    <script src="Scripts/UtilsArmy/MensajeExito.js"></script>
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
                <!-- Código -->
                <div class="row mb-2">
                    <div class="col-md-12">
                        <strong>Código:</strong>
                        <span class="ms-2">
                            <asp:Label ID="lblCodigoEjemplar" runat="server"></asp:Label>
                        </span>
                    </div>
                </div>

                <!-- Ubicación -->
                <div class="row mb-2">
                    <div class="col-md-12">
                        <strong>Ubicación:</strong>
                        <span class="ms-2">
                            <asp:Label ID="lblUbicacionEjemplar" runat="server"></asp:Label>
                        </span>
                    </div>
                </div>

                <!-- Estado -->
                <div class="row">
                    <div class="col-md-12">
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
                    <div class="col-md-6 d-flex align-items-center">
                        <asp:Label runat="server" Text="Tipo de usuario" CssClass="form-label me-2"
                            Style="white-space: nowrap;"></asp:Label>
                        <asp:TextBox ID="txtTipoUsuario" runat="server" CssClass="form-control me-3"
                            Style="white-space: nowrap;"></asp:TextBox>
                    </div>
                    <div class="col-md-6 d-flex align-items-center">
                        <asp:Label runat="server" Text="Préstamos vigentes" CssClass="form-label m-2"
                            Style="white-space: nowrap;"></asp:Label>
                        <asp:TextBox ID="txtPrestamosVigentes" runat="server" CssClass="form-control"
                            Style="white-space: nowrap;"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-3">
                    <div class="col-md-6 d-flex align-items-center">
                        <asp:Label runat="server" Text="Límite de días" CssClass="form-label me-2"
                            Style="white-space: nowrap;"></asp:Label>
                        <asp:TextBox ID="txtLimiteDias" runat="server" CssClass="form-control"
                            ReadOnly="true" Style="white-space: nowrap;"></asp:TextBox>
                    </div>
                    <div class="col-md-6 d-flex align-items-center">
                        <asp:Label runat="server" Text="Límite de préstamos" CssClass="form-label me-2 "
                            Style="white-space: nowrap;"></asp:Label>
                        <asp:TextBox ID="txtLimitePrestamo" runat="server" CssClass="form-control"
                            ReadOnly="true" Style="white-space: nowrap;"></asp:TextBox>
                    </div>
                </div>


                <!-- FILA 3: Fecha vencimiento -->
                <div class="row mb-3">
                    <div class="col-md-6 d-flex align-items-center">
                        <asp:Label runat="server" Text="Fecha de préstamo" CssClass="form-label me-2"
                            Style="white-space: nowrap;"></asp:Label>
                        <asp:TextBox ID="txtFechaPrestamo" runat="server" CssClass="form-control"
                            ReadOnly="true" Style="white-space: nowrap;"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-3">
                    <div class="col-12 d-flex align-items-center flex-wrap">
                        <asp:Label runat="server" Text="Fecha de vencimiento" CssClass="form-label me-2"
                            Style="white-space: nowrap;"></asp:Label>
                        <asp:TextBox ID="txtFechaVencimiento" runat="server" CssClass="form-control me-3"
                            ReadOnly="true" Style="width: 180px; display: inline-block;"></asp:TextBox>

                        <span class="text-danger small ms-2" style="white-space: nowrap;">
                            <i class="fa-solid fa-circle-exclamation me-1"></i>
                            Si no se devuelve a tiempo, se aplicará la sanción correspondiente.
                        </span>
                    </div>

                    <!-- ALERTA 
                            <div class="alert alert-danger small mb-3">
                                <i class="fa-solid fa-circle-exclamation"></i>
                                Si no se devuelve a tiempo, se aplicará la sanción correspondiente.
                            </div>-->
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
        <!-- Modal genérico de alerta -->
        <div class="modal fade" id="alertModal" tabindex="-1" aria-labelledby="alertModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content border-0 shadow-lg">
                    <div class="modal-header bg-danger text-white">
                        <h5 class="modal-title" id="alertModalLabel">⚠️ Alerta</h5>
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                    </div>
                    <div class="modal-body" id="mensajeAlerta">
                        <!-- Aquí irá el mensaje dinámico -->
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Aceptar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal de confirmación final -->
    <div class="modal fade" id="modalPrestamoExitoso" tabindex="-1" aria-labelledby="modalPrestamoExitosoLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalPrestamoExitosoLabel">Confirmación de préstamo</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body text-center">
                    <p>El ejemplar ha sido prestado correctamente.</p>
                    <p id="codigoPrestamo" class="fw-bold text-success">
                        <!-- Aquí se insertará dinámicamente el código -->
                    </p>
                    <p>Para ver detalles del préstamo seleccione “Ir al préstamo”.</p>
                </div>
                <div class="modal-footer d-flex justify-content-center">
                    <button id="btnIrPrestamo" class="btn btn-outline-primary">Ir al préstamo</button>
                    <asp:Button ID="btnContinuar" runat="server" CssClass="btn btn-primary" Text="Continuar buscando" OnClick="btnContinuar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
