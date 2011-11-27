<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% var product = (HappyShopper.Model.Product)ViewData["product"]; %>
    <h2><%= product.Name %> - <%= product.Price %> €</h2>
    <p>
    <img src="<%= product.GetMainPictureUrl() %>" /> <br />
    <%: Html.ActionLink("Add to basket", "Add", "Basket", new { Id = product.GetShortId() }, new { Hack = "" })%>
    </p>
</asp:Content>
