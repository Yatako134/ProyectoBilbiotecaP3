<%@ Page Title="" Language="C#" MasterPageFile="~/HomeEstudiante.Master" AutoEventWireup="true" CodeBehind="MiHistorial.aspx.cs" Inherits="ProyectoP3.MiHistorial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <!-- Superior -->
    <div class="d-flex align-items-center mb-2">
        <i class="fa-solid fa-book fa-sm me-2"></i>
        <span class="fw-bold">Mi historial de préstamos</span>
    </div>

    <div class="container mt-4">

        <!-- === CABECERA SUPERIOR === -->
        <div class="text-start mb-4">
            <h2 class="fw-bold fs-2">Mi Historial</h2>
        </div>

        <!-- Botones de pestaña -->
        <div class="row mb-2">
            <div class="col text-start">
                <asp:LinkButton ID="btnPrestamos" runat="server" CssClass="btn btn-sm btn-outline-primary me-1"
                    OnClick="btnPrestamos_Click">
                    <i class="fa-solid fa-book pe-1"></i> Préstamos
                </asp:LinkButton>

                <asp:LinkButton ID="btnSanciones" runat="server" CssClass="btn btn-sm btn-outline-danger"
                    OnClick="btnSanciones_Click">
                    <i class="fa-solid fa-triangle-exclamation pe-1"></i> Sanciones
                </asp:LinkButton>
            </div>
        </div>

        <!-- Panel de Préstamos -->
        <asp:Panel ID="pnlPrestamos" runat="server" CssClass="d-flex flex-column" Style="height: 500px;">


            <!-- Grid con scroll -->
            <div class="tabla-container shadow-sm rounded-4 overflow-hidden ">

                <!-- === BARRA DE BÚSQUEDA === -->
                <div class="tabla-busqueda d-flex align-items-center p-3 border-bottom bg-white gap-2">

                    <asp:Label ID="lblResultados" runat="server" CssClass="flex-shrink-0 me-3 ColorLetras"></asp:Label>
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control bg-light flex-grow-1" placeholder=" Buscar por código..." TextMode="Number" />
                    <asp:LinkButton ID="btnBuscarPrestamo"
                        runat="server"
                        CssClass="btn btn-sm btn-primary btnBuscarFix"
                        OnClick="btnBuscarPrestamo_Click"
                        UseSubmitBehavior="false">
                        <i class="fa-solid fa-magnifying-glass"></i>
                    </asp:LinkButton>

                </div>

                <!-- === GRIDVIEW === -->
                <asp:GridView ID="gvPrestamos" runat="server"
                    AutoGenerateColumns="False"
                    AllowPaging="true" PageSize="8"
                    CssClass="table table-hover table-responsive table-striped text-center align-middle text-secondary small"
                    OnPageIndexChanging="gvPrestamos_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="IdPrestamo" HeaderText="Código" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField DataField="Fecha_de_prestamo" HeaderText="Fecha de Inicio" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField DataField="Fecha_vencimiento" HeaderText="Fecha de Vencimiento" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="align-middle" />
                        <asp:TemplateField HeaderText="Fecha de Devolución">
                            <ItemTemplate>
                                <%# (Eval("Fecha_devolucion") == null || Convert.ToDateTime(Eval("Fecha_devolucion")) == DateTime.MinValue)? "-" : Convert.ToDateTime(Eval("Fecha_devolucion")).ToString("dd/MM/yyyy") %>
                            </ItemTemplate>
                            <ItemStyle CssClass="align-middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sanción">
                            <ItemTemplate>
                                <%# Eval("Estado").ToString() == "RETRASADO" ? "Sí" : "No" %>
                            </ItemTemplate>
                            <ItemStyle CssClass="align-middle text-center" Width="70px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <%# GetEstadoHtml(Eval("Estado")) %>
                            </ItemTemplate>
                            <ItemStyle CssClass="align-middle text-center" Width="120px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>

            <!-- Contenedor de paginación al fondo -->
            <div style="margin-top: auto; padding-top: 10px; border-top: 1px solid #ddd;">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        Mostrar
                        <asp:DropDownList ID="ddlPageSizePrestamos" runat="server" AutoPostBack="true"
                            CssClass="form-select d-inline-block w-auto"
                            OnSelectedIndexChanged="ddlPageSizePrestamos_SelectedIndexChanged">
                            <asp:ListItem Text="5" Value="5"/>
                            <asp:ListItem Text="10" Value="10"/>
                            <asp:ListItem Text="20" Value="20" Selected="True" />
                        </asp:DropDownList>
                        por página
                    </div>
                    <div>
                        <asp:Label ID="lblPaginaInfoPrestamos" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>

<%--            <!-- === BARRA INFERIOR === -->
            <div class="tabla-footer d-flex justify-content-between align-items-center p-3 bg-light">

                <div class="d-flex align-items-center">
                    <label for="ddlCantidad" class="me-2">Show</label>
                    <asp:DropDownList ID="ddlPageSizePrestamos" runat="server" AutoPostBack="true"
                        CssClass="form-select form-select-sm w-auto"
                        OnSelectedIndexChanged="ddlPageSizePrestamos_SelectedIndexChanged">
                        <asp:ListItem Text="5" Value="5" />
                        <asp:ListItem Text="10" Value="10" Selected="True" />
                        <asp:ListItem Text="25" Value="25" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>
                    <span class="ms-2">per page</span>
                </div>

            </div>--%>

            <asp:Label ID="LabelMensajePrestamo" runat="server"
                Text=""
                Visible="false"
                Style="color: #6c757d; text-align: center; display: block; font-weight: bold;">
            </asp:Label>

        </asp:Panel>

        <!-- Panel de Sanciones -->
        <asp:Panel ID="pnlSanciones" runat="server" Visible="false" CssClass="d-flex flex-column" Style="height: 500px;">
            <div class="tabla-container shadow-sm rounded-4 overflow-hidden ">

                <!-- === BARRA DE BÚSQUEDA === -->
                <div class="tabla-busqueda d-flex align-items-center p-3 border-bottom bg-white gap-2">
                    <asp:Label ID="LabelSancion" runat="server" CssClass="flex-shrink-0 me-3 ColorLetras"></asp:Label>
                    <asp:TextBox ID="TextBoxSancion" runat="server" CssClass="form-control bg-light flex-grow-1" placeholder=" Buscar por código..." TextMode="NUMBER" />
                    <asp:LinkButton ID="btnBuscarSancion"
                        runat="server"
                        CssClass="btn btn-sm btn-primary btnBuscarFix"
                        OnClick="btnBuscarSancion_Click"
                        UseSubmitBehavior="false">
                            <i class="fa-solid fa-magnifying-glass"></i>
                    </asp:LinkButton>
                </div>

                <!-- === GRIDVIEW === -->
                <asp:GridView ID="gvSanciones" runat="server"
                    AutoGenerateColumns="False"
                    AllowPaging="true" PageSize="8"
                    CssClass="table table-hover table-responsive table-striped text-center align-middle text-secondary small"
                    OnPageIndexChanging="gvSanciones_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="id_sancion" HeaderText="Código" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField DataField="id_sancion" HeaderText="Préstamo" ItemStyle-CssClass="align-middle" />
                        <asp:TemplateField HeaderText="Fecha de Inicio">
                            <ItemTemplate>
                                <%# Eval("fecha_inicio", "{0:dd/MM/yyyy}") %>
                            </ItemTemplate>
                            <ItemStyle CssClass="align-middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha de Fin">
                            <ItemTemplate>
                                <%# Eval("fecha_fin", "{0:dd/MM/yyyy}") %>
                            </ItemTemplate>
                            <ItemStyle CssClass="align-middle" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="justificacion" HeaderText="Justificación" ItemStyle-CssClass="align-middle" />
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <%# GetEstadoHtml(Eval("estado")) %>
                            </ItemTemplate>
                            <ItemStyle CssClass="align-middle text-center" Width="120px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <!-- Contenedor de paginación al fondo -->
            <div style="margin-top: auto; padding-top: 10px; border-top: 1px solid #ddd;">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        Mostrar
                        <asp:DropDownList ID="ddlPageSizeSanciones" runat="server" AutoPostBack="true"
                            CssClass="form-select d-inline-block w-auto"
                            OnSelectedIndexChanged="ddlPageSizeSanciones_SelectedIndexChanged">
                            <asp:ListItem Text="5" Value="5" />
                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="20" Value="20" Selected="True" />
                        </asp:DropDownList>
                        por página
                    </div>
                    <div>
                        <asp:Label ID="lblPaginaInfoSanciones" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>

<%--            <!-- Paginación inferior -->
            <div class="tabla-footer d-flex justify-content-between align-items-center p-3 bg-light">
                <div class="d-flex align-items-center">
                    <label for="ddlCantidadSancion" class="me-2">Show</label>
                    <asp:DropDownList ID="ddlPageSizeSanciones" runat="server" AutoPostBack="true"
                        CssClass="form-select form-select-sm w-auto"
                        OnSelectedIndexChanged="ddlPageSizeSanciones_SelectedIndexChanged">
                        <asp:ListItem Text="5" Value="5" />
                        <asp:ListItem Text="10" Value="10" Selected="True" />
                        <asp:ListItem Text="25" Value="25" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>
                    <span class="ms-2">per page</span>
                </div>
            </div>--%>
            <asp:Label ID="lblMensaje" runat="server"
                Text=""
                Visible="false"
                Style="color: #6c757d; text-align: center; display: block; font-weight: bold;">
            </asp:Label>

        </asp:Panel>
    </div>
</asp:Content>
