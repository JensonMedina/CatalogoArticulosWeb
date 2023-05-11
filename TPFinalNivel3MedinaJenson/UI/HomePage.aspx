<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="UI.HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Este es el home</h1>
    <div class="row row-cols-1 row-cols-md-3 g-4">
        <asp:Repeater ID="repRep" runat="server">
            <ItemTemplate>
                <div class=" col mb-3">
                    <div class="card"">
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
