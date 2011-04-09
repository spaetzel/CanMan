<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Routes.aspx.cs" Inherits="CanMan.Routes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ListView ID="routesList" runat="server">
        <LayoutTemplate>
            <ul>
                <li id="itemPlaceholder" runat="server" />
            </ul>
        </LayoutTemplate>
        <ItemTemplate>
            <li><a href="routes/<%# Eval("Id") %>"><%# Eval("Name") %></a></li>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
