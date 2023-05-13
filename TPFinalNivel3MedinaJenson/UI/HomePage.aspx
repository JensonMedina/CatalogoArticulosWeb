<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="UI.HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .mi-card:hover {
            cursor: default;
            /*transform: translateY(-5px);*/
            /*border: 1px solid #808080;*/
            /*box-shadow: 1px 4px 15px rgba(0, 0, 0, 0.32);*/
            box-shadow: -1px 1px 29px 3px rgba(0,0,0,0.55);
            -webkit-box-shadow: -1px 1px 29px 3px rgba(0,0,0,0.55);
            -moz-box-shadow: -1px 1px 29px 3px rgba(0,0,0,0.55);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Este es el home</h1>
    <div class="row row-cols-1 row-cols-md-3 g-4">
        <asp:Repeater ID="repRep" runat="server">
            <ItemTemplate>
                <div class=" col mb-3">
                    <div class="card mi-card">
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
