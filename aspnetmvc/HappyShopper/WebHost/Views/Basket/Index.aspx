<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index.aspx
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Your Shopping Basket</h2>
    <% var basket = (HappyShopper.Model.Basket)ViewData["Basket"]; %>
    <% using(Html.BeginForm("Update", "Basket", FormMethod.Post)) { %>

        <table>
            <thead>
                <tr>
                    <td>Item</td>
                    <td>Quantity</td>
                    <td>Unit Cost</td>
                    <td>Cost</td>
                    <td></td>
                </tr>
            </thead>
        <% foreach (var item in basket.BasketItems) 
           { %>
     
     
            <tr> 
                <td><%= item.Product.Name %></td> 
                <td><%: Html.TextBox("Quantity_" + item.Product.GetShortId(), item.Quantity, new { Size = 6 })%></td>
                <td><%= item.Product.Price %></td>
                <td><%= item.GetTotalPrice() %></td>
                <td><%: Html.ActionLink("Remove", "Remove", new { Id = item.Product.GetShortId() })%></td>
            </tr>     
     
        <% } %>
        <tfoot>
            <tr> 
                <td></td> 
                <td></td>
                <td><strong>Total:</strong></td>
                <td><%= basket.GetTotalPrice() %></td>
                <td></td>
            </tr>     
        </tfoot>
        </table>
        <input type="submit" value="Update" />
    <% } %>

</asp:Content>
