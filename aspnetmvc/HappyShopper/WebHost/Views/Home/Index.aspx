<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Welcome to Happy Shopper Pet Shop</h2>
    <ul>
    <% foreach (var item in (HappyShopper.Model.Product[])ViewData["products"]) 
       { %>
     
     
        <li> 
            <%: Html.ActionLink(item.Name, "Details", new { Id = item.GetShortId() })%> <br />
            <img src="<%= item.GetThumbnailUrl() %>" />
        </li>
     
     
    <% } %>
    </ul>
</asp:Content>
