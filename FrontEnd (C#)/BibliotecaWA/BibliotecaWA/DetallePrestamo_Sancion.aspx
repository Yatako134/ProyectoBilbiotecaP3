<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="DetallePrestamo_Sancion.aspx.cs" Inherits="BibliotecaWA.DetallePrestamo_Sancion" UnobtrusiveValidationMode="None" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Scripts" runat="server">
    <style>
        .sancion-row {
            margin-bottom: 15px;
            position: relative; /* necesario para el posicionamiento de la X */
            padding: 10px; /* opcional, para que no esté pegada al borde */
            border: 1px solid #e0e0e0; /* opcional, para dar un límite sutil */
            border-radius: 5px; /* opcional */
        }

            .sancion-row .btn-remove-sancion {
                position: absolute;
                top: 5px;
                right: 5px;
                background: transparent;
                border: none;
                font-size: 1.2rem;
                color: #dc3545; /* rojo */
                cursor: pointer;
            }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">

    
    
     <!-- CABECERA SUPERIOR-->

    <div class="d-flex align-items-center mb-2">
        <i class="fa-solid fa-book fa-sm me-2"></i>
        <span class="fw-bold me-4">Gestion de préstamos </span>
        <span class="fw-bold me-4">
            <i class="fa-solid fa-greater-than fa-xs me-4"></i>
            <asp:Label ID="lblCabecera" runat="server"></asp:Label>
        </span>
    </div>

    <hr style="border-color: #d1d1d4; border-width: 1px; margin: 10px 0;" />

    <!-- CABECERA -->

   <div class="my-4">
       <h2 class="d-flex align-items-center gap-2">
           <asp:LinkButton ID="btnVolver" runat="server" CssClass="btn-link btn-dark text-decoration-none fa-sm" OnClick="btnVolver_Click">
            <i class="fa-regular fa-arrow-left fa-sm me-2" style="color:black;"></i>
           </asp:LinkButton>
           <asp:Label ID="lblCabeceraInf" runat="server"></asp:Label>
       </h2>
   </div>



    <!-- DETALLE EJEMPLAR -->
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

    

    <!-- DETALLE DE BIBLIOTECA -->
    <div class="card shadow-sm mb-3">
        <div>
            <div class="card-header" style="background-color: #f5faff;">
                <h5 class="mb-0">Información de biblioteca</h5>
            </div>
            <div class="card-body">
                <p class="mb-1">
                    <strong>Nombre:</strong>
                    <asp:Label ID="lblNombreBiblioteca" runat="server" CssClass="fw-normal"></asp:Label>
                </p>
                <p class="mb-1">
                    <strong>Ubicacion:</strong>
                    <asp:Label ID="lblLocacion" runat="server" CssClass="fw-normal"></asp:Label>
                </p>
            </div>
        </div>
    </div>



    <!-- DETALLE DE PRÉSTAMO -->

    <div class="card shadow-sm mb-3">
        <!-- Cabecera -->
        <div class="card-header" style="background-color: #f5faff;">
            <h5 class="mb-0">Detalles de préstamo</h5>
        </div>

        <!-- Cuerpo -->
        <div class="card-body">

            <!-- Primera fila: Código universitario y Nombre (readonly gris) -->
            <div class="row mb-4">
                <div class="col-md-6">
                    <label for="txtCodigo" class="form-label fw-bold">Código universitario</label>
                    <asp:TextBox ID="txtCodigo" runat="server"
                        CssClass="form-control bg-light text-muted"
                        ReadOnly="true"
                        Enabled="false"
                        Style="pointer-events: none; cursor: not-allowed;"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label for="txtNombre" class="form-label fw-bold">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server"
                        CssClass="form-control bg-light text-muted"
                        ReadOnly="true"
                        Enabled="false"
                        Style="pointer-events: none; cursor: not-allowed;"></asp:TextBox>
                </div>
            </div>



            <!-- Fila condicional solo para modo editar -->
            <asp:Panel ID="pnlFilaEditar" runat="server" Visible="false" CssClass="mb-3">

                <hr style="border-color: #d1d1d4; border-width: 1px; margin: 10px 0;" />

                <div class="col-md-6">
                    <label class="form-label fw-bold">Fecha de Devolución</label>
                    <!-- TextBox tipo date -->
                    <<asp:TextBox ID="txtFechaDevo" runat="server" CssClass="form-control mb-3"
                        placeholder="dd/mm/yyyy" TextMode="Date"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="rfvFecha" runat="server"
                        ControlToValidate="txtFechaDevo"
                        ErrorMessage="Debe seleccionar una fecha."
                        ForeColor="Red" Display="Dynamic" />

                    <asp:CustomValidator ID="cvFecha" runat="server"
                        ControlToValidate="txtFechaDevo"
                        OnServerValidate="cvFecha_ServerValidate"
                        ErrorMessage="La fecha debe estar entre hoy y un año a partir de hoy."
                        ForeColor="Red" Display="Dynamic" />
                </div>

                <div>
                    <asp:Panel ID="pnlSanciones" runat="server" CssClass="mb-3">
                        <div id="divSancionesContainer">
                            <!-- Filas dinámicas se agregan aquí -->
                        </div>

                        <!-- Botón añadir sanción -->
                        <div class="mt-2">
                            <input type="button" id="btnAddSancion" value="➕ Añadir Sanción"
                                class="btn btn-primary w-100" clientidmode="Static" />
                        </div>
                    </asp:Panel>

                    <script type="text/javascript">
                        var sancionCounter = 0;

                        // Array de tipos de sanción pasado desde el CodeBehind
                        var tiposDeSancion = <%= new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(TiposDeSancion) %>;

                        document.getElementById("btnAddSancion").addEventListener("click", function () {
                            sancionCounter++;

                            // Contenedor principal de la sanción
                            var divRow = document.createElement("div");
                            divRow.className = "sancion-row";
                            divRow.id = "sancionRow_" + sancionCounter;
                            divRow.style.position = "relative"; // necesario para la X

                            // --------------------
                            // Botón eliminar X
                            // --------------------
                            var btnRemove = document.createElement("button");
                            btnRemove.type = "button";
                            btnRemove.innerHTML = "&times;"; // símbolo X
                            btnRemove.className = "btn-remove-sancion";
                            btnRemove.addEventListener("click", function () {
                                divRow.remove(); // elimina el bloque completo
                            });
                            divRow.appendChild(btnRemove);

                            // --------------------
                            // Campos Tipo y Duración en la misma fila
                            // --------------------
                            var divRowTop = document.createElement("div");
                            divRowTop.className = "row mb-3";

                            // Tipo de sanción como dropdown
                            var divTipoCol = document.createElement("div");
                            divTipoCol.className = "col-md-6 mb-2";

                            var labelTipo = document.createElement("label");
                            labelTipo.className = "form-label";
                            labelTipo.innerText = "Tipo de sanción";

                            var selectTipo = document.createElement("select");
                            selectTipo.name = "txtTipoSancion_" + sancionCounter;
                            selectTipo.className = "form-control";

                            // Llenar opciones desde el array tiposDeSancion
                            tiposDeSancion.forEach(function (tipo) {
                                var option = document.createElement("option");
                                option.value = tipo;
                                option.text = tipo;
                                selectTipo.appendChild(option);
                            });

                            divTipoCol.appendChild(labelTipo);
                            divTipoCol.appendChild(selectTipo);

                            // Duración
                            var divDuracionCol = document.createElement("div");
                            divDuracionCol.className = "col-md-6 mb-2";

                            var labelDuracion = document.createElement("label");
                            labelDuracion.className = "form-label";
                            labelDuracion.innerText = "Duración (días)";

                            var inputDuracion = document.createElement("input");
                            inputDuracion.type = "text";
                            inputDuracion.name = "txtDuracion_" + sancionCounter;
                            inputDuracion.placeholder = "Ingrese duración";
                            inputDuracion.className = "form-control";

                            divDuracionCol.appendChild(labelDuracion);
                            divDuracionCol.appendChild(inputDuracion);

                            divRowTop.appendChild(divTipoCol);
                            divRowTop.appendChild(divDuracionCol);

                            // --------------------
                            // Justificación debajo
                            // --------------------
                            var divJustificacion = document.createElement("div");
                            divJustificacion.className = "mb-3";

                            var labelJustificacion = document.createElement("label");
                            labelJustificacion.className = "form-label";
                            labelJustificacion.innerText = "Justificación";

                            var inputJustificacion = document.createElement("textarea");
                            inputJustificacion.name = "txtJustificacion_" + sancionCounter;
                            inputJustificacion.placeholder = "Ingrese la justificación";
                            inputJustificacion.className = "form-control";
                            inputJustificacion.rows = 3;

                            divJustificacion.appendChild(labelJustificacion);
                            divJustificacion.appendChild(inputJustificacion);

                            // --------------------
                            // Agregar todo al contenedor principal
                            // --------------------
                            divRow.appendChild(divRowTop);
                            divRow.appendChild(divJustificacion);

                            // Agregar la sanción al contenedor
                            document.getElementById("divSancionesContainer").appendChild(divRow);
                        });
                    </script>

                </div>
            </asp:Panel>


            <hr style="border-color: #d1d1d4; border-width: 1px; margin: 10px 0;" />
            <!-- Tipo de usuario y Prestamos vigentes -->
            <div class="mb-3">
                <div class="d-flex justify-content-between align-items-center">
                    <div class="d-flex align-items-center gap-2">
                        <span class="fw-bold">Tipo de usuario :</span>
                        <asp:Label ID="lblTipoUsuario" runat="server" ></asp:Label>
                    </div>
                    <div class="d-flex align-items-center gap-2">
                        <span class="fw-bold">Préstamos vigentes :</span>
                        <asp:Label ID="lblPrestamosVigentes" runat="server"></asp:Label>
                    </div>
                </div>
                <hr style="border-color: #d1d1d4; border-width: 1px; margin: 10px 0;" />
            </div>

            <!-- Limite de días y Limite de préstamos -->
            <div class="mb-3">
                <div class="d-flex justify-content-between align-items-center">
                    <div class="d-flex align-items-center gap-2">
                        <span class="fw-bold">Límite de días :</span>
                        <asp:Label ID="lblLimiteDias" runat="server" ></asp:Label>
                    </div>
                    <div class="d-flex align-items-center gap-2">
                        <span class="fw-bold">Límite de préstamos :</span>
                        <asp:Label ID="lblLimitePrestamos" runat="server" ></asp:Label>
                    </div>
                </div>
                <hr style="b order-color: #d1d1d4; border-width: 1px; margin: 10px 0;" />
            </div>

            <!-- Fecha de préstamo -->
            <div class="mb-3">
                <div class="d-flex align-items-center gap-2">
                    <span class="fw-bold">Fecha de préstamo :</span>
                    <asp:Label ID="lblFechaPrestamo" runat="server" Text="10/10/2025 - 11:00 am"></asp:Label>
                </div>
                <hr style="border-color: #d1d1d4; border-width: 1px; margin: 10px 0;" />
            </div>

            <!-- Fecha de vencimiento y mensaje con leve separación -->
            <div class="mb-3 d-flex align-items-center justify-content-start gap-4">
                <div class="d-flex align-items-center gap-2">
                    <span class="fw-bold">Fecha de vencimiento :</span>
                    <asp:Label ID="lblFechaVencimiento" runat="server"></asp:Label>
                </div>

                <!-- Mensaje personalizado a la derecha, con leve separación -->
                <asp:Label ID="lblMensajePersonalizado" runat="server" CssClass="text-danger fw-bold"></asp:Label>
            </div>

        </div>
    </div>

    <!-- Botón Guardar cambios, solo visible en modo editar -->
    <asp:Panel ID="pnlBotonGuardar" runat="server" Visible="false">
        <div class="d-flex justify-content-end mb-4">
            <asp:Button ID="btnGuardarCambios" runat="server"
                Text="Guardar cambios"
                CssClass="btn btn-success"
                OnClick="btnGuardarCambios_Click" />
        </div>
    </asp:Panel>


</asp:Content>
