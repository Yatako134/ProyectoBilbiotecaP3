<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="DetallePrestamo_Sancion.aspx.cs" Inherits="BibliotecaWA.DetallePrestamo_Sancion" %>

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

    <style>
        .btn-guardar-personalizado {
            background-color: #006deb !important; /* azul que quieres */
            color: white !important; /* texto blanco */
            border: none; /* opcional: quitar borde */
        }

            .btn-guardar-personalizado:hover {
                background-color: #0053b3 !important; /* opcional: hover más oscuro */
                color: white !important;
            }
    </style>

    <style>
        .btn-simulado-apagado {
            opacity: 0.5;
            pointer-events: none; /* ignora clicks */
            cursor: not-allowed;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="server">

    
    
     <!-- CABECERA SUPERIOR-->

    <div class="d-flex align-items-center mb-2">
        <span class="me-4">Gestion de préstamos </span>
        <span class="fw-bold me-4">
            <i class="fa-solid fa-greater-than fa-xs me-4"></i>
            <asp:Label ID="lblCabecera" runat="server"></asp:Label>
        </span>
    </div>

    <hr />

    <!-- CABECERA -->

    <div class="text-start mb-4">
        <h2 class="fw-bold fs-2">
            <asp:LinkButton ID="btnVolver" runat="server" CssClass="btn-link btn-dark text-decoration-none fa-sm" OnClick="btnVolver_Click">
         <i class="fa-regular fa-arrow-left fa-sm me-2 " style="color:black;"></i>
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
                <div class="d-flex mb-2">
                    <strong style="width: 200px;">Nombre:</strong>
                    <!-- ancho fijo -->
                    <asp:Label ID="lblBiblioteca" runat="server" CssClass="fw-normal"></asp:Label>
                </div>
                <div class="d-flex">
                    <strong style="width: 200px;">Código:</strong>
                    <!-- mismo ancho que el anterior -->
                    <asp:Label ID="lblCodEjem" runat="server" CssClass="fw-normal"></asp:Label>
                </div>
                <div class="d-flex">
                    <strong style="width: 200px;">Ubicación:</strong>
                    <!-- mismo ancho que el anterior -->
                    <asp:Label ID="lblLocacion" runat="server" CssClass="fw-normal"></asp:Label>
                </div>
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



            <asp:Panel ID="PanelEditarSancion" runat="server" Visible="false" CssClass="mb-3">
                <div class="sancion-row">
                    <div class="row mb-3">
                        <div class="col-md-6 mb-2">
                            <label class="form-label">Fecha de inicio de la sanción</label>
                            <asp:Label ID="LabelInicioFecha" runat="server" CssClass="form-control bg-light"></asp:Label>
                        </div>
                        <div class="col-md-6 mb-2">
                            <label class="form-label">Fecha de fin de la sanción</label>
                            <asp:Label ID="LabelFinFecha" runat="server" CssClass="form-control bg-light"></asp:Label>
                        </div>
                    </div>
                    <!-- Tipo -->
                    <div class="row mb-3">
                        <div class="col-md-6 mb-2">
                            <label class="form-label">Tipo de sanción</label>

                            <asp:DropDownList ID="ddlTipoSancionUnica" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Entrega tardía" Value="Entrega tardía"></asp:ListItem>
                                <asp:ListItem Text="Daño de material" Value="Daño de material"></asp:ListItem>
                            </asp:DropDownList>

                        </div>

                        <!-- Días -->
                        <div class="col-md-6 mb-2">
                            <label class="form-label">Duración (días)</label>
                            <asp:TextBox ID="txtDuracionUnica" runat="server" CssClass="form-control" TextMode="Number"
                                placeholder="Ingrese duración"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Justificación -->
                    <div class="mb-3">
                        <label class="form-label">Justificación</label>
                        <asp:TextBox ID="txtJustificacionUnica" runat="server"
                            CssClass="form-control" TextMode="MultiLine" Rows="3"
                            placeholder="Ingrese la justificación"></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>


            <asp:Panel ID="pnlSancionUnica" runat="server" Visible="false" CssClass="sancion-row">

                <div class="row mb-3">
                    <div class="col-md-6 mb-2">
                        <label class="form-label">Fecha de inicio de la sanción</label>
                        <asp:Label ID="lblFechaIni" runat="server" CssClass="form-control bg-light"></asp:Label>
                    </div>
                    <div class="col-md-6 mb-2">
                        <label class="form-label">Fecha de fin de la sanción</label>
                        <asp:Label ID="lblFechaFin" runat="server" CssClass="form-control bg-light"></asp:Label>
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-6 mb-2">
                        <label class="form-label">Tipo de sanción</label>
                        <asp:Label ID="lblTipoSancion" runat="server" CssClass="form-control bg-light"></asp:Label>
                    </div>

                    <div class="col-md-6 mb-2">
                        <label class="form-label">Duración (días)</label>
                        <asp:Label ID="lblDuracion" runat="server" CssClass="form-control bg-light"></asp:Label>
                    </div>
                </div>

                <div class="mb-3">
                    <label class="form-label">Justificación</label>
                    <asp:Label ID="lblJustificacion" runat="server" CssClass="form-control bg-light"></asp:Label>
                </div>

            </asp:Panel>




            <!-- Fila condicional solo para modo editar -->
            <asp:Panel ID="pnlFilaEditar" runat="server" Visible="false" CssClass="mb-3">

                <hr style="border-color: #d1d1d4; border-width: 1px; margin: 10px 0;" />

                <div class="col-md-6">
                    <label class="form-label fw-bold">Fecha de Devolución</label>

                    <asp:TextBox ID="txtFechaDevo" runat="server"
                        CssClass="form-control mb-3"
                        Enabled="false"
                        TextMode="SingleLine" 
                        ReadOnly="true"
                        >
                    </asp:TextBox>
                </div>

                <div>
                    <asp:Panel ID="pnlSanciones" runat="server" CssClass="mb-3">
                        <div id="divSancionesContainer">
                            <!-- Filas dinámicas se agregan aquí -->
                        </div>

                        <!-- Botón añadir sanción -->
                        <div class="mt-2">
                            <input type="button" id="btnAddSancion" value=" +  Añadir Sanción"
                                class="btn w-100"
                                clientidmode="Static"
                                style="background-color: #f5faff; color: #1c7ced; border: 1px solid #1c7ced;" />
                        </div>

                    </asp:Panel>
                    
                    <script type="text/javascript">
                        var sancionCounter = 0;

                        var tiposDeSancion = <%= new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(TiposDeSancion) %>;

                        var btnAdd = document.getElementById("btnAddSancion");

                        btnAdd.addEventListener("click", function () {

                            // Solo permitir 1 sanción a la vez
                            if (document.querySelectorAll(".sancion-row").length >= 1) {
                                return;
                            }

                            sancionCounter++;

                            // Desactivar botón después de presionarlo
                            btnAdd.disabled = true;
                            btnAdd.style.opacity = "0.5";
                            btnAdd.style.cursor = "not-allowed";

                            var divRow = document.createElement("div");
                            divRow.className = "sancion-row";
                            divRow.id = "sancionRow_" + sancionCounter;
                            divRow.style.position = "relative";

                            // Botón eliminar
                            var btnRemove = document.createElement("button");
                            btnRemove.type = "button";
                            btnRemove.innerHTML = "X";
                            btnRemove.className = "btn-remove-sancion";
                            btnRemove.style.fontSize = "1.5rem";
                            btnRemove.style.color = "#000";
                            btnRemove.style.fontWeight = "bold";

                            btnRemove.addEventListener("click", function () {
                                divRow.remove();

                                // Reactivar el botón
                                btnAdd.disabled = false;
                                btnAdd.style.opacity = "1";
                                btnAdd.style.cursor = "pointer";
                            });

                            divRow.appendChild(btnRemove);

                            // --- resto de tu código tal cual ---
                            var divRowTop = document.createElement("div");
                            divRowTop.className = "row mb-3";

                            var divTipoCol = document.createElement("div");
                            divTipoCol.className = "col-md-6 mb-2";

                            var labelTipo = document.createElement("label");
                            labelTipo.className = "form-label";
                            labelTipo.innerText = "Tipo de sanción";

                            var selectTipo = document.createElement("select");
                            selectTipo.name = "txtTipoSancion_" + sancionCounter;
                            selectTipo.className = "form-control";
                            selectTipo.required = true;

                            tiposDeSancion.forEach(function (tipo) {
                                var option = document.createElement("option");
                                option.value = tipo;
                                option.text = tipo;
                                selectTipo.appendChild(option);
                            });

                            divTipoCol.appendChild(labelTipo);
                            divTipoCol.appendChild(selectTipo);

                            var divDuracionCol = document.createElement("div");
                            divDuracionCol.className = "col-md-6 mb-2";

                            var labelDuracion = document.createElement("label");
                            labelDuracion.className = "form-label";
                            labelDuracion.innerText = "Duración (días)";

                            var inputDuracion = document.createElement("input");
                            inputDuracion.type = "number";
                            inputDuracion.name = "txtDuracion_" + sancionCounter;
                            inputDuracion.placeholder = "Ingrese duración";
                            inputDuracion.className = "form-control";
                            inputDuracion.required = true;
                            inputDuracion.min = 1;
                            inputDuracion.step = 1;

                            divDuracionCol.appendChild(labelDuracion);
                            divDuracionCol.appendChild(inputDuracion);

                            divRowTop.appendChild(divTipoCol);
                            divRowTop.appendChild(divDuracionCol);

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
                            inputJustificacion.required = true;
                            inputJustificacion.maxLength = "150";

                            divJustificacion.appendChild(labelJustificacion);
                            divJustificacion.appendChild(inputJustificacion);

                            divRow.appendChild(divRowTop);
                            divRow.appendChild(divJustificacion);

                            document.getElementById("divSancionesContainer").appendChild(divRow);
                        });
                    </script>


                </div>

                <div>

                    <!-- SANCIÓN AUTOMÁTICA (único bloque fijo) -->
                    <asp:Panel ID="pnlSancionAutomatica" runat="server" Visible="false" CssClass="mt-3">

                        <div class="alert alert-danger fw-bold mb-2">
                            Sanción automática generada por devolución tardía.
                        </div>

                        <div class="sancion-row" style="border: 1px solid #ccc; padding: 12px; border-radius: 8px; background: #ffffff; position: relative;">

                            <!-- Parte superior (2 columnas) -->
                            <div class="row mb-3">

                                <!-- Tipo de sanción (pero fijo, no editable) -->
                                <div class="col-md-6 mb-2">
                                    <label class="form-label fw-semibold">Tipo de sanción</label>
                                    <input type="text" class="form-control" value="Entrega tardia" readonly style="background: #e9ecef;">
                                </div>

                                <!-- Días -->
                                <div class="col-md-6 mb-2">
                                    <label class="form-label fw-semibold">Duración (días)</label>
                                    <input type="number" step="1" id="txtDiasAuto" class="form-control"
                                        min="1" placeholder="Ingrese días" required/>
                                </div>

                            </div>

                            <!-- Justificación -->
                            <div class="mb-3">
                                <label class="form-label fw-semibold">Justificación</label>
                                <textarea id="txtJustificacionAuto" class="form-control" rows="3"
                                    placeholder="Descripción" required maxlength="150" ></textarea>
                            </div>
                            <!-- HiddenFields que sí se envían al servidor -->
                            <asp:HiddenField ID="hfDiasAuto" runat="server" />
                            <asp:HiddenField ID="hfJustificacionAuto" runat="server" />
                            <script>
                                function pasarDatosAuto() {
                                    document.getElementById("<%= hfDiasAuto.ClientID %>").value =
                                        document.getElementById("txtDiasAuto").value;

                                    document.getElementById("<%= hfJustificacionAuto.ClientID %>").value =
                                        document.getElementById("txtJustificacionAuto").value;
                                }
                            </script>

                        </div>


                    </asp:Panel>
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
                <hr style="border-color: #d1d1d4; border-width: 1px; margin: 10px 0;" />
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
                Text="Guardar cambios →"
                CssClass="btn btn-guardar-personalizado"
                OnClick="btnGuardarCambios_Click" 
                OnClientClick="pasarDatosAuto();"/>
        </div>
    </asp:Panel>



</asp:Content>
