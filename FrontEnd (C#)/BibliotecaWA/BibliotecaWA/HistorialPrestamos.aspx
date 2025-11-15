<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="HistorialPrestamos.aspx.cs" Inherits="BibliotecaWA.HistorialPrestamos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
    <link href="Fonts/css/GestUsuarios.css" rel="stylesheet" />
    <link href="Fonts/css/UsuariosOpciones.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <!-- Superior -->
    <div class="d-flex align-items-center mb-2">
        <i class="fa-solid fa-book fa-sm me-2"></i>
        <span class="fw-bold">Mi historial de préstamos</span>
    </div>

    <div class="container-fluid">

        <!-- === CABECERA SUPERIOR === -->

        <div class="text-start mb-4">
            <h2 class="fw-bold fs-2">Gestion de prestamos</h2>
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

        <!-- === CONTENEDOR UNIFICADO === -->
        <asp:Panel ID="pnlPrestamos" runat="server" CssClass="tabla-container shadow-sm rounded-4 overflow-hidden">
            <!-- Grid con scroll -->

            <div class="tabla-container shadow-sm rounded-4 overflow-hidden ">

                <!-- === BARRA DE BÚSQUEDA === -->
                <div class="tabla-busqueda d-flex align-items-center p-3 border-bottom bg-white gap-2">

                    <asp:Label ID="lblResultados" runat="server" CssClass="flex-shrink-0 me-3 ColorLetras"></asp:Label>
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control bg-light flex-grow-1" placeholder=" Buscar por usuario..." />
                    <asp:LinkButton ID="btnBuscarPrestamo"
                        runat="server"
                        CssClass="btn btn-sm btn-primary btnBuscarFix"
                        OnClick="btnBuscarPrestamo_Click"
                        UseSubmitBehavior="false">
                        <i class="fa-solid fa-magnifying-glass"></i>
                    </asp:LinkButton>

                </div>

                <!-- === GRIDVIEW === -->
                <div class="tabla-datos">

                    <asp:HiddenField ID="hfPrestamoSeleccionado" runat="server" ClientIDMode="Static" />

                    <asp:GridView ID="gvPrestamos" runat="server"
                        AutoGenerateColumns="False"
                        AllowPaging="true" PageSize="10"
                        CssClass="table table-borderless text-center align-middle m-0"
                        OnPageIndexChanging="gvPrestamos_PageIndexChanging"
                        OnRowDataBound="dgvPrestamo_RowDataBound"
                        PagerSettings-Visible="False">

                        <Columns>
                            <asp:BoundField HeaderText="Código" />
                            <asp:BoundField HeaderText="Usuario" />
                            <asp:BoundField HeaderText="Fecha de inicio" />
                            <asp:BoundField HeaderText="Fecha de vencimiento" />
                            <asp:BoundField HeaderText="Fecha de devolucion" />
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
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <a href="#" class="btn btn-link btnOpciones text-dark fs-4"
                                        onclick='abrirMenuOpciones(<%# Eval("idPrestamo") %>, this); return false;'>⋮</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <asp:Button ID="btnVer" runat="server" OnClick="btnVer_Click" Style="display: none" />
                    <asp:Button ID="btnEditar" runat="server" OnClick="btnEditar_Click" Style="display: none" />
                    <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" Style="display: none" />

                </div>
            </div>
            <!-- === BARRA INFERIOR === -->
            <div class="tabla-footer d-flex justify-content-between align-items-center p-3 bg-light">

                <div class="d-flex align-items-center">
                    <label for="ddlCantidad" class="me-2">Show</label>
                    <asp:DropDownList ID="ddlCantidad" runat="server" AutoPostBack="true"
                        CssClass="form-select form-select-sm w-auto"
                        OnSelectedIndexChanged="ddlCantidad_SelectedIndexChanged">
                        <asp:ListItem Text="5" Value="5" />
                        <asp:ListItem Text="10" Value="10" Selected="True" />
                        <asp:ListItem Text="25" Value="25" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>
                    <span class="ms-2">per page</span>
                </div>

            </div>
            <asp:Label ID="LabelMensajePrestamo" runat="server"
                Text=""
                Visible="false"
                Style="color: #6c757d; text-align: center; display: block; font-weight: bold;">
            </asp:Label>
        </asp:Panel>

        <!-- === MENU OPCIONES  === -->
        <div id="menuOpciones" class="menu-opciones" style="display: none;">
            <button type="button" class="opcion" onclick="__doPostBack('<%= btnVer.UniqueID %>', '')">
                <i class="fa-solid fa-eye me-2 text-primary"></i>Ver detalle
            </button>
            <button type="button" class="opcion" onclick="__doPostBack('<%= btnEditar.UniqueID %>', '')">
                <i class="fa-solid fa-pen me-2 text-secondary"></i>Editar
            </button>
            <button type="button" class="opcion eliminar" onclick="__doPostBack('<%= btnEliminar.UniqueID %>', '')">
                <i class="fa-solid fa-trash me-2 text-danger"></i>Eliminar
            </button>
        </div>



        <!-- === SCRIPT MENU OPCIONES === -->
        <script>
            (function () {
                let menuAbierto = null;

                window.abrirMenuOpciones = function (idUsuario, elemento) {
                    const menu = document.getElementById('menuOpciones');
                    const hidden = document.getElementById('hfPrestamoSeleccionado');

                    if (!menu || !hidden) return;

                    if (menuAbierto && menuAbierto !== menu)
                        menuAbierto.style.display = 'none';

                    hidden.value = idUsuario;

                    const rect = elemento.getBoundingClientRect();
                    const menuWidth = 200;
                    let left = rect.right + window.scrollX + 8;
                    const maxLeft = window.scrollX + window.innerWidth - menuWidth - 8;
                    if (left > maxLeft) left = Math.max(window.scrollX + 8, rect.left + window.scrollX - menuWidth - 8);
                    let top = rect.bottom + window.scrollY + 6;

                    menu.style.position = 'absolute';
                    menu.style.top = top + 'px';
                    menu.style.left = left + 'px';
                    menu.style.display = 'block';
                    menu.style.zIndex = 99999;

                    menuAbierto = menu;
                };

                document.addEventListener('click', function (e) {
                    const menu = document.getElementById('menuOpciones');
                    if (!menu) return;
                    if (menu.style.display !== 'block') return;
                    if (e.target.closest('.btnOpciones') || e.target.closest('#menuOpciones')) return;
                    menu.style.display = 'none';
                    menuAbierto = null;
                });

                document.addEventListener('keydown', function (e) {
                    if (e.key === 'Escape') {
                        const menu = document.getElementById('menuOpciones');
                        if (menu) menu.style.display = 'none';
                        menuAbierto = null;
                    }
                });
            })();
        </script>





        <div class="tabla-container shadow-sm rounded-4 overflow-hidden">

            <!-- Panel de Sanciones -->
            <asp:Panel ID="pnlSanciones" runat="server" Visible="false" CssClass="d-flex flex-column">
                <div class="tabla-container shadow-sm rounded-4 overflow-hidden ">
                    <!-- HiddenField requerido por el menú -->
                    <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />

                    <div class="tabla-busqueda d-flex align-items-center p-3 border-bottom bg-white gap-2">
                        <asp:Label ID="LabelSancion" runat="server" CssClass="flex-shrink-0 me-3 ColorLetras"></asp:Label>
                        <asp:TextBox ID="TextBoxSancion" runat="server" CssClass="form-control bg-light flex-grow-1" placeholder=" Buscar por usuario..." TextMode ="NUMBER" />
                        <asp:LinkButton ID="btnBuscarSancion"
                            runat="server"
                            CssClass="btn btn-sm btn-primary btnBuscarFix"
                            OnClick="btnBuscarSancion_Click"
                            UseSubmitBehavior="false">
                            <i class="fa-solid fa-magnifying-glass"></i>
                        </asp:LinkButton>
                    </div>

                    <asp:GridView ID="gvSanciones" runat="server"
                        AutoGenerateColumns="False"
                        AllowPaging="true" PageSize="8"
                        CssClass="table table-borderless text-center align-middle m-0"
                        OnPageIndexChanging="gvSanciones_PageIndexChanging"
                        OnRowDataBound="dgvSanciones_RowDataBound"
                        PagerSettings-Visible="False">

                        <Columns>
                            <asp:BoundField HeaderText="Código" />
                            <asp:BoundField HeaderText="Usuario" />
                            <asp:BoundField HeaderText="Préstamo">
                                <ItemStyle CssClass="me-5" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Fecha de inicio" />
                            <asp:BoundField HeaderText="Fecha de vencimiento" />
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <%# GetEstadoHtml(Eval("estado")) %>
                                </ItemTemplate>
                                <ItemStyle CssClass="align-middle text-center" Width="120px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <a href="#" class="btn btn-link btnOpciones text-dark fs-4"
                                        onclick='abrirMenuOpcionesSanciones(<%# Eval("id_sancion") %>, this); return false;'>⋮</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
                <!-- Paginación inferior -->
                <div class="tabla-footer d-flex justify-content-between align-items-center p-3 bg-light">
                    <div class="d-flex align-items-center">
                        <label for="ddlCantidadSancion" class="me-2">Show</label>
                        <asp:DropDownList ID="ddlCantidadSancion" runat="server" AutoPostBack="true"
                            CssClass="form-select form-select-sm w-auto"
                            OnSelectedIndexChanged="ddlCantidadSancion_SelectedIndexChanged">
                            <asp:ListItem Text="5" Value="5" />
                            <asp:ListItem Text="10" Value="10" Selected="True" />
                            <asp:ListItem Text="25" Value="25" />
                            <asp:ListItem Text="50" Value="50" />
                            <asp:ListItem Text="100" Value="100" />
                        </asp:DropDownList>
                        <span class="ms-2">per page</span>
                    </div>
                </div>
                <asp:Label ID="lblMensaje" runat="server"
                    Text=""
                    Visible="false"
                    Style="color: #6c757d; text-align: center; display: block; font-weight: bold;">
                </asp:Label>
            </asp:Panel>
        </div>
        <asp:Button ID="btnVerSancion" runat="server" OnClick="btnVerSancion_Click" Style="display: none" />
        <asp:Button ID="btnEditarSancion" runat="server" OnClick="btnEditarSancion_Click" Style="display: none" />
        <asp:Button ID="btnEliminarSancion" runat="server" OnClick="btnEliminarSancion_Click" Style="display: none" />
        <!-- Menú de opciones de sanciones -->
        <div id="menuOpcionesSanciones" class="menu-opciones" style="display: none;">
            <button type="button" class="opcion" onclick="__doPostBack('<%= btnVerSancion.UniqueID %>', '')">
                <i class="fa-solid fa-eye me-2 text-primary"></i>Ver detalle
            </button>
            <button type="button" class="opcion" onclick="__doPostBack('<%= btnEditarSancion.UniqueID %>', '')">
                <i class="fa-solid fa-pen me-2 text-secondary"></i>Editar
            </button>
            <button type="button" class="opcion eliminar" onclick="__doPostBack('<%= btnEliminarSancion.UniqueID %>', '')">
                <i class="fa-solid fa-trash me-2 text-danger"></i>Eliminar
            </button>
        </div>

        <script>
            (function () {

                let menuAbierto = null;

                window.abrirMenuOpcionesSanciones = function (idSancion, elemento) {

                    const menu = document.getElementById('menuOpcionesSanciones');
                    const hidden = document.getElementById('HiddenField1');

                    if (!menu || !hidden) return;

                    if (menuAbierto && menuAbierto !== menu)
                        menuAbierto.style.display = 'none';

                    hidden.value = idSancion;

                    const rect = elemento.getBoundingClientRect();
                    const menuWidth = 200;

                    let left = rect.right + window.scrollX + 8;
                    const maxLeft = window.scrollX + window.innerWidth - menuWidth - 8;

                    if (left > maxLeft)
                        left = Math.max(window.scrollX + 8, rect.left + window.scrollX - menuWidth - 8);

                    let top = rect.bottom + window.scrollY + 6;

                    menu.style.position = 'absolute';
                    menu.style.top = top + 'px';
                    menu.style.left = left + 'px';
                    menu.style.display = 'block';
                    menu.style.zIndex = 99999;

                    menuAbierto = menu;
                };


                // Cerrar haciendo click fuera
                document.addEventListener('click', function (e) {
                    const menu = document.getElementById('menuOpcionesSanciones');
                    if (!menu) return;
                    if (menu.style.display !== 'block') return;

                    if (e.target.closest('.btnOpciones') ||
                        e.target.closest('#menuOpcionesSanciones')) return;

                    menu.style.display = 'none';
                    menuAbierto = null;
                });


                // Cerrar con ESC
                document.addEventListener('keydown', function (e) {
                    if (e.key === 'Escape') {
                        const menu = document.getElementById('menuOpcionesSanciones');
                        if (menu) menu.style.display = 'none';
                        menuAbierto = null;
                    }
                });

            })();
        </script>
    </div>
</asp:Content>

