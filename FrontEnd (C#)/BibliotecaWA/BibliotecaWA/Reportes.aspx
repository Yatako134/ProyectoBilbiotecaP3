<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="BibliotecaWA.Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Reportes
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
    <!-- jQuery (requerido para usar modales) -->
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>

    <!-- Bootstrap JS (activa los modales) -->
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script>
        function mostrarModal(titulo, mensaje) {
            document.getElementById("modalTitulo").innerText = titulo;
            document.getElementById("modalCuerpo").innerText = mensaje;
            $("#modalMensaje").modal("show");
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <div class="d-flex align-items-center mb-4">
        <!-- Icono -->
        <i class="fas fa-th" style="margin-right: 10px; font-size: 24px;"></i>

        <div style="border-right: 2px solid #ccc; height: 24px; margin-right: 10px;"></div>

        <!-- Texto -->
        <h6>Reportes</h6>
    </div>
    <hr />

    <!-- === CABECERA SUPERIOR === -->
    <div class="tabla-header d-flex justify-content-between align-items-center p-3">
        <h2><strong>Reportes</strong></h2>
        <asp:LinkButton ID="lkDescargar" CssClass="btn btn-primary" runat="server"
            OnClick="btnRegistrar_click"
            Text="<i class='fas fa-download pe-2'></i> Descargar" />
    </div>

    <!-- === CONTENIDO DE REPORTES === -->
    <div class="container mt-4">

        <!-- ======= REPORTE 1: Usuarios sancionados ======= -->
        <div class="row mb-4">
            <div class="col-md-4">
                <div class="p-3 border rounded shadow-sm bg-white">
                    <div class="form-check">
                        <asp:CheckBox ID="chkReporteUsuarios" runat="server" CssClass="form-check-input" />
                        <label class="form-check-label" for="chkUsuariosSancionados">
                            <strong>Reporte de usuarios sancionados</strong>
                        </label>
                    </div>
                    <p class="text-muted mt-2 mb-0">
                        Informe en formato PDF con los usuarios más sancionados
                    dentro de un rango de fechas especificadas.
                    </p>
                </div>
            </div>

            <!-- Fechas -->
            <div class="col-md-4">
                <label class="form-label">Fecha Inicio</label>
                <asp:TextBox ID="txtFechaInicio1" CssClass="form-control" TextMode="Date" runat="server" />
            </div>

            <div class="col-md-4">
                <label class="form-label">Fecha Fin</label>
                <asp:TextBox ID="txtFechaFin1" CssClass="form-control" TextMode="Date" runat="server" />
            </div>
        </div>


        <!-- ======= REPORTE 2: Materiales más prestados ======= -->
        <div class="row mb-4">
            <div class="col-md-4">
                <div class="p-3 border rounded shadow-sm bg-white">
                    <div class="form-check">
                        <asp:CheckBox ID="chkReporteLibros" runat="server" CssClass="form-check-input" />
                        <label class="form-check-label" for="chkMaterialesPrestados">
                            <strong>Reporte de materiales más prestados</strong>
                        </label>
                    </div>
                    <p class="text-muted mt-2 mb-0">
                        Informe en formato PDF con los materiales que han registrado
                    el mayor número de préstamos dentro de un rango de fechas.
                    </p>
                </div>
            </div>

            <!-- Fechas -->
            <div class="col-md-4">
                <label class="form-label">Fecha Inicio</label>
                <asp:TextBox ID="txtFechaInicio2" CssClass="form-control" TextMode="Date" runat="server" />
            </div>

            <div class="col-md-4">
                <label class="form-label">Fecha Fin</label>
                <asp:TextBox ID="txtFechaFin2" CssClass="form-control" TextMode="Date" runat="server" />
            </div>
        </div>


        <!-- ======= REPORTE 3: Ejemplares en reparación ======= -->
        <div class="row mb-4">
            <div class="col-md-4">
                <div class="p-3 border rounded shadow-sm bg-white">
                    <div class="form-check">
                        <asp:CheckBox ID="chkReporteEjemplares" runat="server" CssClass="form-check-input" />
                        <label class="form-check-label" for="chkEjemplaresReparacion">
                            <strong>Reporte de ejemplares en reparación</strong>
                        </label>
                    </div>
                    <p class="text-muted mt-2 mb-0">
                        Informe en formato PDF con los ejemplares que se encuentran
                    en proceso de reparación.
                    </p>
                </div>
            </div>

            <!-- Este reporte no usa fechas en tu imagen, así que lo dejo vacío -->
            <div class="col-md-8"></div>
        </div>

    </div>
 <!-- Modal unificado para alertas -->
    <div class="modal fade" id="modalAlert" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-warning">
                    <h5 class="modal-title" id="modalTitle">Aviso</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body" id="modalBody">
                    Mensaje
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <!-- === MODAL GENÉRICO === -->
    <div class="modal fade" id="modalMensaje" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">

                <div class="modal-header bg-light">
                    <h5 class="modal-title" id="modalTitulo">Mensaje</h5>
                    <%--                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>--%>
                </div>

                <div class="modal-body" id="modalCuerpo">
                    Texto del mensaje aquí.
                </div>

                <div class="modal-footer">
                    <button class="btn btn-primary" data-dismiss="modal">Continuar</button>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
