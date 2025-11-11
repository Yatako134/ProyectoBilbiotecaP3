<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="GestMaterial.aspx.cs" Inherits="BibliotecaWA.GestMaterial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
    <link href="Fonts/css/Estilos2.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <div class="d-flex align-items-center mb-4">
        <!-- Icono -->
        <i class="fas fa-th" style="margin-right: 10px; font-size: 24px;"></i>

        <div style="border-right: 2px solid #ccc; height: 24px; margin-right: 10px;"></div>

        <!-- Texto -->
        <p1>Gestión de materiales</p1>
    </div>
    <hr />

    <!-- === CABECERA SUPERIOR === -->
    <div class="tabla-header d-flex justify-content-between align-items-center p-3">
        <h1><strong>Inventario de materiales bibliográficos</strong></h1>
        <asp:LinkButton ID="lkRegistrar" CssClass="btn btn-primary" runat="server"
            OnClick="btnRegistrar_click"
            Text="<i class='fa-jelly fa-regular fa-circle-plus pe-2'></i> Registrar material" />
    </div>

    <!-- === CONTENEDOR UNIFICADO === -->
<div class="tabla-container shadow-sm rounded-4 overflow-hidden ">

    <!-- === BARRA DE BÚSQUEDA === -->
    <div class="tabla-busqueda d-flex justify-content-between align-items-center p-3 border-bottom bg-light">
        <asp:Label ID="lblResultados" runat="server" CssClass="text-muted"></asp:Label>

        <div class="input-group w-75">
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar algún material..." />
            <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-outline-primary"
                OnClick="btnBuscar_Click">
                 Buscar
            </asp:LinkButton>
            <asp:LinkButton ID="btnBuscarAvanzada" runat="server" CssClass="btn btn-outline-primary"
                OnClick="btnBuscarAvanzada_Click">
                    <i class="fa-solid fa-magnifying-glass"></i> Búsqueda avanzada
            </asp:LinkButton>
        </div>

    </div>
    <asp:HiddenField ID="hfIdUsuarioSeleccionado" runat="server" ClientIDMode="Static" />
<asp:GridView ID="dgvUsuario" runat="server"
    AutoGenerateColumns="False"
    AllowPaging="True"
    PageSize="10"
    CssClass="tabla-datos"
    Width="100%"
    OnPageIndexChanging="dgvUsuario_PageIndexChanging"
    OnRowDataBound="dgvUsuario_RowDataBound"
    PagerSettings-Visible="False">
    
    <Columns>
        
        <asp:BoundField HeaderText="Código" SortExpression="Codigo"
            HeaderStyle-CssClass="text-center"
            ItemStyle-CssClass="text-center" />

        
        <asp:TemplateField HeaderText="Título" SortExpression="Titulo"
            HeaderStyle-CssClass="text-left"
            ItemStyle-CssClass="text-left">
            <HeaderTemplate>
                Título <i class="fas fa-book ms-2"></i>
            </HeaderTemplate>
            <ItemTemplate>
                <%# Eval("Titulo") %> 
            </ItemTemplate>
        </asp:TemplateField>

        
        <asp:TemplateField HeaderText="Tipo" SortExpression="Tipo"
            HeaderStyle-CssClass="text-center"
            ItemStyle-CssClass="text-center">
            <HeaderTemplate>
                Tipo <i class="fa-solid fa-cards-blank"></i>
            </HeaderTemplate>
            <ItemTemplate>
                <span class="tipo-celda">
                    <span class="pill pill-info">
                        <%# Eval("Tipo") %> 
                    </span>
                </span>
            </ItemTemplate>
        </asp:TemplateField>

        
        <asp:TemplateField HeaderText="Estado" SortExpression="Estado"
            HeaderStyle-CssClass="text-center"
            ItemStyle-CssClass="text-center">
            <HeaderTemplate>
                Estado <i class="fa-solid fa-chart-simple"></i>
            </HeaderTemplate>
            <ItemTemplate>
                <span class="estado-celda">
                    <span class="pill <%# Eval("Estado").ToString() == "Disponible" ? "pill-success" : "pill-danger" %>">
                        <%# Eval("Estado") %> 
                    </span>
                </span>
            </ItemTemplate>
        </asp:TemplateField>

        
        <asp:BoundField HeaderText="Cantidad" SortExpression="Cantidad"
            HeaderStyle-CssClass="text-center"
            ItemStyle-CssClass="text-center" />


        <asp:TemplateField HeaderText=" " HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
            <ItemTemplate>
                <div class="dropdown">
                    <button class="btn btn-link text-dark dropdown-toggle" type="button"
                        data-bs-toggle="dropdown" aria-expanded="false">
                        <i class="fa-solid fa-ellipsis-vertical"></i>
                    </button>
                    <ul class="dropdown-menu">
                        <li>
                            <asp:LinkButton runat="server" CssClass="dropdown-item"
                                CommandArgument='<%# Eval("IdMaterial") %>'
                                OnCommand="Opciones_Command" CommandName="VerDetalle">
                        <i class="fas fa-eye me-2 text-primary"></i>Ver detalle
                            </asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton runat="server" CssClass="dropdown-item"
                                CommandArgument='<%# Eval("IdMaterial") %>'
                                OnCommand="Opciones_Command" CommandName="Editar">
                        <i class="fas fa-edit me-2 text-success"></i>Editar material
                            </asp:LinkButton>
                        </li>
                        <li>
                            <hr class="dropdown-divider">
                        </li>
                        <li>
                            <asp:LinkButton runat="server" CssClass="dropdown-item text-danger"
                                CommandArgument='<%# Eval("IdMaterial") %>'
                                OnCommand="Opciones_Command" CommandName="Eliminar">
                        <i class="fas fa-trash me-2"></i>Eliminar
                            </asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </ItemTemplate>
        </asp:TemplateField>


    </Columns>

</asp:GridView>



        <asp:Button ID="btnVer" runat="server" OnClick="btnVer_Click" Style="display: none" />
        <asp:Button ID="btnEditar" runat="server" OnClick="btnEditar_Click" Style="display: none" />
        <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" Style="display: none" />
    </div>







<!-- Controles de paginación personalizados -->
<div class="pagination-custom">
    <div class="pagination-info">
        <span>Mostrando </span>
        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" 
            OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" CssClass="page-size-dropdown">
            <asp:ListItem Text="10" Value="10" Selected="True" />
            <asp:ListItem Text="25" Value="25" />
            <asp:ListItem Text="50" Value="50" />
            <asp:ListItem Text="100" Value="100" />
        </asp:DropDownList>
        <span> por página</span>
    </div>
    
    <div class="pagination-stats">
        <asp:Label ID="lblPaginationInfo" runat="server" Text="1-10 de 21" />
    </div>
    
    <div class="pagination-controls">
        <asp:LinkButton ID="lnkFirst" runat="server" CommandName="Page" CommandArgument="First" 
            CssClass="page-link" OnClick="lnkFirst_Click">
            <i class="fas fa-angle-double-left"></i>
        </asp:LinkButton>
        
        <asp:LinkButton ID="lnkPrev" runat="server" CommandName="Page" CommandArgument="Prev" 
            CssClass="page-link" OnClick="lnkPrev_Click">
            <i class="fas fa-angle-left"></i>
        </asp:LinkButton>
        
        <asp:Repeater ID="rptPager" runat="server">
            <ItemTemplate>
                <asp:LinkButton ID="lnkPage" runat="server" Text='<%# Eval("Text") %>' 
                    CommandArgument='<%# Eval("Value") %>' CssClass='<%# Eval("Class") %>'
                    OnClick="lnkPage_Click" />
            </ItemTemplate>
        </asp:Repeater>
        
        <asp:LinkButton ID="lnkNext" runat="server" CommandName="Page" CommandArgument="Next" 
            CssClass="page-link" OnClick="lnkNext_Click">
            <i class="fas fa-angle-right"></i>
        </asp:LinkButton>
        
        <asp:LinkButton ID="lnkLast" runat="server" CommandName="Page" CommandArgument="Last" 
            CssClass="page-link" OnClick="lnkLast_Click">
            <i class="fas fa-angle-double-right"></i>
        </asp:LinkButton>
    </div>
</div>




</asp:Content>
