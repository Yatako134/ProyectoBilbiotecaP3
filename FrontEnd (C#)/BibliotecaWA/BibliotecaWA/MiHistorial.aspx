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
        <asp:Panel ID="pnlPrestamos" runat="server" Visible="true" CssClass="d-flex flex-column" Style="height: 500px;">
            <!-- Grid con scroll -->
            <div style="overflow-y: auto; flex: 1 1 auto;">
                <asp:GridView ID="gvPrestamos" runat="server"
                    AutoGenerateColumns="False"
                    AllowPaging="true" PageSize="8"
                    CssClass="table table-hover table-responsive table-striped text-center align-middle text-secondary small"
                    OnPageIndexChanging="gvPrestamos_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="IdPrestamo" HeaderText="Código" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField DataField="Fecha_de_prestamo" HeaderText="Fecha de Inicio" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField DataField="Fecha_vencimiento" HeaderText="Fecha de Vencimiento" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="align-middle" />
                        <asp:BoundField DataField="Fecha_devolucion" HeaderText="Fecha de Devolución" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="align-middle" />
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
                            <asp:ListItem Text="5" Value="5" />
                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="20" Value="20" />
                        </asp:DropDownList>
                        por página
                    </div>
                    <div>
                        <asp:Label ID="lblPaginaInfoPrestamos" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <!-- Panel de Sanciones -->
        <asp:Panel ID="pnlSanciones" runat="server" Visible="false" CssClass="d-flex flex-column" Style="height: 500px;">
            <div style="overflow-y: auto; flex: 1 1 auto;">
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
                            <asp:ListItem Text="20" Value="20" />
                        </asp:DropDownList>
                        por página
                    </div>
                    <div>
                        <asp:Label ID="lblPaginaInfoSanciones" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
