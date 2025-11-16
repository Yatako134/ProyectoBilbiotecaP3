<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="DetallePrestamo_Sancion.aspx.cs" Inherits="BibliotecaWA.DetallePrestamo_Sancion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <!-- Superior -->
    <div class="d-flex align-items-center mb-2">
        <i class="fa-solid fa-book fa-sm me-2"></i>
        <span class="fw-bold me-4">Mi historial de préstamos </span>
        <span class="fw-bold me-4">
            <i class="fa-solid fa-greater-than fa-xs me-4"></i>
            <asp:Label ID="lblCabecera" runat="server"></asp:Label>
        </span>
    </div>

    <div class="card shadow-none mb-3 p-4">
        <h2>
            <asp:LinkButton class="btn-link btn-dark text-decoration-none fa-sm" runat="server">
       <i class="fa-regular fa-arrow-left fa-sm me-2" style="color:black;"></i>
            </asp:LinkButton>
            <asp:Label ID="lbltitlecard" runat="server" Text="Préstamo"></asp:Label>
        </h2>
        <div class="card shadow-none p-3 mb-3">
            <div class="card-body d-flex">
                <asp:Image ID="imgTipo" runat="server" CssClass="me-3 rounded" Width="200px" Height="200px" />
                <div>
                    <h3 class="card-title mb-1">
                        <asp:Label ID="lblTitulo" runat="server"></asp:Label>
                    </h3>
                    <p class="mb-1 text-muted">
                        Autor(es):
        <asp:Label ID="lblAutor" runat="server"></asp:Label>
                    </p>
                    <p class="mb-1 text-muted">
                        Año de publicación:
        <asp:Label ID="lblAnio" runat="server"></asp:Label>
                    </p>
                    <span class="badge rounded-pill bg-primary text-white me-2" id="lblTipo" runat="server"></span>
                    <span class="badge rounded-pill  text-white me-2" id="lblTema" runat="server"></span>
                </div>
                <hr />
            </div>
        </div>
        <div class="card shadow-sm mb-3">
    <!-- Cabecera celeste -->
    <div class="card-header" style="background-color: #c8e5eb;">
        <h5 class="mb-0">Información adicional</h5>
    </div>
    <div class="card-body">
        <p class="mb-1">
            <strong>Código:</strong>
            <span id="lblCodigo" runat="server"></span>
        </p>
        <p class="mb-1">
            <strong>Locación:</strong>
            <span id="lblLocacion" runat="server"></span>
        </p>
        <p class="mb-1">
            <strong>Estado:</strong>
            <span id="lblEstadoCard" runat="server" class="badge rounded-pill text-white"></span>
        </p>
    </div>
</div>
    </div>

</asp:Content>
    