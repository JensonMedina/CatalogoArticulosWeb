<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="UI.AdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row mb-5">
        <asp:GridView ID="dgvArticulos" runat="server" DataKeyNames="Id"
            CssClass="table" AutoGenerateColumns="false"
            OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged"
            OnPageIndexChanging="dgvArticulos_PageIndexChanging"
            AllowPaging="True" PageSize="5">
            <Columns>
                <asp:BoundField HeaderText="Id" DataField="Id" />
                <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                <asp:BoundField HeaderText="Marca" DataField="Marca.Descripcion" />
                <asp:BoundField HeaderText="Categoria" DataField="Categoria.Descripcion" />
                <asp:BoundField HeaderText="Precio" DataField="Precio" />
                <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✍" />
            </Columns>
        </asp:GridView>
    </div>

    <div class="row mb-5">
        <div class="col">
            <div class="mb-3">
                <asp:Label Text="Codigo" CssClass="form-label" runat="server" />
                <asp:TextBox ID="txtCodigo" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <asp:Label Text="Nombre" CssClass="form-label" runat="server" />
                <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <asp:Label Text="Descripción" CssClass="form-label" runat="server" />
                <asp:TextBox ID="txtDescripcion" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <asp:Label Text="Marca" CssClass="form-label" runat="server" />
                <asp:DropDownList ID="ddlMarca" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <asp:Label Text="Categoria" CssClass="form-label" runat="server" />
                <asp:DropDownList ID="ddlCategoria" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <asp:Label Text="Precio" CssClass="form-label" runat="server" />
                <asp:TextBox ID="txtPrecio" CssClass="form-control" runat="server" />
            </div>
        </div>
        <div class="col">
            <div class="mb-3">
                <label class="form-label">Imagen Articulo</label>
                <input type="file" id="txtImagen" runat="server" class="form-control" />
            </div>
            <asp:Image ID="imgArticulo" ImageUrl="./imagenes/placeholder.png"
                runat="server" CssClass="img-fluid mb-3" Width="600px" Height="300px" />
        </div>
    </div>

    <div class="row mb-5">
        <div class="col">
            <asp:Button ID="btnAgregar" OnClick="btnAgregar_Click" CssClass="btn btn-primary" runat="server" Text="Agregar" />
        </div>
        <%if (Id != null)
            { %>
        <div class="col">
            <asp:Button ID="btnModificar" OnClick="btnModificar_Click" CssClass="btn btn-warning" runat="server" Text="Modificar" />
        </div>
        <div class="col">
            <button type="button" class="btn btn-danger" data-toggle="modal" data-target="#myModal">Eliminar</button>
           
           
            <div class="modal" id="myModal" tabindex="-1">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Eliminar articulo</h5>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body">
                            <p>¿Esta seguro de eliminar definitivamente este articulo?</p>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            <asp:Button ID="btnEliminar" OnClick="btnEliminar_Click" CssClass="btn btn-primary" runat="server" Text="Eliminar" />
                        </div>
                    </div>
                </div>
            </div>
            
        </div>
        <%} %>
        <div class="col">
            <asp:Button ID="btnLimpiar" OnClick="btnLimpiar_Click" CssClass="btn btn-secondary" runat="server" Text="Limpiar" />
        </div>
    </div>


</asp:Content>
