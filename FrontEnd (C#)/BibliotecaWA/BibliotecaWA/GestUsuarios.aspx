<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="GestUsuarios.aspx.cs" Inherits="BibliotecaWA.GestUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
    Gestión de usuarios
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">

    <link href="Fonts/css/UsuariosOpciones.css" rel="stylesheet" />
    <link href="Fonts/css/ContornoRol.css" rel="stylesheet" />
    <link href="Fonts/css/GestUsuarios.css" rel="stylesheet" />

    <script src="/Scripts/usuarios-modal.js"></script>

    <style>
        .custom-modal {
            position: fixed;
            top: 20px; /* parte superior */
            left: 50%;
            transform: translateX(-50%);
            width: 90%; /* ancho responsivo */
            max-width: 600px;
            z-index: 9999;
            display: none; /* oculto inicialmente */
            opacity: 0; /* inicio invisible */
            transition: opacity 1s ease; /* transición suave */
        }

            .custom-modal.show {
                display: block;
                opacity: 1; /* visible */
            }

        .custom-modal-content {
            background-color: #f0fdf4; /* fondo solicitado */
            border: 2px solid #22c55e; /* borde visible y verde */
            border-radius: 8px;
            padding: 15px 20px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.2);
            text-align: center;
        }

            .custom-modal-content h4 {
                margin: 0 0 10px 0;
                color: #071437; /* título color oscuro */
                font-weight: bold; /* título en negrita */
            }

            .custom-modal-content p {
                margin: 0;
                font-weight: 500;
                color: #333;
            }
    </style>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">

    <div class="titulo-usuarios">
        <h6 class="ColorLetras fw-bold"> Gestión de Usuarios</h6>
    </div>
    <hr>

    <!-- Modal personalizado -->
    <div id="modalRegistroUsuario" class="custom-modal">
        <div class="custom-modal-content">
            <h4 class="modal-header">Registro de usuario</h4>
            <p class="modal-body">El usuario "Nombre" ha sido registrado correctamente.</p>
        </div>
    </div>

    <script>
        function mostrarModal(nombre) {
            var modal = document.getElementById("modalRegistroUsuario");
            modal.querySelector(".modal-body").textContent = `El usuario "${nombre}" ha sido registrado correctamente.`;

            // Mostrar modal
            modal.classList.add("show");

            // Espera 4.5 segundos antes de empezar a desvanecer
            setTimeout(function () {
                modal.style.opacity = "0"; // comienza a desaparecer
                // Después de la transición de 1s, ocultamos completamente
                setTimeout(function () {
                    modal.classList.remove("show");
                }, 1000);
            }, 4500); // 4.5 segundos visible antes de desvanecer
        }
    </script>

    <div class="container-fluid">

        <!-- === CABECERA SUPERIOR === -->
        <div class="tabla-header d-flex justify-content-between align-items-center p-3 ColorLetras">
            <h2 class="m-0 fw-semibold">Usuarios Registrados</h2>
            <asp:LinkButton ID="lkRegistrar" CssClass="btn btn-primary" runat="server"
                OnClick="btnRegistrar_click"
                Text="<i class='fa-solid fa-plus pe-2'></i> Añadir Usuario" />
        </div>

        <!-- === CONTENEDOR UNIFICADO === -->
        <div class="tabla-container shadow-sm rounded-4 overflow-hidden ">

            <!-- === BARRA DE BÚSQUEDA === -->
            <div class="tabla-busqueda d-flex align-items-center p-3 border-bottom bg-white gap-2">

                <asp:Label ID="lblResultados" runat="server" CssClass="flex-shrink-0 me-3 ColorLetras"></asp:Label>

                <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control bg-light flex-grow-1" placeholder=" Buscar usuario..." />


                <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-outline-primary flex-shrink-0 ms-3" OnClick="btnBuscar_Click">
                     Buscar
                </asp:LinkButton>

            </div>

            <!-- === GRIDVIEW === -->
            <div class="tabla-datos">
                <asp:HiddenField ID="hfIdUsuarioSeleccionado" runat="server" ClientIDMode="Static" />

                <asp:GridView ID="dgvUsuario" runat="server"
                    AutoGenerateColumns="False"
                    AllowPaging="True"
                    PageSize="10"
                    CssClass="table table-borderless text-center align-middle m-0"
                    OnPageIndexChanging="dgvUsuario_PageIndexChanging"
                    OnRowDataBound="dgvUsuario_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Código" />
                        
                        <asp:TemplateField>
                            <HeaderTemplate>
                                 Nro. DOI  <i class="fa-solid fa-user"></i> 
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("DOI") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Nombre y Apellido" />

                        <asp:TemplateField>
                            <HeaderTemplate>
                                Correo institucional  <i class="fa-solid fa-envelope"></i>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("correo") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                Teléfono  <i class="fa-solid fa-phone"></i>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("telefono") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tipo" ItemStyle-Width="120px"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblRol" runat="server" CssClass="contorno-rol"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <a href="#" class="btn btn-link btnOpciones text-dark fs-4"
                                   onclick='abrirMenuOpciones(<%# Eval("Id_usuario") %>, this); return false;'>⋮</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:Button ID="btnVer" runat="server" OnClick="btnVer_Click" Style="display: none" />
                <asp:Button ID="btnEditar" runat="server" OnClick="btnEditar_Click" Style="display: none" />
                <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" Style="display: none" />
            </div>
        </div>

        <!-- === MENU OPCIONES  === -->
        <div id="menuOpciones" class="menu-opciones" style="display:none;">
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
    </div>

    <!-- === SCRIPT MENU OPCIONES === -->
    <script>
        (function () {
            let menuAbierto = null;

            window.abrirMenuOpciones = function (idUsuario, elemento) {
                const menu = document.getElementById('menuOpciones');
                const hidden = document.getElementById('hfIdUsuarioSeleccionado');

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
</asp:Content>
