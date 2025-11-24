<%--<%@ Page Title="" Language="C#" MasterPageFile="~/HomeEstudiante.Master" AutoEventWireup="true" CodeBehind="BusquedaMaterialesEstudiante.aspx.cs" Inherits="BibliotecaWA.BusquedaMaterialesEstudiante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Busqueda de Material
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <!-- Cabecera -->
    <div class="d-flex align-items-center mb-2">
        <i class="fa-solid fa-book fa-sm me-2"></i>
        <span class="fw-bold">Búsqueda</span>
    </div>

    <!-- Contenedor principal -->
    <div class="p-4 border rounded">

        <!-- Panel superior -->
        <div class="d-flex align-items-center mb-3">
            <asp:Label ID="LabelBusqueda" runat="server" CssClass="flex-shrink-0 me-3 ColorLetras"></asp:Label>
            <asp:TextBox ID="txtBusqueda" runat="server" CssClass="form-control flex-grow-1 me-2" Placeholder="Ingrese título o autor" MaxLength="150"></asp:TextBox>
            <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary me-2" Text="Buscar" OnClick="btnBuscar_Click" />
            <!-- Botón de búsqueda avanzada -->
            <asp:Button ID="btnBusquedaAvanzada"
                runat="server"
                Text="Búsqueda Avanzada"
                CssClass="btn btn-light text-primary border-primary"
                data-bs-toggle="modal"
                data-bs-target="#modalBusquedaAvanzada"
                OnClientClick="return false;" />


            <!-- Modal de Búsqueda Avanzada -->
            <div class="modal fade" id="modalBusquedaAvanzada" tabindex="-1" aria-labelledby="tituloModalBusquedaAvanzada" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">

                        <!-- Cabecera -->
                        <div class="modal-header">
                            <h5 class="modal-title fw-bold" id="tituloModalBusquedaAvanzada">Búsqueda Avanzada</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                        </div>

                        <!-- Cuerpo -->
                        <div class="modal-body">

                            <!-- Título -->
                            <div class="mb-3">
                                <label class="form-label">Título</label>
                                <asp:TextBox ID="txtTituloAvanzado" runat="server" CssClass="form-control" />
                            </div>

                            <!-- Tipo de contribuyente + Nombre -->
                            <div class="row mb-3">
                                <div class="col-5">
                                    <label class="form-label">Tipo de contribuyente</label>
                                    <asp:DropDownList ID="ddlContribuyente" runat="server" CssClass="form-select">
                                        <asp:ListItem>AUTOR</asp:ListItem>
                                        <asp:ListItem>TRADUCTOR</asp:ListItem>
                                        <asp:ListItem>EDITOR</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-7">
                                    <label class="form-label">Nombre</label>
                                    <asp:TextBox ID="txtNombreContribuyente" runat="server" CssClass="form-control" />
                                </div>
                            </div>

                            <!-- Tema -->
                            <div class="mb-3">
                                <label class="form-label">Tema</label>
                                <asp:TextBox ID="txtTema" runat="server" CssClass="form-control" />
                            </div>

                            <!-- Años -->
                            <div class="row">
                                <div class="col">
                                    <label class="form-label">Año de publicación (desde)</label>
                                    <asp:TextBox ID="txtAnioDesde" runat="server" CssClass="form-control" TextMode="Number" />
                                </div>
                                <div class="col">
                                    <label class="form-label">Año de publicación (hasta)</label>
                                    <asp:TextBox ID="txtAnioHasta" runat="server" CssClass="form-control" TextMode="Number" />
                                </div>
                            </div>

                            <!-- Tipo de material -->
                            <div class="mt-3">
                                <label class="form-label">Tipo de material</label>
                                <asp:DropDownList ID="ddlTipoMaterial" runat="server" CssClass="form-select">
                                    <asp:ListItem>--Seleccione el material--</asp:ListItem>
                                    <asp:ListItem>Libro</asp:ListItem>
                                    <asp:ListItem>Artículo</asp:ListItem>
                                    <asp:ListItem>Tesis</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <!-- Biblioteca -->
                            <div class="mt-3">
                                <label class="form-label">Biblioteca</label>
                                <asp:DropDownList ID="ddlBiblioteca" runat="server" CssClass="form-select">
                                </asp:DropDownList>
                            </div>

                            <!-- Disponibilidad -->
                            <div class="mt-3">
                                <label class="form-label">Disponibilidad</label>
                                <asp:DropDownList ID="ddlDisponibilidad" runat="server" CssClass="form-select">
                                    <asp:ListItem>--Seleccione--</asp:ListItem>
                                    <asp:ListItem>DISPONIBLE</asp:ListItem>
                                    <asp:ListItem>NO_DISPONIBLE</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>

                        <!-- Pie -->
                        <!-- Pie -->
                        <div class="modal-footer d-flex justify-content-end">

                            <!-- Botón Cancelar -->
                            <button type="button" class="btn border border-primary text-primary fw-bold bg-white me-2" data-bs-dismiss="modal">
                                Cancelar
                            </button>

                            <!-- Botón Limpiar -->
                            <button type="button" class="btn btn-light text-primary fw-bold d-flex align-items-center justify-content-center gap-1 border border-info me-2"
                                onclick="
            document.getElementById('<%= txtTituloAvanzado.ClientID %>').value = '';
            document.getElementById('<%= txtNombreContribuyente.ClientID %>').value = '';
            document.getElementById('<%= txtTema.ClientID %>').value = '';
            document.getElementById('<%= txtAnioDesde.ClientID %>').value = '';
            document.getElementById('<%= txtAnioHasta.ClientID %>').value = '';
            document.getElementById('<%= ddlContribuyente.ClientID %>').selectedIndex = 0;
            document.getElementById('<%= ddlTipoMaterial.ClientID %>').selectedIndex = 0;
            document.getElementById('<%= ddlBiblioteca.ClientID %>').selectedIndex = 0;
            document.getElementById('<%= ddlDisponibilidad.ClientID %>').selectedIndex = 0;
        ">
                                Limpiar
                            </button>


                            <!-- Botón Buscar -->
                            <asp:Button ID="btnBuscarAvanzado" runat="server" CssClass="btn btn-primary text-white fw-bold" Text="Buscar" OnClick="btnBuscarAvanzado_Click" />

                        </div>

                    </div>
                </div>
            </div>

        </div>

        <!-- Grid de resultados -->
        <asp:GridView ID="gvResultados" runat="server" AutoGenerateColumns="false" CssClass="table"
            GridLines="None" ShowHeader="false"
            AllowPaging="true" PageSize="5"
            OnPageIndexChanging="gvResultados_PageIndexChanging">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="d-flex align-items-stretch border p-2 mb-2 rounded shadow-sm">
                            <!-- Imagen -->
                            <asp:Image ID="imgTipo" runat="server" CssClass="me-3 rounded" Width="180px" Height="180px"
                                ImageUrl='<%# GetTipoImagen(Eval("Tipo")) %>' />

                            <!-- Info -->
                            <div class="flex-grow-1 d-flex flex-column justify-content-between">
                                <div>
                                    <h5 class="mb-1 fw-bold"><%# Eval("titulo") %></h5>
                                    <small class="text-muted">Autor(es): <%# Eval("AutoresTexto") %></small><br />
                                    <small class="text-muted">Año: <%# Eval("Anho_publicacion") %></small><br />

                                    <div class="mt-1">
                                        <span class="badge rounded-pill bg-primary text-white me-2"><%# Eval("Tipo") %></span>
                                        <span class="badge rounded-pill text-white me-2" style="background-color: #9b59b6;"><%# Eval("clasificacion_tematica") %></span>
                                        <span class='badge rounded-pill text-white <%# Convert.ToInt32(Eval("CantidadDisponible")) > 0 ? "bg-success" : "bg-danger" %>'>
                                            <%# Convert.ToInt32(Eval("CantidadDisponible")) > 0 ? "Disponible" : "No disponible" %>
                                        </span>
                                    </div>

                                    <div>
                                        <small class="text-muted">Disponible(s) para préstamo: <%# Eval("CantidadDisponible") %></small>
                                    </div>

                                    <div>
                                        <small class="text-muted">Biblioteca(s): <%# Eval("BibliotecasTexto") %></small><br />
                                    </div>
                                </div>

                                <div class="text-end mt-2">
                                    <asp:Button ID="btnSolicitar" runat="server"
                                        Text="Ver detalle"
                                        CssClass="btn btn-sm btn-primary"
                                        CommandArgument='<%# Eval("IdMaterial") %>'
                                        OnClick="btnSolicitar_Click" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>

        <!-- Selector de página y contador -->
        <div class="d-flex justify-content-between align-items-center mt-3">
            <div>
                Mostrar
                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true"
                    CssClass="form-select d-inline-block w-auto"
                    OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                    <asp:ListItem Text="5" Value="5" />
                    <asp:ListItem Text="10" Value="10" />
                    <asp:ListItem Text="20" Value="20" />
                </asp:DropDownList>
                por página
            </div>
            <div>
                <asp:Label ID="lblPaginaInfo" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <asp:Label ID="lblMensaje" runat="server"
        Text=""
        Visible="false"
        Style="color: #6c757d; text-align: center; display: block; font-weight: bold;">
    </asp:Label>
    <!-- Estilos extra -->
    <style>
        .table tr > td {
            border: none !important;
        }

        .gridview-pager {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding-top: 10px;
        }
    </style>
</asp:Content>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/HomeEstudiante.Master" AutoEventWireup="true" CodeBehind="BusquedaMaterialesEstudiante.aspx.cs" Inherits="BibliotecaWA.BusquedaMaterialesEstudiante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Busqueda de Material
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">

    <div class="d-flex align-items-center mb-4">
        <!-- Icono -->
        <i class="fas fa-th" style="margin-right: 10px; font-size: 24px;"></i>
        <div style="border-right: 2px solid #ccc; height: 24px; margin-right: 10px;"></div>
        <!-- Texto -->
        <p1>Búsqueda</p1>
    </div>

    <div class="tabla-header d-flex justify-content-between align-items-center p-3">
<h1><strong>Búsqueda y Préstamos</strong></h1>
    </div>


    <!-- Panel superior de búsqueda -->
    <div class="mb-4">
        <div class="d-flex align-items-center">
            <asp:Label ID="LabelBusqueda" runat="server" CssClass="flex-shrink-0 me-3 ColorLetras"></asp:Label>
            <asp:TextBox ID="txtBusqueda" runat="server" CssClass="form-control flex-grow-1 me-2" Placeholder="Ingrese título o autor" MaxLength="150"></asp:TextBox>
            <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary me-2" Text="Buscar" OnClick="btnBuscar_Click" />
            <!-- Botón de búsqueda avanzada -->
            <asp:Button ID="btnBusquedaAvanzada"
                runat="server"
                Text="Búsqueda Avanzada"
                CssClass="btn btn-outline-primary"
                data-bs-toggle="modal"
                data-bs-target="#modalBusquedaAvanzada"
                OnClientClick="return false;" />
        </div>
    </div>

    <!-- Modal de Búsqueda Avanzada -->
    <div class="modal fade" id="modalBusquedaAvanzada" tabindex="-1" aria-labelledby="tituloModalBusquedaAvanzada" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <!-- Cabecera -->
                <div class="modal-header">
                    <h5 class="modal-title fw-bold" id="tituloModalBusquedaAvanzada">Búsqueda Avanzada</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>

                <!-- Cuerpo -->
                <div class="modal-body">
                    <!-- Título -->
                    <div class="mb-3">
                        <label class="form-label">Título</label>
                        <asp:TextBox ID="txtTituloAvanzado" runat="server" CssClass="form-control" />
                    </div>

                    <!-- Tipo de contribuyente + Nombre -->
                    <div class="row mb-3">
                        <div class="col-5">
                            <label class="form-label">Tipo de contribuyente</label>
                            <asp:DropDownList ID="ddlContribuyente" runat="server" CssClass="form-select">
                                <asp:ListItem>AUTOR</asp:ListItem>
                                <asp:ListItem>TRADUCTOR</asp:ListItem>
                                <asp:ListItem>EDITOR</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-7">
                            <label class="form-label">Nombre</label>
                            <asp:TextBox ID="txtNombreContribuyente" runat="server" CssClass="form-control" />
                        </div>
                    </div>

                    <!-- Tema -->
                    <div class="mb-3">
                        <label class="form-label">Tema</label>
                        <asp:TextBox ID="txtTema" runat="server" CssClass="form-control" />
                    </div>

                    <!-- Años -->
                    <div class="row">
                        <div class="col">
                            <label class="form-label">Año de publicación (desde)</label>
                            <asp:TextBox ID="txtAnioDesde" runat="server" CssClass="form-control" TextMode="Number" />
                        </div>
                        <div class="col">
                            <label class="form-label">Año de publicación (hasta)</label>
                            <asp:TextBox ID="txtAnioHasta" runat="server" CssClass="form-control" TextMode="Number" />
                        </div>
                    </div>

                    <!-- Tipo de material -->
                    <div class="mt-3">
                        <label class="form-label">Tipo de material</label>
                        <asp:DropDownList ID="ddlTipoMaterial" runat="server" CssClass="form-select">
                            <asp:ListItem>--Seleccione el material--</asp:ListItem>
                            <asp:ListItem>Libro</asp:ListItem>
                            <asp:ListItem>Artículo</asp:ListItem>
                            <asp:ListItem>Tesis</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <!-- Biblioteca -->
                    <div class="mt-3">
                        <label class="form-label">Biblioteca</label>
                        <asp:DropDownList ID="ddlBiblioteca" runat="server" CssClass="form-select">
                        </asp:DropDownList>
                    </div>

                    <!-- Disponibilidad -->
                    <div class="mt-3">
                        <label class="form-label">Disponibilidad</label>
                        <asp:DropDownList ID="ddlDisponibilidad" runat="server" CssClass="form-select">
                            <asp:ListItem>--Seleccione--</asp:ListItem>
                            <asp:ListItem>DISPONIBLE</asp:ListItem>
                            <asp:ListItem>NO_DISPONIBLE</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- Pie del Modal -->
                <div class="modal-footer d-flex justify-content-end">
                    <!-- Botón Cancelar -->
                    <button type="button" class="btn btn-outline-danger me-2" data-bs-dismiss="modal">
                        Cancelar
                    </button>

                    <!-- Botón Limpiar -->
                    <button type="button" class="btn btn-outline-secondary me-2"
                        onclick="
                        document.getElementById('<%= txtTituloAvanzado.ClientID %>').value = '';
                        document.getElementById('<%= txtNombreContribuyente.ClientID %>').value = '';
                        document.getElementById('<%= txtTema.ClientID %>').value = '';
                        document.getElementById('<%= txtAnioDesde.ClientID %>').value = '';
                        document.getElementById('<%= txtAnioHasta.ClientID %>').value = '';
                        document.getElementById('<%= ddlContribuyente.ClientID %>').selectedIndex = 0;
                        document.getElementById('<%= ddlTipoMaterial.ClientID %>').selectedIndex = 0;
                        document.getElementById('<%= ddlBiblioteca.ClientID %>').selectedIndex = 0;
                        document.getElementById('<%= ddlDisponibilidad.ClientID %>').selectedIndex = 0;
                    ">
                        Limpiar
                    </button>

                    <!-- Botón Buscar -->
                    <asp:Button ID="btnBuscarAvanzado" runat="server" CssClass="btn btn-primary" Text="Buscar" OnClick="btnBuscarAvanzado_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- CONTENEDOR PRINCIPAL CON DISEÑO REFINADO -->
    <div class="refined-container">
        <!-- Grid de resultados -->
        <asp:GridView ID="gvResultados" runat="server" AutoGenerateColumns="false" CssClass="table"
            GridLines="None" ShowHeader="false"
            AllowPaging="true" PageSize="5"
            OnPageIndexChanging="gvResultados_PageIndexChanging">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <!-- CARD CON DISEÑO REFINADO -->
                        <div class="refined-card">
                            <!-- Imagen -->
                            <asp:Image ID="imgTipo" runat="server" CssClass="book-image"
                                ImageUrl='<%# GetTipoImagen(Eval("Tipo")) %>' />

                            <!-- Info -->
                            <div class="card-content">
                                <div class="card-info">
                                    <h5 class="book-title"><%# Eval("titulo") %></h5>
                                    <small class="text-muted">Autor(es): <%# Eval("AutoresTexto") %></small><br />
                                    <small class="text-muted">Año: <%# Eval("Anho_publicacion") %></small><br />

                                    <div class="badges-container">
                                        <span class="badge badge-type"><%# Eval("Tipo") %></span>
                                        <span class="badge badge-category"><%# Eval("clasificacion_tematica") %></span>
                                        <span class='badge <%# Convert.ToInt32(Eval("CantidadDisponible")) > 0 ? "badge-available" : "badge-unavailable" %>'>
                                            <%# Convert.ToInt32(Eval("CantidadDisponible")) > 0 ? "Disponible" : "No disponible" %>
                                        </span>
                                    </div>

                                    <div class="availability-info">
                                        <small class="text-muted">Disponible(s) para préstamo: <%# Eval("CantidadDisponible") %></small>
                                    </div>

                                    <div class="library-info">
                                        <small class="text-muted">Biblioteca(s): <%# Eval("BibliotecasTexto") %></small><br />
                                    </div>
                                </div>

                                <div class="card-actions">
                                    <!-- BOTÓN VER DETALLE CON BOOTSTRAP -->
                                    <asp:Button ID="btnSolicitar" runat="server"
                                        Text="Ver detalle"
                                        CssClass="btn btn-primary btn-sm"
                                        CommandArgument='<%# Eval("IdMaterial") %>'
                                        OnClick="btnSolicitar_Click" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <!-- Selector de página y contador -->
        <div class="pagination-container">
            <div class="page-size-selector">
                Mostrar
                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true"
                    CssClass="form-select d-inline-block w-auto"
                    OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                    <asp:ListItem Text="5" Value="5" />
                    <asp:ListItem Text="10" Value="10" />
                    <asp:ListItem Text="20" Value="20" />
                </asp:DropDownList>
                por página
            </div>
            <div class="page-info">
                <asp:Label ID="lblPaginaInfo" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>

    <asp:Label ID="lblMensaje" runat="server"
        Text=""
        Visible="false"
        Style="color: #6c757d; text-align: center; display: block; font-weight: bold; margin-top: 20px;">
    </asp:Label>

    <!-- ESTILOS REFINADOS COMO BUSQUEDAMATERIALAS -->
    <style>
        /* Contenedor principal refinado */
        .refined-container {
            background: white;
            border: 1px solid #f0f0f0;
            border-radius: 16px;
            padding: 28px;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.04);
        }

        /* Cards refinadas */
        .refined-card {
            display: flex;
            align-items: stretch;
            padding: 20px;
            margin-bottom: 20px;
            background: white;
            border: 1px solid #f5f5f5;
            border-radius: 12px;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.02);
            transition: all 0.25s ease;
        }

        .refined-card:hover {
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.06);
            border-color: #e8e8e8;
            transform: translateY(-1px);
        }

        /* Imagen del libro */
        .book-image {
            width: 180px;
            height: 180px;
            border-radius: 10px;
            margin-right: 24px;
            object-fit: cover;
            border: 1px solid #f0f0f0;
        }

        /* Contenido de la card */
        .card-content {
            flex-grow: 1;
            display: flex;
            flex-direction: column;
            justify-content: space-between;
        }

        .card-info {
            flex: 1;
        }

        .book-title {
            margin-bottom: 10px;
            font-weight: 600;
            color: #1a1a1a;
            font-size: 1.2rem;
        }

        /* Badges */
        .badges-container {
            margin: 14px 0;
        }

        .badge {
            padding: 5px 12px;
            border-radius: 14px;
            font-size: 0.8rem;
            font-weight: 500;
            margin-right: 10px;
        }

        .badge-type {
            background-color: #006DEB;
            color: white;
        }

        .badge-category {
            background-color: #9b59b6;
            color: white;
        }

        .badge-available {
            background-color: #10b981;
            color: white;
        }

        .badge-unavailable {
            background-color: #ef4444;
            color: white;
        }

        /* Información adicional */
        .availability-info,
        .library-info {
            margin-top: 8px;
        }

        /* Acciones */
        .card-actions {
            text-align: right;
            margin-top: 16px;
        }

        /* Paginación */
        .pagination-container {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-top: 28px;
            padding-top: 24px;
            border-top: 1px solid #f0f0f0;
        }

        .page-size-selector {
            font-size: 0.9rem;
            color: #6b7280;
        }

        .page-info {
            font-size: 0.9rem;
            color: #6b7280;
        }

        /* Tabla sin estilos */
        .table tr > td {
            border: none !important;
            padding: 0 !important;
            background: transparent !important;
        }
    </style>
</asp:Content>