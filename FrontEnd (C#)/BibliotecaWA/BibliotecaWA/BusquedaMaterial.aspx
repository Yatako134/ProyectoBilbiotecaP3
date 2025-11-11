<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="BusquedaMaterial.aspx.cs" Inherits="BibliotecaWA.BusquedaMaterial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Busqueda Material
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <!-- Cabecera -->
    <div class="d-flex align-items-center mb-2">
        <i class="fa-solid fa-book fa-sm me-2"></i>
        <span class="fw-bold">Búsqueda y Préstamos</span>
    </div>

    <!-- Contenedor principal -->
    <div class="p-4 border rounded">

        <!-- Panel superior -->
        <div class="d-flex align-items-center mb-3">
            <asp:TextBox ID="txtBusqueda" runat="server" CssClass="form-control flex-grow-1 me-2" Placeholder="Ingrese título o autor"></asp:TextBox>
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
              <asp:ListItem>Autor</asp:ListItem>
              <asp:ListItem>Ilustrador</asp:ListItem>
              <asp:ListItem>Editor</asp:ListItem>
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

        <!-- Palabras clave -->
        <div class="mt-3">
          <label class="form-label">Palabras clave</label>
          <asp:TextBox ID="txtPalabrasClave" runat="server" CssClass="form-control" />
        </div>

        <!-- Tipo de material -->
        <div class="mt-3">
          <label class="form-label">Tipo de material</label>
          <asp:DropDownList ID="ddlTipoMaterial" runat="server" CssClass="form-select">
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
            <asp:ListItem>DISPONIBLE</asp:ListItem>
            <asp:ListItem>NO DISPONIBLE</asp:ListItem>
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
  <asp:Button ID="btnLimpiarAvanzado" runat="server"
      CssClass="btn btn-light text-primary fw-bold d-flex align-items-center justify-content-center gap-1 border border-info me-2"

      Text='Limpiar'
      OnClick="btnLimpiarAvanzado_Click"
      UseSubmitBehavior="false" />

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
                                    <h5 class="mb-1 fw-bold"><%# Eval("Titulo") %></h5>
                                    <small class="text-muted">Autor(es): <%# Eval("AutoresTexto") %></small><br />
                                    <small class="text-muted">Año: <%# Eval("Anho_publicacion") %></small><br />

                                    <div class="mt-1">
                                        <span class="badge rounded-pill bg-primary text-white me-2"><%# Eval("Tipo") %></span>
                                        <span class="badge rounded-pill text-white me-2" style="background-color: #9b59b6;"><%# Eval("clasificacion_tematica") %></span>
                                        <span class='badge rounded-pill text-white <%# Convert.ToInt32(Eval("CantidadDisponibles")) > 0 ? "bg-success" : "bg-danger" %>'>
                                            <%# Convert.ToInt32(Eval("CantidadDisponibles")) > 0 ? "Disponible" : "No disponible" %>
                                        </span>
                                    </div>

                                    <div>
                                        <small class="text-muted">Disponible(s) para préstamo: <%# Eval("CantidadDisponibles") %></small>
                                    </div>

                                    <div>
                                        <small class="text-muted">Biblioteca(s): <%# Eval("BibliotecasTexto") %></small><br />
                                    </div>
                                </div>

                                <div class="text-end mt-2">
                                    <asp:Button ID="btnSolicitar" runat="server"
                                        Text="Solicitar Préstamo"
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

    <!-- Estilos extra -->
    <style>
        .table tr > td { border: none !important; }
        .gridview-pager {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding-top: 10px;
        }
    </style>
</asp:Content>
