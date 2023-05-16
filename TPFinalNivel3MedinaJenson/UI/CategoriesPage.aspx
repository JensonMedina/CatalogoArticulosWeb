<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CategoriesPage.aspx.cs" Inherits="UI.CategoriesPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Aca voy a tirar la magia para filtras las busquedas</h1>


    <div class="row mt-5 mb-5">
        <div class="col">
            <asp:DropDownList ID="ddlCampo" CssClass="form-select" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
        </div>
        <div class="col">
            <asp:DropDownList ID="ddlCriterio" ClientIDMode="Static" CssClass="form-select" runat="server"></asp:DropDownList>
        </div>
        <div class="col">
            <asp:TextBox ID="txtFiltro" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col">
            <asp:Button ID="btnBuscar" CssClass="btn btn-primary" OnClientClick='return validar("#<%ddlCriterio.ID%>", "#<%txtFiltro.ID%>");' OnClick="btnBuscar_Click"  runat="server" Text="Buscar" />
        </div>
    </div>


    <div class="row row-cols-1 row-cols-md-3 g-4 mb-5">
        <asp:Repeater ID="repRep" runat="server">
            <ItemTemplate>
                <div class="col-lg-4 col-md-6">
                    <div class="card h-100 mi-card">
                        <img src="<%#Eval("ImagenUrl") %>" class="card-img-top" alt="Articulo">
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Nombre")%></h5>
                            <p class="card-text"><%#Eval("Descripcion")%></p>
                        </div>
                        <ul class="list-group list-group-flush">
                            <li class="list-group-item"><%#Eval("Precio")%></li>
                            <li class="list-group-item">A second item</li>
                            <li class="list-group-item">A third item</li>
                        </ul>
                        <div class="card-body">
                            <a href="DetallePage.aspx?id=<%#Eval("Id")%>" class="card-link">Ver detalle</a>
                            <a href="FavoritosPage.aspx?id=<%#Eval("Id")%>" class="card-link">Añadir a favoritos</a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>


</asp:Content>
