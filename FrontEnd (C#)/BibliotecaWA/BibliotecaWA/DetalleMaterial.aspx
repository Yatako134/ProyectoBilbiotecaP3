<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="DetalleMaterial.aspx.cs" Inherits="BibliotecaWA.DetalleMaterial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Detalle Material
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
    <script src="Scripts/UtilsArmy/MostrarSeleccionBiblio.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">
    <!-- CARD 1: Datos del Material -->
    <div class="card">
        <div class="card-header">
            <h2>
                <asp:Label ID="lbltitlecard" runat="server" Text="Datos del Material"></asp:Label>
            </h2>
        </div>
        <div class="card-body">

            <div class="mb-3 row">
                <asp:Label ID="lblTitulo" runat="server" CssClass="col-sm-2 col-form-label" Text="Título:"></asp:Label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <!-- Contribuyentes -->
            <div class="mb-3 row align-items-center">
                <asp:Label ID="lblContribuyenteTitulo" runat="server" Text="Contribuyente(s):" CssClass="col-sm-2 col-form-label"></asp:Label>
                <div class="col-sm-8 d-flex align-items-center">
                    <span id="lblContribuyente" runat="server"></span>
                    <a href="#" class="ms-2 text-primary text-decoration-none" data-bs-toggle="modal" data-bs-target="#modalContribuyentes">Ver más
                    </a>
                </div>
            </div>

            <div class="mb-3 row">
                <asp:Label ID="lblTema" runat="server" Text="Tema:" CssClass="col-sm-2 col-form-label"></asp:Label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtTema" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="mb-3 row">
                <asp:Label ID="lblAnhioPubli" CssClass="col-sm-2 form-label" runat="server" Text="Año de Publicación:"></asp:Label>
                <div class="col-sm-8">
                    <asp:TextBox ID="TextAnhioPubli" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="mb-3 row">
                <asp:Label ID="lblTipoMaterial" runat="server" Text="Tipo de Material:" CssClass="col-sm-2 col-form-label"></asp:Label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtTipoMaterial" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="mb-3 row">
                <asp:Label ID="lblIdioma" runat="server" Text="Idioma:" CssClass="col-sm-2 col-form-label"></asp:Label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtIdioma" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="mb-3 row">
                <asp:Label ID="lblNroPaginas" runat="server" Text="Nro. Páginas:" CssClass="col-sm-2 col-form-label"></asp:Label>
                <div class="col-sm-8">
                    <asp:TextBox ID="TextNroPaginas" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
           

            <div class="mb-3 row">
                <asp:Label ID="lblNroEjemplares" runat="server" Text="Nro. Ejemplares:" CssClass="col-sm-2 col-form-label"></asp:Label>
                <div class="col-sm-8">
                    <asp:TextBox ID="TextNroEjemplares" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="mb-3 row">
                <asp:Label ID="lblEditoriales" runat="server" Text="Editorial:" CssClass="col-sm-2 col-form-label"></asp:Label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtEditoriales" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <!-- Editoriales
            <div ID="Editoriales" runat="server" class="mb-3 row align-items-center">
                <asp:Label ID="lblEditorialTitulo" runat="server" Text="Editorial(es):" CssClass="col-sm-2 col-form-label"></asp:Label>
                <div class="col-sm-8 d-flex align-items-center">
                    <span id="lblEditorial1" runat="server"></span>
                    <a id="lnkVerMasEditoriales" runat="server" 
                        href="#" class="ms-2 text-primary text-decoration-none" data-bs-toggle="modal" data-bs-target="#modalEditoriales">Ver más</a>
                </div>
            </div>
            -->
            <div class="mb-3 row align-items-center">
                <asp:Label ID="lblEstadoT" runat="server" Text="Estado:" CssClass="col-sm-2 col-form-label"></asp:Label>
                <div class="col-sm-8">
                    <span id="lblEstado" runat="server" class="badge rounded-pill px-3 py-2"></span>
                </div>
            </div>
            <div id="Edicion" runat="server" class="mb-3 row">
                <asp:Label ID="lblEdicion" runat="server" Text="Edicion:" CssClass="col-sm-2 col-form-label"></asp:Label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtEdicion" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div id="ISBN" runat="server" class="mb-3 row">
                <asp:Label ID="lblISBN" runat="server" Text="ISBN:" CssClass="col-sm-2 col-form-label"></asp:Label>
                <div class="col-sm-8">
                    <asp:TextBox ID="TextISBN" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <!-- Institución de Publicación -->
            <div ID="Institucion" runat ="server" class="mb-3 row">
                <asp:Label ID="lblInstitucionPublicacion" runat="server" Text="Institución de Publicación:" CssClass="col-sm-2 col-form-label"></asp:Label>
                <div class="col-sm-8">
                    <asp:TextBox ID="TextInstitucionPublicacion" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <!-- Especialidad -->
            <div  ID="Especialidad" runat ="server" class="mb-3 row">
                <asp:Label ID="lblEspecialidad" runat="server" Text="Especialidad:" CssClass="col-sm-2 col-form-label"></asp:Label>
                <div class="col-sm-8">
                    <asp:TextBox ID="TextEspecialidad" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <!-- Asesor -->
            <div ID="Asesor" runat ="server" class="mb-3 row">
                <asp:Label ID="lblAsesor" runat="server" Text="Asesor:" CssClass="col-sm-2 col-form-label"></asp:Label>
                <div class="col-sm-8">
                    <asp:TextBox ID="TextAsesor" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <!-- Grado -->
            <div ID="Grado" runat ="server" class="mb-3 row">
                <asp:Label ID="lblGrado" runat="server" Text="Grado:" CssClass="col-sm-2 col-form-label"></asp:Label>
                <div class="col-sm-8">
                    <asp:TextBox ID="TextGrado" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <!-- ISSN -->
            <div ID="ISSN" runat ="server" class="mb-3 row">
                <asp:Label ID="lblISSN" runat="server" Text="ISSN:" CssClass="col-sm-2 col-form-label"></asp:Label>
                <div class="col-sm-8">
                    <asp:TextBox ID="TextISSN" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <!-- Revista -->
            <div  ID="Revista" runat ="server" class="mb-3 row">
                <asp:Label ID="lblRevista" runat="server" Text="Revista:" CssClass="col-sm-2 col-form-label"></asp:Label>
                <div class="col-sm-8">
                    <asp:TextBox ID="TextRevista" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <!-- Volumen -->
            <div ID="Volumen" runat ="server" class="mb-3 row">
                <asp:Label ID="lblVolumen" runat="server" Text="Volumen:" CssClass="col-sm-2 col-form-label"></asp:Label>
                <div class="col-sm-8">
                    <asp:TextBox ID="TextVolumen" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <!-- Número -->
            <div ID="Número" runat ="server" class="mb-3 row">
                <asp:Label ID="lblNumero" runat="server" Text="Número:" CssClass="col-sm-2 col-form-label"></asp:Label>
                <div class="col-sm-8">
                    <asp:TextBox ID="TextNumero" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            

        </div>
    </div>
        <!-- Modal de Contribuyentes -->
    <div class="modal fade" id="modalContribuyentes" tabindex="-1" aria-labelledby="modalContribuyentesLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalContribuyentesLabel">Contribuyentes</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <asp:Repeater ID="rptContribuyentes" runat="server">
                        <ItemTemplate>
                            <%# Eval("Nombre") + " " + Eval("Primer_apellido") + " " + Eval("Segundo_apellido") %> (<%# Eval("Tipo_contribuyente").ToString().ToLower() %>)                           
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
        <!-- Modal de Editoriales -->
    <div class="modal fade" id="modalEditoriales" tabindex="-1" aria-labelledby="modalEditorialesLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalEditorialesLabel">Editoriales</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <asp:Repeater ID="rptEditoriales" runat="server">
                        <ItemTemplate>
                            <%# Eval("nombre") %><br />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- CARD 2: Módulo de préstamo -->
    <div class="card mt-4">
        <div class="card-header">
            <h2>Módulo de Préstamo</h2>
        </div>
        <div class="card-body">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />

            <asp:UpdatePanel ID="upBibliotecas" runat="server">
            <ContentTemplate>
            <!-- Selector de biblioteca -->
                <div ID="SelectorBiblioteca" runat="server"  class="mb-3">
                    <asp:Label ID="lblBiblioteca" CssClass="form-label" runat="server" Text="Biblioteca"></asp:Label>
                    <div class="input-group">
                        <asp:DropDownList ID="ddlbibliotecas" CssClass="form-select" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlbibliotecas_SelectedIndexChanged">
                            <asp:ListItem Text="Seleccione una opción" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnPrestar" runat="server" CssClass="btn btn-primary" Text="Prestar" OnClick="btnPrestar_Click" />
                    </div>
                </div>
                <asp:Panel ID="panelSinEjemplares" runat="server" Visible="false" CssClass="alert alert-warning">
                    No hay ejemplares disponibles en ninguna biblioteca.
                </asp:Panel>
                <asp:Panel ID="panel1" runat="server" Visible="false"
                    CssClass="alert alert-warning mt-2">
                </asp:Panel>
                <!-- Repeater para mostrar los ejemplares agrupados -->
                <asp:Repeater ID="rptBibliotecas" runat="server">
                    <ItemTemplate>
                        <div class="card mb-3 border border-secondary-subtle">
                            <div class="card-header bg-light fw-bold">
                                <%# Eval("Nombre") %>
                                <%--<span class="text-muted small">
                                    Disponibles: <%# Eval("Disponibles") %> · Prestados: <%# Eval("Prestados") %>
                                </span>--%>
                            </div>
                            <div class="card-body">
                                <asp:Repeater ID="rptEjemplares" runat="server" DataSource='<%# Eval("Ejemplares") %>'>
                                    <ItemTemplate>
                                        <div class="border rounded p-2 mb-2">
                                            <div><strong>Código:</strong> <%# Eval("IdEjemplar") %></div>
                                            <div><strong>Ubicación:</strong> <%# Eval("Ubicacion") %></div>
                                            <div>
                                                <strong>Estado:</strong>
                                               <span class='<%# ((BibliotecaWA.DetalleMaterial)Page).GetEstadoCss(Eval("estado").ToString()) %>'>
                                                   <%# Eval("estado") %>
                                               </span>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="modalSeleccion" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-warning text-dark">
                    <h5 class="modal-title">Advertencia <i class="fa-solid fa-triangle-exclamation"></i></h5>
                </div>
                <div class="modal-body">
                    Debe seleccionar una biblioteca antes de continuar.
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
